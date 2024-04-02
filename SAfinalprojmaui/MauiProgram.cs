using Microsoft.Extensions.Logging;
using MySqlConnector;

namespace SAfinalprojmaui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });


            // create sql connection builder string 
            var builderString = new MySqlConnectionStringBuilder()
            {
                // define your server, we gonna use the localhost 
                Server = "localhost",
                // Which database to use
                Database = "villagerentals1",
                // Add your credentials for the Database
                UserID = "root",
                Password = "1234",
            };




#if DEBUG
            builder.Logging.AddDebug();
#endif


            return builder.Build();




        }






    }
}
