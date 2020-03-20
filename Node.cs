using System;
using System.Collections.Generic;

namespace interpreter
{
    public class Node
    {
        public String Type;
        public Token Value;
        public Node Parent;
        public List<Node> Children;

        public Node(String type, Token value, Node parent)
        {
            Type = type;
            Value = value;
            Parent = parent;
            Children = new List<Node>();
        }

        public void AddChild(Node child)
        {
            Children.Add(child);
        }

        public void PrintPretty(string indent, bool last)
        {
            Console.Write(indent);
            if (last)
            {
                Console.Write("\\-");
                indent += "  ";
            }
            else
            {
                Console.Write("|-");
                indent += "| ";
            }

            if (Value != null)
            {
                Console.WriteLine(Value.Value);
            }
            else
            {
                Console.WriteLine(Type);
            }

            for (int i = 0; i < Children.Count; i++)
                Children[i].PrintPretty(indent, i == Children.Count - 1);
        }
    }
}