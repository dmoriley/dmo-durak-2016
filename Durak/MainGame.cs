/*
 * Authors: David O'Riley, Travis Cowx, Tyler Calderone, Mitchell Hirst
 * Program Name: frmMainGame.cs
 * Date Created: 02/07/2017
 * Description: A form to keep track of the current game of Durak being played. Displays the
 *              current cards in the deck faced-down, the trump card, and the cards in both the 
 *              human player's hand (face up) and the computer's hand (face down). Cards played 
 *              are placed into the middle green area of the form. Includes
 *              event interactions with the cards in the human player's hand, a log file to keep 
 *              track of every move in the game, and stats on previous games played. 
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cards;
using System.IO;
using CardBox;
using System.Diagnostics;

/**
 * ATTRIBUTION
 * ==================================================
 * These set of cards are under a Open Source license from the google code archive
 * https://code.google.com/archive/p/vector-playing-cards/
 * Felt Texture from
 * https://dribbble.com/shots/18239-Green-poker-table-felt
 * Wood Textures from
 * http://www.onlygfx.com/oak-wood-texture-vector-eps-svg/
 */


namespace Durak
{
    public partial class frmMainGame : Form
    {

        #region FIELDS AND PROPERTIES

        private int myFrontVersion;
        /// <summary>
        /// The front version of the card
        /// </summary>
        public int FrontVersion
        {
            get { return myFrontVersion; }
            set { myFrontVersion = value; }
        }

        private int myBackVersion;
        /// <summary>
        /// The back version of the card
        /// </summary>
        public int BackVersion
        {
            get { return myBackVersion; }
            set { myBackVersion = value; }
        }

        private CardRank myMinRank;
        /// <summary>
        /// The min rank card for the game
        /// </summary>
        public CardRank MinRank
        {
            get { return myMinRank; }
            set { myMinRank = value; }
        }

        private string myPlayerName;
        /// <summary>
        /// The name of the player in the game
        /// </summary>
        public string PlayerName
        {
            get { return myPlayerName; }
            set { myPlayerName = value; }
        }

        private DurakGame myGame;
        /// <summary>
        /// Holds the durak game
        /// </summary>
        public DurakGame Game
        {
            get { return myGame; }
            set { myGame = value; }
        }

        /// <summary>
        /// Automatic property that holds the data of the player
        /// </summary>
        public Dictionary<string, string> PlayerData { get; set; }

        /// <summary>
        /// Holds the control for the TrumpCard
        /// </summary>
        public CardBox.CardBox TrumpCard { get; set; }

        /// <summary>
        /// Automatic Property that keeps track of how many cards the humanplayer
        /// has on the table. Used for making the cardbox control names dynamically
        /// </summary>
        public int HumanCardCount { get; set; }

        /// <summary>
        /// Automatic Property that keeps track of how many cards the computerplayer
        /// has on the table used for making the cardbox control names dynamically
        /// </summary>
        public int ComputerCardCount { get; set; }

        /// <summary>
        /// List of cards in the deck to be played
        /// </summary>
        public List<CardBox.CardBox> FormDeck { get; set; }

        /// <summary>
        /// State that determiens whether the computer player is accepting cards
        /// </summary>
        public bool ComputerAcceptingCards { get; set; }

        /// <summary>
        /// Occurs when the computer added more cards to the table for the human to accept
        /// </summary>
        public bool ComputerAddedCards { get; set; }

        /// <summary>
        /// Determines whether the game is won or not
        /// </summary>
        public bool GameWon { get; set; }

        /// <summary>
        /// Streamwriter to write events that occur in the game to the logfile
        /// </summary>
        public StreamWriter Log { get; set; }

        #endregion

        #region VARIABLES

        private bool hoverCardCheck = false;    //An object to state whether a card is being hovered over
        private CardBox.CardBox focusCard;      //Object to hold the card that is beding focused
        private int fieldCardXLocation = 5;     //THe X position of the card that is being placed into the field

        #endregion

        #region CONSTANTS

        private const int HOVER_MOVE_DISTANCE = 25;         //The distance a card moves up when it is being hovered over
        private const int TABLE_LOCATION_X_START = 5;       //The initial location of the first card being played
        private const int COMP_TABLE_Y_LOCATION = 5;        //The Y location of the computer's card being placed
        private const int HUMAN_TABLE_Y_LOCATION = 25;      //The Y location of the human player's card being placed
        private const int ATTACKER_X_INCREMENT = 80;         //The X increment if somebody is the attacker
        private const int DEFENDER_X_INCREMENT = 40;         //The X increment if the defender is playing a card
        private const int HAND_INCREMENT_DISTANCE = 25;     //The increment distance of the card in a player's hand
        private const int CARD_WIDTH = 76;                  //Width of a cardbox

        #endregion


        /// <summary>
        /// Initializes the properties of the maingame
        /// Includes what type of card fronts and backs
        /// the person has chosen
        /// </summary>
        /// <param name="front">The front version of the card</param>
        /// <param name="back">The back version of the card</param>
        /// <param name="minRank">The lowest rank card value</param>
        /// <param name="playerName">The name of the player</param>
        /// <param name="playerData">The data information</param>
        public frmMainGame(int front, int back, CardRank minRank, string playerName, Dictionary<string, string> playerData)
        {
            InitializeComponent();
            FrontVersion = front;
            BackVersion = back;
            MinRank = minRank;
            PlayerName = playerName;
            PlayerData = playerData;
            ComputerAcceptingCards = false;
            GameWon = false;
        }


        /// <summary>
        /// Initialize the main durak game
        /// </summary>
        public void frmMainGame_Load(object sender, EventArgs e)
        {
            Game = new DurakGame(PlayerName, MinRank);  //Initialize the game

            //Add event handlers to the durak events
            Game.RolesSwitched += Durak_RolesSwitched;
            Game.AttackNotAllowed += Durak_BadAttack;
            Game.DefenceNotSameSuit += Durak_DefenceNotSameSuit;
            Game.DefenceTooSmall += Durak_DefenceTooSmall;
            Game.ComputerDefendUnsuccessful += Durak_ComputerAcceptCards;
            Game.CardsDealt += Durak_CardsDealt;
            Game.CardsDealtToPlayer += Durak_CardsDealtToPlayer;
            Game.CardAddedToTable += Durak_AddCardToTable;
            Game.ComputerEndAttack += Durak_ComputerEndAttack;
            Game.RoundEnd += Durak_EndRound;
            Game.ComputerAddedMoreCards += Durak_ComputerAddedMoreCards;
            Game.GameWon += Durak_GameWon;
            grpPlayerHand.MouseMove += grpPlayerHand_MouseMove;
           

            //Setup the logfile streamwriter
            string date = string.Format("{0:M-d-yyyy}", DateTime.Today);
            string fileName = "log-" + date + ".txt";                       //The filename of the log
            FileStream fs = new FileStream(fileName, FileMode.Append);      //Set up the filestream
            StreamWriter sw = new StreamWriter(fs);                         //Set up the stream writer
            Log = sw;                                                       //Set up the property log
            Log.WriteLine(PlayerData["Name"] + " challenging the computer. Date: " + DateTime.Today); //Write date to log

            Game.StartGame();       //Start the durak game
            LoadCardsAndDetails();  //Load the cards
            SetDefaults();          //Set up the text boxes
            IncrementGamesPlayed(); //Add the # of games player has played

        }

        #region METHODS

        /// <summary>
        /// SetDefaults() - Method
        /// Sets the defaults of the form for the game
        /// </summary>
        private void SetDefaults()
        {
            ComputerAcceptingCards = false;     //Reset the flag for computer accepting cards
            ComputerAddedCards = false;
            lblError.Text = string.Empty;
            lblMessages.Text = string.Empty;
            btnNewGame.Visible = false;
            lblError.Refresh();
            btnAccept.Text = "Accept";
            LoadPlayerData();                   //Load the labels with the player data

            //Set up the buttons' visibility to if the human player is attacking or defending
            if(Game.Attacker.Equals(Game.HumanPlayer))
            {            
                btnAccept.Visible = false;
                btnEndAttack.Visible = true;
            }
            else
            {
                btnAccept.Visible = true;
                btnEndAttack.Visible = false;
            }
        }

        /// <summary>
        /// LoadCardsAndDetails() - Method
        /// Load the Deck cards, and the player hands.
        /// Load the trump card and place it horizontal under the deck
        /// Update the Labels
        /// </summary>
        private void LoadCardsAndDetails()
        {
            lblError.Text = "";
            FormDeck = new List<CardBox.CardBox>();     //Initialize the deck that is shown on the form

            //Position of the X value for the deck
            int xPosition = 80;
            if ((int)MinRank == 1)
                xPosition = 125;

            //Load the cards in the deck area
            foreach(PlayingCard card in Game.CardDeck.Cards)
            {
                CardBox.CardBox cardControl = new CardBox.CardBox(card);
                cardControl.Back = BackVersion;                             //Set the back version of the card
                cardControl.Name = "cbFormDeck" + (FormDeck.Count + 1);     //Give the control a unique name
                //Change the X position for the cards to be able to see each card slightly
                cardControl.Location = new Point(xPosition, 200);
                this.Controls.Add(cardControl);                             //Add the control to the form
                FormDeck.Add(cardControl);                                  //Add the control to the FormDeck control list
                xPosition -= 3;                                             //Change the control distance    
            }

            //Load the Trump card onto the form
            Game.TrumpCard.FaceUp = true;   //Change the trump card to be face up
            Log.WriteLine("The trump card is {0}", Game.TrumpCard.ToString());      //Write the trump card to the log
            CardBox.CardBox trumpCard = new CardBox.CardBox(Game.TrumpCard);
            trumpCard.Front = FrontVersion;     //Set the front of the card
            trumpCard.CardOrientation = Orientation.Horizontal;     //Change the orientation of the trump

            //Determine the location based on the size of the deck
            if ((int)MinRank == 1)
                trumpCard.Location = new Point(125, 205);
            else
                trumpCard.Location = new Point(80, 205);

            trumpCard.SendToBack();             //Set the trump card to appear behind the deck
            this.Controls.Add(trumpCard);       //Add the control to the form
            TrumpCard = trumpCard;              //Set the TrumpCard property for the form

            InitiatePlayerHands();              //Load the player hands onto the table

            //Fill the labels on the form
            RefreshRoleLabels();
        }

        /// <summary>
        /// IncrementGamesPlayed() - Method
        /// Increment the games player in the PlayerData dictionary
        /// </summary>
        private void IncrementGamesPlayed()
        {
            //Increment the games player
            int games = Convert.ToInt32(PlayerData["GamesPlayed"]) + 1;
            PlayerData["GamesPlayed"] = games.ToString();

        }

        /// <summary>
        /// LoadPlayerData() - Method
        /// Load the player data onto the form
        /// </summary>
        private void LoadPlayerData()
        {
            lblGamesPlayedValue.Text = PlayerData["GamesPlayed"];
            lblWinsValue.Text = PlayerData["Wins"];
            lblLossesValue.Text = PlayerData["Losses"];
            lblTiesValue.Text = PlayerData["Ties"];
        }

        /// <summary>
        /// ClearPlayerData() - Method
        /// Set all the player data info to 0
        /// </summary>
        private void ClearPlayerData()
        {
            PlayerData["GamesPlayed"] = "0";
            PlayerData["Wins"] = "0";
            PlayerData["Losses"] = "0";
            PlayerData["Ties"] = "0";
        }

        /// <summary>
        /// AddCardToHumanHand(PlayingCard) - Method
        /// Add the card to the human's hand on the field
        /// </summary>
        /// <param name="card">Card to be added onto the field</param>
        private void AddCardToHumanHand(PlayingCard card)
        {
            int playerCardXLocation = TABLE_LOCATION_X_START;                   //Get the location of the start

            card.FaceUp = true;     //Set the card to be visible
            Log.WriteLine("{0} dealt the {1}", PlayerName, card.ToString());    //Write the card to the log file
            CardBox.CardBox cardControl = new CardBox.CardBox(card);            //Create the control
            cardControl.Front = FrontVersion;                                   //Set the front version of the card
            cardControl.Back = BackVersion;                                     //Set the back version of the card

            //Name the card with a unique number that is incremented
            cardControl.Name = "cbHumanCardNumber" + (++HumanCardCount);

            //Add the event handlers for the human's cards
            cardControl.Click += new EventHandler(PlayerCard_Click);
            cardControl.MouseEnter += new EventHandler(PictureBox_MouseEnter);
            cardControl.MouseMove += new MouseEventHandler(grpPlayerHand_MouseMove);

            //Add a distance for each card in the hand
            foreach (CardBox.CardBox cardBox in grpPlayerHand.Controls)
            {
                playerCardXLocation += HAND_INCREMENT_DISTANCE;         //Set the location where the new card would be played
            }

            grpPlayerHand.Controls.Add(cardControl);                //Add the control to the hand
            cardControl.Location = new Point(playerCardXLocation, 30);      //Set the location of the control
            cardControl.BringToFront();                             //Bring the card to the front
        }

        /// <summary>
        /// AddCardToComputerHand(PlayingCard) - Method
        /// Add the card to the computer's hand on the field
        /// </summary>
        /// <param name="card">Card to be added onto the field</param>
        private void AddCardToComputerHand(PlayingCard card)
        {
            int compCardXLocation = TABLE_LOCATION_X_START;

            card.FaceUp = true;    //Set the card to be face up so it can be read
            Log.WriteLine(string.Format("Computer dealt the {0}", card.ToString()));    //Write the card to the log stream
            card.FaceUp = false;    //Set the card to be face down
            CardBox.CardBox cardControl = new CardBox.CardBox(card);                //Create the control

            cardControl.Name = "cbComputerCardNumber" + (++ComputerCardCount);      //Set the name for the control
            cardControl.Front = FrontVersion;                                       //Set the front version of the control
            cardControl.Back = BackVersion;                                         //Set the back version of the control

            foreach (CardBox.CardBox cardBox in grpComputerHand.Controls)
            {
                compCardXLocation += HAND_INCREMENT_DISTANCE;                           //Increment the distance of the next control location
            }

            cardControl.BringToFront();                                             //Bring the card to the front
            grpComputerHand.Controls.Add(cardControl);                              //Add the card to the computer's hand
            cardControl.Location = new Point(compCardXLocation, 20);                //Set the location of the card
        }

        /// <summary>
        /// InitiatePlayerHands() - Method
        /// Load the player's hands onto the table
        /// </summary>
        private void InitiatePlayerHands()
        {
            Log.WriteLine("\nDealing Player Initial Hands: ");

            //Clear the controls in the players' hands
            grpPlayerHand.Controls.Clear();
            grpComputerHand.Controls.Clear();

            //Add each card from the human's hand to the table
            foreach(PlayingCard card in Game.HumanPlayer.Cards)
            {
                AddCardToHumanHand(card);           //Call method to add to the player's hand
            }

            //Add each card from the computer's hand to the tabl
            foreach(PlayingCard card in Game.ComputerPlayer.Cards)
            {
                AddCardToComputerHand(card);        //Call method to add the computer's hand
            }
        }

       
        /// <summary>
        /// RefreshPlayerHands(Player, PlayingCard) - Method
        /// Adds cards to a player hands. Used for inbetween rounds
        /// </summary>
        /// <param name="player">Player to have cards added to their hand</param>
        /// <param name="card">Card to be added to their hand</param>
        private void RefreshPlayerHands(Player player, PlayingCard card)
        {
            //Check if the card is a human or computer
            if (player.Equals(Game.HumanPlayer))
            {
                AddCardToHumanHand(card);
            }
            else  //The player is the computer player
            {
                AddCardToComputerHand(card);
            }
        }

        /// <summary>
        /// DeleteCardFromPlayer(Player, PlayingCard) - Method
        /// Delete the card control from a player's hand
        /// </summary>
        /// <param name="player">Player to have their control removed</param>
        /// <param name="card">Card to be removed</param>
        private void DeleteCardFromPlayer(Player player, PlayingCard card)
        {
            //Make sure it is the human player
            if (player.Equals(Game.HumanPlayer))
            {
                int? cardToDelete = null;
                int indexOfCard = 0;

                //Check each card in the hand
                for(int i = 0; i < grpPlayerHand.Controls.Count; i++)
                {
                    //Convert the control to cardbox and check if the card attribute is the same as the card passed
                    if ((grpPlayerHand.Controls[i] as CardBox.CardBox).Card.Equals(card))
                    {
                        cardToDelete = i;
                        indexOfCard = i;
                    }
                }

                //Check to see if there is a card found to be deleted
                if(cardToDelete.HasValue)
                {
                    CardBox.CardBox moveCard = new CardBox.CardBox();
                    //Write that the card has been played to the log
                    Log.WriteLine("{0} played the {1}", PlayerName, (grpPlayerHand.Controls[cardToDelete.Value] as CardBox.CardBox).Card.ToString());

                    //Iterate through each card in the computer's hand on the field
                    foreach (CardBox.CardBox currentCard in grpPlayerHand.Controls)
                    {
                        //Check to see if the card is further right then the card selected
                        if (currentCard.Location.X > grpPlayerHand.Controls[indexOfCard].Location.X)
                        {
                            int playerCardXLocation = currentCard.Location.X - HAND_INCREMENT_DISTANCE;
                            currentCard.Location = new Point(playerCardXLocation, 30);
                        }
                    }

                    //Remove the card
                    grpPlayerHand.Controls.RemoveAt(cardToDelete.Value);

                }
                else
                {
                    throw new ArgumentNullException("Card to delete is empty.");
                }
            }
            else  //Is the computer player
            {
                int? cardToDelete = null;
                int indexOfCard = 0;

                for (int i = 0; i < grpComputerHand.Controls.Count; i++)
                {
                    //Convert the control to cardbox and check if the card attribute is the same as the card passed
                    if ((grpComputerHand.Controls[i] as CardBox.CardBox).Card.Equals(card))
                    { 
                        cardToDelete = i;
                        indexOfCard = i;
                    }
                }

                //Check to see if there is a card found to be deleted
                if (cardToDelete.HasValue)
                {
                   // CardBox.CardBox moveCard = new CardBox.CardBox();
                    //Write that the card has been played to the log
                    Log.WriteLine("Computer played the {0}", (grpComputerHand.Controls[cardToDelete.Value] as CardBox.CardBox).Card.ToString());

                    //Rearrange the cards
                    //Iterate through each card in the computer's hand on the field
                    foreach (CardBox.CardBox currentCard in grpComputerHand.Controls)
                    {
                        //Check to see if the card is further right then the card selected
                        if (currentCard.Location.X > grpComputerHand.Controls[indexOfCard].Location.X)
                        {
                            int compCardXLocation = currentCard.Location.X - HAND_INCREMENT_DISTANCE;
                            currentCard.Location = new Point(compCardXLocation, 20);
                        }
                    }

                    //Remove the control                  
                    grpComputerHand.Controls.RemoveAt(cardToDelete.Value);


                }
            }
        }

        /// <summary>
        /// ClearTable() - Method
        /// Removes all the cards off of the table
        /// </summary>
        private void ClearTable()
        {
            pnlField.Controls.Clear();  //Clear the table of cards
            fieldCardXLocation = TABLE_LOCATION_X_START;        //Set the X location of the field cards to the default
        }

        /// <summary>
        /// RefreshDeck() - Method
        /// Delete cards from the deck
        /// </summary>
        private void RefreshDeck()
        {
            if(FormDeck.Count != 0)
            {
                //Get the amount of cards to be deleted
                int deleteCount = FormDeck.Count() - Game.CardDeck.CardsRemaining();

                //Get the control index from the form to delete one card from the deck
                for(int i = 0; i < deleteCount; i++)   //Loop through deleting the correct amount of cards
                {
                    int controlIndex = this.Controls.GetChildIndex(FormDeck[0]);
                    this.Controls[controlIndex].Dispose();  //Delete the card control from the form
                    FormDeck.RemoveAt(0);       //Remove the card from the list
                }
            }
        }

        /// <summary>
        /// AddCardToTable(Player, PlayingCard) - Method
        /// Add a playing card from the player's hand to the table. Location depends on attacking/defending
        /// or if it's the player or computer
        /// </summary>
        /// <param name="card">Card to be added</param>
        /// <param name="player">The player adding to the table</param>
        private void AddCardToTable(Player player, PlayingCard card)
        {
            card.FaceUp = true;         //Turn the card faceup
            CardBox.CardBox cardControl = new CardBox.CardBox(card);        //Create the new object
            cardControl.Name = "cbTableCard" + (Game.TableCards.Count + 1); //Name the object
            //Set the versions of cards
            cardControl.Front = FrontVersion;
            cardControl.Back = BackVersion;

            //Set the location based on if it is a human attacking or defending
            //Or a computer attacking or defending
            if(player == Game.HumanPlayer)
            {
                //If the attacker is the human
                if(Game.Attacker.Equals(Game.HumanPlayer))
                {
                    //Check to see if there is cards on the field, increment the distance depending if there is
                    if (pnlField.Controls.Count > 0)
                        fieldCardXLocation += ATTACKER_X_INCREMENT;
                    else    //There is no cards on the field
                        fieldCardXLocation = TABLE_LOCATION_X_START;

                    cardControl.Location = new Point(fieldCardXLocation, HUMAN_TABLE_Y_LOCATION);
                }
                else  //The human is defending
                {
                    fieldCardXLocation += DEFENDER_X_INCREMENT;             //Increment the X distance of the new card
                    cardControl.Location = new Point(fieldCardXLocation, HUMAN_TABLE_Y_LOCATION);
                }
            }
            else  //It is a computer player adding the card to the table
            {
                //Check if the attacker is computer
                if(Game.Attacker.Equals(Game.ComputerPlayer))
                {
                    //Check to see if there is cards on the field, increment the distance depending if there is
                    if (pnlField.Controls.Count > 0)
                        fieldCardXLocation += ATTACKER_X_INCREMENT;
                    else  //There is no cards on the field
                        fieldCardXLocation = TABLE_LOCATION_X_START;

                    cardControl.Location = new Point(fieldCardXLocation, COMP_TABLE_Y_LOCATION);
                }
                else  //The computer is defending
                {
                    fieldCardXLocation += DEFENDER_X_INCREMENT;             //Increment the X distance of the new card
                    cardControl.Location = new Point(fieldCardXLocation, COMP_TABLE_Y_LOCATION);
                }
            }
            

            //Add the cards to the table
            pnlField.Controls.Add(cardControl);
            cardControl.BringToFront();     //Make sure the card is in the front
        }

        /// <summary>
        /// RefreshRoleLabels() - Method
        /// Sets the role labels on who is attacking and who is defending
        /// </summary>
        private void RefreshRoleLabels()
        {
            //Write the role swap to the log file
            Log.WriteLine("Roles Switched. The Attacker is {0} and the the Defender is {1}", Game.Attacker.Name, Game.Defender.Name);
            lblAttackerValue.Text = Game.Attacker.Name;
            lblDefenderValue.Text = Game.Defender.Name;
        }

        /// <summary>
        /// Method MoveCardToOriginalPosition
        /// Moves the card down MOVE_DISTANCE(25) pixels.
        /// </summary>
        private void MoveCardToOriginalPosition()
        {
            //If the mouse isn't in the card
            if (!focusCard.ClientRectangle.Contains(focusCard.PointToClient(Control.MousePosition)))
            {
                int originalPosition = focusCard.Location.Y;                //Get the current position of the card's Y coordinate
                int newYPostion = originalPosition + HOVER_MOVE_DISTANCE;         //Set the new position for the Y coordinate

                focusCard.Location = new Point(focusCard.Location.X, newYPostion);   //Move the card to it's new location
                hoverCardCheck = false;
            }
        }

        #endregion

        #region CARD EVENT HANDLERS

        /// <summary>
        /// PlayerCard_Click(object, EventArgs) - Event Handler
        /// Sends the chosen card to the correct defence or attack
        /// then clears the error label
        /// </summary>
        /// <param name="sender">Card that is clicked</param>
        /// <param name="e"></param>
        private void PlayerCard_Click(object sender, EventArgs e)
        {
            lblMessages.Text = "";
            //Check to see if the game has been won
            if(!GameWon)
            {
                lblError.Text = "";     //Clear the error label

                //Check if the player is defending or attacking
                if(Game.Defender.Equals(Game.HumanPlayer))
                {
                    Game.PlayerDefend((sender as CardBox.CardBox).Card);
                }
                else
                {
                    //Check to see if the computer is accepting cards
                    if(ComputerAcceptingCards)
                    {
                        //Human player adds cards without the computer defending
                        Game.HumanAttack((sender as CardBox.CardBox).Card, true);
                    }
                    else   //Computer isn't accepting cards, so it's a normal attack
                    {
                        Game.PlayerAttack((sender as CardBox.CardBox).Card);
                    }
                }
            }
        }

        /// <summary>
        /// Method to handle the mouse entering the picturebox control
        /// Moves the card up 25 pixels
        /// </summary>
        private void PictureBox_MouseEnter(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Mouse entered");
            //If a card isn't currently being hovered over
            if (hoverCardCheck)
            {
                MoveCardToOriginalPosition();   //Move the card back to it's original position if it's in the control
            }

            if (!hoverCardCheck)
            {
                focusCard = sender as CardBox.CardBox;

                int originalPosition = focusCard.Location.Y;     //Get the current position of the card's Y coordinate
                int newYPostion = originalPosition - HOVER_MOVE_DISTANCE;         //Set the new position for the Y coordinate

                focusCard.Location = new Point(focusCard.Location.X, newYPostion);   //Move the card to it's new location
                hoverCardCheck = true;
            }
        }

        /// <summary>
        /// Method to handle a mouse moving around inside the player hand group controls
        /// If the mouse leaves the focus of a card while the card is hovering set the card back
        /// down to it's original position
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grpPlayerHand_MouseMove(object sender, MouseEventArgs e)
        {
            //If a card is being hovered over
            if (hoverCardCheck)
            {
                MoveCardToOriginalPosition();    //Move the card back to it's original position
            }
        }
        #endregion

        #region OTHER CONTROL EVENT HANDLERS

        /// <summary>
        /// btnClearPlayerData_Click(object, EventArgs) - Event Handler
        /// Handles when clear player is clicked. Clears the player data from
        /// the dictionary and refreshes the labels
        /// </summary>
        private void btnClearPlayerData_Click(object sender, EventArgs e)
        {
            ClearPlayerData();
            LoadPlayerData();
        }

        /// <summary>
        /// btnAccept_Click(object, EventArgs) - Event Handler
        /// When the player clicks Accept, they can't player anymore cards in defence
        /// and have to accept the cards that are on the table
        /// </summary>
        private void btnAccept_Click(object sender, EventArgs e)
        {
            if(string.Equals(btnAccept.Text, "Accept"))
            {
                Game.HumanAcceptCards();        //Computer player adds card to the table if he has any
                btnAccept.Text = "End Round";
            }
            else
            {
                Game.EndRound(false);                               //Adds the cards to the defender's ahdn
                Log.WriteLine("{0} accepted cards.", PlayerName);   //Write the player accepting cards to the log

                //Make sure the attacker is the computer player
                if (Game.Attacker.Equals(Game.ComputerPlayer))
                    Game.PlayerAttack();
            }
        }

        /// <summary>
        /// btnEndAttack_Click(object, EventArgs) - Event Handler
        /// The human player has decided to end their attack
        /// </summary>
        private void btnEndAttack_Click(object sender, EventArgs e)
        {
            //Write the action to the log
            Log.WriteLine("{0} ended their attack.", PlayerName);

            //Determine if the computer is accepting cards
            if (ComputerAcceptingCards)
                Game.EndRound(false);
            else  //They aren't accepting cards
                Game.EndRound();

            //start the computers attack going
            if(Game.Attacker.Equals(Game.ComputerPlayer))
                Game.PlayerAttack();
        }

        /// <summary>
        /// btnNewGame_Click(object, EventArgs) - Event Handler
        /// Handles when a player clicks the new game button
        /// </summary>
        private void btnNewGame_Click(object sender, EventArgs e)
        {
            //Write new game to the log
            Log.WriteLine("New Game Started...");

            //Clear all the cards from the table and players' hands
            Game.ClearAllCards();
            grpComputerHand.Controls.Clear();       //Get rid of the cards in the computer's hand
            grpPlayerHand.Controls.Clear();         //Get rid of the cards in the player's hand
            ClearTable();                           //Clear the cards on the table
            this.Controls.Remove(TrumpCard);        //Get rid of the trump card

            btnNewGame.Visible = false;             //Hide the button
            Game.StartGame(MinRank);                //Reset the deck
            LoadCardsAndDetails();                  //Loads the cards and details for the game
            SetDefaults();                          //Set the defaults for the buttons and labels
            IncrementGamesPlayed();                 //Increment the number of games that the palyer has played
        }

        #endregion

        #region GAME EVENT HANDLERS

        /// <summary>
        /// Durak_RolesSwitched - Event Handler
        /// Handles when the computer and human player switch roles.
        /// Updates the label
        /// </summary>
        /// <param name="sender"></param>
        private void Durak_RolesSwitched(object sender, EventArgs e)
        {
            RefreshRoleLabels();
        }

        /// <summary>
        /// Durak_CardsDealt - Event Handler
        /// Cards have been dealt to all players, clear the table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Durak_CardsDealt(object sender, EventArgs e)
        {
            ClearTable();
        }

        /// <summary>
        /// Durak_CardsDealtToPlayer - Event Handler
        /// Occurs when a card is dealt to a player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Durak_CardsDealtToPlayer(object sender, MultipleEventArgs args)
        {
            RefreshDeck();
            RefreshPlayerHands((args.EventObjects[0] as Player), (args.EventObjects[1] as PlayingCard));
        }

        /// <summary>
        /// Durak_BadAttack - Event Handler
        /// Handles when a player has clicked a card that can't be played
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Durak_BadAttack(object sender, PlayingCard e)
        {
            //Update the error label telling the player they can't attack with that card
            lblError.Text = string.Format("You can't attack the {0}. There are no {1}'s in play.", e.ToString(), e.Rank);
        }

        /// <summary>
        /// Durak_DefenceNotSameSuit - Event Handler
        /// Handles when a player has clicked a card that isn't the same suit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Durak_DefenceNotSameSuit(object sender, PlayingCard e)
        {
            //Update the error label telling the player that the suit isn't the same
            lblError.Text = string.Format("You can't defend with the {0}. It is not the same suit.", e.ToString());
        }

        /// <summary>
        /// Durak_DefenceTooSmall - Event Handler
        /// Handles when a player had clicked a card that they can't defend with
        /// </summary>
        private void Durak_DefenceTooSmall(object sender, PlayingCard e)
        {
            //Update the error label telling the player that the defence rank is too small
            lblError.Text = string.Format("The {0}'s value is too small to defend with.", e.Rank);
        }

        /// <summary>
        /// Durak_ComputerAcceptCards - Event Handler
        /// Handles when a computer player is accepting the cards
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Durak_ComputerAcceptCards(object sender, EventArgs e)
        {
            Log.WriteLine("Computer accepted cards.");      //Write event to log file
            lblMessages.Text = "COMPUTER ACCEPTS THE CARDS. ADD ANY MORE ELEGIBLE CARDS YOU WISH.";
            ComputerAcceptingCards = true;
        }

        /// <summary>
        /// Durak_AddCardToTable - Event Handler
        /// Handles when a card is added to the table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Durak_AddCardToTable(object sender, MultipleEventArgs args)
        {
            AddCardToTable(args.EventObjects[0] as Player, args.EventObjects[1] as PlayingCard);        //Add card to the table
            DeleteCardFromPlayer((args.EventObjects[0] as Player), (args.EventObjects[1] as PlayingCard));      //Refreshes the player's hand
        }

        /// <summary>
        /// Durak_ComputerEndAttack - Event Handler
        /// Handles when a computer has ended their attack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Durak_ComputerEndAttack(object sender, EventArgs e)
        {
            Log.WriteLine("Computer ended their attack.");      //Write the event to the log
            Game.EndRound();        //End the round and remove the cards from the table
        }

        /// <summary>
        /// Durak_EndRound - Event Handler
        /// Handles when the round has ended
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Durak_EndRound(object sender, EventArgs e)
        {
            Log.WriteLine("End of round.");     //Write the event to the log
            SetDefaults();
            lblMessages.Text = Game.Attacker.Name.ToUpper() + " IS ATTACKING";
        }

        /// <summary>
        /// Durak_ComputerAddedMoreCards - Event Handler
        /// Handles when the computer has added more cards to the field when
        /// the player has clicked "accept cards"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Durak_ComputerAddedMoreCards(object sender, EventArgs e)
        {
            lblMessages.Text = "THE COMPUTER ADDED MORE CARDS FOR YOU TO ACCEPT";
            ComputerAddedCards = true;
        }

        /// <summary>
        /// Durak_GameWon - Event Handler
        /// Handles when a player has used all their cards and won the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="winner"></param>
        private void Durak_GameWon(object sender, Player winner)
        {
            Log.WriteLine("{0} has won the game!", winner.Name);        //Write the event to the log
            lblMessages.Text = string.Format("{0} HAS WON THE GAME! Start new game?", winner.Name.ToUpper());

            //Set the visibility for the buttons
            btnAccept.Visible = false;
            btnEndAttack.Visible = false;
            btnNewGame.Visible = true;

            GameWon = true;     //Set the GameWon property to true

            //Check to see if the human player has won or the computer
            if(winner.Equals(Game.HumanPlayer))
            {
                //Increment the number of wins the player has
                int winCount = Convert.ToInt32(PlayerData["Wins"]);
                winCount++;                                     //Increment the number of wins
                PlayerData["Wins"] = winCount.ToString();       //Convert back into a string and store it in playerdata
            }
            else  //The player has lost
            {
                //Increment the number of losses the player has
                int lossCount = Convert.ToInt32(PlayerData["Losses"]);
                lossCount++;                                    //Increment the number of losses
                PlayerData["Losses"] = lossCount.ToString();    //Convert back into a string and store it in playerdata
            }
        }

        #endregion


        #region CLOSE APPLICATION

        /// <summary>
        /// frmMainGame_FormClosing - Event Handler
        /// Close the application when exiting
        /// Also closes the hidden settings form instead of 
        /// the application remaining in limbo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMainGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            Log.Close();        //Close the log stream
            //If the games data was reset to zero, increment games played to 1 on close
            if (PlayerData["GamesPlayed"].Equals("0"))
                PlayerData["GamesPlayer"] = "1";

            //Write the player data to the file
            FileStream fs = new FileStream("player_data.txt", FileMode.Create); //Overrite the file
            StreamWriter sw = new StreamWriter(fs);
            string line = "";

            //Loop through the player data dictionary
            foreach (KeyValuePair<string, string> entry in PlayerData)
            {
                line = string.Format("{0}:{1}", entry.Key, entry.Value);
                sw.WriteLine(line);
            }
            sw.Close();         //Close the application

            Application.Exit();
        }


        #endregion
    }
}
