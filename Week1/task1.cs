using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter your password:");
        string password = Console.ReadLine();

        if (IsValidPassword(password))
        {
            Console.WriteLine("Password is valid!");
        }
        else
        {
            Console.WriteLine("Invalid password! Follow the criteria.");
        }
    }

    static bool IsValidPassword(string password)
    {
        // Ensure maximum length is 12 characters
        if (password.Length > 12)
            return false;

        // Regex pattern breakdown:
        // (?=.*[57])   → Must contain at least one of the digits '5' or '7'
        // (?=.*[A-Z])  → At least one uppercase letter
        // (?=(.*[^a-zA-Z0-9]){2,}) → At least two special characters
        // (?=(.*[ziaullahshah]){4,}) → At least four lowercase letters from "ziaullahshah"
        string pattern = @"^(?=.*[57])(?=.*[A-Z])(?=(.*[^a-zA-Z0-9]){2,})(?=(.*[ziaullahshah]){4,}).{1,12}$";

        return Regex.IsMatch(password, pattern);
    }
}
