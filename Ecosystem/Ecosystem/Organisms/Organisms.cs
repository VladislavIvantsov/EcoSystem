using System;
using System.Collections.Generic;

public class Organism
{
    double EnergyDivisor;
    public double Energy { get; set; }
    public double ReproductionLimit { get; private set; }
    public Position Point { get; set; }
    static readonly Random TempRandom = new Random();

    public Organism(double Energy, double EnergyDivisor, double ReproductionLimit)
    {
        this.Energy = Energy;
        this.EnergyDivisor = EnergyDivisor;
        this.ReproductionLimit = ReproductionLimit;
    }

    public bool Reproduction()
    {
        if (Energy >= ReproductionLimit) return true;
        else return false;
    }

    public Position Reproduction(Cell[,] Scope)
    {
        int Size = 3;
        Position[] Temp = new Position[Size * Size];
        int EndOfArray = 0;
        for (int i = 0; i < Size; ++i)
            for (int j = 0; j < Size; ++j)
                if (Scope[i, j].CellState == State.Empty)
                {
                    Temp[EndOfArray] = new Position(Point.X - Size / 2 + i, Point.Y - Size / 2 + j);
                    EndOfArray++; 
                }
        if (EndOfArray != 0)
        {
            Energy /= EnergyDivisor;
            return RandomMove(Temp, EndOfArray);
        }
        return null;
    }

    public Position RandomMove(Position[] Temp, int Size)
    {
        return Temp[TempRandom.Next(Size)];
    }
}

