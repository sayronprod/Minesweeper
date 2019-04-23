namespace Minesweeper
{
    partial class BestTimes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BestTimes));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.BeginnerTime = new System.Windows.Forms.Label();
            this.IntermediateTime = new System.Windows.Forms.Label();
            this.ExpertTime = new System.Windows.Forms.Label();
            this.Beginner_Name = new System.Windows.Forms.Label();
            this.IntermediateName = new System.Windows.Forms.Label();
            this.ExpertName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(176, 93);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 93);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "Reset Score";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Beginner";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Intermediate";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Expert";
            // 
            // BeginnerTime
            // 
            this.BeginnerTime.Location = new System.Drawing.Point(80, 9);
            this.BeginnerTime.Name = "BeginnerTime";
            this.BeginnerTime.Size = new System.Drawing.Size(89, 13);
            this.BeginnerTime.TabIndex = 1;
            this.BeginnerTime.Text = "Expert";
            // 
            // IntermediateTime
            // 
            this.IntermediateTime.Location = new System.Drawing.Point(80, 38);
            this.IntermediateTime.Name = "IntermediateTime";
            this.IntermediateTime.Size = new System.Drawing.Size(89, 13);
            this.IntermediateTime.TabIndex = 1;
            this.IntermediateTime.Text = "Expert";
            // 
            // ExpertTime
            // 
            this.ExpertTime.Location = new System.Drawing.Point(80, 65);
            this.ExpertTime.Name = "ExpertTime";
            this.ExpertTime.Size = new System.Drawing.Size(89, 13);
            this.ExpertTime.TabIndex = 1;
            this.ExpertTime.Text = "Expert";
            // 
            // Beginner_Name
            // 
            this.Beginner_Name.Location = new System.Drawing.Point(176, 9);
            this.Beginner_Name.Name = "Beginner_Name";
            this.Beginner_Name.Size = new System.Drawing.Size(78, 13);
            this.Beginner_Name.TabIndex = 1;
            this.Beginner_Name.Text = "Expert";
            // 
            // IntermediateName
            // 
            this.IntermediateName.Location = new System.Drawing.Point(176, 38);
            this.IntermediateName.Name = "IntermediateName";
            this.IntermediateName.Size = new System.Drawing.Size(78, 13);
            this.IntermediateName.TabIndex = 1;
            this.IntermediateName.Text = "Expert";
            // 
            // ExpertName
            // 
            this.ExpertName.Location = new System.Drawing.Point(176, 65);
            this.ExpertName.Name = "ExpertName";
            this.ExpertName.Size = new System.Drawing.Size(78, 13);
            this.ExpertName.TabIndex = 1;
            this.ExpertName.Text = "Expert";
            // 
            // BestTimes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(263, 124);
            this.Controls.Add(this.ExpertName);
            this.Controls.Add(this.IntermediateName);
            this.Controls.Add(this.Beginner_Name);
            this.Controls.Add(this.ExpertTime);
            this.Controls.Add(this.IntermediateTime);
            this.Controls.Add(this.BeginnerTime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BestTimes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Fastest Mine sweepers";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label BeginnerTime;
        private System.Windows.Forms.Label IntermediateTime;
        private System.Windows.Forms.Label ExpertTime;
        private System.Windows.Forms.Label Beginner_Name;
        private System.Windows.Forms.Label IntermediateName;
        private System.Windows.Forms.Label ExpertName;
    }
}