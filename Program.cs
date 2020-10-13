using System;
using System.Collections.Generic;

namespace Lab5_Project1
{
    class Program
    {   
        public static List<int> listOfCards = new List<int>();
        public static Random random = new Random();
        public static bool exit = false;

        static void Main(string[] args)
        {
            while(!exit){
                PrintMenu(1);
                int choice = VerifyInput(3,1);

                switch(choice){
                    case 1:
                        //Plays the game
                        //Clears the list everytime a game is started to make sure there is the correct amount of card values
                        listOfCards.Clear();

                        //Adds the card values to the list each game
                        for (int i = 1; i <= 13; i++){
                            for (int j = 1; j <= 4; j++){
                                listOfCards.Add(i);
                            }
                        } 

                        int dealerHand = 0;
                        int playerHand = 0;         
                        
                        //Gets the amount of players that will be playing the game and creates an array to store their hand value
                        int players = VerifyInput(4,3);
                        int[] playerArr = new int[players];

                        //Initial draw for the dealer
                        dealerHand += Draw(dealerHand);

                        //Loops through and asks each player if they want to stay or hit. If there hand is 21 or greater, it will automatically stay.
                        for (int i = 0; i < playerArr.Length; i++){
                            playerHand = 0;
                            playerHand += Draw(playerHand); //Draw Card 1
                            playerHand += Draw(playerHand); //Draw Card 2

                            while (playerHand < 21){
                                PrintMenu(4, playerHand, dealerHand, i+1);

                                choice = VerifyInput(2, 4, playerHand, dealerHand, i +1);

                                switch(choice){
                                    case 1:
                                        //Hit
                                        playerHand += Draw(playerHand);
                                        break;
                                    case 2:
                                        //Stay
                                        goto AfterLoopStay;
                                }        
                            }
                            AfterLoopStay:;
                            PrintMenu(4, playerHand, dealerHand, i+1);

                            //Notifies the player if they went over or got blackjack
                            if (playerHand == 21){
                                Console.WriteLine("Blackjack!");
                                Console.ReadKey();
                            }
                            else if (playerHand > 21){
                                Console.WriteLine("You went over!");
                                Console.ReadKey();
                            }
                            playerArr[i] = playerHand;
                        }

                        //The dealer draws from the deck given certain parameters
                        while(dealerHand < 17){
                            foreach (int i in playerArr){
                                if (i > dealerHand && i < 21){
                                    dealerHand += Draw(dealerHand);
                                }
                            }
                        }
                        //Now it is time to check which players have won, lost, or pushed
                        CheckWin(playerArr, dealerHand);

                        break;
                    case 2:
                        //Lists the rules
                        PrintMenu(2);
                        break;
                    case 3:
                        //Exits the program
                        exit = true;
                        break;
                }
            }
        }
        public static int Draw(int hand){
            int card = random.Next(listOfCards.Count);
            card = listOfCards[card];
            listOfCards.Remove(card);
            
            if (card == 1){
                if (hand < 11){
                    card = 11;
                }
                else{
                    card = 1;
                }
            }
            else if (card > 10){
                card = 10;
            }
            return card;
        }
        public static void CheckWin(int[] playerArr, int dealerHand){
            Console.Clear();
            Console.WriteLine("=====================================================================");
            Console.WriteLine("                            BLACKJACK                                ");
            Console.WriteLine("=====================================================================");
            Console.WriteLine(" - The dealer has a hand value of {0}",dealerHand);
            Console.WriteLine();
                        
            for (int i = 0; i < playerArr.Length; i++){
                int playerHand = playerArr[i];

                if (playerHand > dealerHand && playerHand <= 21 || playerHand <= 21 && dealerHand > 21){
                    //Win
                    Console.WriteLine("Player {0} wins with a hand value of {1}", i+1, playerArr[i]);
                }
                else if (playerHand == dealerHand && playerHand <= 21){
                    //Push or Draw
                    Console.WriteLine("Player {0} pushes with a hand value of {1}", i+1, playerArr[i]);
                }
                else if (playerHand < dealerHand || playerHand > 21){
                    //Lose
                    Console.WriteLine("Player {0} loses with a hand value of {1}", i+1, playerArr[i]);
                }
            }         
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        public static int VerifyInput(int optionMax, int menu, int playerHand = 0, int dealerHand = 0, int playerTurn = 0){
            int intChoice;
            int temp = -1;
            
            while(temp <= 0 || temp > optionMax){
                
                Console.Clear();
                
                if(playerHand != 0)
                    PrintMenu(4, playerHand, dealerHand, playerTurn);
                else
                    PrintMenu(menu);

                string choiceString = Console.ReadLine();

	            while(!int.TryParse(choiceString, out intChoice)){
		            Console.Clear();
                    
                    if(playerHand != 0)
                        PrintMenu(4, playerHand, dealerHand, playerTurn);
                    else
                        PrintMenu(menu);

		            choiceString = Console.ReadLine();
	            }
	            temp = intChoice;
            }
            intChoice = temp;
            return intChoice;
        }
        public static void PrintMenu(int menu, int playerHand = 0, int dealerHand = 0, int playerTurn = 0){
            switch(menu){
                case 1:
                    //Prints the main menu
                    Console.Clear();
                    Console.WriteLine("=====================================================================");
                    Console.WriteLine("                      WELCOME TO BLACKJACK!                          ");
                    Console.WriteLine("=====================================================================");
                    Console.WriteLine(" 1)Play");
                    Console.WriteLine(" 2)Rules");
                    Console.WriteLine(" 3)Exit");
                    break;
                case 2:
                    //Prints the rules
                    Console.Clear();
                    Console.WriteLine("=====================================================================");
                    Console.WriteLine("                       RULES OF BLACKJACK                            ");
                    Console.WriteLine("=====================================================================");
                    Console.WriteLine("- In the game of BlackJack, you are playing against the dealer.");
                    Console.WriteLine("  The goal of the game is to get a higher value hand than the  ");
                    Console.WriteLine("  dealer without having your hand go over 21. If you go over 21");
                    Console.WriteLine("  you lose. If your hand is lower than the dealers and they don't");
                    Console.WriteLine("  go over 21, you lose. If you have the same value hand as the");
                    Console.WriteLine("  dealer, that is a push and nobody wins. If you get 21, that is");
                    Console.WriteLine("  BlackJack or a natural. This is usually an instant win but if the");
                    Console.WriteLine("  dealer gets BlackJack, it is a draw. Also, if the dealers hand is");
                    Console.WriteLine("  valued at 17 or higher, they will not hit for another card.");
                    Console.WriteLine();
                    Console.WriteLine("  Press any key to continue...");
                    Console.ReadKey();
                    break;
                case 3:
                    //Prints the question asking how many players
                    Console.Clear();
                    Console.WriteLine("=====================================================================");
                    Console.WriteLine("                            BLACKJACK                                ");
                    Console.WriteLine("=====================================================================");
                    Console.WriteLine(" - How many players will there be (1-4)?");
                    break;
                case 4:
                    //Prints the game menu
                    Console.Clear();
                    Console.WriteLine("=====================================================================");
                    Console.WriteLine("                            BLACKJACK                                ");
                    Console.WriteLine("=====================================================================");
                    Console.WriteLine("Dealer has a hand with a value of {0} and an unknown card.",dealerHand);
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Player {0} hand value: {1}", playerTurn, playerHand);
                    Console.WriteLine(" 1)Hit");
                    Console.WriteLine(" 2)Stay");
                    break;   
            }
        }
    }
}
