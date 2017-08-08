using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CompareDelete
{
    class Program
    {
        static void Main(string[] args)
        {
            string referencePathCore = @"C:\Program Files\dotnet\shared\Microsoft.NETCore.App\1.1.2";
            string referencePathMLModule = @"C:\Users\jmarymee\Documents\Visual Studio 2017\Projects\iotedgeml\EdgeModule\bin\Debug";
            string sourcePath = @"C:\repos\IOT_Bakeoff\Simulator\EdgeContainer\iot-edge";
            string fileListFile = @"c:\tools\filesCopied.csv";

            CompareAndDeleteCore(sourcePath, referencePathCore);
            //CompareAndCopyMLModuleCode(sourcePath, referencePathMLModule, fileListFile);
            CopyMLFilesToTarget(sourcePath, referencePathMLModule, fileListFile);
            //RemoveMLModuleUniqueFiles(sourcePath, fileListFile);

            Console.WriteLine("All done. Any key...");
            Console.ReadLine();
        }

        public static void RemoveMLModuleUniqueFiles(string src, string fileListFile)
        {
            if (!File.Exists(fileListFile)) { return; }
            string[] fileListToRemove = File.ReadAllLines(fileListFile);
            foreach (string s in fileListToRemove)
            {
                string fName = Path.GetFileName(s);
                string targetToDelete = src + Path.DirectorySeparatorChar + fName;
                File.Delete(targetToDelete);
            }

            Console.WriteLine(String.Format("{0} Files deleted", fileListToRemove.Length));
        }

        public static void CopyMLFilesToTarget(string src, string refPath, string fileListFile)
        {
            if (!File.Exists(fileListFile)) { return; }
            string[] fileList = File.ReadAllLines(fileListFile);

            foreach (string s in fileList)
            {
                string fileName = Path.GetFileName(s);
                string target = src + Path.DirectorySeparatorChar + fileName;

                File.Copy(s, target);
            }

            Console.WriteLine(String.Format("{0} Files copied", fileList.Length));
        }

        public static void CompareAndCopyMLModuleCode(string src, string refPath, string fileListFile)
        {
            IEnumerable<string> refFiles = Directory.EnumerateFiles(refPath);
            List<string> fileList = new List<string>();

            foreach (string s in refFiles)
            {
                string target = src + Path.DirectorySeparatorChar + Path.GetFileName(s);
                if (!File.Exists(target)) { fileList.Add(s); }
            }

            File.WriteAllLines(fileListFile, fileList.ToArray());
        }

        public static void CompareAndDeleteCore(string src, string refPath)
        {
            IEnumerable<string> refFiles = Directory.EnumerateFiles(refPath);
            foreach (string file in refFiles)
            {
                string fName = Path.GetFileName(file);
                string cPath = src + Path.DirectorySeparatorChar + fName;
                if (File.Exists(cPath))
                {
                    Console.WriteLine(String.Format("{0} is a duplicate", cPath));
                    File.Delete(cPath);
                }
            }
        }
    }
}
