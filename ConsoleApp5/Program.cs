using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Введите текст длинной минимум в 100 символов: ");

        string text = Console.ReadLine();

        while (text.Length < 100)
        {
            Console.WriteLine("Текст должен быть >100 символов");
            Console.WriteLine("Введите текст еще раз: ");

            text = Console.ReadLine();
        }

        Console.WriteLine("Текст принят.");
        Console.WriteLine($"Количество символов: {text.Length}");

        int wordCount = 0;
        bool inWord = false;

        foreach (char symbol in text)
        {
            if (symbol != ' ')
            {
                if (inWord == false)
                {
                    wordCount++;
                    inWord = true;
                }
            }
            else
            {
                inWord = false;
            }
        }

        Console.WriteLine($"Количество слов: {wordCount}");
    }
}