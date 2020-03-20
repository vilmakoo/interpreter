using System;

namespace interpreter
{
    public class IntVariableNode : IExpressionNode
    {
        private String Name;
        private int Value;
        private bool ValueSet;

        public IntVariableNode(String name)
        {
            Name = name;
            ValueSet = false;
        }

        public new int GetType()
        {
            return IExpressionNode.INT_VARIABLE_NODE;
        }

        public void SetValue(int value)
        {
            Value = value;
            ValueSet = true;
        }
    }
}