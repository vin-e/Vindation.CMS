
namespace Vin.Core.Data.DataProviders
{
    public partial class EfDataProviderManager : BaseDataProviderManager
    {
        public override IDataProvider LoadDataProvider()
        {
            return new SqlServerDataProvider();
        }
    }
}
