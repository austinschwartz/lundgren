using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Lundgren.Game.Helpers
{
    internal class Mapi
    {
        [DllImport("kernel32.dll")]
        public static extern int ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] bBuffer, uint size, out IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        public static extern int WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] bBuffer, uint size, out IntPtr lpNumberOfBytesWritten);
    }

    public class Memory
    {
        private IntPtr _hReadProcess = IntPtr.Zero;
        private Process _mReadProcess;

        public bool OpenProcess()
        {
            _mReadProcess = Process.GetCurrentProcess();
            if (!(_mReadProcess.Handle != IntPtr.Zero))
                return false;
            _hReadProcess = _mReadProcess.Handle;
            return true;
        }
        public bool OpenProcess(string sProcessName)
        {
            var processesByName = Process.GetProcessesByName(sProcessName);
            if (processesByName.Length <= 0)
                return false;
            _mReadProcess = processesByName[0];
            if (!(_mReadProcess.Handle != IntPtr.Zero))
                return false;
            _hReadProcess = _mReadProcess.Handle;
            return true;
        }
        public int BaseAddress()
        {
            return _mReadProcess.MainModule.BaseAddress.ToInt32();
        }

        public byte ReadByte(long iMemoryAddress)
        {
            var bBuffer = new byte[1];
            IntPtr lpNumberOfBytesRead;
            if (Mapi.ReadProcessMemory(this._hReadProcess, (IntPtr)iMemoryAddress, bBuffer, 1U, out lpNumberOfBytesRead) == 0)
                return (byte)0;
            return bBuffer[0];
        }

        public int ReadBytes(long iMemoryAddress, uint b, bool bigEndian = true)
        {
            var bBuffer = new byte[b];
            IntPtr lpNumberOfBytesRead;
            if (Mapi.ReadProcessMemory(this._hReadProcess, (IntPtr)iMemoryAddress, bBuffer, b, out lpNumberOfBytesRead) == 0)
                return (int)0;
            if (bigEndian)
            {
                Array.Reverse(bBuffer);
            }
            switch (b)
            {
                case 4:
                    return (int)BitConverter.ToInt32(bBuffer, 0);
                case 2:
                    return (int) BitConverter.ToInt16(bBuffer, 0);
                default:
                    return (int) bBuffer[0];
            }

        }

        public ushort ReadShort(long iMemoryAddress)
        {
            var bBuffer = new byte[2];
            IntPtr lpNumberOfBytesRead;
            if (Mapi.ReadProcessMemory(this._hReadProcess, (IntPtr)iMemoryAddress, bBuffer, 2U, out lpNumberOfBytesRead) == 0)
                return (ushort)0;
            Array.Reverse(bBuffer);
            return BitConverter.ToUInt16(bBuffer, 0);
        }

        public uint ReadInt(long iMemoryAddress)
        {
            var bBuffer = new byte[4];
            IntPtr lpNumberOfBytesRead;
            if (Mapi.ReadProcessMemory(this._hReadProcess, (IntPtr)iMemoryAddress, bBuffer, 4U, out lpNumberOfBytesRead) == 0)
                return 0U;
            Array.Reverse(bBuffer);
            return BitConverter.ToUInt32(bBuffer, 0);
        }

        public long ReadLong(long iMemoryAddress)
        {
            var bBuffer = new byte[8];
            IntPtr lpNumberOfBytesRead;
            if (Mapi.ReadProcessMemory(this._hReadProcess, (IntPtr)iMemoryAddress, bBuffer, 8U, out lpNumberOfBytesRead) == 0)
                return 0L;
            Array.Reverse(bBuffer);
            return BitConverter.ToInt64(bBuffer, 0);
        }

        public float ReadFloat(long iMemoryAddress)
        {
            var bBuffer = new byte[4];
            IntPtr lpNumberOfBytesRead;
            if (Mapi.ReadProcessMemory(this._hReadProcess, (IntPtr)iMemoryAddress, bBuffer, 4U, out lpNumberOfBytesRead) == 0)
                return 0.0f;
            Array.Reverse(bBuffer);
            return BitConverter.ToSingle(bBuffer, 0);
        }

        public double ReadDouble(long iMemoryAddress)
        {
            var bBuffer = new byte[8];
            IntPtr lpNumberOfBytesRead;
            if (Mapi.ReadProcessMemory(this._hReadProcess, (IntPtr)iMemoryAddress, bBuffer, 8U, out lpNumberOfBytesRead) == 0)
                return 0.0;
            Array.Reverse(bBuffer);
            return BitConverter.ToDouble(bBuffer, 0);
        }

        public bool Write(long iMemoryAddress, byte bByteToWrite)
        {
            var bBuffer = new byte[1]{bByteToWrite};
            Array.Reverse(bBuffer);
            IntPtr lpNumberOfBytesWritten;
            Mapi.WriteProcessMemory(this._hReadProcess, (IntPtr)iMemoryAddress, bBuffer, 1U, out lpNumberOfBytesWritten);
            return lpNumberOfBytesWritten.ToInt32() == 1;
        }

        public bool Write(long iMemoryAddress, short iShortToWrite)
        {
            var bBuffer = BitConverter.GetBytes(iShortToWrite);
            Array.Reverse(bBuffer);
            IntPtr lpNumberOfBytesWritten;
            Mapi.WriteProcessMemory(this._hReadProcess, (IntPtr)iMemoryAddress, bBuffer, 2U, out lpNumberOfBytesWritten);
            return lpNumberOfBytesWritten.ToInt32() == 2;
        }
    }
}
