using System;
using System.Collections.Generic;
using System.Text;

namespace csharp_interviews.exos.output
{
    public class Forecast
    {
        public int Temperature { get; set; }
        public int Pressure { get; set; }
    }

    public static class Program3
    {
        public static void ChangeTheString(string weather) { weather = "sunny"; }
        public static void ChangeTheArray(string[] rainydays) { rainydays[1] = "Sunday"; }
        public static void ChangeTheClassinstance(Forecast forecast) { forecast.Temperature  = 35; }

        public static void _Main()
        {
            string weather = "rainy";
            ChangeTheString(weather);
            Console.WriteLine("The weather is " + weather);

            string[] rainydays = new[] { "Monday", "Friday" };
            ChangeTheArray(rainydays);
            Console.WriteLine("The rainy days were on " + rainydays[0] + " and " + rainydays[1]);

            Forecast forecast = new Forecast() { Pressure = 700, Temperature = 20 };
            ChangeTheClassinstance(forecast);
            Console.WriteLine("The temperature is " + forecast.Temperature);


        }
    }
}
