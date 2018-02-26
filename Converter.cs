using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_FileOperation
{
    /// <summary>
    /// 流与字节互转
    /// </summary>
    public class Converter
    {
        /// <summary>
        /// 字节数组转换为流
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static Stream ConvertBytesToStream(byte[] bytes)
        {
            return new MemoryStream(bytes);
        }

        /// <summary>
        /// 流转换为字节数组
        /// </summary>
        /// <param name="sourceStream"></param>
        /// <returns></returns>
        public static byte[] ConvertStreamToBytes(Stream sourceStream)
        {
            long length = sourceStream.Length;
            byte[] data = new byte[length];

            sourceStream.Read(data, 0, (int)length);
            return data;
        }

        /// <summary>
        /// 流转换为字节数组
        /// </summary>
        /// <param name="sourceStream"></param>
        /// <param name="lengthPerRead"></param>
        /// <returns></returns>
        public static byte[] ConvertStreamToBytes(Stream sourceStream, int lengthPerRead)
        {
            byte[] data = null;

            if (sourceStream.Length == 0)
            {
                return new byte[] { };
            }
            long offset = 0;
            data = new byte[sourceStream.Length];
            while (true)
            {
                offset = sourceStream.Seek(offset, SeekOrigin.Begin);
                if (offset == sourceStream.Length)
                {
                    break;
                }

                if (offset + lengthPerRead > sourceStream.Length)
                {
                    lengthPerRead = (int)(sourceStream.Length - offset);
                }
                int n = sourceStream.Read(data, (int)offset, lengthPerRead);
                offset += n;
            }

            return data;
        }
    }
}
