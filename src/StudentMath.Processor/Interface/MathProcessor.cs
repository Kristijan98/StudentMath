
using System.Data;


namespace StudentMath.Processor.Interface
{
    public class MathProcessor : IMathProcessor
    {
        public double EvaluateExpression(string expression)
        {
            try
            {
                var dt = new DataTable();
                var result = dt.Compute(expression, "");
                return Convert.ToDouble(result);
            }
            catch
            {
                throw new InvalidOperationException($"Invalid math expression: {expression}");
            }
        }
    }
}
