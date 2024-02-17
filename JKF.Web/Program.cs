using JKF.BLL;
using JKF.DB.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JKF.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            try
            {
                var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

                //var url = configuration["Urls"];
                var dbConnectString = configuration["dbConnectString"];

                //ע��dbConnectString
                BaseDbContext.DbConnectionString = dbConnectString;

                SysSettingsConfig.InitSysSettingsConfig();//ϵͳ����
            }
            catch (Exception ex)
            {
                throw new Exception("��ȡdbContextStringʧ�ܣ�" + ex.Message);
            }

            return Host.CreateDefaultBuilder(args)

               .ConfigureWebHostDefaults(webBuilder =>
               {
                   //webBuilder.UseUrls(url);
                   webBuilder.UseStartup<Startup>();
                   //webBuilder.UseKestrel(op =>
                  // {
                  //     op.Limits.MaxRequestBodySize = 1242880000;
                   //});
               });
        }

    }
        
}
