using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Ecosystem
{
    public partial class ForestEcosystem : Form
    {
        public ForestEcosystem()
        {
            InitializeComponent();
        }

        Simulation MySimulator;
        int x = 2000, y = 2000, EffectiveSize;
        GeneticAlgorithm GA;
        Thread ThreadForGA;
        Organism SelectingOrganism;

        private void ForestEcosystem_Load(object sender, EventArgs e)
        {
            Individ InitialValue = new Individ(0);
            MySimulator = new Simulation(InitialValue, this.Width, this.Height);
            EffectiveSize = MySimulator.BitMap.EffectiveSize;
            GA = new GeneticAlgorithm(12, 20000);
        }

        private void ForestEcosystem_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            for (int i = 0; i < MySimulator.MyField.Size; i++)
                for (int j = 0; j < MySimulator.MyField.Size; j++)
                    g.FillRectangle(MySimulator.MyField.Cells[i, j].Brush, j * EffectiveSize, i * EffectiveSize, EffectiveSize, EffectiveSize);
            g.DrawRectangle(new Pen(Color.Red, 4), x, y, EffectiveSize, EffectiveSize);
        }

        private void GenerateNewGene_button_Click(object sender, EventArgs e)
        {
            CountOfIteration_label.Text = "0";
            MySimulator = new Simulation(Logic.LoadBestIndivid(), this.Width, this.Height);
            EffectiveSize = MySimulator.BitMap.EffectiveSize;
            x = -EffectiveSize;
            y = -EffectiveSize;
            SelectingOrganism = null;
            Invalidate();
        }

        private void StartSimulation_button_Click(object sender, EventArgs e)
        {
            x = -EffectiveSize;
            y = -EffectiveSize;
            if (Simulation_timer.Enabled == false)
            {
                CountOfIteration_label.Text = "0";
                Simulation_timer.Start();
                StartSimulation_button.Text = "Stop simulation";
                GenerateNewMap_button.Enabled = false;
            }
            else
            {
                Simulation_timer.Stop();
                StartSimulation_button.Text = "Start simulation";
                GenerateNewMap_button.Enabled = true;
            }
        }

        private void GeneticAlgorithm_button_Click(object sender, EventArgs e)
        {
            if (LoadGA_checkBox.Checked == true) GA = Logic.LoadExemplarOfGeneticAlgorithm();
            ThreadForGA = new Thread(new ThreadStart(StartGA));
            ThreadForGA.Start();
            StateGeneticAlgorithm_timer.Start();
            GeneticAlgorithm_button.Enabled = false;
            LoadGA_checkBox.Enabled = false;
        }
        
        private void Simulation_timer_Tick(object sender, EventArgs e)
        {
            if (!MySimulator.StartSimulation())
            {
                Simulation_timer.Stop();
                StartSimulation_button.Text = "Start simulation";
                GenerateNewMap_button.Enabled = true;
            }
            CountOfIteration_label.Text = MySimulator.ExperimentalIndividual.StatisticList[MySimulator.ExperimentalIndividual.StatisticList.Count - 1].Fitness.ToString();
            TrackOrganism();
            Invalidate();
        }

        private void ForestEcosystem_MouseDown(object sender, MouseEventArgs e)
        {
            if (Logic.CheckClickOnRange(e.X, e.Y, MySimulator.MyField.Size, EffectiveSize)) 
            {
                x = e.X / EffectiveSize * EffectiveSize; 
                y = e.Y / EffectiveSize * EffectiveSize;
                SelectingOrganism = MySimulator.MyField.GetInformationOfCell(new Position(y / EffectiveSize, x / EffectiveSize));
                TrackOrganism();
                Invalidate();
            }
        }

        void TrackOrganism()
        {
            string Information = string.Empty;
            string TypeOfAnimal = string.Empty;
            if (SelectingOrganism != null)
            {
                if (SelectingOrganism.Energy > -1)
                {
                    x = SelectingOrganism.Point.Y * EffectiveSize;
                    y = SelectingOrganism.Point.X * EffectiveSize;
                    if (MySimulator.MyField.CellGetState(SelectingOrganism.Point) == State.Herb)
                        label1.Text = string.Format("Растение:\n Энергия - {0} \n Предел для размножения {1};\n", SelectingOrganism.Energy, SelectingOrganism.ReproductionLimit);
                    else
                    {
                        if (MySimulator.MyField.CellGetState(SelectingOrganism.Point) == State.Herbivore) TypeOfAnimal = "Травоядное животное";
                        else TypeOfAnimal = "Хищник";
                        label1.Text = string.Format("{0}:\n Энергия - {1} \n Предел для размножения {2};\n", TypeOfAnimal, SelectingOrganism.Energy, SelectingOrganism.ReproductionLimit);
                    }
                }
                else label1.Text = "Животное умерло\nот истощения;";
            }
            else label1.Text = "Ячейка пустая;\n";
        }

        void StartGA()
        {
            GA.Start();
            GA.SaveBestIndivid();
            GA.SaveCurrentPopulation();
        }

        private void StateGeneticAlgorithm_timer_Tick(object sender, EventArgs e)
        {
            label4.Text = GA.CountOfCycleEvolution.ToString() + " из " + GA.CyclesOfEvolution.ToString() + Environment.NewLine;
            label4.Text += GA.BestIndivid.Fitness.ToString();
        }

        private void ForestEcosystem_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (ThreadForGA != null)
            {
                ThreadForGA.Abort();
                GA.SaveBestIndivid();
                GA.SaveCurrentPopulation();
                GA.SaveExemplarOfGeneticAlgorithm();
            }
        }

        
    }
}
