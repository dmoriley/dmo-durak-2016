/*
 * Authors: David O'Riley, Travis Cowx, Tyler Calderone, Mitchell Hirst
 * Program Name: Player.cs
 * Date Created: 02/07/2017
 * Description:  A class to store information on a player's name and their hand. Has methods
 *               for adding, removing, and sorting cards in a player's hand as well as 
 *               a way to store info on each of their cards as a string.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cards;

namespace Durak
{
    public class Player
    {
        #region PROPERTIES AND ATTRIBUTES

        /// <summary>
        /// Property for name, to check if the passed value isn't passed
        /// </summary>
        private String myName;
        public String Name
        {
            get { return myName; }
            set
            {
                if (value.Length == 0)
                    throw (new ArgumentException("Name cannot be blank"));
                else
                {
                    myName = value;
                }
            }
        }


        /// <summary>
        /// Property for the myCards attribute
        /// </summary>
        private List<PlayingCard> myCards = new List<PlayingCard>();
        public List<PlayingCard> Cards
        {
            get { return myCards; }
            set { myCards = value; }
        }

        #endregion


        #region CONSTRUCTORS

        /// <summary>
        /// Player - Constructor
        /// Sets the name of the player, defaults to "Player1"
        /// </summary>
        /// <param name="name"></param>
        public Player(string name = "Player1")
        {
            Name = name;
        }

        #endregion


        #region METHODS

        /// <summary>
        /// CardsString() Method
        /// Detailed list of each card in a player's hand
        /// </summary>
        /// <returns></returns>
        public string CardsString(bool forceFaceUp = true)
        {
            //make sure there are cards to show
            if (Cards.Count == 0)
                throw (new ArgumentException(Name + " has no cards to show.", "CardsString() Error"));

            string returnString = string.Format("\t{0}'s Cards\n\n", Name);
            //go through each card in the list and add its
            //tostring to the returnvalue

            foreach (PlayingCard card in Cards)
            {
                //check if the cards should be all face and thus display the card information
                if (forceFaceUp)
                {
                    //record the original card state for later
                    bool originalCardState = card.FaceUp;
                    card.FaceUp = true; //turn the card over
                    returnString += card.ToString() + "\n"; //add information to the string
                    card.FaceUp = originalCardState; //change the card state to the original
                }
                else //otherwise if will display a string of the cards in there current state
                    returnString += card.ToString() + "\n";
            }

            return returnString;
        }

        /// <summary>
        /// AddCard(card) Method
        /// Adds a card to the player's hand
        /// </summary>
        /// <param name="card">Card to be added to the player's hand</param>
        public void AddCard(PlayingCard card)
        {
            Cards.Add(card);
        }

        /// <summary>
        /// AddCards(Cards) Method
        /// Adds a list of cards into a player's hand
        /// </summary>
        /// <param name="cards">List of cards to be added to the player's hand</param>
        public void AddCards(List<PlayingCard> cards)
        {
            Cards.AddRange(cards); //Add multiple cards to the players deck
        }

        /// <summary>
        /// RemoveCard() Method
        /// Removes the card tom the player's hand
        /// </summary>
        /// <param name="card">Card to be removed</param>
        public void RemoveCard(PlayingCard card)
        {
            Cards.Remove(card); //Removes the first instance of the card passed to it
        }

        /// <summary>
        /// CardCount() Method
        /// Counts the number of cards in a player's hand
        /// </summary>
        /// <returns>Number of Cards in the player's hand</returns>
        public int CardCount()
        {
            return Cards.Count;
        }

        /// <summary>
        /// SortCards() Method
        /// Sorts the cards in a player's hand
        /// </summary>
        public void SortCards()
        {
            Cards.Sort();
        }

        /// <summary>
        /// toString method
        /// Displays the player's hand, and number of cards in their hand
        /// </summary>
        /// <returns>PlayerName has # cards in their hand.</returns>
        public string toString()
        {
            return string.Format("{0} has {1} cards in their hand.", Name, Cards.Count);
        }

        #endregion
    }
}
