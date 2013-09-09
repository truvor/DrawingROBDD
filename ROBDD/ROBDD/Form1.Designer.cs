namespace ROBDD
{
    partial class ROBDD
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buildGraph = new System.Windows.Forms.Button();
            this.formula = new System.Windows.Forms.TextBox();
            this.buildQueen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buildGraph
            // 
            this.buildGraph.Enabled = false;
            this.buildGraph.Location = new System.Drawing.Point(197, 12);
            this.buildGraph.Name = "buildGraph";
            this.buildGraph.Size = new System.Drawing.Size(75, 23);
            this.buildGraph.TabIndex = 0;
            this.buildGraph.Text = "Графу быть";
            this.buildGraph.UseVisualStyleBackColor = true;
            this.buildGraph.Click += new System.EventHandler(this.buildGraph_Click);
            // 
            // formula
            // 
            this.formula.Location = new System.Drawing.Point(12, 14);
            this.formula.Name = "formula";
            this.formula.Size = new System.Drawing.Size(160, 20);
            this.formula.TabIndex = 1;
            this.formula.Text = "Введите формулу";
            this.formula.MouseClick += new System.Windows.Forms.MouseEventHandler(this.formula_MouseClick);
            this.formula.TextChanged += new System.EventHandler(this.formula_TextChanged);
            // 
            // buildQueen
            // 
            this.buildQueen.Location = new System.Drawing.Point(97, 40);
            this.buildQueen.Name = "buildQueen";
            this.buildQueen.Size = new System.Drawing.Size(94, 23);
            this.buildQueen.TabIndex = 2;
            this.buildQueen.Text = "Граф о ферзях";
            this.buildQueen.UseVisualStyleBackColor = true;
            this.buildQueen.Click += new System.EventHandler(this.buildQueen_Click);
            // 
            // ROBDD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 69);
            this.Controls.Add(this.buildQueen);
            this.Controls.Add(this.formula);
            this.Controls.Add(this.buildGraph);
            this.Name = "ROBDD";
            this.Text = "ROBDD";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buildGraph;
        private System.Windows.Forms.TextBox formula;
        private System.Windows.Forms.Button buildQueen;
    }
}

