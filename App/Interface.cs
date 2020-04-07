using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MyWebApi
{
    public interface IWeather
    {
        WeatherModel GetDateWeather(DateTime date, out string status);
        List<WeatherModel> GetRangeDateWeather(DateTime start, DateTime end, out string status);
    }

}
