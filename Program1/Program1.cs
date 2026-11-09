using System;

class Program //Class-based program structure 
{
    static void Main()
    {   //Strong Static Typing, Mutable Variable
        bool playAgain = true;

        //Iterative Control Flow
        while (playAgain)
        {
            //Console I/O - Terminal Screen Management 
            Console.Clear();
            Console.WriteLine("*********************************************");
            Console.WriteLine("       C# Reverse Number Guessing Game       ");
            Console.WriteLine("*********************************************");
            Console.WriteLine("Think of a number between 1 and 100 in your head.");
            Console.WriteLine("For each guess, respond with:");
            Console.WriteLine("  'h' if the guess is too high");
            Console.WriteLine("  'l' if the guess is too low");
            Console.WriteLine("  'c' if the guess is correct");
            Console.WriteLine("Press ENTER when you are ready to begin...");
            Console.ReadLine();


            //Data Types - int (32-bit Signed Integer Value)
            int low = 1;
            int high = 100;
            int attempts = 0;
            bool guessed = false;


            //Control Flow - Condition-Controlled Loop with Compound Boolean Logic
            while (low <= high && !guessed)
            {
                // Binary search picks the midpoint
                // Game will always guess in 7 guesses or less since log(2)(100) ~ 7
                int guess = low + (high - low) / 2;

                // Unary Increment Operator
                attempts++;

                Console.Write($"\nAttempt {attempts}: Is your number {guess}? (h/l/c): ");
                
                // Null Safety Check - Input Handling 
                string? input = Console.ReadLine()?.Trim().ToLower();

                // Multi-Way Branching - Switch Selection Statement
                switch (input)
                {
                    case "c":
                        Console.WriteLine($"\nSuccess! Guessed your number {guess} in {attempts} attempt(s)!");
                        guessed = true;
                        //Jump Statement 
                        break;

                    case "l":
                        // Guess is too high -> Target is lower
                        high = guess - 1;
                        break;

                    case "h":
                        // Guess is too low -> Target is higher
                        low = guess + 1;
                        break;

                    // Exception Handling
                    default:
                        Console.WriteLine("Invalid input. Please enter 'h' (too high), 'l' (too low), or 'c' (correct).");
                        attempts--; 
                        break;
                }
            }

            if (!guessed)
            {
                Console.WriteLine("\nContradiction detected! The answers provided do not lead to a valid whole number between 1 and 100.");
            }

            // Prompt to play again
            // Nested Control Flow 
            while (true)
            {
                Console.Write("\nWould you like to play again? (y/n): ");
                string? response = Console.ReadLine()?.Trim().ToLower();

                // Conditional Branching - Strict Equality Comparison 
                if (response == "y")
                {
                    playAgain = true;
                    break;
                }
                else if (response == "n")
                {
                    playAgain = false;
                    Console.WriteLine("\nThanks for playing! Goodbye.");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option. Please enter 'y' for yes or 'n' for no.");
                }
            }
        }
    }
}