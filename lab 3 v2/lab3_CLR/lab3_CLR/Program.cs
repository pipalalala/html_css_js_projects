using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3_CLR
{
    class Program
    {
        /*
        
        --l <login> --p <password> 
        user info
        file upload "<path-to-file>" 
        file download \"<file-name>\" \"<destination-path>\" 
        file move \"<source-file-name>\" \"<destination-file-name>\" 
        file remove \"<file-name>\" 
        file info \"<file-name>\" 
        file export \"<destination-path>\" --format <format> 
        file export --info 
        exit

        */
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine();

                string str = Console.ReadLine();

                Parser parser = new Parser();

                if (parser.IsCommandCorrect(str))
                {
                    List<string> list = parser.Parse();

                    foreach (var item in list)
                    {
                        Console.WriteLine(item);
                    }

                    Console.WriteLine("--------------------------------------------");
                }
                else
                {
                    Console.WriteLine("Uncorrect command");
                }
            }
        }
    }
}
