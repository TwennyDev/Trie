using System;
using System.Collections.Generic;
using System.Text;

namespace TrieProject
{
    class TrieNode
    {

        private Dictionary<char, TrieNode> children;
        private bool isWordEnd;

        public TrieNode()
        {
            children = new Dictionary<char, TrieNode>();
            isWordEnd = false;
        }

        public Dictionary<char, TrieNode> Children { get => children; }
        public bool IsWordEnd { get => isWordEnd; set => isWordEnd = value; }

    }
}
