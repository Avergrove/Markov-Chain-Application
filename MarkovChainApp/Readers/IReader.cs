using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovChainApp.Readers
{
  /// <summary>
  /// An interface that defines a class as a reader.
  /// </summary>
  public interface IReader
  {
    /// <summary>
    /// Instructs the Reader to begin reading, returning a list of string from reading.
    /// </summary>
    /// <returns>The list of string from reading.</returns>
    List<string> Read();
  }
}
