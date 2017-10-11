using System.Collections.Generic;
using System.Linq;

namespace Wikiled.Server.Core.Helpers
{
    public static class BasicExtensions
    {
        public static IEnumerable<string> SplitCsv(this string csvList)
        {
            if (string.IsNullOrWhiteSpace(csvList))
            {
                return new string[] { };
            }
                
            return csvList
                .TrimEnd(',')
                .Split(',')
                .AsEnumerable()
                .Select(s => s.Trim());
        }
    }
}
