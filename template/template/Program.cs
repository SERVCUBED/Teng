﻿using System;
using System.Diagnostics;
using System.IO;

namespace templategen
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Console.Title = "Template Engine";
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
                    else if (arg.StartsWith("-output="))
                        outputDirectory = arg.Substring(8);
                    else if (arg.StartsWith("-dir="))
                        workingDirectory = arg.Substring(5);
                    else if (arg.StartsWith("-format="))
                        fileFormat = arg.Substring(8);
                    else if (arg == "-clean")
                        shouldCleanOutput = true;
                    else if (arg == "-nominify")
                        shouldMinify = false;
                    else if (arg == "-singlethread")
                        useMultipleThreads = false;
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
        }
    }
}
