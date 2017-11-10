using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntON_programmingLanguage
{
   
    enum Token_type
    {
        FALSE, PRINT, WHILE, VARIABLE, TRUE, IF,
        EQUAL, NOT_EQUAL,

        PLUS, MINUS, MULTIPLY,
        DIVIDE, ASSIGN, GREATER_THAN, LESS_THAN,
        OPEN_PARENTHESIS, CLOSE_PARANTHESIS,

        SEMICOLON, OPEN_BRACKET, CLOSE_BRACKET, 

        NUMBER, ID,

        INVALID_TOKEN
    };

    class Token : ParsingUnit
    {
        private readonly Token_type type;
        private readonly string id;
        private readonly double value;
        private const Unit_type uType = Unit_type.NON_TERMINAL;
        override public Unit_type UnitType { get { return uType; } }

        public string Id
        {
            get
            {
                return id;
            }
        }

        public double Value
        {
            get
            {
                return value;
            }
        }

        public Token_type Type
        {
            get
            {
                return type;
            }
        }

        public Token(Token_type tp)
        {
            type = tp;
        }

        public Token(Token_type tp, double value)
        {
            type = tp;
            this.value = value;
        }

        public Token(Token_type tp, string id)
        {
            type = tp;
            this.id = id;
        }
    }
}
