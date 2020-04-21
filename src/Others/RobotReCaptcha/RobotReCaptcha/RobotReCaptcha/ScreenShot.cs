using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StructScreenShot
{
    class ScreenShot
    {
        [DllImport("user32.dll", EntryPoint = "GetDC")]
        static extern IntPtr GetDC(IntPtr ptr);
        [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
        static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);
        [DllImport("gdi32.dll", EntryPoint = "DeleteDC")]
        static extern IntPtr DeleteDC(IntPtr hDc);
        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleDC")]
        static extern IntPtr CreateCompatibleDC(IntPtr hdc);
        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleBitmap")]
        static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);
        [DllImport("gdi32.dll", EntryPoint = "SelectObject")]
        static extern IntPtr SelectObject(IntPtr hdc, IntPtr bmp);
        [DllImport("gdi32.dll", EntryPoint = "BitBlt")]
        static extern bool BitBlt(IntPtr hdcDest, int xDest, int yDest, int wDest, int hDest, IntPtr hdcSource, int xSrc, int ySrc, int RasterOp);
        [DllImport("user32.dll", EntryPoint = "GetDesktopWindow")]
        static extern IntPtr GetDesktopWindow();
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        static extern IntPtr DeleteObject(IntPtr hDc);

        const int SRCCOPY = 13369376;
        public struct SIZE
        {
            public int cx;
            public int cy;
        }

        public Bitmap ReturnImagemControl(IntPtr controle, Rectangle area)
        {
            SIZE size;
            IntPtr hBitmap;

            IntPtr hDC = GetDC(controle);
            IntPtr hMemDC = CreateCompatibleDC(hDC);

            size.cx = area.Width - area.Left;
            size.cy = area.Bottom - area.Top;

            hBitmap = CreateCompatibleBitmap(hDC, size.cx, size.cy);

            if (hBitmap != IntPtr.Zero)
            {
                IntPtr hOld = (IntPtr)SelectObject(hMemDC, hBitmap);
                BitBlt(hMemDC, 0, 0, size.cx, size.cy, hDC, 0, 0, SRCCOPY);
                SelectObject(hMemDC, hOld);
                DeleteDC(hMemDC);
                ReleaseDC(GetDesktopWindow(), hDC);
                Bitmap bmp = System.Drawing.Image.FromHbitmap(hBitmap);
                DeleteObject(hBitmap);
                return bmp;
            }
            else
            {
                return null;
            }
        }
    }
}
