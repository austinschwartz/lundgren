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
        public static float P1XValue = 0;
        public static float P2XValue = 0;
        public static float P1YValue = 0;
        public static float P2YValue = 0;

        public static PlayerData p1;
        public static PlayerData p2;

        public static byte StageNum = 0;

        public static string TimerString;
        public static TimeSpan Timer;

        private static uint _p1StaticAddress = 0x80443080;
        private static uint _p2StaticAddress = 0x80443f10;


        public static void P1Data()
        {
            var x = Memory.ReadBytesAsBytes(0x80443080, 0xB4, false);
            Debug.WriteLine(String.Format("{0:X}", x));
            Debug.WriteLine(BitConverter.ToString(x));


        }

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
        public static string P1X => p1.x.ToString();
        public static string P2X => p2.x.ToString();
        public static string P1Y => p1.y.ToString();
        public static string P2Y => p2.y.ToString();


        public static void GetState()
        {
            if (!Memory.Initialized)
                Memory.Initialize();

            P1CharNum = Memory.ReadByte(0x8042208F);
            P2CharNum = Memory.ReadByte(0x80422097);
            //P1StockNum = Memory.ReadByte(0x8044310E); //8045310C
            /*
            var x = Memory.ReadBytesAsBytes(0x8044310C, 4, false);

            if (x != null)
            {
                P1StockNum = x[2];
                // P1Suicides = probably x[1] or x[0]
            }
            
            P2StockNum = Memory.ReadByte(0x80443F9E);
            P1Percent = Memory.ReadByte(0x804430E1);
            P2Percent = Memory.ReadByte(0x80443F71);
            */
            P1AnimationNum = Memory.ReadBytes(_p1StaticAddress, 2, true);
            P2AnimationNum = Memory.ReadBytes(_p2StaticAddress, 2, true);


            p1 = new PlayerData(0x80443080);
            p2 = new PlayerData(0x80443f10);

            StageNum = Memory.ReadByte(0x804C6CAE);

            var t = Memory.ReadBytes(0x8045B6C6, 2);
            Timer = TimeSpan.FromHours(8).Subtract(TimeSpan.FromSeconds(t));
            TimerString = Timer.ToString(@"hh\:mm\:ss");

        }

        public static int GetFrame()
        {
            if (!Memory.Initialized)
                Memory.Initialize();

            var frame = Memory.ReadBytes(0x80469D5C, 4);
            if (frame <= LastFrame)
                frame++;
            LastFrame = frame;
            return frame;
        }


    }
}