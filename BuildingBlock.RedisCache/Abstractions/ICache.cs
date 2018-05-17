using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlock.RedisCache.Abstractions
{
    public interface ICache
    {
        Task<TObject> GetAsync<TObject>(string key);
    }
}
