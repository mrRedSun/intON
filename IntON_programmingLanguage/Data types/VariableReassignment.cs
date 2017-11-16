using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntON_programmingLanguage.Data_types
{
    class VariableReassignment : ParsingUnit, IExecutable
    {
        private string id;
        private ICalculatable expr;
        private CodeBlock.VarAdder AddVar;
        private CodeBlock.VarGetter GetVar;

        public VariableReassignment(string identifier, ICalculatable expression)
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
