namespace Lab1
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pictureBoxGrid = new PictureBox();
            btnStartStop = new Button();
            btnStep = new Button();
            btnClear = new Button();
            btnRandom = new Button();
            btnSaveBmp = new Button();
            cbPresets = new ComboBox();
            txtRule = new TextBox();
            btnApplyRule = new Button();
            trackSpeed = new TrackBar();
            labelRulePresets = new Label();
            labelCustomRule = new Label();
            lblSpeed = new Label();
            lblGeneration = new Label();
            lblHint = new Label();
            timerSim = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)pictureBoxGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackSpeed).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxGrid
            // 
            pictureBoxGrid.BackColor = Color.Black;
            pictureBoxGrid.Location = new Point(12, 12);
            pictureBoxGrid.Name = "pictureBoxGrid";
            pictureBoxGrid.Size = new Size(800, 800);
            pictureBoxGrid.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxGrid.TabIndex = 0;
            pictureBoxGrid.TabStop = false;
            pictureBoxGrid.MouseDown += PictureBoxGrid_MouseDown;
            pictureBoxGrid.MouseMove += PictureBoxGrid_MouseMove;
            // 
            // btnStartStop
            // 
            btnStartStop.Location = new Point(12, 825);
            btnStartStop.Name = "btnStartStop";
            btnStartStop.Size = new Size(110, 35);
            btnStartStop.TabIndex = 1;
            btnStartStop.Text = "Start";
            btnStartStop.UseVisualStyleBackColor = true;
            btnStartStop.Click += btnStartStop_Click;
            // 
            // btnStep
            // 
            btnStep.Location = new Point(135, 825);
            btnStep.Name = "btnStep";
            btnStep.Size = new Size(110, 35);
            btnStep.TabIndex = 2;
            btnStep.Text = "Single Step";
            btnStep.UseVisualStyleBackColor = true;
            btnStep.Click += btnStep_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(258, 825);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(100, 35);
            btnClear.TabIndex = 3;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // btnRandom
            // 
            btnRandom.Location = new Point(371, 825);
            btnRandom.Name = "btnRandom";
            btnRandom.Size = new Size(120, 35);
            btnRandom.TabIndex = 4;
            btnRandom.Text = "Randomize";
            btnRandom.UseVisualStyleBackColor = true;
            btnRandom.Click += btnRandom_Click;
            // 
            // btnSaveBmp
            // 
            btnSaveBmp.Location = new Point(504, 825);
            btnSaveBmp.Name = "btnSaveBmp";
            btnSaveBmp.Size = new Size(140, 35);
            btnSaveBmp.TabIndex = 5;
            btnSaveBmp.Text = "Save to BMP";
            btnSaveBmp.UseVisualStyleBackColor = true;
            btnSaveBmp.Click += btnSaveBmp_Click;
            // 
            // cbPresets
            // 
            cbPresets.DropDownStyle = ComboBoxStyle.DropDownList;
            cbPresets.FormattingEnabled = true;
            cbPresets.Location = new Point(940, 20);
            cbPresets.Name = "cbPresets";
            cbPresets.Size = new Size(160, 23);
            cbPresets.TabIndex = 6;
            cbPresets.SelectedIndexChanged += cbPresets_SelectedIndexChanged;
            // 
            // txtRule
            // 
            txtRule.Location = new Point(940, 60);
            txtRule.Name = "txtRule";
            txtRule.Size = new Size(160, 23);
            txtRule.TabIndex = 7;
            txtRule.Text = "23/2/8";
            // 
            // btnApplyRule
            // 
            btnApplyRule.Location = new Point(940, 95);
            btnApplyRule.Name = "btnApplyRule";
            btnApplyRule.Size = new Size(160, 30);
            btnApplyRule.TabIndex = 9;
            btnApplyRule.Text = "Apply Rule";
            btnApplyRule.UseVisualStyleBackColor = true;
            btnApplyRule.Click += btnApplyRule_Click;
            // 
            // trackSpeed
            // 
            trackSpeed.Location = new Point(940, 150);
            trackSpeed.Maximum = 200;
            trackSpeed.Minimum = 1;
            trackSpeed.Name = "trackSpeed";
            trackSpeed.Size = new Size(160, 45);
            trackSpeed.TabIndex = 10;
            trackSpeed.Value = 150;
            trackSpeed.Scroll += trackSpeed_Scroll;
            // 
            // labelRulePresets
            // 
            labelRulePresets.AutoSize = true;
            labelRulePresets.Location = new Point(830, 23);
            labelRulePresets.Name = "labelRulePresets";
            labelRulePresets.Size = new Size(47, 15);
            labelRulePresets.TabIndex = 11;
            labelRulePresets.Text = "Presets:";
            // 
            // labelCustomRule
            // 
            labelCustomRule.AutoSize = true;
            labelCustomRule.Location = new Point(830, 63);
            labelCustomRule.Name = "labelCustomRule";
            labelCustomRule.Size = new Size(73, 15);
            labelCustomRule.TabIndex = 12;
            labelCustomRule.Text = "Rule (S/B/C):";
            // 
            // lblSpeed
            // 
            lblSpeed.AutoSize = true;
            lblSpeed.Location = new Point(830, 155);
            lblSpeed.Name = "lblSpeed";
            lblSpeed.Size = new Size(42, 15);
            lblSpeed.TabIndex = 13;
            lblSpeed.Text = "Speed:";
            // 
            // lblGeneration
            // 
            lblGeneration.AutoSize = true;
            lblGeneration.Font = new System.Drawing.Font("Segoe UI", 11F, FontStyle.Bold);
            lblGeneration.Location = new Point(830, 220);
            lblGeneration.Name = "lblGeneration";
            lblGeneration.Size = new Size(107, 20);
            lblGeneration.TabIndex = 14;
            lblGeneration.Text = "Generation: 0";
            // 
            // lblHint
            // 
            lblHint.AutoSize = true;
            lblHint.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Italic);
            lblHint.ForeColor = Color.DarkSlateGray;
            lblHint.Location = new Point(830, 260);
            lblHint.Name = "lblHint";
            lblHint.Size = new Size(205, 45);
            lblHint.TabIndex = 15;
            lblHint.Text = "Canvas controls hint:\n• Left-click / drag to draw alive cells\n• Right-click / drag to erase cells";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1130, 880);
            Controls.Add(lblHint);
            Controls.Add(lblGeneration);
            Controls.Add(lblSpeed);
            Controls.Add(labelCustomRule);
            Controls.Add(labelRulePresets);
            Controls.Add(trackSpeed);
            Controls.Add(btnApplyRule);
            Controls.Add(txtRule);
            Controls.Add(cbPresets);
            Controls.Add(btnSaveBmp);
            Controls.Add(btnRandom);
            Controls.Add(btnClear);
            Controls.Add(btnStep);
            Controls.Add(btnStartStop);
            Controls.Add(pictureBoxGrid);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cellular Automata: Generations";
            ((System.ComponentModel.ISupportInitialize)pictureBoxGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackSpeed).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBoxGrid;
        private Button btnStartStop;
        private Button btnStep;
        private Button btnClear;
        private Button btnRandom;
        private Button btnSaveBmp;
        private ComboBox cbPresets;
        private TextBox txtRule;
        private Button btnApplyRule;
        private TrackBar trackSpeed;
        private Label labelRulePresets;
        private Label labelCustomRule;
        private Label lblSpeed;
        private Label lblGeneration;
        private Label lblHint;
        private System.Windows.Forms.Timer timerSim;
    }
}