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

    /// <summary>
    /// Creates a Markov chain
    /// </summary>
    /// <returns>A MArkov chain</returns>
    private static MarkovChain InitializeMarkovChain()
    {
      Console.WriteLine("Creating a Markov chain..");

      // You can select from a reader here, please check information on each reader on /Readers/*.
      IReader reader =
        new CsvFileReader("C:\\Users\\P1319424\\Desktop\\Internship\\repos\\MarkovChainApp\\MarkovChainApp\\Resources\\TrumpTweets.csv", 1)
        .SetDoDecodeHtml(true)
        .SetOffset(1);


      var strings = reader.Read();
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

    /// <summary>
    /// Prints out text from a markov chain.
    /// </summary>
    /// <param name="chain"></param>
    private static void GenerateText(MarkovChain chain)
    {
      var message = chain.SetMinimumGenerateLength(5).GenerateText();
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
