using System;
using System.Text;

class LexicalAnalyzer
{
    private const int BufferSize = 10; 
    private char[] buffer1 = new char[BufferSize];
    private char[] buffer2 = new char[BufferSize];
    private int currentBuffer = 1;
    private int position = 0; 
    private string input; 
    private int inputLength; 
    private int inputPosition = 0;  

    public LexicalAnalyzer(string input)
    {
        this.input = input;
        this.inputLength = input.Length;
        FillBuffer(buffer1); 
    }

    private void FillBuffer(char[] buffer)
    {
        for (int i = 0; i < BufferSize; i++)
        {
            if (inputPosition < inputLength)
            {
                buffer[i] = input[inputPosition++];
            }
            else
            {
                buffer[i] = '\0'; 
            }
        }
    }

    private char GetNextChar()
    {
        char[] currentBufferArray = currentBuffer == 1 ? buffer1 : buffer2;
        if (position >= BufferSize)
        {
            if (currentBuffer == 1)
            {
                FillBuffer(buffer2);
                currentBuffer = 2;
            }
            else
            {
                FillBuffer(buffer1);
                currentBuffer = 1;
            }
            position = 0;
            currentBufferArray = currentBuffer == 1 ? buffer1 : buffer2;
        }

        char nextChar = currentBufferArray[position++];
        return nextChar;
    }

    public void Tokenize()
    {
        StringBuilder token = new StringBuilder();
        char ch;

        while ((ch = GetNextChar()) != '\0')
        {
            if (char.IsWhiteSpace(ch))
            {
                if (token.Length > 0)
                {
                    Console.WriteLine($"Token: {token}");
                    token.Clear();
                }
            }
            else if (char.IsLetter(ch))
            {
                token.Append(ch); 
            }
            else if (char.IsDigit(ch))
            {
                token.Append(ch); 
            }
            else
            {
                if (token.Length > 0)
                {
                    Console.WriteLine($"Token: {token}");
                    token.Clear();
                }
                Console.WriteLine($"Operator/Symbol: {ch}");
            }
        }

        if (token.Length > 0)
        {
            Console.WriteLine($"Token: {token}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        string input = "int a = 10 + 20;";
        LexicalAnalyzer analyzer = new LexicalAnalyzer(input);
        analyzer.Tokenize();
    }
}
