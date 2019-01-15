using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarkovChainApp.Readers;

namespace MarkovChainApp
{
  /// <summary>
  /// An edge defines the start node, end node, and their associated probabilities.
  /// </summary>
  public class DirectedEdge
  {
    public string From { get; set; }
    public string To { get; set; }
    public int Weightage { get; set; }

    public DirectedEdge(string from, string to, int weightage )
    {
      this.From = from;
      this.To = to;
      this.Weightage = weightage;
    }
  }
}
