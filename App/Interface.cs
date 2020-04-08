using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWebApi.Models;
namespace MyWebApi
{
    public interface ICar
    {
        Car GetCar(string id, out string status);
        List<Car> GetAllCar(out string status);
    }

    public interface IMongoDatabaseSettings
    {
        string CollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }

}
