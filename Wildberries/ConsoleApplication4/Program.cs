using System;
using System.Collections.Generic;

namespace Wildberries
{
    public delegate void Message(string message, ConsoleColor color);
    public class Account
    {
        public event Message Push;
        private float money { get; set; }

        public float Money
        {
            get
            {
                return money;
            }
            set
            {
                if(value > 0) 
                    money = value;
            }
        }
        public void Take(float count)
        {
            if (money >= count)
            {
                money -= count;
                Push($"Со счёта снято {count} рублей, у вас осталось {money} рублей", ConsoleColor.Green);
            }
            else
            {
                Push($"Недостаточно средств", ConsoleColor.Red);
            }
        }
        public void OnMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        public Account(float _money)
        {
            Money = _money;
            Push += OnMessage;
        }
    }
    public class Wildberries
    {
        public List<Product> listProducts = new List<Product>();

        public void Buy(Account acc, string id)
        {
            foreach (Product pr in listProducts)
            {
                if (pr.ID == id)
                {
                    float count = pr.Count;
                    if (acc.Money >= pr.Count)
                    {
                        acc.Push += PushBuy;
                        
                        pr.Amount -= 1;
                        
                        if (pr.Amount <= 0)
                            listProducts.Remove(pr);
                    }
                    acc.Take(count);
                    acc.Push -= PushBuy;
                    
                    break;
                }
            }
            Program.Start(this, acc);
        }
        public void PushBuy(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("Продукт куплен\t");
            Console.ResetColor();
        }
    }

    public abstract class Product
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public float Count { get; set; }
        public int Amount { get; set; }
        public Product(string id, string name, float count, int amount)
        {
            ID = id;
            Name = name;
            Count = count;
            Amount = amount;
        }
    }

    public class Bread : Product
    {
        public Bread(string id, string name, float count, int amount)
            : base(id, name, count, amount) {}
    }
    
    public class SuperBread : Product
    {
        public SuperBread(string id, string name, float count, int amount)
            : base(id, name, count, amount) {}
    }
    
    public class UltraBread : Product
    {
        public UltraBread(string id, string name, float count, int amount)
            : base(id, name, count, amount)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Скидка на Ultra Bread");
            Console.ResetColor();
            Count /= 2;
        }
    }
    public class Banana : Product
    {
        public Banana(string id, string name, float count, int amount)
            : base(id, name, count, amount) {}
    }

    internal class Program
    {
        protected static void SetCharNum(Account acc, Wildberries magazine, string id)
        {
            Console.WriteLine("Введите число 0 чтобы купить");
            
            string charNum = Console.ReadLine();
            if (int.TryParse(charNum, out int x) && x == 0)
            {
                magazine.Buy(acc, id);
            }
            else
            {
                SetCharNum(acc, magazine, id);
            }
        }
        public static void Start(Wildberries magazine, Account acc)
        {
            if (magazine.listProducts.Count != 0)
            {
                foreach (Product pr in magazine.listProducts)
                {
                    Console.WriteLine($"ИД: {pr.ID}, Название: {pr.Name}, Цена: {pr.Count}, Количество: {pr.Amount} штук");
                }

                Console.WriteLine("Введите ИД продукта: \t");
           
                string selectID = Console.ReadLine();

                if (int.TryParse(selectID, out int x))
                {
                    foreach (Product pr in magazine.listProducts)
                    {
                        if (selectID == pr.ID)
                        {
                            SetCharNum(acc, magazine, selectID);
                        }
                        else
                        {
                            Start(magazine, acc);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("ERROR!");
                }
            }
            else
            {
                Console.WriteLine("Товар закончился");
            }
        }
        public static void Main(string[] args)
        {
            Account acc = new Account(1000);
            Wildberries magazine = new Wildberries();
            
            magazine.listProducts.Add(new Bread("1", "Bread", 25, 5));
            magazine.listProducts.Add(new SuperBread("2", "Super Bread", 75, 2));
            magazine.listProducts.Add(new UltraBread("3", "Ultra Bread", 125, 1));
            magazine.listProducts.Add(new Banana("4", "Banana", 100, 10));
           
            Start(magazine, acc);
        }
    }
}