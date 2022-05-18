using System;
namespace events
{
    public delegate void Print(string message);
    class Worker
    {
        public string Name { get; set; }
        public int Clients_Count { get; set; }
        public int Grant { get; set; }
        public event Print print;

        public void GetNewCount(int User_Clients_Count)
        {
            if (User_Clients_Count < 10)
            {
                print("Остался без премии");
            }
            if (User_Clients_Count > Clients_Count)
            {
                print("Так держать! Получаешь премию!");
            }
            if (User_Clients_Count < Clients_Count && User_Clients_Count >= 10)
            {
                print("В следующий раз попробуй лучше");
            }

            Clients_Count = User_Clients_Count;
            print($"Последнее количество обслуженных клиентов - {User_Clients_Count}");
        }
        public void CalculateGrant()
        {
            if (Clients_Count >= 10)
            {
                Grant = (Clients_Count * 100) + 500;
            }
            else
            {
                Grant = 0;
            }
            print($"Ваша премия - {Grant}");
        }
    }
    class Program
    {
        static void Main()
        {
            Worker Tom = new Worker { Name = "Tom", Clients_Count = 20, Grant = 2500 };
            Tom.print += DisplayMessage;
            Console.WriteLine(Tom.Clients_Count);
            Tom.GetNewCount(30);
            Tom.CalculateGrant();

        }

        static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

    }
}
