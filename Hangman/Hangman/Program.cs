using System;
using System.Collections.Generic;

namespace Hangman
{
    internal class Program
    {
        private static Random random = new Random();
        private static List<string> wordDictionary = new List<string> { "davit", "car", "house", "window", "hello", "phone", "wallet", "laptop" };

        private static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Hangman :)");
            Console.WriteLine("<--------------------------------->");

            string randomWord = SelectRandomWord();
            int lengthOfWordToGuess = randomWord.Length;

            PlayHangman(randomWord, lengthOfWordToGuess);

            Console.WriteLine("\r\nThe word was: " + randomWord);
            Console.WriteLine("Thank You for Playing!");
        }

        private static string SelectRandomWord()
        {
            int i = random.Next(wordDictionary.Count);
            return wordDictionary[i];
        }

        private static void PlayHangman(string randomWord, int lengthOfWordToGuess)
        {
            int amountOfTimesWrong = 0;
            List<char> currentLettersGuessed = new List<char>();
            int currentLettersRight = 0;

            while (amountOfTimesWrong != 6 && currentLettersRight != lengthOfWordToGuess)
            {
                DisplayCurrentGameState(currentLettersGuessed, randomWord);
                char letterGuessed = GetLetterGuessFromUser(currentLettersGuessed);

                if (currentLettersGuessed.Contains(letterGuessed))
                {
                    Console.WriteLine("\r\n Letter Already Guessed");
                }
                else
                {
                    UpdateGameState(letterGuessed, randomWord, ref amountOfTimesWrong, currentLettersGuessed, ref currentLettersRight);
                }
            }
            PrintHangman(currentLettersGuessed, randomWord);
        }

        private static void DisplayCurrentGameState(List<char> currentLettersGuessed, string randomWord)
        {
            Console.Write("\nYour Guesses: ");
            foreach (char letter in currentLettersGuessed)
            {
                Console.Write(letter + " ");
            }
            PrintHangman(currentLettersGuessed, randomWord);
            PrintWord(currentLettersGuessed, randomWord);
            PrintLines(currentLettersGuessed, randomWord);
        }

        private static char GetLetterGuessFromUser(List<char> currentLettersGuessed)
        {
            Console.Write("\nGuess a Letter: ");
            char letterGuessed = Console.ReadLine()[0];
            return letterGuessed;
        }

        private static void UpdateGameState(char letterGuessed, string randomWord, ref int amountOfTimesWrong, List<char> currentLettersGuessed, ref int currentLettersRight)
        {
            bool right = false;
            for (int i = 0; i < randomWord.Length; i++)
            {
                if (letterGuessed == randomWord[i])
                {
                    right = true;
                    currentLettersRight++;
                }
            }

            if (right)
            {
                currentLettersGuessed.Add(letterGuessed);
            }
            else
            {
                amountOfTimesWrong++;
                currentLettersGuessed.Add(letterGuessed);
            }
        }

        private static void PrintHangman(List<char> currentLettersGuessed, string randomWord)
        {
            int wrong = CalculateWrongGuesses(currentLettersGuessed, randomWord);

            if (wrong == 0)
            {
                Console.WriteLine("\n+---+");
                Console.WriteLine("    |");
                Console.WriteLine("    |");
                Console.WriteLine("    |");
                Console.WriteLine("   ===");
            }
            else if (wrong == 1)
            {
                Console.WriteLine("\n+---+");
                Console.WriteLine("O   |");
                Console.WriteLine("    |");
                Console.WriteLine("    |");
                Console.WriteLine("   ===");
            }
            else if (wrong == 2)
            {
                Console.WriteLine("\n+---+");
                Console.WriteLine("O   |");
                Console.WriteLine("|   |");
                Console.WriteLine("    |");
                Console.WriteLine("   ===");
            }
            else if (wrong == 3)
            {
                Console.WriteLine("\n+---+");
                Console.WriteLine(" O  |");
                Console.WriteLine("/|  |");
                Console.WriteLine("    |");
                Console.WriteLine("   ===");
            }
            else if (wrong == 4)
            {
                Console.WriteLine("\n+---+");
                Console.WriteLine(" O  |");
                Console.WriteLine("/|\\ |");
                Console.WriteLine("    |");
                Console.WriteLine("   ===");
            }
            else if (wrong == 5)
            {
                Console.WriteLine("\n+---+");
                Console.WriteLine(" O  |");
                Console.WriteLine("/|\\ |");
                Console.WriteLine("/   |");
                Console.WriteLine("   ===");
            }
            else if (wrong == 6)
            {
                Console.WriteLine("\n+---+");
                Console.WriteLine(" O   |");
                Console.WriteLine("/|\\  |");
                Console.WriteLine("/ \\  |");
                Console.WriteLine("    ===");
            }
        }

        private static int CalculateWrongGuesses(List<char> currentLettersGuessed, string randomWord)
        {
            int count = 0;
            foreach (char letter in currentLettersGuessed)
            {
                if (!randomWord.Contains(letter.ToString()))
                {
                    count++;
                }
            }
            return count;
        }

        private static void PrintWord(List<char> currentLettersGuessed, string randomWord)
        {
            int count = 0;
            Console.Write("\r\n");
            foreach (char c in randomWord)
            {
                if (currentLettersGuessed.Contains(c))
                {
                    Console.Write(c + " ");
                }
                else
                {
                    Console.Write("_ ");
                }
                count += 1;
            }
        }

        private static void PrintLines(List<char> currentLettersGuessed, string randomWord)
        {
            Console.Write("\r");
            foreach (char c in randomWord)
            {
                if (currentLettersGuessed.Contains(c))
                {
                    Console.Write(c + " ");
                }
                else
                {
                    Console.Write("_ ");
                }
            }
        }
    }
}
