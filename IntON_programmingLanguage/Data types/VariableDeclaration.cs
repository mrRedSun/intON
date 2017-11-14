using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntON_programmingLanguage
{
    class VariableDeclaration : StatementBase
    {
        private string id;
        private MathExpression expr;
        private CodeBlock.VarAdder Add;

        public VariableDeclaration(string identifier, MathExpression expression)
        {
            id = identifier;
            expr = expression;
        }

        public void SetDelegate(CodeBlock.VarAdder adder)
        {
            Add = adder;
        }
        

        public override void Run()
        {
            Add(id, expr.Evaluate());
        }
    }
}
