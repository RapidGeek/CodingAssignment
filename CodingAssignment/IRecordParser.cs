using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingAssignment
{
    interface IRecordParser
    {
        Record ParseRecord(String text);
    }
}
