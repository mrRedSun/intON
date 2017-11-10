using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntON_programmingLanguage
{
    enum ParserState
    {
        WAIT,
        VARIABLE_DECLARATION, LOGIC_EXPRESSION, MATH_EXPRESSION,
        PRINT_EXPRESSION, IF_STATEMENT, WHILE_LOOP, CODE_BLOCK
    }

    class Parser
    {
        private TokenStream tokenStream;
        private ParserState currentState;
        private Stack<ParsingUnit> parsingStack;

        public Parser(List<Token> list)
        {
            tokenStream = new TokenStream(list);
            currentState = ParserState.WAIT;
            parsingStack = new Stack<ParsingUnit>();

            Parse();
        }

        private void Parse()
        {
            while (!tokenStream.Eof())
            {
                var currentToken = tokenStream.GetToken();

                switch (currentToken.Type)
                {
                    case Token_type.VARIABLE:
                        currentState = ParserState.VARIABLE_DECLARATION;
                        break;
                    case Token_type.IF:
                        currentState = ParserState.IF_STATEMENT;
                        break;
                    case Token_type.PRINT:
                        currentState = ParserState.PRINT_EXPRESSION;
                        break;
                    case Token_type.WHILE:
                        currentState = ParserState.WHILE_LOOP;
                        break;
                    default:
                        throw new Exception("SYNTAX ERROR");
                }

                parsingStack.Push(currentToken);


            }

        }
    }

    abstract class StatementBase : ParsingUnit
    {
        protected const Unit_type uType = Unit_type.TERMINAL;
        public override Unit_type UnitType { get { return uType; } }

        virtual public void Run() { }
        
    }

    class CodeBlock : StatementBase
    {

    }

    class VariableDeclaration : StatementBase
    {

    }

    class IfStatement : StatementBase
    {

    }


    class MathExpression : StatementBase
    {

    }

    class LogicExpression : StatementBase
    {

    }

    class PrintExpression : StatementBase
    {

    }


}
