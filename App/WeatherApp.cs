using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MyWebApi
{
    public class WeatherApp : IWeather
    {
        public WeatherModel GetDateWeather(DateTime date, out string status)
        {
            WeatherModel wm = new WeatherModel();
            try
            {
                wm.Date = date;
                wm.TemperatureC = 33;
                wm.Summary = "Hot";
                status = "ok";
            }
            catch (Exception e)
            {
                status = "error";
                throw (e);
            }
            return wm;
        }

        public List<WeatherModel> GetRangeDateWeather(DateTime start, DateTime end, out string status)
        {
            var weatherInfoList = new List<WeatherModel>();
            try
            {
                var dateSpan = (end - start).TotalDays;
                for (var i = 0; i <= dateSpan; i++)
                {
                    weatherInfoList.Add(new WeatherModel
                    {
                        Date = start.AddDays(i),
                        TemperatureC = 33,
                        Summary = "Hot"
                    });
                }
                status = "ok";
            }
            catch (Exception e)
            {
                status = "error";
                throw (e);
            }
            return weatherInfoList;
        }
    }

}
