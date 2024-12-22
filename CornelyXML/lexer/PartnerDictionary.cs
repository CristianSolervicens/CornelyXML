using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CornelyXML.lexer;

public class PartnerDictionary
{
    public PartnerDictionary()
    {
        // initialize dictionary here
    }
    private Dictionary<TokenType, TokenType> PartnerDic { get; set; }

    /// <summary>
    /// Find the partner of the token type for popping purposes.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public TokenType Find(TokenType type)
    {
        return PartnerDic.Keys.Where(t => t == type).FirstOrDefault();
    }
}
