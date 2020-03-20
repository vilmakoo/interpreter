using System;
using System.Collections.Generic;

namespace interpreter
{
    public class Interpreter
    {
        public string[] InputLines;
        public Scanner Scanner;
        public List<Token> Tokens;
        public Parser Parser;

        public Interpreter(string[] input)
        {
            InputLines = input;
            Scanner = new Scanner(InputLines);
        }

        public void Interpret()
        {
            Tokens = Scanner.DoTheScanning(); // TODO: change method name
            Parser = new Parser(Tokens);
            Parser.Parse();

        }
    }
}

