using System;
using System.Threading;

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
        text = text.ToLower().Trim();

        int count_sogl = 0;
        int count_glas = 0;
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
                {
                    if (symbol == 'а' || symbol == 'о' || symbol == 'у' || symbol == 'ы' ||
                        symbol == 'э' || symbol == 'я' || symbol == 'ё' || symbol == 'е' ||
                        symbol == 'ю' || symbol == 'и' ||
                        symbol == 'a' || symbol == 'e' || symbol == 'i' || symbol == 'o' ||
                        symbol == 'u')
                    {
                        count_glas++;
                    }

                    if (symbol == 'б' || symbol == 'в' || symbol == 'г' || symbol == 'д' ||
                        symbol == 'ж' || symbol == 'з' || symbol == 'й' || symbol == 'к' ||
                        symbol == 'л' || symbol == 'м' || symbol == 'н' || symbol == 'п' ||
                        symbol == 'р' || symbol == 'с' || symbol == 'т' || symbol == 'ф' ||
                        symbol == 'х' || symbol == 'ц' || symbol == 'ч' || symbol == 'ш' ||
                        symbol == 'щ' ||
                        symbol == 'b' || symbol == 'c' || symbol == 'd' || symbol == 'f' ||
                        symbol == 'g' || symbol == 'h' || symbol == 'j' || symbol == 'k' ||
                        symbol == 'l' || symbol == 'm' || symbol == 'n' || symbol == 'p' ||
                        symbol == 'q' || symbol == 'r' || symbol == 's' || symbol == 't' ||
                        symbol == 'v' || symbol == 'w' || symbol == 'x' || symbol == 'y' ||
                        symbol == 'z')
                    {
                        count_sogl++;
                    }
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

        if (wordCount == 1)
        {
        shortestWord = longestWord;
            countSentences = 1;
        }

        Console.WriteLine($"Количество символов: {text.Length}");
        Console.WriteLine($"Количество слов: {wordCount}");
        Console.WriteLine($"Самое длинное слово содержит {longestWord} символов");
        Console.WriteLine($"Количество предложений: {countSentences}");
        Console.WriteLine($"Самое короткое слово содержит {shortestWord} символов");
        Console.WriteLine($"Количество гласных букв: {count_glas}");
        Console.WriteLine($"Количество согласных букв: {count_sogl}");
    }
}