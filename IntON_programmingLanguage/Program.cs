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
            //var Le = new Lexer("print(id);");
            var Lexerz = new Lexer("var idd = (5+5/5-8*0 + 3*0); var ccd = idd / 3; print(idd + ccd); ");

            var tt = new Queue<int>();
            tt.Enqueue(5);
            tt.Enqueue(6);
            tt.Enqueue(7);

            tt = new Queue<int>(tt.Reverse());

            Console.WriteLine(tt.Dequeue());

            List<Token> queu = new List<Token>(Lexerz.GetList);
            var parser = new Parser(queu);

            parser.GetProgram().Run();

        }
    }
}
