using System;
using System.Collections.Generic;
using System.Linq;

namespace csharp_interviews.exos
{
    public class Node
    {
        public string Name { get; set; }
        public List<Node> ChildNodes { get; set; }

        public Node(string name, params Node[] childNodes)
        {
            Name = name;
            if (childNodes?.Any() == false) ChildNodes = new List<Node>();
            else
            {
                ChildNodes = childNodes.ToList();
            }
        }

        public void GetTree()
        {
            var nodes = new List<Node>() { this };
            ProcessNode(this, ref nodes);
            var result = string.Join("-", nodes.Select(x => x.Name));
            Console.WriteLine(result);
        }

        public static void ProcessNode(Node node, ref List<Node> nodes)
        {
            var childNodes = node.ChildNodes;
            nodes.AddRange(childNodes);
            if (childNodes?.Any() == true)
                foreach (var item in childNodes)
                    ProcessNode(item, ref nodes);
            else
                return;
        }
    }

    public static class Recursion
    {
        public static void _Main()
        {
            Node net = new Node("NET");
            Node te = new Node("TE");
            Node table = new Node("TABLE");
            Node au = new Node("AU");

            Node car = new Node("CAR", net, te, table);
            Node pe = new Node("PE", au);

            Node la = new Node("LA", car, pe);
            la.GetTree();
        }
    }
}
