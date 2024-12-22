using CornelyXML.Helper;
using CornelyXML.lexer;
using CornelyXML.Parser;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;


namespace CornelyXML;


public class CornelyXML
{
    public string xmlString { get; private set; }
    public bool isValid {get;private set;}
    protected List<Token> Tokens = new List<Token>();
    public List<Node> Nodes { get; private set; } = new List<Node>();
    public Encoding Encoding { get; private set; }


    public List<Node> GetXMLNodesByXpath(string xpath, bool strict = false)
    {
        var nodes = new List<Node>();
        var xpathParts = xpath.Split('/');

        if (xpath == "/")
            if (this.Nodes.Count == 2)
            {
                nodes.Add(this.Nodes[1]);
                return nodes;
            }
            else
                return nodes;

        if (xpathParts.Length == 0 )
            return nodes;

        // Search From Root
        int startIndex = 0;
        if (xpathParts[0] == "")
        {
            strict = true;
            startIndex = 1;
        }

        foreach( var node in this.Nodes)
            InternalGetNodesByPath(node, xpathParts, startIndex, nodes, strict);

        return nodes;
    }


    private void InternalGetNodesByPath(Node nodeTree, string[] path, int startIndex, List<Node> nodes, bool strict = false)
    {
        if (strict)
        {
            foreach (var child in nodeTree.Children)
            {
                if (child.Tag.Trim() == path[startIndex].Trim() )
                {
                    if (startIndex >= path.Length - 1)
                    {
                        nodes.Add(child);
                        return;
                    }
                    InternalGetNodesByPath(child, path, startIndex+1, nodes, strict);
                }
            }
            return;
        }

        foreach(var child in nodeTree.Children)
        {
            //Si el Nodo actual es "padre" de lo buscado o lo buscado
            // desde acá el camino debe ser extricto
            if (child.Tag.Trim() == path[startIndex].Trim() )
            {
                if (startIndex >= path.Length - 1)
                {
                    nodes.Add(child);
                    return;
                }
                InternalGetNodesByPath(child, path, startIndex+1, nodes, true);
            }
            else
                InternalGetNodesByPath(child, path, startIndex, nodes, strict);
        }
    }


    /// <summary>
    /// 
    /// GetXMLElementsByTagName(string tagName)
    /// 
    /// </summary>
    /// <param name="tagName"></param>
    /// <returns></returns>
    public List<Node> GetXMLElementsByTagName(string tagName)
    {
        var nodes = new List<Node>();

        foreach (var node in Nodes)
        {
            if (node.Tag == tagName)
            {
                nodes.Add(node);
            }
            foreach (var child in node.Children)
            {
                if (child.Tag == tagName)
                {
                    nodes.Add(child);
                }
            }
        }

        return nodes;
    }


    /// <summary>
    /// 
    /// LoadXMLFile(string filename)
    /// 
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    /// <exception cref="IOException"></exception>
    /// <exception cref="Exception"></exception>
    public bool LoadXMLFile(string filename)
    {
        // Cleaning the internal structures
        Nodes.Clear();
        this.Tokens.Clear();
        this.xmlString = "";

        try
        {
            this.Encoding = EncodingDetector.DetectEncoding(filename);
            this.xmlString = File.ReadAllText(filename, this.Encoding);
            return ReadXml(this.xmlString);
        }
        catch (IOException ex1)
        { 
            throw new IOException(ex1.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    /// <summary>
    /// 
    /// ReadXml(string xmlDoc)
    /// 
    /// </summary>
    /// <param name="xmlDoc"></param>
    /// <returns></returns>
    public bool ReadXml(string xmlDoc)
    {
        // Cleaning the internal structures
        Nodes.Clear();
        this.Tokens.Clear();
        this.xmlString = xmlDoc;

        try
        {
            LexicalAnalyzer lexicalAnalyzer = new LexicalAnalyzer();
            this.Tokens = lexicalAnalyzer.FindTokensInText(xmlDoc);
            SyntaxAnalyzerPDA syntaxAnalyzer = new SyntaxAnalyzerPDA();
            this.isValid = syntaxAnalyzer.Validate(Tokens);
        }
        catch (Exception ex)
        {
            this.isValid = false;
        }

        try
        {
            FormNodesFromTokens();
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error Formando Nodos!!!");
            this.isValid = false;
        }

        return this.isValid;
    }


    /// <summary>
    /// 
    /// SaveXMLFile(string filename)
    /// 
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    /// <exception cref="IOException"></exception>
    /// <exception cref="Exception"></exception>
    public bool SaveXMLFile(string filename)
    {
        try
        {
            string xmlString = GetXmlString();
            File.WriteAllText(filename, xmlString, this.Encoding);
            return true;
        }
        catch (IOException ex1)
        {
            throw new IOException($"{ex1.Message}");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    /// <summary>
    /// 
    /// GetXmlString()
    /// 
    /// </summary>
    /// <returns></returns>
    public string GetXmlString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (var node in this.Nodes)
        {
            stringBuilder.Append( node.GetXML());
        }
        
        return stringBuilder.ToString();
    }


    /// <summary>
    /// 
    /// PrintTokens
    /// 
    /// </summary>
    public void PrintTokens()
    {
        foreach (Token token in this.Tokens)
        Console.WriteLine($"Token:  Type: [{token.TokenType}], Value: [{token.Symbol}], Info [{token.Info}]");
    }


    /// <summary>
    /// 
    /// FormNodesFromTokens
    /// 
    /// </summary>
    /// <returns></returns>
    private bool FormNodesFromTokens()
    {
        Node header = null;
        Node node = null;
        int index = 0;
        (header, index) = ReadXMLHeader(index);
        if (header != null)
        {
            this.Nodes.Add(header);
        }

        (node, index) = ReadXMLRecursiveNodes(index);
        if (node != null)
        {
            this.Nodes.Add(node);
        }

        Node parent = null;
        foreach (var nodo in this.Nodes)
            SetNodeParent(nodo, parent);

        return true;
    }


    /// <summary>
    /// 
    /// SetNodeParent
    /// 
    /// </summary>
    /// <param name="node"></param>
    /// <param name="parent"></param>
    private void SetNodeParent(Node node, Node parent)
    {
        node.Parent = parent;
        foreach(var child in node.Children)
        {
            SetNodeParent(child, node);
        }
    }


    /// <summary>
    /// 
    /// PrintNodes()
    /// 
    /// </summary>
    public void PrintNodes()
    {
        foreach (var node in this.Nodes)
        {
            PrintNodeRecursive(node);
        }
    }


    /// <summary>
    /// 
    /// PrintNodeRecursive(Node node)
    /// 
    /// </summary>
    /// <param name="node"></param>
    private void PrintNodeRecursive(Node node)
    {
        Console.WriteLine($"Node: [{node.Tag}], Text: [{node.Text}] Atributes {GetNodeAtributes(node)}");
        foreach (var child in node.Children)
        {
            PrintNodeRecursive(child);
        }
    }


    /// <summary>
    /// 
    /// GetNodeAtributes(Node node)
    /// 
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    private string GetNodeAtributes(Node node)
    {
        string value = "";
        foreach(var atribute in node.Attributes)
            value += $"{atribute.Name}:{atribute.Value} ";
        return $"[{value}]";
    }


    /// <summary>
    /// 
    /// ReadXMLHeader(int startPos)
    /// 
    /// </summary>
    /// <param name="startPos"></param>
    /// <returns></returns>
    private (Node, int) ReadXMLHeader(int startPos)
    {
        var node = new Node();
        int index = startPos;

        string atribute = "";
        string atributeValue = "";
        var status = FormingState.starting;

        node.NodeType = NodeType.XMLHeader;

        for (int i = startPos; i < this.Tokens.Count; i++)
        {
            // Starting
            if (this.Tokens[i].TokenType == TokenType.HeaderXmlOpeningSymbol && status == FormingState.starting)
            {
                status = FormingState.startingHeader;
            }
            else if (this.Tokens[i].TokenType == TokenType.HeaderXmlClosingSymbol && status == FormingState.tagCreated)
            {
                node.TrailingString = this.Tokens[i].FinalData;
                return (node, i+1);
            }
            else if (this.Tokens[i].TokenType == TokenType.HeaderXmlTag && status == FormingState.startingHeader)
            {
                node.Tag = this.Tokens[i].Symbol;
                status = FormingState.tagCreated;
            }
            else if (this.Tokens[i].TokenType == TokenType.Attribute && status == FormingState.tagCreated)
            {
                atribute = this.Tokens[i].Symbol;
            }
            else if (this.Tokens[i].TokenType == TokenType.AttributeValue && status == FormingState.tagCreated)
            {
                atributeValue = this.Tokens[i].Symbol;
                node.Attributes.Add(new Atribute(atribute, atributeValue.Replace("\"", "") ));
                atribute = "";
                atributeValue = "";
            }

        }

        return (null, -1);

    }


    /// <summary>
    /// 
    /// ReadXMLRecursiveNodes(int startPos)
    /// 
    /// </summary>
    /// <param name="startPos"></param>
    /// <returns></returns>
    private (Node, int) ReadXMLRecursiveNodes(int startPos)
    {
        var node = new Node();
        int index = startPos;

        string atribute = "";
        string atributeValue = "";
        var status = FormingState.starting;

        node.NodeType  = NodeType.XMLNode;

        for (int i = startPos; i < this.Tokens.Count; i++)
        {
            // Starting
            if (status == FormingState.starting)
            {
                if (this.Tokens[i].TokenType == TokenType.OpeningSymbol)
                {
                    continue;
                }
                else if (this.Tokens[i].TokenType == TokenType.Tag)
                {
                    node.Tag = this.Tokens[i].Symbol;
                    status = FormingState.tagCreated;
                }
            }
            
            // tagCreated
            else if (status == FormingState.tagCreated)
            {
                if (this.Tokens[i].TokenType == TokenType.Operator)
                {
                    continue;
                }
                else if (this.Tokens[i].TokenType == TokenType.ClosingSymbol)
                {
                    node.TrailingString = this.Tokens[i].FinalData;
                    status = FormingState.headerClosed;
                }
                else if (this.Tokens[i].TokenType == TokenType.Attribute)
                {
                    atribute = this.Tokens[i].Symbol;
                }
                else if (this.Tokens[i].TokenType == TokenType.AttributeValue)
                {
                    atributeValue = this.Tokens[i].Symbol;
                    node.Attributes.Add(new Atribute(atribute, atributeValue.Replace("\"", "")));
                    atribute = "";
                    atributeValue = "";
                }
                else if (this.Tokens[i].TokenType == TokenType.SelfClosingSymbol)
                {
                    index = i;
                    node.TrailingString = this.Tokens[i].FinalData;
                    node.NodeType = NodeType.XMLNodeAutoClose;
                    return (node, index);
                }
            }

            //headerClosed
            else if (status == FormingState.headerClosed)
            {
                if (this.Tokens[i].TokenType == TokenType.Value && node.Text == "")
                {
                    if (node.Text == "")
                    {
                        node.Text = this.Tokens[i].Symbol;
                    }
                    else
                    {
                        Console.WriteLine($"ERROR Formando Nodos Token N° {i},  Tag: {this.Tokens[i].Symbol}");
                    }
                }
                else if (this.Tokens[i].TokenType == TokenType.OpeningSymbol)
                {
                    Node children;
                    (children, index) = ReadXMLRecursiveNodes(i);
                    i = index;
                    node.Children.Add(children);
                }
                else if (this.Tokens[i].TokenType == TokenType.OpeningWithBackslashSymbol)
                {
                    status = FormingState.ClosingNode;
                }
            }

            // ClosingNode
            else if (status == FormingState.ClosingNode)
            {
                if (this.Tokens[i].TokenType == TokenType.Tag && this.Tokens[i].Symbol == node.Tag)
                {
                    continue;
                }
                if (this.Tokens[i].TokenType == TokenType.ClosingSymbol)
                {
                    index = i;
                    node.ClosingString = this.Tokens[i].FinalData;
                    return (node, index);
                }
            }

        }

        return (node, -1);
    }


    /// <summary>
    /// 
    /// enum FormingState
    /// 
    /// </summary>
    private enum FormingState
    {
        starting,
        startingHeader,
        tagCreated,
        headerClosed,
        ClosingNode
    }

}

