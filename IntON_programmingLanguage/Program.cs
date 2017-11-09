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

            String s = "1534.1234";
            StringStream ss = new StringStream(s);

            double d =  ss.GetNumber();

            Console.WriteLine(d);
            

        }
    }
}
