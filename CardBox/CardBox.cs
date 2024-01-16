/*
 * Authors: David O'Riley, Travis Cowx, Tyler Calderone, Mitchell Hirst
 * Program Name: CardBox.cs
 * Date Created: 1/16/2017
 * Description: A control to hold a card's picture for the Durak application. The rank 
 *              and suit of the card will be stored and 'attached' to the card box instance.
 *              Includes event handlers for when the card is created, clicked, and
 *              hovered over. 
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cards;

namespace CardBox
{
    public partial class CardBox: UserControl
    {
        #region properties

        private PlayingCard myCard;
        public PlayingCard Card
        {
            get { return myCard; }
            set
            {
                myCard = value;
                UpdateCardImage();
            }
        }
        /// <summary>
        /// access the card suit property
        /// </summary>
        public CardSuit Suit
        {
            get { return Card.Suit; }
            set
            {
                Card.Suit = value;
                UpdateCardImage();
            }
        }

        /// <summary>
        /// access the card rank property
        /// </summary>
        public CardRank Rank
        {
            get { return Card.Rank; }
            set
            {
                Card.Rank = value;
                UpdateCardImage();
            }
        }

        /// <summary>
        /// access the card faceup property
        /// </summary>
        public bool FaceUp
        {
            get { return Card.FaceUp; }
            set
            {
                //check if value is different
                if (myCard.FaceUp != value) //card is flipping
                {
                    Card.FaceUp = value;
                    UpdateCardImage();

                    if (CardFlipped != null) //check if the cardflipped event has been coded
                        CardFlipped(this, new EventArgs()); //call event

                }
            }
        }


        private Orientation myOrientation;
        public Orientation CardOrientation
        {
            get { return myOrientation; }
            set
            {
                //only set if the value is different
                if(myOrientation != value)
                {
                    myOrientation = value; //change orientation
                    //swap the height and width
                    this.Size = new Size(Size.Height, Size.Width);
                    //update image
                    UpdateCardImage();
                }
            }
        }

        /// <summary>
        /// the type of back image set for the card
        /// set to a default of one for the first back
        /// option
        /// </summary>
        private int myBack = 1;
        public int Back
        {
            get { return myBack; }
            set
            {
                myBack = value;
                UpdateCardImage();
            }
        }

        /// <summary>
        /// front version of the card
        /// </summary>
        private int myFront = 1;
        /// <summary>
        /// update the card image based on the front version
        /// </summary>
        public int Front
        {
            get { return myFront; }
            set
            {
                myFront = value;
                UpdateCardImage();
            }
        }

        /// <summary>
        /// update the picture box image using the card and the orientaion
        /// </summary>
        private void UpdateCardImage()
        {
            //set the image for the card passing the backtype
            pbMyPictureBox.Image = myCard.GetCardImage(Back,Front);
                 
            if (myOrientation == Orientation.Horizontal)//chck if its horizontal
                pbMyPictureBox.Image.RotateFlip(RotateFlipType.Rotate90FlipNone); //rotate image
        }

        #endregion

        #region constructors
        /// <summary>
        /// default constructor: new card orientaed vertically
        /// </summary>
        public CardBox()
        {
            InitializeComponent(); //required method for designer support
            myOrientation = Orientation.Vertical;
            myCard = new PlayingCard();

        }
        /// <summary>
        /// parameter constructor
        /// </summary>
        /// <param name="card"></param>
        /// <param name="orientation">defaulted with vertical</param>
        public CardBox(PlayingCard card, Orientation orientation = Orientation.Vertical)
        {
            InitializeComponent();
            myOrientation = orientation;
            myCard = card;
        }

        /// <summary>
        /// cardbox that sets what kind of back and front version 
        /// to display
        /// </summary>
        /// <param name="card"></param>
        /// <param name="backVersion"></param>
        /// <param name="frontVersion"></param>
        public CardBox(PlayingCard card, int backVersion, int frontVersion)
        {
            InitializeComponent();
            CardOrientation = Orientation.Vertical;
            Back = backVersion;
            Front = frontVersion;

        }

        #endregion

        #region events and handlers

        /// <summary>
        /// on cardbox load update the card
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardBox_Load(object sender, EventArgs e)
        {
            UpdateCardImage(); //update the image
        }

        /// <summary>
        /// an even the client program can handle for the card flipped
        /// </summary>
        public event EventHandler CardFlipped;

        /// <summary>
        /// an event the client program can handle when the user clicks the control
        /// </summary>
        new public event EventHandler Click;

        /// <summary>
        /// Public event handler for hovering over a cardbox
        /// </summary>
        new public event EventHandler MouseEnter;

        /// <summary>
        /// Public event handler for the mouse leaving the hovering of a cardbox
        /// </summary>
        new public event MouseEventHandler MouseMove;

        /// <summary>
        /// handles the user clicking the picturebox control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbMyPictureBox_Click(object sender, EventArgs e)
        {
            if (Click != null) //user application click event is set
                Click(this, e);
        }

        /// <summary>
        /// An event handler for the user's mouse gaining the focus of the picturebox control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbMyPictureBox_MouseEnter(object sender, EventArgs e)
        {
            if (MouseEnter != null)     //If there is a handler for hovering over the control in the client program
                MouseEnter(this, e);    //Call it
        }

        /// <summary>
        /// An event handler for the user's mouse leaving the focus of the picturebox control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbMyPictureBox_MouseMove(object sender, MouseEventArgs e)
        {

        }

        #endregion

        #region other methods

        /// <summary>
        /// return the card tostring
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return myCard.ToString();
        }



        #endregion


    }
}
