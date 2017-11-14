using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntON_programmingLanguage
{
    abstract class StatementBase : ParsingUnit
    {
        protected const Unit_type uType = Unit_type.TERMINAL;
        public override Unit_type UnitType { get { return uType; } }

        virtual public void Run() { }

    }
}
