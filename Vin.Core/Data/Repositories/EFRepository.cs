using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using Vin.Core.Model;
using Vin.Core.MultiTenancy;
using Vin.Core.Utilities;

namespace Vin.Core.Data.Repositories
{
    /// <summary>
    /// Entity Framework repository
    /// </summary>
    public partial class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IDbContext _context;
        private IDbSet<T> _entities;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="context">Object context</param>
        public EfRepository(IDbContext context)
        {
            this._context = context;
        }

        public virtual T GetById(object id)
        {
            IQueryable<T> entities = this.Entities;
            entities = Expressions.Expressions.Where(entities, "ID", (int)id, Expressions.WhereType.Equals);
            if (ReflectionUtilities.IsSubclassOfRawGeneric(typeof(BaseTenantIdEntity), typeof(T)))
            {
                var tenantInstance = HttpContext.Current.Request.GetOwinContext().GetTenantInstance();
                entities = Expressions.Expressions.Where(entities, "Tenant_ID", tenantInstance.Tenant.ID, Expressions.WhereType.Equals);
            }

            return entities.FirstOrDefault();
        }

        public virtual void Insert(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                if (ReflectionUtilities.IsSubclassOfRawGeneric(typeof(BaseTenantIdEntity), typeof(T)))
                {
                    if ((int?)typeof(BaseTenantIdEntity).GetProperty("Tenant_ID").GetValue(entity, null) == 0)
                    {
                        var tenantInstance = HttpContext.Current.Request.GetOwinContext().GetTenantInstance();
                        typeof(BaseTenantIdEntity).GetProperty("Tenant_ID").SetValue(entity, tenantInstance.Tenant.ID);
                    }
                }

                this.Entities.Add(entity);

                this._context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;

                var fail = new Exception(msg, dbEx);
                
                throw fail;
            }
        }

        public virtual void Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                if (ReflectionUtilities.IsSubclassOfRawGeneric(typeof(BaseTenantIdEntity), typeof(T)))
                {
                    if ((int?)typeof(BaseTenantIdEntity).GetProperty("Tenant_ID").GetValue(entity, null) == 0)
                    {
                        var tenantInstance = HttpContext.Current.Request.GetOwinContext().GetTenantInstance();
                        typeof(BaseTenantIdEntity).GetProperty("Tenant_ID").SetValue(entity, tenantInstance.Tenant.ID);
                    }
                }

                if (ReflectionUtilities.IsSubclassOfRawGeneric(typeof(BaseIdEntity), typeof(T)) || ReflectionUtilities.IsSubclassOfRawGeneric(typeof(BaseTenantIdEntity), typeof(T)))
                {
                    typeof(BaseTenantIdEntity).GetProperty("UpdatedDate").SetValue(entity, DateTime.UtcNow);
                }

                this._context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                var fail = new Exception(msg, dbEx);
                
                throw fail;
            }
        }

        public virtual void Delete(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                if (ReflectionUtilities.IsSubclassOfRawGeneric(typeof(BaseTenantIdEntity), typeof(T)))
                {
                    if ((int?)typeof(BaseTenantIdEntity).GetProperty("Tenant_ID").GetValue(entity, null) == 0)
                    {
                        var tenantInstance = HttpContext.Current.Request.GetOwinContext().GetTenantInstance();
                        typeof(BaseTenantIdEntity).GetProperty("Tenant_ID").SetValue(entity, tenantInstance.Tenant.ID);
                    }
                }

                this.Entities.Remove(entity);

                this._context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                var fail = new Exception(msg, dbEx);
                
                throw fail;
            }
        }

        public virtual IQueryable<T> Table
        {
            get
            {
                if (ReflectionUtilities.IsSubclassOfRawGeneric(typeof(BaseTenantIdEntity), typeof(T)))
                {
                    var tenantInstance = HttpContext.Current.Request.GetOwinContext().GetTenantInstance();
                    return Expressions.Expressions.Where(this.Entities.AsQueryable(), "Tenant_ID", tenantInstance.Tenant.ID, Expressions.WhereType.Equals);
                }

                return this.Entities;
            }
        }

        protected virtual IDbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<T>();
                return _entities;
            }
        }
    }
}
