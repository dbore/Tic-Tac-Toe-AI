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
    public partial class Menu : Form
    {

        Tic_Tac_Toe_Client client;
        Tic_Tac_Toe_Server server;
        Form1 form1;
        Form2 form2;



        public Menu()
        {
            InitializeComponent();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            //add event handlers for mouse hover
            lblComp.MouseHover += new EventHandler(labelHover);
            lblPlayer.MouseHover += new EventHandler(labelHover);
            lblPlayerNetwork.MouseHover += new EventHandler(labelHover);

            //add event handlers for mouse leave
            lblComp.MouseLeave += new EventHandler(labelLeave);
            lblPlayer.MouseLeave += new EventHandler(labelLeave);
            lblPlayerNetwork.MouseLeave += new EventHandler(labelLeave);

            //add event handlers for click
            lblComp.Click += new EventHandler(labelClick);
            lblPlayer.Click += new EventHandler(labelClick);
            lblPlayerNetwork.Click += new EventHandler(labelClick);


        }

        private void lblComp_MouseHover(object sender, EventArgs e)
        {

        }

        void labelHover(object sender, EventArgs e)
        {
            Label lab = sender as Label;
            lab.ForeColor = Color.Red;
            lab.Cursor = System.Windows.Forms.Cursors.Hand;

        }
        void labelLeave(object sender, EventArgs e)
        {
            Label lab = sender as Label;
            lab.ForeColor = Color.White;

        }

        void labelClick(object sender, EventArgs e)
        {
            Label lab = sender as Label;
            //decide what to open
            //Form start;
            if (lab.Name == "lblComp")
            {
                this.Hide();
                form1 = new Form1(this);
                // Show the  form
                form1.Show();
            }else if(lab.Name == "lblPlayer")
            {
           
                this.Hide();
                form2 = new Form2(this);
                // Show the  form
                form2.Show();   

            }else if(lab.Name == "lblPlayerNetwork"){

                this.Hide();
                 server = new Tic_Tac_Toe_Server(this);
                 //Show the  form
                server.Show();

            }

            

        }

        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
          
            //Application.Exit();
            Environment.Exit(0);

        }

        private void Menu_Resize(object sender, EventArgs e)
        {
            //size
            foreach (Control c in this.Controls)
            {

                //font in all controls
                c.Font = new Font(c.Font.Name, this.Height / 20, FontStyle.Bold);
               
            }
        }

        private void lblPlayer_Click(object sender, EventArgs e)
        {

        }

      

        

      
    }
}
