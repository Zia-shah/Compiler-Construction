using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Assuming student ID ends with "23" - replace with actual last two digits
        int studentIdSuffix = 23;
        
        string input = "x:userinput; y:userinput; z:4; result: x * y + z;";
        
        // Extract variables and values
        Dictionary<string, string> variables = new Dictionary<string, string>();
        string[] parts = input.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        
        foreach (string part in parts)
        {
            string trimmed = part.Trim();
            if (trimmed.Contains(":"))
            {
                string[] keyValue = trimmed.Split(':');
                string key = keyValue[0].Trim();
                string value = keyValue[1].Trim();
                
                if (key != "result")
                {
                    variables[key] = value;
                }
            }
        }
        
        // Replace "userinput" with actual values (using student ID suffix for one variable)
        int var23 = studentIdSuffix; // Using student ID in variable name
        variables["x"] = "5"; // Example value for x
        variables["y"] = "7"; // Example value for y
        
        // Parse values
        int x = int.Parse(variables["x"]);
        int y = int.Parse(variables["y"]);
        int z = int.Parse(variables["z"]);
        
        // Perform calculation
        int result = x * y + z;
        
        // Display output
        Console.WriteLine($"x = {x}");
        Console.WriteLine($"y = {y}");
        Console.WriteLine($"z = {z}");
        Console.WriteLine($"var{studentIdSuffix} = {var23}");
        Console.WriteLine($"Result = {result}");
    }
}
