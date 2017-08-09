using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;

namespace DotNetInspectorGadget
{
    class DotNetInspectorGadget
    {
        static void Main(string[] args)
        {
            string srcPath = @"C:\repos\IOT_Bakeoff\Simulator\EdgeContainer\iot-edge";
            string assemplyName = @"System.ComponentModel.TypeConverter";

            string assembly = @"C:\Users\jmarymee\stage\stage2\System.ComponentModel.TypeConverter.dll";

            string assemblyFile = @"C:\Users\jmarymee\Documents\Visual Studio 2017\Projects\iotedgeml\packages\Newtonsoft.Json.10.0.3\lib\net45\Newtonsoft.Json.dll";

            InspectAnAssembly(assembly);

            FindThatDependency(srcPath, assemplyName);

            ////Console.WriteLine(Directory.GetCurrentDirectory());
            //if (args.GetLength(0) < 1)
            //{
            //    Console.WriteLine("Add a single parameter that is your" +
            //    " path to the file you want inspected.");
            //    return 1;
            //}
            //try
            //{
            //    string fullPath = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + args[0];
            //    Console.WriteLine(fullPath);
            //    //var assemblies = Assembly.LoadFile(@args[0]).GetReferencedAssemblies();
            //    var assemblies = Assembly.LoadFile(fullPath).GetReferencedAssemblies();

            //    if (assemblies.GetLength(0) > 0)
            //    {
            //        foreach (var assembly in assemblies)
            //        {
            //            Console.WriteLine(assembly);
            //        }
            //        return 0;
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("An exception occurred: {0}", e.Message);
            //    return 1;
            //}
            //finally { }

            //return 1;
            Console.WriteLine("Done");
            Console.ReadLine();
        }

        public static void InspectAnAssembly(string path)
        {
            Assembly a = Assembly.LoadFile(path);
            AssemblyName[] aNames =a.GetReferencedAssemblies();
        }

        public static void FindThatDependency(string path, string assemblyName)
        {
            IEnumerable<string> dlls = Directory.EnumerateFiles(path, "*.dll");
            foreach(string s in dlls)
            {
                try
                {
                    AssemblyName[] aNames = Assembly.LoadFile(s).GetReferencedAssemblies();
                    foreach (AssemblyName name in aNames)
                    {
                        if (name.Name.Equals(assemblyName))
                        {
                            Console.WriteLine(String.Format("The requiring assembly is: {0}", Path.GetFileName(s)));
                            Console.WriteLine(name);
                            Console.WriteLine();
                        }
                    }
                }
                catch (Exception exp)
                {
                    //Console.WriteLine(exp.Message);
                }
            }

        }
    }
}
