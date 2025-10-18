using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab5v8
{
    public class Delivery
    {
        public string Zone { get; set; }
        public List<Package> Packages { get; set; } = new List<Package>();
        public bool IsOnTime { get; set; }

        public Delivery(string zone, bool isOnTime)
        {
            Zone = zone;
            IsOnTime = isOnTime;
        }

        public double TotalWeight() => Packages.Sum(p => p.Weight);
        public double TotalPrice() => Packages.Sum(p => p.GetTotalPrice());
    }
}
