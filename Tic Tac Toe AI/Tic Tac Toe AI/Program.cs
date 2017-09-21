using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tic_Tac_Toe_AI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
          
            Application.Run(new Menu());
          // var main_form = new Menu();
          //  main_form.Show();
           // Application.Run();
        }
    }
}
