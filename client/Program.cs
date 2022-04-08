using System.Threading.Tasks;
using Grpc.Net.Client;

namespace client
{
    public class Program {
        public static void Main(string[] args) {
            using (GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:8080")) {
                Factorial.FactorialClient client = new Factorial.FactorialClient(channel);
                Console.Clear();

                var reply = client.GetFactorial(
                    new FactorialRequest{
                        Number = GetInput()
                    }
                );

                Console.WriteLine("\nResposta do Servidor: " + reply.Message);
                Console.WriteLine(reply.Result);

                Console.WriteLine("\nPressione qualquer tecla para sair...");
                Console.ReadKey();
            }
        }

        private static uint GetInput() {
            string? input;
            int parsedInput;
            uint validatedInput;
            bool parsed;

            Console.WriteLine("Digite um valor maior ou igual a zero:");


            do {
                input = Console.ReadLine();
                parsed = int.TryParse(input, out parsedInput);

                if(!parsed || parsedInput < 0)
                    Console.WriteLine("Valor inválido. Digite novamente.");

            } while(!parsed || parsedInput < 0);

            validatedInput = Convert.ToUInt32(parsedInput);

            return validatedInput;
        }
    }    
}