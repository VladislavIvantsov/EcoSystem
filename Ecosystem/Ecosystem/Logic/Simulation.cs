using System;
using System.Collections.Generic;

public class Simulation
{
    public Individ ExperimentalIndividual { get; private set; }
    public BitMaps BitMap { get; private set; }
    bool Drawing = false;

    double RatioCountOfHerbivores, RatioCountOfPredators, RatioCountOfHerbs;
    double EnergyHerbBoost, EnergyReductionHerbivore, EnergyReductionPredator;
    double StartValueEnergyHerb, StartValueEnergyHerbivore, StartValueEnergyPredator;
    double ReproductionLimitHerb, ReproductionLimitHerbivore, ReproductionLimitPredator;

    const int EnergyDivisor = 2;

    public Field MyField { get; private set;}

    List<Herb> HerbList = new List<Herb>();
    List<Herbivore> HerbivoreList = new List<Herbivore>();
    List<Predator> PredatorList = new List<Predator>();

    public Simulation(Individ _ExperimentalIndividual, int _Width, int _Height) 
    {
        Drawing = true;
        ExperimentalIndividual = _ExperimentalIndividual;
        BitMap = new BitMaps();
        SetParameter();
        MyField = new Field(RatioCountOfHerbivores, RatioCountOfHerbs, RatioCountOfPredators, Drawing);
        ExperimentalIndividual.StatisticList.Add(new Statistic(MyField.Size, MyField.CountOfHerbivores, MyField.CountOfHerbs, MyField.CountOfPredators));
        BitMap.LoadBitmaps(MyField.Size, _Width, _Height);
        MyField.FieldBuilding();
        BuildLists();
    }

    public Simulation(Individ _ExperimentalIndividual)
    {
        ExperimentalIndividual = _ExperimentalIndividual;
        SetParameter();
        MyField = new Field(RatioCountOfHerbivores, RatioCountOfHerbs, RatioCountOfPredators, Drawing);
        ExperimentalIndividual.StatisticList.Add(new Statistic(MyField.Size, MyField.CountOfHerbivores, MyField.CountOfHerbs, MyField.CountOfPredators));
        MyField.FieldBuilding();
        BuildLists();
    }

    void SetParameter()
    {
        RatioCountOfHerbivores = ExperimentalIndividual.SetOfGenes[(int)Gene.RatioCountOfHerbivores];
        RatioCountOfPredators = ExperimentalIndividual.SetOfGenes[(int)Gene.RatioCountOfPredators];
        RatioCountOfHerbs = ExperimentalIndividual.SetOfGenes[(int)Gene.RatioCountOfHerbs];

        StartValueEnergyHerb = ExperimentalIndividual.SetOfGenes[(int)Gene.StartValueEnergyHerb];
        StartValueEnergyHerbivore = ExperimentalIndividual.SetOfGenes[(int)Gene.StartValueEnergyHerbivore];
        StartValueEnergyPredator = ExperimentalIndividual.SetOfGenes[(int)Gene.StartValueEnergyPredator];

        ReproductionLimitHerb = ExperimentalIndividual.SetOfGenes[(int)Gene.ReproductionLimitHerb];
        ReproductionLimitHerbivore = ExperimentalIndividual.SetOfGenes[(int)Gene.ReproductionLimitHerbivore];
        ReproductionLimitPredator = ExperimentalIndividual.SetOfGenes[(int)Gene.ReproductionLimitPredator];

        EnergyHerbBoost = ExperimentalIndividual.SetOfGenes[(int)Gene.EnergyHerbBoost];
        EnergyReductionHerbivore = ExperimentalIndividual.SetOfGenes[(int)Gene.EnergyReductionHerbivore];
        EnergyReductionPredator = ExperimentalIndividual.SetOfGenes[(int)Gene.EnergyReductionPredator];
    }

    void BuildLists()
    {
        for (int i = 0; i < MyField.Size; i++)
        {
            for (int j = 0; j < MyField.Size; j++)
            {
                if (MyField.Cells[i, j].CellState == State.Herb)
                {
                    Herb NewHerb = new Herb(StartValueEnergyHerb, EnergyDivisor, ReproductionLimitHerb, EnergyHerbBoost);
                    NewHerb.Point = new Position(i, j);
                    MyField.Cells[i, j].HerbInCell = NewHerb;
                    HerbList.Add(NewHerb);
                }
                else if (MyField.Cells[i, j].CellState == State.Herbivore)
                {
                    Herbivore NewHerbivore = new Herbivore(StartValueEnergyHerbivore, EnergyDivisor, ReproductionLimitHerbivore, EnergyReductionHerbivore);
                    NewHerbivore.Point = new Position(i, j);
                    MyField.Cells[i, j].AnimalInCell = NewHerbivore;
                    HerbivoreList.Add(NewHerbivore);
                }
                else if (MyField.Cells[i, j].CellState == State.Predator)
                {
                    Predator NewPredator = new Predator(StartValueEnergyPredator, EnergyDivisor, ReproductionLimitPredator, EnergyReductionPredator);
                    NewPredator.Point = new Position(i, j);
                    MyField.Cells[i, j].AnimalInCell = NewPredator;
                    PredatorList.Add(NewPredator);
                }
                else if (MyField.Cells[i, j].CellState == State.HerbAndPredator)
                {
                    Herb NewHerb = new Herb(StartValueEnergyHerb, EnergyDivisor, ReproductionLimitHerb, EnergyHerbBoost);
                    NewHerb.Point = new Position(i, j);
                    MyField.Cells[i, j].HerbInCell = NewHerb;
                    HerbList.Add(NewHerb);
                    Predator NewPredator = new Predator(StartValueEnergyPredator, EnergyDivisor, ReproductionLimitPredator, EnergyReductionPredator);
                    NewPredator.Point = new Position(i, j);
                    MyField.Cells[i, j].AnimalInCell = NewPredator;
                    PredatorList.Add(NewPredator);
                }
            }
        }
    }

    void FirtsStepOfSimulation()
    {
        int SizeOfScopeReproduction = 3;
        List<Predator> TempPredatorList = new List<Predator>();
        foreach (Predator _Predator in PredatorList)
            if (_Predator.Reproduction())
            {
                Position NewPosition = _Predator.Reproduction(GetScope(SizeOfScopeReproduction, _Predator.Point));
                if (NewPosition != null)
                {
                    MyField.CellSetState(NewPosition, State.Predator);
                    Predator TempPredator = new Predator(ReproductionLimitPredator / EnergyDivisor, EnergyDivisor, ReproductionLimitPredator, EnergyReductionPredator);
                    TempPredator.Point = NewPosition;
                    MyField.Cells[NewPosition.X, NewPosition.Y].AnimalInCell = TempPredator;
                    TempPredatorList.Add(TempPredator);
                    ExperimentalIndividual.StatisticList[ExperimentalIndividual.StatisticList.Count - 1].PredatorsGrowth++;
                }
            }
        PredatorList.AddRange(TempPredatorList);
        List<Herbivore> TempHerbivoreList = new List<Herbivore>();
        foreach (Herbivore _Herbivore in HerbivoreList)
            if (_Herbivore.Reproduction())
            {
                Position NewPosition = _Herbivore.Reproduction(GetScope(SizeOfScopeReproduction, _Herbivore.Point));
                if (NewPosition != null)
                {
                    MyField.CellSetState(NewPosition, State.Herbivore);
                    Herbivore TempHerbivore = new Herbivore(ReproductionLimitHerbivore / EnergyDivisor, EnergyDivisor, ReproductionLimitHerbivore, EnergyReductionHerbivore);
                    TempHerbivore.Point = NewPosition;
                    MyField.Cells[NewPosition.X, NewPosition.Y].AnimalInCell = TempHerbivore;
                    TempHerbivoreList.Add(TempHerbivore);
                    ExperimentalIndividual.StatisticList[ExperimentalIndividual.StatisticList.Count - 1].HerbivoresGrowth++;
                }
            }
        HerbivoreList.AddRange(TempHerbivoreList);
        List<Herb> TempHerbList = new List<Herb>();
        foreach (Herb _Herb in HerbList)
            if (_Herb.Reproduction())
            {
                Position NewPosition = _Herb.Reproduction(GetScope(SizeOfScopeReproduction, _Herb.Point));
                if (NewPosition != null)
                {
                    MyField.CellSetState(NewPosition, State.Herb);
                    Herb TempHerb = new Herb(ReproductionLimitHerb / EnergyDivisor, EnergyDivisor, ReproductionLimitHerb, EnergyHerbBoost);
                    TempHerb.Point = NewPosition;
                    MyField.Cells[NewPosition.X, NewPosition.Y].HerbInCell = TempHerb;
                    TempHerbList.Add(TempHerb);
                    ExperimentalIndividual.StatisticList[ExperimentalIndividual.StatisticList.Count - 1].HerbsGrowth++;
                }
            }
        HerbList.AddRange(TempHerbList);
     }

    void SecondStepOfSimulation() //Energy was adding/reducing 
    {
        foreach (Herb _Herb in HerbList)
            _Herb.EnergyAdd();
        List<Herbivore> TempHerbivoreList = new List<Herbivore>();
        foreach (Herbivore _Herbivore in HerbivoreList)
        {
            _Herbivore.EnergyReduce();
            if (_Herbivore.Energy <= 0)
            {
                MyField.CellClearFromAnimal(_Herbivore.Point);
                TempHerbivoreList.Add(_Herbivore);
                ExperimentalIndividual.StatisticList[ExperimentalIndividual.StatisticList.Count - 1].HerbivoresDiedFromStarvation++;
            }
        }
        foreach (Herbivore _Herbivore in TempHerbivoreList)
        {
            HerbivoreList.Remove(_Herbivore);
        }
        List<Predator> TempPredatorList = new List<Predator>();
        foreach (Predator _Predator in PredatorList)
        {
            _Predator.EnergyReduce();
            if (_Predator.Energy <= 0)
            {
                MyField.CellClearFromAnimal(_Predator.Point);
                TempPredatorList.Add(_Predator);
                ExperimentalIndividual.StatisticList[ExperimentalIndividual.StatisticList.Count - 1].PredatorsDiedFromStarvation++;
            }
        }
        foreach (Predator _Predator in TempPredatorList)
        {
            PredatorList.Remove(_Predator);
        }
    }

    void ThirdStepOfSimulation()
    {
        int SizeOfScope = 7;
        foreach (Predator _Predator in PredatorList)
        {
            Position NewPosition = _Predator.Move(GetScope(SizeOfScope, _Predator.Point));
            if (NewPosition != null)
            {
                if (MyField.CellGetState(NewPosition) == State.Herbivore)
                {
                    _Predator.Feeding(MyField.Cells[NewPosition.X, NewPosition.Y].AnimalInCell.Energy);
                    HerbivoreList.Remove((Herbivore)MyField.Cells[NewPosition.X, NewPosition.Y].AnimalInCell);
                    ExperimentalIndividual.StatisticList[ExperimentalIndividual.StatisticList.Count - 1].EatenHerbivores++;
                }
                MyField.CellsMove(_Predator.Point, NewPosition);
                _Predator.Point = NewPosition;
            }
        }
        SizeOfScope = 5;
        foreach (Herbivore _Herbivore in HerbivoreList)
        {
            Position NewPosition = _Herbivore.Move(GetScope(SizeOfScope, _Herbivore.Point));
            if (NewPosition != null)
            {
                if (MyField.CellGetState(NewPosition) == State.Herb)
                {
                    _Herbivore.Feeding(MyField.Cells[NewPosition.X, NewPosition.Y].HerbInCell.Energy);
                    HerbList.Remove(MyField.Cells[NewPosition.X, NewPosition.Y].HerbInCell);
                    ExperimentalIndividual.StatisticList[ExperimentalIndividual.StatisticList.Count - 1].EatenHerbs++;
                }
                MyField.CellsMove(_Herbivore.Point, NewPosition);
                _Herbivore.Point = NewPosition;
            }
        }
    }

    public bool StartSimulation()
    {
        FirtsStepOfSimulation();
        SecondStepOfSimulation();
        ThirdStepOfSimulation();
        if (HerbList.Count == 0 || HerbivoreList.Count == 0 || PredatorList.Count == 0) return false;
        ExperimentalIndividual.StatisticList[ExperimentalIndividual.StatisticList.Count - 1].Fitness++;
        return true;
    }

    Cell[,] GetScope(int Size, Position PositionOfOrganism)
    {
        int Area = Size / 2;
        Cell[,] Scope = new Cell[Size, Size];
        for (int i = -Area; i <= Area; ++i)
            for (int j = -Area; j <= Area; ++j)
                if ((PositionOfOrganism.X + i) >= 0 && (PositionOfOrganism.X + i) < MyField.Size && (PositionOfOrganism.Y + j) >= 0 && (PositionOfOrganism.Y + j) < MyField.Size)
                    Scope[i + Area, j + Area] = new Cell(MyField.Cells[PositionOfOrganism.X + i, PositionOfOrganism.Y + j].CellState);
                else
                    Scope[i + Area, j + Area] = new Cell(State.InaccessibleCell);
        return Scope;
    }
}

