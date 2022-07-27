using NativeApi;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PFCentering
{
    internal static class WindowManager
    {
        #region Internal Methods

        internal static void DoCentering(IntPtr handle)
        {
            if (!User32.GetWindowRect(handle, out RECT rect))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            int rectLeft = rect.Left;
            int rectTop = rect.Top;
            int rectWidth = (rect.Right - rectLeft);
            int rectHeight = (rect.Bottom - rectTop);
            Rectangle windowRectangle = new Rectangle(rectLeft, rectTop, rectWidth, rectHeight);
            uint area = 0;
            Rectangle screenBounds = Rectangle.Empty;

            foreach (Screen screen in Screen.AllScreens)
            {
                Rectangle bounds = screen.Bounds;
                uint area_ = GetArea(bounds, windowRectangle);

                if (area_ <= area) continue;

                screenBounds = bounds;
                area = area_;
            }

            if (!screenBounds.IsEmpty)
            {
                if (!User32.SetWindowPos(handle, IntPtr.Zero
                    , (screenBounds.Left + screenBounds.Width / 2 - rectWidth / 2)
                    , (screenBounds.Top + screenBounds.Height / 2 - rectHeight / 2)
                    , 0, 0, (SWP.ASYNCWINDOWPOS | SWP.NOSIZE)))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
        }

        #endregion

        #region Private Methods

        private static uint GetArea(Rectangle screen, Rectangle window)
        {
            Rectangle intersect = Rectangle.Intersect(screen, window);

            if (intersect.IsEmpty)
            {
                return 0;
            }

            return ((uint)intersect.Width * (uint)intersect.Height);
        }

        #endregion
    }
}
