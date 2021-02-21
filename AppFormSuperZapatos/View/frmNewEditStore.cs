using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace Elipgo.SuperZapatos.AppFormSuperZapatos.Views
{
    public partial class frmNewEditStore : Form
    {
        private readonly ILogger logger;
        private string apiStores = ConfigurationManager.AppSettings.Get("apiStore").ToString();

        public frmNewEditStore(ILogger log)
        {
            this.logger = log;
            InitializeComponent();
        }
        #region Eventos
        private void frmNewEditStore_Load(object sender, EventArgs e)
        {
            if (this.Tag != null)
            {
                LoadDataForm((Models.Store)this.Tag);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.Tag == null)
            {
                AddStore();
                return;
            }
            UpdateStore();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Agrega un store consumiendo un webapi rest
        /// </summary>
        private void AddStore()
        {
            try
            {


                string urlApi = apiStores; //"https://localhost:44380/api/Stores";
                Models.Store store = new Models.Store();
                store.Name = txtName.Text;
                store.Address = txtAddress.Text;
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.MaxDepth = int.MaxValue;
                var inputJson = JsonConvert.SerializeObject(store, settings);
                var content = new StringContent(inputJson, System.Text.Encoding.UTF8, "application/json");

                using (HttpClient cliente = new HttpClient())
                {
                    using (var response = cliente.PostAsync(urlApi, content).Result)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                            return;
                        }
                        logger.LogInformation("La petición Post " + urlApi + " devolvió código " + response.StatusCode.ToString());
                        MessageBox.Show("No se han registrado los datos", "", MessageBoxButtons.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Ocurrió un error al actualizar el registro", ex);
                MessageBox.Show("Ocurrió un error al actualizar el registro", "", MessageBoxButtons.OK);
            }
        }
        /// <summary>
        /// Actualiza un store consumiendo un webapi rest
        /// </summary>
        private void UpdateStore()
        {
            try
            {
                Models.Store store = (Models.Store)this.Tag;
                string urlApi = apiStores + "/" + store.Id; //"https://localhost:44380/api/Stores/" + store.Id;
                store.Name = txtName.Text;
                store.Address = txtAddress.Text;
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.MaxDepth = int.MaxValue;
                var inputJson = JsonConvert.SerializeObject(store, settings);
                var content = new StringContent(inputJson, System.Text.Encoding.UTF8, "application/json");

                using (HttpClient cliente = new HttpClient())
                {
                    using (var response = cliente.PutAsync(urlApi, content).Result)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                            return;
                        }
                        logger.LogInformation("La petición Put " + urlApi + " devolvió código " + response.StatusCode.ToString());
                        MessageBox.Show("No se han registrado los datos", "", MessageBoxButtons.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Ocurrió un error al actualizar el registro", ex);
                MessageBox.Show("Ocurrió un error al actualizar el registro", "", MessageBoxButtons.OK);
            }
        }
        /// <summary>
        /// Carga los datos para edición en el formulario
        /// </summary>
        /// <param name="store">Store para edición</param>
        private void LoadDataForm(Models.Store store)
        {
            txtName.Text = store.Name;
            txtAddress.Text = store.Address;
        }
        #endregion
    }
}
