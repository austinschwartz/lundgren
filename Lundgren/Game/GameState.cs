using System;
using System.Diagnostics;
using System.Threading;
using Lundgren.Game.Helpers;

namespace Lundgren.Game
{
    public static class GameState
    {
        public static byte P1CharNum = 0;
        public static byte P2CharNum = 0;
        public static byte P1Percent = 0;
        public static byte P2Percent = 0;
        public static byte P1StockNum = 0;
        public static byte P2StockNum = 0;
        public static int P1AnimationNum = 0;
        public static int P2AnimationNum = 0;

        public static byte StageNum = 0;

        public static string TimerString;
        public static TimeSpan Timer;

        public static int LastFrame = 0;

        public static Memory Mem = null;

        /* Strings */
        public static string P1Char => GameData.ExternalChar(P1CharNum);
        public static string P2Char => GameData.ExternalChar(P2CharNum);
        public static string P1Stocks => P1StockNum.ToString();
        public static string P2Stocks => P2StockNum.ToString();
        public static string P1Animation => GameData.Animation(P1AnimationNum);
        public static string P2Animation => GameData.Animation(P2AnimationNum);
        public static string Stage => GameData.Stage(StageNum);

        public static bool Initialize()
        {
            Mem = new Memory();
            var success = Mem.OpenProcess("Dolphin");
            while (success == false)
            {
                Thread.Sleep(1000);
                success = Mem.OpenProcess("Dolphin");
            }
            return true;
        }

        public static void GetState()
        {
            if (Mem == null)
            {
                Initialize();
            }

            P1CharNum = Mem.ReadByte(0x8042208F);
            P2CharNum = Mem.ReadByte(0x80422097);
            P1StockNum = Mem.ReadByte(0x8044310E);
            P2StockNum = Mem.ReadByte(0x80443F9E);
            P1Percent = Mem.ReadByte(0x804430E1);
            P2Percent = Mem.ReadByte(0x80443F71);
            P1AnimationNum = Mem.ReadBytes(0x80C601F2, 2, true);
            P2AnimationNum = Mem.ReadBytes(0x80498712, 2, true);

            StageNum = Mem.ReadByte(0x804C6CAE);

            var t = Mem.ReadBytes(0x8045B6C6, 2);
            Timer = TimeSpan.FromHours(8).Subtract(TimeSpan.FromSeconds(t));
            TimerString = Timer.ToString(@"hh\:mm\:ss");

        }

        public static int GetFrame()
        {
            if (Mem == null) Initialize();

            var frame = Mem.ReadBytes(0x80469D5C, 4);
            if (frame <= LastFrame)
                frame++;
            LastFrame = frame;
            return frame;
        }


    }
}