﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntON_programmingLanguage
{
    interface IExecutable 
    {
        void Run();
        void SetDelegates(CodeBlock.VarAdder adder, CodeBlock.VarGetter getter);
    }
}
