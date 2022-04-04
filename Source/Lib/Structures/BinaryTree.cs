using System;

namespace Lib.Structures
{
    public class BinaryTree<T>
    {
        public BinaryTree()
        {
        }
        public BinaryTree(T value)
        {
            Value = value;
        }
        public BinaryTree(T value,BinaryTree<T> left,BinaryTree<T> right)
        {
            Value = value;
            Left = left;
            Right = right;
        }
        public T Value = default;
        public BinaryTree<T> Left = default;
        public BinaryTree<T> Right = default;        
        public static BinaryTree<T> CreateNode(T value)
        {
            return new BinaryTree<T>
            {
                Value = value,
                Left = new BinaryTree<T>(),
                Right = new BinaryTree<T>()
            };
        }
        public void AddChild(T value)
        {
            var child = BinaryTree<T>.CreateNode(value);
            AddChild(child);
        }
        public void AddChild(BinaryTree<T> child)
        {            
            Value = child.Value;
            Left = child.Left;
            Right = child.Right;
        }
        public void TraverseTree(Action<T> callback)
        {                                                         
            callback(Value);
            TraverseTree(Left,callback);
            TraverseTree(Right,callback);            
        }
        private void TraverseTree(BinaryTree<T> parent,Action<T> callback)
        {            
            if(parent is null)
                return;
                                 
            callback(parent.Value);
            TraverseTree(parent.Left,callback);
            TraverseTree(parent.Right,callback);            
        }
    }
}
