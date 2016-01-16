using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lundgren.Game.Helpers;

namespace Lundgren.Game
{
    public class PlayerData
    {
        public enum Player { One = 1, Two = 2, Three = 3, Four = 4 }



        public float x = 0.0F, y = 0.0F;
        public byte CostumeId = 0;

        public PlayerData(uint baseAddress)
        {
            var temp = Memory.ReadBytesAsBytes(baseAddress, 0xB4, false);
            if (temp == null) return;

            x = Memory.BytesToFloat(temp[19], temp[18], temp[17], temp[16]);
            y = Memory.BytesToFloat(temp[23], temp[22], temp[21], temp[20]);
            CostumeId = temp[68];
        }

    }
}