using System.Collections.Generic;

namespace MyWebApi.Services
{
    public interface IBaseService<T>
    {
        List<T> Get();
        T Get(string id);
        T Create(T obj);
        void Remove(T objIn);
        void Remove(string id);
    }
}