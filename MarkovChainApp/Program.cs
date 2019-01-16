using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MarkovChainApp.Readers;

namespace MarkovChainApp
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Welcome to the Markov Chain app!");

      MarkovChain chain = GenerateMarkovChain();

      bool doRunApplication = true;

      do
      {
        GenerateText(chain);
        doRunApplication = promptForRerun();

      } while (doRunApplication);


    }

    private static MarkovChain GenerateMarkovChain()
    {
      var reader = new FileReader();
      var strings = reader.Read("C:\\Users\\P1319424\\Desktop\\Internship\\repos\\MarkovChainApp\\MarkovChainApp\\TextFiles\\beeMovieScript.txt");

      // Generate chain
      var chain = new MarkovChain(strings);
      return chain;

    }

    private static void GenerateText(MarkovChain chain)
    {

      // Vomit sentences
      var message = chain.GenerateText();
      Console.WriteLine(message);
    }

    private static bool promptForRerun()
    {

      Console.WriteLine("Press [Space] to generate another line, [Esc] to exit.\n");

      bool hasResponded = false;
      bool respondedWith = false;
      do
      {
        ConsoleKeyInfo keyInfo = Console.ReadKey();
        if (keyInfo.Key.Equals(ConsoleKey.Spacebar))
        {
          respondedWith = true;
          hasResponded = true;
        }

        else if (keyInfo.Key.Equals(ConsoleKey.Escape))
        {
          respondedWith = false;
          hasResponded = true;
        }

      } while (!hasResponded);

      return respondedWith;
    }
  }
}
