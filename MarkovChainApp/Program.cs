using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarkovChainApp.Readers;

namespace MarkovChainApp
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Welcome to the Markov Chain app!");

      // Read
      var reader = new FileReader();
      var strings = reader.Read("C:\\Users\\**Snip**\\Desktop\\Internship\\repos\\MarkovChainApp\\MarkovChainApp\\TextFiles\\beeMovieScript.txt");


      // Generate chain
      var chain = new MarkovChain(strings);

      // Vomit 
      var message = chain.GenerateText();
      Console.WriteLine(message);
      
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();

    }
  }
}
