using System;
using MarkovChainApp.Readers;

namespace MarkovChainApp
{
  class Program
  {
    static void Main(string[] args)
    {
      MarkovChain chain = InitializeMarkovChain();
      RunApplication(chain);
    }

    private static MarkovChain InitializeMarkovChain()
    {
      Console.WriteLine("Creating a Markov chain..");

      IReader reader = new FileReader();
      var strings = reader.Read("C:\\Users\\P1319424\\Desktop\\Internship\\repos\\MarkovChainApp\\MarkovChainApp\\TextFiles\\beeMovieScript.txt");
      return new MarkovChain(strings);
    }

    private static void RunApplication(MarkovChain chain)
    {
      bool doRunApplication = true;
      do
      {
        GenerateText(chain);
        doRunApplication = promptForRerun();

      } while (doRunApplication);
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
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
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
