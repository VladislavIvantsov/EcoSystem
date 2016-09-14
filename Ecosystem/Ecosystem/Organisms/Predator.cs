using System;
using System.Collections.Generic;

public class Predator : Animal
{
    public Predator(double Energy, double EnergyDivisor, double ReproductionLimit, double EnergyReduction)
        : base(Energy, EnergyDivisor, ReproductionLimit, EnergyReduction) { }

    public Position Move(Cell[,] Scope)
    {
        int ScopeSize = 7;
        int Surroundings = 3;
        List<Vector> MyHerbivores = new List<Vector>();
        Position[] NearScope = new Position[Surroundings * Surroundings];
        Position[] NearScopeWithHerbivore = new Position[Surroundings * Surroundings];
        int EndOfArray = 0, EndOfArrayWithHerbivore = 0;
        for (int i = 2; i < Surroundings + 2; ++i)
            for (int j = 2; j < Surroundings + 2; ++j)
                if (Scope[i, j].CellState < State.Predator)
                {
                    if (Scope[i, j].CellState == State.Herbivore)
                    {
                        NearScopeWithHerbivore[EndOfArrayWithHerbivore] = new Position(Point.X - ScopeSize / 2 + i, Point.Y - ScopeSize / 2 + j);
                        EndOfArrayWithHerbivore++;
                    }
                    else
                    {
                        NearScope[EndOfArray] = new Position(Point.X - ScopeSize / 2 + i, Point.Y - ScopeSize / 2 + j);
                        EndOfArray++;
                    }
                }
        if (EndOfArrayWithHerbivore != 0) return RandomMove(NearScopeWithHerbivore, EndOfArrayWithHerbivore);
        if (EndOfArray == 0) return null;
        else
        {
            for (int i = 0; i < ScopeSize; ++i)
                for (int j = 0; j < ScopeSize; ++j)
                    if (Scope[i, j].CellState == State.Herbivore)
                        MyHerbivores.Add(new Vector(Point, new Position(Point.X - ScopeSize / 2 + i, Point.Y - ScopeSize / 2 + j)));
            if (MyHerbivores.Count != 0)
            {
                MyHerbivores.Sort(Vector.VectorCompare);
                foreach (Vector Prey in MyHerbivores)
                    if (RivalCheck(Scope, Prey))
                    {
                        Position MoveCell = new Position(0, 0);
                        Vector MinVector = new Vector(new Position(0, 0), new Position(ScopeSize, ScopeSize));
                        Vector tmpVector;
                        for (int i = 0; i < EndOfArray; ++i)
                        {
                            tmpVector = new Vector(NearScope[i], Prey.SecondPoint);
                            if (MinVector.Distance > tmpVector.Distance)
                            {
                                MinVector = tmpVector;
                                MoveCell = NearScope[i];
                            }
                        }
                        return MoveCell;
                    }
                return RandomMove(NearScope, EndOfArray);
            }
            else return RandomMove(NearScope, EndOfArray);
        }
    }

    bool RivalCheck(Cell[,] Scope, Vector Prey)
    {
        int ScopeSize = 7;
        for (int i = 0; i < ScopeSize; ++i)
            for (int j = 0; j < ScopeSize; ++j)
                if (Scope[i, j].CellState == State.Predator || Scope[i, j].CellState == State.HerbAndPredator)
                {
                    Vector RivalVector = new Vector(new Position(Point.X - ScopeSize / 2 + i, Point.Y - ScopeSize / 2 + j), Prey.SecondPoint);
                    if (RivalVector.Distance < Prey.Distance) return false;
                }
        return true;
    }
}