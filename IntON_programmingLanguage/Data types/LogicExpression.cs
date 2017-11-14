using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntON_programmingLanguage
{ 
    class LogicExpression : ParsingUnit
    {
        private Queue<Token> logicInfix;
        private Stack<Token> logicPostfix;
        private Stack<Token> operators;
        private Stack<Token> output = new Stack<Token>();
        private CodeBlock.VarGetter getVar;

        public LogicExpression(Queue<Token> expr)
        {
            logicInfix = expr;
            logicPostfix = new Stack<Token>();
            operators = new Stack<Token>();
            ConvertToPostfix();
        }

        public void SetDelegate(CodeBlock.VarGetter getter)
        {
            getVar = getter;
        }

        private void ConvertToPostfix()
        {
            while (logicInfix.Count != 0)
            {
                Token temp = logicInfix.Dequeue();

                if (temp.Type == Token_type.NUMBER || temp.Type == Token_type.VARIABLE)
                {
                    logicPostfix.Push(temp);
                }
                else if (temp.Type == Token_type.OPEN_PARENTHESIS)
                {
                    operators.Push(temp);
                }
                else if (temp.Type == Token_type.CLOSE_PARANTHESIS)
                {
                    while (operators.Count >= 1 && operators.Peek().Type != Token_type.OPEN_PARENTHESIS)
                    {
                        logicPostfix.Push(operators.Pop());
                    }
                    if (operators.Peek().Type == Token_type.OPEN_PARENTHESIS)
                    {
                        operators.Pop();
                    }
                    else
                    {
                        throw new Exception("Expression Syntax error");
                    }
                }
                else
                {
                    while (operators.Count >= 1)
                    {
                        logicPostfix.Push(operators.Pop());
                    }
                    operators.Push(temp);
                }
            }

            while (operators.Count != 0)
            {
                logicPostfix.Push(operators.Pop());
            }
        }

        public double Evaluate()
        {
            Stack<Token> functionListCopy = new Stack<Token>(logicPostfix);
            functionListCopy.Reverse();

            while (functionListCopy.Count != 0)
            {
                Token temp = functionListCopy.Pop();
                if (temp.Type == Token_type.NUMBER)
                {
                    output.Push(temp);
                }
                else if (temp.Type == Token_type.VARIABLE)
                {
                    output.Push(new Token(Token_type.NUMBER, getVar(temp.Id)));
                }
                else
                {
                    DoOperator(temp);
                }
            }

            return output.Pop().Value;
        }

        void DoOperator(Token opertr)
        {
            double rVal = output.Pop().Value;
            double lVal = output.Pop().Value;

            switch (opertr.Type)
            {
                case Token_type.PLUS:
                    output.Push(new Token(Token_type.NUMBER, rVal + lVal));
                    break;
                case Token_type.MINUS:
                    output.Push(new Token(Token_type.NUMBER, lVal - rVal));
                    break;
                case Token_type.MULTIPLY:
                    output.Push(new Token(Token_type.NUMBER, lVal * rVal));
                    break;
                case Token_type.DIVIDE:
                    if (rVal == 0)
                    {
                        throw new DivideByZeroException("Zero division error");
                    }
                    output.Push(new Token(Token_type.NUMBER, lVal / rVal));
                    break;
                default:
                    throw new Exception("MATH EXPRESSION ERROR");
            }

        }
    }
}
