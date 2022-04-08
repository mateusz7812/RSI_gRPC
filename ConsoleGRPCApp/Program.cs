using Grpc.Net.Client;
using System;
using System.Threading.Tasks;
using GrpcServerService;
using RSI_lab2;

namespace ConsoleGRPCApp
{
    class Program
    {
        static async Task Main(string [] args)
        {
            MyData.Info();
            //GrpcChannel channel = await GreeterTest();
            GrpcChannel channel = await MyGrpcTest();
        }


        private static async Task<GrpcChannel> MyGrpcTest()
        {
            Console.WriteLine("Starting gRPC Client");
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new GrpcService.GrpcServiceClient(channel);
            Console.Write("Enter the name: ");
            String str = Console.ReadLine();
            int val = 21;
            var reply = await client.GrpcProcAsync(new GrpcRequest
            {
                Name = str,
                Age = val
            });
            Console.WriteLine("From server: " + reply.Message);
            Console.WriteLine("From server: " + val + " years = " + reply.Days + " days");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            channel.ShutdownAsync().Wait();
            return channel;
        }

        private static async Task<GrpcChannel> GreeterTest()
        {
            // The port number(5001) must match the port of the gRPC server.
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(
                              new HelloRequest { Name = "GreeterClient" });
            Console.WriteLine("Greeting: " + reply.Message);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            return channel;
        }
    }
}
