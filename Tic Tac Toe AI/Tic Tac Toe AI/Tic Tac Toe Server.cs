using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;





namespace Tic_Tac_Toe_AI
{
    public partial class Tic_Tac_Toe_Server : Form
    {

        private char[] board; //Tic-Tac-Toe game board
        private NetPlayer[] players; // players array 
        private Thread[] playerThreads; //Thread that run clinets
        private int currentPlayer; // indicates current player "X" or "O"
        private Boolean disconnect  = false; // indicates whether servr has diconnected

        TcpListener listener;
        Thread getPlayers;
        Form menu;
        

        public Tic_Tac_Toe_Server(Form menu_form)
        {
            InitializeComponent();
            menu = menu_form;

        //Add any initialization after the InitializeComponent() call.

        board = new char[9]; // create board with nine squares
        players = new NetPlayer[2]; // create two players

        //create one thread for each player 
        playerThreads = new Thread[2];
        currentPlayer = 0;

        

        //use separate thread to accept connections
       getPlayers = new Thread(new ThreadStart(SetUp));


        getPlayers.Start();

        }

        
   

        private void Tic_Tac_Toe_Server_Load(object sender, EventArgs e)
        {
            //start the client
            Boolean error = false;
            Tic_Tac_Toe_Client client = new Tic_Tac_Toe_Client(this, error, menu);
            client.Show();
        }

          //accept connections from two clients applications
        public void SetUp()
        {
     
        //server listens for requests on port 5000
        try
        {

         

            listener = new TcpListener(System.Net.IPAddress.Any, 5000);
            listener.Start();

         
            
            //accept first client (player) and start its thread
         
             players[0] = new NetPlayer(listener.AcceptSocket(), this, 'X');
             playerThreads[0] = new Thread(new ThreadStart(players[0].Run));

             playerThreads[0].Start();
             
           // //accept second client (Player) and start its thread
           players[1] = new NetPlayer(listener.AcceptSocket(), this, 'O');
           playerThreads[1] = new Thread(new ThreadStart(players[1].Run));

            playerThreads[1].Start();

            //inform first player of other player's connection to the server
           lock ((players[0]))
            {
                players[0].threadSuspended = false;
               Monitor.Pulse(players[0]);
          }

            
      


        }
        catch (SocketException ex)
        {

            //MessageBox.Show("server setup error " + ex.Message);
            //start the client
            Boolean error = true;
            Tic_Tac_Toe_Client client = new Tic_Tac_Toe_Client(this, error, menu);
            client.Show();

            closeTheForm(); // exit the server if the server is already running
            //this.Close();
        }
     
        }

        private void closeTheForm()
        {
 
            try
            {
                disconnect = true;  //disconnect the server

                if (players[0] != null)
                  players[0].DisconnectServer();
               if (players[1] != null)
                    players[1].DisconnectServer();

                if (playerThreads[0] != null)
                    playerThreads[0].Suspend();
                if (playerThreads[1] != null)
                    playerThreads[1].Suspend();

                if (listener != null)
                {
                    listener.Stop();
                    listener = null;
                }
                if (players[0] != null)
                players[0] = null;
                if (players[1] != null)
                players[1] = null;

                if(getPlayers.ThreadState == ThreadState.Running)
                    getPlayers.Suspend();

                //   //show menu form
                //menu.Show();
             
            }
            catch (Exception v)
            {
                //MessageBox.Show("server closeTheform error " + v.Message);
            }

        }

        private void Tic_Tac_Toe_Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            closeTheForm();
            
        }

            //The delegate is only needed for the VB 9 or less version
        public delegate void UpdateTextBoxDelegate(TextBox txtBox, String value);

           private void UpdateTextBox(TextBox txtBox, String value){
               //Basically ask the textbox if we need to invoke
               if(txtBox.InvokeRequired){

                  //For VB 9 or less you need a delegate
                    txtBox.Invoke(new UpdateTextBoxDelegate(UpdateTextBox), txtBox, value);
                  }else{
                      txtBox.Text = value;
            }

       }

         //display message argument in txtDisplay
    public void Display(String message){
        UpdateTextBox(txtDisplay, txtDisplay.Text + message + Environment.NewLine);

    } //Display


      //determine whether move is valid
    public Boolean ValidMove(int location, Char player){

        //prevent other threads from making moves
        lock(this){

            int playerNumber = 0;

            //playerNumber = 0 if player "X", else playerNumber = 1
            if(player == 'O') 
                playerNumber = 1;
            

            //wait while not current player's turn
            while(playerNumber != currentPlayer)
                Monitor.Wait(this);
    

            //determine whether desired square is occupied
            if (!isOccupied(location)){

                //place either an "X" or an "O" on board
                if(currentPlayer == 0)
                    board[location] = 'X';
                else
                    board[location] = 'O';
               

                //set currentPlayer as other player(change turns)
                currentPlayer = (currentPlayer + 1) % 2;

                //notify other player move
                players[currentPlayer].OtherPlayerMoved(location);

                //alert other player to move
                Monitor.Pulse(this);

                return true;
            }
            else{
                return false;
            }
    } 
    }// ValidMove


           //determine whether specified square is occupied
    public Boolean isOccupied(int location){

       //return True if board loaction contains "X" or "O"
        if (board[location] == 'X' || board[location] == 'O')
            return true;
        else
            return false;
    

    } // IsOccupied

        //allow clients to see if server has disconnected
    public Boolean Disconnected(){
        return disconnect;

    } // Disconnected

    //determine whether game is Over
    public Boolean GameOver(){
        //place code here to test for winner of game
        return false;
    } // GameOver

 



        //-------------------------------------------------
    }
}
