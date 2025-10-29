using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentMath.Processor.Interface
{
    public interface IMathProcessor
    {
        double EvaluateExpression(string expression);

    }
}
