using System.Collections.Generic;
using System.Drawing;

namespace MyWebApi.Services
{
    public interface IDBService<T>
    {
        List<T> Get();
        T Get(string id);
        T Create(T obj);
        void Remove(T objIn);
        void Remove(string id);
    }

    public interface IQRCodeService<T>
    {
        T InitCode(string unionid, string url, int pixel);

    }
}