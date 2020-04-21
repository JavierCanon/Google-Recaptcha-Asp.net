using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using StructMp3ToWavConvert;
using StructSendPOST;
using StructScreenShot;


namespace StructMouseBase
{
    class Base
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]

        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
        //Mouse actions
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        private const int MOUSEEVENTF_ABSOLUTE = 0x80;

        public void MakeCoordinates(int CoordinateX, int CoordinateY, int sleep, string btnMouse)
        {
            int x = Cursor.Position.X;
            int y = Cursor.Position.Y;
            var xy = new Point(x, y);

            while (true)
            {
                Random random = new Random();
                if (x < CoordinateX)
                {
                    x += random.Next(0, 6);
                    xy = new Point(x, y);
                    Cursor.Position = xy;
                }
                if (x > CoordinateX)
                {
                    x -= random.Next(0, 6);
                    xy = new Point(x, y);
                    Cursor.Position = xy;
                }
                if (y < CoordinateY)
                {
                    y += random.Next(0, 6);
                    xy = new Point(x, y);
                    Cursor.Position = xy;
                }
                if (y > CoordinateY)
                {
                    y -= random.Next(0, 6);
                    xy = new Point(x, y);
                    Cursor.Position = xy;
                }
                if (x == CoordinateX && y == CoordinateY)
                {
                    Cursor.Position = xy;
                    uint X = (uint)Cursor.Position.X;
                    uint Y = (uint)Cursor.Position.Y;
                    if (btnMouse == "left")
                    {
                        mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTDOWN, X, Y, 0, 0);
                        mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
                    }
                    if (btnMouse == "right")
                    {
                        mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_RIGHTDOWN, X, Y, 0, 0);
                        mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_RIGHTUP, X, Y, 0, 0);
                    }
                    break;
                }
                Thread.Sleep(random.Next(1, 10));
            }

            Thread.Sleep(sleep);
        }
        public void InputKey(string key)
        {
            SendKeys.SendWait(key);
        }

        public void ClickOnReCAPTCHACheckbox(int ex, int ey)
        {
            int CoordinateX = ex;
            int CoordinateY = ey;
            string btnMouse = "left";
            int sleep = 3000;
            MakeCoordinates(CoordinateX, CoordinateY, sleep, btnMouse);

        }

        public void ClickToAudioChallenge(int ex, int ey)
        {
            int CoordinateX = ex;
            int CoordinateY = ey;
            string btnMouse = "left";
            int sleep = 2000;
            MakeCoordinates(CoordinateX, CoordinateY, sleep, btnMouse);
        }

        public void ClickToDownloadAudio(int ex, int ey)
        {
            int CoordinateX = ex;
            int CoordinateY = ey;
            string btnMouse = "left";
            int sleep = 2000;
            MakeCoordinates(CoordinateX, CoordinateY, sleep, btnMouse);
        }

        public void RightButtonToDownload()
        {
            int coordenadaX = 626;
            int coordenadaY = 470;
            int sleep = 2000;
            string btnMouse = "right";
            MakeCoordinates(coordenadaX, coordenadaY, sleep, btnMouse);
        }

        public void SaveAs()
        {
            int coordenadaX = 670;
            int coordenadaY = 570;
            int sleep = 2000;
            string btnMouse = "left";
            MakeCoordinates(coordenadaX, coordenadaY, sleep, btnMouse);
        }

        public void Save()
        {
            int coordenadaX = 1106;
            int coordenadaY = 949;
            int sleep = 2000;
            string btnMouse = "left";
            MakeCoordinates(coordenadaX, coordenadaY, sleep, btnMouse);
        }

        public void BackToPage()
        {
            int coordenadaX = 98;
            int coordenadaY = 18;
            int sleep = 2000;
            string btnMouse = "left";
            MakeCoordinates(coordenadaX, coordenadaY, sleep, btnMouse);
        }
        public void AudioAnswer(int ex, int ey)
        {
            int CoordinateX = ex;
            int CoordinateY = ey;
            string btnMouse = "left";
            int sleep = 2000;
            MakeCoordinates(CoordinateX, CoordinateY, sleep, btnMouse);
        }

        public void CheckAudio(int ex, int ey)
        {
            int CoordinateX = ex;
            int CoordinateY = ey;
            string btnMouse = "left";
            int sleep = 2000;
            MakeCoordinates(CoordinateX, CoordinateY, sleep, btnMouse);
        }
        public void Send(int ex, int ey)
        {
            int CoordinateX = ex;
            int CoordinateY = ey;
            string btnMouse = "left";
            int sleep = 2000;
            MakeCoordinates(CoordinateX, CoordinateY, sleep, btnMouse);
        }
    }
}
