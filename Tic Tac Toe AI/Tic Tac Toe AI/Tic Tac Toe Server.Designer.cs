namespace Tic_Tac_Toe_AI
{
    partial class Tic_Tac_Toe_Server
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
            this.txtDisplay = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtDisplay
            // 
            this.txtDisplay.Location = new System.Drawing.Point(12, 12);
            this.txtDisplay.Multiline = true;
            this.txtDisplay.Name = "txtDisplay";
            this.txtDisplay.Size = new System.Drawing.Size(276, 276);
            this.txtDisplay.TabIndex = 0;
            // 
            // Tic_Tac_Toe_Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(300, 302);
            this.Controls.Add(this.txtDisplay);
            this.Name = "Tic_Tac_Toe_Server";
            this.Text = "Tic_Tac_Toe_Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Tic_Tac_Toe_Server_FormClosing);
            this.Load += new System.EventHandler(this.Tic_Tac_Toe_Server_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDisplay;
    }
}