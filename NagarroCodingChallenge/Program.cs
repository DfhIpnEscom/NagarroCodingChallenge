using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NagarroCodingChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach(var input in args)
            {
                Console.WriteLine(input);
                Console.WriteLine(ReplaceWIthNumberOfDistinctChartacters(input));
                Console.WriteLine("---");
            }
            Console.ReadLine();
        }

        static string ReplaceWIthNumberOfDistinctChartacters(string input)
        {
            if (string.IsNullOrEmpty(input))
                return "Invalid input";
            
            Dictionary<char, int> distinctCharacters = new Dictionary<char, int>(); //Save the characters between start and end of the word
            StringBuilder result = new StringBuilder();
            Regex nonAlphabeticCharacter = new Regex(@"[^a-zA-Z0-9]");

            int start = 0;            
            int numberOfDistinctCharacters = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (nonAlphabeticCharacter.IsMatch(input[i].ToString())) //find if the character is a non-alphabetic
                {
                    if (start != i && i - start > 1) //if word has at least 3 letters or not is a non-alphabetic sequence of letters
                    {
                        numberOfDistinctCharacters = distinctCharacters.Count();
                        if (distinctCharacters[input[i - 1]] == 1) //My process count the last letter, here validate if the last letter is unique, in that case less one in other case save the value
                            numberOfDistinctCharacters--;
                        result.Append($"{input[start]}{numberOfDistinctCharacters}{input[i - 1]}{input[i]}"); //join the start, number, end and non-alphabetic chars to create a new word
                    }
                    else
                    {
                        if(i - start == 1) //words with only one letter
                            result.Append(input[start]);
                        result.Append(input[i]); //only a non-alphabetic letter
                    }
                    
                    distinctCharacters = new Dictionary<char, int>(); //clean up the dictionary for a new count
                    start = i + 1; // move the start of the word
                    continue;
                }
                if (start != i)
                {
                    if (distinctCharacters.ContainsKey(input[i])) // count the time apear the same character
                    {
                        distinctCharacters[input[i]]++;
                    }
                    else
                    {
                        distinctCharacters.Add(input[i], 1); // save the character
                    }
                }
            }
            if (start < input.Length) // works for only 1 word or with the last word in a sentence
            {
                numberOfDistinctCharacters = distinctCharacters.Count();
                if (distinctCharacters[input[input.Length - 1]] == 1) //My process count the last letter, here validate if the last letter is unique, in that case less one in other case save the value
                    numberOfDistinctCharacters--;
                result.Append($"{input[start]}{numberOfDistinctCharacters}{input[input.Length - 1]}");//join the start, number, end and non-alphabetic chars to create a new word
            }
            return result.ToString();
        }
    }
}
