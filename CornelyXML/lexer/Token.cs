using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CornelyXML.lexer;

public class Token
{
    public Token()
    {
        this.FinalData = "";
    }

    public string Symbol { get; set; }

    public TokenType TokenType { get; set; }

    public string Info { get; set; }

    public Token(string symbol)
    {
        this.Symbol = symbol;
    }

    public string FinalData {  get; set; }
    
    public string Partner { get; set; }

    public List<string> Partners { get; set; }

    public string Content
    { 
        get { 
             return $"{this.Symbol}{this.FinalData}"; 
            }
    }
}

