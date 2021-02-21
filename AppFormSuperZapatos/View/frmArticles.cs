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
    public partial class frmArticles : Form
    {
        private readonly ILogger logger;
        private string apiStores = ConfigurationManager.AppSettings.Get("apiStore").ToString();
        private string apiArticles = ConfigurationManager.AppSettings.Get("apiArticles").ToString();

        public frmArticles(ILogger log)
        {
            logger = log;
            InitializeComponent();
            ConfigurarGrid();
        }

        #region Eventos
        private void frmArticles_Load(object sender, EventArgs e)
        {
            LoadStores();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbStores.SelectedItem != null)
                LoadEditForm("New Record", null, (Models.Store)cmbStores.SelectedItem);
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (gvwArticles.CurrentRow != null)
            {
                LoadEditForm("Update Record", gvwArticles.CurrentRow.DataBoundItem, (Models.Store)cmbStores.SelectedItem);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gvwArticles.CurrentRow != null)
            {
                DeleteArticle(((Article)gvwArticles.CurrentRow.DataBoundItem).Id);
            }
        }
        private void cmbStores_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).SelectedItem != null)
                LoadArticles(((Models.Store)((ComboBox)sender).SelectedItem).Id);
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Configura aspectos del gridview
        /// </summary>
        private void ConfigurarGrid()
        {
            gvwArticles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gvwArticles.MultiSelect = false;
            cmbStores.AutoCompleteMode = AutoCompleteMode.None;
        }
        /// <summary>
        /// Carga el formulario de edición
        /// </summary>
        /// <param name="title">Titulo del formulario</param>
        /// <param name="record">Registro para editar</param>
        /// <param name="store">Store seleccionado</param>
        private void LoadEditForm(string title, object record, Models.Store store)
        {
            try
            {
                frmNewEditArticle formulario = new frmNewEditArticle(logger);
                formulario.Text = title;
                formulario.Tag = record;
                formulario.Store = store;
                var result = formulario.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    if (cmbStores.SelectedItem != null)
                        LoadArticles(((Models.Store)cmbStores.SelectedItem).Id);
                }
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
                string urlApi = apiStores; // "https://localhost:44380/api/Stores";
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
                                    cmbStores.DataSource = datosResponse.Stores;
                                    cmbStores.DisplayMember = "Name";
                                    cmbStores.ValueMember = "Id";
                                }
                                break;
                            case HttpStatusCode.BadRequest:
                            case HttpStatusCode.NotFound:
                            case HttpStatusCode.InternalServerError:
                                {
                                    apiResponse = response.Content.ReadAsStringAsync().Result;
                                    var datosResponse = JsonConvert.DeserializeObject<ErrorModel>(apiResponse);
                                    //MessageBox.Show(datosResponse.ErrorMessage, datosResponse.ErrorCode.ToString(), MessageBoxButtons.OK);
                                    lblMsg.Text = datosResponse.ErrorCode.ToString() + " - " + datosResponse.ErrorMessage;
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
            }
            catch (Exception ex)
            {
                logger.LogError("Ocurrió un error al cargar Stores", ex);
            }
        }
        /// <summary>
        /// Carga los datos de los Articles
        /// </summary>
        /// <param name="idStore">Id del Store</param>
        private void LoadArticles(long idStore)
        {
            try
            {
                string urlApi = apiStores + "/" + idStore + "/Articles"; // "https://localhost:44380/api/Stores/" + idStore.ToString() + "/Articles";
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
                                    var datosResponse = JsonConvert.DeserializeObject<ArticlesModel>(apiResponse);
                                    gvwArticles.DataSource = datosResponse.Articles;
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
                        if (response.StatusCode != HttpStatusCode.OK)
                        {
                            lblTotal.Text = "No se han cargado los datos";
                            return;
                        }
                    }
                }
                if (gvwArticles.Columns.Count >= 1)
                {
                    gvwArticles.Columns["Id"].Visible = false;
                    gvwArticles.Columns["StoreId"].Visible = false;
                    gvwArticles.AutoResizeColumns();
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Ocurrió un error al cargar Articles.", ex);
            }
        }
        /// <summary>
        /// Elimina un registro
        /// </summary>
        /// <param name="id">Id del registro</param>
        private void DeleteArticle(long id)
        {
            try
            {
                string urlApi = apiArticles + "/" + id; //"https://localhost:44380/api/Article/" + id;
                using (HttpClient cliente = new HttpClient())
                {
                    using (var response = cliente.DeleteAsync(urlApi).Result)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            if (cmbStores.SelectedItem != null)
                                LoadArticles(((Models.Store)cmbStores.SelectedItem).Id);
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
