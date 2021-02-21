using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;

namespace Elipgo.SuperZapatos.AppFormSuperZapatos.Views
{
    /// <summary>
    /// Formulario principal
    /// </summary>
    public partial class frmPrincipal : Form
    {
        private readonly ILogger logger;
        public frmPrincipal(ILogger<frmPrincipal> log)
        {
            logger = log;
            InitializeComponent();
        }

        #region Eventos
        private void btnSlide_Click(object sender, EventArgs e)
        {
            if (MenuVertical.Width == 250)
                MenuVertical.Width = 35;
            else
                MenuVertical.Width = 250;
        }
        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void btnStores_Click(object sender, EventArgs e)
        {
            AbrirForm(new frmStores(logger));
        }
        private void btnArticles_Click(object sender, EventArgs e)
        {
            AbrirForm(new frmArticles(logger));
        }

        #endregion
        #region Métodos
        /// <summary>
        /// Libera la captura
        /// </summary>
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        /// <summary>
        /// Envia mensaje a la ventana
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="wmsg"></param>
        /// <param name="wparam"></param>
        /// <param name="lparam"></param>
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        /// <summary>
        /// Abre un formulario dentro del contenedor
        /// </summary>
        /// <param name="formChild">formulario hijo</param>
        private void AbrirForm(object formChild)
        {
            if (this.panelContenedor.Controls.Count > 0)
            {
                this.panelContenedor.Controls.RemoveAt(0);
            }
            Form fh = formChild as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panelContenedor.Controls.Add(fh);
            this.panelContenedor.Tag = fh;
            this.lblTitulo.Text = fh.Text;
            fh.Show();
        }
        #endregion

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
