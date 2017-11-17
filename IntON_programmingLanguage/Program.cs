using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
    static class Program
    {

        [STAThread]

        static void Main()
        {
            //Lexer Lexerz = new Lexer("var i = 0; while (i < 100000) { print (i); i = i + 1; }");
            //var Le = new Lexer("print(id);");
            //var Lexerz = new Lexer("var id = true; while (id) { while (true) { print(id);  if (id) { print(id);} } print(0); }");
            var Lexerz = new Lexer("var idd = 10; if (idd > 1) { print(idd); idd = idd - 1; }");


            List<Token> queu = new List<Token>(Lexerz.GetList);
            var parser = new Parser(queu);

            parser.GetProgram().Run(Console.WriteLine);

            if (Environment.OSVersion.Version.Major >= 6)
                SetProcessDPIAware();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

        }


        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();    
    }
}
