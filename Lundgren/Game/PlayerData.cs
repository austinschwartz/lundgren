using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lundgren.Game.Helpers;

namespace Lundgren.Game
{
    public class PlayerData
    {
        public enum Player { One = 0, Two = 1, Three = 2, Four = 3 }

        /* Addresses */
        private static readonly uint[] StaticAddress  = new uint[] { 0x80443080, 0x80443f10, 0, 0 };
        private static readonly uint[] CharNumAddress = new uint[] { 0x8042208F, 0x80422097, 0, 0 };
        private static readonly uint[] PercentAddress = new uint[] { 0x804430E1, 0x80443F71, 0, 0 };


        /* Values */
        public double x = 0.0D, y = 0.0D;
        public byte CostumeId = 0;
        public byte CharNum = 99;
        public byte Percent = 0;
        public byte StockNum = 0;
        public uint ActionNum = 0;
        public uint AnimationNum = 0;

        public String Character => GameData.ExternalChar(CharNum);

        public PlayerData(Player player)
        {
            /* Static character block */
            var staticBlock = Memory.ReadBytesAsBytes(StaticAddress[(int)player], 0xB4, false);
            if (staticBlock == null) return;

            x = Memory.BytesToFloat(staticBlock[19], staticBlock[18], staticBlock[17], staticBlock[16]);
            y = Memory.BytesToFloat(staticBlock[23], staticBlock[22], staticBlock[21], staticBlock[20]);
            CostumeId = staticBlock[68];

            Percent = Memory.ReadByte(PercentAddress[(int)player]);
            CharNum = Memory.ReadByte(CharNumAddress[(int)player]);

            /* Character block */
            long player1Pointer = Memory.BytesToLong(staticBlock[179], staticBlock[178], staticBlock[177], staticBlock[176]) - 0x10000;
            var temp = Memory.ReadBytesAsBytes(player1Pointer, 0x14C, false);
            if (temp == null) return;

            ActionNum = Memory.BytesToInt32(temp[115], temp[114], temp[113], temp[112]);
            AnimationNum = Memory.BytesToInt32(temp[119], temp[118], temp[117], temp[116]);


        }

        public bool HasCharacterSelected => CharNum != 99;

        public String CharacterImage
        {
            get {
                if (Character.Equals("None"))
                    return "None";
                return $"Lundgren.Game.Images.{Character.Replace(" ", "_").Replace("&", "and")}.{CostumeId + 1}.png";
            }
        }

        public bool OnLeftLedge(StageData stage)
        {
            if (Math.Abs(this.x - stage.LeftEdge.x) < 0.1)
                if (Math.Abs(this.y - stage.LeftEdge.y) < 0.1)
                    return true;
            return false;
        }

        public bool OnRightLedge(StageData stage)
        {
            if (Math.Abs(this.x - stage.RightEdge.x) < 0.1)
                if (Math.Abs(this.y - stage.RightEdge.y) < 0.1)
                    return true;
            return false;
        }

    }
}
/*
var x = Memory.ReadBytesAsBytes(0x8044310C, 4, false);

if (x != null)
{
    P1StockNum = x[2];
    // P1Suicides = probably x[1] or x[0]
}

//P1StockNum = Memory.ReadByte(0x8044310E); //8045310C
//P2StockNum = Memory.ReadByte(0x80443F9E);
*/
