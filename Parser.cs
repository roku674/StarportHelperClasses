//Created by Alexander Fields https://github.com/roku674
using StarportObjects;
using Algorithms; //Algorithms-for-C-sharp
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace StarportBuilderBot.Models
{
    public static class Parser
    {
        /// <summary>
        /// will parse the holdings into blockof info from holdings
        /// </summary>
        /// <param name="info"></param>
        /// <returns>Colony Object</returns>
        public static Colony ParseHoldings(string info)
        {
            Colony colony = new Colony();
            StringReader reader = new StringReader(info);
            string line;

            for (int i = 0; i < info.Length; i++)
            {
                line = reader.ReadLine();
                if (line != null)
                {
                    if (i == 0)
                    {
                        colony.ColonyName = line;
                        Console.WriteLine(line);
                    }
                    else if (i == 1)
                    {
                        if (line.Contains("in system"))
                        {
                        }
                        else if (line.Contains("away"))
                        {
                        }
                    }
                    else if (i == 3)
                    {
                        line = line.Replace("Planet Type:", "");
                        line = line.Trim();
                        colony.PlanetType = line;
                    }
                    else if (i == 4)
                    {
                        line = line.Replace("Location:", "");
                        string[] temp = line.Split("(");
                        line = temp[0];
                        line = line.Trim();
                        colony.PlanetName = line;
                        Console.WriteLine(colony.PlanetName);

                        temp[1] = temp[1].Trim();
                        colony.Coordinates = temp[i];
                    }
                    else if (i == 5)
                    {
                        line = line.Replace("Owner:", "");
                        line = line.Trim();

                        colony.Owner = line;
                    }
                    else if (i == 6)
                    {
                        line = line.Replace("Corporation:", "");
                        line = line.Trim();

                        colony.Corporation = line;
                    }
                    else if (i == 7)
                    {
                        line = line.Replace("Founded:", "");
                        line = line.Trim();

                        colony.Founded = line;
                    }
                    else if (i == 8)
                    {
                        string[] temp = line.Split(':');
                        temp = temp[1].Split('('); //temp[1] is the growth rate
                        temp[0] = Regex.Replace(temp[0], " ", "");
                        temp[0] = StringManipulation.RemoveParenthesisColonComma(temp[0]);
                        Console.WriteLine(temp[0]);

                        colony.Biodome.Population = uint.Parse(temp[0]);
                    }
                    else if (i == 9)
                    {
                        line.Replace("Morale:", "");
                        string[] temp = line.Split("(");
                        line = temp[0];
                        temp = line.Split(" ");

                        Console.WriteLine(line);
                        colony.Biodome.MoraleWord = temp[0];
                        colony.Biodome.Morale = int.Parse(temp[1]);
                    }
                    else if (i == 10)
                    {
                        line = line.Replace("Government:", "");
                        line = line.Trim();
                        colony.Biodome.Government = Regex.Replace(line, " ", "");
                        Console.WriteLine(colony.Biodome.Government);
                    }
                    else if (i == 11)
                    {
                        line = line.Replace("Treasury:", "");
                        line = line.Trim();

                        string[] temp = line.Split('(');

                        temp[0] = StringManipulation.RemoveParenthesisColonComma(temp[0]);
                        temp[0] = Regex.Replace(temp[0], " ", "");
                        colony.Biodome.Treasury = uint.Parse(temp[0]);

                        temp[1] = StringManipulation.RemoveParenthesisColonComma(temp[1]);
                        temp[1] = Regex.Replace(temp[1], "[A-Za-z ]", "");
                        if (temp[1].Equals(null) || temp[1].Equals("")) { }
                        else
                        {
                            colony.Biodome.HourlyIncome = uint.Parse(temp[1]);
                        }

                        Console.WriteLine(temp[0] + " " + temp[1] + "/hr");
                    }
                    else if (i == 12)
                    {
                        line = line.Replace("Pollution", "");
                        line = line.Trim();

                        string[] temp = line.Split('(');
                        temp[0] = temp[0].Replace("%", "");
                        colony.Biodome.CurrentPollution = uint.Parse(temp[0]);

                        if (temp.Length > 2)
                        {
                            string trim = StringManipulation.RemoveParenthesisColonComma(temp[1]);
                            colony.Biodome.Disasters = uint.Parse(trim);
                            colony.Biodome.PollutionRate = temp[2];
                        }
                        else
                        {
                            colony.Biodome.PollutionRate = temp[1];
                        }
                        colony.Biodome.PollutionRate = Regex.Replace(colony.Biodome.PollutionRate, "[A-Za-z ]", "");
                        colony.Biodome.PollutionRate = StringManipulation.RemoveParenthesisColonComma(colony.Biodome.PollutionRate);
                        colony.Biodome.PollutionRate = StringManipulation.RemoveSlashes(colony.Biodome.PollutionRate);

                        Console.WriteLine(colony.Biodome.CurrentPollution + " " + colony.Biodome.Disasters);
                        Console.WriteLine(colony.Biodome.PollutionRate);
                    }
                    else if (i == 15)
                    {
                        line = line.Replace("Construction:", "");
                        line = line.Trim();

                        string[] temp = line.Split("(");
                        line = temp[0];
                        Console.WriteLine(line);
                        line = Regex.Replace(line, " ", "");
                        line = line.Replace("%", "");
                        colony.Biodome.AllocationConstruction = uint.Parse(line);
                    }
                    else if (i == 16)
                    {
                        line = line.Replace("Research:", "");
                        line = line.Trim();

                        string[] temp = line.Split("(");
                        line = temp[0];

                        Console.WriteLine(line);
                        line = Regex.Replace(line, " ", "");
                        line = line.Replace("%", "");
                        colony.Biodome.AllocationResearch = uint.Parse(line);
                    }
                    else if (i == 17)
                    {
                        line = line.Replace("Military:", "");
                        line = line.Trim();

                        string[] temp = line.Split("(");
                        line = temp[0];
                        Console.WriteLine(line);
                        line = Regex.Replace(line, " ", "");
                        line = line.Replace("%", "");
                        colony.Biodome.AllocationMilitary = uint.Parse(line);
                    }
                    else if (i == 18)
                    {
                        line = line.Replace("Harvesting:", "");
                        line = line.Trim();

                        string[] temp = line.Split("(");
                        line = temp[0];

                        Console.WriteLine(line);
                        line = Regex.Replace(line, " ", "");
                        colony.Biodome.AllocationHarvesting = uint.Parse(line);
                    }
                    else if (i == 20)
                    {
                        line = line.Replace("Building:", "");
                        line = line.Trim();

                        colony.Biodome.CurrentlyConstructing = line;
                        Console.WriteLine(colony.Biodome.CurrentlyConstructing);
                    }
                    else if (i == 30)
                    {
                        //metal and anae on thsir ow
                        string[] temp = line.Split(':');
                        temp[1] = Regex.Replace(temp[1], "[A-Za-z ]", "");
                        temp[2] = Regex.Replace(temp[2], " ", "");
                        colony.Refinery.resourcesInRefinery.Metal = uint.Parse(temp[1]);
                        colony.Refinery.resourcesInRefinery.Anaerobes = uint.Parse(temp[2]);
                        Console.WriteLine(temp[1] + '\n' + temp[2]);
                    }
                    else if (i == 31)
                    {
                        //meds & orgs
                        string[] temp = line.Split(':');
                        temp[1] = Regex.Replace(temp[1], "[A-Za-z ]", "");
                        temp[2] = Regex.Replace(temp[2], " ", "");
                        colony.Refinery.resourcesInRefinery.Medicine = uint.Parse(temp[1]);
                        colony.Refinery.resourcesInRefinery.Organics = uint.Parse(temp[2]);
                        Console.WriteLine(temp[1] + '\n' + temp[2]);
                    }
                    else if (i == 32)
                    {
                        //oil and uri
                        string[] temp = line.Split(':');
                        temp[1] = Regex.Replace(temp[1], "[A-Za-z ]", "");
                        temp[2] = Regex.Replace(temp[2], " ", "");
                        colony.Refinery.resourcesInRefinery.Oil = uint.Parse(temp[1]);
                        colony.Refinery.resourcesInRefinery.Uranium = uint.Parse(temp[2]);
                        Console.WriteLine(temp[1] + '\n' + temp[2]);
                    }
                    else if (i == 33)
                    {
                        //equi and spice
                        string[] temp = line.Split(':');
                        temp[1] = Regex.Replace(temp[1], "[A-Za-z ]", "");
                        temp[2] = Regex.Replace(temp[2], " ", "");
                        colony.Refinery.resourcesInRefinery.Equipment = uint.Parse(temp[1]);
                        colony.Refinery.resourcesInRefinery.Spice = uint.Parse(temp[2]);
                        Console.WriteLine(temp[1] + '\n' + temp[2]);
                    }
                    else if (i == 36)
                    {
                        if (line.Equals("No weapons factory present."))
                        {
                            for (int j = i; j < info.Length; j++)
                            {
                                if (line != null)
                                {
                                    if (j == 38)
                                    {
                                        string[] temp = line.Split('(');
                                        temp[0] = Regex.Replace(temp[0], "[A-Za-z ]", "");
                                        temp[0] = StringManipulation.RemoveParenthesisColonComma(temp[0]);

                                        temp[1] = Regex.Replace(temp[1], "[A-Za-z ]", "");
                                        temp[1] = StringManipulation.RemoveParenthesisColonComma(temp[1]);
                                        if (temp[1] == null || temp[1].Equals(""))
                                        {
                                            temp[1] = "0";
                                        }

                                        colony.Solar.SolarShots = uint.Parse(temp[0]);
                                        colony.Solar.SolarRate = uint.Parse(temp[1]);
                                        Console.WriteLine(temp[0] + '\n' + temp[1]);
                                    }
                                    line = reader.ReadLine();
                                    if (j >= 39 && j <= 50)
                                    {
                                        colony.Biodome.Discoveries = colony.Biodome.Discoveries + '\n' + line;
                                    }
                                }
                            }
                            Console.WriteLine("No Weapons Factory");
                            break;
                        }
                        else
                        {
                            //nukes
                            string[] temp = line.Split(':');
                            temp[1] = Regex.Replace(temp[1], " ", "");
                            colony.WeaponsFactory.Nukes = uint.Parse(temp[1]);
                            Console.WriteLine(temp[1]);
                        }
                    }
                    else if (i == 37)
                    {
                        //Neggers
                        line = line.Replace("Negotiators:", "");
                        line = line.Trim();

                        colony.WeaponsFactory.Negotiators = uint.Parse(line);
                    }
                    else if (i == 38)
                    {
                        //normal mines
                        line = line.Replace("Mines:", "");
                        line = line.Trim();

                        colony.WeaponsFactory.Mines = uint.Parse(line);
                    }
                    else if (i == 39)
                    {
                        //cmines
                        string[] temp = line.Split(':');
                        temp[1] = Regex.Replace(temp[1], " ", "");

                        colony.WeaponsFactory.CompoundMines = uint.Parse(temp[1]);
                        Console.WriteLine(temp[1]);
                    }
                    else if (i == 40)
                    {
                        //flaks
                        line = line.Replace("Flak Cannons:", "");
                        line = line.Trim();

                        colony.WeaponsFactory.FlakCannons = uint.Parse(line);
                    }
                    else if (i == 41)
                    {
                        //lasers
                        string[] temp = line.Split(':');
                        temp[1] = Regex.Replace(temp[1], " ", "");

                        colony.WeaponsFactory.LaserCannons = uint.Parse(temp[1]);
                        Console.WriteLine(temp[1]);
                    }
                    else if (i == 42)
                    {
                        //shields
                        line = line.Replace("Shields:", "");
                        line = line.Trim();

                        colony.WeaponsFactory.Shields = uint.Parse(line);
                    }
                    else if (i == 46)
                    {
                        //solar
                        string[] temp = line.Split('(');
                        temp[0] = Regex.Replace(temp[0], "[A-Za-z ]", "");
                        temp[0] = StringManipulation.RemoveParenthesisColonComma(temp[0]);

                        temp[1] = Regex.Replace(temp[1], "[A-Za-z ]", "");
                        temp[1] = StringManipulation.RemoveParenthesisColonComma(temp[1]);

                        colony.Solar.SolarShots = uint.Parse(temp[0]);
                        colony.Solar.SolarRate = uint.Parse(temp[1]);
                        Console.WriteLine(temp[0] + '\n' + temp[1]);
                    }
                    else if (i >= 48 && i <= 59)
                    {
                        colony.Biodome.Discoveries = colony.Biodome.Discoveries + '\n' + line;

                        //Console.WriteLine(line);
                    }
                }
            }

            return colony;
        }
    }
}