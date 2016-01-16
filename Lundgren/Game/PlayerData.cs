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
        public float x = 0.0F, y = 0.0F;
        public byte CostumeId = 0;
        public byte CharNum = 99;
        public byte Percent = 0;
        public byte StockNum = 0;
        public int AnimationNum = 0;

        public String Character => GameData.ExternalChar(CharNum);

        public PlayerData(Player player)
        {
            var temp = Memory.ReadBytesAsBytes(StaticAddress[(int)player], 0xB4, false);
            if (temp == null) return;

            x = Memory.BytesToFloat(temp[19], temp[18], temp[17], temp[16]);
            y = Memory.BytesToFloat(temp[23], temp[22], temp[21], temp[20]);
            CostumeId = temp[68];

            Percent = Memory.ReadByte(PercentAddress[(int)player]);
            CharNum = Memory.ReadByte(CharNumAddress[(int)player]);

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
    }
}
//P1StockNum = Memory.ReadByte(0x8044310E); //8045310C
/*
var x = Memory.ReadBytesAsBytes(0x8044310C, 4, false);

if (x != null)
{
    P1StockNum = x[2];
    // P1Suicides = probably x[1] or x[0]
}

P2StockNum = Memory.ReadByte(0x80443F9E);
*/
