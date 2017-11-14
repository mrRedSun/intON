using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntON_programmingLanguage
{
    class CodeBlock : StatementBase
    {
        private List<StatementBase> statements;
        private Dictionary<string, double> variables;
        public delegate double VarGetter(string id);
        public delegate void VarAdder(string id, double value);
        private VarGetter getVar;
        private VarAdder setVar;

        public CodeBlock()
        {
            getVar = GetVar;
            setVar = AddVar;
            statements = new List<StatementBase>();
            variables = new Dictionary<string, double>();
        }

        private double GetVar(string id)
        {
            return variables[id];
        }

        private void AddVar(string id, double value)
        {
            variables.Add(id, value);
        }

    }
}
