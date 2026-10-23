namespace Lab3
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
            this.components = new System.ComponentModel.Container();
            this.panelTop = new System.Windows.Forms.FlowLayoutPanel();
            this.btnLoadImage = new System.Windows.Forms.Button();
            this.btnSaveResult = new System.Windows.Forms.Button();
            this.btnResetImage = new System.Windows.Forms.Button();
            this.btnUseResultAsSource = new System.Windows.Forms.Button();
            this.lblSep1 = new System.Windows.Forms.Label();
            this.btnQuickApplyFilter = new System.Windows.Forms.Button();
            this.btnQuickApplyBezier = new System.Windows.Forms.Button();

            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanelImages = new System.Windows.Forms.TableLayoutPanel();
            this.lblOriginalTitle = new System.Windows.Forms.Label();
            this.lblResultTitle = new System.Windows.Forms.Label();
            this.pbOriginal = new System.Windows.Forms.PictureBox();
            this.pbResult = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusTime = new System.Windows.Forms.ToolStripStatusLabel();

            this.tabControlTools = new System.Windows.Forms.TabControl();
            
            // Tab 1: Filters Library
            this.tabFilters = new System.Windows.Forms.TabPage();
            this.lblSelectFilter = new System.Windows.Forms.Label();
            this.cmbFilterLibrary = new System.Windows.Forms.ComboBox();
            this.grpFilterInfo = new System.Windows.Forms.GroupBox();
            this.lblFilterInfoText = new System.Windows.Forms.Label();
            this.dgvFilterPreview = new System.Windows.Forms.DataGridView();
            this.btnApplyFilter = new System.Windows.Forms.Button();

            // Tab 2: Custom Filter
            this.tabCustomFilter = new System.Windows.Forms.TabPage();
            this.lblKernelSize = new System.Windows.Forms.Label();
            this.numKernelSize = new System.Windows.Forms.NumericUpDown();
            this.btnCreateGrid = new System.Windows.Forms.Button();
            this.dgvCustomKernel = new System.Windows.Forms.DataGridView();
            this.lblCustomName = new System.Windows.Forms.Label();
            this.txtCustomName = new System.Windows.Forms.TextBox();
            this.btnApplyCustom = new System.Windows.Forms.Button();
            this.btnAddCustomToLib = new System.Windows.Forms.Button();

            // Tab 3: Bezier Brightness
            this.tabBezier = new System.Windows.Forms.TabPage();
            this.lblBezierTitle = new System.Windows.Forms.Label();
            this.pbBezier = new System.Windows.Forms.PictureBox();
            this.panelPresets = new System.Windows.Forms.FlowLayoutPanel();
            this.btnLinear = new System.Windows.Forms.Button();
            this.btnBrighten = new System.Windows.Forms.Button();
            this.btnDarken = new System.Windows.Forms.Button();
            this.btnContrast = new System.Windows.Forms.Button();
            this.btnInvert = new System.Windows.Forms.Button();
            this.lblBezierOrder = new System.Windows.Forms.Label();
            this.numBezierOrder = new System.Windows.Forms.NumericUpDown();
            this.chkLivePreview = new System.Windows.Forms.CheckBox();
            this.lblPointsInfo = new System.Windows.Forms.Label();
            this.btnApplyBezier = new System.Windows.Forms.Button();

            // Tab 4: Noise & Extras
            this.tabNoise = new System.Windows.Forms.TabPage();
            this.lblNoiseTitle = new System.Windows.Forms.Label();
            this.lblNoisePercent = new System.Windows.Forms.Label();
            this.numNoisePercent = new System.Windows.Forms.NumericUpDown();
            this.btnAddNoise = new System.Windows.Forms.Button();
            this.btnAddGaussNoise = new System.Windows.Forms.Button();

            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.tableLayoutPanelImages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOriginal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbResult)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.tabControlTools.SuspendLayout();
            this.tabFilters.SuspendLayout();
            this.grpFilterInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilterPreview)).BeginInit();
            this.tabCustomFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numKernelSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomKernel)).BeginInit();
            this.tabBezier.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBezier)).BeginInit();
            this.panelPresets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBezierOrder)).BeginInit();
            this.tabNoise.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numNoisePercent)).BeginInit();
            this.SuspendLayout();

            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(242, 244, 247);
            this.panelTop.Controls.Add(this.btnLoadImage);
            this.panelTop.Controls.Add(this.btnSaveResult);
            this.panelTop.Controls.Add(this.btnResetImage);
            this.panelTop.Controls.Add(this.btnUseResultAsSource);
            this.panelTop.Controls.Add(this.lblSep1);
            this.panelTop.Controls.Add(this.btnQuickApplyFilter);
            this.panelTop.Controls.Add(this.btnQuickApplyBezier);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.panelTop.Size = new System.Drawing.Size(1260, 44);
            this.panelTop.TabIndex = 0;

            // btnLoadImage
            this.btnLoadImage.Text = "📁 Завантажити BMP";
            this.btnLoadImage.Size = new System.Drawing.Size(150, 30);
            this.btnLoadImage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnLoadImage.Click += new System.EventHandler(this.btnLoadImage_Click);

            // btnSaveResult
            this.btnSaveResult.Text = "💾 Зберегти результат";
            this.btnSaveResult.Size = new System.Drawing.Size(150, 30);
            this.btnSaveResult.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnSaveResult.Click += new System.EventHandler(this.btnSaveResult_Click);

            // btnResetImage
            this.btnResetImage.Text = "🔄 Скинути зміни";
            this.btnResetImage.Size = new System.Drawing.Size(125, 30);
            this.btnResetImage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnResetImage.Click += new System.EventHandler(this.btnResetImage_Click);

            // btnUseResultAsSource
            this.btnUseResultAsSource.Text = "◀ Результат як джерело";
            this.btnUseResultAsSource.Size = new System.Drawing.Size(160, 30);
            this.btnUseResultAsSource.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnUseResultAsSource.Click += new System.EventHandler(this.btnUseResultAsSource_Click);

            // lblSep1
            this.lblSep1.AutoSize = true;
            this.lblSep1.Text = " | ";
            this.lblSep1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSep1.ForeColor = System.Drawing.Color.Gray;
            this.lblSep1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 0);

            // btnQuickApplyFilter
            this.btnQuickApplyFilter.Text = "▶ Застосувати фільтр";
            this.btnQuickApplyFilter.Size = new System.Drawing.Size(155, 30);
            this.btnQuickApplyFilter.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnQuickApplyFilter.Click += new System.EventHandler(this.btnApplyFilter_Click);

            // btnQuickApplyBezier
            this.btnQuickApplyBezier.Text = "⚡ Застосувати Безьє";
            this.btnQuickApplyBezier.Size = new System.Drawing.Size(155, 30);
            this.btnQuickApplyBezier.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnQuickApplyBezier.Click += new System.EventHandler(this.btnApplyBezier_Click);

            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 44);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Size = new System.Drawing.Size(1260, 680);
            this.splitContainerMain.SplitterDistance = 780;
            this.splitContainerMain.TabIndex = 1;

            // 
            // splitContainerMain.Panel1 (Images)
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.tableLayoutPanelImages);
            this.splitContainerMain.Panel1.Controls.Add(this.statusStrip1);

            // tableLayoutPanelImages
            this.tableLayoutPanelImages.ColumnCount = 2;
            this.tableLayoutPanelImages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelImages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelImages.RowCount = 2;
            this.tableLayoutPanelImages.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanelImages.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelImages.Controls.Add(this.lblOriginalTitle, 0, 0);
            this.tableLayoutPanelImages.Controls.Add(this.lblResultTitle, 1, 0);
            this.tableLayoutPanelImages.Controls.Add(this.pbOriginal, 0, 1);
            this.tableLayoutPanelImages.Controls.Add(this.pbResult, 1, 1);
            this.tableLayoutPanelImages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelImages.Padding = new System.Windows.Forms.Padding(6);

            // lblOriginalTitle
            this.lblOriginalTitle.Text = "Вхідне (оригінальне) зображення";
            this.lblOriginalTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOriginalTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblOriginalTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lblResultTitle
            this.lblResultTitle.Text = "Результат обробки";
            this.lblResultTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblResultTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblResultTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // pbOriginal
            this.pbOriginal.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            this.pbOriginal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbOriginal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbOriginal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;

            // pbResult
            this.pbResult.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            this.pbResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbResult.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;

            // statusStrip1
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.statusLabel,
                this.statusTime
            });
            this.statusStrip1.Location = new System.Drawing.Point(0, 658);
            this.statusStrip1.Size = new System.Drawing.Size(780, 22);

            this.statusLabel.Spring = true;
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statusLabel.Text = "Оберіть зображення (.bmp) для початку роботи.";

            this.statusTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.statusTime.Text = "Час обробки: -";

            // 
            // splitContainerMain.Panel2 (Tools TabControl)
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.tabControlTools);
            this.splitContainerMain.Panel2.Padding = new System.Windows.Forms.Padding(4);

            // tabControlTools
            this.tabControlTools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlTools.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tabControlTools.Controls.Add(this.tabFilters);
            this.tabControlTools.Controls.Add(this.tabCustomFilter);
            this.tabControlTools.Controls.Add(this.tabBezier);
            this.tabControlTools.Controls.Add(this.tabNoise);

            // ----------------------------------------------------
            // Tab 1: Filters Library
            // ----------------------------------------------------
            this.tabFilters.Text = "Фільтри";
            this.tabFilters.Padding = new System.Windows.Forms.Padding(10);
            this.tabFilters.Controls.Add(this.lblSelectFilter);
            this.tabFilters.Controls.Add(this.cmbFilterLibrary);
            this.tabFilters.Controls.Add(this.grpFilterInfo);
            this.tabFilters.Controls.Add(this.btnApplyFilter);

            this.lblSelectFilter.Location = new System.Drawing.Point(10, 12);
            this.lblSelectFilter.Size = new System.Drawing.Size(350, 20);
            this.lblSelectFilter.Text = "Виберіть фільтр з бібліотеки:";
            this.lblSelectFilter.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);

            this.cmbFilterLibrary.Location = new System.Drawing.Point(10, 36);
            this.cmbFilterLibrary.Size = new System.Drawing.Size(430, 25);
            this.cmbFilterLibrary.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterLibrary.SelectedIndexChanged += new System.EventHandler(this.cmbFilterLibrary_SelectedIndexChanged);

            this.grpFilterInfo.Location = new System.Drawing.Point(10, 75);
            this.grpFilterInfo.Size = new System.Drawing.Size(430, 300);
            this.grpFilterInfo.Text = "Ядро вибраного фільтру";
            this.grpFilterInfo.Controls.Add(this.lblFilterInfoText);
            this.grpFilterInfo.Controls.Add(this.dgvFilterPreview);

            this.lblFilterInfoText.Location = new System.Drawing.Point(10, 25);
            this.lblFilterInfoText.Size = new System.Drawing.Size(410, 45);
            this.lblFilterInfoText.Text = "Розмір: 3x3";

            this.dgvFilterPreview.Location = new System.Drawing.Point(10, 75);
            this.dgvFilterPreview.Size = new System.Drawing.Size(410, 210);
            this.dgvFilterPreview.AllowUserToAddRows = false;
            this.dgvFilterPreview.AllowUserToDeleteRows = false;
            this.dgvFilterPreview.ReadOnly = true;
            this.dgvFilterPreview.RowHeadersVisible = false;
            this.dgvFilterPreview.ColumnHeadersVisible = false;
            this.dgvFilterPreview.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.btnApplyFilter.Location = new System.Drawing.Point(10, 390);
            this.btnApplyFilter.Size = new System.Drawing.Size(430, 40);
            this.btnApplyFilter.Text = "▶ Застосувати фільтр";
            this.btnApplyFilter.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnApplyFilter.BackColor = System.Drawing.Color.FromArgb(225, 238, 255);
            this.btnApplyFilter.Click += new System.EventHandler(this.btnApplyFilter_Click);

            // ----------------------------------------------------
            // Tab 2: Custom Filter
            // ----------------------------------------------------
            this.tabCustomFilter.Text = "Власний фільтр";
            this.tabCustomFilter.Padding = new System.Windows.Forms.Padding(10);
            this.tabCustomFilter.Controls.Add(this.lblKernelSize);
            this.tabCustomFilter.Controls.Add(this.numKernelSize);
            this.tabCustomFilter.Controls.Add(this.btnCreateGrid);
            this.tabCustomFilter.Controls.Add(this.dgvCustomKernel);
            this.tabCustomFilter.Controls.Add(this.lblCustomName);
            this.tabCustomFilter.Controls.Add(this.txtCustomName);
            this.tabCustomFilter.Controls.Add(this.btnApplyCustom);
            this.tabCustomFilter.Controls.Add(this.btnAddCustomToLib);

            this.lblKernelSize.Location = new System.Drawing.Point(10, 15);
            this.lblKernelSize.Size = new System.Drawing.Size(120, 20);
            this.lblKernelSize.Text = "Розмір ядра (NxN):";

            this.numKernelSize.Location = new System.Drawing.Point(135, 12);
            this.numKernelSize.Size = new System.Drawing.Size(60, 23);
            this.numKernelSize.Minimum = 3;
            this.numKernelSize.Maximum = 9;
            this.numKernelSize.Increment = 2; // 3, 5, 7, 9
            this.numKernelSize.Value = 3;

            this.btnCreateGrid.Location = new System.Drawing.Point(205, 10);
            this.btnCreateGrid.Size = new System.Drawing.Size(160, 26);
            this.btnCreateGrid.Text = "Згенерувати матрицю";
            this.btnCreateGrid.Click += new System.EventHandler(this.btnCreateGrid_Click);

            this.dgvCustomKernel.Location = new System.Drawing.Point(10, 48);
            this.dgvCustomKernel.Size = new System.Drawing.Size(430, 260);
            this.dgvCustomKernel.AllowUserToAddRows = false;
            this.dgvCustomKernel.AllowUserToDeleteRows = false;
            this.dgvCustomKernel.RowHeadersVisible = false;
            this.dgvCustomKernel.ColumnHeadersVisible = false;
            this.dgvCustomKernel.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.lblCustomName.Location = new System.Drawing.Point(10, 318);
            this.lblCustomName.Size = new System.Drawing.Size(100, 20);
            this.lblCustomName.Text = "Назва фільтру:";

            this.txtCustomName.Location = new System.Drawing.Point(115, 315);
            this.txtCustomName.Size = new System.Drawing.Size(325, 23);
            this.txtCustomName.Text = "Власний фільтр";

            this.btnApplyCustom.Location = new System.Drawing.Point(10, 350);
            this.btnApplyCustom.Size = new System.Drawing.Size(210, 40);
            this.btnApplyCustom.Text = "▶ Застосувати фільтр";
            this.btnApplyCustom.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnApplyCustom.BackColor = System.Drawing.Color.FromArgb(225, 238, 255);
            this.btnApplyCustom.Click += new System.EventHandler(this.btnApplyCustom_Click);

            this.btnAddCustomToLib.Location = new System.Drawing.Point(230, 350);
            this.btnAddCustomToLib.Size = new System.Drawing.Size(210, 40);
            this.btnAddCustomToLib.Text = "💾 Додати до бібліотеки";
            this.btnAddCustomToLib.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnAddCustomToLib.Click += new System.EventHandler(this.btnAddCustomToLib_Click);

            // ----------------------------------------------------
            // Tab 3: Bezier Curves & Brightness
            // ----------------------------------------------------
            this.tabBezier.Text = "Криві Безьє";
            this.tabBezier.Padding = new System.Windows.Forms.Padding(10);
            this.tabBezier.Controls.Add(this.lblBezierTitle);
            this.tabBezier.Controls.Add(this.pbBezier);
            this.tabBezier.Controls.Add(this.panelPresets);
            this.tabBezier.Controls.Add(this.lblBezierOrder);
            this.tabBezier.Controls.Add(this.numBezierOrder);
            this.tabBezier.Controls.Add(this.chkLivePreview);
            this.tabBezier.Controls.Add(this.lblPointsInfo);
            this.tabBezier.Controls.Add(this.btnApplyBezier);

            this.lblBezierTitle.Location = new System.Drawing.Point(10, 8);
            this.lblBezierTitle.Size = new System.Drawing.Size(355, 18);
            this.lblBezierTitle.Text = "Графік профілю корекції (перетягуйте точки):";
            this.lblBezierTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);

            this.pbBezier.Location = new System.Drawing.Point(10, 30);
            this.pbBezier.Size = new System.Drawing.Size(280, 280);
            this.pbBezier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbBezier.BackColor = System.Drawing.Color.White;
            this.pbBezier.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pbBezier.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbBezier_MouseDown);
            this.pbBezier.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbBezier_MouseMove);
            this.pbBezier.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbBezier_MouseUp);

            this.panelPresets.Location = new System.Drawing.Point(10, 315);
            this.panelPresets.Size = new System.Drawing.Size(430, 30);
            this.panelPresets.Controls.Add(this.btnLinear);
            this.panelPresets.Controls.Add(this.btnBrighten);
            this.panelPresets.Controls.Add(this.btnDarken);
            this.panelPresets.Controls.Add(this.btnContrast);
            this.panelPresets.Controls.Add(this.btnInvert);

            this.btnLinear.Text = "Лінійна";
            this.btnLinear.Size = new System.Drawing.Size(65, 26);
            this.btnLinear.Click += new System.EventHandler(this.btnPreset_Click);

            this.btnBrighten.Text = "Освітлення";
            this.btnBrighten.Size = new System.Drawing.Size(85, 26);
            this.btnBrighten.Click += new System.EventHandler(this.btnPreset_Click);

            this.btnDarken.Text = "Затемнення";
            this.btnDarken.Size = new System.Drawing.Size(85, 26);
            this.btnDarken.Click += new System.EventHandler(this.btnPreset_Click);

            this.btnContrast.Text = "Контраст";
            this.btnContrast.Size = new System.Drawing.Size(75, 26);
            this.btnContrast.Click += new System.EventHandler(this.btnPreset_Click);

            this.btnInvert.Text = "Негатив";
            this.btnInvert.Size = new System.Drawing.Size(75, 26);
            this.btnInvert.Click += new System.EventHandler(this.btnPreset_Click);

            this.lblBezierOrder.Location = new System.Drawing.Point(10, 352);
            this.lblBezierOrder.Size = new System.Drawing.Size(120, 20);
            this.lblBezierOrder.Text = "Порядок кривої N:";

            this.numBezierOrder.Location = new System.Drawing.Point(135, 350);
            this.numBezierOrder.Size = new System.Drawing.Size(55, 23);
            this.numBezierOrder.Minimum = 3;
            this.numBezierOrder.Maximum = 6;
            this.numBezierOrder.Value = 3;
            this.numBezierOrder.ValueChanged += new System.EventHandler(this.numBezierOrder_ValueChanged);

            this.chkLivePreview.Location = new System.Drawing.Point(200, 351);
            this.chkLivePreview.Size = new System.Drawing.Size(165, 22);
            this.chkLivePreview.Text = "Живий перегляд";
            this.chkLivePreview.Checked = true;
            this.chkLivePreview.CheckedChanged += new System.EventHandler(this.chkLivePreview_CheckedChanged);

            this.lblPointsInfo.Location = new System.Drawing.Point(10, 380);
            this.lblPointsInfo.Size = new System.Drawing.Size(355, 28);
            this.lblPointsInfo.Text = "P1: (85, 85) | P2: (170, 170)";
            this.lblPointsInfo.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

            this.btnApplyBezier.Location = new System.Drawing.Point(10, 415);
            this.btnApplyBezier.Size = new System.Drawing.Size(430, 38);
            this.btnApplyBezier.Text = "⚡ Застосувати корекцію яскравості";
            this.btnApplyBezier.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnApplyBezier.BackColor = System.Drawing.Color.FromArgb(225, 238, 255);
            this.btnApplyBezier.Click += new System.EventHandler(this.btnApplyBezier_Click);

            // ----------------------------------------------------
            // Tab 4: Noise & Extras
            // ----------------------------------------------------
            this.tabNoise.Text = "Шум";
            this.tabNoise.Padding = new System.Windows.Forms.Padding(10);
            this.tabNoise.Controls.Add(this.lblNoiseTitle);
            this.tabNoise.Controls.Add(this.lblNoisePercent);
            this.tabNoise.Controls.Add(this.numNoisePercent);
            this.tabNoise.Controls.Add(this.btnAddNoise);
            this.tabNoise.Controls.Add(this.btnAddGaussNoise);

            this.lblNoiseTitle.Location = new System.Drawing.Point(10, 15);
            this.lblNoiseTitle.Size = new System.Drawing.Size(350, 20);
            this.lblNoiseTitle.Text = "Генерація шуму для тестування фільтрів:";
            this.lblNoiseTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);

            this.lblNoisePercent.Location = new System.Drawing.Point(10, 50);
            this.lblNoisePercent.Size = new System.Drawing.Size(150, 20);
            this.lblNoisePercent.Text = "Відсоток шуму (%):";

            this.numNoisePercent.Location = new System.Drawing.Point(165, 48);
            this.numNoisePercent.Size = new System.Drawing.Size(65, 23);
            this.numNoisePercent.Minimum = 1;
            this.numNoisePercent.Maximum = 50;
            this.numNoisePercent.Value = 5;

            this.btnAddNoise.Location = new System.Drawing.Point(10, 85);
            this.btnAddNoise.Size = new System.Drawing.Size(430, 36);
            this.btnAddNoise.Text = "🧂 Додати імпульсний шум (Сіль та перець)";
            this.btnAddNoise.Click += new System.EventHandler(this.btnAddNoise_Click);

            this.btnAddGaussNoise.Location = new System.Drawing.Point(10, 130);
            this.btnAddGaussNoise.Size = new System.Drawing.Size(430, 36);
            this.btnAddGaussNoise.Text = "〰 Додати Гаусів шум";
            this.btnAddGaussNoise.Click += new System.EventHandler(this.btnAddGaussNoise_Click);

            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1260, 724);
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MinimumSize = new System.Drawing.Size(1000, 650);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Комп'ютерна графіка - Лабораторна робота №3: Цифрова фільтрація та криві Безьє";

            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel1.PerformLayout();
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.tableLayoutPanelImages.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbOriginal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbResult)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControlTools.ResumeLayout(false);
            this.tabFilters.ResumeLayout(false);
            this.grpFilterInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilterPreview)).EndInit();
            this.tabCustomFilter.ResumeLayout(false);
            this.tabCustomFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numKernelSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomKernel)).EndInit();
            this.tabBezier.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbBezier)).EndInit();
            this.panelPresets.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numBezierOrder)).EndInit();
            this.tabNoise.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numNoisePercent)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel panelTop;
        private System.Windows.Forms.Button btnLoadImage;
        private System.Windows.Forms.Button btnSaveResult;
        private System.Windows.Forms.Button btnResetImage;
        private System.Windows.Forms.Button btnUseResultAsSource;
        private System.Windows.Forms.Label lblSep1;
        private System.Windows.Forms.Button btnQuickApplyFilter;
        private System.Windows.Forms.Button btnQuickApplyBezier;

        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelImages;
        private System.Windows.Forms.Label lblOriginalTitle;
        private System.Windows.Forms.Label lblResultTitle;
        private System.Windows.Forms.PictureBox pbOriginal;
        private System.Windows.Forms.PictureBox pbResult;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripStatusLabel statusTime;

        private System.Windows.Forms.TabControl tabControlTools;

        // Tab 1
        private System.Windows.Forms.TabPage tabFilters;
        private System.Windows.Forms.Label lblSelectFilter;
        private System.Windows.Forms.ComboBox cmbFilterLibrary;
        private System.Windows.Forms.GroupBox grpFilterInfo;
        private System.Windows.Forms.Label lblFilterInfoText;
        private System.Windows.Forms.DataGridView dgvFilterPreview;
        private System.Windows.Forms.Button btnApplyFilter;

        // Tab 2
        private System.Windows.Forms.TabPage tabCustomFilter;
        private System.Windows.Forms.Label lblKernelSize;
        private System.Windows.Forms.NumericUpDown numKernelSize;
        private System.Windows.Forms.Button btnCreateGrid;
        private System.Windows.Forms.DataGridView dgvCustomKernel;
        private System.Windows.Forms.Label lblCustomName;
        private System.Windows.Forms.TextBox txtCustomName;
        private System.Windows.Forms.Button btnApplyCustom;
        private System.Windows.Forms.Button btnAddCustomToLib;

        // Tab 3
        private System.Windows.Forms.TabPage tabBezier;
        private System.Windows.Forms.Label lblBezierTitle;
        private System.Windows.Forms.PictureBox pbBezier;
        private System.Windows.Forms.FlowLayoutPanel panelPresets;
        private System.Windows.Forms.Button btnLinear;
        private System.Windows.Forms.Button btnBrighten;
        private System.Windows.Forms.Button btnDarken;
        private System.Windows.Forms.Button btnContrast;
        private System.Windows.Forms.Button btnInvert;
        private System.Windows.Forms.Label lblBezierOrder;
        private System.Windows.Forms.NumericUpDown numBezierOrder;
        private System.Windows.Forms.CheckBox chkLivePreview;
        private System.Windows.Forms.Label lblPointsInfo;
        private System.Windows.Forms.Button btnApplyBezier;

        // Tab 4
        private System.Windows.Forms.TabPage tabNoise;
        private System.Windows.Forms.Label lblNoiseTitle;
        private System.Windows.Forms.Label lblNoisePercent;
        private System.Windows.Forms.NumericUpDown numNoisePercent;
        private System.Windows.Forms.Button btnAddNoise;
        private System.Windows.Forms.Button btnAddGaussNoise;
    }
}
