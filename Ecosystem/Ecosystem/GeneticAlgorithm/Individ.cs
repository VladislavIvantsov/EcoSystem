using System;
using System.Collections.Generic;

public enum Gene { RatioCountOfHerbivores, RatioCountOfHerbs, RatioCountOfPredators, StartValueEnergyHerb, StartValueEnergyHerbivore, StartValueEnergyPredator,
            EnergyHerbBoost, EnergyReductionHerbivore, EnergyReductionPredator, ReproductionLimitHerb, ReproductionLimitHerbivore, ReproductionLimitPredator}

[Serializable]
public class Individ
{
    const int MutationProbability = 2;
    public const int CountOfGenes = 12;

    public List<Statistic> StatisticList { get; set; }
    public double[] SetOfGenes { get; private set; }
    public int Fitness { get; set; }

    private int UpperBound_CountOfOrganisms = 100;
    private int LowerBound_CountOfOrganisms = 1;
    private double Divisor = 100;

    private int LowerBound_BoostAndReduction = 1;
    private int UpperBound_BoostAndReduction = 500;
    private int UpperBound_StartEnergy = 1000;
    private int UpperBound_Reproduction = 10000;

    public Individ()
    {
        Fitness = 0;
        StatisticList = new List<Statistic>();
        SetOfGenes = new double[CountOfGenes];
        GenerateCountOfHerbivores();
        GenerateCountOfHerbs();
        GenerateCountOfPredators();
        GenerateEnergyReductionPredator();
        GenerateEnergyReductionHerbivore();
        GenerateEnergyHerbBoost();
        GenerateStartValueEnergyHerb();
        GenerateStartValueEnergyHerbivore();
        GenerateStartValueEnergyPredator();
        GenerateReproductionLimitHerb();
        GenerateReproductionLimitHerbivore();
        GenerateReproductionLimitPredator();
    }

    public Individ(int Empty)
    {
        Fitness = Empty;
        StatisticList = new List<Statistic>();
        SetOfGenes = new double[CountOfGenes];
        for (int i = 0; i < CountOfGenes; ++i)
        {
            SetOfGenes[i] = Empty;
        }
    }

    public Individ(double[] Param)
    {
        StatisticList = new List<Statistic>();
        Fitness = 0;
        SetOfGenes = new double[CountOfGenes];
        for (int i = 0; i < CountOfGenes; ++i)
            SetOfGenes[i] = Param[i];
    }

    static public int IndividCompare(Individ tmp1, Individ tmp2)
    {
        return (-1) * tmp1.Fitness.CompareTo(tmp2.Fitness);
    }

    public void AddTheLastResult()
    {
        Fitness += StatisticList[StatisticList.Count - 1].Fitness;
    }

    public void GenerateCountOfHerbivores()
    {
        if (SetOfGenes[(int)Gene.RatioCountOfHerbivores] == 0) SetOfGenes[(int)Gene.RatioCountOfHerbivores] = RandomGenerator.RandomValue.Next(LowerBound_CountOfOrganisms, UpperBound_CountOfOrganisms) / Divisor;
        else SetOfGenes[(int)Gene.RatioCountOfHerbivores] = RandomGenerator.RandomValue.Next(LowerBound_CountOfOrganisms, (int)(UpperBound_CountOfOrganisms - Divisor * Math.Max(SetOfGenes[(int)Gene.RatioCountOfPredators], SetOfGenes[(int)Gene.RatioCountOfHerbs])));

    }

    public void GenerateCountOfHerbs()
    {
        SetOfGenes[(int)Gene.RatioCountOfHerbs] = RandomGenerator.RandomValue.Next(LowerBound_CountOfOrganisms, (int)(UpperBound_CountOfOrganisms - Divisor * SetOfGenes[(int)Gene.RatioCountOfHerbivores])) / Divisor;
    }

    public void GenerateCountOfPredators()
    {
        SetOfGenes[(int)Gene.RatioCountOfPredators] = RandomGenerator.RandomValue.Next(LowerBound_CountOfOrganisms, (int)(UpperBound_CountOfOrganisms - Divisor * SetOfGenes[(int)Gene.RatioCountOfHerbivores])) / Divisor;
    }

    public void GenerateEnergyHerbBoost()
    {
        SetOfGenes[(int)Gene.EnergyHerbBoost] = RandomGenerator.RandomValue.Next(LowerBound_BoostAndReduction, UpperBound_BoostAndReduction);
    }

    public void GenerateEnergyReductionHerbivore()
    {
        SetOfGenes[(int)Gene.EnergyReductionHerbivore] = RandomGenerator.RandomValue.Next(LowerBound_BoostAndReduction, UpperBound_BoostAndReduction);
    }

    public void GenerateEnergyReductionPredator()
    {
        SetOfGenes[(int)Gene.EnergyReductionPredator] = RandomGenerator.RandomValue.Next(LowerBound_BoostAndReduction, UpperBound_BoostAndReduction);
    }

    public void GenerateStartValueEnergyHerb()
    {
        SetOfGenes[(int)Gene.StartValueEnergyHerb] = RandomGenerator.RandomValue.Next((int)(SetOfGenes[(int)Gene.EnergyHerbBoost]), UpperBound_StartEnergy);
    }

    public void GenerateStartValueEnergyHerbivore()
    {
        SetOfGenes[(int)Gene.StartValueEnergyHerbivore] = RandomGenerator.RandomValue.Next((int)(SetOfGenes[(int)Gene.EnergyReductionHerbivore]), UpperBound_StartEnergy);
    }

    public void GenerateStartValueEnergyPredator()
    {
        SetOfGenes[(int)Gene.StartValueEnergyPredator] = RandomGenerator.RandomValue.Next((int)(SetOfGenes[(int)Gene.EnergyReductionPredator]), UpperBound_StartEnergy);
    }

    public void GenerateReproductionLimitHerb()
    {
        SetOfGenes[(int)Gene.ReproductionLimitHerb] = RandomGenerator.RandomValue.Next((int)(SetOfGenes[(int)Gene.StartValueEnergyHerb]), UpperBound_Reproduction);
    }

    public void GenerateReproductionLimitHerbivore()
    {
        SetOfGenes[(int)Gene.ReproductionLimitHerbivore] = RandomGenerator.RandomValue.Next((int)(SetOfGenes[(int)Gene.StartValueEnergyHerbivore]), UpperBound_Reproduction);
    }

    public void GenerateReproductionLimitPredator()
    {
        SetOfGenes[(int)Gene.ReproductionLimitPredator] = RandomGenerator.RandomValue.Next((int)(SetOfGenes[(int)Gene.StartValueEnergyPredator]), UpperBound_Reproduction);
    }

    public void Mutation()
    {
        if(RandomGenerator.RandomValue.Next(100) < MutationProbability)
        {
            int MutatedGene = RandomGenerator.RandomValue.Next(CountOfGenes);
            switch ((Gene)MutatedGene)
            {
                case Gene.RatioCountOfHerbivores:
                    GenerateCountOfHerbivores();
                    break;
                case Gene.RatioCountOfHerbs:
                    GenerateCountOfHerbs();
                    break;
                case Gene.RatioCountOfPredators:
                    GenerateCountOfPredators();
                    break;
                case Gene.EnergyHerbBoost:
                    GenerateEnergyHerbBoost();
                    break;
                case Gene.EnergyReductionHerbivore:
                    GenerateEnergyReductionHerbivore();
                    break;
                case Gene.EnergyReductionPredator:
                    GenerateEnergyReductionPredator();
                    break;
                case Gene.ReproductionLimitHerb:
                    GenerateReproductionLimitHerb();
                    break;
                case Gene.ReproductionLimitHerbivore:
                    GenerateReproductionLimitHerbivore();
                    break;
                case Gene.ReproductionLimitPredator:
                    GenerateReproductionLimitPredator();
                    break;
                case Gene.StartValueEnergyHerb:
                    GenerateStartValueEnergyHerb();
                    break;
                case Gene.StartValueEnergyHerbivore:
                    GenerateStartValueEnergyHerbivore();
                    break;
                case Gene.StartValueEnergyPredator:
                    GenerateStartValueEnergyPredator();
                    break;
            }
        }
    }
}