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
    /// <summary>
    /// Fenêtre vue à l’ouverture de l’application, 
    /// elle permet de choisir entre rejoindre une partie 
    /// ou en créer une ainsi que choisir son pseudo. 
    /// Elle ne contient cependant pas de code lié avec le réseau
    /// </summary>
    public partial class FrmMenu : Form
    {
        ///<summary> La fenêtre de jeu à lancer</summary>
        FrmGame frmGame;

        /// <summary>
        /// Initalise la fenêtre
        /// </summary>
        public FrmMenu()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Crée une fenêtre de jeu et passe les paramètres de connexion si les champs sont valides.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnJoin_Click(object sender, EventArgs e)
        {
            if (verifyFields())
            {
                frmGame = new FrmGame(true, tbxIP.Text, tbxNickname.Text);
                frmGame.Show();
            }
        }

        /// <summary>
        /// Crée une fenêtre de jeu et passe les paramètres de création si les champs sont valides.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (verifyFields())
            {
                frmGame = new FrmGame(false, tbxIP.Text, tbxNickname.Text);
                frmGame.Show();
            }
        }

        /// <summary>
        /// Vérifie si les champs ip et pseudo ne sont pas vide et si l’ip est à un bon format grâce à une expression régulière
        /// </summary>
        /// <returns>True si les champs sont valides, false sinon</returns>
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
            return ok;
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
