using Elipgo.SuperZapatos.AppFormSuperZapatos.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace Elipgo.SuperZapatos.AppFormSuperZapatos.Views
{
    public partial class frmStores : Form
    {
        private readonly ILogger logger;
        private string apiStores = ConfigurationManager.AppSettings.Get("apiStore").ToString();

        public frmStores(ILogger log)
        {
            logger = log;
            InitializeComponent();
            //var hostBuilder = Host.CreateDefaultBuilder()
            //    .ConfigureAppConfiguration((context, builder) =>
            //    {
            //        // Add other configuration files...
            //        config = builder.AddJsonFile("clientsettings.json", optional: true).Build();

            //    });
            ConfigurarGrid();
        }

        #region Eventos
        private void frmStores_Load(object sender, EventArgs e)
        {
            LoadStores();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            LoadEditForm("New Record", null);
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (gvwStores.CurrentRow != null)
            {
                LoadEditForm("Update Record", gvwStores.CurrentRow.DataBoundItem);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gvwStores.CurrentRow != null)
            {
                DeleteStore(((Store)gvwStores.CurrentRow.DataBoundItem).Id);
            }
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Configura aspectos del gridview
        /// </summary>
        private void ConfigurarGrid()
        {
            gvwStores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gvwStores.MultiSelect = false;
        }
        /// <summary>
        /// Carga el formulario de edición
        /// </summary>
        /// <param name="title">Titulo del formulario</param>
        /// <param name="record">Registro para editar</param>
        private void LoadEditForm(string title, object record)
        {
            try
            { 
            frmNewEditStore formulario = new frmNewEditStore(logger);
            formulario.Text = title;
            formulario.Tag = record;
            var result = formulario.ShowDialog(this);
            if (result == DialogResult.OK)
                LoadStores();
            }
            catch (Exception ex)
            {
                logger.LogError("Ocurrió un error al cargar el formulario para edición.", ex);
            }
        }
        /// <summary>
        /// Carga los datos de los Stores
        /// </summary>
        private void LoadStores()
        {
            try
            {
                string urlApi = apiStores; //"https://localhost:44380/api/Stores";
                string apiResponse;
                using (HttpClient cliente = new HttpClient())
                {
                    using (var response = cliente.GetAsync(urlApi).Result)
                    {
                        switch (response.StatusCode)
                        {
                            case HttpStatusCode.OK:
                                {
                                    apiResponse = response.Content.ReadAsStringAsync().Result;
                                    var datos = JsonConvert.DeserializeObject(apiResponse);
                                    //gvwStores.DataSource = ((Newtonsoft.Json.Linq.JObject)datos).SelectToken("stores").ToObject<Stores>();
                                    var datosResponse = JsonConvert.DeserializeObject<StoresModel>(apiResponse);
                                    gvwStores.DataSource = datosResponse.Stores;
                                    lblTotal.Text = datosResponse.TotalElements.ToString();
                                    lblTitulo.Visible = true;
                                    lblTotal.Visible = true;
                                    lblMsg.Visible = false;
                                }
                                break;
                            case HttpStatusCode.BadRequest:
                            case HttpStatusCode.NotFound:
                            case HttpStatusCode.InternalServerError:
                                {
                                    apiResponse = response.Content.ReadAsStringAsync().Result;
                                    var datosResponse = JsonConvert.DeserializeObject<ErrorModel>(apiResponse);
                                    lblMsg.Text = datosResponse.ErrorMessage;
                                    lblTitulo.Visible = false;
                                    lblTotal.Visible = false;
                                    lblMsg.Visible = true;
                                }
                                break;
                            default:
                                {
                                    lblMsg.Text = "Ocurrió un error";
                                    lblTitulo.Visible = false;
                                    lblTotal.Visible = false;
                                    lblMsg.Visible = true;
                                }
                                break;
                        }
                    }
                }
                if (gvwStores.Columns.Count > 0)
                {
                    gvwStores.Columns["Id"].Visible = false;
                    gvwStores.AutoResizeColumns();
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Ocurrió un error al cargar Stores", ex);
            }
        }
        /// <summary>
        /// Elimina un registro
        /// </summary>
        /// <param name="id">Id del registro</param>
        private void DeleteStore(long id)
        {
            try
            {
                string urlApi = apiStores + "/" + id; //"https://localhost:44380/api/Stores" + id;
                using (HttpClient cliente = new HttpClient())
                {
                    using (var response = cliente.DeleteAsync(urlApi).Result)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        { 
                            LoadStores();
                            return;
                        }
                        MessageBox.Show("No se eliminó el registro", "Delete Record", MessageBoxButtons.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Ocurrió un error al eliminar el articulo id " + id, ex);
            }
        }
        #endregion
    }
}
