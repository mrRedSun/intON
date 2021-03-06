﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntON_programmingLanguage
{
    class IfStatement : ParsingUnit, IExecutable
    {
        LogicExpression lExpr;
        CodeBlock cBlock;
        CodeBlock.VarAdder adder;
        CodeBlock.VarGetter getter;

        public IfStatement(LogicExpression expr, CodeBlock block)
        {
            lExpr = expr;
            cBlock = block;
        }

        public void Run(CodeBlock.OutputFunction printF)
        {
            cBlock.SetDelegates(adder, getter);
            lExpr.SetDelegate(getter);

            if (lExpr.Evaluate().Type == Token_type.TRUE)
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
