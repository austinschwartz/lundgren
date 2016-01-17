using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Lundgren.Game.Helpers;

namespace Lundgren.Game
{
    public static class GameState
    {

        public static PlayerData p1;
        public static PlayerData p2;

        public static byte StageNum = 99;
        public static byte PrevStageNum = 99;

        public static string TimerString;
        public static TimeSpan Timer;

        public static StageData Stage = null; 

        public static void P1Data()
        {
            var x = Memory.ReadBytesAsBytes(0x80443080, 0xB4, false);
            Debug.WriteLine(String.Format("{0:X}", x));
            Debug.WriteLine(BitConverter.ToString(x));

        }

        public static int LastFrame = 0;

        public static Memory Mem = null;

        /* Strings */
        public static string P1Char => p1.Character;
        public static string P2Char => p2.Character;
        public static string P1Stocks => p1.StockNum.ToString();
        public static string P2Stocks => p2.StockNum.ToString();
        public static string P1Percent => p1.Percent.ToString();
        public static string P2Percent => p2.Percent.ToString();
        public static string P1Animation => "??";
        public static string P2Animation => "??";
        public static string StageString => GameData.Stage(StageNum);
        public static string P1X => p1.x.ToString();
        public static string P2X => p2.x.ToString();
        public static string P1Y => p1.y.ToString();
        public static string P2Y => p2.y.ToString();


        public static void GetState()
        {
            if (!Memory.Initialized)
                Memory.Initialize();

            p1 = new PlayerData(PlayerData.Player.One);
            p2 = new PlayerData(PlayerData.Player.Two);

            UpdateStage();

            TimerString = GetTime().ToString(@"hh\:mm\:ss");

        }
        
        public static void UpdateStage()
        {
            StageNum = Memory.ReadByte(0x804C6CAE);
            if (StageNum != PrevStageNum)
            {
                switch (StageNum)
                {
                    case 7:
                        Stage = new YoshisIsland();
                        break;
                    case 8:
                        Stage = new FountainOfDreams();
                        break;
                    case 18:
                        Stage = new PokemonStadium();
                        break;
                    case 24:
                        Stage = new Battlefield();
                        break;
                    case 25:
                        Stage = new FinalDestination();
                        break;
                    case 26:
                        Stage = new Dreamland();
                        break;
                }
                PrevStageNum = StageNum;
            }
        }

        public static TimeSpan GetTime()
        {
            var timerBytes = Memory.ReadBytes(0x8045B6C6, 2);
            return TimeSpan.FromHours(8).Subtract(TimeSpan.FromSeconds(timerBytes));
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