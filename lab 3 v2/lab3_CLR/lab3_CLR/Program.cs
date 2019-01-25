using System;

namespace lab3_CLR
{
    class Program
    {
        static void Main(string[] args)
        {
            ApplicatonManager applicatonManager = new ApplicatonManager();

            while (true)
            {
                applicatonManager.ExecuteCommand(Console.ReadLine());
            }
        }
    }
}
