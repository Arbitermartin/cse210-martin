using System;
using System.Collections.Generic;

namespace ExerciseTrackingApp
{
    // Enum for Units of Measurement
    public enum Unit
    {
        Kilometers,
        Miles
    }

    // Base Activity Class
    public abstract class Activity
    {
        private DateTime _date;
        private int _length;  // Length in minutes
        private Unit _unit;

        public Activity(DateTime date, int length, Unit unit)
        {
            _date = date;
            _length = length;
            _unit = unit;
        }

        public DateTime Date => _date;
        public int Length => _length;
        public Unit MeasurementUnit => _unit;

        public abstract double GetDistance();
        public abstract double GetSpeed();
        public abstract double GetPace();

        public virtual string GetSummary()
        {
            string unitDistance = _unit == Unit.Kilometers ? "km" : "miles";
            string unitSpeed = _unit == Unit.Kilometers ? "kph" : "mph";
            string unitPace = _unit == Unit.Kilometers ? "min per km" : "min per mile";
            
            return $"{Date.ToString("dd MMM yyyy")} {this.GetType().Name} ({Length} min) - " +
                   $"Distance: {GetDistance():F1} {unitDistance}, Speed: {GetSpeed():F1} {unitSpeed}, Pace: {GetPace():F2} {unitPace}";
        }
    }

    // Derived Running Class
    public class Running : Activity
    {
        private double _distance;  // in kilometers or miles

        public Running(DateTime date, int length, double distance, Unit unit) : base(date, length, unit)
        {
            _distance = distance;
        }

        public override double GetDistance() => MeasurementUnit == Unit.Kilometers ? _distance : _distance * 0.621371;

        public override double GetSpeed() => (GetDistance() / Length) * 60;

        public override double GetPace() => Length / GetDistance();
    }

    // Derived Cycling Class
    public class Cycling : Activity
    {
        private double _speed;  // in kph or mph

        public Cycling(DateTime date, int length, double speed, Unit unit) : base(date, length, unit)
        {
            _speed = speed;
        }

        public override double GetDistance() => (_speed * Length) / 60;

        public override double GetSpeed() => MeasurementUnit == Unit.Kilometers ? _speed : _speed * 0.621371;

        public override double GetPace() => 60 / GetSpeed();
    }

    // Derived Swimming Class
    public class Swimming : Activity
    {
        private int _laps;
        private const double LapDistanceKm = 50 / 1000.0;  // 50 meters per lap, in km
        private const double LapDistanceMiles = LapDistanceKm * 0.621371;

        public Swimming(DateTime date, int length, int laps, Unit unit) : base(date, length, unit)
        {
            _laps = laps;
        }

        public override double GetDistance() => MeasurementUnit == Unit.Kilometers ? _laps * LapDistanceKm : _laps * LapDistanceMiles;

        public override double GetSpeed() => (GetDistance() / Length) * 60;

        public override double GetPace() => Length / GetDistance();
    }

    // Main Program to create instances and display summaries
    class Program
    {
        static void Main(string[] args)
        {
            List<Activity> activities = new List<Activity>
            {
                new Running(new DateTime(2022, 11, 3), 30, 4.8, Unit.Kilometers),
                new Cycling(new DateTime(2022, 11, 3), 45, 20.0, Unit.Miles),
                new Swimming(new DateTime(2022, 11, 3), 60, 30, Unit.Kilometers)
            };

            foreach (var activity in activities)
            {
                Console.WriteLine(activity.GetSummary());
            }
        }
    }
}
