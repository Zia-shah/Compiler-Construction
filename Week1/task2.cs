using System;
using System.Linq;
using System.Text;

class Program
{
    static void Main()
    {
        // User input
        Console.Write("Enter your First Name: ");
        string firstName = Console.ReadLine().ToLower();

        Console.Write("Enter your Last Name: ");
        string lastName = Console.ReadLine().ToLower();

        Console.Write("Enter your Registration Number (3 digits): ");
        string regNumber = Console.ReadLine();

        Console.Write("Enter your Favourite Movie: ");
        string favMovie = Console.ReadLine();

        Console.Write("Enter your Favourite Food: ");
        string favFood = Console.ReadLine();

        // Generate password
        string password = GeneratePassword(firstName, lastName, regNumber, favMovie, favFood);
        Console.WriteLine($"\nGenerated Password: {password}");
    }

    static string GeneratePassword(string firstName, string lastName, string regNumber, string favMovie, string favFood)
    {
        Random random = new Random();
        StringBuilder password = new StringBuilder();

        // 1. Pick 4 random lowercase letters from first and last name
        string nameLetters = (firstName + lastName).ToLower();
        password.Append(new string(nameLetters.OrderBy(_ => random.Next()).Take(4).ToArray()));

        // 2. Pick 2 digits from registration number
        password.Append(new string(regNumber.OrderBy(_ => random.Next()).Take(2).ToArray()));

        // 3. Pick 1 uppercase letter from first/last name
        string nameUpper = (firstName + lastName).ToUpper();
        password.Append(nameUpper[random.Next(nameUpper.Length)]);

        // 4. Pick 2 special characters from the initials of favorite movie & food
        char specialChar1 = favMovie.Length > 0 ? favMovie[0] : '@'; // First letter of movie
        char specialChar2 = favFood.Length > 0 ? favFood[0] : '!';   // First letter of food
        password.Append($"@{specialChar2}");

        // Shuffle the password to randomize order
        return new string(password.ToString().OrderBy(_ => random.Next()).ToArray());
    }
}
