using System;

public class Herb : Organism
{
    public double EnergyBoost { get; set; }

    public Herb(double Energy, double EnergyDivisor, double ReproductionLimit, double _EnergyBoost)
        : base(Energy, EnergyDivisor, ReproductionLimit)
    {
        EnergyBoost = _EnergyBoost;
    }

    public void EnergyAdd()
    {
        Energy += EnergyBoost;
        if (Energy > ReproductionLimit) Energy = ReproductionLimit;
    }
}