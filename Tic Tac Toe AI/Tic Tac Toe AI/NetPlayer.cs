using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Windows.Forms;




namespace Tic_Tac_Toe_AI
{
    class NetPlayer
    {

           private Socket connection; // connection to server
           private Tic_Tac_Toe_Server server; // reference to Tic-Tac-Toe server

           //object for sending data to server 
           private NetworkStream socketStream;

           //objects for writing and reading bytes to streams
           private BinaryWriter writer;
           private BinaryReader reader;

           private char mark; // "X" or "O"
           public Boolean threadSuspended  = true;


        public NetPlayer(Socket socketValue, Tic_Tac_Toe_Server serverValue , char markValue){
              
            //assign argument values to class-member values
            connection = socketValue;
            server = serverValue;
            mark = markValue;

            //use Socket to create NetworkStream object
            socketStream = new NetworkStream(connection);

            //create objects for writing and reading bytes across streams
            writer = new BinaryWriter(socketStream);
            reader = new BinaryReader(socketStream);

         }

        public NetPlayer()
        {


        }

          //inform other player that move was made
    public void OtherPlayerMoved(int location){
        //notify opponent
        writer.Write("Opponent moved");
        writer.Write(location);
    } // OtherPlayerMoved

       //inform server of move and receive move from other player

  public void Run(){

        Boolean done = false; // indicates whether game is over 

        //indicate successful connection and send mark to server
        if(mark == 'X'){
            server.Display("Player X connected");
            writer.Write(mark);
            writer.Write("Player X connected" + Environment.NewLine);
        }
        else{
            server.Display("Player O connected");
            writer.Write(mark);
            writer.Write("Player O connected, please wait"  + Environment.NewLine);
        }

  
        //wait for other players to connect
        if(mark == 'X'){
            writer.Write("Waiting for another player");

            //wait for notification that other player has connected
            lock (this){

                while(threadSuspended)
                    Monitor.Wait(this);
               

            }

            writer.Write("Other player connected. Your move");
        }

        //play game

        while(!done){
            //wait for data to become available
           while (connection.Available == 0){
                Thread.Sleep(1000);

                //end loop if server disconnects
                if (server.Disconnected())
                    return;
           

            }


            //receive other player's move
            int location = reader.ReadInt32();

            //determine whether move is valid
            if(server.ValidMove(location, mark)) {
                //display move on server
                server.Display("loc:" + location);

                //notify server of valid move
                writer.Write("Valid move.");
            }
            else{ //notify server of invalid move
                writer.Write("Invalid move, try again");

            }

            //exit loop if game over
            if(server.GameOver()) 
                done = true;

           }

        //close all connections
        writer.Close();
        reader.Close();
        socketStream.Close();
        connection.Close();

  } // Run


  public void DisconnectServer()
  {
      //close all connections
      try
      {
          writer.Close();
          reader.Close();
          socketStream.Close();
          connection.Disconnect(false);
          connection.Close();
       
        
    
      }
      catch (Exception z)
      {
          //MessageBox.Show("NetPlayer DisconnectServer error " + z.Message);
      }
    
     
  }

        //-----------------------------------------

    }


}
