namespace PRNG_Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting PRNG service in a separate process...");
            PRNGService prngService = new();
            
            Console.WriteLine("PRNG Service is now checking the prng-service.text file for 'run' command...");
            prngService.ProcessPrngRequest();
        }
    }

    public class PRNGService
    {
        private readonly string filePath = 
            @"D:\vu-repos\LocalPersonalDocs\Computer-Science-degree-competion\OSU\CS361\AssigmentOneUI\PRNG-Service\prng-service.txt";

        public void ProcessPrngRequest()
        {
            while (true)
            {
                string content = File.ReadAllText(filePath);

                if (content.Contains("run"))
                {
                    Thread.Sleep(2000);
                    Console.WriteLine("RUN request is found! Writing pseudo-random number to it...");
                    using var writer = new StreamWriter(filePath, false);
                    Random random = new();
                    int randomNumber = random.Next(1, 100);
                    writer.Write(randomNumber.ToString());
                }

                Thread.Sleep(3000);
            }
        }
    }
}
