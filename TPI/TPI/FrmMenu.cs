/**
 * Document: Menu.cs
 * Description: Fenêtre de menu
 * Auteur: Ibanez Thomas
 * Date: 28.05.15
 * Version: 0.1
 */
using System;
using System.Windows.Forms;

namespace TPI
{
    public partial class FrmMenu : Form
    {
        FrmGame frmGame;
        public FrmMenu()
        {
            InitializeComponent();
            
        }
        
        private void btnJoin_Click(object sender, EventArgs e)
        {
            frmGame = new FrmGame(true, tbxIP.Text, tbxNickname.Text);
            frmGame.Show();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            frmGame = new FrmGame(false, tbxIP.Text, tbxNickname.Text);
            frmGame.Show();
        }
    }
}
