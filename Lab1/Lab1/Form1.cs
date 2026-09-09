using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Form1 : Form
    {
        private const int GridWidth = 200;
        private const int GridHeight = 200;

        private byte[,] grid = new byte[GridWidth, GridHeight];
        private byte[,] nextGrid = new byte[GridWidth, GridHeight];

        private HashSet<int> survive = new HashSet<int>();
        private HashSet<int> birth = new HashSet<int>();
        private int statesCount = 8;

        private int generationCount = 0;
        private Bitmap renderBitmap;

        public Form1()
        {
            InitializeComponent();

            renderBitmap = new Bitmap(GridWidth, GridHeight, PixelFormat.Format24bppRgb);

            // Populate presets
            cbPresets.Items.Clear();
            cbPresets.Items.Add("Insects (23/2/8)");
            cbPresets.Items.Add("Sparks (2/2/25)");
            cbPresets.Items.Add("Flames (235678/3468/9)");
            cbPresets.SelectedIndex = 0;

            ParseRule("23/2/8");

            // Simulation timer setup
            timerSim.Interval = 50;
            timerSim.Tick += (s, e) => MakeStep();

            DrawGrid();
        }

        #region Simulation Logic (Generations)
        private void ParseRule(string ruleStr)
        {
            try
            {
                string[] parts = ruleStr.Replace(" ", "").Split('/');
                if (parts.Length != 3) throw new Exception();

                survive.Clear();
                foreach (char c in parts[0])
                    survive.Add(c - '0');

                birth.Clear();
                foreach (char c in parts[1])
                    birth.Add(c - '0');

                statesCount = int.Parse(parts[2]);
                if (statesCount < 2) statesCount = 2;
            }
            catch
            {
                MessageBox.Show("Invalid rule format! Use S/B/C notation (e.g., 23/2/8)",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MakeStep()
        {
            for (int x = 0; x < GridWidth; x++)
            {
                for (int y = 0; y < GridHeight; y++)
                {
                    byte state = grid[x, y];
                    int liveNeighbors = CountLiveNeighbors(x, y);

                    if (state == 0) // Dead cell
                    {
                        nextGrid[x, y] = (byte)(birth.Contains(liveNeighbors) ? 1 : 0);
                    }
                    else if (state == 1) // Alive cell
                    {
                        nextGrid[x, y] = (byte)(survive.Contains(liveNeighbors) ? 1 : 2);
                    }
                    else // Dying cell (2 .. statesCount-1)
                    {
                        byte nextState = (byte)(state + 1);
                        nextGrid[x, y] = (nextState >= statesCount) ? (byte)0 : nextState;
                    }
                }
            }

            var temp = grid;
            grid = nextGrid;
            nextGrid = temp;

            generationCount++;
            lblGeneration.Text = $"Generation: {generationCount}";

            DrawGrid();
        }

        private int CountLiveNeighbors(int x, int y)
        {
            int count = 0;
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0) continue;

                    int nx = (x + dx + GridWidth) % GridWidth;
                    int ny = (y + dy + GridHeight) % GridHeight;

                    if (grid[nx, ny] == 1)
                        count++;
                }
            }
            return count;
        }
        #endregion

        #region Fast Rendering (LockBits)
        private void DrawGrid()
        {
            BitmapData bmpData = renderBitmap.LockBits(
                new Rectangle(0, 0, GridWidth, GridHeight),
                ImageLockMode.WriteOnly,
                PixelFormat.Format24bppRgb);

            unsafe
            {
                byte* ptr = (byte*)bmpData.Scan0;
                int stride = bmpData.Stride;

                for (int y = 0; y < GridHeight; y++)
                {
                    byte* row = ptr + (y * stride);
                    for (int x = 0; x < GridWidth; x++)
                    {
                        byte state = grid[x, y];
                        Color color = GetColorForState(state);

                        row[x * 3] = color.B;
                        row[x * 3 + 1] = color.G;
                        row[x * 3 + 2] = color.R;
                    }
                }
            }

            renderBitmap.UnlockBits(bmpData);
            pictureBoxGrid.Image = renderBitmap;
        }

        private Color GetColorForState(byte state)
        {
            if (state == 0) return Color.Black;
            if (state == 1) return Color.White;

            float ratio = 1.0f - ((float)(state - 1) / (statesCount - 1));
            int red = (int)(255 * ratio);
            int green = (int)(120 * ratio);
            return Color.FromArgb(red, green, 30);
        }
        #endregion

        #region Mouse Interactive Drawing
        private void PictureBoxGrid_MouseDown(object sender, MouseEventArgs e)
        {
            SetCellByMouse(e);
        }

        private void PictureBoxGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                SetCellByMouse(e);
            }
        }

        private void SetCellByMouse(MouseEventArgs e)
        {
            int cellX = (int)((float)e.X / pictureBoxGrid.Width * GridWidth);
            int cellY = (int)((float)e.Y / pictureBoxGrid.Height * GridHeight);

            if (cellX >= 0 && cellX < GridWidth && cellY >= 0 && cellY < GridHeight)
            {
                grid[cellX, cellY] = (byte)(e.Button == MouseButtons.Left ? 1 : 0);
                DrawGrid();
            }
        }
        #endregion

        #region Controls & Button Handlers
        private void btnStartStop_Click(object sender, EventArgs e)
        {
            timerSim.Enabled = !timerSim.Enabled;
            btnStartStop.Text = timerSim.Enabled ? "Pause" : "Start";
        }

        private void btnStep_Click(object sender, EventArgs e)
        {
            if (!timerSim.Enabled)
                MakeStep();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Array.Clear(grid, 0, grid.Length);
            generationCount = 0;
            lblGeneration.Text = "Generation: 0";
            DrawGrid();
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            for (int x = 0; x < GridWidth; x++)
            {
                for (int y = 0; y < GridHeight; y++)
                {
                    grid[x, y] = (byte)(rnd.Next(100) < 15 ? 1 : 0);
                }
            }
            generationCount = 0;
            lblGeneration.Text = "Generation: 0";
            DrawGrid();
        }

        private void btnApplyRule_Click(object sender, EventArgs e)
        {
            ParseRule(txtRule.Text);
        }

        private void cbPresets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPresets.SelectedIndex == 0) txtRule.Text = "23/2/8";
            else if (cbPresets.SelectedIndex == 1) txtRule.Text = "2/2/25";
            else if (cbPresets.SelectedIndex == 2) txtRule.Text = "235678/3468/9";

            ParseRule(txtRule.Text);
        }

        private void trackSpeed_Scroll(object sender, EventArgs e)
        {
            timerSim.Interval = Math.Max(1, 201 - trackSpeed.Value);
        }

        private void btnSaveBmp_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "BMP Image (*.bmp)|*.bmp";
                sfd.FileName = $"generation_{generationCount}.bmp";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    renderBitmap.Save(sfd.FileName, ImageFormat.Bmp);
                    MessageBox.Show("Image successfully saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        #endregion
    }
}