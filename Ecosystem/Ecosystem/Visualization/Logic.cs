using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

class Logic
{
    static public bool CheckClickOnRange(int x,int y, int Size, int EffectiveSize)
    {
        if (x > (Size * EffectiveSize) || y > (Size * EffectiveSize) || x < 0 || y < 0) return false;
        else return true;
    }

    static public Individ LoadBestIndivid()
    {
        if (File.Exists("Logs/BestIndivid/BestIndivid.txt"))
        {
            using (StreamReader StreamRead = new StreamReader("Logs/BestIndivid/BestIndivid.txt"))
            {
                string Input = string.Empty;
                double[] Param = new double[12];
                Input = StreamRead.ReadLine();
                for (int i = 0; i < Individ.CountOfGenes; ++i)
                {
                    int k = Input.IndexOf('|');
                    Param[i] = Convert.ToDouble(Input.Substring(0, k));
                    Input = Input.Remove(0, k + 1);
                }
                return new Individ(Param);
            }
        }
        else return new Individ(0);
    }

    static public GeneticAlgorithm LoadExemplarOfGeneticAlgorithm()
    {
        if (File.Exists("BinaryFiles/ExemplarOfGeneticAlgorithm.bin"))
        {
            BinaryFormatter Deserializer = new BinaryFormatter();
            using (FileStream StreamRead = new FileStream("BinaryFiles/ExemplarOfGeneticAlgorithm.bin", FileMode.OpenOrCreate))
            {
                return (GeneticAlgorithm)Deserializer.Deserialize(StreamRead);
            }
        }
        else return new GeneticAlgorithm(52, 20000);
    }
}

