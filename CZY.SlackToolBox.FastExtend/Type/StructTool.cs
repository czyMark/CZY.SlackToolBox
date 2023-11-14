using System;
using System.Runtime.InteropServices;

namespace  CZY.SlackToolBox.FastExtend
{
    public static class StructTool
	{ 
        /// <summary>
        /// 结构体转换 Byte 数组
        /// </summary>
        /// <param name="structObj"></param>
        /// <returns></returns>
        public static byte[] StructToBytes(object structObj)
        {
            try
            {
                //返回类的非托管大小（以字节为单位）  
                int size = Marshal.SizeOf(structObj);
                //分配大小  
                byte[] bytes = new byte[size];
                //从进程的非托管堆中分配内存给structPtr  
                IntPtr structPtr = Marshal.AllocHGlobal(size);
                //将数据从托管对象structObj封送到非托管内存块structPtr  
                Marshal.StructureToPtr(structObj, structPtr, false);
                //Marshal.StructureToPtr(structObj, structPtr, true);  
                //将数据从非托管内存指针复制到托管 8 位无符号整数数组  
                Marshal.Copy(structPtr, bytes, 0, size);
                //释放使用 AllocHGlobal 从进程的非托管内存中分配的内存  
                Marshal.FreeHGlobal(structPtr);
                return bytes;
            }
            catch (Exception ex)
            { 
                return new byte[0];
            }
        }
    }
}
