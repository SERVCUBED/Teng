using System;
using System.Diagnostics;
using System.IO;

namespace templategen
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Console.Title = "Teng Template Engine";
            if (args.Length == 0)
                ShowHelp();
            else
            {
                bool shouldGen = false;
                string workingDirectory = Environment.CurrentDirectory + Path.DirectorySeparatorChar;
                string outputDirectory = workingDirectory + "output" + Path.DirectorySeparatorChar;
                bool shouldCleanOutput = false;
                bool shouldMinify = true;
                bool useMultipleThreads = true;
                string fileFormat = "%n.html";

                foreach (var arg in args)
                {
                    if (arg == "-generate" || arg == "-g" || arg == "-gen")
                        shouldGen = true;
                    else if (arg.StartsWith("-dir="))
                    {
                        workingDirectory = arg.Substring(5).AddDirectorySeparatorChar();
                        outputDirectory = workingDirectory + "output" + Path.DirectorySeparatorChar;
                    }
                    else if (arg.StartsWith("-output="))
                        outputDirectory = arg.Substring(8).AddDirectorySeparatorChar();
                    else if (arg.StartsWith("-format="))
                        fileFormat = arg.Substring(8);
                    else if (arg == "-clean")
                        shouldCleanOutput = true;
                    else if (arg == "-nominify")
                        shouldMinify = false;
                    else if (arg == "-singlethread")
                        useMultipleThreads = false;
                    else if (File.Exists(arg))
                    {
                        try
                        {
                            var contents = File.ReadAllText(arg);
                            var split = contents.Split('\n');
                            if (split.Length >= 5)
                            {
                                workingDirectory = arg.Substring(0, arg.LastIndexOf(Path.DirectorySeparatorChar) + 1);

                                var path = split[0].Replace('\r', '\\');
                                if (Path.IsPathRooted(path))
                                    outputDirectory = path;
                                else
                                    outputDirectory = workingDirectory.AddDirectorySeparatorChar() + path;
                                shouldGen = split[1].Contains("True");
                                shouldCleanOutput = split[2].Contains("True");
                                shouldMinify = split[3].Contains("True");
                                fileFormat = split[4].Contains("null") ? "%n.html" : split[4];
                            }
                            else
                                Console.WriteLine("Entries missing from file. Ignoring file.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(@"Error when loading file: " + ex.Message + Environment.NewLine + ex.StackTrace);
                            return;
                        }
                    }
                    else
                        Console.WriteLine("Invalid argument: " + arg);
                }

                Console.WriteLine("Starting template engine...");
                var engine = new TemplateEngine(outputDirectory, workingDirectory, shouldMinify, fileFormat, useMultipleThreads);

                if (!engine.VerifyArgs())
                    return;

                if (shouldCleanOutput)
                {
                    Console.WriteLine("Cleaning output directory...");
                    engine.CleanOutput();
                    Console.WriteLine("Done!");
                }

                if (shouldGen)
                {
                    Console.WriteLine("Generating...");
                    engine.Generate();
                    Console.WriteLine("Done!");
                }
#if DEBUG
                if (!Debugger.IsAttached) return;

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(">> Press any key");
                Console.ReadKey();
                Console.ForegroundColor = ConsoleColor.White;
#endif
            }
        }

        private static void ShowHelp()
        {
            Console.WriteLine("Template Engine");
            Console.WriteLine("Usage:");
            Console.WriteLine("    -generate -g -gen    Generate output from template");
            Console.WriteLine("    -output=<value>      Set the output directory. Default is ./output/");
            Console.WriteLine("    -dir=<value>         Set the working directory. Default is ./");
            Console.WriteLine("    -format=<value>      Set the output file format. Default is %n.html");
            Console.WriteLine("    -clean               Clear the output directory before generation");
            Console.WriteLine("    -nominify            Do not minify the output files");
            Console.WriteLine("    -singlethread        Disable multithreading. Use on single-core machines");
            Console.WriteLine("    <filename>           Loads values from a project file. Use other values after to overwrite ones in this file.");
        }
    }

    public static class Extensions
    {
        public static string AddDirectorySeparatorChar(this string path)
        {
            if (path.EndsWith(Path.DirectorySeparatorChar.ToString()))
                return path;
            return path + Path.DirectorySeparatorChar;
        }
    }
}
