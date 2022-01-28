using System;
using System.Collections.Generic;

namespace Wordle_String_Algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            // a list of words is created and initialized
            List<string> words = new List<string>();
            // since each line contains a word, we can read the file line by line and add the line straight to words
            foreach (string line in System.IO.File.ReadLines("sgb-words.txt"))
            {
                words.Add(line);
            }

            // while loop runs until there is one word left or no words left in the list
            while (true)
            {
                for (int i = 1; i <= 3; i++)
                {
                    // GetInput() is called 3 times consecutively to get the 3 different types of input
                    GetInput(i, words);
                }
                // since GetInput() also calls functions to remove words from the list, the list has already been revised
                if (words.Count > 1)
                {
                    // if the word count is greater than one, another iteration of the algorithm needs to be done to narrow
                    // the list again
                    Console.WriteLine("\nPossible Words (" + words.Count + "):");
                    // every word still in the list is printed
                    for (int i = 0; i < words.Count; i++)
                    {
                        Console.WriteLine("  " + (i + 1) + ") " + words[i]);
                    }
                    Console.WriteLine();
                }
                // if word count is equal to 0, then the word does not exist in the list of words
                else if (words.Count == 0)
                {
                    Console.WriteLine("\nNo matches. Word unable to be found.");
                    break;
                }
                // else, there is only one word in the list, so it is printed
                else
                {
                    Console.WriteLine("\nMatch Found: " + words[0]);
                    break;
                }
                
            }

            // the user is able to run the program again if they would like
            Console.WriteLine("\nWould you like to run the program again? (y/n)");
            try
            {
                char toggle = Char.Parse(Console.ReadLine());
                if (toggle == 'y' || toggle == 'Y')
                {
                    Console.WriteLine();
                    Main(args);
                }
            }
            catch (Exception e)
            {

            }
        }

        // function GetInput() uses a switch to get user input for:
        // 1) correct letter correct location, 2) correct letter incorrect location, or 3) incorrect letter
        // ^ this is based on the toggle input value
        static void GetInput(int toggle, List<string> words)
        {
            string[] array = null;
            char letter = '0';
            int index = -1;
            switch (toggle)
            {
                // if toggle is equal to 1, the user is prompted for correct letters in their correct locations
                case 1:
                    // after inputting any correct letters with correct locations, the user can enter next to continue
                    Console.WriteLine("Enter a correct letter & its correct location separated by a space (Example: a 1) or \"next\" to continue:");
                    try
                    {
                        // the user input is split by the space
                        array = Console.ReadLine().Split(' ');
                        Console.WriteLine();
                        // the element at index 0 is "next", then the switch is exited
                        if (array[0] == "next") break;
                        // else the values are parsed accordingly
                        letter = Char.Parse(array[0].ToLower());
                        index = Int32.Parse(array[1]);
                        // index must be between 1 and 5 inclusive
                        if (index < 1 || index > 5)
                        {
                            // if not, GetInput is called again with the same parameters so the user can input a valid input
                            GetInput(1, words);
                            break;
                        }
                    }
                    catch (Exception e)
                    {
                        // if any exceptions occur, GetInput is called again with the same parameters so the user can input a valid input
                        GetInput(1, words);
                        break;
                    }
                    // if the input is valid, CorrectLetterCorrectPlace() is called to shorten the list
                    words = CorrectLetterCorrectPlace(words, letter, index);
                    // the results are inputted to the user
                    Console.WriteLine("Input = '" + letter + " " + index + "'. List reduced to " + words.Count + " words.\n");
                    // GetInput() is called again with the same parameters to give the user a chance to input another correct letter
                    GetInput(1, words);
                    break;
                // if toggle is equal to 2, case 2 runs which if for correct letters in incorrect locations
                case 2:
                    // after inputting any correct letters with incorrect locations, the user can enter next to continue
                    Console.WriteLine("Enter a correct letter & its incorrect location separated by a space (Example: b 2) or \"next\" to continue:");
                    // this try catch block is essentially the same as above
                    try
                    {
                        array = Console.ReadLine().Split(' ');
                        Console.WriteLine();
                        if (array[0] == "next") break;
                        letter = Char.Parse(array[0].ToLower());
                        index = Int32.Parse(array[1]);
                        if (index < 1 || index > 5)
                        {
                            GetInput(1, words);
                            break;
                        }
                    }
                    catch (Exception e)
                    {
                        GetInput(2, words);
                        break;
                    }
                    // if the inputs are valid, CorrectLetterWrongPlace() is called to shorten the list of words accordingly
                    words = CorrectLetterWrongPlace(words, letter, index);
                    // the results are printed
                    Console.WriteLine("Input = '" + letter + " " + index + "'. List reduced to " + words.Count + " words.\n");
                    // GetInput() is called again so the user can enter more correct letters in incorrect locations
                    GetInput(2, words);
                    break;
                // case 3 is for incorrerct letters
                case 3:
                    // the user is prompted
                    Console.WriteLine("Enter any incorrect letters separated by spaces (Example: a b c) or \"next\" to continue:");
                    string input;
                    try
                    {
                        // the input is read and then split by the spaces
                        input = Console.ReadLine();
                        array = input.Split(' ');
                        Console.WriteLine();
                        // if next is inputted, the switch is broken out of
                        if (array[0] == "next") break;
                        char check;
                        // for every element in the array, its value is converted to lowercase
                        foreach (string element in array)
                        {
                            check = Char.Parse(element.ToLower());
                        }
                    }
                    catch (Exception e)
                    {
                        // for any exceptions, the GetInput() function is called again
                        GetInput(3, words);
                        break;
                    }
                    // for every element in the wrong letter array, function WrongLetter is called
                    foreach (string element in array)
                    {
                        words = WrongLetter(words, Char.Parse(element));
                    }
                    // the result is printed
                    Console.WriteLine("Input = '" + input + "'. List reduced to " + words.Count + " words.\n");
                    break;
            }

         
        }

        // function CorrectLetterCorrectPlace() reduces the list of words by correct letters in correct positions
        static List<string> CorrectLetterCorrectPlace(List<string> words, char letter, int index)
        {
            // the list is iterated, and if the word does not contain the correct letter at the specified location, the word is removed from 
            // the list
            for (int i = 0; i < words.Count; i++)
            {
                if (words[i][index - 1] != letter)
                {
                    words.RemoveAt(i);
                    i--;
                }
            }
            // words is returned
            return words;
        }

        // function CorrectLetterWrongPlace() reduces the list of words by correct letters in incorrect positions
        static List<string> CorrectLetterWrongPlace(List<string> words, char letter, int index)
        {
            // the list is iterated through
            for (int i = 0; i < words.Count; i++)
            {
                // if the word does not contain the letter, the word is removed from the list
                if (!words[i].Contains(letter))
                {
                    words.RemoveAt(i);
                    i--;
                    continue;
                }
                // if the word contains the letter in the incorrect location, the word is removed from the list
                if (words[i][index - 1] == letter)
                {
                    words.RemoveAt(i);
                    i--;
                }
            }
            // words is returned
            return words;
        }

        // function WrongLetter() reduces the list of words by incorrect letters
        static List<string> WrongLetter(List<string> words, char letter)
        {
            // the list is iterated through
            for (int i = 0; i < words.Count; i++)
            {
                // if the word contains the letter, it is removed from the list
                if (words[i].Contains(letter))
                {
                    words.RemoveAt(i);
                    i--;
                }
            }
            // words is returned
            return words;
        }
    }
}
