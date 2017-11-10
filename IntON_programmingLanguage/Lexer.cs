using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IntON_programmingLanguage
{


    class Lexer
    {
        private readonly string[] keywords = // Array contains all the keywords reserved by language. Should be sorted by length
        {
          //   0         1        2       3        4       5
            "false",  "print", "while", "var ", "true",  "if",
        //   6     7    8    9    10   11    12   13   14    15  16
            "==", "!=", "+", "-", "*", "/", "=", ">", "<",  "(", ")",
         //  17    18    19
            ";", "{", "}"
        };
        

        private string sourceCode;
        private List<Token> tokenList;

        public Lexer(string source)
        {
            tokenList = new List<Token>();
            sourceCode = source;
            Parse();
        }

        private void Parse()
        {
            StringStream stringStream = new StringStream(sourceCode);

            while (!stringStream.Eof())
            {
                char current = stringStream.Get();

                if ('0' <= current && current <= '9') // check if current character is a digit
                {
                    stringStream.Putback();
                    tokenList.Add(new Token(Token_type.NUMBER, stringStream.GetNumber()));
                    continue; // continue seeking for tokens, ignore further code
                }

                bool isFound = false;
                
                for (int i = 0; i < keywords.Length; i++) // checks if current character is a start of some of operators
                {
                    if (current == keywords[i][0])
                    {
                        stringStream.Putback();
                        

                        if (stringStream.GetCharsLeft() >= keywords[i].Length && // Checks if the stream can provide enough characters for slice
                            stringStream.PeekSlice(keywords[i].Length) == keywords[i])
                        {
                            stringStream.Shift(keywords[i].Length);
                            tokenList.Add(new Token((Token_type)i));
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

                if (isFound == true) continue; // if the token is found, continue searching


            //    for (int i = 0; i < separators.Length; i++)
            //    {
            //        if (current == separators[i][0])
            //        {
            //            stringStream.Putback();

            //            if (stringStream.GetCharsLeft() >= operators[i].Length && // Checks if the stream can provide enough characters for slice
            //                stringStream.PeekSlice(operators[i].Length) == operators[i])
            //            {
            //                tokenList.Add(new Token(Token_type.SEPARATOR, stringStream.GetSlice(separators[i].Length)));
            //                Console.WriteLine(separators[i]);
            //                isFound = true;
            //                break;
            //            }
            //            else
            //            {
            //                stringStream.Get(); // to omit infinite loop, get character back from stream
            //            }
            //        }
            //    } if (isFound == true) continue; // if the token is found, continue searching
            }

        }

        public List<Token> GetList
        {
            get
            {
                return tokenList;
            }
        }
    }


}
