using System;
using System.Collections.Generic;

namespace interpreter
{
    public class Parser
    {
        public LinkedList<Token> Tokens;
        public LinkedListNode<Token> Lookahead;
        public Node RootNode;

        public Parser(List<Token> tokens)
        {
            Tokens = new LinkedList<Token>(tokens);
            Lookahead = Tokens.First;
            RootNode = new Node("<program>", null, null);
        }

        public void NextToken(Node Parent)
        {
            LinkedListNode<Token> Temp = Lookahead;

            if (Tokens.Count > 1)
            {
                Lookahead = Lookahead.Next;
                Tokens.Remove(Temp);
            }
            else
            {
                Lookahead = new LinkedListNode<Token>(new Token("", "epsilon"));
            }

            Node NewNode = new Node("<token>", Lookahead.Value, Parent);
            Parent.AddChild(NewNode);
        }

        public void Parse()
        {
            Node NewNode = new Node("<stmts>", null, RootNode);
            RootNode.AddChild(NewNode);
            Stmts(NewNode);
            RootNode.PrintPretty("", false);
        }

        public void Stmts(Node Parent)
        {
            // <stmts> ::= <stmt> ";" <stmts>
            // <stmts> ::= "epsilon"

            Node NewNode = new Node("<stmt>", null, Parent);
            Parent.AddChild(NewNode);
            Stmt(NewNode);
            if (Lookahead.Value.Tag != ";")
            {
                throw new Exception();
            }

            // NewNode = new Node("<stmt>", null, Parent);
            // Parent.AddChild(NewNode);
            // NextToken(NewNode);
            // if (Lookahead.Value.Tag != "epsilon" && Lookahead.Value.Tag != "end")
            Console.WriteLine(Lookahead.Value);
            if (Lookahead.Value.Tag != "epsilon" && Lookahead.Next.Value.Tag != "epsilon" && Lookahead.Next.Value.Tag != "end")
            {
                NewNode = new Node("<stmt>", null, Parent);
                Parent.AddChild(NewNode);
                NextToken(NewNode);
                Stmts(Parent);
            }
            else
            {
                NextToken(Parent);
                if (Lookahead.Value.Tag != "epsilon" && Lookahead.Value.Tag != "end")
                {
                    Stmts(Parent);
                }
            }
        }

        public void Stmt(Node Parent)
        {
            if (Lookahead.Value.Tag == "var")
            {
                //  <stmt> ::= "var" <var_ident> ":" <type> <var_assgnmnt>

                NextToken(Parent);
                if (Lookahead.Value.Tag != "var_ident")
                {
                    throw new Exception();
                }
                NextToken(Parent);
                if (Lookahead.Value.Tag != ":")
                {
                    throw new Exception();
                }
                NextToken(Parent);
                if (Lookahead.Value.Tag != "type")
                {
                    throw new Exception();
                }

                NextToken(Parent);

                Node NewNode = new Node("<var_assagnmnt>", null, Parent);
                Parent.AddChild(NewNode);
                VarAssgnmnt(NewNode);
            }
            else if (Lookahead.Value.Tag == "var_ident")
            {
                //  <stmt> ::= <var_ident> ":=" <expr>

                NextToken(Parent);
                if (Lookahead.Value.Tag != "assignment")
                {
                    throw new Exception();
                }

                NextToken(Parent);

                Node NewNode = new Node("<expr>", null, Parent);
                Parent.AddChild(NewNode);
                Expr(NewNode);
            }
            else if (Lookahead.Value.Tag == "for")
            {
                //  <stmt> ::= "for" <var_ident> "in" <expr> ".." <expr> "do" <stmts> "end" "for"
                
                NextToken(Parent);
                if (Lookahead.Value.Tag != "var_ident")
                {
                    throw new Exception();
                }
                NextToken(Parent);
                if (Lookahead.Value.Tag != "in")
                {
                    throw new Exception();
                }

                NextToken(Parent);

                Node NewNode = new Node("<expr>", null, Parent);
                Parent.AddChild(NewNode);
                Expr(NewNode);

                if (Lookahead.Value.Tag != "range")
                {
                    throw new Exception();
                }

                NextToken(Parent);

                NewNode = new Node("<expr>", null, Parent);
                Parent.AddChild(NewNode);
                Expr(NewNode);

                if (Lookahead.Value.Tag != "do")
                {
                    throw new Exception();
                }

                NextToken(Parent);

                NewNode = new Node("<stmts>", null, Parent);
                Parent.AddChild(NewNode);
                Stmts(NewNode);

                if (Lookahead.Value.Tag != "end")
                {
                    throw new Exception();
                }
                NextToken(Parent);
                if (Lookahead.Value.Tag != "for")
                {
                    throw new Exception();
                }

            }
            else if (Lookahead.Value.Tag == "read")
            {
                //  <stmt> ::= "read" <var_ident>
                
                NextToken(Parent);
                if (Lookahead.Value.Tag != "var_ident")
                {
                    throw new Exception();
                }
            }
            else if (Lookahead.Value.Tag == "print")
            {
                //  <stmt> ::= "print" <expr>

                NextToken(Parent);

                Node NewNode = new Node("<expr>", null, Parent);
                Parent.AddChild(NewNode);
                Expr(NewNode);
            }
            else if (Lookahead.Value.Tag == "assert")
            {
                //  <stmt> ::= "assert" "(" <expr> ")"

                NextToken(Parent);
                if (Lookahead.Value.Tag != "(")
                {
                    throw new Exception();
                }

                NextToken(Parent);

                Node NewNode = new Node("<expr>", null, Parent);
                Parent.AddChild(NewNode);
                Expr(NewNode);

                if (Lookahead.Value.Tag != ")")
                {
                    throw new Exception();
                }
            }
            if (Lookahead.Value.Tag != ";")
            {
                NextToken(Parent);
            }
        }

        public void VarAssgnmnt(Node Parent)
        {
            if (Lookahead.Value.Tag == "assignment")
            {
                // <var_assgnmnt> ::= ":=" <expr>

                NextToken(Parent);

                Node NewNode = new Node("<expr>", null, Parent);
                Parent.AddChild(NewNode);
                Expr(NewNode);
            }
            else if (Lookahead.Value.Tag == "epsilon" || Lookahead.Value.Tag == ";")
            {
                // <var_assgnmnt> ::= "epsilon"
                // something
            } 
            else
            {
                Console.WriteLine(Lookahead.Value);
            }
        }

        public void Expr(Node Parent)
        {
            // <expr> ::= <opnd> <op_opnd>
            // <expr> :== <unary_op> <opnd>

            if (Lookahead.Value.Tag == "unary_op")
            {
                NextToken(Parent);
            }

            Node NewNode = new Node("<opnd>", null, Parent);
            Parent.AddChild(NewNode);
            Opnd(NewNode);
            NextToken(Parent);

            if (Lookahead.Value.Tag == "op") {
                NewNode = new Node("<op_opnd>", null, Parent);
                Parent.AddChild(NewNode);
                OpOpnd(NewNode);
                NextToken(Parent);
            }
        }

        public void Opnd(Node Parent)
        {
            // <opnd> ::= <int>
            //     | <string>
            //     | <var_ident>
            //     | "(" expr ")"

            if (Lookahead.Value.Tag == "int")
            {
                Node NewNode = new Node("<token>", Lookahead.Value, Parent);
                Parent.AddChild(NewNode);
            }
            else if (Lookahead.Value.Tag == "string")
            {
                Node NewNode = new Node("<token>", Lookahead.Value, Parent);
                Parent.AddChild(NewNode);
            }
            else if (Lookahead.Value.Tag == "var_ident")
            {
                Node NewNode = new Node("<token>", Lookahead.Value, Parent);
                Parent.AddChild(NewNode);
            }
            else if (Lookahead.Value.Tag == "(")
            {
                NextToken(Parent);
                Node NewNode = new Node("<expr>", null, Parent);
                Parent.AddChild(NewNode);
                Expr(NewNode);
                if (Lookahead.Value.Tag != ")")
                {
                    throw new Exception();
                }
            }
        }

        public void OpOpnd(Node Parent)
        {
            // <op_opnd>  ::= <op> <opnd>
            // <op_opnd>  ::= "epsilon"

            if (Lookahead.Value.Tag == "epsilon")
            {
                // do something
            }
            else if (Lookahead.Value.Tag == "op")
            {
                NextToken(Parent);
                Node NewNode = new Node("<opnd>>", null, Parent);
                Parent.AddChild(NewNode);
                Opnd(NewNode);
            }
        }
    }
}