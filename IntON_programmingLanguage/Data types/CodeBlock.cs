using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntON_programmingLanguage
{
    class CodeBlock : ParsingUnit, IExecutable
    {
        private List<IExecutable> statements;
        private Dictionary<string, double> variables;
        public delegate double VarGetter(string id);
        public delegate void VarAdder(string id, double value);
        private VarGetter getVar;
        private VarAdder setVar;
        private VarGetter getOutterVar;
        private VarAdder setOutterVar;

        public CodeBlock(List<IExecutable> stBlock)
        {
            getVar = GetVar;
            setVar = AddVar;
            statements = stBlock;
            variables = new Dictionary<string, double>();
        }

        private double GetVar(string id)
        {
            try
            {
                return variables[id];
            }
            catch (KeyNotFoundException e)
            {
                return getOutterVar(id);
            }
        }

        private void AddVar(string id, double value)
        {
            if (variables.TryGetValue(id, out double trash))
            {
                variables[id] = value; return;
            }
            variables.Add(id, value);
        }

        public void Run()
        {

            foreach(var item in statements)
            {
                item.SetDelegates(setVar, getVar);
            }
            foreach (var item in statements)
            {
                item.Run();
            }
        }

        public void SetDelegates(VarAdder adder, VarGetter getter)
        {
            getOutterVar = getter;
            setOutterVar = adder;
        }
    }
}
