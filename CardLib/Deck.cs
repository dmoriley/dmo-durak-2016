/*
 * Authors: David O'Riley, Travis Cowx, Tyler Calderone, Mitchell Hirst
 * Program Name: Deck.cs
 * Date Created: 1/16/2017
 * Description: A class for instantiating, shuffling, and holding the values of cards.
 *              Includes a value to check if aces are the highest or lowest value in
 *              the deck as well as if this is a deck utilizing trump cards.
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLib
{
    public class Deck : ICloneable
    {
        //Event handler for last card drawn
        public EventHandler LastCardDrawn;

        /// <summary>
        /// Nondefault constructor. Allows aces to be set high
        /// </summary>
        /// <param name="isAceHigh">Set true if aces are high</param>
        public Deck(bool isAceHigh) : this()
        {
            Card.isAceHigh = isAceHigh;
        }

        /// <summary>
        /// Nondefault constructor. Allows a trump suit to be use
        /// </summary>
        /// <param name="useTrumps">Set true if trumps are being used</param>
        /// <param name="trump">Pass the suit to be trump</param>
        public Deck(bool useTrumps, Suit trump) : this()
        {
            Card.useTrumps = useTrumps;
            Card.trump = trump;
        }

        /// <summary>
        /// Nondefault constructor. Allows aces to be set high and a trump suit
        /// </summary>
        /// <param name="isAceHigh">Set true if aces are high</param>
        /// <param name="useTrumps">Set true if trumps are being used</param>
        /// <param name="trump">Pass the suit to be trump</param>
        public Deck(bool isAceHigh, bool useTrumps, Suit trump) : this()
        {
            Card.isAceHigh = isAceHigh;
            Card.useTrumps = useTrumps;
            Card.trump = trump;
        }

        //Copies a deck object
        public object Clone()
        {
            Deck newDeck = new Deck(cards.Clone() as Cards);
            return newDeck;
        }

        //Constructor
        public Deck(Cards newCards)
        {
            cards = newCards;
        }

        private Cards cards = new Cards();

        //Constructor, creates a deck of 52 cards
        public Deck()
        {
            for (int suitVal = 0; suitVal < 4; suitVal++)
            {
                for (int rankVal = 1; rankVal < 14; rankVal++)
                {
                    cards.Add(new Card((Suit)suitVal, (Rank)rankVal));
                }
            }
        }

        //Draws a get from the deck
        public Card GetCard(int cardNum)
        {
            if (cardNum >= 0 && cardNum <= 51)
            {
                if ((cardNum == 51) && (LastCardDrawn != null))
                    LastCardDrawn(this, EventArgs.Empty);

                return cards[cardNum];
            }
            else
            {
                throw new CardOutOfRangeException(cards.Clone() as Cards);
            }
        }

        //Shuffles the Card Objects throughout the deck
        public void Shuffle()
        {
            Cards newDeck = new Cards();
            bool[] assigned = new bool[52];
            Random sourceGen = new Random();

            for (int i = 0; i < 52; i++)
            {
                int sourceCard = 0;
                bool foundCard = false;
                while (foundCard == false)
                {
                    sourceCard = sourceGen.Next(52);
                    if (assigned[sourceCard] == false)
                        foundCard = true;
                }
                assigned[sourceCard] = true;
                newDeck.Add(cards[sourceCard]);
            }
            newDeck.CopyTo(cards);
        }
    }
}
