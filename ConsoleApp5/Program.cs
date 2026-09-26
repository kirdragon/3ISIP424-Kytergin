using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Введите текст длинной минимум в 100 символов: ");

        string text = Console.ReadLine();

        while (text.Length < 100)
        {
            Console.WriteLine("Текст должен быть не менее 100 символов");
            Console.WriteLine("Введите текст еще раз: ");

            text = Console.ReadLine();
        }

        Console.WriteLine("Текст принят.");

        int wordCount = 0;
        bool inWord = false;

        int comparison = 0;
        int longestWord = 0;

        int shortestWord = 0;
        int countSentences = 0;

        foreach (char symbol in text)
        {

            if (symbol == '.')
            {
                countSentences++;
            }

            if (symbol != ' ' && symbol != '.')
            {
                if (inWord == false)
                {
                    wordCount++;
                    inWord = true;
                }

                comparison++;

                if (comparison > longestWord)
                {
                    longestWord = comparison;
                }
            }
            else
            {
                if (shortestWord == 0 || comparison< shortestWord)
                {
                    shortestWord = comparison;
                }
                inWord = false;
                comparison = 0;
            }
        }

        Console.WriteLine($"Количество символов: {text.Length}");
        Console.WriteLine($"Количество слов: {wordCount}");
        Console.WriteLine($"Самое длинное слово содержит {longestWord} символов");
        Console.WriteLine($"Количество предложений: {countSentences}");
        Console.WriteLine($"Самое короткое слово содержит {shortestWord} символов");
    }
}