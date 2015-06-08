/**
 * Document: Menu.cs
 * Description: Fenêtre de menu
 * Auteur: Ibanez Thomas
 * Date: 28.05.15
 * Version: 0.1
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
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
            cmbIP.Items.AddRange(getLocalIPs());
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
                frmGame = new FrmGame(true, cmbIP.Text, tbxNickname.Text);
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
                frmGame = new FrmGame(false, cmbIP.Text, tbxNickname.Text);
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
            if (cmbIP.Text.Trim().Equals(string.Empty))
            {
                cmbIP.BackColor = Color.Red;
                ok = false;
            }
            if (tbxNickname.Text.Trim().Equals(string.Empty))
            {
                tbxNickname.BackColor = Color.Red;
                ok = false;
            }
            if (ok == true)
            {
                if (!Regex.IsMatch(cmbIP.Text, @"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$", RegexOptions.None))
                {
                    cmbIP.BackColor = Color.Red;
                    ok = false;
                }
            }
            return ok;
        }

        private void tbxNickname_TextChanged(object sender, EventArgs e)
        {
            tbxNickname.BackColor = Color.White;
        }

        /// <summary>
        /// Permet de retrouver toutes les ip disponible pour cette machine
        /// </summary>
        /// <returns>Tableau d'ip</returns>
        public static string[] getLocalIPs()
        {
            IPHostEntry host;
            List<string> localIPs = new List<string>();
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIPs.Add(ip.ToString());
                }
            }
            return localIPs.ToArray();
        }
    }
}
