/*
 * Authors: David O'Riley, Travis Cowx, Tyler Calderone, Mitchell Hirst
 * Program Name: Deck.cs
 * Date Created: 02/10/2017
 * Description: A class for instantiating, shuffling, and holding the values of cards.
 *              Includes a value to check if aces are the highest or lowest value in
 *              the deck as well as if this is a deck utilizing trump cards. Keeps track
 *              of the last card drawn from the deck, and allows for the drawing of cards 
 *              in general.
 * 
 */
/*
 * Deck.cs
 * Author: Travis Cowx
 * Date: Feb 2017
 * Description: THe deck class, instatiates, shuffles, and holds card values
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cards;

namespace Cards
{
    /// <summary>
    /// Holds a deck of cards
    /// </summary>
    public class Deck
    {

        #region FIELDS AND PROPERTIES

        /// <summary>
        /// List of cards that the deck holds
        /// </summary>
        private List<PlayingCard> myCards = new List<PlayingCard>();
        public List<PlayingCard> Cards
        {
            get { return myCards; }
        }

        //Event handler for last card drawn
        public EventHandler LastCardDrawn;

        /// <summary>
        /// Attribute to get the minimum rank in the deck. Defaulted to 1
        /// Cooresponds with the enum rank
        /// </summary>
        protected int myMinRank = 1;
        public int MinRank
        {

            get { return myMinRank; }
            private set { myMinRank = value; }
        }

        /// <summary>
        /// Attribute to get whether to include jokers
        /// </summary>
        protected bool myIncludeJokers;
        public bool IncludeJokers
        {
            //only getter for include jokers because you shouldnt be able to choose to include if jokes are included
            //after the deck has been made.
            get { return myIncludeJokers; }
            private set { myIncludeJokers = value; }
        }

        /// <summary>
        /// Attribute to determine whether to include aces
        /// </summary>
        protected bool? myIncludeAces = null;
        public bool IncludeAces
        {
            //return the value of inc
            get { return myIncludeAces ?? false; }
            private set { myIncludeAces = value; }
        }

        #endregion

        #region CONSTRUCTORS

        //Constructor
        public Deck(PlayingCard newCards)
        {
            cards = newCards;
        }

        private PlayingCard cards = new PlayingCard();

        /// <summary>
        /// Initialize the deck, and determine whether to include te jokers
        /// </summary>
        /// <param name="includeJokers"></param>
        public Deck(bool includeJokers = false, bool? includeAces = null, CardRank? minRank = null)
        {
            IncludeJokers = includeJokers;

            //check if the minRank has been set
            if (minRank.HasValue)
            {
                MinRank = (int)minRank.Value; //set the min rank
                //check if the min rank is greater then one
                if ((int)minRank.Value > 1)
                {
                    //no point is setting has aces if min rank already includes them
                    //so now check if the include aces has a value
                    if (includeAces.HasValue)
                        IncludeAces = includeAces.Value;
                }

            }

            Initialize();
        }

        /// <summary>
        /// Overloaded constructor if only the min rank wants to be set
        /// </summary>
        /// <param name="minRank"></param>
        public Deck(CardRank minRank) : this(false, null, minRank) { }

        /// <summary>
        /// Overloaded constructor if the min rank and the include aces wants to be set
        /// </summary>
        /// <param name="minRank"></param>
        /// <param name="includeAces"></param>
        public Deck(CardRank minRank, bool includeAces) : this(false, includeAces, minRank) { }

        #endregion

        #region METHODS

        /// <summary>
        /// Initialize a deck of 52 cards. 54 Cards if jokers are included
        /// </summary>
        /// <param name="includeJokers">A boolean to check if jokers are included. True if they are to be included</param>
        protected void Initialize()
        {
            int maxSuits = 4;
            int maxRank = 13;

            int maxRankWithJokers = 14;

            //go through eachsuit
            for (int i = 0; i < maxSuits; i++)
            {
                //go through each rank starting at the min card rank
                for (int j = MinRank; j <= ((myIncludeJokers) ? maxRankWithJokers : maxRank); j++)
                {
                    myCards.Add(new PlayingCard((CardRank)j, (CardSuit)i)); //add cards to the list
                }
                //if the min rank is set above the ace but the aces still want to be included
                //add the ace of that suit to the deck
                if (MinRank > 1 && IncludeAces == true)
                {
                    myCards.Add(new PlayingCard(CardRank.Ace, (CardSuit)i));
                }
            }
        }

        /// <summary>
        /// Method to return the number of cards remaining
        /// </summary>
        /// <returns>The number of cards remaining in the deck</returns>
        public int CardsRemaining()
        {
            return myCards.Count();
        }


        /// <summary>
        /// DrawNextCard() - Method
        /// Draws the card at the top of the deck
        /// </summary>
        /// <returns>The next card</returns>
        public PlayingCard DrawNextCard()
        {
            //If there is no cards in the deck, throw an exception
            if (myCards.Count == 0)
                throw new ArgumentNullException("DrawNextCard: No more cards in the deck");

            PlayingCard card = myCards.ElementAt(0);    //Declare the card object from the first card in the deck
            myCards.RemoveAt(0);                        //Remove the card from the deck
            return card;                                //Return the Card

        }

        /// <summary>
        /// Shuffle() - Method
        /// Shuffles the PlayingCard objects in the Deck
        /// </summary>
        public void Shuffle()
        {
            PlayingCard tempCard;   //A temporary card
            Random sourceGen = new Random();                        //A random seed generator

            //Loop to randomly place cards in the deck
            for(int i = 0; i < myCards.Count(); i++)
            {
                int randomCard = sourceGen.Next(CardsRemaining() - 1);
                tempCard = myCards.ElementAt(randomCard);
                myCards[randomCard] = myCards[i];
                myCards[i] = tempCard;
            }
        }
        #endregion

    }
}
