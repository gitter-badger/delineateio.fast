using System.Collections.Generic;

namespace Delineate.Cloud.Fast
{ 
    public enum NodeOperations
    {
        None,

        Create,

        Delete
    }

    public enum NodeTypes
    {
        Root,

        File,

        Directory
    }

    public sealed class Node
    {
        public const string ROOT = "root";

        public Node Parent { get; private set; }

        public string Name { get; set; }

        public NodeTypes Type { get; set; }

        public NodeOperations Operation { get; set; }

        public List<Node> Nodes { get; private set; }
        
        public static Node CreateRoot()
        {
            return new Node()
            {
                Name = ROOT,
                Type = NodeTypes.Root,
                Operation = NodeOperations.None
            };
        }

        public Node Add(Node node)
        {
            if(Nodes == null)
                Nodes = new List<Node>();

            node.Parent = this;

            Nodes.Add(node);

            return node;
        }

        public Node Add(string name, NodeTypes type, NodeOperations operation)
        {
            Node node = new Node()
            {
                Name = name,
                Type = type,
                Operation = operation 
            };

            return Add(node);
        }
    }
}