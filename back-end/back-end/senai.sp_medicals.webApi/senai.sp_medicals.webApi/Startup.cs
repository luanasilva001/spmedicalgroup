using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using senai.sp_medicals.webApi.Interfaces;
using senai.sp_medicals.webApi.Settings;
using System;
using System.IO;
using System.Reflection;
using Microsoft.OpenApi.Models;

namespace senai.sp_medicals.webApi
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

    public void ConfigureServices(IServiceCollection services)
    {
      // Adiciona o CORS ao projeto
      services.AddCors(options =>
      {
        options.AddPolicy("CorsPolicy",
            builder =>
            {
              builder.WithOrigins("http://localhost:3000", "http://localhost:19006")
                                                                  .AllowAnyHeader()
                                                                  .AllowAnyMethod();
            }
        );
      });
      services.AddTransient<IMailService, MailService.WebApi.Services.MailService>();

      services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "SPMedicals.webApi", Version = "v1" });

        // Set the comments path for the Swagger JSON and UI.
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
      });

      services
         .AddControllers()
         .AddNewtonsoftJson(options =>
         {
           //Ignora os loopings nas consultas
           options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

           //Ignora valores nulos ao fazer jun��es nas consultas
           options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
         });

      services
      // Define a forma de autentica��o
      .AddAuthentication(options =>
          {
            options.DefaultAuthenticateScheme = "JwtBearer";
            options.DefaultChallengeScheme = "JwtBearer";
          })

          // Define os par�metros de valida��o do token
          .AddJwtBearer("JwtBearer", options =>
          {
            options.TokenValidationParameters = new TokenValidationParameters
            {
              // quem est� emitindo
              ValidateIssuer = true,

              // quem est� recebendo
              ValidateAudience = true,

              // o tempo de expira��o ser� validado
              ValidateLifetime = true,

              // forma de criptografia e a chave de autentica��o
              IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("medicals-chave-autenticacao")),

              // tempo de expira��o do token 
              ClockSkew = TimeSpan.FromMinutes(5),

              // nome do issuer, de onde est� vindo
              ValidIssuer = "medicals.webApi",

              // nome do audience, para onde est� indo
              ValidAudience = "medicals.webApi"
            };
          });
    }
    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      // Habilita o CORS
      app.UseCors("CorsPolicy");

      app.UseSwagger();

      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
      });

      app.UseSwagger(c =>
      {
        c.SerializeAsV2 = true;
      });

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseAuthentication();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
