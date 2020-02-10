using System;
using System.Collections.Generic;

namespace interpreter
{
    public class Token
    {
        public Token(string value, int line, int col) {
            Value = value;
            Location = new Dictionary<string, int>();
            Location.Add("line", line);
            Location.Add("col", col);
        }

        public string Value { get; }
        public Dictionary<string, int> Location { get; }
    }
}