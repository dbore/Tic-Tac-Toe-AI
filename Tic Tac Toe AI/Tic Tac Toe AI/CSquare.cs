using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tic_Tac_Toe_AI
{
    public class CSquare
    {

        // panel on which user clicks 
        private Button squarePanel;
        // "X" or "O"
        private char squareMark;
        // position on board
        private int squareLocation;

        //constructor assigns argument values to class-member values

        public CSquare(Button panelValue, char markValue, int locationValue)
        {
            squarePanel = panelValue;
            squareMark = markValue;
            squareLocation = locationValue;

        }
        // New

        //return panel on which user click
        public Button Panel
        {
            get { return squarePanel; }
        }
        // Panel

        //set and get squareMark ("X" or "O")
        public char Mark
        {
            get { return squareMark; }
            set { squareMark = value; }
        }
        // Mark

        //return squarePanel position on Tic-Tac-Toe board
        public int Location
        {
            get { return squareLocation; }
        }
        // Location

    }
    // CSquare

}
