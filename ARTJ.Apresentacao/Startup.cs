using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace ARTJ.Apresentacao
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Validade do token e verificação da expiração do token
            var key = Encoding.ASCII.GetBytes(Settings.SecretKey);//Pegando os bits da string
            services.AddHealthChecks();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ARTJ.Apresentacao", Version = "v1" });
            });

            services.AddAuthentication(x =>
            {//Basicamente aqui está setado a configuração para procurar por um token, ver se o teken existe  
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;//Schema de altenticação que estamos utilizando || Bearer and JWT
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;//Basicamente estou dizendo que ele não precisa do HTTPS
                x.SaveToken = true; //Pedir para salvar porém não persistir em nehum lugar pq não está configurado
                x.TokenValidationParameters = new TokenValidationParameters
                {//Aqui são os parametros que ele vai precisar para efetuar a validação do token
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = true,
                };
            });



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ARTJ.Apresentacao v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();//Tem que ser nessa ordem Authe e Autho

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
