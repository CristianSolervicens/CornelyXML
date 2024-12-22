using CornelyXML.lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CornelyXML.Parser;

public class State
{
    public List<Tuple<TokenType, State>> Transitions { get; set; }
    public StateAction StateAction { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public State()
    {
        Transitions = new List<Tuple<TokenType, State>>();
    }

    public State GoToNextState(TokenType tokenType, Token token = null)
    {
        var matches = Transitions.Where(item => item.Item1 == tokenType);

        if (matches.Count() == 0)
        {
            return null;
        }
            if (matches.Count() > 1)
        {
            // non-deterministic
            return null;
        }
        
        return matches.FirstOrDefault().Item2;
    }

    public List<State> GetPossibleNextStates(TokenType tokenType)
    {
        var matches = Transitions.Where(item => item.Item1 == tokenType);
        var retVal = new List<State>();

        foreach (var item in matches)
        {
            retVal.Add(item.Item2);
        }

        return retVal;
    }

    public State ReEvaluateNextStateBasedOnNextToken(TokenType tokenType)
    {
        var match = Transitions.Where(item => item.Item1 == tokenType).FirstOrDefault();
        return match.Item2;
    }
}

public enum StateAction
{
    Read,
    Write,
    Scan,
    Start,
    Accept,
    Reject
}
