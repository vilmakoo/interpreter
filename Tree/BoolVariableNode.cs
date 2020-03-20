using System;

namespace interpreter
{
    public class BoolVariableNode : IExpressionNode
    {
        private String Name;
        private bool Value;
        private bool ValueSet;

        public BoolVariableNode(String name)
        {
            Name = name;
            ValueSet = false;
        }

        public new int GetType()
        {
            return IExpressionNode.BOOL_VARIABLE_NODE;
        }

        public void SetValue(bool value)
        {
            Value = value;
            ValueSet = true;
        }
    }
}