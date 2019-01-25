using System;
using System.Collections.Generic;

namespace lab3_CLR
{
    public class ApplicatonManager
    {
        private Parser parser;
        private UserManager userManager = new UserManager();

        private List<string> subCommandKists;
        private readonly List<string> supportedCommandsList = new List<string>()
        {
            "--l <login> --p <password> - user login and password",
            "user info - information about current user",
            "file upload \"<path-to-file>\" - uploading of file, located in path-to-file, to storage",
            "file download \"<file-name>\" \"<destination-path>\" - downloading file with name file-name from the repository to the directory destinatin-path",
            "file move \"<source-file-name>\" \"<destination-file-name>\" - file renaming: from the path source-file-name to destination-file-name",
            "file remove \"<file-name>\" - remote file-name from storage",
            "file info \"<file-name>\" - information about file with name <file-name>",
            "file export \"<destination-path>\" --format <format> - saving meta information about files in the specified path destination-path",
            "file export --info - display the list of supported formats",
            "exit - exit from the program"
        };

        public void ExecuteCommand(string command)
        {
            parser = new Parser();

            if (parser.IsCommandCorrect(command))
            {
                subCommandKists = parser.GetListOfSubCommands();

                switch (subCommandKists[0])
                {
                    case "-help":
                    {
                        ShowSupportedCommands();
                        break;
                    }
                    case "exit":
                    {
                        CloseApplication();
                        break;
                    }
                    case "--l":
                    {
                        if (userManager.IsUserLogged)
                        {
                            Console.WriteLine("You are already logged");
                        }
                        else
                        {
                            userManager.Authenticate(subCommandKists);
                        }
                        break;
                    }
                    default:
                    {
                        if (userManager.IsUserLogged)
                        {
                            switch (subCommandKists[0])
                            {
                                case "user":
                                {
                                    //юзерменеджеру сделать что-то
                                    break;
                                }
                                case "file":
                                {
                                    //файлменеджеру сделать что-то
                                    break;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("You are not logged");
                        }
                        break;
                    }                       
                }
            }
            else
            {
                Console.WriteLine("Invalid command");
            }
        }

        private void ShowSupportedCommands()
        {
            Console.ForegroundColor = ConsoleColor.Blue;

            foreach (string supportedCommand in supportedCommandsList)
            {
                Console.WriteLine(supportedCommand);
                Console.WriteLine();
            }

            Console.ResetColor();
        }

        private void CloseApplication()
        {
            Environment.Exit(0);
        }
    }
}


