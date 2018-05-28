using System.Threading.Tasks;

namespace BuildingBlock.Core.Caching
{
    public interface ICache
    {
        Task<TObject> GetAsync<TObject>(string key);
    }
}
