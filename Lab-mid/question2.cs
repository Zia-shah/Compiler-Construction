using System; // Needed for basic input/output
using System.Text.RegularExpressions; // Needed for using Regex

class Program
{
    static void Main()
    {
        // Ask user to input their mini-language code
        Console.WriteLine("Enter your code:");
        string input = Console.ReadLine(); // Read a line of input from the user

        // Regex pattern:
        // - VarName: starts with a/b/c and ends with digits
        // - SpecialSymbol: looks for special characters in the value
        string pattern = @"(?<VarName>\b[a-cA-C]\w*\d)\s*=\s*[^;]*?(?<SpecialSymbol>[^a-zA-Z0-9\s]+)";
        
        // Create regex object with our pattern
        Regex regex = new Regex(pattern);

        // Find all matches in the user's input
        MatchCollection matches = regex.Matches(input);

        // Display table headers
        Console.WriteLine("\n| VarName | SpecialSymbol | TokenType |");
        Console.WriteLine("|---------|----------------|-----------|");

        // Loop through each match found by regex
        foreach (Match match in matches)
        {
            // Extract variable name from the match
            string varName = match.Groups["VarName"].Value;

            // Extract special symbol from the value assigned to the variable
            string specialSymbol = match.Groups["SpecialSymbol"].Value;

            // Print the result in a table format
            Console.WriteLine($"| {varName,-7} | {specialSymbol,-14} | {"Variable",-9} |");
        }

        // Prompt to exit
        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey(); // Waits for a key press
    }
}
