using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Введите текст длинной минимум в 100 символов: ");

        string text = Console.ReadLine();

        while (text.Length<100)
        {
            Console.WriteLine("Текст должен быть >100 символов");
            Console.WriteLine("Введите текст еще раз: ");

            text = Console.ReadLine();
        }
    }
}