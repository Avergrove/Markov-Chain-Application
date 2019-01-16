using System;
using System.Collections.Generic;
using System.IO;

namespace MarkovChainApp.Readers
{
  class CsvFileReader : IReader
  {

    private string filePath;
    private char seperator;
    private int stringColumn;
    private int offset;
    private bool doDecodeHtml;

    /// <summary>
    /// A file reader that is able to read CSV files.
    /// </summary>
    /// <param name="filePath">The path to the file to read.</param>
    /// <param name="stringColumn">The column in which strings reside in the csv file.</param>
    public CsvFileReader(String filePath, int stringColumn = 0, int offset = 0, char seperator = ',', bool doDecodeHtml = false)
    {
      this.filePath = filePath;
      this.stringColumn = stringColumn;
      this.offset = offset;
      this.seperator = seperator;
      this.doDecodeHtml = doDecodeHtml;
    }

    /// <summary>
    /// Reads from a CSV file
    /// </summary>
    /// <returns>A list of strings read from the csv file.</returns>
    public List<string> Read()
    {
      var csvLines = this.getCsvLines();
      return getStringsFromCsvLines(csvLines, stringColumn, seperator);
    }

    private List<string> getCsvLines()
    {
      List<string> csvLines = new List<string>();

      try
      {
        StreamReader sr = new StreamReader(filePath);
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          if (offset != 0)
          {
            offset--;
            continue;
          }

          else
          {
            csvLines.Add(line);
          }
        }

      }
      catch (FileNotFoundException ex)
      {
        Console.WriteLine(ex.Message);
        return null;
      }

      return csvLines;
    }

    private List<string> getStringsFromCsvLines(List<string> csvLines, int stringColumn, char seperator)
    {
      List<string> strings = new List<string>();

      foreach(string csvLine in csvLines)
      {
        string currLine = csvLine;

        if (doDecodeHtml)
        {
          currLine = System.Web.HttpUtility.HtmlDecode(currLine);
        }

        strings.Add(currLine.Split(seperator)[stringColumn]);
      }

      return strings;
    }

    public CsvFileReader SetFilePath(string filePath)
    {
      this.filePath = filePath;
      return this;
    }

    public CsvFileReader SetStringColumn(int stringColumn)
    {
      this.stringColumn = stringColumn;
      return this;
    } 

    public CsvFileReader SetSeperator(char seperator)
    {
      this.seperator = seperator;
      return this;
    }

    public CsvFileReader SetOffset(int offset)
    {
      this.offset = offset;
      return this;
    }

    public CsvFileReader SetDoDecodeHtml(bool doDecode)
    {
      this.doDecodeHtml = doDecode;
      return this;
    }
  }
}
