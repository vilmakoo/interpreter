using System;

namespace interpreter
{
    public class IntNode : IExpressionNode
    {
        private int Value;

        public IntNode(int value)
        {
            Value = value;
        }

        public int GetValue()
        {
            return Value;
        }

        public new int GetType()
        {
            return IExpressionNode.INT_NODE;
        }
    }
}