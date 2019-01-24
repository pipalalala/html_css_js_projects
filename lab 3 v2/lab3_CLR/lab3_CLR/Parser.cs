using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3_CLR
{
    class Parser
    {
        public Parser()
        {
        }

        private List<string> subCommandsList = new List<string>();
        private bool isCommandCorrect = false;

        public List<string> Parse(string command)
        {
            string[] commands = command.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            switch (commands[0])
            {
                case "-help":
                {
                    if (commands.Length == 1)
                    {
                        AddSubCommandsToSubCommandsList(commands);
                        isCommandCorrect = true;

                        return subCommandsList;
                    }
                    break;
                }

                case "--l":
                {
                    if (commands.Length == 4 && commands[2] == "--p")
                    {
                        AddSubCommandsToSubCommandsList(commands);
                        isCommandCorrect = true;

                        return subCommandsList;
                    }

                    break;
                }

                case "user":
                {
                    if (commands.Length == 2 && commands[1] == "info")
                    {
                        AddSubCommandsToSubCommandsList(commands);
                        isCommandCorrect = true;

                        return subCommandsList;
                    }

                    break;
                }

                case "file":
                {
                    switch (commands[1])
                    {
                        case "upload":
                        {
                            List<int> positionsOfQuotes = GetCountOfQuotes(command);
                            bool isAnyCharacterBeforeQuote = IsAnyCharacterAfterQuote(command, positionsOfQuotes);

                            if (positionsOfQuotes.Count == 2 && isAnyCharacterBeforeQuote == false)
                            {
                                subCommandsList.Add(commands[0]);
                                subCommandsList.Add(commands[1]);
                                subCommandsList.Add(command.Substring(positionsOfQuotes[0] + 1, positionsOfQuotes[1] - positionsOfQuotes[0] - 1));
                                isCommandCorrect = true;

                                return subCommandsList;
                            }

                            break;
                        }

                        case "download":
                        {
                            List<int> positionsOfQuotes = GetCountOfQuotes(command);

                            bool isAnyCharacter = IsAnyCharacterAfterLastQuote(command, positionsOfQuotes);
                            isAnyCharacter = IsAnyCharacterBetweenNames(command, positionsOfQuotes, isAnyCharacter);

                            if (positionsOfQuotes.Count == 4 && isAnyCharacter == false)
                            {
                                subCommandsList.Add(commands[0]);
                                subCommandsList.Add(commands[1]);
                                subCommandsList.Add(command.Substring(positionsOfQuotes[0] + 1, positionsOfQuotes[1] - positionsOfQuotes[0] - 1));
                                subCommandsList.Add(command.Substring(positionsOfQuotes[2] + 1, positionsOfQuotes[3] - positionsOfQuotes[2] - 1));

                                return subCommandsList;
                            }

                            break;
                        }

                        case "move":
                        {
                            List<int> positionsOfQuotes = GetCountOfQuotes(command);

                            bool isAnyCharacter = IsAnyCharacterAfterLastQuote(command, positionsOfQuotes);
                            isAnyCharacter = IsAnyCharacterBetweenNames(command, positionsOfQuotes, isAnyCharacter);

                            if (positionsOfQuotes.Count == 4 && isAnyCharacter == false)
                            {
                                subCommandsList.Add(commands[0]);
                                subCommandsList.Add(commands[1]);
                                subCommandsList.Add(command.Substring(positionsOfQuotes[0] + 1, positionsOfQuotes[1] - positionsOfQuotes[0] - 1));
                                subCommandsList.Add(command.Substring(positionsOfQuotes[2] + 1, positionsOfQuotes[3] - positionsOfQuotes[2] - 1));

                                return subCommandsList;
                            }

                            break;
                        }

                        case "remove":
                        {
                            List<int> positionsOfQuotes = GetCountOfQuotes(command);
                            bool isAnyCharacterBeforeQuote = IsAnyCharacterAfterQuote(command, positionsOfQuotes);

                            if (positionsOfQuotes.Count == 2 && isAnyCharacterBeforeQuote == false)
                            {
                                subCommandsList.Add(commands[0]);
                                subCommandsList.Add(commands[1]);
                                subCommandsList.Add(command.Substring(positionsOfQuotes[0] + 1, positionsOfQuotes[1] - positionsOfQuotes[0] - 1));

                                isCommandCorrect = true;

                                return subCommandsList;
                            }

                            break;
                        }

                        case "info":
                        {
                            List<int> positionsOfQuotes = GetCountOfQuotes(command);
                            bool isAnyCharacterBeforeQuote = IsAnyCharacterAfterQuote(command, positionsOfQuotes);

                            if (positionsOfQuotes.Count == 2 && isAnyCharacterBeforeQuote == false)
                            {
                                subCommandsList.Add(commands[0]);
                                subCommandsList.Add(commands[1]);
                                subCommandsList.Add(command.Substring(positionsOfQuotes[0] + 1, positionsOfQuotes[1] - positionsOfQuotes[0] - 1));

                                isCommandCorrect = true;

                                return subCommandsList;
                            }

                            break;
                        }

                        case "export":
                        {
                            List<int> positionsOfQuotes = GetCountOfQuotes(command);

                            if (positionsOfQuotes.Count == 2)
                            {
                                bool isAnyCharacter = false;

                                int indexOfEndOfExportSubComand = GetIndexOfEndOfExportSubComand(command);

                                //проверка после export и перед путём
                                isAnyCharacter = IsAnyCharacterBetweenExportAndPath(command, positionsOfQuotes, isAnyCharacter, indexOfEndOfExportSubComand);

                                if (isAnyCharacter == false)
                                {
                                    //вырезаем 2 команды
                                    string twoCommands = command.Substring(positionsOfQuotes[1] + 1, command.Length - positionsOfQuotes[1] - 1);

                                    string[] twoLastCommands = twoCommands.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    
                                    if (twoLastCommands.Length < 1 || twoLastCommands.Length > 2)
                                    {
                                        isAnyCharacter = true;
                                        Console.WriteLine("Invalid operation");
                                    }
                                    else
                                    {
                                        if (twoLastCommands[0] != "--format")
                                        {
                                            isAnyCharacter = true;
                                            Console.WriteLine("Invalid operation");
                                        }
                                        else
                                        {
                                            string path = command.Substring(positionsOfQuotes[0] + 1, positionsOfQuotes[1] - positionsOfQuotes[0] - 1);

                                            DirectoryInfo directory = new DirectoryInfo(path);

                                            if (directory.Exists)
                                            {
                                                switch (twoLastCommands.Length)
                                                {
                                                    case 1:
                                                    {
                                                        JSONSerialization(metaFile, path);

                                                        break;
                                                    }
                                                    case 2:
                                                    {
                                                        if (twoLastCommands.Length == 2 && (twoLastCommands[1] != "json" && twoLastCommands[1] != "xml"))
                                                        {
                                                            isAnyCharacter = true;
                                                            Console.WriteLine("Invalid operation");
                                                        }
                                                        else
                                                        {
                                                            if (twoLastCommands[1] == "json")
                                                            {
                                                                JSONSerialization(metaFile, path);
                                                            }
                                                            else
                                                            {
                                                                XMLSerialization(metaFile, path);
                                                            }
                                                        }
                                                        break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine($"Directory {path} not exist");
                                            }

                                            if (twoLastCommands.Length == 2 && (twoLastCommands[1] != "json" && twoLastCommands[1] != "xml"))
                                            {
                                                isAnyCharacter = true;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid operation");
                                }
                            }
                            else
                            {
                                flag = true;
                                Console.WriteLine("Invalid operation");
                            }

                            break;
                        }

                        default:
                            break;
                    }
                    break;
                }

                default:
                    break;
            }

            return null;
        }

        private static bool IsAnyCharacterBetweenExportAndPath(string command, List<int> positionsOfQuotes, bool isAnyCharacter, int indexOfEndOfExportSubComand)
        {
            for (int i = indexOfEndOfExportSubComand + 1; i < positionsOfQuotes[0]; i++)
            {
                if (command[i] != ' ')
                {
                    isAnyCharacter = true;
                    break;
                }
            }

            return isAnyCharacter;
        }

        private static int GetIndexOfEndOfExportSubComand(string command)
        {
            int indexOfT = 0;

            for (int i = 0; i < command.Length; i++)
            {
                if (command[i] == 't')
                {
                    indexOfT = i;
                }
            }

            return indexOfT;
        }

        private static bool IsAnyCharacterBetweenNames(string command, List<int> positionsOfQuotes, bool flag)
        {
            for (int i = positionsOfQuotes[2] - 1; i > positionsOfQuotes[1]; i--)
            {
                if (command[i] != ' ')
                {
                    flag = true;
                    break;
                }
            }

            return flag;
        }

        private static bool IsAnyCharacterAfterLastQuote(string command, List<int> positionsOfQuotes)
        {
            bool flag = false;

            if (positionsOfQuotes.Count == 4)
            {
                for (int i = command.Length - 1; i > positionsOfQuotes[3]; i--)
                {
                    if (command[i] != ' ')
                    {
                        flag = true;
                        break;
                    }
                }
            }

            return flag;
        }

        private static bool IsAnyCharacterAfterQuote(string command, List<int> positions)
        {
            bool flag = false;

            if (positions.Count == 2)
            {
                for (int i = command.Length - 1; i > positions[1]; i--)
                {
                    if (command[i] != ' ')
                    {
                        flag = true;
                        break;
                    }
                }
            }

            return flag;
        }

        private static List<int> GetCountOfQuotes(string command)
        {
            List<int> positions = new List<int>();

            for (int i = 0; i < command.Length; i++)
            {
                if (command[i] == '"')
                {
                    positions.Add(i);
                }
            }

            return positions;
        }

        private void AddSubCommandsToSubCommandsList(string[] commands)
        {
            foreach (var subCommand in commands)
            {
                subCommandsList.Add(subCommand);
            }
        }       
    }
}
