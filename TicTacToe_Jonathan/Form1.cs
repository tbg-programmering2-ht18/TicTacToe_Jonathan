//Took help from: https://www.youtube.com/watch?v=p3gYVcggQOU&t=1709s
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace TicTacToe_Jonathan
{

    public partial class Form1 : Form
    {
        bool someone_won = false;
        bool turn = true; // A value that will determine who's turn it was at the time of game end.

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ActiveControl = reset;
        }

        private bool checkContent(Button A1, Button A2, Button A3) //checks for three of the same symbol in a row
        {
            return (A1.Text == A2.Text) && (A2.Text == A3.Text) && (!A1.Enabled);
        }

        private void checkForWinner()
        {
            if (checkContent(A1, A2, A3) || checkContent(B1, B2, B3) || checkContent(C1, C2, C3) || checkContent(A1, B1, C1) || checkContent(A2, B2, C2) || checkContent(A3, B3, C3) || checkContent(A1, B2, C3) || checkContent(A3, B2, C1))
                someone_won = true; //all the ways someone can win. Probably not the best way tp write the code as it's repetative, but it works.
           
            if (someone_won)
            {
                String path = @"C:\Temp\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                String grFilename = path + "game_nr.txt";
                if (!File.Exists(grFilename))
                {
                    FileStream f = File.Create(grFilename);
                    f.Close();

                } //If someone wins a file is created to store the scores.

                String symbol = "";
                if (turn)
                    symbol = "O ";

                else
                    symbol = "X "; // And the winner is set.
                try
                {
                    string gamestat = File.ReadAllText(grFilename);
                    string[] arr = gamestat.Split('\n');
                    int rownr = arr.Length;
                    gamestat += "Game " + rownr.ToString() + ": " + symbol + Environment.NewLine;
                    File.WriteAllText(grFilename, gamestat);
                }
                catch {MessageBox.Show("File-related ERROR");}  //Will read the file and count how many lines are in it. And if the file is gone or something an alert will tell you.
                

                MessageBox.Show(symbol + "has won!");  //Message to tell you that the game is over

                foreach (Control x in this.Controls)
                {
                    if (x is Button && x.Tag == null)
                    {
                        x.Enabled = false; //disables all the buttons without a tag.
                    }
                }
            }
        }

        private void button_click(object sender, EventArgs e)
        {
            Button x = (Button)sender;
            if (turn)
                x.Text = "X";
            else
                x.Text = "O";

            turn = !turn;
            x.Enabled = false;
            this.ActiveControl = reset;  //disables controls after being pressed
            checkForWinner();
        }

        private void reset_Click(object sender, EventArgs e)
        {
            Application.Restart();
            Environment.Exit(0);  // A crude but functional way to reset the application.
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Process.Start("C:\\Temp\\game_nr.txt");  //Opens the scoreboard.
        }
    }
    
}

//1. Press start
//2. Press a square to place your symbol. 
//3. Take turns with a friend for maximum effect.
//4. Realise you don't have friends and seriously question the choice to play tic-tac-toe alone
//5. Press score Button if you want to see which player (your right hand or left hand) won every game.