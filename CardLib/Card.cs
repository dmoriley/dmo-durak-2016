/*
 * Authors: David O'Riley, Travis Cowx, Tyler Calderone, Mitchell Hirst
 * Program Name: Card.cs
 * Date Created: 02/05/2017
 * Description: A class for creating a playing card for using in card game programs
 *              (in this case, Durak) Each card will contain a suit, a rank, a value
 *              indicating if trump cards are used in the game (and if so, which suit
 *              is the trump), and a boolean value indicating if the card is facing up or down. 
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLib
{
    public class Card : ICloneable
    {
        /// <summary>
        /// Flag for trump usage. If true, trumps are valued higher
        /// than cards of other suits.
        /// </summary>
        public static bool useTrumps = false;

        /// <summary>
        /// Trump suit to use if useTrumps is true
        /// </summary>
        public static Suit trump = Suit.Club;

        /// <summary>
        /// Flag that determines whether aces are higher than kings or lower
        /// than deuces
        /// </summary>
        public static bool isAceHigh = true;

        //Method that clones a class object
        public object Clone()
        {
            return MemberwiseClone();
        }

        public readonly Rank rank;
        public readonly Suit suit;

        public Card(Suit newSuit, Rank newRank)
        {
            suit = newSuit;
            rank = newRank;
        }

        private Card()
        {

        }

        public override String ToString()
        {
            return "The " + rank + " of " + suit + "s";
        }

        /// <summary>
        /// Compares two cards, both suit & rank
        /// </summary>
        /// <param name="card1">The first card to compare</param>
        /// <param name="card2">The second card to compare</param>
        /// <returns></returns>
        public static bool operator ==(Card card1, Card card2)
        {
            return (card1.suit == card2.suit) && (card1.rank == card2.rank);
        }

        /// <summary>
        /// Returns the result of checking if two cards do not equal
        /// </summary>
        /// <param name="card1">The first card to compare</param>
        /// <param name="card2">The second card to compare</param>
        /// <returns></returns>
        public static bool operator !=(Card card1, Card card2)
        {
            //Use the already overloaded operator and use a not to reverse
            return !(card1 == card2);
        }

        /// <summary>
        /// Use overloaded == operator to compare cards
        /// </summary>
        /// <param name="card">Card to be compared to</param>
        /// <returns></returns>
        public override bool Equals(object card)
        {
            return this == (Card)card;
        }

        /// <summary>
        /// Get the hard code based on the rank and suit
        /// </summary>
        /// <returns>13 * (int)suit + (int)rank;</returns>
        public override int GetHashCode()
        {
            return 13 * (int)suit + (int)rank;
        }

        /// <summary>
        /// Check if the value of card1 is smaller than the value of card2
        /// </summary>
        /// <param name="card1">The first card to compare</param>
        /// <param name="card2">The second card to compare</param>
        /// <returns>True if Card1 is greater than card 2</returns>
        public static bool operator >(Card card1, Card card2)
        {
            //If they are the same suit
            if (card1.suit == card2.suit)
            {
                //Check if Ace is high
                if (isAceHigh)
                {
                    //Check to see if card 1 is ace
                    if (card1.rank == Rank.Ace)
                    {
                        //Check to see if the card2 is an ace
                        if (card2.rank == Rank.Ace)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else   //Otherwise
                    {
                        //Check if card2 is ace
                        if (card2.rank == Rank.Ace)
                        {
                            return false;
                        }
                        else
                        {
                            return (card1.rank > card2.rank);   //Return result of checking if rank1 > rank2
                        }
                    }
                }
                else
                {
                    return (card1.rank > card2.rank);   //Return result of checking if rank1 > rank2
                }
            }
            else
            {
                //If using trumps, check to see if the second card is a trump
                if (useTrumps && (card2.suit == Card.trump))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Check to see if Card1 is less than card2
        /// </summary>
        /// <param name="card1">The first card to compare</param>
        /// <param name="card2">The second card to compare</param>
        /// <returns></returns>
        public static bool operator <(Card card1, Card card2)
        {
            return !(card1 >= card2);
        }


        /// <summary>
        /// Check to see if card1 is greater than or equal to card2
        /// </summary>
        /// <param name="card1">The first card to compare</param>
        /// <param name="card2">The second card to compare</param>
        /// <returns></returns>
        public static bool operator >=(Card card1, Card card2)
        {
            //If they are the same suit
            if (card1.suit == card2.suit)
            {
                //Check if ace is high
                if (isAceHigh)
                {
                    //If they are the same suit and card1 is an ace it's highest possible value
                    if (card1.rank == Rank.Ace)
                    {
                        return true;
                    }
                    else   //otherwise
                    {
                        //If the second card is an ace, highest possible value so card1 can't be greater
                        if (card2.rank == Rank.Ace)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
                else   //Otherwise
                {
                    return (card1.rank >= card2.rank);  //Check to see if they have the same rank
                }
            }
            else
            {
                //If using trumps, check to see if the second card is a trump
                if (useTrumps && (card2.suit == trump))
                {
                    return false;
                }
                else   // otherwise
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Check to see if card1 is less than or equal to card2
        /// </summary>
        /// <param name="card1">The first card to compare</param>
        /// <param name="card2">The second card to compare</param>
        /// <returns></returns>
        public static bool operator <=(Card card1, Card card2)
        {
            return !(card1 > card2);
        }
    }
}
