using System;
using System.Collections.Generic;

public class Field
{
    private int UpperBound_Size = 200;
    private int LowerBound_Size = 100;
    private double RatioCountOfHerbivores, RatioCountOfHerbs, RatioCountOfPredators;
    public int CountOfHerbivores {get; set;}
    public int CountOfHerbs {get; set;}
    public int CountOfPredators {get; set;}
    bool Drawing;

    public int Size { get; private set; }
    public Cell[,] Cells { get; set; }

    public Field(double _RatioCountOfHerbivores, double _RatioCountOfHerbs, double _RatioCountOfPredators, bool _Drawing)
    {
        RatioCountOfHerbivores = _RatioCountOfHerbivores;
        RatioCountOfHerbs = _RatioCountOfHerbs;
        RatioCountOfPredators = _RatioCountOfPredators;
        Drawing = _Drawing;
        GetRandomSize();
    }

    public bool ChechMap()
    {
        int q1 = 0, q2 = 0, q3 = 0;
        for (int i = 0; i < Size; ++i)
        {
            for (int j = 0; j < Size; ++j)
            {
                if (Cells[i, j].CellState == State.Herb) ++q1;
                if (Cells[i, j].CellState == State.Herbivore) ++q2;
                if (Cells[i, j].CellState == State.Predator) ++q3;
                if (Cells[i, j].CellState == State.HerbAndPredator)
                {
                    ++q1;
                    ++q3;
                }
            }
        }
        if (q1 != 0 && q2 != 0 && q3 != 0) return true;
        else return false;
    }

    public bool ChechMap(int _q1, int _q2, int _q3, string qwe)
    {
        int q1 = 0, q2 = 0, q3 = 0;
        for (int i = 0; i < Size; ++i)
        {
            for (int j = 0; j < Size; ++j)
            {
                if (Cells[i, j].CellState == State.Herb) ++q1;
                if (Cells[i, j].CellState == State.Herbivore) ++q2;
                if (Cells[i, j].CellState == State.Predator) ++q3;
                if (Cells[i, j].CellState == State.HerbAndPredator)
                {
                    ++q1;
                    ++q3;
                }
            }
        }
        if (q1 == _q1 && q2 == _q2 && q3 == _q3) return true;
        else
        {
            int Debug = 123;
            Debug++;
            return false;
        }
    }

    public void CellsMove(Position OldPoint, Position NewPoint)
    {
        Animal TempAnimal;
        if (Cells[NewPoint.X, NewPoint.Y].CellState == State.Empty)
        {
            State StateOfOldCell = Cells[OldPoint.X, OldPoint.Y].CellState;
            if (StateOfOldCell == State.Herbivore)
            {
                Cells[OldPoint.X, OldPoint.Y].SetState(State.Empty);
                TempAnimal = Cells[OldPoint.X, OldPoint.Y].AnimalInCell;
                Cells[NewPoint.X, NewPoint.Y].SetState(State.Herbivore);
                Cells[NewPoint.X, NewPoint.Y].AnimalInCell = TempAnimal;
                Cells[OldPoint.X, OldPoint.Y].AnimalInCell = null;
            }
            if (StateOfOldCell == State.Predator)
            {
                Cells[OldPoint.X, OldPoint.Y].SetState(State.Empty);
                TempAnimal = Cells[OldPoint.X, OldPoint.Y].AnimalInCell;
                Cells[NewPoint.X, NewPoint.Y].SetState(State.Predator);
                Cells[NewPoint.X, NewPoint.Y].AnimalInCell = TempAnimal;
                Cells[OldPoint.X, OldPoint.Y].AnimalInCell = null;
            }
            if (StateOfOldCell == State.HerbAndPredator)
            {
                Cells[OldPoint.X, OldPoint.Y].SetState(State.Herb);
                TempAnimal = Cells[OldPoint.X, OldPoint.Y].AnimalInCell;
                Cells[NewPoint.X, NewPoint.Y].SetState(State.Predator);
                Cells[NewPoint.X, NewPoint.Y].AnimalInCell = TempAnimal;
                Cells[OldPoint.X, OldPoint.Y].AnimalInCell = null;
            }
            return;
        }
        if (Cells[NewPoint.X, NewPoint.Y].CellState == State.Herb)
        {
            State StateOfOldCell = Cells[OldPoint.X, OldPoint.Y].CellState;
            if (StateOfOldCell == State.Herbivore)
            {
                Cells[OldPoint.X, OldPoint.Y].SetState(State.Empty);
                TempAnimal = Cells[OldPoint.X, OldPoint.Y].AnimalInCell;
                Cells[NewPoint.X, NewPoint.Y].SetState(State.Herbivore);
                Cells[NewPoint.X, NewPoint.Y].AnimalInCell = TempAnimal;
                Cells[NewPoint.X, NewPoint.Y].HerbInCell = null;
                Cells[OldPoint.X, OldPoint.Y].AnimalInCell = null;
            }
            if (StateOfOldCell == State.Predator)
            {
                Cells[OldPoint.X, OldPoint.Y].SetState(State.Empty);
                TempAnimal = Cells[OldPoint.X, OldPoint.Y].AnimalInCell;
                Cells[NewPoint.X, NewPoint.Y].SetState(State.HerbAndPredator);
                Cells[NewPoint.X, NewPoint.Y].AnimalInCell = TempAnimal;
                Cells[OldPoint.X, OldPoint.Y].AnimalInCell = null;
            }
            if (StateOfOldCell == State.HerbAndPredator)
            {
                Cells[OldPoint.X, OldPoint.Y].SetState(State.Herb);
                TempAnimal = Cells[OldPoint.X, OldPoint.Y].AnimalInCell;
                Cells[NewPoint.X, NewPoint.Y].SetState(State.HerbAndPredator);
                Cells[NewPoint.X, NewPoint.Y].AnimalInCell = TempAnimal;
                Cells[OldPoint.X, OldPoint.Y].AnimalInCell = null;
            }
            return;
        }
        if (Cells[NewPoint.X, NewPoint.Y].CellState == State.Herbivore)
        {
            State StateOfOldCell = Cells[OldPoint.X, OldPoint.Y].CellState;
            if (StateOfOldCell == State.Herbivore) return;
            if (StateOfOldCell == State.Predator)
            {
                Cells[OldPoint.X, OldPoint.Y].SetState(State.Empty);
                TempAnimal = Cells[OldPoint.X, OldPoint.Y].AnimalInCell;
                Cells[NewPoint.X, NewPoint.Y].SetState(State.Predator);
                Cells[NewPoint.X, NewPoint.Y].AnimalInCell = TempAnimal;
                Cells[OldPoint.X, OldPoint.Y].AnimalInCell = null;
            }
            if (StateOfOldCell == State.HerbAndPredator)
            {
                Cells[OldPoint.X, OldPoint.Y].SetState(State.Herb);
                TempAnimal = Cells[OldPoint.X, OldPoint.Y].AnimalInCell;
                Cells[NewPoint.X, NewPoint.Y].SetState(State.Predator);
                Cells[NewPoint.X, NewPoint.Y].AnimalInCell = TempAnimal;
                Cells[OldPoint.X, OldPoint.Y].AnimalInCell = null;
            }
            return;
        }
        if (Cells[NewPoint.X, NewPoint.Y].CellState == State.Predator) //Stupid Herbivore
        {
            State StateOfOldCell = Cells[OldPoint.X, OldPoint.Y].CellState;
            if (StateOfOldCell == State.Herbivore)
            {
                Cells[OldPoint.X, OldPoint.Y].SetState(State.Empty);
                Cells[OldPoint.X, OldPoint.Y].AnimalInCell = null;
            }
            if (StateOfOldCell == State.Predator) return;
            if (StateOfOldCell == State.HerbAndPredator) return;
            return;
        }
        if (Cells[NewPoint.X, NewPoint.Y].CellState == State.HerbAndPredator) //Greedy Herbivore
        {
            State StateOfOldCell = Cells[OldPoint.X, OldPoint.Y].CellState;
            if (StateOfOldCell == State.Herbivore)
            {
                Cells[OldPoint.X, OldPoint.Y].SetState(State.Empty);
                Cells[OldPoint.X, OldPoint.Y].AnimalInCell = null;
            }
            if (StateOfOldCell == State.Predator) return;
            if (StateOfOldCell == State.HerbAndPredator) return;
            return;
        }
    }

    private void GetRandomSize()
    {
        Size = RandomGenerator.RandomValue.Next(LowerBound_Size, UpperBound_Size);
        if ((RatioCountOfHerbivores + Math.Max(RatioCountOfHerbs, RatioCountOfPredators)) <= 1)
        {
            CountOfHerbivores = (int)(Size * Size * RatioCountOfHerbivores);
            CountOfHerbs = (int)(Size * Size * RatioCountOfHerbs);
            CountOfPredators = (int)(Size * Size * RatioCountOfPredators);
        }
        else
        {
            CountOfHerbivores = 0;
            CountOfHerbs = 0;
            CountOfPredators = 0;
        }
    }

    public void FieldBuilding()
    {
        List<Position> RandomPoint = new List<Position>();
        Cells = new Cell[Size, Size];
        for (int i = 0; i < Size; ++i)
        {
            for (int j = 0; j < Size; ++j)
            {
                Cells[i, j] = new Cell(Drawing);
                RandomPoint.Add(new Position(i, j));
            }
        }

        Shuffle<Position>.ListShuffle(ref RandomPoint);

        for (int i = 0; i < CountOfHerbivores; ++i)
        {
            Cells[RandomPoint[0].X, RandomPoint[0].Y].SetState(State.Herbivore);
            RandomPoint.Remove(RandomPoint[0]);
        }
        for (int i = 0; i < CountOfHerbs; ++i)
        {
            Cells[RandomPoint[i].X, RandomPoint[i].Y].SetState(State.Herb);
        }

        Shuffle<Position>.ListShuffle(ref RandomPoint);

        for (int i = 0; i < CountOfPredators; ++i)
        {
            if (Cells[RandomPoint[0].X, RandomPoint[0].Y].CellState == State.Herb) Cells[RandomPoint[0].X, RandomPoint[0].Y].SetState(State.HerbAndPredator);
            else Cells[RandomPoint[0].X, RandomPoint[0].Y].SetState(State.Predator);
            RandomPoint.Remove(RandomPoint[0]);
        }
    }

    public void CellClearFromAnimal(Position Point)
    {
        if (Cells[Point.X, Point.Y].CellState == State.HerbAndPredator) Cells[Point.X, Point.Y].SetState(State.Herb);
        else Cells[Point.X, Point.Y].SetState(State.Empty);
        Cells[Point.X, Point.Y].AnimalInCell = null;
    }

    public void CellSetState(Position Point, State CellState)
    {
        Cells[Point.X, Point.Y].SetState(CellState);
    }

    public State CellGetState(Position Point)
    {
        return Cells[Point.X, Point.Y].CellState;
    }

    public Organism GetInformationOfCell(Position Point)
    {
        if (Point.X < Size && Point.Y < Size)
            return Cells[Point.X, Point.Y].GetInformation();
        else return null;
    }
}

