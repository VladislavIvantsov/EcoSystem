using System;
using System.Collections.Generic;

public class Herbivore : Animal
{
    public Herbivore(double Energy, double EnergyDivisor, double ReproductionLimit, double EnergyReduction)
        : base(Energy, EnergyDivisor, ReproductionLimit, EnergyReduction) { }

    public Position Move(Cell[,] Scope)
    {
        int ScopeSize = 5;
        int Surroundings = 3;
        int EndOfArray = 0, EndOfArrayWithHerb = 0;
        List<Vector> HerbInScope = new List<Vector>();
        Position[] NearScope = new Position[Surroundings * Surroundings];
        Position[] NearScopeWithHerb = new Position[Surroundings * Surroundings];
        for (int i = 0; i < ScopeSize; ++i)
            for (int j = 0; j < ScopeSize; ++j)
                if (Scope[i, j].CellState == State.Predator || Scope[i, j].CellState == State.HerbAndPredator)
                    DoInaccessibleCell(Scope, Point);
        for (int i = 1; i < Surroundings + 1; ++i)
            for (int j = 1; j < Surroundings + 1; ++j)
                if (Scope[i, j].CellState <= State.Herb)
                {
                    if (Scope[i, j].CellState == State.Herb)
                    {
                        NearScopeWithHerb[EndOfArrayWithHerb] = new Position(Point.X - ScopeSize / 2 + i, Point.Y - ScopeSize / 2 + j);
                        EndOfArrayWithHerb++;
                    }
                    else
                    {
                        NearScope[EndOfArray] = new Position(Point.X - ScopeSize / 2 + i, Point.Y - ScopeSize / 2 + j);
                        EndOfArray++;
                    }
                }
        if (EndOfArrayWithHerb != 0) return RandomMove(NearScopeWithHerb, EndOfArrayWithHerb);
        if (EndOfArray == 0) return null;
        else
        {
            for (int i = 0; i < ScopeSize; ++i)
                for (int j = 0; j < ScopeSize; ++j)
                    if (Scope[i, j].CellState == State.Herb)
                        HerbInScope.Add(new Vector(Point, new Position(Point.X - ScopeSize / 2 + i, Point.Y - ScopeSize / 2 + j)));
            if (HerbInScope.Count != 0)
            {
                HerbInScope.Sort(Vector.VectorCompare);
                foreach (Vector Prey in HerbInScope)
                {
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
                                MoveCell = new Position(NearScope[i].X, NearScope[i].Y);
                            }
                        }
                        return MoveCell;
                    }
                }
            }
            else return RandomMove(NearScope, EndOfArray);
        }
        return null;
    }

    void DoInaccessibleCell(Cell[,] Scope, Position Point)
    {
        int ScopeSize = 5;
        for (int i = -1; i <= 1; ++i)
            for (int j = -1; j <= 1; ++j)
                if (((i + Point.X) >= 0) && ((i + Point.X) < ScopeSize) && ((j + Point.X) >= 0) && ((j + Point.X) < ScopeSize))
                {
                    if (Scope[i + 1, j + 1].CellState != State.Predator || Scope[i + 1, j + 1].CellState != State.HerbAndPredator)
                        Scope[i + 1, j + 1] = new Cell(State.InaccessibleCell);
                }
    }

    bool RivalCheck(Cell[,] Scope, Vector Prey)
    {
        int ScopeSize = 5;
        for (int i = 0; i < ScopeSize; ++i)
            for (int j = 0; j < ScopeSize; ++j)
                if (Scope[i, j].CellState == State.Herbivore)
                {
                    Vector RivalVector = new Vector(new Position(Point.X - ScopeSize / 2 + i, Point.Y - ScopeSize / 2 + j), Prey.SecondPoint);
                    if (RivalVector.Distance < Prey.Distance) return false;
                }
        return true;
    }
}