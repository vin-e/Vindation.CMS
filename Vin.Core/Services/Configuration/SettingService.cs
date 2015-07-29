using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Vin.Core.Configuration;
using Vin.Core.Data.Repositories;
using Vin.Core.Model.Configuration;
using Vin.Core.Utilities;

namespace Vin.Core.Services.Configuration
{
    public partial class SettingService : ISettingService
    {
        private readonly IRepository<Setting> _settingRepository;

        public SettingService(IRepository<Setting> settingRepository)
        {
            this._settingRepository = settingRepository;
        }

        public virtual Setting GetByID(int id)
        {
            if (id == 0)
                return null;

            return _settingRepository.GetById(id);
        }

        public virtual void Insert(Setting entity)
        {
            if (entity == null)
                throw new ArgumentNullException("setting");

            _settingRepository.Insert(entity);
        }

        public virtual void Update(Setting entity)
        {
            if (entity == null)
                throw new ArgumentNullException("setting");

            _settingRepository.Update(entity);
        }

        public virtual void Delete(Setting entity)
        {
            if (entity == null)
                throw new ArgumentNullException("setting");

            _settingRepository.Delete(entity);
        }

        public virtual T GetSettingByKey<T>(string key, T defaultValue = default(T))
        {
            if (String.IsNullOrEmpty(key))
                return defaultValue;

            var settings = GetAllSettingsDictionary();
            key = key.Trim().ToLowerInvariant();
            if (settings.ContainsKey(key))
            {
                var settingsByKey = settings[key];
                var setting = settingsByKey.FirstOrDefault();

                if (setting != null)
                    return ReflectionUtilities.To<T>(setting.Value);
            }

            return defaultValue;
        }

        public virtual void SetSetting<T>(string key, T value)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            key = key.Trim().ToLowerInvariant();
            string valueStr = TypeDescriptor.GetConverter(typeof(T)).ConvertToInvariantString(value);

            var allSettings = GetAllSettingsDictionary();
            var settingForCaching = allSettings.ContainsKey(key) ?
                allSettings[key].FirstOrDefault() : null;
            if (settingForCaching != null)
            {
                //update
                var setting = GetByID(settingForCaching.ID);
                setting.Value = valueStr;
                Update(setting);
            }
            else
            {
                //insert
                var setting = new Setting()
                {
                    Name = key,
                    Value = valueStr
                };
                Insert(setting);
            }
        }

        public virtual IList<Setting> GetAllSettings()
        {
            return _settingRepository.Table.OrderBy(ob => ob.Name).ToList();
        }

        public virtual bool SettingExists<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector) where T : ISettings, new()
        {
            var member = keySelector.Body as MemberExpression;
            if (member == null)
            {
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    keySelector));
            }

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException(string.Format(
                       "Expression '{0}' refers to a field, not a property.",
                       keySelector));
            }

            string key = typeof(T).Name + "." + propInfo.Name;

            string setting = GetSettingByKey<string>(key);
            return setting != null;
        }

        public virtual T LoadSetting<T>() where T : ISettings, new()
        {
            var settings = Activator.CreateInstance<T>();

            foreach (var prop in typeof(T).GetProperties())
            {
                // get properties we can read and write to
                if (!prop.CanRead || !prop.CanWrite)
                    continue;

                var key = typeof(T).Name + "." + prop.Name;
                
                string setting = GetSettingByKey<string>(key);
                if (setting == null)
                    continue;

                if (!TypeDescriptor.GetConverter(prop.PropertyType).CanConvertFrom(typeof(string)))
                    continue;

                if (!TypeDescriptor.GetConverter(prop.PropertyType).IsValid(setting))
                    continue;

                object value = TypeDescriptor.GetConverter(prop.PropertyType).ConvertFromInvariantString(setting);

                //set property
                prop.SetValue(settings, value, null);
            }

            return settings;
        }

        public virtual void SaveSetting<T>(T settings) where T : ISettings, new()
        {
            foreach (var prop in typeof(T).GetProperties())
            {
                // get properties we can read and write to
                if (!prop.CanRead || !prop.CanWrite)
                    continue;

                if (!TypeDescriptor.GetConverter(prop.PropertyType).CanConvertFrom(typeof(string)))
                    continue;

                string key = typeof(T).Name + "." + prop.Name;
                //Duck typing is not supported in C#. That's why we're using dynamic type
                dynamic value = prop.GetValue(settings, null);
                if (value != null)
                    SetSetting(key, value);
                else
                    SetSetting(key, "");
            }
        }

        public virtual void SaveSetting<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector) where T : ISettings, new()
        {
            var member = keySelector.Body as MemberExpression;
            if (member == null)
            {
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    keySelector));
            }

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException(string.Format(
                       "Expression '{0}' refers to a field, not a property.",
                       keySelector));
            }

            string key = typeof(T).Name + "." + propInfo.Name;
            //Duck typing is not supported in C#. That's why we're using dynamic type
            dynamic value = propInfo.GetValue(settings, null);
            if (value != null)
                SetSetting(key, value);
            else
                SetSetting(key, "");
        }

        public virtual void DeleteSetting<T>() where T : ISettings, new()
        {
            var settingsToDelete = new List<Setting>();
            var allSettings = GetAllSettings();
            foreach (var prop in typeof(T).GetProperties())
            {
                string key = typeof(T).Name + "." + prop.Name;
                settingsToDelete.AddRange(allSettings.Where(x => x.Name.Equals(key, StringComparison.InvariantCultureIgnoreCase)));
            }

            foreach (var setting in settingsToDelete)
                Delete(setting);
        }

        public virtual void DeleteSetting<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector) where T : ISettings, new()
        {
            var member = keySelector.Body as MemberExpression;
            if (member == null)
            {
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    keySelector));
            }

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException(string.Format(
                       "Expression '{0}' refers to a field, not a property.",
                       keySelector));
            }

            string key = typeof(T).Name + "." + propInfo.Name;
            key = key.Trim().ToLowerInvariant();

            var allSettings = GetAllSettingsDictionary();
            var settingForCaching = allSettings.ContainsKey(key) ?
                allSettings[key].FirstOrDefault() : null;
            if (settingForCaching != null)
            {
                //update
                var setting = GetByID(settingForCaching.ID);
                Delete(setting);
            }
        }

        #region Nested classes

        [Serializable]
        public class SettingForCaching
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Value { get; set; }
            public int StoreId { get; set; }
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Gets all settings
        /// </summary>
        /// <returns>Setting collection</returns>
        protected virtual IDictionary<string, IList<SettingForCaching>> GetAllSettingsDictionary()
        {
            //TODO: need to cache this. Too many database calls

            var settings = _settingRepository.Table.OrderBy(ob => ob.Name).ToList();

            var dictionary = new Dictionary<string, IList<SettingForCaching>>();
            foreach (var s in settings)
            {
                var resourceName = s.Name.ToLowerInvariant();
                var settingForCaching = new SettingForCaching()
                {
                    ID = s.ID,
                    Name = s.Name,
                    Value = s.Value
                };
                if (!dictionary.ContainsKey(resourceName))
                {
                    //first setting
                    dictionary.Add(resourceName, new List<SettingForCaching>()
                        {
                            settingForCaching
                        });
                }
                else
                {
                    //already added
                    dictionary[resourceName].Add(settingForCaching);
                }
            }
            return dictionary;
        }

        #endregion
    }
}
