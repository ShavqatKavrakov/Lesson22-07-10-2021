using System;
using System.Collections.Generic;
using System.Threading;

namespace Lesson
{
    class Program
    {
        static List<Client> clients = new List<Client>();
        static private decimal difference = 0;
        static private int Id = 0;
        static private int index = 0;
        public static void Main()
        {
            Timer timer = new Timer(Change, null, 2000, 3000);
            ParameterizedThreadStart collback;
            object par = null;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1.Add new client\n2.Update client\n3.Delete client\n4.Show all clients\n5.Exit");
                int n = int.Parse(Console.ReadLine());
                switch (n)
                {
                    case 1:
                        {
                            Console.WriteLine("Input Id and Balance");
                            par = new Client() { Id = int.Parse(Console.ReadLine()), Balance = decimal.Parse(Console.ReadLine()) };
                            Insert(par);
                            collback = Insert;
                        }
                        break;
                    case 2:
                        {
                            Select(par);
                            Console.WriteLine("Choose one of Id and updated balance");
                            par = new Client() { Id = int.Parse(Console.ReadLine()), Balance = decimal.Parse(Console.ReadLine()) };
                            Update(par);
                            collback = Update;
                        }
                        break;
                    case 3:
                        {
                            Console.WriteLine("Choose one of Id");
                            Select(par);
                            par = int.Parse(Console.ReadLine());
                            Delete(par);
                            collback = Delete;
                        }
                        break;
                    case 4:
                        {
                            Select(par);
                            collback = Select;
                        }
                        break;
                    case 5:
                        return;
                }
            }

        }
        static void Insert(object ob)
        {
            clients.Add((Client)ob);
        }
        static void Update(object ob)
        {
            Client client = (Client)ob;
            Id = client.Id;
            index = clients.FindIndex(a => a.Id == client.Id);
            Console.WriteLine(index);
            difference = client.Balance - clients[index].Balance;
            clients[index].Balance = client.Balance;
        }
        static void Delete(object ob)
        {
            int id = (int)ob;
            index = clients.FindIndex(a => a.Id == id);
            clients.RemoveAt(index);
            Console.WriteLine("Client successfully removed");
            Thread.Sleep(2000);
        }
        static void Select(object ob)
        {
            foreach (var client in clients)
            {
                Console.WriteLine($"Id: {client.Id}, Balance: {client.Balance}");
            }
            Thread.Sleep(2000);
        }
        static void Change(object ob)
        {
            if (difference == 0) return;
            if (difference < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Id: {Id},Balance before defference: {clients[index].Balance - difference},Balance after difference:{clients[index].Balance},difference -{difference} ");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Id: {Id},Balance before defference: {clients[index].Balance - difference} and after difference:{clients[index].Balance} and +{difference} ");
                Console.ResetColor();

            }
            difference = 0;
        }

    }

}