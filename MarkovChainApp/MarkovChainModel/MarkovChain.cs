using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace MarkovChainApp
{
  public class MarkovChain
  {
    public List<Node> Nodes { get; set; }
    public List<DirectedEdge> Edges { get; set; }
    private static Random rand;

    private int minimumSentenceLength = 15;

    public MarkovChain(List<string> inputStrings)
    {
      Nodes = new List<Node>();
      Edges = new List<DirectedEdge>();
      rand = new Random();

      GenerateChain(inputStrings);
      // Console.WriteLine(this.ToString());
    }

    /// <summary>
    /// Generates a Markov chain model with a lsit of string
    /// TODO: Add opening words.
    /// </summary>
    /// <param name="inputStrings"></param>
    private void GenerateChain(IEnumerable<string> inputStrings)
    {

      string prev = null;       // Indicates the previous word

      foreach (var line in inputStrings)
      {
        foreach (var word in line.Split(' '))
        {
          // Generate nodes
          var node = Nodes.FirstOrDefault(x => x.Key == word);

          if (node == null)
          {
            node = new Node(false, word);
            Nodes.Add(node);
          }

          // Then generate edges
          if (prev != null)
          {
            var edge = Edges.FirstOrDefault(x => (x.To == word) && (x.From == prev));

            if (edge == null)
            {
              edge = new DirectedEdge(prev, word, 1);
              Edges.Add(edge);
            }

            else
            {
              edge.Weightage++;
            }

          }

          prev = word;

        }
      }
    }


    /// <summary>
    /// Generates a random string of text from the current Markov chain.
    /// </summary>
    /// <returns></returns>
    public string GenerateText()
    {

      string returnText = string.Empty;
      string curr = GetStarterKey();

      bool textGenerated = false;
      int currentSentenceLength = 0;
      while(!textGenerated)
      {
        returnText = string.Concat(returnText, curr, " ");

        if (currentSentenceLength >= minimumSentenceLength && IsSentenceEnder(curr))
        {
          textGenerated = true;
        }

        curr = GetNextToValue(curr);
        currentSentenceLength++;

      }

      return returnText;

    }

    public MarkovChain SetMinimumGenerateLength(int length)
    {
      this.minimumSentenceLength = length;
      return this;
    }

    private bool IsSentenceStarter(String s)
    {
      Regex startsWithCapital = new Regex(@"^[A-Z]");
      if (startsWithCapital.IsMatch(s))
      {
        return true;
      }

      return false;
    }

    private bool IsSentenceEnder(String s)
    {
      Regex endsWithFullStop = new Regex(@".*\.$");
      if (endsWithFullStop.IsMatch(s))
      {
        return true;
      }

      // Is ender if string ends with question mark (?)
      Regex endsWithQuestionMark = new Regex(@".*\?$");
      if(endsWithQuestionMark.IsMatch(s))
      {
        return true;
      }

      // Is ender if string ends with exclamation mark (!)
      Regex endsWithExclamationMark = new Regex(@".*!$");
      if(endsWithExclamationMark.IsMatch(s))
      {
        return true;
      }

      return false;
    }

    /// <summary>
    /// Retrieves a key that starts a sentence.
    /// </summary>
    /// <returns>Key that starts a sentence</returns>
    private String GetStarterKey()
    {

      var rand = new Random();
      List<string> values = Nodes.Select(x => x.Key).ToList();

      bool isStarterKeyFound = false;
      String selectedString = null;

      while (!isStarterKeyFound)
      {
        selectedString = values[rand.Next(0, values.Count)];
        if (IsSentenceStarter(selectedString))
        {
          isStarterKeyFound = true;
        }
      }

      return selectedString;
    }

    /// <summary>
    /// Retrieves a random key from the Markov Chain dictionary
    /// </summary>
    /// <returns></returns>
    private string GetRandomKey()
    {
      var rand = new Random();
      List<string> values = Nodes.Select(x => x.Key).ToList();
      int size = Nodes.Count();

      return values[rand.Next(0, size)];
    }

    /// <summary>
    /// Retrieves the next key based on a provided key
    /// </summary>
    /// <param name="key">The key to check on</param>
    /// <returns></returns>
    private string GetNextToValue(string key)
    {
      var toEdges = Edges.Where(x => x.From == key);
      var words = new List<string>();

      foreach (var edge in toEdges)
      {
        for (var i = 0; i < edge.Weightage; i++)
        {
          words.Add(edge.To);
        }
      }

      var randIndex = rand.Next(0, words.Count);

      return words[randIndex];
    }


    public override string ToString()
    {

      var s = string.Empty;

      foreach (var node in Nodes)
      {
        s = string.Concat(s, "From: ", node.Key, ": \n");
        foreach (var edge in Edges.Where(x => x.From == node.Key))
        {
          s = string.Concat(s, "    To: ", edge.To, ",  Weightage: ", edge.Weightage, "\n");
        }

      }

      return s;
    }


  }
}
