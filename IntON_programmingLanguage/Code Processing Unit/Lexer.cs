using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntON_programmingLanguage
{


    class Lexer
    {

        /// <summary>
        /// Contains all the keywords of language. Can be changed for syntax changes. 
        /// Should stay sorted for parser to work. Indexes match those in enum with types
        /// </summary>
        private readonly string[] keywords = 
        {
          //   0         1        2       3        4       5
            "false",  "print", "while", "var ", "true",  "if",
        //   6     7    8    9    10   11    12   13   14    15  16
            "==", "!=", "+", "-", "*", "/", "=", ">", "<",  "(", ")",
         //  17   18  19
            ";", "{", "}"
        };
        


        private string sourceCode; // string with source code 
        private List<Token> tokenList; // Container for all the tokens

        /// <summary>
        /// Takes source code as an argument, creates object that contains list of Token objects, generated for a code
        /// </summary>
        /// <param name="source"> Sting with source code </param>
        public Lexer(string source)
        {
            tokenList = new List<Token>();
            sourceCode = source.Trim();
            Tokenize();
        }



        private void Tokenize() // Parses code and creates
        {
            var stringStream = new StringStream(sourceCode);

            while (!stringStream.Eof())
            {
                char current = stringStream.Get();

                while (!stringStream.Eof() && (current == ' ' || current == '\n' || current == '\t')) // skips all the whitespace
                    current = stringStream.Get();

                if ('0' <= current && current <= '9') // check if current character is a digit
                {
                    stringStream.Putback();
                    tokenList.Add(new Token(Token_type.NUMBER, stringStream.GetNumber()));
                    continue; // continue seeking for tokens, ignore further code
                }

                bool isFound = false;

                for (int i = 0; i < keywords.Length; i++) // checks if current character is a start of some of operators
                {
                    if (current == keywords[i][0]) // checks if current character mathes first character of current keyword
                    {
                        stringStream.Putback(); // shift stringstream one character to the left, for slice funcitons to work properly


                        if (stringStream.GetCharsLeft() >= keywords[i].Length && // Checks if the stream can provide enough characters for slice
                            stringStream.PeekSlice(keywords[i].Length) == keywords[i]) // Checks if slice matches current keyword
                        {
                            stringStream.Shift(keywords[i].Length);
                            tokenList.Add(new Token((Token_type)i)); // adds token of current keyword type to the end of a list
                            Console.WriteLine(keywords[i]);
                            isFound = true;
                            break;
                        }
                        else
                        {
                            stringStream.Get(); // to omit infinite loop, get character back from stream
                        }
                    }
                }
                if (isFound == true) continue; // if the token is found, skip further code and continue searching


                // ---------- if current character isn't one of keywords or number, check if it's a keyword
                string id = "";


                while ((current >= 'A' && current <= 'Z') || // keywords should only contain letters for A to z
                       (current >= 'a' && current <= 'z'))
                {
                    id += current; // add character to id string
                    if (!stringStream.Eof() && 
                        ((stringStream.Peek() >= 'A' && stringStream.Peek() <= 'Z') || 
                       (stringStream.Peek() >= 'a' && stringStream.Peek() <= 'z'))) current = stringStream.Get(); 
                    else break; // break, if the stream has ended
                }

                //stringStream.Putback();

                if (id != "") tokenList.Add(new Token(Token_type.ID, id)); // checks, if any characters were added to a string
                else tokenList.Add(new Token(Token_type.INVALID_TOKEN, current)); // if not, create now token with id of Invaild character
            }

            foreach(Token t in tokenList) {
                Console.Write($"{t.Type} ");
            }


        }

        /// <summary>
        /// Parameter returns list of tokens for given source code
        /// </summary>
        public List<Token> GetList
        {
            get
            {
                return tokenList;
            }
        }
    }


}
