using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntON_programmingLanguage
{
    /// <summary>
    /// Represents set of possible parser states.
    /// </summary>
    enum ParserState
    {
        WAIT,

        VARIABLE_DECLARATION, VARIABLE_REASSIGNMENT,
        PRINT_EXPRESSION, IF_STATEMENT, WHILE_LOOP
    }

    
    class Parser
    {
        private TokenStream tokenStream;
        private ParserState currentState;
        private Stack<ParsingUnit> parsingStack;

        /// <summary>
        /// Takes list of Tokens as an argument and parses it into CodeBlock program
        /// </summary>
        /// <param name="list"></param>
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

                switch (currentToken.Type) // sets parser's state to the one, current token represents
                {
                    case Token_type.VARIABLE:
                        currentState = ParserState.VARIABLE_DECLARATION;
                        break;
                    case Token_type.IF:
                        parsingStack.Push(currentToken);
                        currentState = ParserState.IF_STATEMENT;
                        break;
                    case Token_type.PRINT:
                        parsingStack.Push(currentToken);
                        currentState = ParserState.PRINT_EXPRESSION;
                        break;
                    case Token_type.WHILE:
                        parsingStack.Push(currentToken);
                        currentState = ParserState.WHILE_LOOP;
                        break;
                    case Token_type.ID:
                        parsingStack.Push(currentToken);
                        currentState = ParserState.VARIABLE_REASSIGNMENT;
                        break;
                    default:
                        throw new Exception("SYNTAX ERROR"); // if current token.Type is none of the above, throw SYNTAX error
                }

               

                CompleteState(); // call all required functions for executable unit reducing
            }

            Pack(); // create a new CodeBlock object with all reduced executable units
        }

        /// <summary>
        /// Returns result of parsing
        /// </summary>
        /// <returns> returns CodeBlock object, that parser created</returns>
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

            statements.Reverse(); // Stack reverses all Executables, so reversing back is required

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
                    ReduceIf();
                    break;
                case ParserState.WHILE_LOOP:
                    ReduceWhile();
                    break;
                case ParserState.VARIABLE_REASSIGNMENT:
                    ReduceReassignment();
                    break;
                default:
                    break;
            }
        }

        private void ReduceReassignment()
        {
            Token currentToken;
            do
            {
                currentToken = tokenStream.GetToken();
                parsingStack.Push(currentToken);
            } while (currentToken.Type != Token_type.SEMICOLON);
            parsingStack.Pop(); // Getting rid of semicolon;

            ICalculatable expr = ReduceExpression(); 
            string id = ((Token)parsingStack.Pop()).Id;

            var reassignment = new VariableReassignment(id, expr);

            parsingStack.Push(reassignment);
        }

        private void ReduceVariable()
        {
            Token currentToken;
            do
            {
                currentToken = tokenStream.GetToken();
                parsingStack.Push(currentToken);
            } while (currentToken.Type != Token_type.SEMICOLON);
                parsingStack.Pop(); // Getting rid of semicolon;

            ICalculatable expr = ReduceExpression();
            string id = ((Token)parsingStack.Pop()).Id;

            var declaration = new VariableDeclaration(id, expr);

            parsingStack.Push(declaration);
        }

        private void ReducePrint()
        {
            Token currentToken;
            do
            {
                currentToken = tokenStream.GetToken();
                parsingStack.Push(currentToken);
            } while (currentToken.Type != Token_type.SEMICOLON);
            parsingStack.Pop(); // Getting rid of semicolon;

            ICalculatable expr = ReduceExpression();

            var print = new PrintExpression(expr);

            parsingStack.Push(print);
        }

        private ICalculatable ReduceExpression(bool isLogic = false)
        {
            Token temp;
            var exprQ = new Queue<Token>();

            
            do
            {
                temp = (Token)parsingStack.Pop();
                if (temp.Type == Token_type.ASSIGN ||
                    temp.Type == Token_type.PRINT ||
                    temp.Type == Token_type.WHILE ||
                    temp.Type == Token_type.IF) break; // getting rid of '=' sign

                Console.WriteLine($"Tuda-suda {temp.Type}"); // TODO: REMOVE

                if (temp.Type == Token_type.GREATER_THAN ||
                    temp.Type == Token_type.LESS_THAN || 
                    Token_type.EQUAL == temp.Type || 
                    Token_type.NOT_EQUAL == temp.Type ||
                    temp.Type == Token_type.TRUE ||
                    temp.Type == Token_type.FALSE)
                {
                    isLogic = true;
                }

                exprQ.Enqueue(temp);
            } while (parsingStack.Count != 0);

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

            return expr;
        }

        private void ReduceIf()
        {
            Token temp;
            do
            {
                temp = tokenStream.GetToken(); if (temp.Type == Token_type.OPEN_BRACKET) break; // getting rid of bracket
                parsingStack.Push(temp);

            } while (!tokenStream.Eof());
            LogicExpression expr = (LogicExpression)ReduceExpression(true);

            parsingStack.Push(temp); //putting  bracket back to stack so codeBlock knows when to stop
            int bracketCount = 1; // needed to process nested blocks;
            while (bracketCount != 0 || temp.Type != Token_type.CLOSE_BRACKET)
            {
                temp = tokenStream.GetToken();

                if (temp.Type == Token_type.OPEN_BRACKET) bracketCount++;
                else if (temp.Type == Token_type.CLOSE_BRACKET) bracketCount--;

                parsingStack.Push(temp);
            }

            CodeBlock codeBlock = ReduceCodeBlock();


            parsingStack.Push(new IfStatement(expr, codeBlock));


        }

        private void ReduceWhile()
        {
            Token temp;
            do
            {
                temp = tokenStream.GetToken(); if (temp.Type == Token_type.OPEN_BRACKET) break; // getting rid of bracket
                parsingStack.Push(temp);

            } while (!tokenStream.Eof());
            LogicExpression expr = (LogicExpression)ReduceExpression(true);

            parsingStack.Push(temp); //putting  bracket back to stack so codeBlock knows when to stop
            int bracketCount = 1; // needed to process nested blocks;
            while (bracketCount != 0 || temp.Type != Token_type.CLOSE_BRACKET)
            {
                temp = tokenStream.GetToken();

                if (temp.Type == Token_type.OPEN_BRACKET) bracketCount++;
                else if (temp.Type == Token_type.CLOSE_BRACKET) bracketCount--;

                parsingStack.Push(temp);
            }

            CodeBlock codeBlock = ReduceCodeBlock();


            parsingStack.Push(new WhileStatement(expr, codeBlock));
        }

        private CodeBlock ReduceCodeBlock()
        {
            Token temp = (Token) parsingStack.Pop();
            if(temp.Type != Token_type.CLOSE_BRACKET) throw new Exception("SYNTAX ERROR");

            List<Token> codeBlock = new List<Token>();

            int bracketCount = 1; // needed to process nested blocks;
            while (parsingStack.Count != 0 && (bracketCount != 0  || temp.Type != Token_type.OPEN_BRACKET))
            {
                temp = (Token)parsingStack.Pop();

                if (temp.Type == Token_type.OPEN_BRACKET) bracketCount--;
                else if (temp.Type == Token_type.CLOSE_BRACKET) bracketCount++;
                codeBlock.Add(temp);
            } codeBlock.RemoveAt(codeBlock.Count - 1); // deleting open bracket

            codeBlock.Reverse();
            

            Parser p = new Parser(codeBlock);
            return p.GetProgram();
        }
    }
}
