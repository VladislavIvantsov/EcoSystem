using System;

[Serializable]
public class Statistic
{
    public int FieldSize { get; set; }
    public int InitialCountOfHerbivores { get; set; }
    public int InitialCountOfHerbs { get; set; }
    public int InitialCountOfPredators { get; set; }

    public int HerbivoresGrowth { get; set; }
    public int HerbsGrowth { get; set; }
    public int PredatorsGrowth { get; set; }

    public int EatenHerbivores { get; set; }
    public int EatenHerbs { get; set; }

    public int HerbivoresDiedFromStarvation { get; set; }
    public int PredatorsDiedFromStarvation { get; set; }

    public int Fitness { get; set; }

    public Statistic(int _FieldSize, int _InitialCountOfHerbivores, int _InitialCountOfHerbs, int _InitialCountOfPredators)
    {
        FieldSize = _FieldSize;
        InitialCountOfHerbivores = _InitialCountOfHerbivores;
        InitialCountOfHerbs = _InitialCountOfHerbs;
        InitialCountOfPredators = _InitialCountOfPredators;
        SetToZero();
    }

    void SetToZero()
    {
        HerbivoresGrowth = 0;
        HerbsGrowth = 0;
        PredatorsGrowth = 0;
        EatenHerbivores = 0;
        EatenHerbs = 0;
        HerbivoresDiedFromStarvation = 0;
        PredatorsDiedFromStarvation = 0;
        Fitness = 0;
    }
}