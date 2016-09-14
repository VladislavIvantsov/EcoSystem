namespace Ecosystem
{
    partial class ForestEcosystem
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.StartSimulation_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Simulation_timer = new System.Windows.Forms.Timer(this.components);
            this.GenerateNewMap_button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CountOfIteration_label = new System.Windows.Forms.Label();
            this.GeneticAlgorithm_button = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.StateGeneticAlgorithm_timer = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.LoadGA_checkBox = new System.Windows.Forms.CheckBox();
            this.TrackOrganism_timer = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // StartSimulation_button
            // 
            this.StartSimulation_button.Location = new System.Drawing.Point(568, 41);
            this.StartSimulation_button.Name = "StartSimulation_button";
            this.StartSimulation_button.Size = new System.Drawing.Size(120, 23);
            this.StartSimulation_button.TabIndex = 0;
            this.StartSimulation_button.Text = "Start simulation";
            this.StartSimulation_button.UseVisualStyleBackColor = true;
            this.StartSimulation_button.Click += new System.EventHandler(this.StartSimulation_button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.MaximumSize = new System.Drawing.Size(120, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 1;
            // 
            // Simulation_timer
            // 
            this.Simulation_timer.Tick += new System.EventHandler(this.Simulation_timer_Tick);
            // 
            // GenerateNewMap_button
            // 
            this.GenerateNewMap_button.Location = new System.Drawing.Point(568, 12);
            this.GenerateNewMap_button.Name = "GenerateNewMap_button";
            this.GenerateNewMap_button.Size = new System.Drawing.Size(120, 23);
            this.GenerateNewMap_button.TabIndex = 2;
            this.GenerateNewMap_button.Text = "Generate the Map";
            this.GenerateNewMap_button.UseVisualStyleBackColor = true;
            this.GenerateNewMap_button.Click += new System.EventHandler(this.GenerateNewGene_button_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(565, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Информация о ячейке:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(565, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Количество итераций:";
            // 
            // CountOfIteration_label
            // 
            this.CountOfIteration_label.AutoSize = true;
            this.CountOfIteration_label.Location = new System.Drawing.Point(621, 89);
            this.CountOfIteration_label.MaximumSize = new System.Drawing.Size(100, 0);
            this.CountOfIteration_label.Name = "CountOfIteration_label";
            this.CountOfIteration_label.Size = new System.Drawing.Size(13, 13);
            this.CountOfIteration_label.TabIndex = 5;
            this.CountOfIteration_label.Text = "0";
            // 
            // GeneticAlgorithm_button
            // 
            this.GeneticAlgorithm_button.Location = new System.Drawing.Point(564, 293);
            this.GeneticAlgorithm_button.Name = "GeneticAlgorithm_button";
            this.GeneticAlgorithm_button.Size = new System.Drawing.Size(120, 23);
            this.GeneticAlgorithm_button.TabIndex = 6;
            this.GeneticAlgorithm_button.Text = "Genetic algorithm";
            this.GeneticAlgorithm_button.UseVisualStyleBackColor = true;
            this.GeneticAlgorithm_button.Click += new System.EventHandler(this.GeneticAlgorithm_button_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(564, 133);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(120, 154);
            this.panel1.TabIndex = 7;
            // 
            // StateGeneticAlgorithm_timer
            // 
            this.StateGeneticAlgorithm_timer.Tick += new System.EventHandler(this.StateGeneticAlgorithm_timer_Tick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(565, 387);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 13);
            this.label4.TabIndex = 8;
            // 
            // LoadGA_checkBox
            // 
            this.LoadGA_checkBox.AutoSize = true;
            this.LoadGA_checkBox.Location = new System.Drawing.Point(568, 322);
            this.LoadGA_checkBox.Name = "LoadGA_checkBox";
            this.LoadGA_checkBox.Size = new System.Drawing.Size(109, 17);
            this.LoadGA_checkBox.TabIndex = 9;
            this.LoadGA_checkBox.Text = "Load check point";
            this.LoadGA_checkBox.UseVisualStyleBackColor = true;
            // 
            // TrackOrganism_timer
            // 
            this.TrackOrganism_timer.Enabled = true;
            // 
            // ForestEcosystem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 562);
            this.Controls.Add(this.LoadGA_checkBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.GeneticAlgorithm_button);
            this.Controls.Add(this.CountOfIteration_label);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.GenerateNewMap_button);
            this.Controls.Add(this.StartSimulation_button);
            this.DoubleBuffered = true;
            this.Name = "ForestEcosystem";
            this.Text = "ForestEcosystem";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ForestEcosystem_FormClosed);
            this.Load += new System.EventHandler(this.ForestEcosystem_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ForestEcosystem_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ForestEcosystem_MouseDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartSimulation_button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer Simulation_timer;
        private System.Windows.Forms.Button GenerateNewMap_button;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label CountOfIteration_label;
        private System.Windows.Forms.Button GeneticAlgorithm_button;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer StateGeneticAlgorithm_timer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox LoadGA_checkBox;
        private System.Windows.Forms.Timer TrackOrganism_timer;


    }
}

