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
                        parsingStack.Push(currentToken);
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
                    parsingStack.Push(currentToken);
                }
                parsingStack.Pop(); // Getting rid of semicolon;

                CompleteState();
            }

            Pack();
        }

        public CodeBlock GetProgram()
        {
            return (CodeBlock) parsingStack.Pop();
        }

        private void Pack()
        {
            var statements = new List<IExecutable>();
            IExecutable temp;

            while (parsingStack.Count != 0)
            {
                temp = (IExecutable) parsingStack.Pop();
                statements.Add(temp);
            }

            statements.Reverse();

            CodeBlock programm = new CodeBlock(statements);

            parsingStack.Push(programm);
        }

        private void CompleteState()
        {
            switch (currentState)
            {
                case ParserState.VARIABLE_DECLARATION:
                    ReduceVariable();
                    break;
                case ParserState.PRINT_EXPRESSION:
                    ReducePrint();
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


            bool isLogic = false;
            do
            {
                temp = (Token)parsingStack.Pop(); if (temp.Type == Token_type.ASSIGN) break; // getting rid of '=' sign
                Console.WriteLine($"Tuda-suda {temp.Type}");
                if (temp.Type == Token_type.GREATER_THAN || temp.Type == Token_type.LESS_THAN
                    || Token_type.EQUAL == temp.Type || Token_type.NOT_EQUAL == temp.Type)
                {
                    isLogic = true;
                }


                exprQ.Enqueue(temp);
                
                
            } while (temp.Type != Token_type.ASSIGN);

            exprQ = new Queue<Token>(exprQ.Reverse());

            ICalculatable expr;

            if (isLogic)
            {
                expr = new LogicExpression(exprQ);
            }
            else
            {
                expr = new MathExpression(exprQ);
            }
            string id = ((Token)parsingStack.Pop()).Id;

            var declaration = new VariableDeclaration(id, expr);

            parsingStack.Push(declaration);
        }

        private void ReducePrint()
        {
            Token temp;
            var exprQ = new Queue<Token>();

            bool isLogic = false;
            
            do
            {
                temp = (Token)parsingStack.Pop(); if (temp.Type == Token_type.PRINT) break;

                Console.WriteLine($"Tuda-suda {temp.Type}"); // TODO: DELETE

                if (temp.Type == Token_type.GREATER_THAN || temp.Type == Token_type.LESS_THAN
                    || Token_type.EQUAL == temp.Type || Token_type.NOT_EQUAL == temp.Type)
                {
                    isLogic = true;
                }


                exprQ.Enqueue(temp);


            } while (temp.Type != Token_type.PRINT);

            exprQ = new Queue<Token>(exprQ.Reverse());

            ICalculatable expr;

            if (isLogic)
            {
                expr = new LogicExpression(exprQ);
            }
            else
            {
                expr = new MathExpression(exprQ);
            }


            var print = new PrintExpression(expr);

            parsingStack.Push(print);
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
