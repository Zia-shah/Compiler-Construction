using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        // The regular expression for logical operators and parentheses
        string pattern = @"\s*(&&|\|\||!|\(|\))\s*";

        // Test string with logical operators and parentheses
        string input = "x && y || !z (x || y)";

        // Create a Regex object with the pattern
        Regex regex = new Regex(pattern);

        // Find all matches
        MatchCollection matches = regex.Matches(input);

        // Output the matches
        foreach (Match match in matches)
        {
            Console.WriteLine($"Found: {match.Value}");
        }
    }
}
