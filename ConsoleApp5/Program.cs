using System;
using System.Collections.Generic;

namespace StoreApp
{
    enum Category
    {
        Food,
        Electronics,
        Clothes
    }

    class Product
    {
        public int Code;
        public string Name;
        public double Price;
        public int Quantity;
        public Category Category;

        public void Show()
        {
            Console.WriteLine(
                $"Код: {Code} | Название: {Name} | Цена: {Price} | " +
                $"Количество: {Quantity} | На складе: {(Quantity > 0 ? "Да" : "Нет")} | " +
                $"Категория: {Category}");
        }
    }

    class Program
    {
        static List<Product> products = new List<Product>();
        static int nextCode = 10006;

        static void Main()
        {
            products.Add(new Product { Code = 10001, Name = "Хлеб", Price = 60, Quantity = 10, Category = Category.Food });
            products.Add(new Product { Code = 10002, Name = "Мышь", Price = 1200, Quantity = 5, Category = Category.Electronics });
            products.Add(new Product { Code = 10003, Name = "Футболка", Price = 1500, Quantity = 7, Category = Category.Clothes });
            products.Add(new Product { Code = 10004, Name = "Молоко", Price = 100, Quantity = 20, Category = Category.Food });
            products.Add(new Product { Code = 10005, Name = "Наушники", Price = 2500, Quantity = 3, Category = Category.Electronics });

            while (true)
            {
                Console.WriteLine("\n1 - Показать\n2 - Добавить\n3 - Удалить\n4 - Поставка\n5 - Продать\n6 - Поиск\n0 - Выход");
                Console.Write("Выбор: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    foreach (Product p in products)
                        p.Show();
                }

                else if (choice == "2")
                {
                    Console.Write("Название: ");
                    string name = Console.ReadLine();

                    Console.Write("Цена: ");
                    double price = double.Parse(Console.ReadLine());

                    Console.Write("Количество: ");
                    int quantity = int.Parse(Console.ReadLine());

                    if (string.IsNullOrWhiteSpace(name) || price < 0 || quantity < 0)
                    {
                        Console.WriteLine("Неверные данные!");
                        continue;
                    }

                    products.Add(new Product
                    {
                        Code = nextCode++,
                        Name = name,
                        Price = price,
                        Quantity = quantity,
                        Category = Category.Food
                    });

                    Console.WriteLine("Товар добавлен.");
                }

                else if (choice == "3")
                {
                    Console.Write("Код: ");
                    int code = int.Parse(Console.ReadLine());

                    Product p = products.Find(x => x.Code == code);

                    if (p != null)
                    {
                        products.Remove(p);
                        Console.WriteLine("Удалено.");
                    }
                    else
                        Console.WriteLine("Товар не найден.");
                }

                else if (choice == "4")
                {
                    Console.Write("Код: ");
                    int code = int.Parse(Console.ReadLine());
                    Product p = products.Find(x => x.Code == code);

                    if (p != null)
                    {
                        Console.Write("Количество: ");
                        int count = int.Parse(Console.ReadLine());

                        if (count > 0)
                            p.Quantity += count;
                    }
                }

                else if (choice == "5")
                {
                    Console.Write("Код: ");
                    int code = int.Parse(Console.ReadLine());
                    Product p = products.Find(x => x.Code == code);

                    if (p != null)
                    {
                        Console.Write("Количество: ");
                        int count = int.Parse(Console.ReadLine());

                        if (count > 0 && count <= p.Quantity)
                        {
                            p.Quantity -= count;
                            Console.WriteLine("Продано.");
                        }
                        else
                            Console.WriteLine("Недостаточно товара.");
                    }
                }

                else if (choice == "6")
                {
                    Console.Write("Название: ");
                    string name = Console.ReadLine();

                    foreach (Product p in products)
                        if (p.Name.ToLower().Contains(name.ToLower()))
                            p.Show();
                }

                else if (choice == "0")
                    break;

                else
                    Console.WriteLine("Неверная команда.");
            }
        }
    }
}