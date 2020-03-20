using System;

namespace interpreter
{
    public class BoolNode : IExpressionNode
    {
        private bool Value;

        public BoolNode(bool value)
        {
            Value = value;
        }

        public bool GetValue()
        {
            return Value;
        }

        public new int GetType()
        {
            return IExpressionNode.BOOL_NODE;
        }
    }
}