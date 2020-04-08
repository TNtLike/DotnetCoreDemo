using System.Collections.Generic;
using MyWebApi.Models;

namespace MyWebApi.Services
{
    public interface ICarService
    {
        List<Car> Get();
        Car Get(string id);
        Car Create(Car car);
        void Remove(Car carIn);
        void Remove(string id);
    }
}