/**
 * Document: Menu.cs
 * Description: Fenêtre de menu
 * Auteur: Ibanez Thomas
 * Date: 28.05.15
 * Version: 0.1
 */
using System;
using System.Drawing;
using System.Text.RegularExpressions;
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
            if (verifyFields())
            {
                frmGame = new FrmGame(true, tbxIP.Text, tbxNickname.Text);
                frmGame.Show();
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (verifyFields())
            {
                frmGame = new FrmGame(false, tbxIP.Text, tbxNickname.Text);
                frmGame.Show();
            }
        }

        private bool verifyFields()
        {
            bool ok = true;
            if (tbxIP.Text.Trim().Equals(string.Empty))
            {
                tbxIP.BackColor = Color.Red;
                ok = false;
            }
            if (tbxNickname.Text.Trim().Equals(string.Empty))
            {
                tbxNickname.BackColor = Color.Red;
                ok = false;
            }
            if (ok == true)
            {
                if(!Regex.IsMatch(tbxIP.Text,@"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$", RegexOptions.None)) {
                    tbxIP.BackColor = Color.Red;
                    ok = false;
                }
            }
            return !ok;
        }

        private void tbxIP_TextChanged(object sender, EventArgs e)
        {
            tbxIP.BackColor = Color.White;
        }

        private void tbxNickname_TextChanged(object sender, EventArgs e)
        {
            tbxNickname.BackColor = Color.White;
        }
    }
}
