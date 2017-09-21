using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tic_Tac_Toe_AI
{
    public partial class Form2 : Form
    {

        Board b; // game board
        Char player = 'X'; // starting player is X

        Form test; //better solution to navigate forms back and forward
        public Form2(Form menu_form)
        {
            InitializeComponent();
            test = menu_form;

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            //show menu form
            // Menu start = new Menu();
            //start.Show();
            test.Show();
        }

        private void Form2_Resize(object sender, EventArgs e)
        {
            //size
            foreach (Control c in this.Controls)
            {

                if (c is Button)
                {
                    c.Width = (this.Width - 16) / 6;
                    c.Height = (this.Height - 38) / 6;
                }

                //font in all controls
                c.Font = new Font(c.Font.Name, this.Height / 20, FontStyle.Bold);
                // c.Font = new Font(c.Font.Name, this.ClientSize.Height / 20, FontStyle.Bold); // ClientSize.Height gives error

            }

            //position

            int start_x = ((this.Width - (btn1.Width + btn2.Width + btn3.Width + 6 + 6)) / 2) - 8;
            int start_y = ((this.Height - (btn1.Height + btn2.Height + btn3.Height + 6 + 6)) / 2) - 16;


            for (int i = 1; i <= 9; i++)
            {
                this.Controls["btn" + i.ToString()].Location = new Point(start_x, start_y);

                if (i % 3 == 0)
                {
                    //change y
                    start_y = start_y + this.Controls["btn" + i.ToString()].Height + 6;
                    start_x = ((this.Width - (btn1.Width + btn2.Width + btn3.Width + 6 + 6)) / 2) - 8;
                }
                else
                {
                    start_x = start_x + this.Controls["btn" + i.ToString()].Width + 6;

                }

            }

            //label position
            linkLabel1.Location = new Point(btn8.Location.X, btn8.Location.Y + btn8.Height + 15);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //create the board object
            b = new Board();
           
            //display the board
            display_board();

            //add handlers to buttons
            btn1.Click += new EventHandler(MyButtonClick);
            btn2.Click += new EventHandler(MyButtonClick);
            btn3.Click += new EventHandler(MyButtonClick);
            btn4.Click += new EventHandler(MyButtonClick);
            btn5.Click += new EventHandler(MyButtonClick);
            btn6.Click += new EventHandler(MyButtonClick);
            btn7.Click += new EventHandler(MyButtonClick);
            btn8.Click += new EventHandler(MyButtonClick);
            btn9.Click += new EventHandler(MyButtonClick);
        }

        private void display_board() // from class board
        {

            char[,] board_to_show = b.getBoard();
            int j = 1;

            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    this.Controls["btn" + j.ToString()].Text = board_to_show[x, y].ToString();
                    j++;
                }
            }

        }


        void MyButtonClick(object sender, EventArgs e)
        {

            Button button = sender as Button;

            int x = 0; // coordinates
            int y = 0;

            int i = 0;
            int j = 0;


            //which field to update based on clicked button
            int btn_number = Convert.ToInt32(button.Name.Substring(3, 1));
            int counter = 0;
            bool found_flag = false;

            for (i = 0; i < 3; i++)
            {

                if (found_flag == true)
                    break;

                for (j = 0; j < 3; j++)
                {
                    if (counter < btn_number - 1)
                    {
                        counter++;
                    }
                    else
                    {
                        x = i;
                        y = j;
                        found_flag = true;
                        break;
                    }

                }
            }

            //check if not empty
            if (button.Text != "X" && button.Text != "O")
            {
                //update the game board
                b.update_board(x, y, player);
                display_board();
            }


            //computer move
     
            display_board();

            //output info about winner
            if (b.winner() == 'X' || b.winner() == 'O')
            {
                MessageBox.Show("The winner is: " + Convert.ToString(b.winner()), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                show_winning_fileds(); //apply color
                disable_enable_Buttons(false);


            }
            else if (b.isfull_gameboard())
            {
                MessageBox.Show("Draw", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                disable_enable_Buttons(false);
            }

            //change the player
            if (player == 'X')
                player = 'O';
            else
                player = 'X';



        }


        void show_winning_fileds()
        {

            List<Space> winning_fields = new List<Space>();
            winning_fields = b.getWinningFields();

            char[,] board_to_show = b.getBoard();
            int j = 1;

            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    Space c = new Space(x, y);
                    if (winning_fields.Contains(c))
                    {
                        //this.Controls["btn" + j.ToString()].ForeColor = Color.Red;
                        apply_color((Button)this.Controls["btn" + j.ToString()], Color.Red);
                    }
                    j++;
                }
            }



        }

        void apply_color(Button btn_selected, Color c)
        {

            foreach (Control ctr in this.Controls)
                if (ctr.Name == btn_selected.Name && ctr is Button)
                    ctr.BackColor = c;
        }

        void disable_enable_Buttons(bool value)
        {

            btn1.Enabled = value; btn2.Enabled = value; btn3.Enabled = value;
            btn4.Enabled = value; btn5.Enabled = value; btn6.Enabled = value;
            btn7.Enabled = value; btn8.Enabled = value; btn9.Enabled = value;

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //new game

            //change color to normal
            foreach (Control ctr in this.Controls)
                if (ctr is Button)
                    ctr.BackColor = Color.White;

            player = 'X'; //reset the player
            disable_enable_Buttons(true);
            b.clear_gameboard();
            display_board();
        }
       


        //---------------------------------------

    }
}
