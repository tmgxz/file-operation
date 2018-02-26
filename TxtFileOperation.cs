using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_FileOperation
{
    /// <summary>
    /// 操作文档
    /// </summary>
    public class TxtFileOperation
    {
        //读文件
        //StreamReader可以读取任何格式的文本文件
        //目前Windows系统文本编码为ASCII,unicode,utf7,utf8,utf32
        public static void Read()
        {
            string path = @"E:\Directory\first\txtFileOp.txt";
            StringBuilder sb = new StringBuilder();
            using (StreamReader sr = new StreamReader(path,Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    sb.Append(sr.ReadLine());
                }
            }
            Console.WriteLine(sb.ToString());
        }

        //写文件
        //可以指定文件的编码格式
        //目前Windows系统文本编码为ASCII,unicode,utf7,utf8,utf32
        public static void Write()
        {
            string path = @"E:\Directory\first\txtFileOp.txt";
            using (StreamWriter sw = new StreamWriter(path,true,Encoding.Default))
            {
                sw.WriteLine("txt file writer");

                sw.WriteLine("写文件测试");
            }
        }
    }
}
