using System;
using System.Collections.Generic;
using System.IO;
using TemplateEngine;

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
                bool useMultipleThreads = true;
                List<string> noMinify = new List<string>();
                Dictionary<string, string> fileFormats = new Dictionary<string, string>();

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
                    else if (arg == "-clean")
                        shouldCleanOutput = true;
                    else if (arg == "-singlethread")
                        useMultipleThreads = false;
                    else if (File.Exists(arg))
                    {
                        try
                        {
                            var contents = File.ReadAllText(arg);
                            var split = contents.Split('\n');
                            if (split.Length >= 3)
                            {
                                workingDirectory = arg.Substring(0, arg.LastIndexOf(Path.DirectorySeparatorChar) + 1);
                                if (workingDirectory == String.Empty)
                                    workingDirectory = ".".AddDirectorySeparatorChar();

                                var path = split[0].Replace('\r', '\\');
                                if (Path.IsPathRooted(path))
                                    outputDirectory = path.AddDirectorySeparatorChar();
                                else
                                    outputDirectory = workingDirectory.AddDirectorySeparatorChar() + path.AddDirectorySeparatorChar();
                                shouldGen = split[1].Contains("True");
                                shouldCleanOutput = split[2].Contains("True");
                            }
                            else
                                Console.WriteLine("Entries missing from file. Ignoring file.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(@"Error when loading file: " + ex.Message + Environment.NewLine +
                                              ex.StackTrace);
                            return;
                        }
                    }
                    else
                        Console.WriteLine("Invalid argument: " + arg);
                }

                // Get file formats from file
                var ffPath = workingDirectory + "fileformats";
                if (File.Exists(ffPath))
                {
                    Console.WriteLine("Using fileformats file");
                    try
                    {
                        var t = File.ReadAllText(ffPath);
                        var lines = t.Split('\n');
                        foreach (var line in lines)
                        {
                            var items = line.Split(':');

                            if (items.Length < 2)
                                continue;

                            for (int i = 1; i < items.Length; i++)
                            {
                                if (StripNewlineChars(items[i]).Trim() == "nomin")
                                    noMinify.Add(items[0]);
                                else
                                    fileFormats.Add(items[0], StripNewlineChars(items[i]).Trim());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(@"Error when loading fileformats file: " + ex.Message + Environment.NewLine +
                                          ex.StackTrace);
                        return;
                    }
                }

                Console.WriteLine("Starting template engine...");
                var engine = new Engine(outputDirectory, workingDirectory, noMinify, fileFormats, useMultipleThreads);

                var verifyResult = engine.VerifyArgs();
                if (verifyResult != null)
                {
                    Console.WriteLine(verifyResult);
                    return;
                }

                if (shouldCleanOutput)
                {
                    Console.WriteLine("Cleaning output directory...");
                    var result = engine.CleanOutput();
                    if (result != null)
                    {
                        Console.WriteLine(result);
                        Environment.Exit(0);
                    }
                    Console.WriteLine("Done!");
                }

                if (shouldGen)
                {
                    Console.WriteLine("Generating...");
                    var result = engine.Generate();
                    foreach (var item in result)
                    {
                        Console.WriteLine("> " + item);
                    }
                    Console.WriteLine("Done!");
                }
#if DEBUG
                if (!System.Diagnostics.Debugger.IsAttached) return;

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(">> Press any key");
                Console.ReadKey();
                Console.ForegroundColor = ConsoleColor.White;
#endif
            }
        }

        private static string StripNewlineChars(string s) => s.Replace("\r", "").Replace("\n", "").Trim();

        private static void ShowHelp()
        {
            Console.WriteLine("Template Engine");
            Console.WriteLine("Usage:");
            Console.WriteLine("    -generate -g -gen    Generate output from template");
            Console.WriteLine("    -output=<value>      Set the output directory. Default is ./output/");
            Console.WriteLine("    -dir=<value>         Set the working directory. Default is ./");
            Console.WriteLine("    -clean               Clear the output directory before generation");
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
