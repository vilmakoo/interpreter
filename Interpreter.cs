using System;
using System.Collections.Generic;

namespace interpreter
{
    public class Interpreter
    {
        public string[] InputLines;
        public List<Token> Tokens;
        public char[] oneCharTokens = {'(', ')', '[', ']', ',', ';', '+', '-', ':', '.'};


        public Interpreter(string[] input)
        {
            InputLines = input;
            Tokens = new List<Token>();
        }

        public void Interpret()
        {
            Scan();
            foreach (Token t in Tokens)
            {
                Console.WriteLine(t);
            }
        }

        public void Scan()
        {
            for (int i = 0; i < InputLines.Length; i++)
            { //todo käsittele alun välilyönnit?? käsittele kommentit
                String Line = InputLines[i];
                String Token = "";
                
                int j = 0;
                while (j <= Line.Length)
                {
                    char c;
                    try
                    {
                        c = Line[j];
                    }
                    catch (System.Exception)
                    {
                        if (Token.Length > 0)
                        {
                            Tokens.Add(new Token(Token, Tag(Token)));
                        }                        
                        break;
                    }

                    if (c == '"') // string
                    {
                        String StringToken = "" + c;
                        j++;
                        while (Line[j] != '"')
                        {
                            StringToken += Line[j];
                            j++;
                        }
                        Tokens.Add(new Token(StringToken + '"', "string"));
                    }
                    else if (Array.Exists(oneCharTokens, e => e == c)) // one char token
                    {
                        if (c == ':')
                        {
                            if (Line[j + 1] == '=')
                            {
                                Tokens.Add(new Token(":=", "assignment"));
                                j += 2;
                                continue;
                            }
                        }
                        if (c == '.')
                        {
                            if (Line[j + 1] == '.') {
                                Tokens.Add(new Token(Token, Tag(Token)));
                                Tokens.Add(new Token("..", "range"));
                                Token = "";
                                j += 2;
                                continue;
                            }
                        }
                        if (Token.Length > 0)
                        {
                            Tokens.Add(new Token(Token, Tag(Token)));
                        }
                        Tokens.Add(new Token(c.ToString(), Tag(c.ToString())));
                        Token = "";
                    }
                    else if (c != ' ') // tai se ei oo tab tai rivinvaihto // "normal" character
                    {
                        Token += c;
                    } 
                    else if (Token.Length > 0)
                    {
                        Tokens.Add(new Token(Token, Tag(Token)));
                        Token = "";
                    }
                    j++;
                }
            }
        }

        public String Tag(String Token)
        {
            // var_ident, type, int, string, op, unary_op, var, for, in, do, end, read, print, assert

            String[] types = {"int", "string", "bool"};
            String[] reserved = {"var", "for", "in", "do", "end", "read", "print", "assert"};
            String[] operators = {"+", "-", "*", "/", "&", "=", "<"};
            String unary_op = "!";

            if (Array.Exists(operators, o => o == Token)) {
                return "op";
            }
            else if (Token == unary_op) {
                return "unary_op";
            }
            else if (Array.Exists(oneCharTokens, e => e.ToString() == Token) || Token == '"'.ToString()) // one char token
            {
                return Token;
            }
            else if (Array.Exists(types, t => t == Token))
            {
                return "type";
            }
            else if (Array.Exists(reserved, e => e == Token)) {
                return Token;
            }
            else if (IsInt(Token))
            {
                return "int";
            }
            else
            {
                return "var_ident";
            }
        }

        public bool IsInt(String Token) {
            String[] digits = {"0", "1", "2", "3", "3", "5", "6", "7", "8", "9"};
            foreach (char c in Token)
            {
                if (!Array.Exists(digits, d => d == Token))
                {
                    return false;
                }
            }
            return true;
        }
    }
}

