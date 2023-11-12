namespace SiusTimer
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabelArduino = new ToolStripStatusLabel();
            toolStripStatusLabelSiusData = new ToolStripStatusLabel();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            testTCU25ToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem1 = new ToolStripMenuItem();
            labelTimer = new Label();
            comboBoxPrograms = new ComboBox();
            buttonProgramLoad = new Button();
            bindingSource1 = new BindingSource(components);
            listViewProgram = new ListView();
            columnHeaderGroup = new ColumnHeader();
            columnHeaderSound = new ColumnHeader();
            columnHeaderDelaySeconds = new ColumnHeader();
            columnHeaderLight = new ColumnHeader();
            columnHeaderTimer = new ColumnHeader();
            buttonStart = new Button();
            buttonContinue = new Button();
            columnHeaderRepeat = new ColumnHeader();
            statusStrip1.SuspendLayout();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabelArduino, toolStripStatusLabelSiusData });
            statusStrip1.Location = new Point(0, 630);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1161, 22);
            statusStrip1.TabIndex = 5;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelArduino
            // 
            toolStripStatusLabelArduino.Name = "toolStripStatusLabelArduino";
            toolStripStatusLabelArduino.Size = new Size(50, 17);
            toolStripStatusLabelArduino.Text = "Arduino";
            // 
            // toolStripStatusLabelSiusData
            // 
            toolStripStatusLabelSiusData.Name = "toolStripStatusLabelSiusData";
            toolStripStatusLabelSiusData.Size = new Size(52, 17);
            toolStripStatusLabelSiusData.Text = "SiusData";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, aboutToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1161, 24);
            menuStrip1.TabIndex = 6;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { testTCU25ToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "&File";
            // 
            // testTCU25ToolStripMenuItem
            // 
            testTCU25ToolStripMenuItem.Name = "testTCU25ToolStripMenuItem";
            testTCU25ToolStripMenuItem.Size = new Size(130, 22);
            testTCU25ToolStripMenuItem.Text = "&Test TCU25";
            testTCU25ToolStripMenuItem.Click += testTCU25ToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(130, 22);
            exitToolStripMenuItem.Text = "E&xit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aboutToolStripMenuItem1 });
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(44, 20);
            aboutToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem1
            // 
            aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            aboutToolStripMenuItem1.Size = new Size(107, 22);
            aboutToolStripMenuItem1.Text = "&About";
            // 
            // labelTimer
            // 
            labelTimer.AutoSize = true;
            labelTimer.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            labelTimer.Location = new Point(967, 38);
            labelTimer.Name = "labelTimer";
            labelTimer.Size = new Size(112, 32);
            labelTimer.TabIndex = 11;
            labelTimer.Text = "00:00:00";
            // 
            // comboBoxPrograms
            // 
            comboBoxPrograms.FormattingEnabled = true;
            comboBoxPrograms.Location = new Point(12, 38);
            comboBoxPrograms.Name = "comboBoxPrograms";
            comboBoxPrograms.Size = new Size(342, 23);
            comboBoxPrograms.TabIndex = 12;
            // 
            // buttonProgramLoad
            // 
            buttonProgramLoad.Location = new Point(360, 38);
            buttonProgramLoad.Name = "buttonProgramLoad";
            buttonProgramLoad.Size = new Size(75, 23);
            buttonProgramLoad.TabIndex = 13;
            buttonProgramLoad.Text = "&Load";
            buttonProgramLoad.UseVisualStyleBackColor = true;
            buttonProgramLoad.Click += buttonProgramLoad_Click;
            // 
            // bindingSource1
            // 
            bindingSource1.DataSource = typeof(PortInfo);
            // 
            // listViewProgram
            // 
            listViewProgram.Columns.AddRange(new ColumnHeader[] { columnHeaderGroup, columnHeaderSound, columnHeaderDelaySeconds, columnHeaderLight, columnHeaderTimer, columnHeaderRepeat });
            listViewProgram.FullRowSelect = true;
            listViewProgram.GridLines = true;
            listViewProgram.Location = new Point(12, 102);
            listViewProgram.Name = "listViewProgram";
            listViewProgram.Size = new Size(1067, 395);
            listViewProgram.TabIndex = 14;
            listViewProgram.UseCompatibleStateImageBehavior = false;
            listViewProgram.View = View.Details;
            listViewProgram.DoubleClick += listViewProgram_DoubleClick;
            // 
            // columnHeaderGroup
            // 
            columnHeaderGroup.Text = "Group";
            columnHeaderGroup.Width = 120;
            // 
            // columnHeaderSound
            // 
            columnHeaderSound.Text = "Sound";
            columnHeaderSound.Width = 300;
            // 
            // columnHeaderDelaySeconds
            // 
            columnHeaderDelaySeconds.Text = "Delay Seconds";
            columnHeaderDelaySeconds.Width = 100;
            // 
            // columnHeaderLight
            // 
            columnHeaderLight.Text = "TCU25";
            columnHeaderLight.Width = 100;
            // 
            // columnHeaderTimer
            // 
            columnHeaderTimer.Text = "TImer";
            // 
            // buttonStart
            // 
            buttonStart.Location = new Point(441, 38);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(75, 23);
            buttonStart.TabIndex = 15;
            buttonStart.Text = "&Start";
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += buttonStart_Click;
            // 
            // buttonContinue
            // 
            buttonContinue.Location = new Point(522, 38);
            buttonContinue.Name = "buttonContinue";
            buttonContinue.Size = new Size(75, 23);
            buttonContinue.TabIndex = 16;
            buttonContinue.Text = "Continue";
            buttonContinue.UseVisualStyleBackColor = true;
            buttonContinue.Click += buttonContinue_Click;
            // 
            // columnHeaderRepeat
            // 
            columnHeaderRepeat.Text = "Repeat";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1161, 652);
            Controls.Add(buttonContinue);
            Controls.Add(buttonStart);
            Controls.Add(listViewProgram);
            Controls.Add(buttonProgramLoad);
            Controls.Add(comboBoxPrograms);
            Controls.Add(labelTimer);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "MainForm";
            Text = "SiusTimer - morten@teinum.no";
            Load += MainForm_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private StatusStrip statusStrip1;
        private MenuStrip menuStrip1;

        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem1;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripStatusLabel toolStripStatusLabelSiusData;
        private ToolStripMenuItem testTCU25ToolStripMenuItem;
        private ToolStripStatusLabel toolStripStatusLabelArduino;

        private Label labelTimer;
        private ComboBox comboBoxPrograms;
        private Button buttonProgramLoad;
        private BindingSource bindingSource1;
        private ListView listViewProgram;
        private ColumnHeader columnHeaderSound;
        private ColumnHeader columnHeaderDelaySeconds;
        private ColumnHeader columnHeaderLight;
        private ColumnHeader columnHeaderTimer;
        private ColumnHeader columnHeaderGroup;
        private Button buttonStart;
        private Button buttonContinue;
        private ColumnHeader columnHeaderRepeat;
    }
}