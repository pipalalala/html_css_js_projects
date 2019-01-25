using System;
using System.Collections.Generic;

namespace lab3_CLR
{
    public class Parser
    {
        private List<string> subCommandsList = new List<string>();
        private bool isCommandCorrect = false;

        /// <summary>
        /// Tries to parse command and if it correct returns "true" and returns "false" if command is uncorrect
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public bool IsCommandCorrect(string command)
        {
            TryParse(command);

            return isCommandCorrect;
        }
                
        public List<string> GetListOfSubCommands()
        {
            return subCommandsList;
        }

        /// <summary>
        /// Tries to parse command and returns list of subcommands
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private List<string> TryParse(string command)
        {
            string[] commands = command.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (commands.Length != 0)
            {
                switch (commands[0])
                {
                    case "exit":
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
                            case "remove":
                            case "info":
                            {
                                List<int> positionsOfQuotes = GetPositionsOfQuotes(command);

                                if (positionsOfQuotes.Count == 2)
                                {
                                    bool isAnyCharacterAfterQuote = IsAnyCharacterAfterQuote(command, positionsOfQuotes);

                                    string[] partsOfCommand = command.Split(new char[] { '"' }, StringSplitOptions.RemoveEmptyEntries);
                                    string[] startCommands = partsOfCommand[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                                    if (isAnyCharacterAfterQuote == false && startCommands.Length == 2 && command[positionsOfQuotes[0] - 1] == ' ')
                                    {
                                        subCommandsList.Add(commands[0]);
                                        subCommandsList.Add(commands[1]);
                                        subCommandsList.Add(command.Substring(positionsOfQuotes[0] + 1, positionsOfQuotes[1] - positionsOfQuotes[0] - 1));

                                        isCommandCorrect = true;

                                        return subCommandsList;
                                    }
                                }
                                break;
                            }

                            case "download":
                            case "move":
                            {
                                List<int> positionsOfQuotes = GetPositionsOfQuotes(command);

                                if (positionsOfQuotes.Count == 4)
                                {
                                    bool isAnyCharacter = IsAnyCharacterAfterLastQuote(command, positionsOfQuotes);
                                    isAnyCharacter = IsAnyCharacterBetweenNames(command, positionsOfQuotes, isAnyCharacter);

                                    string[] partsOfCommand = command.Split(new char[] { '"' }, StringSplitOptions.RemoveEmptyEntries);
                                    string[] startCommands = partsOfCommand[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                                    if (isAnyCharacter == false && startCommands.Length == 2
                                        && command[positionsOfQuotes[0] - 1] == ' ')
                                    {
                                        subCommandsList.Add(commands[0]);
                                        subCommandsList.Add(commands[1]);
                                        subCommandsList.Add(command.Substring(positionsOfQuotes[0] + 1, positionsOfQuotes[1] - positionsOfQuotes[0] - 1));
                                        subCommandsList.Add(command.Substring(positionsOfQuotes[2] + 1, positionsOfQuotes[3] - positionsOfQuotes[2] - 1));

                                        isCommandCorrect = true;

                                        return subCommandsList;
                                    }
                                }
                                break;
                            }

                            case "export":
                            {
                                if (commands.Length == 3 && commands[2] == "--info")
                                {
                                    AddSubCommandsToSubCommandsList(commands);
                                    isCommandCorrect = true;

                                    return subCommandsList;
                                }
                                else
                                {
                                    List<int> positionsOfQuotes = GetPositionsOfQuotes(command);

                                    if (positionsOfQuotes.Count == 2 && command[positionsOfQuotes[0] - 1] == ' ' && command[positionsOfQuotes[1] + 1] == ' ')
                                    {
                                        string[] partsOfCommand = command.Split(new char[] { '"' }, StringSplitOptions.RemoveEmptyEntries);
                                        string[] lastCommands = partsOfCommand[2].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                        string[] startCommands = partsOfCommand[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                                        if (lastCommands.Length == 1 && lastCommands[0] == "--format"
                                            && startCommands.Length == 2 && command[positionsOfQuotes[0] - 1] == ' ')
                                        {
                                            subCommandsList.Add(commands[0]);
                                            subCommandsList.Add(commands[1]);
                                            subCommandsList.Add(partsOfCommand[1]);
                                            subCommandsList.Add(lastCommands[0]);

                                            isCommandCorrect = true;

                                            return subCommandsList;
                                        }

                                        if (lastCommands.Length == 2 && lastCommands[0] == "--format"
                                            && (lastCommands[1] == "json" || lastCommands[1] == "xml")
                                            && startCommands.Length == 2 && command[positionsOfQuotes[0] - 1] == ' ')
                                        {
                                            subCommandsList.Add(commands[0]);
                                            subCommandsList.Add(commands[1]);
                                            subCommandsList.Add(partsOfCommand[1]);
                                            subCommandsList.Add(lastCommands[0]);
                                            subCommandsList.Add(lastCommands[1]);

                                            isCommandCorrect = true;

                                            return subCommandsList;
                                        }
                                    }
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
            }
            return null;
        }

        private bool IsAnyCharacterBetweenExportAndPath(string command, List<int> positionsOfQuotes, bool isAnyCharacter, int indexOfEndOfExportSubComand)
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
        
        private bool IsAnyCharacterBetweenNames(string command, List<int> positionsOfQuotes, bool flag)
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

        private bool IsAnyCharacterAfterLastQuote(string command, List<int> positionsOfQuotes)
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

        private bool IsAnyCharacterAfterQuote(string command, List<int> positions)
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
        
        private List<int> GetPositionsOfQuotes(string command)
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
