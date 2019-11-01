using System;
using System.Collections.Generic;
using System.Text;

namespace Sakartvelosoft.API.Core.Filters
{
    public class UnaryComparison : ComparationOperand<bool>
    {
        public override void DetectNewParameters(IDictionary<string, IParameterReference> parametersBag)
        {
        }
    }
}
