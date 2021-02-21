﻿using System;
using System.Collections.Generic;
using System.IO;
using FinchAPI;


namespace Project_FinchControl
{
    //
    // Title: Finch Controler
    // Description: A program to control the finch in diffrent ways
    // Application Type: Console
    // Author: Ryan Bertschy
    // Dated Created: 2/20/2021
    // Last Modified: 2/21/2021  
    //
   

    class Program
    {
        /// <summary>
        /// Main Program Order
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            SetTheme();

            DisplayWelcomeScreen();
            DisplayMenuScreen();
            DisplayClosingScreen();
        }

        /// <summary>
        /// console theme
        /// </summary>
        static void SetTheme()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        /// <summary>
        /// Home Screen
        /// </summary>
        static void DisplayMenuScreen()
        {
            Console.CursorVisible = true;

            bool quitApplication = false;
            string menuChoice;

            Finch finchRobot = new Finch();

            do
            {
                DisplayScreenHeader("Main Menu");

                //
                // home screen menu
                //
                Console.WriteLine("\ta) Connect Finch Robot");
                Console.WriteLine("\tb) Talent Show");
                Console.WriteLine("\tc) Data Recorder");
                Console.WriteLine("\td) Alarm System");
                Console.WriteLine("\te) User Programming");
                Console.WriteLine("\tf) Disconnect Finch Robot");
                Console.WriteLine("\tq) Quit");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // Home Screen choice
                //
                switch (menuChoice)
                {
                    case "a":
                        DisplayConnectFinchRobot(finchRobot);
                        break;

                    case "b":
                        DisplayTalentShowMenuScreen(finchRobot);
                        break;

                    case "c":
                        DisplayDataRecoderMenuScreen(finchRobot);
                        break;

                    case "d":

                        break;

                    case "e":

                        break;

                    case "f":
                        DisplayDisconnectFinchRobot(finchRobot);
                        break;

                    case "q":
                        DisplayDisconnectFinchRobot(finchRobot);
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitApplication);
        }

        #region Data Recorder

        /// <summary>
        /// Data recorder menu
        /// </summary>
        static void DisplayDataRecoderMenuScreen(Finch finchRobot)
        {
            Console.CursorVisible = true;

            bool quitMenu = false;
            string menuChoice;

            int numberOfDataPoints = 0;
            double frequencyOfDataPointsInSecounds = 0;
            double[] temperatures = null;


            do
            {
                DisplayScreenHeader("Data Recoder Menu");

                //
                // user Data Recorder screen
                //
                Console.WriteLine("\ta)Get the number of Data Points ");
                Console.WriteLine("\tb)Get the frequency of data Points ");
                Console.WriteLine("\tc)Get the Data set ");
                Console.WriteLine("\td) Display the data set");
                Console.WriteLine("\tq) Back");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // user Data Recorder choice
                //
                switch (menuChoice)
                {
                    case "a":
                        numberOfDataPoints = DataRecorderDisplayGetNumberOfDataPoints();
                        break;

                    case "b":
                        frequencyOfDataPointsInSecounds = DataRecorderDisplayGetFrequencyOfDataPoints();
                        break;

                    case "c":
                        if (numberOfDataPoints == 0 || frequencyOfDataPointsInSecounds == 0)
                        {
                            Console.WriteLine();
                            Console.WriteLine("\t\tPlease preform the actions in A and B first.");
                            DisplayContinuePrompt();
                        }
                        else
                        {
                            temperatures = DateRecorderDisplayGetDataSet(numberOfDataPoints, frequencyOfDataPointsInSecounds, finchRobot);
                        }
                        break;

                    case "d":
                        DataRecorderDisplayGetDataSet(temperatures);
                        break;

                    case "q":
                        quitMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitMenu);
        }

        /// <summary>
        /// Data Recorder, Display Data Table
        /// </summary>
        /// <param name="finchRobot"></param>
        static void DataRecorderDisplayDataTable(double[] temperatures)
        {
            Console.WriteLine(
                "\t\tReading #".PadLeft(15) +
                "Temerature".PadLeft(15)
                );

            for (int index = 0; index < temperatures.Length; index++)
            {
                Console.WriteLine(
                    (index + 1).ToString().PadLeft(15) +
                    temperatures[index].ToString("n2").PadLeft(15)
                    );

            }
        }

        /// <summary>
        /// Data Recorder, Display Data Set
        /// </summary>
        /// <param name="finchRobot"></param>
        static void DataRecorderDisplayGetDataSet(double[] temperatures)
        {
            DisplayScreenHeader("\t\tData set");

            DataRecorderDisplayDataTable(temperatures);

            DisplayContinuePrompt();
        }

        /// <summary>
        /// Data Recorder, Get The Data Points
        /// </summary>
        /// <param name="finchRobot"></param>
        static double[] DateRecorderDisplayGetDataSet(int numberOfDataPoints, double frequencyOfDataPointsInSecounds, Finch finchRobot)
        {
            double[] temeratures = new double[numberOfDataPoints];



            DisplayScreenHeader("\t\tGet Data Set");

            Console.WriteLine($"\t\tNumber of Data Points: {numberOfDataPoints}");
            Console.WriteLine($"\t\tFrequency of Data Points: {frequencyOfDataPointsInSecounds}");
            Console.WriteLine();

            Console.WriteLine("\t\tThe Finch Robot is ready to recorder the temperatures. Please press any key to start.");
            Console.ReadKey();


            double temperature;
            int frequencyMillSec;
            for (int index = 0; index < numberOfDataPoints; index++)
            {
                temperature = finchRobot.getTemperature();
                Console.WriteLine($"\t\tTemerature Recorded {index + 1}: {temperature}");
                temeratures[index] = temperature;
                frequencyMillSec = (int)(frequencyOfDataPointsInSecounds * 1000);
                finchRobot.wait(frequencyMillSec);
            }


            DisplayContinuePrompt();

            return temeratures;
        }

        /// <summary>
        /// Data Recorder, Get The Frequency Of Data Points
        /// </summary>
        /// <param name="finchRobot"></param>
        static double DataRecorderDisplayGetFrequencyOfDataPoints()
        {
            double frequencyOfDataPoints;
            bool validResponse;


            validResponse = false;
            do
            {

                DisplayScreenHeader("\t\tThe Frequency of the Data Points");

                Console.Write("\t\tEnter the Frequency of the Data Points");
                double.TryParse(Console.ReadLine(), out frequencyOfDataPoints);

                if (frequencyOfDataPoints > 0)
                {
                    validResponse = true;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("\t\tPlease enter a postive number and a number greater then 0");
                    DisplayContinuePrompt();
                }


            } while (!validResponse);


            Console.WriteLine();
            Console.WriteLine($"\t\tThe Frequency of the Data Points: {frequencyOfDataPoints}");


            DisplayContinuePrompt();

            return frequencyOfDataPoints;
        }


        /// <summary>
        /// Data Recorder, Get Number Of Data Points
        /// </summary>
        /// <param name="finchRobot"></param>
        static int DataRecorderDisplayGetNumberOfDataPoints()
        {
            int numberOfDataPoints;

            DisplayScreenHeader("\t\tNumber of Data Points");

            Console.Write("\t\tEnter the number of Data Points");
            int.TryParse(Console.ReadLine(), out numberOfDataPoints);

            Console.WriteLine();
            Console.WriteLine($"\t\tNumber of Data Points: {numberOfDataPoints}");


            DisplayContinuePrompt();

            return numberOfDataPoints;
        }

        #endregion

        #region TALENT SHOW

        /// <summary>
        /// talent show menu
        /// </summary>
        static void DisplayTalentShowMenuScreen(Finch finchRobot)
        {
            Console.CursorVisible = true;

            bool quitTalentShowMenu = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Talent Show Menu");

                //
                // user talent show screen
                //
                Console.WriteLine("\ta) Light and Sound");
                Console.WriteLine("\tb) Dance ");
                Console.WriteLine("\tc) Song");
                Console.WriteLine("\td) Interpretive Dance");
                Console.WriteLine("\tq) Back");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // user talent show choice
                //
                switch (menuChoice)
                {
                    case "a":
                        DisplayLightAndSound(finchRobot);
                        break;

                    case "b":
                        DisplayDance(finchRobot);
                        break;

                    case "c":
                        DisplaySong(finchRobot);
                        break;

                    case "d":
                        DisplayInterpretiveDance(finchRobot);
                        break;

                    case "q":
                        quitTalentShowMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitTalentShowMenu);
        }
        /// <summary>
        /// Interpretive Dance
        /// </summary>
        /// <param name="finchRobot"></param>
        private static void DisplayInterpretiveDance(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Light and Sound");

            Console.WriteLine("\tThe Finch robot will not show off its glowing talent!");
            DisplayContinuePrompt();

            for (int lightSoundLevel = 0; lightSoundLevel < 200; lightSoundLevel++)
            {
                finchRobot.setLED(0, lightSoundLevel, lightSoundLevel);
                finchRobot.noteOn(lightSoundLevel * 100);


            }

            finchRobot.setMotors(250, 0);
            finchRobot.wait(500);
            finchRobot.setMotors(0, 250);
            finchRobot.wait(500);
            finchRobot.noteOff();
            finchRobot.wait(200);

            for (int i = 200; i < 255; i++)
            {
                finchRobot.setMotors(i, -i);
                finchRobot.setLED(0, i, i);


                finchRobot.setMotors(0, 0);
                finchRobot.setLED(0, 0, 0);

            }


            DisplayMenuPrompt("Talent Show Menu");
        }




        /// <summary>
        /// Talent Show, Song
        /// </summary>
        /// <param name="finchRobot"></param>
        static void DisplaySong(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Song");

            Console.WriteLine("\t\tThe Finch will now Sing!");
            DisplayContinuePrompt();

            //verse 1


            finchRobot.noteOn(392);
            finchRobot.noteOff();
            finchRobot.wait(500);
            finchRobot.noteOn(392);
            finchRobot.wait(500);
            finchRobot.noteOff();
            finchRobot.wait(500);
            finchRobot.noteOn(392);
            finchRobot.wait(500);
            finchRobot.noteOff();
            finchRobot.wait(500);
            finchRobot.noteOn(392);
            finchRobot.noteOn(440);
            finchRobot.wait(250);
            finchRobot.noteOff();
            finchRobot.wait(750);

            //verse 2
            finchRobot.noteOn(329);
            finchRobot.wait(250);
            finchRobot.noteOff();
            finchRobot.noteOn(392);
            finchRobot.wait(250);
            finchRobot.noteOff();
            finchRobot.noteOn(392);
            finchRobot.wait(250);
            finchRobot.noteOn(1046);
            finchRobot.noteOn(1174);
            finchRobot.wait(250);
            finchRobot.noteOff();
            finchRobot.wait(750);

            //verse 3
            finchRobot.noteOn(1318);
            finchRobot.noteOn(1318);
            finchRobot.wait(250);
            finchRobot.noteOff();
            finchRobot.noteOn(1318);
            finchRobot.noteOn(1174);
            finchRobot.wait(250);
            finchRobot.noteOff();
            finchRobot.noteOn(1174);
            finchRobot.wait(250);
            finchRobot.noteOn(1046);
            finchRobot.wait(250);
            finchRobot.noteOff();
            finchRobot.noteOn(1046);
            finchRobot.wait(250);
            finchRobot.noteOff();
            finchRobot.wait(750);

            //verse 4
            finchRobot.noteOn(1318);
            finchRobot.noteOn(1318);
            finchRobot.wait(550);
            finchRobot.noteOn(1396);
            finchRobot.wait(250);
            finchRobot.noteOn(1318);
            finchRobot.wait(250);
            finchRobot.noteOn(1318);
            finchRobot.noteOn(1174);
            finchRobot.wait(500);
            finchRobot.noteOff();
            finchRobot.wait(750);

            // cor
            finchRobot.noteOn(1318);
            finchRobot.wait(250);
            finchRobot.noteOn(1174);
            finchRobot.wait(250);
            finchRobot.noteOn(1174);
            finchRobot.noteOn(1046);
            finchRobot.wait(250);
            finchRobot.noteOff();
            finchRobot.wait(600);

            //verse 5
            finchRobot.noteOn(392);
            finchRobot.wait(500);
            finchRobot.noteOn(392);
            finchRobot.wait(250);
            finchRobot.noteOn(392);
            finchRobot.wait(500);
            finchRobot.noteOn(440);
            finchRobot.wait(250);
            finchRobot.noteOn(1046);
            finchRobot.wait(250);
            finchRobot.noteOn(440);
            finchRobot.noteOn(392);
            finchRobot.wait(250);
            finchRobot.noteOff();
            finchRobot.wait(250);

            //verse 6
            finchRobot.noteOn(1046);
            finchRobot.wait(250);
            finchRobot.noteOn(1174);
            finchRobot.wait(250);
            finchRobot.noteOn(1318);
            finchRobot.noteOn(1318);
            finchRobot.wait(500);
            finchRobot.noteOff();
            finchRobot.wait(250);

            //verse 7
            finchRobot.noteOn(1318);
            finchRobot.wait(250);
            finchRobot.noteOn(1174);
            finchRobot.wait(250);
            finchRobot.noteOn(1174);
            finchRobot.wait(250);
            finchRobot.noteOn(1046);
            finchRobot.wait(250);
            finchRobot.noteOn(1046);
            finchRobot.wait(500);
            finchRobot.noteOff();
            finchRobot.wait(250);

            //verse 8
            finchRobot.noteOn(1318);
            finchRobot.noteOn(1318);
            finchRobot.wait(500);
            finchRobot.noteOn(1396);
            finchRobot.wait(500);
            finchRobot.noteOn(1318);
            finchRobot.wait(250);
            finchRobot.noteOn(1318);
            finchRobot.noteOn(1174);
            finchRobot.wait(500);
            finchRobot.noteOff();
            finchRobot.wait(250);

            //cor
            finchRobot.noteOn(1318);
            finchRobot.wait(250);
            finchRobot.noteOn(1174);
            finchRobot.wait(250);
            finchRobot.noteOn(1046);
            finchRobot.wait(500);
            finchRobot.noteOff();
            finchRobot.wait(250);

            finchRobot.noteOn(1318);
            finchRobot.wait(250);
            finchRobot.noteOn(1567);
            finchRobot.wait(250);
            finchRobot.noteOn(1760);
            finchRobot.wait(500);
            finchRobot.noteOff();
            finchRobot.wait(250);

            finchRobot.noteOn(1567);
            finchRobot.wait(250);
            finchRobot.noteOn(1318);
            finchRobot.wait(250);
            finchRobot.noteOn(1046);
            finchRobot.wait(500);
            finchRobot.noteOff();
            finchRobot.wait(250);

            finchRobot.noteOn(440);
            finchRobot.wait(250);
            finchRobot.noteOn(392);
            finchRobot.wait(250);
            finchRobot.noteOn(1318);
            finchRobot.wait(500);
            finchRobot.noteOff();
            finchRobot.wait(250);

            //verse 9
            finchRobot.noteOn(1318);
            finchRobot.noteOn(1318);
            finchRobot.wait(500);
            finchRobot.noteOn(1396);
            finchRobot.wait(250);
            finchRobot.noteOn(1318);
            finchRobot.wait(250);
            finchRobot.noteOn(1318);
            finchRobot.noteOn(1174);
            finchRobot.wait(500);
            finchRobot.noteOff();
            finchRobot.wait(250);

            //cor
            finchRobot.noteOn(1318);
            finchRobot.wait(250);
            finchRobot.noteOn(1174);
            finchRobot.wait(250);
            finchRobot.noteOn(1174);
            finchRobot.noteOn(1046);
            finchRobot.wait(500);
            finchRobot.noteOff();
            finchRobot.wait(250);
        }


        /// <summary>
        /// Talent Show, Dance
        /// </summary>
        /// <param name="finchRobot"></param>
        static void DisplayDance(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Dance");

            Console.WriteLine("\t\tThe Finch will now dance!");
            DisplayContinuePrompt();

            finchRobot.setMotors(0, 150);
            finchRobot.wait(500);
            finchRobot.setMotors(0, 0);
            finchRobot.wait(100);
            finchRobot.setMotors(0, -150);
            finchRobot.wait(500);
            finchRobot.setMotors(0, 0);
            finchRobot.wait(100);
            finchRobot.setMotors(150, 0);
            finchRobot.wait(500);
            finchRobot.setMotors(0, 0);
            finchRobot.wait(100);
            finchRobot.setMotors(-150, 0);
            finchRobot.wait(500);
            finchRobot.setMotors(0, 0);
            finchRobot.wait(100);
            finchRobot.setMotors(150, 150);
            finchRobot.wait(1000);
            finchRobot.setMotors(0, 0);
            finchRobot.wait(100);
            finchRobot.setMotors(255, -255);
            finchRobot.wait(750);
            finchRobot.setMotors(0, 0);

        }

        /// <summary>
        /// Talent Show, Light and Sound
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void DisplayLightAndSound(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Light and Sound");

            Console.WriteLine("\tThe Finch robot will not show off its glowing talent!");
            DisplayContinuePrompt();

            for (int lightSoundLevel = 0; lightSoundLevel < 255; lightSoundLevel++)
            {
                finchRobot.setLED(lightSoundLevel, lightSoundLevel, lightSoundLevel);
                finchRobot.noteOn(lightSoundLevel * 100);

            }

            DisplayMenuPrompt("Talent Show Menu");
        }

        #endregion

        #region FINCH ROBOT MANAGEMENT

        /// <summary>
        /// disconnect screen
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void DisplayDisconnectFinchRobot(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Disconnect Finch Robot");

            Console.WriteLine("\tAbout to disconnect from the Finch robot.");
            DisplayContinuePrompt();

            finchRobot.disConnect();

            Console.WriteLine("\tThe Finch robot is now disconnect.");

            DisplayMenuPrompt("Main Menu");
        }

        /// <summary>
        /// connect screen
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>notify if the robot is connected</returns>
        static bool DisplayConnectFinchRobot(Finch finchRobot)
        {
            Console.CursorVisible = false;

            bool robotConnected;

            DisplayScreenHeader("Connect Finch Robot");

            Console.WriteLine("\tAbout to connect to Finch robot. Please be sure the USB cable is connected to the robot and computer now.");
            DisplayContinuePrompt();

            robotConnected = finchRobot.connect();

            if (robotConnected)
            {
                Console.WriteLine("\t\tRobot Connected.");
                for (int i = 0; i < 255; i++)
                {
                    finchRobot.setLED(i, 0, i);

                }
                for (int i = 255; i > 0; i--)
                {
                    finchRobot.setLED(i, 0, i);

                }
                finchRobot.noteOff();
            }
            else
            {
                Console.WriteLine("\t\tThere was a problem connecting");
            }

            DisplayMenuPrompt("Main Menu");

            return robotConnected;
        }

        #endregion

        #region USER INTERFACE

        /// <summary>
        /// welcome screen
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t\t Welcome to the Finch Control");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// closing screen
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using Finch Control!");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("\tPress any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// display menu prompt
        /// </summary>
        static void DisplayMenuPrompt(string menuName)
        {
            Console.WriteLine();
            Console.WriteLine($"\tPress any key to return to the {menuName} Menu.");
            Console.ReadKey();
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }

        #endregion
    }
}
