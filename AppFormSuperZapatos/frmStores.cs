using Elipgo.SuperZapatos.AppFormSuperZapatos.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace Elipgo.SuperZapatos.AppFormSuperZapatos
{
    public partial class frmStores : Form
    {
        //IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();

        //IConfiguration config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
        //    .AddJsonFile("clientsettings.json", true, true).Build();

        //hostBuilder = Host.CreateDefaultBuilder()
        //        .ConfigureAppConfiguration((context, builder) =>
        //        {
        //            // Add other configuration files...
        //            builder.AddJsonFile("Custom.json", optional: true);

        //        })

        IConfiguration config = null;

        public frmStores()//IConfiguration configuration
        {
            InitializeComponent();
            var hostBuilder = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, builder) =>
                {
                    // Add other configuration files...
                    config = builder.AddJsonFile("clientsettings.json", optional: true).Build();

                });
            //hostBuilder.Build().get
            //config = configuration;     
            ConfigurarGrid();
        }

        private void LoadStores()
        {
            //string urlApi = config.GetSection("Api").GetSection("endpointStore").Value;
            string urlApi = "https://localhost:44380/api/Stores";
            string apiResponse;
            //var datosResponse = object;
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
                        //case HttpStatusCode.Created:
                        //    break;
                        //case HttpStatusCode.Accepted:
                        //    break;
                        //case HttpStatusCode.NoContent:
                        //    break;
                        //case HttpStatusCode.Found:
                        //    break;
                        //case HttpStatusCode.NotModified:
                        //    break;
                        case HttpStatusCode.BadRequest:
                        case HttpStatusCode.NotFound:
                        //case HttpStatusCode.RequestTimeout:
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

                    //string apiResponse = response.Content.ReadAsStringAsync().Result;
                    //var datos = JsonConvert.DeserializeObject(apiResponse);
                    ////gvwStores.DataSource = ((Newtonsoft.Json.Linq.JObject)datos).SelectToken("stores").ToObject<Stores>();
                    //var datosResponse = JsonConvert.DeserializeObject<StoresModel>(apiResponse);
                    //gvwStores.DataSource = datosResponse.Stores;
                    //lblTotal.Text = datosResponse.TotalElements.ToString();
                }
            }
            if (gvwStores.Columns.Count >= 3)
            {
                gvwStores.Columns[0].Visible = false;
                //gvwStores.Columns[1].Width = 50;            
                //gvwStores.Columns[2].Width = 250;
                gvwStores.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                gvwStores.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void frmStores_Load(object sender, EventArgs e)
        {
            LoadStores();
        }

        private void splitContainer_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            LoadEditForm("New Record",null);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (gvwStores.CurrentRow != null)
            {
                LoadEditForm("Update Record", gvwStores.CurrentRow.DataBoundItem);
            }            
        }



        #region Métodos
        private void ConfigurarGrid()
        {
            gvwStores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //gvwStores.CanSelect = true;
            gvwStores.MultiSelect = false;
            //if (gvwStores.Columns.Count >= 3)
            //{
            //    gvwStores.Columns[0].Visible = false;
            //    //gvwStores.Columns[1].Width = 50;            
            //    //gvwStores.Columns[2].Width = 250;
            //    gvwStores.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //    gvwStores.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //}
        }
        private void LoadEditForm(string title, object record)
        {
            frmNewEditStore formulario = new frmNewEditStore();
            formulario.Text = title;
            formulario.Tag = record;
            //formulario.Tag = gvwStores.SelectedRows;            
            var result = formulario.ShowDialog(this);
            if (result == DialogResult.OK)
                LoadStores();
        }
        #endregion
    }
}
