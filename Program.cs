using System;

namespace interpreter
{
    class Program
    {
        static string[] SourceCode;
        static void Main(string[] args)
        {
            ReadSourceCode(args[0]);

            Interpreter Interpreter = new Interpreter(SourceCode);
            Interpreter.Interpret();
        }

        static void ReadSourceCode(string file)
        {
            SourceCode = System.IO.File.ReadAllLines("./ExamplePrograms/" + file);
            // foreach (String line in SourceCode)
            // {
            //     Console.WriteLine(line);
            // }
        }
    }
}
