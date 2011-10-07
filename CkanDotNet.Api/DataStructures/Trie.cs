using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * From http://datastructures.codeplex.com.  See NOTICE.txt included with this project.
 */
namespace CkanDotNet.Api.DataStructures
{
    /// <summary>
    /// A collection Trie class that can be used to locate words by prefix
    /// Can also return a list of all words starting with the requested prefix.
    /// NOTE: the case insensitive Trie will always return lowercase words.
    /// </summary>
    public class Trie
    {
        protected TrieNode _rootNode;
        protected bool _isCaseSensitive = false;

        /// <summary>
        /// Trie Consructor
        /// </summary>
        public Trie()
        {
            _rootNode = new TrieNode(' ');
        }

        /// <summary>
        /// Trie Consructor
        /// </summary>
        /// <param name="isCaseSensitive">true - if case sensitivity is required from the Trie</param>
        public Trie(bool isCaseSensitive)
            : this()
        {
            _isCaseSensitive = isCaseSensitive;
        }

        /// <summary>
        /// Add an additional word to the collection of words represented by the Trie
        /// </summary>
        /// <param name="item"></param>
        public void Add(String item)
        {
            if (!_isCaseSensitive)
                item = item.ToLower();
            AddRecursive(_rootNode, item, 0);
        }

        /// <summary>
        /// Performs the actual recursive insertion of the characters in the item into the Trie nodes
        /// </summary>
        /// <param name="node"></param>
        /// <param name="item"></param>
        /// <param name="index"></param>
        protected void AddRecursive(TrieNode node, String item, int index)
        {
            // If we are at and end of word - mark the node as ending a word
            if (item.Length == index)
            {
                node.EndsWord = true;
                return;
            }
            char firstChar = item[index];
            TrieNode childNode = node.AddOrGetNode(firstChar);
            if (childNode != null)
            {
                AddRecursive(childNode, item, index + 1);
            }
        }

        /// <summary>
        /// Searches for a node that corresponds to the requires item.
        /// If such a node is found in the Trie - returns true.
        /// </summary>
        /// <param name="item">a string to search for in the Trie</param>
        /// <returns>true - if item is found in the Trie</returns>
        public bool Contains(String item)
        {
            if (!_isCaseSensitive)
                item = item.ToLower();
            TrieNode matchedNode = FindPrefixRecursive(_rootNode, item);
            return (matchedNode != null);
        }

        /// <summary>
        /// Performs the actual recursive lookup of the characters in the item
        /// </summary>
        /// <param name="node">node to search for matches</param>
        /// <param name="item">string to be looked-up</param>
        /// <returns>returns </returns>
        protected TrieNode FindPrefixRecursive(TrieNode node, string item)
        {
            if (String.IsNullOrEmpty(item))
            {
                return node;
            }
            char firstChar = item[0];
            TrieNode childNode = node.GetChildNode(firstChar);

            // If there is no corresponding node, then we didn't find our item - return null.
            if (childNode == null)
            {
                return null;
            }
            else
            {
                // we did find our matching node - go to it and continue matching the rest of the string
                return FindPrefixRecursive(childNode, item.Substring(1));
            }
        }

        /// <summary>
        /// Returns all words contained in the Trie that match the prefix
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public List<String> GetCompletionList(String prefix)
        {
            return GetCompletionList(prefix, int.MaxValue);
        }

        /// <summary>
        /// Returns the requested amount of words contained in the Trie that match the prefix.
        /// Note that there is no specific order for the returned words.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="completionSetCount">maximum number of words to return</param>
        /// <returns>A list of strings that match the required prefix</returns>
        public List<String> GetCompletionList(String prefix, int completionSetCount)
        {
            if (!_isCaseSensitive)
                prefix = prefix.ToLower();
            TrieNode matchedNode = FindPrefixRecursive(_rootNode, prefix);
            List<String> completions = new List<String>();
            if (!String.IsNullOrEmpty(prefix))
            {
                StringBuilder prefixSB = new StringBuilder(prefix);
                GetCompletionsRecursive(matchedNode, prefixSB, completionSetCount, completions);
            }
            return completions;
        }

        /// <summary>
        /// Performs the actual lookup for the all words contained in the subtree. 
        /// </summary>
        /// <param name="node">root node of a subtree to look for contained words</param>
        /// <param name="wordBuilder">a StringBuilder to perform fast character appending</param>
        /// <param name="completionSetCount">maximum number of words to return</param>
        /// <param name="completions">A list to contain all the words that are found</param>
        protected void GetCompletionsRecursive(TrieNode node, StringBuilder wordBuilder, int completionSetCount, List<String> completions)
        {
            if ((node == null) || (completions.Count >= completionSetCount))
            {
                // no match or we have enough matches to fulfil request - rollback builder and return
                wordBuilder.Length -= 1;
                return;
            }
            if (node.EndsWord)
            {
                // end reached, append the built word into completions list.
                completions.Add(wordBuilder.ToString());
            }
            IDictionary<char, TrieNode> childNodes = node.Children;
            if (childNodes != null)
            {
                foreach (KeyValuePair<char, TrieNode> pair in childNodes)
                {
                    char childChar = pair.Value.Character;
                    wordBuilder.Append(childChar);
                    GetCompletionsRecursive(pair.Value, wordBuilder, completionSetCount, completions);
                }
            }
            wordBuilder.Length -= 1;
        }
    }

    /// <summary>
    /// Node class for the Trie representing one trie node
    /// </summary>
    public class TrieNode
    {
        protected char _character;
        protected IDictionary<char, TrieNode> _children;
        public bool EndsWord { get; set; }

        public TrieNode(char c)
        {
            this._character = c;
            EndsWord = false;
        }

        #region Properties

        public char Character
        {
            get
            {
                return _character;
            }
        }

        public IDictionary<char, TrieNode> Children
        {
            get
            {
                return _children;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds the requested character to the Node subtree children
        /// If there is already a child representing the character - return it
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public TrieNode AddOrGetNode(char c)
        {
            TrieNode newNode;
            if (_children == null)
                _children = new Dictionary<char, TrieNode>();

            if (!_children.TryGetValue(c, out newNode))
            {
                // children do not contain c, add a TrieNode
                newNode = new TrieNode(c);
                _children.Add(c, newNode);
            }
            return newNode;
        }

        public TrieNode GetChildNode(char c)
        {
            TrieNode newNode;
            if ((_children == null) || (!_children.TryGetValue(c, out newNode)))
                newNode = null;
            return newNode;
        }

        #endregion

        #region Overrides

        public override int GetHashCode()
        {
            return _character.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            TrieNode that = obj as TrieNode;
            if (that == null)
                return false;
            return (this.Character == that.Character);
        }

        public override string ToString()
        {
            return base.ToString() + "(" + Character + ")";
        }

        #endregion

    }
}
