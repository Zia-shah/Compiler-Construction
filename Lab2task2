using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string input = "if (a == b && c != d && x >= y && z <= w && p > q && r < s) { }";
        string pattern = @"(==|!=|>=|<=|>|<)";

        // Find all matches
        MatchCollection matches = Regex.Matches(input, pattern);

        // Print the matches
        Console.WriteLine("Relational operators found:");
        foreach (Match match in matches)
        {
            Console.WriteLine(match.Value);
        }
    }
}
