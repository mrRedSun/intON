using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntON_programmingLanguage
{
    class PrintExpression : ParsingUnit, IExecutable
    {
        private ICalculatable expr;
        private CodeBlock.VarGetter getVar;

        public PrintExpression(ICalculatable expression)
        {
            expr = expression;
        }

        public void SetDelegates(CodeBlock.VarAdder adder, CodeBlock.VarGetter getter)
        {
            getVar = getter;
        }

        public void Run()
        {
            expr.SetDelegate(getVar);
            Console.WriteLine(expr.Evaluate().Value);
        }
    }
}
