using Grpc.Core;
using System.Diagnostics;
using server;

namespace server.Services;

public class FactorialService : Factorial.FactorialBase
{
    private readonly ILogger<FactorialService> _logger;
    public FactorialService(ILogger<FactorialService> logger)
    {
        _logger = logger;
    }

    override
    public Task<FactorialReply> GetFactorial(
        FactorialRequest request, 
        ServerCallContext context)
    {
        var watch = new System.Diagnostics.Stopwatch();

        watch.Start();
        ulong result = CalculateFactorial(request.Number);
        watch.Stop();

        Task<FactorialReply> reply = Task.FromResult(new FactorialReply
            {
                Message = (request.Number >= 0) ? "Valid Request" : "Invalid Request",
                Result = result
            }
        );

        Console.WriteLine(context.Peer);
        Console.WriteLine(watch.Elapsed.TotalMilliseconds + "ms");

        return reply;
    }

    private ulong CalculateFactorial(uint input) {
        if(input > 0) {
            return input * CalculateFactorial(input - 1);
        }
        return 1;
    }
}
