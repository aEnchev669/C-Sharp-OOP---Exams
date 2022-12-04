﻿using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlanetWars.Models
{
    public abstract class Weapon : IWeapon
    {
        public Weapon(int destructionLevel, double price)
        {
            Price = price;
            DestructionLevel = destructionLevel;
        }
        private double price;

        public double Price
        {
            get { return price; }
            private set { price = value; }
        }

        private int destructionLevel;

        public int DestructionLevel
        {
            get { return destructionLevel; }
            private set
            {
                if (value < 1)
                {
                    throw new ArgumentException(ExceptionMessages.TooLowDestructionLevel);
                }
                else if (value > 10)
                {
                    throw new ArgumentException(ExceptionMessages.TooHighDestructionLevel);
                }
                destructionLevel = value;
            }
        }



    }
}
