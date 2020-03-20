using System;

namespace interpreter
{
    public class StringNode : IExpressionNode
    {
        private String Value;

        public StringNode(String value)
        {
            Value = value;
        }

        public String GetValue()
        {
            return Value;
        }

        public new int GetType()
        {
            return IExpressionNode.STRING_NODE;
        }
    }
}