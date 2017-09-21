namespace Tic_Tac_Toe_AI
{
    partial class Menu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblComp = new System.Windows.Forms.Label();
            this.lblPlayer = new System.Windows.Forms.Label();
            this.lblPlayerNetwork = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblComp
            // 
            this.lblComp.AutoSize = true;
            this.lblComp.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComp.ForeColor = System.Drawing.Color.White;
            this.lblComp.Location = new System.Drawing.Point(22, 31);
            this.lblComp.Name = "lblComp";
            this.lblComp.Size = new System.Drawing.Size(138, 25);
            this.lblComp.TabIndex = 0;
            this.lblComp.Text = "Player vs AI";
            this.lblComp.MouseHover += new System.EventHandler(this.lblComp_MouseHover);
            // 
            // lblPlayer
            // 
            this.lblPlayer.AutoSize = true;
            this.lblPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayer.ForeColor = System.Drawing.Color.White;
            this.lblPlayer.Location = new System.Drawing.Point(22, 108);
            this.lblPlayer.Name = "lblPlayer";
            this.lblPlayer.Size = new System.Drawing.Size(184, 25);
            this.lblPlayer.TabIndex = 1;
            this.lblPlayer.Text = "Player vs Player";
            this.lblPlayer.Click += new System.EventHandler(this.lblPlayer_Click);
            // 
            // lblPlayerNetwork
            // 
            this.lblPlayerNetwork.AutoSize = true;
            this.lblPlayerNetwork.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayerNetwork.ForeColor = System.Drawing.Color.White;
            this.lblPlayerNetwork.Location = new System.Drawing.Point(22, 193);
            this.lblPlayerNetwork.Name = "lblPlayerNetwork";
            this.lblPlayerNetwork.Size = new System.Drawing.Size(289, 25);
            this.lblPlayerNetwork.TabIndex = 2;
            this.lblPlayerNetwork.Text = "Player vs Player (network)";
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(324, 300);
            this.Controls.Add(this.lblPlayerNetwork);
            this.Controls.Add(this.lblPlayer);
            this.Controls.Add(this.lblComp);
            this.Name = "Menu";
            this.Text = "Menu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Menu_FormClosing);
            this.Load += new System.EventHandler(this.Menu_Load);
            this.Resize += new System.EventHandler(this.Menu_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblComp;
        private System.Windows.Forms.Label lblPlayer;
        private System.Windows.Forms.Label lblPlayerNetwork;
    }
}