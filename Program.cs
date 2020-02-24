using System;
using System.Collections.Generic;

namespace Game_1 {
    class Program {

        public static int x;

        static void Main (string[] args) {
            while (true) {
                runGame ();
            }
        }

        static void runGame () {

            //Initialization of the game

            //Turn flag
            bool myTurn = false;

            int y = -1;

            while (y <= 0) {
                Console.Clear ();

                Console.WriteLine ("> Please enter a positive integer as the starting point:");
                Console.WriteLine ("> Please note that for large numbers (like 10^6), the application may hang for a while depending on the hardware. Please be patient.");
                y = Convert.ToInt32 (Console.ReadLine ());
                if (y <= 0) {
                    Console.WriteLine ("The number you have entered is not a positive integer. Press a key to try again.");
                    Console.ReadKey ();
                }

            }

            Console.Clear ();
            Console.WriteLine ("> By default the computer starts the game. If you'd like to start first, enter (y). If not, press any key to start.");
            if (Console.ReadLine () == "y") {
                myTurn = true;
            }

            //Index+1 to go up to y
            x = y + 1;

            string[] stateList = new string[x];

            //Step 1
            stateList[0] = "p";

            List<int> squaresUpToN = populateUpToN (x);

            //Check if null labels exist, repeat until none exists

            while (nullLabelExists (stateList)) {

                //Step 2
                for (var i = 0; i < stateList.Length; i++) {
                    if (stateList[i] == "p") {
                        for (var j = 0; j < squaresUpToN.Count; j++) {
                            if (i + squaresUpToN[j] < x) {
                                stateList[i + squaresUpToN[j]] = "n";
                            }
                        }
                    }
                }

                //Step 3
                for (var i = 0; i < stateList.Length; i++) {
                    if (stateList[i] == null) {
                        var pFlag = false;
                        for (var j = 0; j < squaresUpToN.Count; j++) {
                            if (i - squaresUpToN[j] > 0 && stateList[i - squaresUpToN[j]] == "n") {
                                continue;
                            }
                            if (i - squaresUpToN[j] > 0 && stateList[i - squaresUpToN[j]] == "p") {
                                pFlag = true;
                                continue;
                            }
                        }
                        if (pFlag) {
                            continue;
                        } else {
                            stateList[i] = "p";
                        }
                    }
                }

            }

            // //Print out the list for debug purposes
            // for (int i = 0; i < stateList.Length; i++) {
            //     Console.WriteLine ("{0} : {1}", i, stateList[i]);
            // }

            //Game logic goes here
            while (y > 0) {
                Console.Clear ();
                //Repopulate the list of all squares up to y at the beginning of every turn
                squaresUpToN = populateUpToN (y);

                //Player's turn
                if (myTurn) {
                    myTurn = false;
                    var userInput = -1;
                    while (userInput == -1) {
                        Console.Clear ();
                        Console.WriteLine ("> Your turn!\n> Remaining: " + y + "\n> Choose a squared number to subtract. ");

                        Console.WriteLine ("> Available numbers are:");
                        string availableSquares = "";

                        //List all valid numbers that can be played
                        foreach (int num in squaresUpToN) {
                            availableSquares += num + ", ";
                        }

                        availableSquares = availableSquares.Remove (availableSquares.Length - 2);

                        Console.WriteLine ("> " + availableSquares);

                        userInput = takeInput ();
                        if (userInput == -1) {
                            Console.WriteLine ("\n> Wrong input. Please try again.");
                            Console.ReadKey ();
                        }
                    }

                    if (userInput <= y) {
                        y -= userInput;
                    }

                }
                //Computer's turn
                else {
                    myTurn = true;
                    Console.WriteLine ("> Computer's turn!\n> Remaining: " + y);
                    Console.WriteLine ("> Press any key to see the computer's move.");

                    Console.ReadKey ();

                    //Computer logic done here
                    var move = 1;
                    foreach (int k in squaresUpToN) {
                        if (stateList[y - k] == "p") {
                            if (k > move) {
                                move = k;
                            }
                        }

                    }

                    // Print out remaining p and n states for debug purposes
                    // for (int i = 0; i < y+1; i++) {
                    //     Console.WriteLine ("{0} : {1}", i, stateList[i]);
                    // }

                    if (move <= y) {
                        y -= move;
                    }

                    Console.WriteLine ("\n> Computer subtracted: " + move + ". The remaining amount is: " + y + ".");
                    Console.WriteLine ("> Press any key to move to your turn.");
                    Console.ReadKey ();

                }

                if (!myTurn) {
                    Console.Clear ();
                    Console.WriteLine ("> Congratulations. You won!");
                } else {
                    Console.Clear ();
                    Console.WriteLine ("> BEEP BOOP. COMPUTER WINS.");
                }

            }
            Console.WriteLine ("\n> Press any key to start over.");

            Console.ReadKey ();
        }

        //Input taking and checking
        static int takeInput () {
            string val = Console.ReadLine ();

            int fin = Convert.ToInt32 (val);

            if (isSquared (fin) && fin > 0) {
                return fin;
            } else {
                return -1;
            }

        }

        //Generate list of squares up to n
        static List<int> populateUpToN (int n) {
            List<int> squaresUpToN = new List<int> ();

            for (int i = 1; i <= Math.Sqrt (n); i++) {
                squaresUpToN.Add (i * i);
            }

            return squaresUpToN;
        }

        //Check whether given input is a squared integer
        static bool isSquared (int input) {
            return (Math.Sqrt (input) % 1 == 0);
        }

        //Check given array for a null label
        static bool nullLabelExists (string[] array) {
            bool flag = false;
            foreach (string x in array) {
                if (x == null) {
                    flag = true;
                    break;
                }
            }
            return flag;
        }

    }
}