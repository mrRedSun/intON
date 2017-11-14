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

                while (currentToken.Type != Token_type.SEMICOLON)
                {
                    currentToken = tokenStream.GetToken();
                }
                tokenStream.GetToken();

                CompleteState();
            }
        }

        private void CompleteState()
        {
            switch (currentState)
            {
                case ParserState.VARIABLE_DECLARATION:
                    ReduceVariable();
                    break;
                case ParserState.PRINT_EXPRESSION:
                    break;
                case ParserState.IF_STATEMENT:
                    break;
                case ParserState.WHILE_LOOP:
                    break;
                default:
                    break;
            }
        }

        private void ReduceVariable()
        {
            Token temp;
            var exprQ = new Queue<Token>();

            do
            {
                temp = (Token)parsingStack.Pop();
                if (temp.Type == Token_type.TRUE)
                {
                    
                }


                exprQ.Enqueue(temp);
                
                
            } while (temp.Type != Token_type.ASSIGN);

            exprQ.Reverse();

            var expr = new MathExpression(exprQ);
            string id = ((Token)parsingStack.Pop()).Id;

            var declaration = new VariableDeclaration(id, expr);

            parsingStack.Push(declaration);
        }

        private void ReducePrint()
        {

        }

        private void ReduceLogic()
        {

        }

        private void ReduceMath()
        {

        }

        private void ReduceIf()
        {

        }

        private void ReduceWhile()
        {

        }

        private void ReduceCodeBlock()
        {

        }
    }
}
