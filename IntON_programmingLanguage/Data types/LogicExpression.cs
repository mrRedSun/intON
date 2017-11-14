using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntON_programmingLanguage
{ 
    class LogicExpression : ParsingUnit, ICalculatable
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

                if (temp.Type == Token_type.NUMBER || temp.Type == Token_type.ID 
                    || temp.Type == Token_type.TRUE || temp.Type == Token_type.FALSE)
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

        public Token Evaluate()
        {
            Stack<Token> functionListCopy = new Stack<Token>(logicPostfix);
            functionListCopy.Reverse();

            while (functionListCopy.Count != 0)
            {
                Token temp = functionListCopy.Pop();
                if (temp.Type == Token_type.NUMBER || temp.Type == Token_type.FALSE || temp.Type == Token_type.TRUE)
                {
                    output.Push(temp);
                }
                else if (temp.Type == Token_type.ID)
                {
                    output.Push(new Token(Token_type.NUMBER, getVar(temp.Id)));
                }
                else
                {
                    DoOperator(temp);
                }
            }

            return output.Pop();
        }

        void DoOperator(Token opertr)
        {
            double rVal = output.Pop().Value;
            double lVal = output.Pop().Value;

            switch (opertr.Type)
            {
                case Token_type.EQUAL:
                    if (rVal == lVal) output.Push(new Token(Token_type.TRUE));
                    else output.Push(new Token(Token_type.FALSE));
                    break;
                case Token_type.LESS_THAN:
                    if (rVal > lVal) output.Push(new Token(Token_type.TRUE));
                    else output.Push(new Token(Token_type.FALSE));
                    break;
                case Token_type.GREATER_THAN:
                    if (rVal < lVal) output.Push(new Token(Token_type.TRUE));
                    else output.Push(new Token(Token_type.FALSE));
                    break;
                case Token_type.NOT_EQUAL:
                    if (rVal != lVal) output.Push(new Token(Token_type.TRUE));
                    else output.Push(new Token(Token_type.FALSE));
                    break;
                default:
                    throw new Exception("LOGIC EXPRESSION ERROR");
            }

        }
    }
}
