using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntON_programmingLanguage
{
    enum Unit_type { TERMINAL, NON_TERMINAL }
    abstract class ParsingUnit
    {
        virtual public Unit_type UnitType { get; }
    }
}
