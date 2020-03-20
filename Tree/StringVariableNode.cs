using System;

namespace interpreter
{
    public class StringVariableNode : IExpressionNode
    {
        private String Name;
        private String Value;
        private bool ValueSet;

        public StringVariableNode(String name)
        {
            Name = name;
            ValueSet = false;
        }

        public new int GetType()
        {
            return IExpressionNode.STRING_VARIABLE_NODE;
        }

        public void SetValue(String value)
        {
            Value = value;
            ValueSet = true;
        }
    }
}