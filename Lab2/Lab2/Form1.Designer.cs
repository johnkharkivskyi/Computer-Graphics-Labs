namespace Lab2
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

        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pbSource = new System.Windows.Forms.PictureBox();
            this.pbTarget = new System.Windows.Forms.PictureBox();
            this.pbResult = new System.Windows.Forms.PictureBox();
            this.pbHistSource = new System.Windows.Forms.PictureBox();
            this.pbHistTarget = new System.Windows.Forms.PictureBox();
            this.pbHistResult = new System.Windows.Forms.PictureBox();
            
            this.panelTop = new System.Windows.Forms.FlowLayoutPanel();
            this.btnLoadSource = new System.Windows.Forms.Button();
            this.btnLoadTarget = new System.Windows.Forms.Button();
            
            this.lblSpace = new System.Windows.Forms.Label();
            this.cmbColorSpace = new System.Windows.Forms.ComboBox();
            
            this.chkApplyCh1 = new System.Windows.Forms.CheckBox();
            this.chkApplyCh2 = new System.Windows.Forms.CheckBox();
            this.chkApplyCh3 = new System.Windows.Forms.CheckBox();
            
            this.lblBrush = new System.Windows.Forms.Label();
            this.cmbBrushType = new System.Windows.Forms.ComboBox();
            this.lblBrushSize = new System.Windows.Forms.Label();
            this.numBrushSize = new System.Windows.Forms.NumericUpDown();
            this.btnClearMask = new System.Windows.Forms.Button();
            
            this.btnTransfer = new System.Windows.Forms.Button();
            
            this.lblChannel = new System.Windows.Forms.Label();
            this.cmbChannel = new System.Windows.Forms.ComboBox();
            
            this.btnSave = new System.Windows.Forms.Button();

            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTarget)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHistSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHistTarget)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHistResult)).BeginInit();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBrushSize)).BeginInit();
            this.SuspendLayout();
            
            // panelTop
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Height = 70;
            this.panelTop.Padding = new System.Windows.Forms.Padding(5);
            
            this.panelTop.Controls.Add(this.btnLoadSource);
            this.panelTop.Controls.Add(this.btnLoadTarget);
            this.panelTop.Controls.Add(this.lblSpace);
            this.panelTop.Controls.Add(this.cmbColorSpace);
            this.panelTop.Controls.Add(this.chkApplyCh1);
            this.panelTop.Controls.Add(this.chkApplyCh2);
            this.panelTop.Controls.Add(this.chkApplyCh3);
            this.panelTop.Controls.Add(this.lblBrush);
            this.panelTop.Controls.Add(this.cmbBrushType);
            this.panelTop.Controls.Add(this.lblBrushSize);
            this.panelTop.Controls.Add(this.numBrushSize);
            this.panelTop.Controls.Add(this.btnClearMask);
            this.panelTop.Controls.Add(this.btnTransfer);
            this.panelTop.Controls.Add(this.lblChannel);
            this.panelTop.Controls.Add(this.cmbChannel);
            this.panelTop.Controls.Add(this.btnSave);
            
            // Elements initialization
            this.btnLoadSource.Text = "Load Source";
            this.btnLoadSource.Size = new System.Drawing.Size(90, 25);
            this.btnLoadSource.Click += new System.EventHandler(this.btnLoadSource_Click);
            
            this.btnLoadTarget.Text = "Load Target";
            this.btnLoadTarget.Size = new System.Drawing.Size(90, 25);
            this.btnLoadTarget.Click += new System.EventHandler(this.btnLoadTarget_Click);

            this.lblSpace.Text = "Space:";
            this.lblSpace.AutoSize = true;
            this.lblSpace.Margin = new System.Windows.Forms.Padding(10, 5, 0, 0);

            this.cmbColorSpace.Size = new System.Drawing.Size(60, 25);
            this.cmbColorSpace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbColorSpace.Items.AddRange(new object[] { "Lab", "HSL", "HSV", "YIQ" });
            this.cmbColorSpace.SelectedIndex = 0;
            this.cmbColorSpace.SelectedIndexChanged += new System.EventHandler(this.cmbColorSpace_SelectedIndexChanged);

            this.chkApplyCh1.Text = "Ch1";
            this.chkApplyCh1.Checked = true;
            this.chkApplyCh1.AutoSize = true;
            this.chkApplyCh1.Margin = new System.Windows.Forms.Padding(10, 5, 0, 0);

            this.chkApplyCh2.Text = "Ch2";
            this.chkApplyCh2.Checked = true;
            this.chkApplyCh2.AutoSize = true;
            this.chkApplyCh2.Margin = new System.Windows.Forms.Padding(5, 5, 0, 0);

            this.chkApplyCh3.Text = "Ch3";
            this.chkApplyCh3.Checked = true;
            this.chkApplyCh3.AutoSize = true;
            this.chkApplyCh3.Margin = new System.Windows.Forms.Padding(5, 5, 0, 0);

            this.lblBrush.Text = "Brush:";
            this.lblBrush.AutoSize = true;
            this.lblBrush.Margin = new System.Windows.Forms.Padding(10, 5, 0, 0);

            this.cmbBrushType.Size = new System.Drawing.Size(90, 25);
            this.cmbBrushType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBrushType.Items.AddRange(new object[] { "None", "Rectangle", "Circle" });
            this.cmbBrushType.SelectedIndex = 0;

            this.lblBrushSize.Text = "Size:";
            this.lblBrushSize.AutoSize = true;
            this.lblBrushSize.Margin = new System.Windows.Forms.Padding(5, 5, 0, 0);

            this.numBrushSize.Size = new System.Drawing.Size(45, 25);
            this.numBrushSize.Minimum = 1;
            this.numBrushSize.Maximum = 200;
            this.numBrushSize.Value = 20;

            this.btnClearMask.Text = "Clear Mask";
            this.btnClearMask.Size = new System.Drawing.Size(80, 25);
            this.btnClearMask.Click += new System.EventHandler(this.btnClearMask_Click);

            this.btnTransfer.Text = "Transfer Color";
            this.btnTransfer.Size = new System.Drawing.Size(95, 25);
            this.btnTransfer.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnTransfer.Click += new System.EventHandler(this.btnTransfer_Click);

            this.lblChannel.Text = "View:";
            this.lblChannel.AutoSize = true;
            this.lblChannel.Margin = new System.Windows.Forms.Padding(10, 5, 0, 0);

            this.cmbChannel.Size = new System.Drawing.Size(80, 25);
            this.cmbChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChannel.Items.AddRange(new object[] { "All", "Channel 1", "Channel 2", "Channel 3" });
            this.cmbChannel.SelectedIndex = 0;
            this.cmbChannel.SelectedIndexChanged += new System.EventHandler(this.cmbChannel_SelectedIndexChanged);

            this.btnSave.Text = "Save BMP";
            this.btnSave.Size = new System.Drawing.Size(80, 25);
            this.btnSave.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // tableLayoutPanel1
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            
            this.tableLayoutPanel1.Controls.Add(this.pbSource, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pbTarget, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.pbResult, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.pbHistSource, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pbHistTarget, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.pbHistResult, 2, 1);

            // pbSource, pbTarget, pbResult
            this.pbSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbSource.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbSource.Cursor = System.Windows.Forms.Cursors.Cross;

            this.pbTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbTarget.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbTarget.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbTarget.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pbTarget.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbTarget_MouseDown);
            this.pbTarget.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbTarget_MouseMove);

            this.pbResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbResult.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Histograms
            this.pbHistSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbHistSource.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbHistSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.pbHistTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbHistTarget.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbHistTarget.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.pbHistResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbHistResult.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbHistResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Form1
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panelTop);
            this.Text = "Lab 2: Statistical Color Correction";
            
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTarget)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHistSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHistTarget)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHistResult)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBrushSize)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pbSource;
        private System.Windows.Forms.PictureBox pbTarget;
        private System.Windows.Forms.PictureBox pbResult;
        private System.Windows.Forms.PictureBox pbHistSource;
        private System.Windows.Forms.PictureBox pbHistTarget;
        private System.Windows.Forms.PictureBox pbHistResult;
        private System.Windows.Forms.FlowLayoutPanel panelTop;
        private System.Windows.Forms.Button btnLoadSource;
        private System.Windows.Forms.Button btnLoadTarget;
        private System.Windows.Forms.ComboBox cmbColorSpace;
        private System.Windows.Forms.Button btnTransfer;
        private System.Windows.Forms.ComboBox cmbChannel;
        private System.Windows.Forms.Label lblSpace;
        private System.Windows.Forms.Label lblChannel;
        private System.Windows.Forms.CheckBox chkApplyCh1;
        private System.Windows.Forms.CheckBox chkApplyCh2;
        private System.Windows.Forms.CheckBox chkApplyCh3;
        private System.Windows.Forms.Label lblBrush;
        private System.Windows.Forms.ComboBox cmbBrushType;
        private System.Windows.Forms.Label lblBrushSize;
        private System.Windows.Forms.NumericUpDown numBrushSize;
        private System.Windows.Forms.Button btnClearMask;
        private System.Windows.Forms.Button btnSave;
    }
}
