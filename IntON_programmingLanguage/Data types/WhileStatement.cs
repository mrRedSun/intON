using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace IntON_programmingLanguage
{
    class WhileStatement : ParsingUnit, IExecutable
    {
        LogicExpression lExpr;
        CodeBlock cBlock;
        CodeBlock.VarAdder adder;
        CodeBlock.VarGetter getter;

        public WhileStatement(LogicExpression expr, CodeBlock block)
        {
            lExpr = expr;
            cBlock = block;
        }

        public async void Run(CodeBlock.OutputFunction printF)
        {
            cBlock.SetDelegates(adder, getter);
            lExpr.SetDelegate(getter);
            
            while (lExpr.Evaluate().Value == 1)
            {
                cBlock.Run(printF);
            }
        }

        public void SetDelegates(CodeBlock.VarAdder _adder, CodeBlock.VarGetter _getter)
        {
            adder = _adder;
            getter = _getter;
        }
    }

}
