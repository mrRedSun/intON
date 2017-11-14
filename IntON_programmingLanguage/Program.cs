using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/* Alphabet:
 *  Terminals:
 *      var, [identifier], [number], +, -, *, /, >, <, ==, (, ), =, ==, {, }, print, [bool], while
 *  Non-terminals:
 *      DECLARATION, BLOCK, LOGIC_STATEMENT, EXPRESSION, TERM, 
 *      PRIMAL, WHILE_LOOP, IF_STATEMENT, PRINT_STATEMENT
 * 
 * */
namespace IntON_programmingLanguage
{
    class Program
    {



        static void Main(string[] args)
        {
            //Lexer lexer = new Lexer("while(true == true) { var integer = 5 + 3.534; print (8); }");

            var Lexerz = new Lexer("(3-3)*5-6/6");


            Dictionary<string, double> dictionary = new Dictionary<string, double>
            {
                { "var", 5 }
            };


            Queue<Token> queu = new Queue<Token>(Lexerz.GetList);
            MathExpression mathExpression = new MathExpression(queu, null);
            Console.WriteLine("Result: " + mathExpression.Evaluate());
           

        }
    }
}
