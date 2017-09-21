using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Tic_Tac_Toe_AI
{

// Describes a space on the board.
 public struct Space
  {
    public int X;
    public int Y;

  public Space(int x, int y)
    {
      this.X = x;
      this.Y = y;
    }
  }

    class Board
    {
        //fields
        char[,] gameboard;

        List<Space> winner_coordinates = new List<Space>(); // contains the winner fields

        //default constructor
        public Board()
        {
            //gameboard
            gameboard = new char[3, 3] { { ' ', ' ', ' ' }, { ' ', ' ', ' ' }, { ' ', ' ', ' ' } };

        }

        //custom constructor
        public Board(char[,] board)
        {
            this.gameboard = board;
        }


        //methods

      //clear all spaces in game board
       public void clear_gameboard()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++) 
                    gameboard[i,j] = ' ';
        }

        //check if full gameboard
       public bool isfull_gameboard()
       {
           for (int i = 0; i < 3; i++)
               for (int j = 0; j < 3; j++)
                   if (gameboard[i, j] == ' ')
                       return false;

           return true; // is full

       }

        //get open fields
       public List<Space> getOpenFileds()
       {
           List<Space> open_fileds = new List<Space>();

           for (int i = 0; i < 3; i++)
               for (int j = 0; j < 3; j++)
                   if (gameboard[i, j] == ' ')
                   {
                       open_fileds.Add(new Space(i,j));
                   }

           //return the array with the spaces
           return open_fileds;

       }

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
                   if (gameboard[x, y] == 'X')
                   {
                       count_X++;
                       winner_coordinates.Add(new Space(x, y));
                   }
                   else if (gameboard[x, y] == 'O')
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
                   if (gameboard[y, x] == 'X')
                   {
                       count_X++;
                       winner_coordinates.Add(new Space(y, x));
                   }
                   else if (gameboard[y, x] == 'O')
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
           if (gameboard[0, 0] == 'X' && gameboard[1, 1] == 'X' && gameboard[2, 2] == 'X')
           {
               winner_coordinates.Add(new Space(0, 0));
               winner_coordinates.Add(new Space(1, 1));
               winner_coordinates.Add(new Space(2, 2));
               return 'X';
            
           }
           else if (gameboard[0, 0] == 'O' && gameboard[1, 1] == 'O' && gameboard[2, 2] == 'O')
           {
               winner_coordinates.Add(new Space(0, 0));
               winner_coordinates.Add(new Space(1, 1));
               winner_coordinates.Add(new Space(2, 2));
               return 'O';
           }
         

           //diagonal top right to bottom left
           winner_coordinates.Clear();
           if (gameboard[0, 2] == 'X' && gameboard[1, 1] == 'X' && gameboard[2, 0] == 'X')
           {
               winner_coordinates.Add(new Space(0, 2));
               winner_coordinates.Add(new Space(1, 1));
               winner_coordinates.Add(new Space(2, 0));
               return 'X';
           }
           else if (gameboard[0, 2] == 'O' && gameboard[1, 1] == 'O' && gameboard[2, 0] == 'O')
           {
               winner_coordinates.Add(new Space(0, 2));
               winner_coordinates.Add(new Space(1, 1));
               winner_coordinates.Add(new Space(2, 0));
               return 'O';
           }
         
           winner_coordinates.Clear();
           return ' '; // no winner
       }

        //return board
        public char[,] getBoard(){
           return gameboard;
        }

        //update board
        public void update_board(int x, int y, char c)
        {
            gameboard[x, y] = c;
        }

        //get winningfields
        public List<Space> getWinningFields()
        {
            return winner_coordinates;
        }


        //----------------------------------------------------
    }
}
