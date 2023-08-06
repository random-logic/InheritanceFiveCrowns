using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace InheritanceFiveCrowns
{
    public class LogNode
    {
        protected LinkedListNode<string> Node;

        public LogNode(LinkedListNode<string> node)
        {
            Node = node;
        }

        public LogNode Previous() => new(Node.Previous);
        public LogNode Next() => new(Node.Next);
        public string Get() => Node.Value;
        public bool Exists() => Node != null;
    }
}