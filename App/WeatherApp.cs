using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWebApi.Models;

namespace MyWebApi
{
    public class WeatherApp : IWeather
    {
        public Car GetCar(string id, out string status)
        {
            Car wm = new Car();
            try
            {
                wm.Id = id;
                wm.CarTypeName = "BZ";
                status = "ok";
            }
            catch (Exception e)
            {
                status = "error";
                throw (e);
            }
            return wm;
        }

        public List<Car> GetAllCar(out string status)
        {
            var listCar = new List<Car>();
            try
            {
                for (var i = 0; i <= 5; i++)
                {
                    listCar.Add(new Car
                    {
                        CarTypeName = "BZ"
                    });
                }
                status = "ok";
            }
            catch (Exception e)
            {
                status = "error";
                throw (e);
            }
            return listCar;
        }
    }

}
