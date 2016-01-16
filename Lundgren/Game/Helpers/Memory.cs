using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using Lundgren.Game.Helpers;

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
        private static IntPtr _hReadProcess = IntPtr.Zero;
        private static Process _mReadProcess;

        public static bool Initialized = false;

        public static bool Initialize()
        {
            var success = OpenProcess("Dolphin");
            while (success == false)
            {
                Thread.Sleep(1000);
                success = OpenProcess("Dolphin");
            }
            Initialized = true;
            return true;
        }


        public static bool OpenProcess()
        {
            _mReadProcess = Process.GetCurrentProcess();
            if (!(_mReadProcess.Handle != IntPtr.Zero))
                return false;
            _hReadProcess = _mReadProcess.Handle;
            return true;
        }
        public static bool OpenProcess(string sProcessName)
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
        public static int BaseAddress()
        {
            return _mReadProcess.MainModule.BaseAddress.ToInt32();
        }

        public static float BytesToFloat(params byte[] bytes)
        {
            return System.BitConverter.ToSingle(bytes, 0);
        }
        public static UInt32 BytesToInt32(byte[] bytes, int i)
        {
            return System.BitConverter.ToUInt32(bytes, i);
        }
        public static UInt16 BytesToInt16(byte[] bytes, int i)
        {
            return System.BitConverter.ToUInt16(bytes, i);
        }

        public static byte ReadByte(long iMemoryAddress)
        {
            var bBuffer = new byte[1];
            IntPtr lpNumberOfBytesRead;
            if (Mapi.ReadProcessMemory(_hReadProcess, (IntPtr)iMemoryAddress, bBuffer, 1U, out lpNumberOfBytesRead) == 0)
                return (byte)0;
            return bBuffer[0];
        }

        public static int ReadBytes(long iMemoryAddress, uint b, bool bigEndian = true)
        {
            var bBuffer = new byte[b];
            IntPtr lpNumberOfBytesRead;
            if (Mapi.ReadProcessMemory(_hReadProcess, (IntPtr)iMemoryAddress, bBuffer, b, out lpNumberOfBytesRead) == 0)
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
                    return (int)BitConverter.ToInt16(bBuffer, 0);
                default:
                    return (int)bBuffer[0];
            }
        }

        public static byte[] ReadBytesAsBytes(long iMemoryAddress, uint b, bool bigEndian = true)
        {
            var bBuffer = new byte[b];
            IntPtr lpNumberOfBytesRead;
            if (Mapi.ReadProcessMemory(_hReadProcess, (IntPtr)iMemoryAddress, bBuffer, b, out lpNumberOfBytesRead) == 0)
                return null;
            if (bigEndian)
            {
                Array.Reverse(bBuffer);
            }
            return bBuffer;

        }

        public static ushort ReadShort(long iMemoryAddress)
        {
            var bBuffer = new byte[2];
            IntPtr lpNumberOfBytesRead;
            if (Mapi.ReadProcessMemory(_hReadProcess, (IntPtr)iMemoryAddress, bBuffer, 2U, out lpNumberOfBytesRead) == 0)
                return (ushort)0;
            Array.Reverse(bBuffer);
            return BitConverter.ToUInt16(bBuffer, 0);
        }

        public static uint ReadInt(long iMemoryAddress)
        {
            var bBuffer = new byte[4];
            IntPtr lpNumberOfBytesRead;
            if (Mapi.ReadProcessMemory(_hReadProcess, (IntPtr)iMemoryAddress, bBuffer, 4U, out lpNumberOfBytesRead) == 0)
                return 0U;
            Array.Reverse(bBuffer);
            return BitConverter.ToUInt32(bBuffer, 0);
        }

        public static long ReadLong(long iMemoryAddress)
        {
            var bBuffer = new byte[8];
            IntPtr lpNumberOfBytesRead;
            if (Mapi.ReadProcessMemory(_hReadProcess, (IntPtr)iMemoryAddress, bBuffer, 8U, out lpNumberOfBytesRead) == 0)
                return 0L;
            Array.Reverse(bBuffer);
            return BitConverter.ToInt64(bBuffer, 0);
        }

        public static float ReadFloat(long iMemoryAddress)
        {
            var bBuffer = new byte[4];
            IntPtr lpNumberOfBytesRead;
            if (Mapi.ReadProcessMemory(_hReadProcess, (IntPtr)iMemoryAddress, bBuffer, 4U, out lpNumberOfBytesRead) == 0)
                return 0.0f;
            Array.Reverse(bBuffer);
            return BitConverter.ToSingle(bBuffer, 0);
        }

        public static double ReadDouble(long iMemoryAddress)
        {
            var bBuffer = new byte[8];
            IntPtr lpNumberOfBytesRead;
            if (Mapi.ReadProcessMemory(_hReadProcess, (IntPtr)iMemoryAddress, bBuffer, 8U, out lpNumberOfBytesRead) == 0)
                return 0.0;
            Array.Reverse(bBuffer);
            return BitConverter.ToDouble(bBuffer, 0);
        }

        public static bool Write(long iMemoryAddress, byte bByteToWrite)
        {
            var bBuffer = new byte[1]{bByteToWrite};
            Array.Reverse(bBuffer);
            IntPtr lpNumberOfBytesWritten;
            Mapi.WriteProcessMemory(_hReadProcess, (IntPtr)iMemoryAddress, bBuffer, 1U, out lpNumberOfBytesWritten);
            return lpNumberOfBytesWritten.ToInt32() == 1;
        }

        public static bool Write(long iMemoryAddress, short iShortToWrite)
        {
            var bBuffer = BitConverter.GetBytes(iShortToWrite);
            Array.Reverse(bBuffer);
            IntPtr lpNumberOfBytesWritten;
            Mapi.WriteProcessMemory(_hReadProcess, (IntPtr)iMemoryAddress, bBuffer, 2U, out lpNumberOfBytesWritten);
            return lpNumberOfBytesWritten.ToInt32() == 2;
        }
    }
}
