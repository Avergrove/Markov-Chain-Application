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

    public TextFileReader(string filePath)
    {
      this.filePath = filePath;
    }

    /// <summary>
    /// Reads strings from a file.
    /// </summary>
    /// <param name="arguments">[0]: File path</param>
    /// <returns></returns>
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
