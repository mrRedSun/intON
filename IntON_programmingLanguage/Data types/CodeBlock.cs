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
        public delegate void OutputFunction(string text);
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
                if (getOutterVar != null)
                {
                    return getOutterVar(id);
                }
                throw new Exception("SYNTAX ERROR");
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

        public void Run(CodeBlock.OutputFunction PrintF)
        {

            foreach(var item in statements)
            {
                item.SetDelegates(setVar, getVar);
            }
            foreach (var item in statements)
            {
                item.Run(PrintF);
            }
        }

    

        public void SetDelegates(VarAdder adder, VarGetter getter)
        {
            getOutterVar = getter;
            setOutterVar = adder;
        }
    }
}
