using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntON_programmingLanguage
{
    class VariableDeclaration : ParsingUnit, IExecutable
    {
        private string id;
        private ICalculatable expr;
        private CodeBlock.VarAdder AddVar;
        private CodeBlock.VarGetter GetVar;

        public VariableDeclaration(string identifier, ICalculatable expression)
        {
            id = identifier;
            expr = expression;
        }
        

        public void Run()
        {
            expr.SetDelegate(GetVar);
            AddVar(id, expr.Evaluate().Value);
        }

        public void SetDelegates(CodeBlock.VarAdder adder, CodeBlock.VarGetter getter)
        {
            AddVar = adder;
            GetVar = getter;
        }
    }
}
