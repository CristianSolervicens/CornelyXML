using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CornelyXML.lexer;

public enum TokenType
{
    ClosingSymbol,
    OpeningSymbol,
    Tag,
    Attribute,
    AttributeValue,
    Operator,
    Value,
    None,
    OpeningWithBackslashSymbol,
    SelfClosingSymbol,
    HeaderXmlOpeningSymbol,
    HeaderXmlClosingSymbol,
    HeaderXmlTag,
    Dollar
}
