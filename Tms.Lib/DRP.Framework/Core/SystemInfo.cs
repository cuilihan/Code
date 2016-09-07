using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace DRP.Framework.Core
{
    /// <summary>
    /// 计算机系统信息
    /// </summary>
    public class SystemInfo
    {
        /// <summary>
        /// 取得Windows的目录
        /// </summary>
        /// <param name="WinDir"></param>
        /// <param name="count"></param>
        [DllImport("kernel32")]
        public static extern void GetWindowsDirectory(StringBuilder WinDir, int count);
       
        /// <summary>
        /// 获取系统路径
        /// </summary>
        /// <param name="SysDir"></param>
        /// <param name="count"></param>
        [DllImport("kernel32")]
        public static extern void GetSystemDirectory(StringBuilder SysDir, int count);
       
        /// <summary>
        /// 取得CPU信息
        /// </summary>
        /// <param name="cpuinfo"></param>
        [DllImport("kernel32")]
        public static extern void GetSystemInfo(ref CPU_INFO cpuinfo);
       
        /// <summary>
        /// 取得内存状态
        /// </summary>
        /// <param name="meminfo"></param>
        [DllImport("kernel32")]
        public static extern void GlobalMemoryStatus(ref MEMORY_INFO meminfo);
       
        /// <summary>
        /// 取得系统时间
        /// </summary>
        /// <param name="stinfo"></param>
        [DllImport("kernel32")]
        public static extern void GetSystemTime(ref SYSTEMTIME_INFO stinfo);
    }

    //定义CPU的信息结构 
    [StructLayout(LayoutKind.Sequential)]
    public struct CPU_INFO
    {
        public uint dwOemId;
        public uint dwPageSize;
        public uint lpMinimumApplicationAddress;
        public uint lpMaximumApplicationAddress;
        public uint dwActiveProcessorMask;
        public uint dwNumberOfProcessors;
        public uint dwProcessorType;
        public uint dwAllocationGranularity;
        public uint dwProcessorLevel;
        public uint dwProcessorRevision;
    }
  
    //定义内存的信息结构 
    [StructLayout(LayoutKind.Sequential)]
    public struct MEMORY_INFO
    {
        public uint dwLength;
        public uint dwMemoryLoad;
        public uint dwTotalPhys;
        public uint dwAvailPhys;
        public uint dwTotalPageFile;
        public uint dwAvailPageFile;
        public uint dwTotalVirtual;
        public uint dwAvailVirtual;
    }
  
    //定义系统时间的信息结构 
    [StructLayout(LayoutKind.Sequential)]
    public struct SYSTEMTIME_INFO
    {
        public ushort wYear;
        public ushort wMonth;
        public ushort wDayOfWeek;
        public ushort wDay;
        public ushort wHour;
        public ushort wMinute;
        public ushort wSecond;
        public ushort wMilliseconds;
    } 
}
