using System;
using System.Collections.Generic;
using System.Text;

namespace ObserverPattern
{
    public class CurrentConditionsDisplay : IObserver, IDisplayElement
    {
        private float _temperature;
        private float _humidity;
        private WeatherData _weatherData;

        public CurrentConditionsDisplay(WeatherData weatherData)
        {
            _weatherData = weatherData;
            weatherData.RegisterObserver(this);
        }

        public void Update(float temp, float humidity, float pressure)
        {
            _temperature = temp;
            _humidity = humidity;

            Display();
        }

        public void Display()
        {
            Console.WriteLine($"Current conditions: {_temperature} F degrees and {_humidity}% humidity");
        }
    }
}
