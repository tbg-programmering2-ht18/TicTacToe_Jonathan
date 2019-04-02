﻿using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe_Jonathan
{

    public partial class Form1 : Form
    {
        bool someone_won = false;
        bool turn = true;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ActiveControl = reset;
        }

        private bool checkContent(Button A1, Button A2, Button A3)
        {
            return (A1.Text == A2.Text) && (A2.Text == A3.Text) && (!A1.Enabled);
        }

        private void checkForWinner()
        {
            if (checkContent(A1, A2, A3) || checkContent(B1, B2, B3) || checkContent(C1, C2, C3) || checkContent(A1, B1, C1) || checkContent(A2, B2, C2) || checkContent(A3, B3, C3) || checkContent(A1, B2, C3) || checkContent(A3, B2, C1))
                someone_won = true;
           
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

                }

                String symbol = "";
                if (turn)
                    symbol = "O ";

                else
                    symbol = "X ";

                string gamestat = File.ReadAllText(grFilename);
                string[] arr = gamestat.Split('\n');
                int rownr = arr.Length;
                gamestat += "Game " + rownr.ToString() + ": " + symbol + Environment.NewLine;
                File.WriteAllText(grFilename, gamestat);

                

                MessageBox.Show(symbol + "has won!");

                foreach (Control x in this.Controls)
                {
                    if (x is Button && x.Tag == null)
                    {
                        x.Enabled = false;
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
            this.ActiveControl = reset;
            checkForWinner();
        }

        private void reset_Click(object sender, EventArgs e)
        {
            Application.Restart();
            Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

        }
    }
    
}

/*String path = @"C:\Temp\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            String urFilename = path + "user_register.txt";
            if (!File.Exists(urFilename))
            {
                FileStream f = File.Create(urFilename);
f.Close();
                userPasswdDict.Add("Jonis", "123");
                userPasswdDict.Add("PV", "321");
                userPasswdDict.Add("Adam", "rick");

                string jsonuserPasswd = JsonConvert.SerializeObject(userPasswdDict, Formatting.Indented);
File.WriteAllText(urFilename, jsonuserPasswd);
            }
            else
            {
                string json = File.ReadAllText(urFilename);
userPasswdDict = JsonConvert.DeserializeObject<Dictionary<String, String>>(json);
            }
            */
