namespace MarkovChainApp
{
  public class Node
  {
    public bool IsFirst { get; set; }
    public string Key { get; set; }

    public Node(bool isFirst, string key)
    {
      IsFirst = isFirst;
      Key = key;
    }

  }
}
