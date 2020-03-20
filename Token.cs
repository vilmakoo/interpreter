using System;
using System.Collections.Generic;

namespace interpreter
{
    public class Token
    {
        public Token(String value, String tag)
        {
            Value = value;
            Tag = tag;
        }

        public string Value { get; }
        // public Dictionary<string, int> Location { get; }
        public string Tag { get; }

        public override String ToString()
        {
            return Tag + ", value: " + Value;
        }
    }
}