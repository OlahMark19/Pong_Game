using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pong_Game
{
    public partial class CustomInputDialog : Form
    {
        public string player1Inp { get; private set; }
        public string player2Inp { get; private set; }
        public CustomInputDialog()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            textPlayer1.Text = "player1";
            textPlayer2.Text = "player2";
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            player1Inp = textPlayer1.Text;
            player2Inp = textPlayer2.Text;


            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Application.Exit();
        }
    }
}
