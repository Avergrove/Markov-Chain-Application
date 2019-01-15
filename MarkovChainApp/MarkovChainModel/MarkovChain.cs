using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MarkovChainApp
{
  public class MarkovChain
  {
    public List<Node> Nodes { get; set; }
    public List<DirectedEdge> Edges { get; set; }
    private static Random rand;


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
      string curr = GetRandomKey();

      for (int i = 0; i < 100; i++)
      {
        returnText = string.Concat(returnText, curr, " ");
        curr = GetNextToValue(curr);
      }

      return returnText;

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
