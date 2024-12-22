// See https://aka.ms/new-console-template for more information
using CornelyXML.lexer;
using CornelyXML.Parser;
using CornelyXML;
using System.Diagnostics;


try
{
    Stopwatch s1 = new Stopwatch();
    s1.Start();
    // var fileName = @"C:\MisArchivos\dev\C#\PENDIENTE PROCESO - Pro_77303395-1_33_4285.xml";
    var fileName = @"C:\MisArchivos\dev\C#\FIRMADO - AEC_E77303395-1T33F4285C76945222-2A1S2.xml";

    var cornelyXml = new CornelyXML.CornelyXML();
    var valid = cornelyXml.LoadXMLFile(fileName);

    if (valid)
    {
        s1.Stop();
        Console.WriteLine($"Yes, valid {s1.ElapsedMilliseconds.ToString()} ms");
    }
    else
    {
        Console.WriteLine($"No, invalid {s1.ElapsedMilliseconds.ToString()} ms");
    }

    Console.WriteLine();
    cornelyXml.PrintTokens();
    Console.Write(" >> "); Console.ReadLine();

    //cornelyXml.PrintNodes();
    cornelyXml.SaveXMLFile("Salida.xml");
    Console.WriteLine("Archivo [Salida.xml] escrito");

    string path = "DTE/Documento/Encabezado/Emisor";
    Console.WriteLine($"Buscando {path} en {fileName}");
    var nodes = cornelyXml.GetXMLNodesByXpath(path);
    if (nodes.Count == 0)
        Console.WriteLine("Path No encontrado");

    foreach (var node in nodes)
    {
        foreach (var child in node.Children)
        {
            Console.WriteLine($"Nodo: {child.Tag} Texto: [{child.Text} Atributos [{child.GetAtributes()}]");
        }
        Console.WriteLine($"Nodo: {node.Tag} Texto: [{node.Text} Atributos [{node.GetAtributes()}]");
        Console.WriteLine($"Nodo Padre: {node.Parent.Tag} Texto: [{node.Parent.Text} Atributos [{node.Parent.GetAtributes()}]");
        Console.WriteLine();
        Console.WriteLine(node.GetXML());
        Console.WriteLine();
    }
    
    Console.WriteLine();
    Console.Write("Fin >> "); Console.ReadLine();

}
catch (Exception ex)
{
    var x = ex;
    Console.WriteLine($"Error, Invalid - {ex.Source} - {ex.Message}\n{ex.StackTrace}");
}
