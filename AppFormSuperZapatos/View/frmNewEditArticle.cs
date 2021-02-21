using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Windows.Forms;

namespace Elipgo.SuperZapatos.AppFormSuperZapatos.Views
{
    public partial class frmNewEditArticle : Form
    {
        private readonly ILogger logger;
        private string apiStores = ConfigurationManager.AppSettings.Get("apiStore").ToString();
        private string apiArticles = ConfigurationManager.AppSettings.Get("apiArticles").ToString();

        public Models.Store Store { get; set; }
        
        public frmNewEditArticle(ILogger log)
        {
            this.logger = log;
            InitializeComponent();
        }

        private void frmNewEditArticle_Load(object sender, EventArgs e)
        {
            if (Store!= null)
                lblStoreName.Text = Store.Name;
            if (this.Tag != null)
            {
                LoadDataForm((Models.Article)this.Tag);
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.Tag == null && this.Store != null)
            {
                AddArticle(this.Store.Id);
                return;
            }
            UpdateArticle();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Metodos
        /// <summary>
        /// Agrega un articulo consumiendo un webapi rest
        /// </summary>
        /// <param name="storeId">Id del Store</param>
        private void AddArticle(long storeId)
        {
            try
            {
                string urlApi = apiArticles; //"https://localhost:44380/api/Articles";
                Models.Article article = new Models.Article();
                article.StoreId = storeId;
                article.Name = txtName.Text;
                article.Description = txtDescription.Text;
                article.Price = double.Parse(mskPrice.Text);
                article.TotalInShelf = double.Parse(mskTotalShelf.Text);
                article.TotalInVault = double.Parse(mskTotalVault.Text);
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.MaxDepth = int.MaxValue;
                var inputJson = JsonConvert.SerializeObject(article, settings);
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
        /// Actualiza un articulo consumiendo un webapi rest
        /// </summary>
        private void UpdateArticle()
        {
            try
            {
                Models.Article article = (Models.Article)this.Tag;
                string urlApi = apiArticles + "/" + article.Id; //"https://localhost:44380/api/Articles/" + article.Id;
                article.Name = txtName.Text;
                article.Description = txtDescription.Text;
                article.Price = double.Parse(mskPrice.Text);
                article.TotalInShelf = double.Parse(mskTotalShelf.Text);
                article.TotalInVault = double.Parse(mskTotalVault.Text);
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.MaxDepth = int.MaxValue;
                var inputJson = JsonConvert.SerializeObject(article, settings);
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
        /// <param name="article">Article para edición</param>
        private void LoadDataForm(Models.Article article)
        {
            txtName.Text = article.Name;
            txtDescription.Text = article.Description;
            mskPrice.Text = article.Price.ToString();
            mskTotalShelf.Text = article.TotalInShelf.ToString();
            mskTotalVault.Text = article.TotalInVault.ToString();
        }
        #endregion
    }
}
