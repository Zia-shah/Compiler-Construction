using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        // Regular expression for floating point numbers with length <= 6
        string pattern = @"^[+-]?\d{1,3}(\.\d{1,3})?$|^[+-]?\.\d{1,3}$";

        // Test strings
        string[] testStrings = {
            "123",        // valid
            "-12.34",     // valid
            "+0.567",     // valid
            ".678",       // valid
            "0.5",        // valid
            "123456",     // invalid
            "1.2345",     // invalid
            "+1234",      // invalid
            ".1234"       // invalid
        };

        // Check each string against the regex
        foreach (var test in testStrings)
        {
            bool isMatch = Regex.IsMatch(test, pattern);
            Console.WriteLine($"{test}: {(isMatch ? "Valid" : "Invalid")}");
        }
    }
}
