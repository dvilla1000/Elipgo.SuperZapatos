using Elipgo.SuperZapatos.AppFormSuperZapatos;
using Elipgo.SuperZapatos.AppFormSuperZapatos.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppFormSuperZapatos
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var services = new ServiceCollection();
            ConfigureServices(services);
            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                var formPrincipal = serviceProvider.GetRequiredService<frmPrincipal>();
                Application.Run(formPrincipal);
            }

        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<frmPrincipal>()
                    .AddLogging(configure => configure.AddConsole());
        }
    }
}
