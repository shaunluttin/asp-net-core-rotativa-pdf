using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace asp_net_core_rotativa_pdf
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
            Rotativa.AspNetCore.RotativaConfiguration.Setup(env);
        }
    }

    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var pdf = new Rotativa.AspNetCore.ViewAsPdf("Index")
            {
                FileName = "C:\\Test.pdf",
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageHeight = 20,
            };

            var byteArray = await pdf.BuildFile(ControllerContext);
            return File(byteArray, "application/pdf");
        }
    }
}
