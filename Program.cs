using System;
using System.Collections.Generic;
using System.IO;

namespace TrieProject
{
    class Program
    {
        static void Main(string[] args)
        {

            Trie trie = new Trie();

            Console.WriteLine("Type \"help\" to list available commands\n");

            while (true)
            {

                string input = Console.ReadLine().ToLower();

                string[] inputArgs = input.Split(' ');

                switch (inputArgs[0])
                {
                    case "check":
                        Console.WriteLine((trie.IsWordInTrie(inputArgs[1])) ? inputArgs[1] + " is in the trie" : inputArgs[1] + " is not in the trie");
                        break;

                    case "add":
                        try
                        {
                            bool added = trie.AddWord(inputArgs[1]);
                            Console.WriteLine((added) ? inputArgs[1] + " added to trie" : inputArgs[1] + " is already in the trie");
                        } 
                        catch(ArgumentException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        
                        break;

                    case "delete":
                        try
                        {
                            bool added = trie.DeleteWord(inputArgs[1]);
                            Console.WriteLine((added) ? inputArgs[1] + " deleted from trie" : inputArgs[1] + " is not in the trie");
                        }
                        catch (ArgumentException e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        break;

                    case "size":
                        int size = trie.GetNodeCount();
                        Console.WriteLine("The trie contains " + size + " nodes");
                        break;


                    case "find":
                        string prefix = inputArgs[1];
                        List<string> words = trie.FindWords(prefix);

                        if(words == null || words.Count == 0)
                        {
                            Console.WriteLine("No words found containing the prefix " + prefix);
                        }
                        else
                        {
                            Console.WriteLine("Words found:");
                            foreach(string word in words)
                            {
                                Console.WriteLine(word);
                            }
                        }
                        break;

                    case "read":
                        string workingDirectory = Environment.CurrentDirectory;
                        string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

                        string[] lines = File.ReadAllLines(projectDirectory + "\\wordlist.txt");

                        int cntr = 0;
                        foreach (string line in lines)
                        {
                            trie.AddWord(line);
                            cntr++;
                        }

                        Console.WriteLine(cntr + " words successfully added from file");
                        break;

                    case "clear":
                        trie.Clear();
                        Console.WriteLine("Trie cleared");
                        break;

                    case "gc":
                        System.GC.Collect();
                        Console.WriteLine("Garbage collected");
                        break;

                    case "help":
                        Console.WriteLine("---------- Commands ----------");
                        Console.WriteLine("add <WORD>    ---- Add word to trie");
                        Console.WriteLine("delete <WORD> ---- Delete word from trie");
                        Console.WriteLine("check <WORD>  ---- Check if word is in trie");
                        Console.WriteLine("find <PREFIX> ---- Find words beginning with given index");
                        Console.WriteLine("size          ---- Show number of nodes in trie");
                        Console.WriteLine("read          ---- Add words from data file");
                        Console.WriteLine("clear         ---- Clear trie");
                        break;

                    default:
                        Console.WriteLine("Invalid syntax!");
                        break;

                }

                Console.WriteLine();

            }
        }
    }
}
