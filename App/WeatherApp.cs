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
        public WeatherModel GetDateWeather(DateTime date)
        {
            date = (date == null) ? DateTime.Now : date;
            return new WeatherModel
            {
                Date = date,
                TemperatureC = 33,
                Summary = "Hot"
            };

        }

        public List<WeatherModel> GetRangeDateWeather(DateTime start, DateTime end)
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
            }
            catch (Exception e)
            {
                throw (e);

            }
            return weatherInfoList;
        }
    }

}
