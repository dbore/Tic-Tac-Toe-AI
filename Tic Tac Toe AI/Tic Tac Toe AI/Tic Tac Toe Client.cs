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

    using Microsoft.VisualBasic;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Windows.Forms;
    using System.Net.Sockets;
    using System.Threading;
    using System.IO;


    public partial class Tic_Tac_Toe_Client : Form
    {

        //board contains nine panels where user can place "X" or "O"
        //Friend WithEvents Panel1 As Panel
        //Friend WithEvents Panel2 As Panel
        //Friend WithEvents Panel3 As Panel
        //Friend WithEvents Panel4 As Panel
        //Friend WithEvents Panel5 As Panel
        //Friend WithEvents Panel6 As Panel
        //Friend WithEvents Panel7 As Panel
        //Friend WithEvents Panel8 As Panel
        //Friend WithEvents Panel9 As Panel

        //TextBox displays game status and other player's moves
        //Friend WithEvents txtDisplay As TextBox
        //Friend WithEvents lblId As Label ' Label displays player

        //Tic-Tac-Toe board
        private CSquare[,] board;

        //square that user previously clicked

        private CSquare mCurrentSquare;
        // connection to the server
        private TcpClient connection;
        // stream to transfer data
        private NetworkStream stream;

        //objects for writing and reading bytes to streams 
        private BinaryWriter writer;

        private BinaryReader reader;
        // "X" or "O"
        private char mark;
        // indicate which player should move
        private bool turn;

        // brush for painting board
        private SolidBrush brush;

        // indicates whether game is over
        private bool done = false;

        Thread outputThread;

        Form server;
        Form menu;
        List<Space> winner_coordinates = new List<Space>(); // contains the winner fields
        bool flag = false;




        public Tic_Tac_Toe_Client(Form server, Boolean error, Form menu)
        {
             // This call is required by the designer.
            InitializeComponent();
            this.server = server;
            this.menu = menu;

     




            if (error == true)
                server.Close();




            // Add any initialization after the InitializeComponent() call.

            board = new CSquare[3, 3];
            // create 3 X 3 board

            //create nine CSquare's and place their Panels on board
            board[0, 0] = new CSquare(btn1, ' ', 0);
            board[0, 1] = new CSquare(btn2, ' ', 1);
            board[0, 2] = new CSquare(btn3, ' ', 2);
            board[1, 0] = new CSquare(btn4, ' ', 3);
            board[1, 1] = new CSquare(btn5, ' ', 4);
            board[1, 2] = new CSquare(btn6, ' ', 5);
            board[2, 0] = new CSquare(btn7, ' ', 6);
            board[2, 1] = new CSquare(btn8, ' ', 7);
            board[2, 2] = new CSquare(btn9, ' ', 8);

            //create SolidBrush for writing on Squares
            brush = new SolidBrush(Color.Black);

            //make connection request to server on port 5000

            
                connection = new TcpClient("localhost", 5000);
                stream = connection.GetStream();

                //create objects for writing and reading bytes to streams
                writer = new BinaryWriter(stream);
                reader = new BinaryReader(stream);
         

      

            //create thread for sending and receiving messages
            outputThread = new Thread(Run);
            outputThread.Start();



        }

 

        //Visual Studio .NET generated code

      
       

        //redraw Tic-Tac-Toe
        public void PaintSquares()
        {
            //Graphics graphics = default(Graphics);

            //counters for traversing Tic-Tac-Toe board
            int row = 0;
            int column = 0;

         


            //draw appopriate mark on each panel
            for (row = 0; row <= 2; row++)
            {
                for (column = 0; column <= 2; column++)
                {
                    //get graphics for each panel 
                   // graphics = board[row, column].Panel.CreateGraphics();

                    //draw appopriate letter on panel
                    Font f = new Font("Arial", 18, FontStyle.Bold);
                    //graphics.DrawString(board[row, column].Mark.ToString(), f, brush, 8, 8);
                    board[row, column].Panel.Font = f;
                    board[row, column].Panel.Text = board[row, column].Mark.ToString();



                    //check the score or board availability
                    char winning_player = winner();
                    if (winning_player == 'X' || winning_player == 'O')
                    {
                        //someone won
                        
                        //show the winning fields
                        foreach (Space s in winner_coordinates)
                        {
                            board[s.X, s.Y].Panel.BackColor = Color.Red;
                        }

                        //block the input
                        blockInput();
                        
                        //no ones turn 
                        lblTurn.Text = "";

                        //show winner message
                        if (flag == false)
                        {
                            MessageBox.Show("The winner is: " + Convert.ToString(winning_player), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            flag = true;
                        }

                    }
                    else if (isfull() == true)
                    {
                     
                        //block the input
                        blockInput();


                        //no ones turn 
                        lblTurn.Text = "";

                        //nobody won draw
                        if (flag == false)
                        {
                            MessageBox.Show("Draw", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            flag = true;
                        }
                    }
                    else
                    {

                        //change turn
                        lblTurn.Text = "Opponent move.";
                    }
                }
            }
        }

        void blockInput()
        {
            int row, column = 0;

            for (row = 0; row <= 2; row++)
            {
                for (column = 0; column <= 2; column++)
                {
                    board[row, column].Panel.Enabled = false;
                }
            }

        }

        //check if full gameboard
        public bool isfull()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (!(board[i, j].Panel.Text == "X" || board[i, j].Panel.Text == "O"))
                        return false;
                        

            return true; // is full

        }
        // PaintSquares

        //invoked when user clicks Panels

        //private void square_MouseUp(System.Object sender, System.Windows.Forms.MouseEventArgs e)
        //{

        //}
        // square_mouseUp

        void square_MouseUP(object sender, EventArgs e)
        {
             //counters for traversing Tic-Tac-Toe board
        int row, column;

        for(row = 0; row <= 2; row++)
            for (column = 0; column <= 2; column++){

                //determine which Panel was clicked
                if (board[row, column].Panel == sender){
                    mCurrentSquare = board[row, column];

                    //send move to server 
                    sendClickedSqaure(board[row, column].Location);
            }
            }
        }


        //send square position to server
        public void sendClickedSqaure(int location){

        //send location to the server if current turn
        if(turn){
            writer.Write(location);
            turn = false; // change turns
}
} // SendClickSqaure




        //'//The delegate is only needed for the VB 9 or less version
        private delegate void UpdateTextBoxDelegate(TextBox txtBox, string value);

        private void UpdateTextBox(TextBox txtBox, string value)
        {

            //'//Basically ask the textbox if we need to invoke
            if (txtBox.InvokeRequired)
            {
                //'//For VB 9 or less you need a delegate
                txtBox.Invoke(new UpdateTextBoxDelegate(UpdateTextBox), txtBox, value);
            }
            else
            {
                txtBox.Text = value;
            }
        }

        private delegate void UpdateLabelDelegate(Label lblBox, string value);

        private void UpdateLabelBox(Label lblBox, string value)
        {

            //'//Basically ask the textbox if we need to invoke
            if (lblBox.InvokeRequired)
            {
                //'//For VB 9 or less you need a delegate
                lblBox.Invoke(new UpdateLabelDelegate(UpdateLabelBox), lblBox, value);
            }
            else
            {
                lblBox.Text = value;
            }
        }

        //continously update TextBox display

        public void Run()
        {
         
            char quote = (char)34; //Strings.ChrW(34);
            // single quote

            //get player's mark ("X" or "O")
            mark = Convert.ToChar(stream.ReadByte());
            //lblId.Text = "You are player " & quote & mark & quote
            UpdateLabelBox(lblId, "You are player " + quote + mark + quote);



            //determine which player should move
            if (mark == 'X')
            {
                turn = true;
            }
            else
            {
                turn = false;
            }

            //processing incomming messages


            try
            {
                //receive messages sent to client

                while (true) 
                {
                    ProcessMessage(reader.ReadString());
                }



                //notify user if server closes connection
            }
            catch (EndOfStreamException exception)
            {
                //txtDisplay.Text = "Server closed connection. Game over."
                //UpdateTextBox(txtDisplay, "Server closed connection. Game over.");


            }
            catch (Exception ex)
            {
               // MessageBox.Show("client run error " + ex.Message);
            }

        }
        // Run


        //process message send to client
        public void ProcessMessage(string messageValue)
        {

           
            //if valid move, set mark to clicked square
            if (messageValue == "Valid move.")
            {
                //txtDisplay.Text += "Valid move, please wait." + Environment.NewLine;
                mCurrentSquare.Mark = mark;
                PaintSquares();
                //if invalid move, inform user to try again
            }
            else if (messageValue == "Invalid move, try again")
            {
                //txtDisplay.Text += messageValue + Environment.NewLine;
                turn = true;
                //if oponent moved, mark opposite mark on square

            }
            else if (messageValue == "Opponent moved")
            {
                //find location of opponent 's move
                int location = reader.ReadInt32();

                //mark that square with opponent's mark
                if (mark == 'X')
                {
                    board[location / 3, location % 3].Mark = 'O';
                }
                else
                {
                    board[location / 3, location % 3].Mark = 'X';
                }

                PaintSquares();
                //txtDisplay.Text += "Opponent moved. Your turn." + Environment.NewLine;
                lblTurn.Text = "Your turn.";

                turn = true;
                //change turns

                //display message as default case
            }
            else
            {
           
                //txtDisplay.Text &= messageValue & vbCrLf
                //UpdateTextBox(txtDisplay, txtDisplay.Text + messageValue + Environment.NewLine);
                if (messageValue.Contains("Your move"))
                {
                    lblTurn.Text = "Your turn.";
                }

            }
        }
        // ProcessMessage


        //Property CurrentSquare
        public CSquare CurrentSquare
        {
            set { mCurrentSquare = value; }
        }
        // CurrentSquare


        private void Tic_Tac_Toe_Client_Load(object sender, EventArgs e)
        {

            //add event handlers for click
            btn1.Click += new EventHandler(square_MouseUP);
            btn2.Click += new EventHandler(square_MouseUP);
            btn3.Click += new EventHandler(square_MouseUP);
            btn4.Click += new EventHandler(square_MouseUP);
            btn5.Click += new EventHandler(square_MouseUP);
            btn6.Click += new EventHandler(square_MouseUP);
            btn7.Click += new EventHandler(square_MouseUP);
            btn8.Click += new EventHandler(square_MouseUP);
            btn9.Click += new EventHandler(square_MouseUP);

        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Tic_Tac_Toe_Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            done = true;
            outputThread.Abort();
            stream.Close();
            connection.Close();
            writer.Close();
            reader.Close();
            menu.Show();
        }

        private void Tic_Tac_Toe_Client_Paint(object sender, PaintEventArgs e)
        {
          //  PaintSquares();
        }

        //---------------------------------------------------------------

        //get the winner as player
        public char winner()
        {

            int count_X = 0;
            int count_O = 0;
            winner_coordinates.Clear();

            //check by rows
            for (int x = 0; x < 3; x++)
            {
                count_X = 0;
                count_O = 0;
                winner_coordinates.Clear();

                for (int y = 0; y < 3; y++)
                {
                    if (board[x, y].Panel.Text == "X")
                    {
                        count_X++;
                        winner_coordinates.Add(new Space(x, y));
                    }
                    else if (board[x, y].Panel.Text == "O")
                    {
                        count_O++;
                        winner_coordinates.Add(new Space(x, y));
                    }

                }

                //found the winner for row
                if (count_X == 3)
                    return 'X';
                else if (count_O == 3)
                    return 'O';

            }

            //check by column
            for (int x = 0; x < 3; x++)
            {
                count_X = 0;
                count_O = 0;
                winner_coordinates.Clear();

                for (int y = 0; y < 3; y++)
                {
                    if (board[y, x].Panel.Text == "X")
                    {
                        count_X++;
                        winner_coordinates.Add(new Space(y, x));
                    }
                    else if (board[y, x].Panel.Text == "O")
                    {
                        count_O++;
                        winner_coordinates.Add(new Space(y, x));
                    }

                }

                //found the winner for column
                if (count_X == 3)
                    return 'X';
                else if (count_O == 3)
                    return 'O';


            }

            //diagonal top left to bottom right
            winner_coordinates.Clear();
            if (board[0, 0].Panel.Text == "X" && board[1, 1].Panel.Text == "X" && board[2, 2].Panel.Text == "X")
            {
                winner_coordinates.Add(new Space(0, 0));
                winner_coordinates.Add(new Space(1, 1));
                winner_coordinates.Add(new Space(2, 2));
                return 'X';

            }
            else if (board[0, 0].Panel.Text == "O" && board[1, 1].Panel.Text == "O" && board[2, 2].Panel.Text == "O")
            {
                winner_coordinates.Add(new Space(0, 0));
                winner_coordinates.Add(new Space(1, 1));
                winner_coordinates.Add(new Space(2, 2));
                return 'O';
            }


            //diagonal top right to bottom left
            winner_coordinates.Clear();
            if (board[0, 2].Panel.Text == "X" && board[1, 1].Panel.Text == "X" && board[2, 0].Panel.Text == "X")
            {
                winner_coordinates.Add(new Space(0, 2));
                winner_coordinates.Add(new Space(1, 1));
                winner_coordinates.Add(new Space(2, 0));
                return 'X';
            }
            else if (board[0, 2].Panel.Text == "O" && board[1, 1].Panel.Text == "O" && board[2, 0].Panel.Text == "O")
            {
                winner_coordinates.Add(new Space(0, 2));
                winner_coordinates.Add(new Space(1, 1));
                winner_coordinates.Add(new Space(2, 0));
                return 'O';
            }

            winner_coordinates.Clear();
            return ' '; // no winner
        }
        //-----------------------------------------------------------------

    }
    //FrmClient


}
