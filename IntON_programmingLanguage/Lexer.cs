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
        private readonly string[] keywords =
        {
          //  0       1       2     3        4         5
            "var", "while", "if", "true", "false",  "print"
        };

        private readonly string[] operators =
        { 
        //  0     1    2    3    4    5    6    7      8    9   10
            "+", "-", "*", "/", "=", ">", "<", "==", "!=", "(", ")"
        };

        private readonly string[] separators =
        {
         //  1    2    3
            ";", "{", "}"
        };




        private string sourceCode;
        private List<Token> tokenList;

        public Lexer(string source)
        {
            tokenList = new List<Token>();
            sourceCode = source;
        }

        private void Parse()
        {
            StringStream stringStream = new StringStream(sourceCode);

            while (!stringStream.Eof())
            {

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
