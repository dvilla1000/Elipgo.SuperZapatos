using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace Elipgo.SuperZapatos.AppFormSuperZapatos
{
    public partial class frmNewEditStore : Form
    {
        public frmNewEditStore()
        {
            InitializeComponent();
        }

        private void frmNewEditStore_Load(object sender, EventArgs e)
        {
            if (this.Tag != null)
            {
                ;                
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void AddStore()
        {
            string urlApi = "https://localhost:44380/api/Stores";
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
                        this.Close();
                    }
                }
            }
        }

        private void UpdateStore()
        {
            Models.Store store = (Models.Store) this.Tag;
            string urlApi = "https://localhost:44380/api/Stores/" + store.Id;
            //Models.Store store = new Models.Store();
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
                        this.Close();
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.Tag == null)
            {
                AddStore();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
