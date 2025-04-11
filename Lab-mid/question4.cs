using System;
using System.Collections.Generic;
using System.Linq;

class GrammarAnalyzer
{
    static Dictionary<string, List<string>> grammar = new Dictionary<string, List<string>>();
    static Dictionary<string, HashSet<string>> firstSets = new Dictionary<string, HashSet<string>>();
    static Dictionary<string, HashSet<string>> followSets = new Dictionary<string, HashSet<string>>();
    static HashSet<string> nonTerminals = new HashSet<string>();
    static HashSet<string> terminals = new HashSet<string>();

    static void Main()
    {
        Console.WriteLine("Grammar FIRST and FOLLOW Set Calculator");
        Console.WriteLine("Enter grammar rules (one per line, format: A → B C | D)");
        Console.WriteLine("Enter 'done' when finished");

        // Input grammar
        while (true)
        {
            Console.Write("> ");
            string input = Console.ReadLine().Trim();
            if (input.ToLower() == "done") break;

            try
            {
                ParseGrammarRule(input);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Validate grammar
        if (HasLeftRecursion())
        {
            Console.WriteLine("Grammar invalid for top-down parsing: Left recursion detected.");
            return;
        }

        // Compute FIRST and FOLLOW sets
        try
        {
            ComputeFirstSets();
            ComputeFollowSets();

            // Display results
            Console.WriteLine("\nFIRST Sets:");
            foreach (var nt in nonTerminals.OrderBy(x => x))
            {
                Console.WriteLine($"FIRST({nt}) = {{{string.Join(", ", firstSets[nt])}}}");
            }

            Console.WriteLine("\nFOLLOW Sets:");
            foreach (var nt in nonTerminals.OrderBy(x => x))
            {
                Console.WriteLine($"FOLLOW({nt}) = {{{string.Join(", ", followSets[nt])}}}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void ParseGrammarRule(string rule)
    {
        string[] parts = rule.Split(new[] { "→", "->" }, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 2) throw new Exception("Invalid rule format");

        string lhs = parts[0].Trim();
        nonTerminals.Add(lhs);

        string[] productions = parts[1].Split('|');
        grammar[lhs] = new List<string>();

        foreach (string prod in productions)
        {
            string production = prod.Trim();
            if (string.IsNullOrEmpty(production)) continue;

            grammar[lhs].Add(production);

            // Collect terminals
            foreach (string symbol in production.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (!nonTerminals.Contains(symbol) && symbol != "ε")
                {
                    terminals.Add(symbol);
                }
            }
        }
    }

    static bool HasLeftRecursion()
    {
        foreach (var nt in nonTerminals)
        {
            foreach (string production in grammar[nt])
            {
                string firstSymbol = production.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
                if (firstSymbol == nt)
                {
                    return true;
                }
            }
        }
        return false;
    }

    static void ComputeFirstSets()
    {
        foreach (string nt in nonTerminals)
        {
            firstSets[nt] = new HashSet<string>();
        }

        bool changed;
        do
        {
            changed = false;
            foreach (string nt in nonTerminals)
            {
                foreach (string production in grammar[nt])
                {
                    string[] symbols = production.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (symbols.Length == 0) continue;

                    HashSet<string> originalFirst = new HashSet<string>(firstSets[nt]);
                    firstSets[nt].UnionWith(ComputeFirst(symbols));
                    if (!originalFirst.SetEquals(firstSets[nt]))
                    {
                        changed = true;
                    }
                }
            }
        } while (changed);
    }

    static HashSet<string> ComputeFirst(string[] symbols)
    {
        var result = new HashSet<string>();
        if (symbols.Length == 0) return result;

        string firstSymbol = symbols[0];
        if (terminals.Contains(firstSymbol) || firstSymbol == "ε")
        {
            result.Add(firstSymbol);
            return result;
        }

        result.UnionWith(firstSets[firstSymbol]);

        if (firstSets[firstSymbol].Contains("ε"))
        {
            if (symbols.Length > 1)
            {
                result.UnionWith(ComputeFirst(symbols.Skip(1).ToArray()));
                result.Remove("ε");
            }
            else
            {
                result.Add("ε");
            }
        }

        return result;
    }

    static void ComputeFollowSets()
    {
        foreach (string nt in nonTerminals)
        {
            followSets[nt] = new HashSet<string>();
        }

        // Start symbol gets $ in FOLLOW set
        string startSymbol = nonTerminals.First();
        followSets[startSymbol].Add("$");

        bool changed;
        do
        {
            changed = false;
            foreach (string nt in nonTerminals)
            {
                foreach (string production in grammar[nt])
                {
                    string[] symbols = production.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < symbols.Length; i++)
                    {
                        if (!nonTerminals.Contains(symbols[i])) continue;

                        HashSet<string> originalFollow = new HashSet<string>(followSets[symbols[i]]);
                        if (i < symbols.Length - 1)
                        {
                            string[] remaining = symbols.Skip(i + 1).ToArray();
                            HashSet<string> firstOfRemaining = ComputeFirst(remaining);

                            followSets[symbols[i]].UnionWith(firstOfRemaining);
                            followSets[symbols[i]].Remove("ε");

                            if (firstOfRemaining.Contains("ε"))
                            {
                                followSets[symbols[i]].UnionWith(followSets[nt]);
                            }
                        }
                        else
                        {
                            followSets[symbols[i]].UnionWith(followSets[nt]);
                        }

                        if (!originalFollow.SetEquals(followSets[symbols[i]]))
                        {
                            changed = true;
                        }
                    }
                }
            }
        } while (changed);
    }
}
