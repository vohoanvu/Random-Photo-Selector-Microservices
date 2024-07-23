namespace Image_Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting Image service in a separate process...");
            ImageService imageService = new();
            Console.WriteLine("Image Service is checking the image-service.txt file for pseudo-random number...");
            imageService.ProcessImageRequest();
        }
    }

    public class ImageService
    {
        private readonly string textFilePath = 
            @"D:\vu-repos\LocalPersonalDocs\Computer-Science-degree-competion\OSU\CS361\AssigmentOneUI\Image-Service\image-service.txt";
        private readonly string imageFolderPath = 
            @"D:\vu-repos\LocalPersonalDocs\Computer-Science-degree-competion\OSU\CS361\AssigmentOneUI\Image-Service\ImageAssets";

        public void ProcessImageRequest()
        {
            while (true)
            {
                string content = File.ReadAllText(textFilePath);
                if (int.TryParse(content, out int i))
                {
                    File.WriteAllText(textFilePath, string.Empty);
                    Thread.Sleep(2000);
                    string[] imageFiles = Directory.GetFiles(imageFolderPath, "*.jpg");

                    if (imageFiles.Length > 0)
                    {
                        Console.WriteLine("Writing the corresponding image file path to image-service.txt...");
                        int selectedImageIndex = (i - 1) % imageFiles.Length;
                        string selectedImagePath = imageFiles[selectedImageIndex];
                        string relativeImagePath = Path.GetFileName(selectedImagePath);
                        string genericPathPrefix = "~/Image-Service/ImageAssets/";
                        string finalPath = genericPathPrefix + relativeImagePath;

                        File.WriteAllText(textFilePath, finalPath);
                    }
                }

                Thread.Sleep(3000);
            }
        }
    }
}
