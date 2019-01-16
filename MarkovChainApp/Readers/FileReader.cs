using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovChainApp.Readers
{
  internal class FileReader : IReader
  {
    public List<string> Read(string inputUrl)
    {

      var returnStrings = new List<string>();

      try
      {
        StreamReader sr = new StreamReader(inputUrl);
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
  }
}
