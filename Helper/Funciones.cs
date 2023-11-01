using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPAvisosPM.Helper
{
  public  class Funciones
    {
      public static bool IsNumeric(string input)
      {
          int test;
          return int.TryParse(input, out test);
      }

      public static IEnumerable<string> GetNextChars(string str, int iterateCount)
      {
          var words = new List<string>();

          for (int i = 0; i < str.Length; i += iterateCount)
              if (str.Length - i >= iterateCount) words.Add(str.Substring(i, iterateCount));
              else words.Add(str.Substring(i, str.Length - i));

          return words;
      }
    }
}
