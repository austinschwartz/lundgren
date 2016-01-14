using System;
using System.Runtime.InteropServices;

public class ProcessMemoryReaderWriter
{
    [DllImport("kernel32.dll")]
    public static extern IntPtr OpenProcess(uint dwDesiredAccess, int bInheritHandle, uint dwProcessId);
    [DllImport("kernel32.dll")]
    public static extern int ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] buffer, uint size, out IntPtr lpNumberOfBytesRead);
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, int lpNumberOfBytesWritten);
    private int Handle;

    public ProcessMemoryReaderWriter(int pid)
    {
        this.Handle = (int)OpenProcess(0x38, 1, (uint)pid);
    }

    public int MultiLevelPointerReader(int Address, int[] offsets)
    {
        int level = offsets.Length;
        int CurrentAddress = Address;
        byte[] bAddr = new byte[4];
        int alevel = level - 1;
        for (int cur = 0; cur <= alevel; cur++)
        {
            IntPtr br = IntPtr.Zero;
            IntPtr brp = br;
            ReadProcessMemory((IntPtr)this.Handle, (IntPtr)CurrentAddress, bAddr, 4, out brp);
            CurrentAddress = BitConverter.ToInt32(bAddr, 0) + offsets[cur];
        }
        return CurrentAddress;
    }

    public byte[] ReadByteArray(int Address, int Size)
    {
        int ret = 0;
        byte[] bArray = new byte[(Size - 1) + 1];
        IntPtr rdata = (IntPtr)ret;
        ReadProcessMemory((IntPtr)this.Handle, (IntPtr)Address, bArray, (uint)Size, out rdata);
        ret = (int)rdata;
        return bArray;
    }
    public double ReadDouble(int Address)
    {
        IntPtr rdata = IntPtr.Zero;
        byte[] bArray = new byte[8];
        IntPtr rpdata = rdata;
        ReadProcessMemory((IntPtr)this.Handle, (IntPtr)Address, bArray, 8, out rpdata);
        int rDbl = (int)Math.Round(BitConverter.ToDouble(bArray, 0));
        return (double)rDbl;
    }


    public float ReadFloat(int Address)
    {
        IntPtr intPtr = IntPtr.Zero;
        byte[] bArray = new byte[4];
        IntPtr intPtr1 = intPtr;
        ProcessMemoryReaderWriter.ReadProcessMemory((IntPtr)this.Handle, (IntPtr)Address, bArray, 4, out intPtr1);
        int rFlt = (int)Math.Round((double)BitConverter.ToSingle(bArray, 0));
        float single = (float)rFlt;
        return single;
    }

    public int ReadInteger(int Address)
    {
        IntPtr intPtr = IntPtr.Zero;
        byte[] bArray = new byte[4];
        IntPtr intPtr1 = intPtr;
        ProcessMemoryReaderWriter.ReadProcessMemory((IntPtr)this.Handle, (IntPtr)Address, bArray, 4, out intPtr1);
        int rInt = BitConverter.ToInt32(bArray, 0);
        int num = rInt;
        return num;
    }
    public bool WriteByteArray(int Address, byte[] bArray)
    {
        bool flag = ProcessMemoryReaderWriter.WriteProcessMemory((IntPtr)this.Handle, (IntPtr)Address, bArray, (uint)((int)bArray.Length), 0);
        return flag;
    }
    public bool WriteDouble(int Address, double Value)
    {
        byte[] bArray = BitConverter.GetBytes(Value);
        bool flag = ProcessMemoryReaderWriter.WriteProcessMemory((IntPtr)this.Handle, (IntPtr)Address, bArray, 8, 0);
        return flag;
    }
    public bool WriteFloat(int Address, float Value)
    {
        byte[] bArray = BitConverter.GetBytes(Value);
        bool flag = ProcessMemoryReaderWriter.WriteProcessMemory((IntPtr)this.Handle, (IntPtr)Address, bArray, 4, 0);
        return flag;
    }
    public bool WriteInteger(int Address, int Value)
    {
        byte[] bArray = BitConverter.GetBytes(Value);
        bool flag = ProcessMemoryReaderWriter.WriteProcessMemory((IntPtr)this.Handle, (IntPtr)Address, bArray, 4, 0);
        return flag;
    }
}