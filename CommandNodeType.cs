namespace Delineate.Cloud.Fast
{
    /// <summary>
    /// Value type for the node type in the tree
    /// used in the Plan and Apply command methods
    /// </summary>
    public enum CommandNodeType
    {
        
        Root, /// Root node of the tree

        File, /// File nodein the tree

        Directory /// Directory node in the tree 
    }
}