using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Lab3
{
    public partial class Form1 : Form
    {
        // Filter Kernel structure representing convolution and median filters
        public class FilterKernel
        {
            public string Name { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public double[,] Weights { get; set; }
            public double Divisor { get; set; }
            public double Offset { get; set; }
            public bool IsMedian { get; set; }

            public FilterKernel(string name, int width, int height, double[,] weights, double divisor = 1.0, double offset = 0.0, bool isMedian = false)
            {
                Name = name;
                Width = width;
                Height = height;
                Weights = weights;
                Divisor = divisor;
                Offset = offset;
                IsMedian = isMedian;
            }

            public override string ToString() => Name;
        }

        // Active image bitmaps
        private Bitmap? originalBmp;
        private Bitmap? currentBmp;
        private Bitmap? resultBmp;

        // Library of filters
        private List<FilterKernel> filterLibrary = new List<FilterKernel>();

        // Bezier curve control points
        private PointF[] controlPoints = new PointF[4];
        private int draggedPointIndex = -1;

        // Bezier canvas settings
        private const int GraphMargin = 15;
        private const int GraphWidth = 250;
        private const int GraphHeight = 250;
        private Bitmap bezierCanvasBmp;

        // Random generator for noise
        private Random random = new Random();

        public Form1()
        {
            InitializeComponent();

            // Initialize Bezier canvas
            bezierCanvasBmp = new Bitmap(pbBezier.Width, pbBezier.Height);
            pbBezier.Image = bezierCanvasBmp;

            // Initialize Bezier control points
            ResetBezierPointsLinear();

            // Initialize Filter Library
            InitializeFilterLibrary();

            // Setup Custom Filter Grid
            SetupCustomKernelGrid(3);

            statusLabel.Text = "Оберіть зображення (.bmp) для початку роботи.";
        }

        // -------------------------------------------------------------
        // Filter Library Setup
        // -------------------------------------------------------------

        private void InitializeFilterLibrary()
        {
            filterLibrary.Clear();

            // 1. Box Blur 3x3
            filterLibrary.Add(new FilterKernel(
                "Фільтр Бокса (Box Blur 3x3)",
                3, 3,
                new double[,] {
                    { 1, 1, 1 },
                    { 1, 1, 1 },
                    { 1, 1, 1 }
                },
                divisor: 9.0,
                offset: 0.0
            ));

            // 2. Gaussian Blur 3x3
            filterLibrary.Add(new FilterKernel(
                "Гаус-розмиття (Gaussian Blur 3x3)",
                3, 3,
                new double[,] {
                    { 1, 2, 1 },
                    { 2, 4, 2 },
                    { 1, 2, 1 }
                },
                divisor: 16.0,
                offset: 0.0
            ));

            // 3. Sharpen 3x3
            filterLibrary.Add(new FilterKernel(
                "Підвищення чіткості (Sharpen 3x3)",
                3, 3,
                new double[,] {
                    { -1, -2, -1 },
                    { -2, 22, -2 },
                    { -1, -2, -1 }
                },
                divisor: 10.0,
                offset: 0.0
            ));

            // 4. Edge Detection (Laplacian)
            filterLibrary.Add(new FilterKernel(
                "Знаходження границь (Laplacian 3x3)",
                3, 3,
                new double[,] {
                    {  0, -1,  0 },
                    { -1,  4, -1 },
                    {  0, -1,  0 }
                },
                divisor: 1.0,
                offset: 0.0
            ));

            // 5. Edge Detection (Sobel Horizontal)
            filterLibrary.Add(new FilterKernel(
                "Знаходження границь (Sobel Horiz 3x3)",
                3, 3,
                new double[,] {
                    { -1,  0,  1 },
                    { -2,  0,  2 },
                    { -1,  0,  1 }
                },
                divisor: 1.0,
                offset: 0.0
            ));

            // 6. Edge Detection (Sobel Vertical)
            filterLibrary.Add(new FilterKernel(
                "Знаходження границь (Sobel Vert 3x3)",
                3, 3,
                new double[,] {
                    { -1, -2, -1 },
                    {  0,  0,  0 },
                    {  1,  2,  1 }
                },
                divisor: 1.0,
                offset: 0.0
            ));

            // 7. Emboss 3x3
            filterLibrary.Add(new FilterKernel(
                "Тиснення (Emboss 3x3)",
                3, 3,
                new double[,] {
                    {  0,  1,  0 },
                    {  1,  0, -1 },
                    {  0, -1,  0 }
                },
                divisor: 1.0,
                offset: 128.0
            ));

            // 8. Median Filter 3x3
            filterLibrary.Add(new FilterKernel(
                "Медіанний фільтр (Median Filter 3x3)",
                3, 3,
                new double[3, 3],
                divisor: 1.0,
                offset: 0.0,
                isMedian: true
            ));

            cmbFilterLibrary.Items.Clear();
            foreach (var filter in filterLibrary)
            {
                cmbFilterLibrary.Items.Add(filter);
            }
            cmbFilterLibrary.SelectedIndex = 1;
        }

        private void cmbFilterLibrary_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFilterLibrary.SelectedItem is FilterKernel filter)
            {
                DisplayFilterDetails(filter);
            }
        }

        private void DisplayFilterDetails(FilterKernel filter)
        {
            if (filter.IsMedian)
            {
                lblFilterInfoText.Text = $"Медіанний фільтр ({filter.Width}x{filter.Height})";
                dgvFilterPreview.Columns.Clear();
                dgvFilterPreview.Rows.Clear();
                return;
            }

            lblFilterInfoText.Text = $"Розмір: {filter.Width}x{filter.Height} | Дільник: {filter.Divisor} | Зсув: {filter.Offset}";

            dgvFilterPreview.Columns.Clear();
            for (int col = 0; col < filter.Width; col++)
            {
                dgvFilterPreview.Columns.Add($"col{col}", "");
            }
            dgvFilterPreview.Rows.Add(filter.Height);

            for (int r = 0; r < filter.Height; r++)
            {
                for (int c = 0; c < filter.Width; c++)
                {
                    dgvFilterPreview.Rows[r].Cells[c].Value = filter.Weights[r, c];
                }
            }
        }

        // -------------------------------------------------------------
        // Convolution & Median Filtering
        // -------------------------------------------------------------

        private unsafe Bitmap ApplyConvolution(Bitmap src, FilterKernel filter)
        {
            int w = src.Width;
            int h = src.Height;
            Bitmap dst = new Bitmap(w, h, PixelFormat.Format24bppRgb);

            int kw = filter.Width;
            int kh = filter.Height;
            int rx = kw / 2;
            int ry = kh / 2;
            double divisor = (filter.Divisor == 0) ? 1.0 : filter.Divisor;
            double offset = filter.Offset;

            BitmapData srcData = src.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData dstData = dst.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            byte* srcPtr = (byte*)srcData.Scan0;
            byte* dstPtr = (byte*)dstData.Scan0;
            int stride = srcData.Stride;

            for (int y = 0; y < h; y++)
            {
                byte* dstRow = dstPtr + (y * stride);

                for (int x = 0; x < w; x++)
                {
                    double sumB = 0;
                    double sumG = 0;
                    double sumR = 0;

                    for (int ky = -ry; ky <= ry; ky++)
                    {
                        int py = y + ky;
                        if (py < 0) py = 0;
                        else if (py >= h) py = h - 1;

                        byte* srcRow = srcPtr + (py * stride);

                        for (int kx = -rx; kx <= rx; kx++)
                        {
                            int px = x + kx;
                            if (px < 0) px = 0;
                            else if (px >= w) px = w - 1;

                            double weight = filter.Weights[ky + ry, kx + rx];

                            byte b = srcRow[px * 3];
                            byte g = srcRow[px * 3 + 1];
                            byte r = srcRow[px * 3 + 2];

                            sumB += b * weight;
                            sumG += g * weight;
                            sumR += r * weight;
                        }
                    }

                    int finalB = (int)Math.Round(sumB / divisor + offset);
                    int finalG = (int)Math.Round(sumG / divisor + offset);
                    int finalR = (int)Math.Round(sumR / divisor + offset);

                    dstRow[x * 3] = (byte)Math.Clamp(finalB, 0, 255);
                    dstRow[x * 3 + 1] = (byte)Math.Clamp(finalG, 0, 255);
                    dstRow[x * 3 + 2] = (byte)Math.Clamp(finalR, 0, 255);
                }
            }

            src.UnlockBits(srcData);
            dst.UnlockBits(dstData);

            return dst;
        }

        private unsafe Bitmap ApplyMedian(Bitmap src, int radius = 1)
        {
            int w = src.Width;
            int h = src.Height;
            Bitmap dst = new Bitmap(w, h, PixelFormat.Format24bppRgb);

            int windowSize = (2 * radius + 1) * (2 * radius + 1);
            byte[] listB = new byte[windowSize];
            byte[] listG = new byte[windowSize];
            byte[] listR = new byte[windowSize];

            BitmapData srcData = src.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData dstData = dst.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            byte* srcPtr = (byte*)srcData.Scan0;
            byte* dstPtr = (byte*)dstData.Scan0;
            int stride = srcData.Stride;

            for (int y = 0; y < h; y++)
            {
                byte* dstRow = dstPtr + (y * stride);

                for (int x = 0; x < w; x++)
                {
                    int count = 0;

                    for (int ky = -radius; ky <= radius; ky++)
                    {
                        int py = y + ky;
                        if (py < 0) py = 0;
                        else if (py >= h) py = h - 1;

                        byte* srcRow = srcPtr + (py * stride);

                        for (int kx = -radius; kx <= radius; kx++)
                        {
                            int px = x + kx;
                            if (px < 0) px = 0;
                            else if (px >= w) px = w - 1;

                            listB[count] = srcRow[px * 3];
                            listG[count] = srcRow[px * 3 + 1];
                            listR[count] = srcRow[px * 3 + 2];
                            count++;
                        }
                    }

                    Array.Sort(listB, 0, count);
                    Array.Sort(listG, 0, count);
                    Array.Sort(listR, 0, count);

                    int medianIdx = count / 2;

                    dstRow[x * 3] = listB[medianIdx];
                    dstRow[x * 3 + 1] = listG[medianIdx];
                    dstRow[x * 3 + 2] = listR[medianIdx];
                }
            }

            src.UnlockBits(srcData);
            dst.UnlockBits(dstData);

            return dst;
        }

        private void btnApplyFilter_Click(object sender, EventArgs e)
        {
            if (currentBmp == null)
            {
                MessageBox.Show("Будь ласка, завантажте BMP зображення.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (cmbFilterLibrary.SelectedItem is FilterKernel filter)
            {
                Stopwatch sw = Stopwatch.StartNew();

                if (filter.IsMedian)
                {
                    resultBmp = ApplyMedian(currentBmp, 1);
                }
                else
                {
                    resultBmp = ApplyConvolution(currentBmp, filter);
                }

                sw.Stop();
                pbResult.Image = resultBmp;
                statusTime.Text = $"Час обробки: {sw.ElapsedMilliseconds} мс";
                statusLabel.Text = $"Застосовано: {filter.Name}";
            }
        }

        // -------------------------------------------------------------
        // Custom Filter Logic
        // -------------------------------------------------------------

        private void SetupCustomKernelGrid(int size)
        {
            dgvCustomKernel.Columns.Clear();
            dgvCustomKernel.Rows.Clear();

            for (int col = 0; col < size; col++)
            {
                var column = new DataGridViewTextBoxColumn
                {
                    Width = 45,
                    HeaderText = "",
                    SortMode = DataGridViewColumnSortMode.NotSortable
                };
                dgvCustomKernel.Columns.Add(column);
            }

            dgvCustomKernel.Rows.Add(size);

            int center = size / 2;
            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    dgvCustomKernel.Rows[r].Cells[c].Value = (r == center && c == center) ? 1.0 : 0.0;
                }
            }
        }

        private void btnCreateGrid_Click(object sender, EventArgs e)
        {
            int size = (int)numKernelSize.Value;
            if (size % 2 == 0) size++;
            SetupCustomKernelGrid(size);
        }

        private FilterKernel BuildCustomFilterFromUi()
        {
            int rows = dgvCustomKernel.RowCount;
            int cols = dgvCustomKernel.ColumnCount;
            double[,] weights = new double[rows, cols];
            double sum = 0;

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    string cellStr = dgvCustomKernel.Rows[r].Cells[c].Value?.ToString() ?? "0";
                    if (!double.TryParse(cellStr, out double val)) val = 0.0;
                    weights[r, c] = val;
                    sum += val;
                }
            }

            double divisor = (sum != 0) ? sum : 1.0;
            string name = string.IsNullOrWhiteSpace(txtCustomName.Text) ? "Власний фільтр" : txtCustomName.Text.Trim();

            return new FilterKernel(name, cols, rows, weights, divisor, 0.0);
        }

        private void btnApplyCustom_Click(object sender, EventArgs e)
        {
            if (currentBmp == null)
            {
                MessageBox.Show("Будь ласка, завантажте BMP зображення.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            FilterKernel filter = BuildCustomFilterFromUi();

            Stopwatch sw = Stopwatch.StartNew();
            resultBmp = ApplyConvolution(currentBmp, filter);
            sw.Stop();

            pbResult.Image = resultBmp;
            statusTime.Text = $"Час обробки: {sw.ElapsedMilliseconds} мс";
            statusLabel.Text = $"Застосовано: {filter.Name} ({filter.Width}x{filter.Height})";
        }

        private void btnAddCustomToLib_Click(object sender, EventArgs e)
        {
            FilterKernel filter = BuildCustomFilterFromUi();
            filterLibrary.Add(filter);
            cmbFilterLibrary.Items.Add(filter);
            cmbFilterLibrary.SelectedItem = filter;

            MessageBox.Show($"Фільтр '{filter.Name}' успішно додано до бібліотеки!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
            tabControlTools.SelectedTab = tabFilters;
        }

        // -------------------------------------------------------------
        // Bezier Curves: Mathematics & Manual Bresenham Rasterization
        // -------------------------------------------------------------

        private void ResetBezierPointsLinear()
        {
            SetBezierOrder(3);
        }

        private void SetBezierOrder(int order)
        {
            PointF[] newPts = new PointF[order + 1];
            newPts[0] = new PointF(0, 0);
            newPts[order] = new PointF(255, 255);

            for (int i = 1; i < order; i++)
            {
                float ratio = (float)i / order;
                newPts[i] = new PointF(ratio * 255f, ratio * 255f);
            }

            controlPoints = newPts;
            RenderBezierGraph();
            if (chkLivePreview != null && chkLivePreview.Checked && currentBmp != null)
            {
                ApplyBezierBrightness();
            }
        }

        private void numBezierOrder_ValueChanged(object sender, EventArgs e)
        {
            int order = (int)numBezierOrder.Value;
            SetBezierOrder(order);
        }

        private PointF CalculateCubicBezierPoint(double t, PointF p0, PointF p1, PointF p2, PointF p3)
        {
            double ax = -p0.X + 3.0 * p1.X - 3.0 * p2.X + p3.X;
            double bx = 3.0 * p0.X - 6.0 * p1.X + 3.0 * p2.X;
            double cx = -3.0 * p0.X + 3.0 * p1.X;
            double dx = p0.X;

            double ay = -p0.Y + 3.0 * p1.Y - 3.0 * p2.Y + p3.Y;
            double by = 3.0 * p0.Y - 6.0 * p1.Y + 3.0 * p2.Y;
            double cy = -3.0 * p0.Y + 3.0 * p1.Y;
            double dy = p0.Y;

            double t2 = t * t;
            double t3 = t2 * t;

            double x = ax * t3 + bx * t2 + cx * t + dx;
            double y = ay * t3 + by * t2 + cy * t + dy;

            return new PointF((float)x, (float)y);
        }

        private PointF CalculateGeneralBezierPoint(double t, PointF[] points)
        {
            int n = points.Length - 1;
            double x = 0;
            double y = 0;

            for (int i = 0; i <= n; i++)
            {
                double b = BinomialCoefficient(n, i) * Math.Pow(t, i) * Math.Pow(1.0 - t, n - i);
                x += b * points[i].X;
                y += b * points[i].Y;
            }

            return new PointF((float)x, (float)y);
        }

        private double BinomialCoefficient(int n, int k)
        {
            if (k < 0 || k > n) return 0;
            if (k == 0 || k == n) return 1;

            double result = 1.0;
            for (int i = 1; i <= k; i++)
            {
                result = result * (n - (k - i)) / i;
            }
            return result;
        }

        private PointF CalculateBezierPoint(double t)
        {
            if (controlPoints.Length == 4)
            {
                return CalculateCubicBezierPoint(t, controlPoints[0], controlPoints[1], controlPoints[2], controlPoints[3]);
            }
            return CalculateGeneralBezierPoint(t, controlPoints);
        }

        private void DrawLineBresenham(Bitmap bmp, int x0, int y0, int x1, int y1, Color color)
        {
            int dx = Math.Abs(x1 - x0);
            int dy = Math.Abs(y1 - y0);
            int sx = x0 < x1 ? 1 : -1;
            int sy = y0 < y1 ? 1 : -1;
            int err = dx - dy;

            while (true)
            {
                if (x0 >= 0 && x0 < bmp.Width && y0 >= 0 && y0 < bmp.Height)
                {
                    bmp.SetPixel(x0, y0, color);
                }

                if (x0 == x1 && y0 == y1) break;

                int e2 = 2 * err;
                if (e2 > -dy)
                {
                    err -= dy;
                    x0 += sx;
                }
                if (e2 < dx)
                {
                    err += dx;
                    y0 += sy;
                }
            }
        }

        private void DrawDashedLineBresenham(Bitmap bmp, int x0, int y0, int x1, int y1, Color color, int dashLen = 4, int spaceLen = 4)
        {
            int dx = Math.Abs(x1 - x0);
            int dy = Math.Abs(y1 - y0);
            int sx = x0 < x1 ? 1 : -1;
            int sy = y0 < y1 ? 1 : -1;
            int err = dx - dy;
            int step = 0;
            int period = dashLen + spaceLen;

            while (true)
            {
                if (step % period < dashLen)
                {
                    if (x0 >= 0 && x0 < bmp.Width && y0 >= 0 && y0 < bmp.Height)
                    {
                        bmp.SetPixel(x0, y0, color);
                    }
                }

                if (x0 == x1 && y0 == y1) break;

                int e2 = 2 * err;
                if (e2 > -dy)
                {
                    err -= dy;
                    x0 += sx;
                }
                if (e2 < dx)
                {
                    err += dx;
                    y0 += sy;
                }
                step++;
            }
        }

        private void DrawControlPoint(Bitmap bmp, int cx, int cy, int radius, Color color)
        {
            int r2 = radius * radius;
            for (int y = cy - radius; y <= cy + radius; y++)
            {
                for (int x = cx - radius; x <= cx + radius; x++)
                {
                    if (x >= 0 && x < bmp.Width && y >= 0 && y < bmp.Height)
                    {
                        if ((x - cx) * (x - cx) + (y - cy) * (y - cy) <= r2)
                        {
                            bmp.SetPixel(x, y, color);
                        }
                    }
                }
            }
        }

        private Point GraphToCanvas(PointF pt)
        {
            int cx = GraphMargin + (int)Math.Round((pt.X / 255.0) * GraphWidth);
            int cy = GraphMargin + GraphHeight - (int)Math.Round((pt.Y / 255.0) * GraphHeight);
            return new Point(cx, cy);
        }

        private PointF CanvasToGraph(int cx, int cy)
        {
            double gx = ((double)(cx - GraphMargin) / GraphWidth) * 255.0;
            double gy = ((double)(GraphMargin + GraphHeight - cy) / GraphHeight) * 255.0;
            return new PointF((float)Math.Clamp(gx, 0.0, 255.0), (float)Math.Clamp(gy, 0.0, 255.0));
        }

        private void RenderBezierGraph()
        {
            if (bezierCanvasBmp == null) return;

            using (Graphics g = Graphics.FromImage(bezierCanvasBmp))
            {
                g.Clear(Color.White);
            }

            Color gridColor = Color.FromArgb(230, 230, 235);
            Color axisColor = Color.FromArgb(120, 120, 130);
            Color diagColor = Color.FromArgb(190, 190, 200);
            Color curveColor = Color.FromArgb(20, 100, 220);
            Color polyColor = Color.FromArgb(180, 180, 190);

            // Coordinate grid
            for (int i = 0; i <= 256; i += 64)
            {
                Point pH0 = GraphToCanvas(new PointF(0, i));
                Point pH1 = GraphToCanvas(new PointF(255, i));
                DrawLineBresenham(bezierCanvasBmp, pH0.X, pH0.Y, pH1.X, pH1.Y, (i == 0 || i == 256) ? axisColor : gridColor);

                Point pV0 = GraphToCanvas(new PointF(i, 0));
                Point pV1 = GraphToCanvas(new PointF(i, 255));
                DrawLineBresenham(bezierCanvasBmp, pV0.X, pV0.Y, pV1.X, pV1.Y, (i == 0 || i == 256) ? axisColor : gridColor);
            }

            // Diagonal reference line
            Point d0 = GraphToCanvas(new PointF(0, 0));
            Point d1 = GraphToCanvas(new PointF(255, 255));
            DrawDashedLineBresenham(bezierCanvasBmp, d0.X, d0.Y, d1.X, d1.Y, diagColor, 4, 3);

            // Control polygon
            for (int i = 0; i < controlPoints.Length - 1; i++)
            {
                Point ca = GraphToCanvas(controlPoints[i]);
                Point cb = GraphToCanvas(controlPoints[i + 1]);
                DrawDashedLineBresenham(bezierCanvasBmp, ca.X, ca.Y, cb.X, cb.Y, polyColor, 3, 3);
            }

            // Sample and rasterize Bezier curve
            int samples = 500;
            Point prevCanvasPt = GraphToCanvas(CalculateBezierPoint(0));

            for (int i = 1; i <= samples; i++)
            {
                double t = (double)i / samples;
                PointF graphPt = CalculateBezierPoint(t);
                Point currCanvasPt = GraphToCanvas(graphPt);

                DrawLineBresenham(bezierCanvasBmp, prevCanvasPt.X, prevCanvasPt.Y, currCanvasPt.X, currCanvasPt.Y, curveColor);
                DrawLineBresenham(bezierCanvasBmp, prevCanvasPt.X, prevCanvasPt.Y + 1, currCanvasPt.X, currCanvasPt.Y + 1, curveColor);

                prevCanvasPt = currCanvasPt;
            }

            // Draw control points
            for (int i = 0; i < controlPoints.Length; i++)
            {
                Point c = GraphToCanvas(controlPoints[i]);
                if (i == 0 || i == controlPoints.Length - 1)
                {
                    DrawControlPoint(bezierCanvasBmp, c.X, c.Y, 4, Color.Black);
                }
                else
                {
                    DrawControlPoint(bezierCanvasBmp, c.X, c.Y, 5, Color.Crimson);
                }
            }

            pbBezier.Refresh();

            if (controlPoints.Length == 4)
            {
                lblPointsInfo.Text = $"P1: ({(int)controlPoints[1].X}, {(int)controlPoints[1].Y}) | P2: ({(int)controlPoints[2].X}, {(int)controlPoints[2].Y})";
            }
            else
            {
                lblPointsInfo.Text = $"Опорних точок: {controlPoints.Length} (порядок N={controlPoints.Length - 1})";
            }
        }

        // -------------------------------------------------------------
        // Bezier Mouse Interaction
        // -------------------------------------------------------------

        private void pbBezier_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            for (int i = 1; i < controlPoints.Length - 1; i++)
            {
                Point c = GraphToCanvas(controlPoints[i]);
                double dist = Math.Sqrt(Math.Pow(e.X - c.X, 2) + Math.Pow(e.Y - c.Y, 2));

                if (dist <= 12)
                {
                    draggedPointIndex = i;
                    break;
                }
            }
        }

        private void pbBezier_MouseMove(object sender, MouseEventArgs e)
        {
            if (draggedPointIndex != -1)
            {
                PointF newGraphPt = CanvasToGraph(e.X, e.Y);
                controlPoints[draggedPointIndex] = newGraphPt;

                RenderBezierGraph();

                if (chkLivePreview.Checked && currentBmp != null)
                {
                    ApplyBezierBrightness();
                }
            }
        }

        private void pbBezier_MouseUp(object sender, MouseEventArgs e)
        {
            if (draggedPointIndex != -1)
            {
                draggedPointIndex = -1;
                if (!chkLivePreview.Checked && currentBmp != null)
                {
                    ApplyBezierBrightness();
                }
            }
        }

        // -------------------------------------------------------------
        // Brightness Correction
        // -------------------------------------------------------------

        private byte[] BuildBezierLut()
        {
            byte[] lut = new byte[256];
            bool[] defined = new bool[256];

            int samples = 3000;
            for (int i = 0; i <= samples; i++)
            {
                double t = (double)i / samples;
                PointF pt = CalculateBezierPoint(t);

                int x = (int)Math.Round(pt.X);
                int y = (int)Math.Round(pt.Y);

                x = Math.Clamp(x, 0, 255);
                y = Math.Clamp(y, 0, 255);

                lut[x] = (byte)y;
                defined[x] = true;
            }

            lut[0] = (byte)Math.Clamp((int)Math.Round(controlPoints[0].Y), 0, 255);
            defined[0] = true;
            lut[255] = (byte)Math.Clamp((int)Math.Round(controlPoints[controlPoints.Length - 1].Y), 0, 255);
            defined[255] = true;

            int lastX = 0;
            for (int x = 1; x < 256; x++)
            {
                if (defined[x])
                {
                    for (int k = lastX + 1; k < x; k++)
                    {
                        double factor = (double)(k - lastX) / (x - lastX);
                        lut[k] = (byte)Math.Clamp((int)Math.Round(lut[lastX] + factor * (lut[x] - lut[lastX])), 0, 255);
                    }
                    lastX = x;
                }
            }

            return lut;
        }

        private unsafe Bitmap ApplyBrightnessCorrection(Bitmap src, byte[] lut)
        {
            int w = src.Width;
            int h = src.Height;
            Bitmap dst = new Bitmap(w, h, PixelFormat.Format24bppRgb);

            BitmapData srcData = src.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData dstData = dst.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            byte* srcPtr = (byte*)srcData.Scan0;
            byte* dstPtr = (byte*)dstData.Scan0;
            int stride = srcData.Stride;

            for (int y = 0; y < h; y++)
            {
                byte* srcRow = srcPtr + (y * stride);
                byte* dstRow = dstPtr + (y * stride);

                for (int x = 0; x < w; x++)
                {
                    dstRow[x * 3] = lut[srcRow[x * 3]];
                    dstRow[x * 3 + 1] = lut[srcRow[x * 3 + 1]];
                    dstRow[x * 3 + 2] = lut[srcRow[x * 3 + 2]];
                }
            }

            src.UnlockBits(srcData);
            dst.UnlockBits(dstData);

            return dst;
        }

        private void ApplyBezierBrightness()
        {
            if (currentBmp == null) return;

            byte[] lut = BuildBezierLut();
            Stopwatch sw = Stopwatch.StartNew();
            resultBmp = ApplyBrightnessCorrection(currentBmp, lut);
            sw.Stop();

            pbResult.Image = resultBmp;
            statusTime.Text = $"Час обробки: {sw.ElapsedMilliseconds} мс";
            statusLabel.Text = "Застосовано корекцію яскравості за кривою Безьє.";
        }

        private void btnApplyBezier_Click(object sender, EventArgs e)
        {
            if (currentBmp == null)
            {
                MessageBox.Show("Будь ласка, завантажте BMP зображення.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ApplyBezierBrightness();
        }

        private void chkLivePreview_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLivePreview.Checked && currentBmp != null)
            {
                ApplyBezierBrightness();
            }
        }

        private void btnPreset_Click(object sender, EventArgs e)
        {
            if (sender == btnLinear)
            {
                ResetBezierPointsLinear();
            }
            else if (sender == btnBrighten)
            {
                controlPoints[0] = new PointF(0, 0);
                controlPoints[1] = new PointF(50, 140);
                controlPoints[2] = new PointF(150, 230);
                controlPoints[3] = new PointF(255, 255);
            }
            else if (sender == btnDarken)
            {
                controlPoints[0] = new PointF(0, 0);
                controlPoints[1] = new PointF(100, 30);
                controlPoints[2] = new PointF(200, 120);
                controlPoints[3] = new PointF(255, 255);
            }
            else if (sender == btnContrast)
            {
                controlPoints[0] = new PointF(0, 0);
                controlPoints[1] = new PointF(100, 25);
                controlPoints[2] = new PointF(155, 230);
                controlPoints[3] = new PointF(255, 255);
            }
            else if (sender == btnInvert)
            {
                controlPoints[0] = new PointF(0, 255);
                controlPoints[1] = new PointF(85, 170);
                controlPoints[2] = new PointF(170, 85);
                controlPoints[3] = new PointF(255, 0);
            }

            RenderBezierGraph();
            if (currentBmp != null)
            {
                ApplyBezierBrightness();
            }
        }

        // -------------------------------------------------------------
        // Noise Generation
        // -------------------------------------------------------------

        private unsafe Bitmap AddSaltAndPepperNoise(Bitmap src, double noisePercent)
        {
            int w = src.Width;
            int h = src.Height;
            Bitmap dst = new Bitmap(src);

            BitmapData data = dst.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            byte* ptr = (byte*)data.Scan0;
            int stride = data.Stride;

            int totalPixels = w * h;
            int noisyCount = (int)(totalPixels * (noisePercent / 100.0));

            for (int i = 0; i < noisyCount; i++)
            {
                int x = random.Next(w);
                int y = random.Next(h);
                byte noiseColor = (random.Next(2) == 0) ? (byte)0 : (byte)255;

                byte* row = ptr + (y * stride);
                row[x * 3] = noiseColor;
                row[x * 3 + 1] = noiseColor;
                row[x * 3 + 2] = noiseColor;
            }

            dst.UnlockBits(data);
            return dst;
        }

        private unsafe Bitmap AddGaussianNoise(Bitmap src, double stdDev = 25.0)
        {
            int w = src.Width;
            int h = src.Height;
            Bitmap dst = new Bitmap(src);

            BitmapData data = dst.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            byte* ptr = (byte*)data.Scan0;
            int stride = data.Stride;

            for (int y = 0; y < h; y++)
            {
                byte* row = ptr + (y * stride);
                for (int x = 0; x < w; x++)
                {
                    double u1 = 1.0 - random.NextDouble();
                    double u2 = 1.0 - random.NextDouble();
                    double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
                    int noise = (int)(stdDev * randStdNormal);

                    int b = Math.Clamp(row[x * 3] + noise, 0, 255);
                    int g = Math.Clamp(row[x * 3 + 1] + noise, 0, 255);
                    int r = Math.Clamp(row[x * 3 + 2] + noise, 0, 255);

                    row[x * 3] = (byte)b;
                    row[x * 3 + 1] = (byte)g;
                    row[x * 3 + 2] = (byte)r;
                }
            }

            dst.UnlockBits(data);
            return dst;
        }

        private void btnAddNoise_Click(object sender, EventArgs e)
        {
            if (currentBmp == null)
            {
                MessageBox.Show("Будь ласка, завантажте BMP зображення.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            double percent = (double)numNoisePercent.Value;
            currentBmp = AddSaltAndPepperNoise(currentBmp, percent);
            pbOriginal.Image = currentBmp;
            statusLabel.Text = $"Додано імпульсний шум: {percent}% пікселів.";
        }

        private void btnAddGaussNoise_Click(object sender, EventArgs e)
        {
            if (currentBmp == null)
            {
                MessageBox.Show("Будь ласка, завантажте BMP зображення.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            currentBmp = AddGaussianNoise(currentBmp, 25.0);
            pbOriginal.Image = currentBmp;
            statusLabel.Text = "Додано Гаусів шум (σ=25).";
        }

        // -------------------------------------------------------------
        // Image Loading & Saving
        // -------------------------------------------------------------

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog
            {
                Title = "Виберіть зображення",
                Filter = "BMP зображення (*.bmp)|*.bmp|Всі зображення (*.bmp;*.png;*.jpg;*.jpeg)|*.bmp;*.png;*.jpg;*.jpeg|Всі файли (*.*)|*.*",
                DefaultExt = "bmp"
            })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var temp = new Bitmap(ofd.FileName))
                        {
                            originalBmp = new Bitmap(temp.Width, temp.Height, PixelFormat.Format24bppRgb);
                            using (Graphics g = Graphics.FromImage(originalBmp))
                            {
                                g.DrawImage(temp, 0, 0, temp.Width, temp.Height);
                            }
                        }

                        currentBmp = new Bitmap(originalBmp);
                        resultBmp = new Bitmap(originalBmp);

                        pbOriginal.Image = currentBmp;
                        pbResult.Image = resultBmp;

                        statusLabel.Text = $"Завантажено: {ofd.SafeFileName} ({currentBmp.Width}x{currentBmp.Height} px)";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Помилка завантаження зображення:\n{ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnSaveResult_Click(object sender, EventArgs e)
        {
            if (resultBmp == null)
            {
                MessageBox.Show("Немає результуючого зображення для збереження.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog
            {
                Title = "Зберегти результуюче зображення",
                Filter = "BMP зображення (*.bmp)|*.bmp|PNG (*.png)|*.png|JPEG (*.jpg)|*.jpg",
                DefaultExt = "bmp",
                FileName = "Result.bmp"
            })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ImageFormat format = ImageFormat.Bmp;
                    string ext = System.IO.Path.GetExtension(sfd.FileName).ToLower();
                    if (ext == ".png") format = ImageFormat.Png;
                    else if (ext == ".jpg" || ext == ".jpeg") format = ImageFormat.Jpeg;

                    resultBmp.Save(sfd.FileName, format);
                    statusLabel.Text = $"Результат успішно збережено у {System.IO.Path.GetFileName(sfd.FileName)}";
                }
            }
        }

        private void btnResetImage_Click(object sender, EventArgs e)
        {
            if (originalBmp == null) return;

            currentBmp = new Bitmap(originalBmp);
            resultBmp = new Bitmap(originalBmp);

            pbOriginal.Image = currentBmp;
            pbResult.Image = resultBmp;

            statusLabel.Text = "Зображення скинуто до вихідного стану.";
        }

        private void btnUseResultAsSource_Click(object sender, EventArgs e)
        {
            if (resultBmp == null) return;

            currentBmp = new Bitmap(resultBmp);
            pbOriginal.Image = currentBmp;

            statusLabel.Text = "Результуюче зображення встановлено як нове вхідне джерело.";
        }
    }
}
