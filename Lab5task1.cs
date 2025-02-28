using System;
using System.Collections.Generic;

class SymbolTable
{
    private const int TableSize = 100; 
    private LinkedList<SymbolEntry>[] table; 

    public SymbolTable()
    {
        table = new LinkedList<SymbolEntry>[TableSize];
        for (int i = 0; i < TableSize; i++)
        {
            table[i] = new LinkedList<SymbolEntry>();
        }
    }

    private int HashFunction(string key)
    {
        int hash = 0;
        foreach (char c in key)
        {
            hash = (hash * 31 + c) % TableSize; 
        }
        return hash;
    }

    public void Insert(string name, string type, object value)
    {
        int index = HashFunction(name);
        var entry = new SymbolEntry(name, type, value);

        foreach (var existingEntry in table[index])
        {
            if (existingEntry.Name == name)
            {
                Console.WriteLine($"Symbol '{name}' already exists in the symbol table.");
                return;
            }
        }

        table[index].AddLast(entry);
        Console.WriteLine($"Inserted: {entry}");
    }

    public SymbolEntry Lookup(string name)
    {
        int index = HashFunction(name);
        foreach (var entry in table[index])
        {
            if (entry.Name == name)
            {
                Console.WriteLine($"Found: {entry}");
                return entry;
            }
        }

        Console.WriteLine($"Symbol '{name}' not found in the symbol table.");
        return null;
    }

    public void Delete(string name)
    {
        int index = HashFunction(name);
        var node = table[index].First;

        while (node != null)
        {
            if (node.Value.Name == name)
            {
                table[index].Remove(node);
                Console.WriteLine($"Deleted: {node.Value}");
                return;
            }
            node = node.Next;
        }

        Console.WriteLine($"Symbol '{name}' not found in the symbol table.");
    }

    public void Display()
    {
        Console.WriteLine("Symbol Table:");
        for (int i = 0; i < TableSize; i++)
        {
            if (table[i].Count > 0)
            {
                Console.WriteLine($"Bucket {i}:");
                foreach (var entry in table[i])
                {
                    Console.WriteLine($"  {entry}");
                }
            }
        }
    }

    public class SymbolEntry
    {
        public string Name { get; }
        public string Type { get; }
        public object Value { get; set; }

        public SymbolEntry(string name, string type, object value)
        {
            Name = name;
            Type = type;
            Value = value;
        }

        public override string ToString()
        {
            return $"[Name: {Name}, Type: {Type}, Value: {Value}]";
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        SymbolTable symbolTable = new SymbolTable();

        symbolTable.Insert("x", "int", 10);
        symbolTable.Insert("y", "float", 20.5);
        symbolTable.Insert("z", "string", "Hello");

        var entryX = symbolTable.Lookup("x");
        var entryY = symbolTable.Lookup("y");
        var entryW = symbolTable.Lookup("w"); 

        symbolTable.Delete("y");

        symbolTable.Display();
    }
}
