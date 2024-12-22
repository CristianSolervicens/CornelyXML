using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CornelyXML.lexer;

public class Lexeme
{
    public char Character { get; private set; }
    public Lexeme(char character)
    {
        Character = character;
    }

    public Productions Productions { get; set; }
}

