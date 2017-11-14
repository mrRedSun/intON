using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntON_programmingLanguage
{
    class MathExpression : ParsingUnit, ICalculatable
    {
        private Queue<Token> functionInfix;
        private Stack<Token> functionPostfix;
        private Stack<Token> operators;
        private Stack<Token> output = new Stack<Token>();
        private CodeBlock.VarGetter getVar;

        public MathExpression(Queue<Token> expr)
        {
            functionInfix = expr;
            functionPostfix = new Stack<Token>();
            operators = new Stack<Token>();
            ConvertToPostfix();
        }

        public void SetDelegate(CodeBlock.VarGetter getter)
        {
            getVar = getter;
        }

        private void ConvertToPostfix()
        {
            while (functionInfix.Count != 0)
            {
                Token temp = functionInfix.Dequeue();

                if (temp.Type == Token_type.NUMBER || temp.Type == Token_type.ID)
                {
                    functionPostfix.Push(temp);
                }
                else if (temp.Type == Token_type.OPEN_PARENTHESIS)
                {
                    operators.Push(temp);
                }
                else if (temp.Type == Token_type.CLOSE_PARANTHESIS)
                {
                    while (operators.Count >= 1 && operators.Peek().Type != Token_type.OPEN_PARENTHESIS)
                    {
                        functionPostfix.Push(operators.Pop());
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
                    while (operators.Count >= 1 &&
                        GetPrecedence(temp) <= GetPrecedence(operators.Peek()))
                    {
                        functionPostfix.Push(operators.Pop());
                    }
                    operators.Push(temp);
                }
            }

            while (operators.Count != 0)
            {
                functionPostfix.Push(operators.Pop());
            }
        }

        public Token Evaluate()
        {
            Stack<Token> functionListCopy = new Stack<Token>(functionPostfix);
            functionListCopy.Reverse();

            while (functionListCopy.Count != 0)
            {
                Token temp = functionListCopy.Pop();
                if (temp.Type == Token_type.NUMBER)
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
                case Token_type.PLUS:
                    output.Push(new Token(Token_type.NUMBER, rVal + lVal));
                    break;
                case Token_type.MINUS:
                    output.Push(new Token(Token_type.NUMBER, lVal - rVal));
                    break;
                case Token_type.MULTIPLY:
                    output.Push(new Token(Token_type.NUMBER, lVal * rVal));
                    break;
                case  Token_type.DIVIDE:
                    if (rVal == 0)
                    {
                        throw new DivideByZeroException("Zero division error");
                    }
                    output.Push(new Token(Token_type.NUMBER, lVal / rVal));
                    break;
                default:
                    throw  new Exception("MATH EXPRESSION ERROR");
            }
        
        }


        private int GetPrecedence(Token t)
        {
            switch (t.Type)
            {
                case Token_type.PLUS:
                case Token_type.MINUS:
                    return 1;
                case Token_type.MULTIPLY:
                case Token_type.DIVIDE:
                    return 2;
                case Token_type.OPEN_PARENTHESIS:
                case Token_type.CLOSE_PARANTHESIS:
                    return 0;
            }
            return -1;
        }
    }
}
