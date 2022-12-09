﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace OnlineShop.Models.Products
{
    public abstract class Component : Product, IComponent
    {
        public Component(int id, string manufacturer, string model, decimal price, double overallPerformance, int generation) : base(id, manufacturer, model, price, overallPerformance)
        {
            Generation = generation;
        }

        public int Generation { get; }
        public override string ToString()
        {
            return base.ToString() + $" Generation: {Generation}";
        }
    }
}
