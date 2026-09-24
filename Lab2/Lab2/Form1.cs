using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Lab2
{
    public partial class Form1 : Form
    {
        // Original loaded images and the final resulting image
        private Bitmap sourceBmpOrig;
        private Bitmap targetBmpOrig;
        private Bitmap resultBmpOrig;

        // Image data converted to the currently selected color space
        private Vector3[] sourceSpace;
        private Vector3[] targetSpace;
        private Vector3[] resultSpace;
        
        // Mask for regional color correction (true = pixel is selected by brush)
        private bool[] targetMask;
        private bool hasMask = false;
        // A simple 3D vector structure to store the three channels of any color space.
        struct Vector3 { public double X, Y, Z; }

        public Form1()
        {
            InitializeComponent();
        }
        // Handles loading the source image.
        private void btnLoadSource_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog { Filter = "Image Files|*.bmp;*.jpg;*.png" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    sourceBmpOrig = new Bitmap(ofd.FileName);
                    // Convert the newly loaded image to the active color space
                    sourceSpace = BitmapToSpace(sourceBmpOrig, cmbColorSpace.SelectedIndex);
                    UpdateDisplays();
                }
            }
        }
        // Handles loading the target image.
        private void btnLoadTarget_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog { Filter = "Image Files|*.bmp;*.jpg;*.png" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    targetBmpOrig = new Bitmap(ofd.FileName);
                    // Convert the target image to the active color space
                    targetSpace = BitmapToSpace(targetBmpOrig, cmbColorSpace.SelectedIndex);
                    
                    // Initialize a new blank mask for the regional correction tool
                    targetMask = new bool[targetBmpOrig.Width * targetBmpOrig.Height];
                    hasMask = false;
                    
                    UpdateDisplays();
                }
            }
        }
        // Triggered when the user selects a different color space from the dropdown.
        // Converts existing images to the newly selected space.
        private void cmbColorSpace_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sourceBmpOrig != null) sourceSpace = BitmapToSpace(sourceBmpOrig, cmbColorSpace.SelectedIndex);
            if (targetBmpOrig != null) targetSpace = BitmapToSpace(targetBmpOrig, cmbColorSpace.SelectedIndex);
            
            // Re-apply transfer automatically if we already have a result
            if (resultBmpOrig != null) PerformTransfer();
            else UpdateDisplays();
        }
        // Triggered when the user switches the channel view (e.g., All, Channel 1, Channel 2).
        private void cmbChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDisplays();
        }
        // Validates inputs and initiates the statistical color transfer process.
        private void btnTransfer_Click(object sender, EventArgs e)
        {
            if (sourceBmpOrig == null || targetBmpOrig == null)
            {
                MessageBox.Show("Please load both Source and Target images first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            PerformTransfer();
        }
        // Clears any user-drawn brush mask, returning the tool to full-image processing mode.
        private void btnClearMask_Click(object sender, EventArgs e)
        {
            if (targetMask != null) Array.Clear(targetMask, 0, targetMask.Length);
            hasMask = false;
            UpdateDisplays();
        }
        // Prompts the user to save the resulting color-corrected image to disk as a BMP file.
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (resultBmpOrig == null)
            {
                MessageBox.Show("No result image to save.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (SaveFileDialog sfd = new SaveFileDialog { Filter = "BMP Image|*.bmp" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    resultBmpOrig.Save(sfd.FileName, ImageFormat.Bmp);
                    MessageBox.Show("Result saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        // Translates a mouse coordinate from the PictureBox control into the underlying Bitmap's pixel coordinates.
        // This accounts for the "Zoom" SizeMode used by the PictureBoxes.
        private Point GetImagePoint(PictureBox pb, Point p)
        {
            if (pb.Image == null) return new Point(-1, -1);
            
            int imgW = pb.Image.Width;
            int imgH = pb.Image.Height;
            int pbW = pb.Width;
            int pbH = pb.Height;

            float ratioImg = (float)imgW / imgH;
            float ratioPb = (float)pbW / pbH;
            
            int drawW = pbW, drawH = pbH;
            int offX = 0, offY = 0;

            // Calculate the padding offsets applied by the Zoom mode
            if (ratioPb > ratioImg)
            {
                drawW = (int)(pbH * ratioImg);
                offX = (pbW - drawW) / 2;
            }
            else
            {
                drawH = (int)(pbW / ratioImg);
                offY = (pbH - drawH) / 2;
            }

            // Return -1,-1 if the mouse is outside the actual image area
            if (p.X < offX || p.X >= offX + drawW || p.Y < offY || p.Y >= offY + drawH) return new Point(-1, -1);

            int imgX = (int)((p.X - offX) / (float)drawW * imgW);
            int imgY = (int)((p.Y - offY) / (float)drawH * imgH);
            return new Point(imgX, imgY);
        }

        // Mouse events for drawing the brush mask on the target image
        private void pbTarget_MouseDown(object sender, MouseEventArgs e) { ApplyBrush(e); }
        private void pbTarget_MouseMove(object sender, MouseEventArgs e) { if (e.Button == MouseButtons.Left) ApplyBrush(e); }
        // Modifies the region mask array based on the user's brush strokes.
        private void ApplyBrush(MouseEventArgs e)
        {
            if (targetBmpOrig == null || targetMask == null) return;
            int brushType = cmbBrushType.SelectedIndex;
            if (brushType <= 0) return; // None selected

            Point p = GetImagePoint(pbTarget, e.Location);
            if (p.X < 0 || p.Y < 0) return;

            int radius = (int)numBrushSize.Value;
            int w = targetBmpOrig.Width;
            int h = targetBmpOrig.Height;

            bool changed = false;

            // Loop through a bounding box around the cursor
            for (int y = Math.Max(0, p.Y - radius); y <= Math.Min(h - 1, p.Y + radius); y++)
            {
                for (int x = Math.Max(0, p.X - radius); x <= Math.Min(w - 1, p.X + radius); x++)
                {
                    bool inside = false;
                    if (brushType == 1) // Rectangle Brush
                    {
                        inside = true;
                    }
                    else if (brushType == 2) // Circle Brush
                    {
                        // Check if distance from center is within radius squared
                        if ((x - p.X) * (x - p.X) + (y - p.Y) * (y - p.Y) <= radius * radius)
                            inside = true;
                    }

                    if (inside)
                    {
                        int idx = y * w + x;
                        if (!targetMask[idx]) // Avoid redundant updates
                        {
                            targetMask[idx] = true;
                            hasMask = true;
                            changed = true;
                        }
                    }
                }
            }

            // Only redraw if the mask was actually modified
            if (changed) UpdateDisplays();
        }
        // Performs statistical color correction based on mathematical expectation (E) and standard deviation (D).
        // Replaces the target image's color characteristics with the source image's characteristics.
        private void PerformTransfer()
        {
            int spaceIndex = cmbColorSpace.SelectedIndex;
            int nS = sourceSpace.Length;
            int nT = targetSpace.Length;
            resultSpace = new Vector3[nT];

            // Which channels to process based on UI checkboxes
            bool doCh1 = chkApplyCh1.Checked;
            bool doCh2 = chkApplyCh2.Checked;
            bool doCh3 = chkApplyCh3.Checked;

            // Calculate the number of pixels actively targeted (either full image or just the mask)
            int maskCount = 0;
            for (int i = 0; i < nT; i++) if (!hasMask || targetMask[i]) maskCount++;
            if (maskCount == 0) maskCount = 1; // Prevent division by zero

            // Step 1: Calculate Mathematical Expectation (mean) for Source Image
            double Es_X = 0, Es_Y = 0, Es_Z = 0;
            for (int i = 0; i < nS; i++)
            {
                Es_X += sourceSpace[i].X;
                Es_Y += sourceSpace[i].Y;
                Es_Z += sourceSpace[i].Z;
            }
            Es_X /= nS; Es_Y /= nS; Es_Z /= nS;

            // Step 2: Calculate Mathematical Expectation (mean) for Target Image (only masked region)
            double Et_X = 0, Et_Y = 0, Et_Z = 0;
            for (int i = 0; i < nT; i++)
            {
                if (!hasMask || targetMask[i])
                {
                    Et_X += targetSpace[i].X;
                    Et_Y += targetSpace[i].Y;
                    Et_Z += targetSpace[i].Z;
                }
            }
            Et_X /= maskCount; Et_Y /= maskCount; Et_Z /= maskCount;

            // Step 3: Calculate Standard Deviation (variance) for Source Image
            double Ds_X = 0, Ds_Y = 0, Ds_Z = 0;
            for (int i = 0; i < nS; i++)
            {
                Ds_X += (sourceSpace[i].X - Es_X) * (sourceSpace[i].X - Es_X);
                Ds_Y += (sourceSpace[i].Y - Es_Y) * (sourceSpace[i].Y - Es_Y);
                Ds_Z += (sourceSpace[i].Z - Es_Z) * (sourceSpace[i].Z - Es_Z);
            }
            Ds_X = Math.Sqrt(Ds_X / nS); Ds_Y = Math.Sqrt(Ds_Y / nS); Ds_Z = Math.Sqrt(Ds_Z / nS);

            // Step 4: Calculate Standard Deviation (variance) for Target Image (only masked region)
            double Dt_X = 0, Dt_Y = 0, Dt_Z = 0;
            for (int i = 0; i < nT; i++)
            {
                if (!hasMask || targetMask[i])
                {
                    Dt_X += (targetSpace[i].X - Et_X) * (targetSpace[i].X - Et_X);
                    Dt_Y += (targetSpace[i].Y - Et_Y) * (targetSpace[i].Y - Et_Y);
                    Dt_Z += (targetSpace[i].Z - Et_Z) * (targetSpace[i].Z - Et_Z);
                }
            }
            Dt_X = Math.Sqrt(Dt_X / maskCount); Dt_Y = Math.Sqrt(Dt_Y / maskCount); Dt_Z = Math.Sqrt(Dt_Z / maskCount);

            // Safeguard against division by zero in flat color areas
            if (Dt_X == 0) Dt_X = 1;
            if (Dt_Y == 0) Dt_Y = 1;
            if (Dt_Z == 0) Dt_Z = 1;

            // Step 5: Apply the linear transformation to shift color statistics
            // Formula: Result = E_source + (Target - E_target) * (D_source / D_target)
            for (int i = 0; i < nT; i++)
            {
                if (!hasMask || targetMask[i]) // Process only if part of the mask (or full image if no mask)
                {
                    resultSpace[i].X = doCh1 ? (Es_X + (targetSpace[i].X - Et_X) * (Ds_X / Dt_X)) : targetSpace[i].X;
                    resultSpace[i].Y = doCh2 ? (Es_Y + (targetSpace[i].Y - Et_Y) * (Ds_Y / Dt_Y)) : targetSpace[i].Y;
                    resultSpace[i].Z = doCh3 ? (Es_Z + (targetSpace[i].Z - Et_Z) * (Ds_Z / Dt_Z)) : targetSpace[i].Z;
                }
                else
                {
                    resultSpace[i] = targetSpace[i]; // Ignore unmasked pixels
                }
            }

            // Finally, reconstruct the result Bitmap from the processed space data
            resultBmpOrig = SpaceToBitmap(resultSpace, targetBmpOrig.Width, targetBmpOrig.Height, spaceIndex);
            UpdateDisplays();
        }
        // Updates all UI picture boxes to show the current images and histograms.
        // Handles switching between rendering Full RGB or rendering single grayscale channels.
        private void UpdateDisplays()
        {
            int channelIndex = cmbChannel.SelectedIndex;

            if (sourceBmpOrig != null)
            {
                Bitmap disp = GetDisplayBitmap(sourceBmpOrig, sourceSpace, channelIndex, null);
                pbSource.Image = disp;
                pbHistSource.Image = GetHistogram(disp, channelIndex);
            }
            if (targetBmpOrig != null)
            {
                Bitmap disp = GetDisplayBitmap(targetBmpOrig, targetSpace, channelIndex, targetMask);
                pbTarget.Image = disp;
                pbHistTarget.Image = GetHistogram(disp, channelIndex);
            }
            if (resultBmpOrig != null && resultSpace != null)
            {
                Bitmap disp = GetDisplayBitmap(resultBmpOrig, resultSpace, channelIndex, null);
                pbResult.Image = disp;
                pbHistResult.Image = GetHistogram(disp, channelIndex);
            }
        }
        // High-performance conversion from a raw RGB Bitmap into a 1D array of Vector3 structures
        // representing the currently selected color space (Lab, HSL, HSV, or YIQ).
        // Uses unmanaged pointers via LockBits to drastically improve processing speed.
        private unsafe Vector3[] BitmapToSpace(Bitmap bmp, int spaceIndex)
        {
            int w = bmp.Width;
            int h = bmp.Height;
            Vector3[] result = new Vector3[w * h];
            
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            byte* ptr = (byte*)data.Scan0;
            int stride = data.Stride;

            int idx = 0;
            for (int y = 0; y < h; y++)
            {
                byte* row = ptr + (y * stride);
                for (int x = 0; x < w; x++)
                {
                    // Memory format in Format24bppRgb is BGR, not RGB
                    Color c = Color.FromArgb(row[x * 3 + 2], row[x * 3 + 1], row[x * 3]);
                    
                    if (spaceIndex == 0) result[idx] = RGBtoLab(c);
                    else if (spaceIndex == 1) result[idx] = RGBtoHSL(c);
                    else if (spaceIndex == 2) result[idx] = RGBtoHSV(c);
                    else if (spaceIndex == 3) result[idx] = RGBtoYIQ(c);
                    
                    idx++;
                }
            }
            bmp.UnlockBits(data);
            return result;
        }
        // High-performance conversion from the active color space back to a raw RGB Bitmap.
        // Preserves the exact original pixels for unmasked regions to prevent rounding errors.
        private unsafe Bitmap SpaceToBitmap(Vector3[] space, int w, int h, int spaceIndex)
        {
            Bitmap bmp = new Bitmap(w, h, PixelFormat.Format24bppRgb);
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            
            // Lock the original target bitmap to copy unmasked pixels directly
            BitmapData origData = null;
            if (targetBmpOrig != null) origData = targetBmpOrig.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            
            byte* ptr = (byte*)data.Scan0;
            byte* ptrOrig = (origData != null) ? (byte*)origData.Scan0 : null;
            int stride = data.Stride;

            int idx = 0;
            for (int y = 0; y < h; y++)
            {
                byte* row = ptr + (y * stride);
                byte* rowOrig = (ptrOrig != null) ? ptrOrig + (y * stride) : null;
                for (int x = 0; x < w; x++)
                {
                    // For unmasked pixels, bypass space conversion completely to guarantee 100% losslessness
                    if (hasMask && targetMask != null && !targetMask[idx] && rowOrig != null)
                    {
                        row[x * 3] = rowOrig[x * 3];
                        row[x * 3 + 1] = rowOrig[x * 3 + 1];
                        row[x * 3 + 2] = rowOrig[x * 3 + 2];
                    }
                    else // Convert color space coordinates back to RGB
                    {
                        Color c = Color.Black;
                        if (spaceIndex == 0) c = LabtoRGB(space[idx]);
                        else if (spaceIndex == 1) c = HSLtoRGB(space[idx]);
                        else if (spaceIndex == 2) c = HSVtoRGB(space[idx]);
                        else if (spaceIndex == 3) c = YIQtoRGB(space[idx]);
                        
                        row[x * 3 + 2] = c.R; // R
                        row[x * 3 + 1] = c.G; // G
                        row[x * 3] = c.B;     // B
                    }
                    idx++;
                }
            }
            
            bmp.UnlockBits(data);
            if (targetBmpOrig != null && origData != null) targetBmpOrig.UnlockBits(origData);
            return bmp;
        }
        // Renders an image for the UI display. Responsible for generating single-channel 
        // grayscale representations (when requested) and drawing the reddish mask overlay.
        private unsafe Bitmap GetDisplayBitmap(Bitmap orig, Vector3[] space, int channel, bool[] mask)
        {
            if (orig == null) return null;

            int w = orig.Width;
            int h = orig.Height;
            Bitmap bmp = new Bitmap(w, h, PixelFormat.Format24bppRgb);

            // Calculate a *global* minimum and maximum value across source, target, and result images
            // to ensure that channel-wise grayscale visualizations all share the exact same scale.
            double minC = double.MaxValue;
            double maxC = double.MinValue;
            
            if (channel > 0)
            {
                var allSpaces = new[] { sourceSpace, targetSpace, resultSpace };
                foreach (var s in allSpaces)
                {
                    if (s == null) continue;
                    for (int i = 0; i < s.Length; i++)
                    {
                        double val = (channel == 1) ? s[i].X : (channel == 2) ? s[i].Y : s[i].Z;
                        if (val < minC) minC = val;
                        if (val > maxC) maxC = val;
                    }
                }
                if (maxC == minC) maxC = minC + 1; // Prevent division by zero
            }

            BitmapData origData = null;
            if (channel == 0) origData = orig.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            
            byte* ptrOut = (byte*)data.Scan0;
            byte* ptrIn = (channel == 0) ? (byte*)origData.Scan0 : null;
            int strideOut = data.Stride;
            int strideIn = (channel == 0) ? origData.Stride : 0;

            int idx = 0;
            for (int y = 0; y < h; y++)
            {
                byte* rowOut = ptrOut + (y * strideOut);
                byte* rowIn = (channel == 0) ? ptrIn + (y * strideIn) : null;
                for (int x = 0; x < w; x++)
                {
                    byte r, g, b;
                    
                    if (channel == 0) // Full RGB rendering mode
                    {
                        b = rowIn[x * 3];
                        g = rowIn[x * 3 + 1];
                        r = rowIn[x * 3 + 2];
                    }
                    else // Grayscale single-channel rendering mode
                    {
                        double val = (channel == 1) ? space[idx].X : (channel == 2) ? space[idx].Y : space[idx].Z;
                        byte gray = (byte)Math.Max(0, Math.Min(255, (val - minC) / (maxC - minC) * 255));
                        r = g = b = gray;
                    }

                    // Render a faint red overlay where the user has painted the mask
                    if (mask != null && mask[idx])
                    {
                        r = (byte)Math.Min(255, r + 50);
                        g = (byte)Math.Max(0, g - 20);
                        b = (byte)Math.Max(0, b - 20);
                    }

                    rowOut[x * 3] = b;
                    rowOut[x * 3 + 1] = g;
                    rowOut[x * 3 + 2] = r;
                    idx++;
                }
            }
            
            bmp.UnlockBits(data);
            if (channel == 0) orig.UnlockBits(origData);
            
            return bmp;
        }
        // Generates a simple pixel-count histogram. Renders three combined histograms
        // for RGB, or a single black histogram when viewing a single channel.
        private unsafe Bitmap GetHistogram(Bitmap bmp, int channel)
        {
            int[] countsR = new int[256];
            int[] countsG = new int[256];
            int[] countsB = new int[256];
            int[] countsGray = new int[256];

            int w = bmp.Width;
            int h = bmp.Height;
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            byte* ptr = (byte*)data.Scan0;
            int stride = data.Stride;

            // Compute bucket counts
            int maxCount = 0;
            for (int y = 0; y < h; y++)
            {
                byte* row = ptr + (y * stride);
                for (int x = 0; x < w; x++)
                {
                    byte b = row[x * 3];
                    byte g = row[x * 3 + 1];
                    byte r = row[x * 3 + 2];

                    if (channel == 0)
                    {
                        countsB[b]++; countsG[g]++; countsR[r]++;
                        if (countsB[b] > maxCount) maxCount = countsB[b];
                        if (countsG[g] > maxCount) maxCount = countsG[g];
                        if (countsR[r] > maxCount) maxCount = countsR[r];
                    }
                    else
                    {
                        countsGray[r]++;
                        if (countsGray[r] > maxCount) maxCount = countsGray[r];
                    }
                }
            }
            bmp.UnlockBits(data);
            if (maxCount == 0) maxCount = 1; // Prevent division by zero

            // Render the graph vertically
            int histW = 256;
            int histH = 100;
            Bitmap histBmp = new Bitmap(histW, histH);
            using (Graphics graphics = Graphics.FromImage(histBmp))
            {
                graphics.Clear(Color.White);
                if (channel == 0)
                {
                    for (int i = 0; i < 256; i++)
                    {
                        int hr = (int)((double)countsR[i] / maxCount * histH);
                        int hg = (int)((double)countsG[i] / maxCount * histH);
                        int hb = (int)((double)countsB[i] / maxCount * histH);
                        if (hr > 0) graphics.DrawLine(Pens.Red, i, histH, i, histH - hr);
                        if (hg > 0) graphics.DrawLine(Pens.Green, i, histH, i, histH - hg);
                        if (hb > 0) graphics.DrawLine(Pens.Blue, i, histH, i, histH - hb);
                    }
                }
                else
                {
                    for (int i = 0; i < 256; i++)
                    {
                        int hgray = (int)((double)countsGray[i] / maxCount * histH);
                        if (hgray > 0) graphics.DrawLine(Pens.Black, i, histH, i, histH - hgray);
                    }
                }
            }
            return histBmp;
        }

        #region Color Space Conversions
        // Converts RGB to CIELAB space via LMS intermediary.
        private Vector3 RGBtoLab(Color c)
        {
            double R = (c.R / 255.0) * 0.92157;
            double G = (c.G / 255.0) * 0.92157;
            double B = (c.B / 255.0) * 0.92157;

            // RGB to LMS
            double L_lms = 0.3811 * R + 0.5783 * G + 0.0402 * B;
            double M_lms = 0.1967 * R + 0.7244 * G + 0.0782 * B;
            double S_lms = 0.0241 * R + 0.1288 * G + 0.8444 * B;

            // Transform
            L_lms = Math.Log10(Math.Max(L_lms, 0.01176));
            M_lms = Math.Log10(Math.Max(M_lms, 0.01176));
            S_lms = Math.Log10(Math.Max(S_lms, 0.01176));

            // LMS to Lab
            double l = 0.5774 * L_lms + 0.5774 * M_lms + 0.5774 * S_lms;
            double a = 0.4082 * L_lms + 0.4082 * M_lms - 0.8164 * S_lms;
            double b = 0.7071 * L_lms - 0.7071 * M_lms;

            return new Vector3 { X = l, Y = a, Z = b };
        }
        // Converts CIELAB space back to RGB via LMS intermediary.
        private Color LabtoRGB(Vector3 lab)
        {
            double l = lab.X, a = lab.Y, b = lab.Z;

            // Lab to LMS
            double L_lms = 0.5774 * l + 0.4082 * a + 0.7071 * b;
            double M_lms = 0.5774 * l + 0.4082 * a - 0.7071 * b;
            double S_lms = 0.5774 * l - 0.8164 * a;

            // Transform
            L_lms = Math.Pow(10, L_lms);
            M_lms = Math.Pow(10, M_lms);
            S_lms = Math.Pow(10, S_lms);

            // LMS to RGB
            double R = 4.4679 * L_lms - 3.5873 * M_lms + 0.1193 * S_lms;
            double G = -1.2186 * L_lms + 2.3809 * M_lms - 0.1624 * S_lms;
            double B = 0.0497 * L_lms - 0.2439 * M_lms + 1.2045 * S_lms;

            R /= 0.92157;
            G /= 0.92157;
            B /= 0.92157;

            // Clamp and return
            return Color.FromArgb(
                (int)Math.Max(0, Math.Min(255, R * 255)),
                (int)Math.Max(0, Math.Min(255, G * 255)),
                (int)Math.Max(0, Math.Min(255, B * 255))
            );
        }
        // Converts standard RGB to HSL (Hue, Saturation, Lightness).
        private Vector3 RGBtoHSL(Color c1)
        {
            double r = c1.R / 255.0, g = c1.G / 255.0, b = c1.B / 255.0;
            double min = Math.Min(r, Math.Min(g, b));
            double max = Math.Max(r, Math.Max(g, b));
            double delta = max - min;
            
            double l = (min + max) / 2;
            double s = 0;
            double h = 0;

            if (l > 0 && l < 1)
            {
                double div = (l < 0.5) ? (2 * l) : (2 - 2 * l);
                if (div > 0) s = delta / div;
            }

            if (delta > 0)
            {
                if (max == r && max != g) h += (g - b) / delta;
                else if (max == g && max != b) h += 2 + (b - r) / delta;
                else if (max == b && max != r) h += 4 + (r - g) / delta;
                h *= 60;
            }
            if (h < 0) h += 360; // Normalize Hue to 0-360 degrees
            
            return new Vector3 { X = h, Y = s, Z = l };
        }
        // Converts HSL (Hue, Saturation, Lightness) back to standard RGB.
        private Color HSLtoRGB(Vector3 hsl)
        {
            double h = hsl.X, s = hsl.Y, l = hsl.Z;
            s = Math.Max(0, Math.Min(1, s));
            l = Math.Max(0, Math.Min(1, l));
            while (h < 0) h += 360;
            while (h >= 360) h -= 360;

            double sat_r = 0, sat_g = 0, sat_b = 0;
            
            // Map hue to primary color contributions
            if (h < 120) { sat_r = (120 - h) / 60.0; sat_g = h / 60.0; }
            else if (h < 240) { sat_g = (240 - h) / 60.0; sat_b = (h - 120) / 60.0; }
            else { sat_r = (h - 240) / 60.0; sat_b = (360 - h) / 60.0; }

            sat_r = Math.Min(Math.Max(0, sat_r), 1);
            sat_g = Math.Min(Math.Max(0, sat_g), 1);
            sat_b = Math.Min(Math.Max(0, sat_b), 1);

            double ctmp_r = 2 * s * sat_r + (1 - s);
            double ctmp_g = 2 * s * sat_g + (1 - s);
            double ctmp_b = 2 * s * sat_b + (1 - s);

            double r, g, b;
            
            // Adjust based on lightness
            if (l < 0.5)
            {
                r = l * ctmp_r; g = l * ctmp_g; b = l * ctmp_b;
            }
            else
            {
                r = (1 - l) * ctmp_r + 2 * l - 1;
                g = (1 - l) * ctmp_g + 2 * l - 1;
                b = (1 - l) * ctmp_b + 2 * l - 1;
            }
            
            return Color.FromArgb(
                (int)(Math.Max(0, Math.Min(1, r)) * 255), 
                (int)(Math.Max(0, Math.Min(1, g)) * 255), 
                (int)(Math.Max(0, Math.Min(1, b)) * 255));
        }
        // Converts standard RGB to HSV (Hue, Saturation, Value).
        private Vector3 RGBtoHSV(Color c1)
        {
            double r = c1.R / 255.0, g = c1.G / 255.0, b = c1.B / 255.0;
            double min = Math.Min(r, Math.Min(g, b));
            double max = Math.Max(r, Math.Max(g, b));
            double delta = max - min;
            
            double v = max;
            double s = (max > 0) ? delta / max : 0;
            double h = 0;
            
            if (delta > 0)
            {
                if (max == r && max != g) h += (g - b) / delta;
                else if (max == g && max != b) h += 2 + (b - r) / delta;
                else if (max == b && max != r) h += 4 + (r - g) / delta;
                h *= 60;
            }
            if (h < 0) h += 360;
            
            return new Vector3 { X = h, Y = s, Z = v };
        }
        // Converts HSV (Hue, Saturation, Value) back to standard RGB.
        private Color HSVtoRGB(Vector3 hsv)
        {
            double h = hsv.X, s = hsv.Y, v = hsv.Z;
            s = Math.Max(0, Math.Min(1, s));
            v = Math.Max(0, Math.Min(1, v));
            while (h < 0) h += 360;
            while (h >= 360) h -= 360;

            double sat_r = 0, sat_g = 0, sat_b = 0;
            
            if (h < 120) { sat_r = (120 - h) / 60.0; sat_g = h / 60.0; }
            else if (h < 240) { sat_g = (240 - h) / 60.0; sat_b = (h - 120) / 60.0; }
            else { sat_r = (h - 240) / 60.0; sat_b = (360 - h) / 60.0; }

            sat_r = Math.Min(Math.Max(0, sat_r), 1);
            sat_g = Math.Min(Math.Max(0, sat_g), 1);
            sat_b = Math.Min(Math.Max(0, sat_b), 1);

            double r = (1 - s + s * sat_r) * v;
            double g = (1 - s + s * sat_g) * v;
            double b = (1 - s + s * sat_b) * v;
            
            return Color.FromArgb(
                (int)(Math.Max(0, Math.Min(1, r)) * 255), 
                (int)(Math.Max(0, Math.Min(1, g)) * 255), 
                (int)(Math.Max(0, Math.Min(1, b)) * 255));
        }
        // Converts standard RGB to NTSC YIQ color space (Luma, In-phase, Quadrature).
        private Vector3 RGBtoYIQ(Color c)
        {
            double r = c.R / 255.0, g = c.G / 255.0, b = c.B / 255.0;
            double y = 0.299 * r + 0.587 * g + 0.114 * b;
            double i = 0.596 * r - 0.275 * g - 0.321 * b;
            double q = 0.212 * r - 0.528 * g + 0.311 * b;
            return new Vector3 { X = y, Y = i, Z = q };
        }
        // Converts NTSC YIQ color space back to standard RGB.
        // Note: The +1.703 term in the Blue channel is required for accurate mathematical inversion.
        private Color YIQtoRGB(Vector3 yiq)
        {
            double y = yiq.X, i = yiq.Y, q = yiq.Z;
            double r = y + 0.956 * i + 0.621 * q;
            double g = y - 0.272 * i - 0.647 * q;
            double b = y - 1.106 * i + 1.703 * q;
            
            return Color.FromArgb(
                (int)Math.Max(0, Math.Min(255, r * 255)),
                (int)Math.Max(0, Math.Min(255, g * 255)),
                (int)Math.Max(0, Math.Min(255, b * 255))
            );
        }

        #endregion
    }
}
