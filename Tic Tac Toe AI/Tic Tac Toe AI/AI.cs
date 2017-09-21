using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tic_Tac_Toe_AI
{
    class AI
    {
        //default constructor
        public AI(){}

        //methods

        //minimax algorithm using recursion
        //the parameters is game board and player name
         public int minimax(Board gameboard, char gracz){
                 
         int m, mmx;


            //check the winner
            if (gameboard.winner() == 'X')
            {
                return -1; // X is the winner
            }
            else if (gameboard.winner() == 'O')
            {
                return 1; // O (opponent is the winner)
            }

            //check draw
            if (gameboard.isfull_gameboard())      
                return 0; // draw, no body won

            //---------------------------------------
           
            // Będziemy analizować możliwe posunięcia przeciwnika. Zmieniamy zatem
            // bieżącego gracza na jego przeciwnika
            gracz = (gracz == 'O') ? 'X' : 'O';
            //Console.WriteLine(gracz);


            // Algorytm MINIMAX w kolejnych wywołaniach rekurencyjnych naprzemiennie analizuje
            // grę gracza oraz jego przeciwnika. Dla gracza oblicza maksimum wyniku gry, a dla
            // przeciwnika oblicza minimum. Wartość mmx ustawiamy w zależności od tego, czyje
            // ruchy analizujemy:
            // X - liczymy max, zatem mmx <- -10
            // O - liczymy min, zatem mmx <-  10

            mmx = (gracz == 'X') ? 10 : -10;

            // Przeglądamy planszę szukając wolnych pół na ruch gracza. Na wolnym polu ustawiamy
            // literkę gracza i wyznaczamy wartość tego ruchu rekurencyjnym wywołaniem
            // algorytmu MINIMAX. Planszę przywracamy i w zależności kto gra:
            // X - wyznaczamy maximum
            // O - wyznaczamy minimum

            foreach (Space s in gameboard.getOpenFileds())
            {

                gameboard.update_board(s.X, s.Y, gracz);
                m = minimax(gameboard, gracz);
                gameboard.update_board(s.X, s.Y, ' ');

                if (((gracz == 'X') && (m < mmx)) || ((gracz == 'O') && (m > mmx))) mmx = m;
            }
         

             return mmx;
         
      
    }
//-------------------------------------------

    }
}
