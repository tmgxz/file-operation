using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_FileOperation
{
    /// <summary>
    /// 基本操作
    /// </summary>
    public class BaseOperation
    {
        //创建文件夹
        public static void CreateFolder()
        {
            string path = @"E:\Directory\first";
            //此方法在文件夹存在时也不会抛异常
            Directory.CreateDirectory(path);
        }

        //获得指定目录下的所有文件路径
        public static void GetAllFile()
        {
            string path = @"C:\MyEclipse";
            //*匹配0个或多个字符
            //?匹配0个或一个字符
            string[] paths = Directory.GetFiles(path,"*",SearchOption.AllDirectories);
        }

        //删除指定文件
        //注意！删除的文件在回收站中找不到
        public static void DeleteFile()
        {
            string path = @"E:\txtFileOp.txt";
            File.Delete(path);
        }

        //删除文件夹及其子目录
        //注意！删除的文件夹在回收站中找不到
        public static void DeleteDir()
        {
            string path = @"E:\rr";
            DirectoryInfo dir = new DirectoryInfo(path);
            if (dir.Exists)
            {
                dir.Delete(true);
            }
        }

        /// <summary>
        /// 删除文件到回收站
        /// 也可以指定永久删除
        /// 需要引入Microsoft.VisualBasic.dll
        /// using Microsoft.VisualBasic.FileIO
        /// </summary>
        public static void DeleteFileToRecycleBin()
        {
            string path = @"E:\rr";
            Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(path,
                Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, 
                Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
        }

        /// <summary>
        /// 删除文件夹及其子文件到回收站
        /// 也可以指定永久删除
        /// 需要引入Microsoft.VisualBasic.dll
        /// using Microsoft.VisualBasic.FileIO
        /// </summary>
        public static void DeleteDirToRecycleBin()
        {
            string path = @"E:\rr";
            Microsoft.VisualBasic.FileIO.FileSystem.DeleteDirectory(path,
                Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
        }

        /// <summary>
        /// 拷贝指定文件
        /// </summary>
        /// <param name="sourceFileName"></param>
        /// <param name="destFileName"></param>
        public static void CopyFile(string sourceFileName, string destFileName)
        {
            //若文件已存在，复制则抛异常，所以判断文件是否存在
            if (File.Exists(destFileName))
            {
                Console.WriteLine("文件已经存在");
                return;
            }
            File.Copy(sourceFileName, destFileName,false);
        }

        /// <summary>
        /// 拷贝指定文件夹下所有文件
        /// 并保持原文件夹结构
        /// 
        /// 原来的文件夹放在destPath下
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="destPath"></param>
        public static void CopyFolder(string sourcePath, string destPath)
        {
            if (!Directory.Exists(sourcePath))
            {
                throw new DirectoryNotFoundException("源路径不存在！");
            }

            Directory.CreateDirectory(destPath);

            //获得源路径下所有文件
            string[] files = Directory.GetFiles(sourcePath);
            foreach (var file in files)
            {
                //Path.GetFileName获得文件名和扩展名，不包含文件所在的文件夹路径
                string destFile = Path.Combine(destPath, Path.GetFileName(file));
                File.Copy(file, destFile, true);
            }

            //获得源文件下所有目录文件
            string[] folders = Directory.GetDirectories(sourcePath);
            foreach (var folder in folders)
            {
                string destDir = Path.Combine(destPath, Path.GetFileName(folder));
                //采用递归的方法实现
                CopyFolder(folder, destDir);
            }
        }

        /// <summary>
        /// 压缩文件
        /// 
        /// 使用通用解压缩工具解压后的文件没有扩展名，但可以打开
        /// </summary>
        /// <param name="sourceFile">待压缩文件流</param>
        public static void Compress(FileStream sourceFile)
        {
            //压缩文件名称
            string zipFileName = @"E:\ziptest.zip" ;
            using (FileStream zipFileStream = new FileStream(zipFileName,FileMode.CreateNew))
            {
                using (GZipStream gzs = new GZipStream(zipFileStream, CompressionMode.Compress))
                {
                    sourceFile.CopyTo(gzs);
                }
            }
        }
        /// <summary>
        /// 解压缩
        /// </summary>
        /// <param name="zipfs">待解压文件</param>
        public static void Decompress(FileStream zipfs)
        {
            //指定解压后的文件名（包括扩展名）
            string dzipFileName = @"E:\dziptest.jpg";
            using (FileStream zipFileStream = new FileStream(dzipFileName, FileMode.CreateNew))
            {
                using (GZipStream zipStream = new GZipStream(zipfs, CompressionMode.Decompress))
                {
                    zipStream.CopyTo(zipFileStream);
                }
            }
        }
    }
}
