using StudentMath.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentMath.Core.Interface
{
    public interface IXmlExamParser
    {
        Teacher ParseXml(string xmlContent);

    }
}
