using System;
using System.Diagnostics;
using System.Threading;
using Lundgren.Game.Helpers;

namespace Lundgren.Game
{
    public static class GameState
    {
        public static byte p1CharNum = 0;
        public static byte p1Percent = 0;
        public static byte p1Stocks = 0;

        public static byte p2CharNum = 0;
        public static byte p2Percent = 0;
        public static byte p2Stocks = 0;

        public static byte stageNum = 0;

        public static String timerString;
        public static TimeSpan timer;

        public static int lastFrame = 0;

        public static Memory mem = null;

        public static bool Initialize()
        {
            mem = new Memory();
            var success = mem.OpenProcess("Dolphin");
            while (success == false)
            {
                Thread.Sleep(1000);
                success = mem.OpenProcess("Dolphin");
            }
            return true;
        }

        public static void GetState()
        {
            if (mem == null)
            {
                Initialize();
            }

            p1CharNum = mem.ReadByte(0x8042208F);
            p2CharNum = mem.ReadByte(0x80422097);
            p1Percent = mem.ReadByte(0x804430E1);
            p2Percent = mem.ReadByte(0x80443F71);
            p1Stocks  = mem.ReadByte(0x8044310E);
            p2Stocks  = mem.ReadByte(0x80443F9E);

            stageNum = mem.ReadByte(0x804C6CAE);

            var t = mem.ReadBytes(0x8045B6C6, 2);
            timer = TimeSpan.FromHours(8).Subtract(TimeSpan.FromSeconds(t));
            timerString = timer.ToString(@"hh\:mm\:ss");

        }

        public static int GetFrame()
        {
            if (mem == null)
            {
                Initialize();
            }
            int frame = (int)(mem.ReadBytes(0x80469D5C, 4));
            if (frame <= lastFrame)
                frame++;
            lastFrame = frame;
            return frame;
        }


    }
}