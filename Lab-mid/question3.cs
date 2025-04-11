using System;
using System.Collections.Generic;

class SymbolTableProgram
{
    // Symbol table entry class
    class SymbolEntry
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public int LineNumber { get; set; }

        public override string ToString()
        {
            return $"{LineNumber,-8} {Name,-15} {Type,-10} {Value}";
        }
    }

    // Symbol table storage
    static List<SymbolEntry> symbolTable = new List<SymbolEntry>();
    static int currentLineNumber = 0;

    static void Main(string[] args)
    {
        Console.WriteLine("Symbol Table System - Enter variable declarations (type 'exit' to quit)");
        Console.WriteLine("Format: [type] [name] = [value];  (e.g., 'int val33 = 999;')");
        Console.WriteLine("Note: Variable names must contain a palindrome substring of length ≥ 3\n");

        while (true)
        {
            Console.Write("> ");
            string input = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(input))
                continue;

            if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                break;

            currentLineNumber++;
            ProcessDeclaration(input);
        }

        DisplaySymbolTable();
    }

    static void ProcessDeclaration(string input)
    {
        try
        {
            // Basic parsing of the input
            string[] parts = input.Split(new[] { ' ', '=', ';' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length < 3)
            {
                Console.WriteLine("Error: Invalid format. Use: [type] [name] = [value];");
                return;
            }

            string type = parts[0];
            string name = parts[1];
            string value = parts[2];

            // Validate variable name contains a palindrome substring of length ≥ 3
            if (!ContainsValidPalindrome(name))
            {
                Console.WriteLine($"Rejected: '{name}' doesn't contain a palindrome substring of length ≥ 3");
                return;
            }

            // Add to symbol table
            symbolTable.Add(new SymbolEntry
            {
                Name = name,
                Type = type,
                Value = value,
                LineNumber = currentLineNumber
            });

            Console.WriteLine($"Added: {name}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing input: {ex.Message}");
        }
    }

    // Checks if the string contains any palindrome substring of minimum length
    static bool ContainsValidPalindrome(string s, int minLength = 3)
    {
        if (s.Length < minLength)
            return false;

        for (int i = 0; i <= s.Length - minLength; i++)
        {
            for (int j = i + minLength; j <= s.Length; j++)
            {
                string substring = s.Substring(i, j - i);
                if (IsPalindrome(substring))
                {
                    Console.WriteLine($"Found valid palindrome: '{substring}' in '{s}'");
                    return true;
                }
            }
        }

        return false;
    }

    // Custom palindrome checker
    static bool IsPalindrome(string s)
    {
        int left = 0;
        int right = s.Length - 1;

        while (left < right)
        {
            if (s[left] != s[right])
                return false;

            left++;
            right--;
        }

        return true;
    }

    static void DisplaySymbolTable()
    {
        if (symbolTable.Count == 0)
        {
            Console.WriteLine("\nSymbol table is empty.");
            return;
        }

        Console.WriteLine("\nSymbol Table:");
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine("Line #    Name            Type       Value");
        Console.WriteLine("--------------------------------------------------");

        foreach (var entry in symbolTable)
        {
            Console.WriteLine(entry);
        }

        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine($"Total entries: {symbolTable.Count}");
    }
}
int val33 = 999;
Found valid palindrome: 'l33' in 'val33'
Added: val33
