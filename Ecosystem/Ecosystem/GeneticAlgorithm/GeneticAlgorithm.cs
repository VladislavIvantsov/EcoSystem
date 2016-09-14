using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class GeneticAlgorithm
{
    int PopulationSize, SetSizeForSelection, CountOfNewBlood, QualityPopulation;
    public int CyclesOfEvolution { get; set; }
    public int CountOfCycleEvolution { get; set; }
    const int DegeneratePopulation = 20;
    const int CountOfEstimate = 10;

    public Individ BestIndivid { get; set; }

    List<Individ> Population;
    List<Individ> Descendants;
    List<Individ> TheEvolution { get; set; }
    [field: NonSerialized()]
    Simulation PopulationChecker;

    public GeneticAlgorithm(int _PopulationSize, int _CyclesOfEvolution)
    {
        PopulationSize = _PopulationSize;
        CyclesOfEvolution = _CyclesOfEvolution;
        SetSizeForSelection = _PopulationSize / 2;
        CountOfNewBlood = _PopulationSize / 2;
        QualityPopulation = 0;
        CountOfCycleEvolution = 0;
        BestIndivid = new Individ(0);
        TheEvolution = new List<Individ>();
        TheEvolution.Add(BestIndivid);
        GenerateNewPopulation();
    }

    public void GenerateNewPopulation()
    {
        Population = new List<Individ>();
        for (int i = 0; i < PopulationSize; ++i)
            Population.Add(new Individ());
    }

    public void Start()
    {
        while (CountOfCycleEvolution != CyclesOfEvolution)
        {
            PopulationEstimate();
            ShakePopulation();
            Selection();
            ++CountOfCycleEvolution;
        }
    }

    public void SaveBestIndivid()
    {
        using (StreamWriter StreamWrite = new StreamWriter("Logs/BestIndivid/BestIndivid.txt"))
        {
            string Output = string.Empty;
            for (int i = 0; i < Individ.CountOfGenes; ++i)
            {
                Output += BestIndivid.SetOfGenes[i].ToString() + "|";
            }
            Output += BestIndivid.Fitness.ToString() + "|" + CountOfCycleEvolution;
            StreamWrite.WriteLine(Output);
        }
    }

    public void SaveCurrentPopulation()
    {
        using (StreamWriter StreamWrite = new StreamWriter("Logs/LastPopulation/LastPopulation.txt"))
        {
            foreach (Individ _Individ in Population)
            {
                string Output = string.Empty;
                for (int i = 0; i < Individ.CountOfGenes; ++i)
                {
                    Output += _Individ.SetOfGenes[i].ToString() + "|";
                }
                Output += _Individ.Fitness.ToString();
                StreamWrite.WriteLine(Output);
            }
        }
    }

    public void SaveExemplarOfGeneticAlgorithm()
    {
        using (FileStream StreamWrite = new FileStream("BinaryFiles/ExemplarOfGeneticAlgorithm.bin", FileMode.OpenOrCreate))
        {
            BinaryFormatter Serializer = new BinaryFormatter();
            Serializer.Serialize(StreamWrite, this);
        }
    }

    void PopulationEstimate()
    {
        foreach (Individ _Individ in Population)
        {
            for (int i = 0; i < CountOfEstimate; ++i)
            {
                PopulationChecker = new Simulation(_Individ);
                while (PopulationChecker.StartSimulation()) ;
                _Individ.AddTheLastResult();
            }
            _Individ.Fitness /= CountOfEstimate;
        }
        Population.Sort(Individ.IndividCompare);
        if (BestIndivid.Fitness < Population[0].Fitness) 
        {
            BestIndivid = Population[0];
            TheEvolution.Add(BestIndivid);
            QualityPopulation = 0;
            SaveBestIndivid();
        }
        ++QualityPopulation;
    }

    void Selection()
    {
        Descendants = new List<Individ>();
        Population.RemoveRange(SetSizeForSelection, SetSizeForSelection);
        for (int Iter = 0; Iter < 2; ++Iter)
        {
            Shuffle<Individ>.ListShuffle(ref Population);
            for (int i = 0; i < SetSizeForSelection; i += 2)
            {
                Hybridization(RandomGenerator.RandomValue.Next(Individ.CountOfGenes), i);
            }
        }
        Population = Descendants;
    }

    void Hybridization(int CrossoverPoint, int Parent)
    {
        Descendants.Add(new Individ(0));
        Descendants.Add(new Individ(0));
        int ChildOne = Descendants.Count - 1;
        int ChildTwo = Descendants.Count - 2;
        for (int i = 0; i < CrossoverPoint; ++i)
        {
            Descendants[ChildOne].SetOfGenes[i] = Population[Parent].SetOfGenes[i];
            Descendants[ChildTwo].SetOfGenes[i] = Population[Parent + 1].SetOfGenes[i];
        }
        for (int i = CrossoverPoint; i < Individ.CountOfGenes; ++i)
        {
            Descendants[ChildOne].SetOfGenes[i] = Population[Parent + 1].SetOfGenes[i];
            Descendants[ChildTwo].SetOfGenes[i] = Population[Parent].SetOfGenes[i];
        }
        Descendants[ChildOne].Mutation();
        Descendants[ChildTwo].Mutation();
    }

    void ShakePopulation()
    {
        if (QualityPopulation == DegeneratePopulation)
        {
            Shuffle<Individ>.ListShuffle(ref Population);
            for (int i = 0; i < CountOfNewBlood; ++i)
            {
                Population[i] = new Individ();
            }
            QualityPopulation = 0;
        }
    }
}
