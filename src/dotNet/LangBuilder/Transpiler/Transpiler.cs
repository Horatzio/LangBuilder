﻿using System;
using System.IO;
using Antlr4.Runtime;

namespace Transpiler
{
    public static class Transpiler
    {
        private static class Options
        {
            public const string HelpOption = "-h";
            public const string ConsoleOption = "-c";
            public const string InputFileOption = "-i";
            public const string OutputFileOption = "-o";
        }

        // TODO: google how to load at compile time a const text
        private const string HelpText = "";

        public static void Main(string[] args)
        {
            if (args.Length >= 1 && args[0] == Options.HelpOption)
            {
                Console.WriteLine(HelpText);
                return;
            }


            string inputFilePath = null;
            string outputFilePath = null;
            var sourceText = "";
            bool isConsole = false;

            for (var i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case Options.ConsoleOption:
                        isConsole = true;
                        break;
                    case Options.InputFileOption:
                        inputFilePath = args[i + 1];
                        i++;
                        break;

                    case Options.OutputFileOption:
                        outputFilePath = args[i + 1];
                        i++;
                        break;
                }
            }

            if (isConsole)
            {
                sourceText = Console.In.ReadToEnd();
            }
            if (inputFilePath != null)
            {
                if (!File.Exists(inputFilePath))
                {
                    Console.Error.WriteLine($"{inputFilePath} does not exist");
                    return;
                }

                try
                {
                    sourceText = File.ReadAllText(inputFilePath);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine($"Error reading from {inputFilePath}: {e.Message}");
                    throw;

                }
            }
            else
            {
                Console.Error.WriteLine("No input file path provided. Please provide one after the '-i' option");
            }

            var output = Translate(sourceText);

            if (outputFilePath != null)
            {
                try
                {
                    File.WriteAllText(outputFilePath, output);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine($"Error writing to {outputFilePath}: {e.Message}");
                }
            }
            else
            {
                Console.Write(output);
            }
        }

        private static string Translate(string text)
        {
            var inputStream = new AntlrInputStream(text);
            var lexer = new TranspilerGrammarLexer(inputStream);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new TranspilerGrammarParser(tokenStream);

            var visitor = new TranspilerGrammarConcreteVisitor();

            var p = parser.program();

            var result = visitor.Visit(p);
            return result;
        }
    }
}
