using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovChainApp.Readers
{
  class TextFileReader : IReader
  {
    private string filePath;

    /// <summary>
    /// A reader that can read strings from text files.
    /// </summary>
    /// <param name="filePath">The path of the file to read from.</param>
    public TextFileReader(string filePath)
    {
      this.filePath = filePath;
    }

    /// <summary>
    /// Reads strings from the filePath supplied.
    /// </summary>
    /// <returns>A list of strings from the file.</returns>
    public List<string> Read()
    {
      var returnStrings = new List<string>();

      try
      {
        StreamReader sr = new StreamReader(filePath);
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          returnStrings.Add(line);
        }

      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }

      return returnStrings;
    }

    public TextFileReader SetFilePath(String filePath)
    {
      this.filePath = filePath;
      return this;
    }
  }
}
