/*
 * Authors: David O'Riley, Travis Cowx, Tyler Calderone, Mitchell Hirst
 * Program Name: frmInitialSettings.cs
 * Date Created: 03/25/2017
 * Description: A form for setting the initial settings of a Durak game including
 *              what card type to use (both front and back), the amount of cards in the deck,
 *              and the name of the human player. Provides validation for the player's name,
 *              writes a new player's info to the log file, and launches the MainGame form when
 *              'Start' is clicked.
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

namespace Durak
{
    public partial class frmInitialSettings : Form
    {
        /// <summary>
        /// private automatic property that will store the players data from a file.
        /// </summary>
        private Dictionary<string, string> PlayerData { get; set; }

        public frmInitialSettings()
        {
            InitializeComponent();
        }

        /// <summary>
        /// load the defaults of the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmInitialSettings_Load(object sender, EventArgs e)
        {
            //set up the cbo's to the first option
            cboBackType.SelectedIndex = 0;
            cboFrontType.SelectedIndex = 0;
            pbFront.FaceUp = true; //flip the card to show the front design
            checkPlayerData();
            if (PlayerData != null)
                txtName.Text = PlayerData["Name"];
            txtName.Focus();

        }
        /// <summary>
        /// change the back type of the card
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboBackType_SelectedIndexChanged(object sender, EventArgs e)
        {
            pbBack.Back = cboBackType.SelectedIndex + 1; //change the card back type to the selected index
        }

        /// <summary>
        /// change the front card version
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboFrontType_SelectedIndexChanged(object sender, EventArgs e)
        {
            pbFront.Front = cboFrontType.SelectedIndex + 1; //change the front type of the card
        }

        /// <summary>
        /// when back is clicked show next back version
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbBack_Click(object sender, EventArgs e)
        {
            const int backTypesMax = 5;     //Number of available back types

            //If the current back version is the last one, go to the first
            if (pbBack.Back == backTypesMax)
                cboBackType.SelectedIndex = 0;
            else //increment the selected index
                cboBackType.SelectedIndex++;
        }

        /// <summary>
        /// increment the front types by 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbFront_Click(object sender, EventArgs e)
        {
            const int frontTypesMax = 2;

            //if the currect front type is the last verion available go to the first
            if (pbFront.Front == frontTypesMax)
                cboFrontType.SelectedIndex = 0;
            else//increment the version type
                cboFrontType.SelectedIndex++;
        }

        /// <summary>
        /// start the main game form and pass all the necessary variables to it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (ValidName())
            {
              //*******************************************************************************************************
                PlayerData["Name"] = txtName.Text;
                //create a new main form
                frmMainGame game = new frmMainGame((cboFrontType.SelectedIndex + 1), (cboBackType.SelectedIndex + 1),
                    DetermineMinRank(), txtName.Text, PlayerData);
                game.Show();
                this.Hide();

            }
        }

        /// <summary>
        /// make sure the name isnt blank or larger then 20 characters
        /// </summary>
        private bool ValidName()
        {
            const int nameLengthMax = 20;
            bool valid = true;

            if (txtName.Text.Length == 0) //make sure name isnt empty
            {
                txtName.Text = "Name cannot be blank";
                txtName.ForeColor = Color.Red;
                valid = false;
            }
            else if (txtName.Text.Length > nameLengthMax)
            {
                txtName.Text = "Name cannot exceeded " + nameLengthMax + " characters";
                txtName.ForeColor = Color.Red;
                valid = false;
            }

            return valid;
        }

        /// <summary>
        /// determine what the minimum rank if for the deck
        /// based on what the user chose
        /// </summary>
        /// <returns></returns>
        private CardRank DetermineMinRank()
        {
            CardRank minRank;

            if (rdo52.Checked)
                minRank = CardRank.Ace;
            else if (rdo36.Checked)
                minRank = CardRank.Six;
            else
                minRank = CardRank.Ten;

            return minRank;
        }

        /// <summary>
        /// clear error from the text box and changed to font color back to black
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_Enter(object sender, EventArgs e)
        {
            if (txtName.ForeColor == Color.Red)
            {
                txtName.Text = string.Empty;
                txtName.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Check if the player has a file on record, if on record
        /// supply the name in the file to the text box
        /// </summary>
        private void checkPlayerData()
        {
            //create a file info
            FileInfo playerFile = new FileInfo("player_data.txt");
            if (playerFile.Exists) //check if the file exists
            {
                //dictionary to hold the various player data
                Dictionary<string, string> playerData = new Dictionary<string, string>();
                FileStream fs = playerFile.OpenRead();
                StreamReader sr = new StreamReader(fs); //reader to read the file
                string[] keyAndValue = new string[2]; //to hold the key and value of the line
                string line = sr.ReadLine(); //read the first line

                while (line != null) //line will be null at end or file
                {
                    keyAndValue = line.Split(':'); //split the line on the delimeter of :
                    playerData.Add(keyAndValue[0], keyAndValue[1]); //add the key and value to the dictionary
                    line = sr.ReadLine();
                }
                sr.Close();
                //set the text to the name key of the dictionary
                PlayerData = playerData;
            }
            else //file doesnt exist
            {
                //set up the player data dictionary to pass to the main game form
                Dictionary<string, string> playerData = new Dictionary<string, string>();
                playerData.Add("Name", "Player1");
                playerData.Add("GamesPlayed", "0");
                playerData.Add("Wins", "0");
                playerData.Add("Ties", "0");
                playerData.Add("Losses", "0");
                PlayerData = playerData;

                //FileStream fs = playerFile.OpenWrite();
                //StreamWriter sw = new StreamWriter(fs);
                //sw.WriteLine("Name:");
                //sw.WriteLine("Wins:");
                //sw.WriteLine("Ties:");
                //sw.WriteLine("Losses:");
                //sw.Close();

            }
        }
    }
}
