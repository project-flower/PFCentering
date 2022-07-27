using System;
using System.Drawing;
using System.Windows.Forms;

namespace PFCentering
{
    public partial class FormPreview : Form
    {
        #region Private Fields

        private Image baseImage = null;
        private Color previewFrameColor;
        private Color previewShadowColor;
        private int prevX = int.MinValue;
        private int prevY = int.MinValue;
        private int windowIndex = -1;
        private (IntPtr, Rectangle)[] windows = new (IntPtr, Rectangle)[0];

        #endregion

        #region Public Methods

        public FormPreview()
        {
            InitializeComponent();

#if DEBUG
            TopMost = false;
#endif
        }

        public void DoCentering()
        {
            if (windowIndex < 0)
            {
                return;
            }

            WindowManager.DoCentering(windows[windowIndex].Item1);
        }

        public void Initialize(Color previewShadowColor, byte previewShadowAlpha, Color previewFrameColor)
        {
            this.previewFrameColor = previewFrameColor;
            this.previewShadowColor = Color.FromArgb(previewShadowAlpha, previewShadowColor);
        }

        public void ShowPreview()
        {
            windows = CaptureEngine.CollectWindows(out Bitmap bitmap, out Rectangle totalScreenSize);
            windowIndex = -1;
            Image image = pictureBox.Image;
            pictureBox.Image = bitmap;
            baseImage = bitmap.Clone() as Image;

            if (image != null)
            {
                image.Dispose();
            }

            if (!Visible)
            {
                Show();
            }

            Left = totalScreenSize.Left;
            Top = totalScreenSize.Top;
            Width = totalScreenSize.Width;
            Height = totalScreenSize.Height;
        }

        #endregion

        #region Private Methods

        private void EmphasizeRectangle(Rectangle rectangle)
        {
            if (baseImage == null)
            {
                return;
            }

            pictureBox.Image.Dispose();
            Bitmap bitmap = baseImage.Clone() as Bitmap;

            if (!rectangle.IsEmpty)
            {
                DrawingEngine.FillHollowRectangle(bitmap, rectangle, previewShadowColor, previewFrameColor);
            }

            pictureBox.Image = bitmap;
        }

        private Rectangle PointToClient(Rectangle rectangle)
        {
            return new Rectangle(pictureBox.PointToClient(rectangle.Location), rectangle.Size);
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(this, message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private bool SmallerThan(Rectangle rectangle1, Rectangle rectangle2)
        {
            return (rectangle1.Width * rectangle1.Height) < (rectangle2.Width * rectangle2.Height);
        }

        #endregion

        // Designer's Methods

        private void formClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void keyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                windowIndex = -1;
                Close();
            }
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                DoCentering();
            }
            catch (Exception exception)
            {
                ShowErrorMessage(exception.Message);
            }

            Close();
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.X == prevX) && (e.Y == prevY))
            {
                return;
            }

            prevX = e.X;
            prevY = e.Y;
            int candidate = -1;

            for (int i = 0; i < windows.Length; ++i)
            {
                Rectangle window = windows[i].Item2;
                Point point = pictureBox.PointToScreen(e.Location);
                int x = point.X;
                int y = point.Y;

                if ((x < window.Left)
                    || (y < window.Top)
                    || (x > window.Right)
                    || (y > window.Bottom)) continue;

                if (candidate >= 0 && !SmallerThan(window, windows[candidate].Item2))
                {
                    continue;
                }

                candidate = i;
            }

            if ((candidate == -1) && (windowIndex != -1))
            {
                EmphasizeRectangle(Rectangle.Empty);
                windowIndex = -1;
            }
            else if ((candidate != -1) && (candidate != windowIndex))
            {
                EmphasizeRectangle(PointToClient(windows[candidate].Item2));
                windowIndex = candidate;
            }
        }
    }
}
