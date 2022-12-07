using CarRacing.Models.Cars.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRacing.Models.Cars
{
    public abstract class Car : ICar
    {
        public Car(string make, string model, string VIN, int horsePower, double fuelAvailable, double fuelConsumptionPerRace)
        {
            Make = make;
            Model = model;
            HorsePower = horsePower;
            this.VIN = VIN;
            FuelAvailable = fuelAvailable;
            FuelConsumptionPerRace = fuelConsumptionPerRace;

        }
        private string make;

        public string Make
        {
            get { return make; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Car make cannot be null or empty.");
                }
                make = value;
            }
        }

        private string model;

        public string Model
        {
            get { return model; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Car model cannot be null or empty.");
                }
                model = value;
            }
        }


        private string vIN;

        public string VIN
        {
            get { return vIN; }
            private set
            {
                if (value.Length != 17)
                {
                    throw new ArgumentException("Car VIN must be exactly 17 characters long.");
                }
                vIN = value;
            }
        }


        private int horsePower;

        public int HorsePower
        {
            get { return horsePower; }
            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Horse power cannot be below 0.");
                }
                horsePower = value;
            }
        }


        private double fuelAvailable;

        public double FuelAvailable
        {
            get { return fuelAvailable; }
            private set
            {
                if (value < 0)
                {
                    value = 0;
                }
                fuelAvailable = value;
            }
        }

        private double fuelConsumptionPerRace;

        public double FuelConsumptionPerRace
        {
            get { return fuelConsumptionPerRace; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Fuel consumption cannot be below 0.");
                }
                fuelConsumptionPerRace = value;
            }
        }


        public  virtual void Drive()
        {
            fuelAvailable -= fuelConsumptionPerRace;
        }
    }
}
