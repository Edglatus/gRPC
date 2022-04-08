using server.Services;

namespace server
{
    public class Program {
        public static void Main(string[] args) {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.Services.AddGrpc();

            WebApplication app = builder.Build();

            app.MapGrpcService<FactorialService>();
            app.MapGet("/", () => "Reached gRPC program!");

            app.Run();
        }
    }
}
