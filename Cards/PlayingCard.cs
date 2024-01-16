/*
 * Authors: David O'Riley, Travis Cowx, Tyler Calderone, Mitchell Hirst
 * Program Name: PlayingCard.cs
 * Date Created: 03/01/2017
 * Description: A class for creating a playing card for using in card game programs
 *              (in this case, Durak) Each card will contain a suit, a rank, a value
 *              indicating if trump cards are used in the game (and if so, which suit
 *              is the trump), and a boolean value indicating if the card is facing up or down.
 *              Also includes IComparable for comparing cards and a custom ToString for displaying
 *              info on the current card. 
 * 
 */


/**
 *                      ATTRIBUTION
 * ==================================================
 * Card pictures taken from 
 * http://opengameart.org/content/playing-cards-0
 * Author: Iron Star Media
 */

using System;
using System.Drawing;

namespace Cards
{
    /// <summary>
    /// Used to represent a standard "French" playing card that can be used in
    /// several card game projects.
    /// </summary>
    public class PlayingCard : IComparable
    {

        #region FIELDS AND PROPERTIES

        /// <summary>
        /// Suit Property
        /// Used to set or get the Card Suit
        /// </summary>
        protected CardSuit mySuit;
        public CardSuit Suit
        {
            get { return mySuit; }      //Return the suit
            set { mySuit = value; }     //Set the suit
        }

        /// <summary>
        /// Rank Property
        /// Used to Set or get the Card Rank
        /// </summary>
        protected CardRank myRank;
        public CardRank Rank
        {
            get { return myRank; }      //Return the Rank
            set { myRank = value; }     //Set the rank
        }

        /// <summary>
        /// CardValue property
        /// Used to st or get the Card Value
        /// </summary>
        protected int myValue;
        public int Value
        {
            get { return myValue; }     //Return the Value
            set
            {
                //Check if the value being set is greater than the range allowed for a card
                if(value < (int)CardRank.Ace || value > (int)CardRank.Joker)
                {
                    throw new ArgumentOutOfRangeException(string.Format("{0} is out of range. Must be between {1} - {2}",
                        value, (int)CardRank.Ace, (int)CardRank.Joker));
                }
                myValue = value;
            }
        }

        /// <summary>
        /// Alternate Value Property
        /// Used to set or get an alternate value for certain games. Set to null by default
        /// </summary>
        protected int? altValue = null;     //nullable type
        public int? AlternateValue
        {
            get { return altValue; }    //Return the altValue
            set { altValue = value; }   //Set the altValue
        }

        /// <summary>
        /// FaceUp Property
        /// Used to set or get whether the card is face up.
        /// Set to false by default
        /// </summary>
        protected bool myFaceUp = false;
        public bool FaceUp
        {
            get { return myFaceUp; }      //Return the faceUp bool
            set { myFaceUp = value; }     //Set hte faceUp value
        }

        #endregion


        #region CONSTRUCTORS

        /// <summary>
        /// Card Constructor
        /// Initializes the playing card object. By default, the card is face down, with no alternate valure
        /// </summary>
        /// <param name="rank">The playing card rank. Defaults to Ace</param>
        /// <param name="suit">The playing card suit. Defaults to Hearts</param>
        public PlayingCard(CardRank rank = CardRank.Ace, CardSuit suit = CardSuit.Hearts)
        {
            //Set the rank & suit
            this.myRank = rank;
            this.mySuit = suit;
            //Set the default card value
            this.myValue = (int)rank;
        }
        #endregion


        #region METHODS

        /// <summary>
        /// CompareTo Method
        /// Card-specific comparison method used to sort Card instances. Compares this instance with another
        /// </summary>
        /// <param name="obj">The object this card is being comapred to</param>
        /// <returns>An integer that indicates whether this Card preceds, follows, or occurs in the same instance</returns>
        public int CompareTo(object value)
        {
            //Is the arguement null?
            if(value == null)
            {
                //Throw an arguement null exception
                throw new ArgumentNullException("Unable to compare a card to a null object");
            }

            if (value is PlayingCard)
            {
                PlayingCard card = (PlayingCard)value;//object not null convert to PlayingCard

                return (this.GetHashCode().CompareTo(card.GetHashCode())); //compare the hashcodes of the objects to determine sort

            }
            else //cant compare to object that is not of the family playingcard
                throw new ArgumentException(string.Format("Cannot compare PlayingCard object to type {0}.", value.GetType().ToString()));
        }

        /// <summary>
        /// Clone Method
        /// To Support the ICloneable interface. Used for deep copying in card collection classes
        /// </summary>
        /// <returns>A copy of the card as a System.Object</returns>
        public object Clone()
        {
            return this.MemberwiseClone();  //Return a memberwise clone.
        }

        /// <summary>
        /// ToString: Overrides System.Object.ToString()
        /// </summary>
        /// <returns>THe name of the card as a string</returns>
        public override string ToString()
        {
            string cardString;  //Holds the playing card name

            //If the card is face up
            if(myFaceUp)
            {
                //If the card is a joker
                if( myRank == CardRank.Joker)
                {
                    //set the name string to {Red|Black} Joker
                    //if the suit is black
                    if (mySuit == CardSuit.Clubs || mySuit == CardSuit.Spades)
                    {
                        //Set the name string to black joker
                        cardString = "Black Joker";
                    }
                    else    //The suit must be red
                    {
                        //Set the name string to red joker
                        cardString = "Red Joker";
                    }
                }
                else    //Otherwise, the card is a face up but not a Joker
                {
                    //Set the card name string to {Rank} of {Suit}
                    cardString = myRank.ToString() + " of " + mySuit.ToString();
                }
            }
            else   //Otherwise the card is face down
            {
                //set the card name to face dowm
                cardString = "Face-Down";
            }

            return cardString;      //Return the string
        }

        /// <summary>
        /// Equals: Overrides System.Object.Equals()
        /// </summary>
        /// <param name="obj">Object being compared to</param>
        /// <returns>True if the card values are equal</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                throw new ArgumentException("Cannot compare PlayingCard to a null object");

            if(obj is PlayingCard)
            {
                //Compare the values of the cards
                return (this.myValue == (obj as PlayingCard).Value && SameSuit(this, (obj as PlayingCard)));
            }
            else
                throw new ArgumentException(string.Format("Cannot compare PlayingCard object to type {0}.", obj.GetType().ToString()));
        }

        /// <summary>
        /// SameSuit(PlayingCard, PlayingCard) - Method
        /// Compares the suits of the two PlayingCard objects
        /// </summary>
        /// <param name="left">First card to be compared</param>
        /// <param name="right">Second Card to be compared</param>
        /// <returns>True if they are the same suit</returns>
        public static bool SameSuit(PlayingCard left, PlayingCard right)
        {
            return (left.Suit == right.Suit);
        }

        /// <summary>
        /// GetHashCode: Overrides System.Object.GetHashCode()
        /// </summary>
        /// <returns>((int)mySuit + 1) * 50 + Value</returns>
        public override int GetHashCode()
        {
            return ((int)mySuit + 1) * 50 + Value;
        }

        /// <summary>
        /// GetCardImage
        /// Gets the image assorciated with the card from the resource file.
        /// </summary>
        /// <returns>An image corresponding to the playing card.</returns>
        public Image GetCardImage(int backVersion = 1, int cardVersion = 1)
        {
            string imageName = "";   //The name of the image in the resources file
            Image cardImage;    //Holds the image

            //if the card is not face up
            if (!myFaceUp)
            {
                //Set the image name to "Back"
                imageName = "Back" + backVersion; //Sets it to the image name for the back of a card
            }
            else
            {
                if (cardVersion == 1)
                {
                    //build file name based on suit and rank
                    imageName = string.Format("{0}_{1}", myRank, mySuit);
                }
                else if (cardVersion == 2)
                {
                    //for some reason the below version wouldnt work
                    //imageName = string.Format("M{0}_{1}", myRank, mySuit);
                    imageName = "M" + myRank + "_" + mySuit;
                }
            }

            //Set the image to the appropriate object we get from the resources file
            cardImage = Properties.Resources.ResourceManager.GetObject(imageName) as Image;

            //return the image
            return cardImage;
        }

        /// <summary>
        /// DebugString
        /// Generates a string showing the state of the card object; useful for debug purposes
        /// </summary>
        /// <returns>A string showing the state of this card object</returns>
        public string DebugString()
        {
            string cardState = (string)(myRank.ToString() + " of " + mySuit.ToString()).PadLeft(20);
            cardState += (string)((FaceUp) ? "(Face Up)" : "(Face Down)").PadLeft(12);
            cardState += " Value: " + myValue.ToString().PadLeft(2);
            cardState += ((altValue != null) ? "/" + altValue.ToString() : "");
            return cardState;
        }


        #endregion


        #region RELATIONAL OPERATORS

        /// <summary>
        /// The overloaded equals operator for comparison between 2 cards
        /// </summary>
        /// <param name="left">First Card to Compare</param>
        /// <param name="right">Second Card to compare</param>
        /// <returns>True for they are equal, false otherwise</returns>
        public static bool operator == (PlayingCard left, PlayingCard right)
        {
            return (left.Value == right.Value);
        }

        /// <summary>
        /// The overloaded does not equal operator for comparison between to cards
        /// </summary>
        /// <param name="left">The first card to compare</param>
        /// <param name="right">The second card to compare</param>
        /// <returns>True if they are not equal, false otherwise</returns>
        public static bool operator !=(PlayingCard left, PlayingCard right)
        {
            return (left.Value != right.Value);
        }

        /// <summary>
        /// The overloaded less than operator
        /// </summary>
        /// <param name="left">The First card to compare if it's less than</param>
        /// <param name="right">The second card that's being compared to</param>
        /// <returns>True if the 1st card is less than the second</returns>
        public static bool operator <(PlayingCard left, PlayingCard right)
        {
            return (left.Value < right.Value);
        }

        /// <summary>
        /// The overloaded less than or equals to operator
        /// </summary>
        /// <param name="left">The first card being compared</param>
        /// <param name="right">The second card that is being compared to</param>
        /// <returns>True if the 1st card is equal to or less than the second</returns>
        public static bool operator <=(PlayingCard left, PlayingCard right)
        {
            return (left.Value <= right.Value);
        }

        /// <summary>
        /// The overloaded greater than operator
        /// </summary>
        /// <param name="left">The first card being compared</param>
        /// <param name="right">The second card that is being compared to</param>
        /// <returns>True if the 1st card is greater than the second</returns>
        public static bool operator >(PlayingCard left, PlayingCard right)
        {
            return (left.Value > right.Value);
        }

        /// <summary>
        /// The overloaded greater than or equal to operator
        /// </summary>
        /// <param name="left">The first card being compared</param>
        /// <param name="right">The second card that is being compared to</param>
        /// <returns>True if the 1st card is equal to or greater than the second</returns>
        public static bool operator >=(PlayingCard left, PlayingCard right)
        {
            return (left.Value >= right.Value);
        }

        #endregion

    }
}
