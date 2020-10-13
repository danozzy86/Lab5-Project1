# Lab5-Project1
 Blackjack

# How to make better
 Use objects to simplify 
 possibly introduce enums as for the card values
 
# Need to work on
 Everytime I try to use or understand objects more it just confuses me more, should probably try to go over them.
 This is why there were no objects present in this lab

# Possible Features
 Add a split method that will allow the player to split their hand if they have 2 matching cards in hand.
 Create a value or currency that players can use to bet.
 Add art in the console that can be used to represent the cards in the dealers and players hand.

# Known Bugs
 When playing with one player, if the player goes over 21, the program halts and no longer continues to the CheckWin screen.
 !!!Temporarily fixed with a goto!!! - Problem is that with only one player, if they go over 21 it gets stuck in the loop used to decide whether
 the dealer draws a card or not. If the dealers hand is less than 17 when the player goes over, it will never exit the while loop.
 !!!NEED TO REDESIGN LOOP STARTING LINE 81!!!