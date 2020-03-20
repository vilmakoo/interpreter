using System;

namespace interpreter
{
    public interface IExpressionNode
    {
        public static int STRING_NODE = 1;
        public static int INT_NODE = 2;
        public static int BOOL_NODE = 3;
        public static int STRING_VARIABLE_NODE = 4;
        public static int INT_VARIABLE_NODE = 5;
        public static int BOOL_VARIABLE_NODE = 6;

        public int GetType();
        // public GetValue();
    }
}