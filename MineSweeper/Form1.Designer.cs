namespace Minesweeper
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
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
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.expertItemMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameItemMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.beginnerItemMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.intermediateItemMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.expertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customItemMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.besttimesItemMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gameTime = new System.Windows.Forms.Label();
            this.newGame = new System.Windows.Forms.Button();
            this.mineCount = new System.Windows.Forms.Label();
            this.board = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.menu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.expertItemMenu,
            this.helpToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Padding = new System.Windows.Forms.Padding(0);
            this.menu.Size = new System.Drawing.Size(229, 24);
            this.menu.TabIndex = 0;
            this.menu.Text = "menuStrip1";
            // 
            // expertItemMenu
            // 
            this.expertItemMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameItemMenu,
            this.toolStripSeparator1,
            this.beginnerItemMenu,
            this.intermediateItemMenu,
            this.expertToolStripMenuItem,
            this.customItemMenu,
            this.toolStripSeparator2,
            this.besttimesItemMenu,
            this.toolStripSeparator4,
            this.exitMenuItem});
            this.expertItemMenu.Name = "expertItemMenu";
            this.expertItemMenu.Size = new System.Drawing.Size(50, 24);
            this.expertItemMenu.Text = "Game";
            // 
            // newGameItemMenu
            // 
            this.newGameItemMenu.Name = "newGameItemMenu";
            this.newGameItemMenu.ShortcutKeyDisplayString = "F2";
            this.newGameItemMenu.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.newGameItemMenu.Size = new System.Drawing.Size(141, 22);
            this.newGameItemMenu.Text = "New";
            this.newGameItemMenu.Click += new System.EventHandler(this.newGameItemMenu_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(138, 6);
            // 
            // beginnerItemMenu
            // 
            this.beginnerItemMenu.Checked = true;
            this.beginnerItemMenu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.beginnerItemMenu.Name = "beginnerItemMenu";
            this.beginnerItemMenu.Size = new System.Drawing.Size(141, 22);
            this.beginnerItemMenu.Text = "Beginner";
            this.beginnerItemMenu.Click += new System.EventHandler(this.lvlItemMenu_Click);
            // 
            // intermediateItemMenu
            // 
            this.intermediateItemMenu.Name = "intermediateItemMenu";
            this.intermediateItemMenu.Size = new System.Drawing.Size(141, 22);
            this.intermediateItemMenu.Text = "Intermediate";
            this.intermediateItemMenu.ToolTipText = "F2";
            this.intermediateItemMenu.Click += new System.EventHandler(this.lvlItemMenu_Click);
            // 
            // expertToolStripMenuItem
            // 
            this.expertToolStripMenuItem.Name = "expertToolStripMenuItem";
            this.expertToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.expertToolStripMenuItem.Text = "Expert";
            this.expertToolStripMenuItem.Click += new System.EventHandler(this.lvlItemMenu_Click);
            // 
            // customItemMenu
            // 
            this.customItemMenu.Name = "customItemMenu";
            this.customItemMenu.Size = new System.Drawing.Size(141, 22);
            this.customItemMenu.Text = "Custom...";
            this.customItemMenu.Click += new System.EventHandler(this.customItemMenu_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(138, 6);
            // 
            // besttimesItemMenu
            // 
            this.besttimesItemMenu.Name = "besttimesItemMenu";
            this.besttimesItemMenu.Size = new System.Drawing.Size(141, 22);
            this.besttimesItemMenu.Text = "Best Times...";
            this.besttimesItemMenu.Click += new System.EventHandler(this.besttimesItemMenu_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(138, 6);
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.Size = new System.Drawing.Size(141, 22);
            this.exitMenuItem.Text = "Exit";
            this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Name = "aboutMenuItem";
            this.aboutMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutMenuItem.Text = "About";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.gameTime);
            this.panel1.Controls.Add(this.newGame);
            this.panel1.Controls.Add(this.mineCount);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.panel1.MinimumSize = new System.Drawing.Size(220, 45);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(229, 45);
            this.panel1.TabIndex = 1;
            // 
            // gameTime
            // 
            this.gameTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.gameTime.AutoSize = true;
            this.gameTime.BackColor = System.Drawing.Color.Black;
            this.gameTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gameTime.Font = new System.Drawing.Font("Digital-7 Mono", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameTime.ForeColor = System.Drawing.Color.Red;
            this.gameTime.Location = new System.Drawing.Point(157, 0);
            this.gameTime.Margin = new System.Windows.Forms.Padding(1);
            this.gameTime.Name = "gameTime";
            this.gameTime.Size = new System.Drawing.Size(68, 39);
            this.gameTime.TabIndex = 2;
            this.gameTime.Text = "000";
            // 
            // newGame
            // 
            this.newGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.newGame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.newGame.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.newGame.FlatAppearance.BorderSize = 2;
            this.newGame.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.newGame.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.newGame.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.newGame.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.newGame.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.newGame.Location = new System.Drawing.Point(95, 1);
            this.newGame.Margin = new System.Windows.Forms.Padding(1);
            this.newGame.MaximumSize = new System.Drawing.Size(40, 40);
            this.newGame.MinimumSize = new System.Drawing.Size(40, 40);
            this.newGame.Name = "newGame";
            this.newGame.Size = new System.Drawing.Size(40, 40);
            this.newGame.TabIndex = 0;
            this.newGame.UseVisualStyleBackColor = true;
            this.newGame.MouseDown += new System.Windows.Forms.MouseEventHandler(this.newGame_MouseDown);
            this.newGame.MouseEnter += new System.EventHandler(this.newGame_MouseEnter);
            this.newGame.MouseLeave += new System.EventHandler(this.newGame_MouseLeave);
            this.newGame.MouseUp += new System.Windows.Forms.MouseEventHandler(this.newGame_MouseUp);
            // 
            // mineCount
            // 
            this.mineCount.AutoSize = true;
            this.mineCount.BackColor = System.Drawing.Color.Black;
            this.mineCount.Dock = System.Windows.Forms.DockStyle.Left;
            this.mineCount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mineCount.Font = new System.Drawing.Font("Digital-7 Mono", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mineCount.ForeColor = System.Drawing.Color.Red;
            this.mineCount.Location = new System.Drawing.Point(0, 0);
            this.mineCount.Margin = new System.Windows.Forms.Padding(0);
            this.mineCount.Name = "mineCount";
            this.mineCount.Size = new System.Drawing.Size(68, 39);
            this.mineCount.TabIndex = 0;
            this.mineCount.Text = "000";
            // 
            // board
            // 
            this.board.Dock = System.Windows.Forms.DockStyle.Fill;
            this.board.Location = new System.Drawing.Point(0, 69);
            this.board.Name = "board";
            this.board.Size = new System.Drawing.Size(229, 228);
            this.board.TabIndex = 2;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(107, 1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(44, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(229, 297);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.board);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = global::Minesweeper.Properties.Resources.icon;
            this.MainMenuStrip = this.menu;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Minesweeper";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem expertItemMenu;
        private System.Windows.Forms.ToolStripMenuItem newGameItemMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem beginnerItemMenu;
        private System.Windows.Forms.ToolStripMenuItem intermediateItemMenu;
        private System.Windows.Forms.ToolStripMenuItem expertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customItemMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem besttimesItemMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label mineCount;
        private System.Windows.Forms.Button newGame;
        private System.Windows.Forms.Label gameTime;
        private System.Windows.Forms.Panel board;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button1;
    }
}

