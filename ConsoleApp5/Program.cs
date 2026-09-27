using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static void Main()
    {
        List<string> all_information = new List<string>();

        while (true)
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

            string all_vocabulary = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяabcdefghijklmnopqrstuvwxyz";
            int[] vocabulary_count = new int[59];
            string count_of_letters = "Текст содержит:\n";
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
                    if (shortestWord == 0 || comparison < shortestWord)
                    {
                        shortestWord = comparison;
                    }
                    inWord = false;
                    comparison = 0;
                }
            }

            foreach (char symbol in text)
            {
                int index = all_vocabulary.IndexOf(symbol);
                if (index != -1)
                {
                    vocabulary_count[index]++;
                }
            }

            for (int i = 0; i < all_vocabulary.Length; i++)
            {
                if (vocabulary_count[i] > 0)
                {
                    count_of_letters += $"{vocabulary_count[i]} - {all_vocabulary[i]}\n";
                }
            }
            if (wordCount == 1)
            {
                shortestWord = longestWord;
                countSentences = 1;
            }
            string information = "";
            information += ($"\n\nКоличество символов: {text.Length}\n");
            information += ($"Количество слов: {wordCount}\n");
            information += ($"Самое длинное слово содержит {longestWord} символов\n");
            information += ($"Количество предложений: {countSentences}\n");
            information += ($"Самое короткое слово содержит {shortestWord} символов\n");
            information += ($"Количество гласных букв: {count_glas}\n");
            information += ($"{count_of_letters}");
            all_information.Add(information);
            Console.WriteLine(all_information[all_information.Count - 1]);
            Console.WriteLine("Введите ваше действие: \n1 - Написать еще один текст\n2 - Выйти\n3 - Посмотреть статистику по всем введенным текстам");
            string answer = Console.ReadLine();
            if (answer == "2")
            {
                break;
            }
            else if (answer == "3")
            {
                foreach (string pon_information in all_information)
                {
                    Console.WriteLine(pon_information);
                }
            }
            else
            { }
        }
    }
}