using CornelyXML.lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CornelyXML.Parser;

public class SyntaxAnalyzerPDA
{
    public SyntaxAnalyzerPDA()
    {
        var state19 = new State();
        state19.StateAction = StateAction.Accept;
        state19.Name = "ACCEPT";
        this.FinalState = state19;

        var state18 = new State();
        state18.StateAction = StateAction.Scan;
        state18.Name = "state18";

        var state17 = new State();
        // non-deterministic, FIX THIS
        state17.Transitions.Add(new Tuple<TokenType, State>(TokenType.ClosingSymbol, state19));
        state17.Transitions.Add(new Tuple<TokenType, State>(TokenType.ClosingSymbol, state18));
        state17.StateAction = StateAction.Read;
        state17.Name = "state17";

        var state16 = new State();
        state16.Transitions.Add(new Tuple<TokenType, State>(TokenType.ClosingSymbol, state17));
        state16.StateAction = StateAction.Scan;
        state16.Name = "state16";

        var state15 = new State();
        state15.Transitions.Add(new Tuple<TokenType, State>(TokenType.Tag, state16));
        state15.StateAction = StateAction.Read;
        state15.Name = "state15";

        var state14 = new State();
        state14.Transitions.Add(new Tuple<TokenType, State>(TokenType.Tag, state15));
        state14.StateAction = StateAction.Scan;
        state14.Name = "state14";

        var state13 = new State();
        state13.Transitions.Add(new Tuple<TokenType, State>(TokenType.OpeningWithBackslashSymbol, state14));
        state18.Transitions.Add(new Tuple<TokenType, State>(TokenType.OpeningWithBackslashSymbol, state13));
        state13.StateAction = StateAction.Write;
        state13.Name = "state13";

        var state12 = new State();
        state12.Transitions.Add(new Tuple<TokenType, State>(TokenType.OpeningWithBackslashSymbol, state13));

        state12.StateAction = StateAction.Scan;
        state12.Name = "state12";

        var state11 = new State();
        state11.Transitions.Add(new Tuple<TokenType, State>(TokenType.Value, state12));
        state11.Transitions.Add(new Tuple<TokenType, State>(TokenType.OpeningWithBackslashSymbol, state13));
        state11.StateAction = StateAction.Scan;
        state11.Name = "state11";

        var state10 = new State();
        state10.Transitions.Add(new Tuple<TokenType, State>(TokenType.ClosingSymbol, state11));
        state10.Transitions.Add(new Tuple<TokenType, State>(TokenType.SelfClosingSymbol, state11));
        state10.Transitions.Add(new Tuple<TokenType, State>(TokenType.SelfClosingSymbol, state19));
        state10.StateAction = StateAction.Read;
        state10.Name = "state10";

        var state9 = new State();
        state9.Transitions.Add(new Tuple<TokenType, State>(TokenType.ClosingSymbol, state10));
        state9.Transitions.Add(new Tuple<TokenType, State>(TokenType.SelfClosingSymbol, state10));
        state9.StateAction = StateAction.Scan;
        state9.Name = "state9";

        var state8 = new State();
        state8.Transitions.Add(new Tuple<TokenType, State>(TokenType.AttributeValue, state9));
        state8.StateAction = StateAction.Scan;
        state8.Name = "state8";

        var state7 = new State();
        state7.Transitions.Add(new Tuple<TokenType, State>(TokenType.Operator, state8));
        state7.Transitions.Add(new Tuple<TokenType, State>(TokenType.ClosingSymbol, state10));
        state7.Transitions.Add(new Tuple<TokenType, State>(TokenType.SelfClosingSymbol, state10));


        state9.Transitions.Add(new Tuple<TokenType, State>(TokenType.Attribute, state7));
        state7.StateAction = StateAction.Scan;
        state7.Name = "state7";

        var state6 = new State();
        state6.Transitions.Add(new Tuple<TokenType, State>(TokenType.Attribute, state7));
        state6.Transitions.Add(new Tuple<TokenType, State>(TokenType.ClosingSymbol, state10));
        state6.Transitions.Add(new Tuple<TokenType, State>(TokenType.SelfClosingSymbol, state10));
        state6.StateAction = StateAction.Scan;
        state6.Name = "state6";

        var state5 = new State();
        state5.Transitions.Add(new Tuple<TokenType, State>(TokenType.Tag, state6));
        state5.StateAction = StateAction.Write;
        state5.Name = "state5";

        var state4 = new State();
        state4.Transitions.Add(new Tuple<TokenType, State>(TokenType.Tag, state5));
        state4.StateAction = StateAction.Scan;
        state4.Name = "state4";

        var state3 = new State();
        state3.Transitions.Add(new Tuple<TokenType, State>(TokenType.OpeningSymbol, state4));
        state18.Transitions.Add(new Tuple<TokenType, State>(TokenType.OpeningSymbol, state3));
        state11.Transitions.Add(new Tuple<TokenType, State>(TokenType.OpeningSymbol, state3));
        state12.Transitions.Add(new Tuple<TokenType, State>(TokenType.OpeningSymbol, state3));
        state3.StateAction = StateAction.Write;
        state3.Name = "state3";

        var state2 = new State();
        state2.Transitions.Add(new Tuple<TokenType, State>(TokenType.OpeningSymbol, state3));
        state2.StateAction = StateAction.Scan;
        state2.Name = "state2";


        /*
         Header Part
         (which was added later hence the numbering on the states)
         */
        //var state29 = new State();
        //state29.Transitions.Add(new Tuple<TokenType, State>(TokenType.HeaderXmlTag, state2));
        //state29.StateAction = StateAction.Read;
        //state29.Name = "state29";

        var state28 = new State();
        state28.Transitions.Add(new Tuple<TokenType, State>(TokenType.HeaderXmlClosingSymbol, state2));
        state28.Transitions.Add(new Tuple<TokenType, State>(TokenType.HeaderXmlClosingSymbol, state19));

        state28.StateAction = StateAction.Read;
        state28.Name = "state28";

        var state27 = new State();
        state27.Transitions.Add(new Tuple<TokenType, State>(TokenType.HeaderXmlClosingSymbol, state28));
        //state27.Transitions.Add(new Tuple<TokenType, State>(TokenType.HeaderXmlClosingSymbol, state19));

        state27.StateAction = StateAction.Scan;
        state27.Name = "state27";

        var state26 = new State();
        state26.Transitions.Add(new Tuple<TokenType, State>(TokenType.AttributeValue, state27));
        state26.StateAction = StateAction.Scan;
        state26.Name = "state26";

        var state25 = new State();
        state25.Transitions.Add(new Tuple<TokenType, State>(TokenType.HeaderXmlClosingSymbol, state28));
        state25.Transitions.Add(new Tuple<TokenType, State>(TokenType.Operator, state26));

        state27.Transitions.Add(new Tuple<TokenType, State>(TokenType.Attribute, state25));
        state25.StateAction = StateAction.Scan;
        state25.Name = "state25";

        var state24 = new State();
        state24.Transitions.Add(new Tuple<TokenType, State>(TokenType.Attribute, state25));
        state24.Transitions.Add(new Tuple<TokenType, State>(TokenType.HeaderXmlClosingSymbol, state28));

        state24.StateAction = StateAction.Scan;
        state24.Name = "state24";


        var state23 = new State();
        state23.Transitions.Add(new Tuple<TokenType, State>(TokenType.HeaderXmlTag, state24));
        state23.StateAction = StateAction.Write;
        state23.Name = "state23";

        var state22 = new State();
        state22.Transitions.Add(new Tuple<TokenType, State>(TokenType.HeaderXmlTag, state23));
        state22.StateAction = StateAction.Scan;
        state22.Name = "state22";

        var state21 = new State();
        state21.Transitions.Add(new Tuple<TokenType, State>(TokenType.HeaderXmlOpeningSymbol, state22));
        state21.StateAction = StateAction.Write;
        state21.Name = "state21";


        var state20 = new State();
        state20.Transitions.Add(new Tuple<TokenType, State>(TokenType.HeaderXmlOpeningSymbol, state21));
        state20.StateAction = StateAction.Scan;
        state20.Name = "state20";


        var initialState = new State();
        initialState.Transitions.Add(new Tuple<TokenType, State>(TokenType.Dollar, state20));
        initialState.StateAction = StateAction.Start;
        this.InitialState = initialState;
        this.CurrentState = initialState;

        this.SymbolStack = new Stack<string>();
        this.TagStack = new Stack<string>();
    }

    public State InitialState { get; set; }

    public State FinalState { get; set; }

    public State CurrentState { get; set; }

    public Stack<string> SymbolStack { get; set; }

    public Stack<string> TagStack { get; set; }

    public bool Validate(List<Token> tokens, int idx = -1)
    {
        Token currentToken = null;
        CurrentState = CurrentState.GoToNextState(TokenType.Dollar); // move from the initialstate going forward

        try
        {
            for (int i = idx; i < tokens.Count;)
            {
                if (CurrentState == null)
                {
                    throw new ArgumentException($"Syntax error No match at Token {i}  Symbol: [{tokens[i].Symbol}]  Type: [{tokens[i].TokenType}]  Info: [{tokens[i].Info}]");
                }

                if (CurrentState.StateAction == StateAction.Accept)
                {
                    break;
                }
                if (CurrentState.StateAction == StateAction.Scan)
                {
                    // do nothing
                    i++;
                    currentToken = i >= 0 ? tokens[i] : null;
                }
                else if (CurrentState.StateAction == StateAction.Read && currentToken.TokenType == TokenType.Tag)
                {
                    if (TagStack.Peek() == currentToken.Symbol)
                    {
                        TagStack.Pop();
                    }
                }
                else if (CurrentState.StateAction == StateAction.Read && (currentToken.TokenType == TokenType.HeaderXmlOpeningSymbol || currentToken.TokenType == TokenType.HeaderXmlClosingSymbol || currentToken.TokenType == TokenType.ClosingSymbol || currentToken.TokenType == TokenType.OpeningSymbol || currentToken.TokenType == TokenType.OpeningWithBackslashSymbol || currentToken.TokenType == TokenType.SelfClosingSymbol))
                {
                    if (currentToken.TokenType == TokenType.SelfClosingSymbol || currentToken.TokenType == TokenType.HeaderXmlClosingSymbol)
                    {
                        TagStack.Pop();
                    }
                    var peek = SymbolStack.Peek();
                    if ((currentToken.Partners != null && currentToken.Partners.Any(item => item == peek)) || peek == currentToken.Partner)
                    {
                        SymbolStack.Pop();

                    }
                    // TO DO: handle partner tags
                }
                else if (CurrentState.StateAction == StateAction.Write && (currentToken.TokenType == TokenType.Tag || currentToken.TokenType == TokenType.HeaderXmlTag))
                {
                    TagStack.Push(currentToken.Symbol);
                }
                else if (CurrentState.StateAction == StateAction.Write && (currentToken.TokenType == TokenType.HeaderXmlClosingSymbol || currentToken.TokenType == TokenType.HeaderXmlOpeningSymbol || currentToken.TokenType == TokenType.ClosingSymbol || currentToken.TokenType == TokenType.OpeningSymbol || currentToken.TokenType == TokenType.OpeningWithBackslashSymbol || currentToken.TokenType == TokenType.SelfClosingSymbol))
                {
                    SymbolStack.Push(currentToken.Symbol);
                }
                var nextState = CurrentState.GoToNextState(currentToken.TokenType); // move from the initialstate going forward

                if (nextState == null)
                {
                    //// non-deterministic
                    //// guess next state based on next token
                    var nextToken = (i + 1) >= tokens.Count ? null : tokens[i + 1];
                    if (nextToken != null)
                    {
                        var states = CurrentState.GetPossibleNextStates(currentToken.TokenType);
                        nextState = states.Where(state => state.StateAction != StateAction.Accept).FirstOrDefault();
                    }
                    else
                    {
                        // end state
                        var states = CurrentState.GetPossibleNextStates(currentToken.TokenType);
                        nextState = states.Where(state => state.StateAction == StateAction.Accept).FirstOrDefault();
                    }
                }

                CurrentState = nextState;

            }
        }
        catch (Exception ex)
        {

            throw;
        }

        // scan
        //foreach (var item in tokens)
        //{
        //    if (item.TokenType == TokenType.Tag)
        //    {
        //        if (tags.Count != 0 && tags.Peek() == item.Symbol)
        //        {
        //            tags.Pop();
        //        }
        //        else
        //        {
        //            tags.Push(item.Symbol);

        //        }
        //    }
        //    else if (item.TokenType == TokenType.OpeningSymbol || item.TokenType == TokenType.OpeningWithBackslashSymbol)
        //    {
        //        symbols.Push(item);
        //    }
        //    else if (symbols.Count != 0 && item.TokenType == TokenType.ClosingSymbol)
        //    {
        //        if (symbols.Peek().TokenType == TokenType.OpeningSymbol || symbols.Peek().TokenType == TokenType.OpeningWithBackslashSymbol)
        //        {
        //            symbols.Pop();
        //        }
        //    }
        //    else if (item.TokenType == TokenType.SelfClosingSymbol)
        //    {
        //        tags.Pop();
        //    }
        //}

        if (TagStack.Count == 0 && SymbolStack.Count == 0 && CurrentState == FinalState)
        {
            return true;
        }

        return false;
    }

    public Token Scan(List<Token> tokens)
    {
        return tokens.FirstOrDefault();
    }
}

public enum States
{
    Write,
    Scan,
}

