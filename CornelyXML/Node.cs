using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CornelyXML;

public class Node
{
    public string Tag { get; set; } = "";

    public string Text { get; set; } = "";

    public string TrailingString { get; set; } = "";  // Al Cierre del Nodo Mismo

    public string ClosingString { get; set; } = "";  // Cuando es un Nodo con Hijos y Cierre

    public string LeadingString { get; set; } = "";
    
    public string InnerText 
    { get;
      set;
    } = "";

    public NodeType NodeType { get; internal set; }
    
    public List<Atribute> Attributes { get; set; } = new List<Atribute>();

    public List<Node> Children { get; set; } = new List<Node>();

    // Needed to Find Siblings
    public Node Parent { get; set; }

    //Implementar
    public override string ToString()
    {
        return base.ToString();
    }

    // -------------------------
    //  C o n s t r u c t o r s
    // -------------------------
    public Node() { }    
    
    public Node(string name, string text)
    {
        Tag = name;
        Text = text;
    }

    // --------------------------
    //  M e t h o d s
    // -------------------------
    public Node FindNode(string tag)
    {
        foreach (Node child in Children)
        {
            if (child.Tag == tag)
            {
                return child;
            }
        }

        return null;
    }


    public void AddAtribute(string name, string value)
    {
        Atribute atribute = new Atribute(name, value);
        Attributes.Add(atribute);
    }


    public string GetXML()
    {
        if (this.NodeType == NodeType.XMLHeader)
            return $"<?{Tag}{this.GetAtributes()}?>{TrailingString}";

        if (this.NodeType == NodeType.XMLNodeAutoClose)
            return $"<{Tag}{this.GetAtributes()}/>{TrailingString}";

        return $"<{Tag}{this.GetAtributes()}>{TrailingString}{Text}{this.GetChildrenXML()}</{Tag}>{this.ClosingString}";
    }


    private string GetChildrenXML()
    {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (Node child in Children)
        {
            stringBuilder.Append(child.GetXML());
        }
        return stringBuilder.ToString();
    }


    public string GetAtributes()
    {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (Atribute atribute in Attributes)
        {
            stringBuilder.Append($" {atribute.Name}=\"{atribute.Value}\"");
        }
        return stringBuilder.ToString();
    }

}


public enum NodeType
{
    XMLHeader,
    XMLRoot,
    XMLNode,
    XMLNodeAutoClose
}

