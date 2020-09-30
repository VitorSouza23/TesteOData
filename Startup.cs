using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OData.Edm;
using TesteOData.EF;
using TesteOData.Models;

namespace TesteOData
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options => options.EnableEndpointRouting = false);
            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("FakeDatabase"));
            services.AddOData();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApiContext apiContext)
        {
            IEnumerable<Student> students = FakeDataFactory.CreateStudents();
            FakeDataFactory.SaveFakeStudents(students, apiContext);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => 
                endpoints.MapODataRoute("odata", "odata", GetEdmModel()));
        }

        private IEdmModel GetEdmModel(){
            var odataBuilder = new ODataConventionModelBuilder();
            odataBuilder.EntitySet<Student>("Students")
                .EntityType
                .HasKey(s => s.Id)
                .Filter()
                .Count()
                .Expand()
                .OrderBy()
                .Page()
                .Select();
            return odataBuilder.GetEdmModel();
        }
    }
}
