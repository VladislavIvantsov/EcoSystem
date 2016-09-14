using System;
using System.Collections.Generic;

public class Animal : Organism
{
    public double EnergyReduction { get; set; }

    public Animal(double Energy, double EnergyDivisor, double ReproductionLimit, double EnergyReduction)
        : base(Energy, EnergyDivisor, ReproductionLimit)
    {
        this.EnergyReduction = EnergyReduction;
    }

    public void EnergyReduce()
    {
        Energy -= EnergyReduction;
    }

    public void Feeding(double _Energy)
    {
        Energy += _Energy;
        if (Energy > ReproductionLimit) Energy = ReproductionLimit;
    }
}