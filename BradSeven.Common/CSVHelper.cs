using System;
using System.Collections.Generic;

namespace BradSeven.Common
{
    public static class CSVHelper
    {
        public static string ListToCSV(List<string> list)
        {
            return String.Join(",", list.ToArray());
        }
    }
}
