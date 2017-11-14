using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntON_programmingLanguage
{
    interface ICalculatable
    {
        Token Evaluate();
        void SetDelegate(CodeBlock.VarGetter getter);
    }
}
