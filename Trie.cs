using System;
using System.Collections.Generic;
using System.Text;

namespace TrieProject
{
    class Trie
    {

        private TrieNode _root;

        public Trie()
        {
            _root = new TrieNode();
        }

        public int GetNodeCount() => GetNodeCount(_root);        

        public bool IsWordInTrie(string word) => IsWordInTrie(new Queue<char>(word), _root);

        public void Clear()
        {
            _root.Children.Clear();
        }
        
        public List<string> FindWords(string prefix)
        {

            Queue<char> split = new Queue<char>(prefix);

            TrieNode root = _root;

            while(split.Count > 0)
            {

                char c = split.Dequeue();

                if (root.Children.ContainsKey(c))
                {
                    root = root.Children[c];
                }
                else
                {
                    return null;
                }

            }

            List<string> wordList = new List<string>();

            FindWords(root, wordList, prefix);

            return wordList;

        }

        public bool AddWord(string word)
        {

            if(word.Length == 0)
            {
                throw new ArgumentException("String cannot be empty!");
            }

            if (!IsWordInTrie(word))
            {
                AddWord(new Queue<char>(word), _root);
                return true;
            }

            return false;
            
        }

        public bool DeleteWord(string word)
        {

            if (word.Length == 0)
            {
                throw new ArgumentException("String cannot be empty!");
            }

            if (IsWordInTrie(word))
            {
                DeleteWord(new Queue<char>(word), _root);
                return true;
            }

            return false;

        }

        private void FindWords(TrieNode root, List<string> wordList, string prefix)
        {

            if (root.IsWordEnd)
            {
                wordList.Add(prefix);
            }

            foreach(char key in root.Children.Keys)
            {
                string newPrefix = prefix + key;
                FindWords(root.Children[key], wordList, newPrefix);
            }
            
        }

        private int GetNodeCount(TrieNode root)
        {

            int count = root.Children.Count;

            foreach(char key in root.Children.Keys)
            {

                count += GetNodeCount(root.Children[key]);

            }

            return count;

        }

        private void DeleteWord(Queue<char> word, TrieNode root)
        {
            if(word.Count == 0)
            {
                root.IsWordEnd = false;
                return;
            }

            char c = word.Dequeue();

            if (root.Children.ContainsKey(c))
            {
                DeleteWord(word, root.Children[c]);
                
                if(root.Children[c].Children.Count == 0 && !root.Children[c].IsWordEnd)
                {
                    root.Children.Remove(c);
                }
            }
            
        }

        private void AddWord(Queue<char> word, TrieNode root)
        {

            if(word.Count == 0)
            {
                root.IsWordEnd = true;
                return;
            }

            char c = word.Dequeue();

            if (!root.Children.ContainsKey(c))
            {
                root.Children.Add(c, new TrieNode());
            }

            AddWord(word, root.Children[c]);

        }

        private bool IsWordInTrie(Queue<char> word, TrieNode root)
        {

            if(word.Count == 0)
            {
                return root.IsWordEnd;
            }

            char c = word.Dequeue();

            if (!root.Children.ContainsKey(c))
            {
                return false;
            }

            return IsWordInTrie(word, root.Children[c]);

        }

    }
}
