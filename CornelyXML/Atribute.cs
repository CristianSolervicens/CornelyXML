using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CornelyXML;

public class Atribute
{
    public string Name { get; set; }
    public string Value { get; set; }

    public Atribute(string name, string value)
    {
        Name = name;
        Value = value;
    }
}
