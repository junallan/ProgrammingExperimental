using System;
using System.Collections.Generic;
using System.Text;

namespace ObserverPattern
{
    public class WeatherData : ISubject
    {
        private List<IObserver> _observers;
        private float _temperature;
        private float _humidity;
        private float _pressure;

        public WeatherData()
        {
            _observers = new List<IObserver>();
        }

        public void NotifyObservers()
        {
            foreach(IObserver observer in _observers)
            {
                observer.Update();
                //observer.Update(_temperature, _humidity, _pressure);
            }
        }

        internal float GetPressure()
        {
            return _pressure;
        }

        public void RegisterObserver(IObserver o)
        {
            _observers.Add(o);
        }

        internal float GetTemperature()
        {
            return _temperature;
        }

        internal float GetHumidity()
        {
            return _humidity;
        }

        public void RemoveObserver(IObserver o)
        {
            _observers.Remove(o);
        }

        public void MeasurementsChanged()
        {
            NotifyObservers();
        }

        public void SetMeasurements(float temperature, float humidity, float pressure)
        {
            _temperature = temperature;
            _humidity = humidity;
            _pressure = pressure;
            MeasurementsChanged();
        }
    }
}
