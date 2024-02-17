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

                //注册dbConnectString
                BaseDbContext.DbConnectionString = dbConnectString;

                SysSettingsConfig.InitSysSettingsConfig();//系统配置
            }
            catch (Exception ex)
            {
                throw new Exception("获取dbContextString失败！" + ex.Message);
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
