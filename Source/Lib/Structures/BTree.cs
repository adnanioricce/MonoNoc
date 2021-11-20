using System;

namespace Lib.Structures
{
    public class BTree<T>
    {
        public BTree()
        {            
        }
        public BTree(T value)
        {
            Value = value;
        }
        public BTree(T value,BTree<T> left,BTree<T> right)
        {
            Value = value;
            Left = left;
            Right = right;
        }
        public T Value = default;
        public BTree<T> Left = default;
        public BTree<T> Right = default;        
        public static BTree<T> CreateNode(T value)
        {
            return new BTree<T>
            {
                Value = value,
                Left = new BTree<T>(),
                Right = new BTree<T>()
            };
        }
        public void AddChild(T value)
        {
            var child = BTree<T>.CreateNode(value);
            AddChild(child);
        }
        public void AddChild(BTree<T> child)
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
        private void TraverseTree(BTree<T> parent,Action<T> callback)
        {            
            if(parent is null)
                return;
                                 
            callback(parent.Value);
            TraverseTree(parent.Left,callback);
            TraverseTree(parent.Right,callback);            
        }
    }
}
