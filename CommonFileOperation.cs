using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_FileOperation
{
    /// <summary>
    /// 普通文件操作
    /// </summary>
    public class CommonFileOperation
    {
        /// <summary>
        /// 读取文件，将内容写入字节数组
        /// 确保待操作文件最大不超过2147483647,约为2048MB（略小于）
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="lengthPerRead">每次读取的最大值，不能超过文件长度</param>
        /// <returns></returns>
        public static byte[] ReadFileToBytes(string sourcePath, int lengthPerRead)
        {
            byte[] data = null;
            using (FileStream fs = new FileStream(sourcePath, FileMode.Open))
            {
                if (fs.Length == 0)
                {
                    return new byte[]{};
                }

                if (lengthPerRead > fs.Length)
                {
                    throw new Exception("lengthPerRead 超过文件总长度");
                }

                long offset = 0;
                data = new byte[fs.Length];
                while (true)
                {
                    offset = fs.Seek(offset, SeekOrigin.Begin);
                    if (offset == fs.Length)
                    {
                        break;
                    }

                    if (offset + lengthPerRead > fs.Length)
                    {
                        lengthPerRead = (int)(fs.Length - offset);
                    }
                    int n = fs.Read(data, (int)offset, lengthPerRead);
                    offset += n;
                }
            }

            return data;
        }

        /// <summary>
        /// 读取文件，将内容写入字节数组
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <returns></returns>
        public static byte[] ReadFileToBytes(string sourcePath)
        {
            return File.ReadAllBytes(sourcePath);
        }

        /// <summary>
        /// 保存文件到磁盘
        /// </summary>
        /// <param name="sourceStream"></param>
        /// <param name="destinationPath"></param>
        public static void WriteToDisk(Stream sourceStream,string destinationPath)
        {
            using (Stream fss = new FileStream(destinationPath, FileMode.OpenOrCreate))
            {
                sourceStream.CopyTo(fss);
            }
        }
    }
}
