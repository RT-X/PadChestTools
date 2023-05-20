namespace DataFilterer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter path to CSV for filtering");
            string csvFilePath = Console.ReadLine()?.Trim('\"') ?? "";

            Console.WriteLine("\nPlease enter the root directory of data");
            string inputDataDirectory = Console.ReadLine()?.Trim('\"') ?? "";

            Console.WriteLine("\nPlease enter the directory to output categorised data");
            string outputDataDirectory = Console.ReadLine()?.Trim('\"') ?? "";

            // Construct a dictionary of Conditions : List of 'Image'
            Dictionary<string, List<Image>> conditionImageDictionary = new Dictionary<string, List<Image>>();

            // Read in the CSV but skip the first line of the CSV as it contains the column headers
            List<string> csvFileContents = File.ReadAllLines(csvFilePath).Skip(1).ToList();

            // Iterate over each line/record in the CSV
            foreach (var record in csvFileContents)
            {
                // Split the CSV record into its relative parts
                string[] tokenisedRecord = record.Split(",");

                Image image = new Image
                {
                    imageID = tokenisedRecord[0] ?? "Null",
                    imageDir = tokenisedRecord[1] ?? "Null",
                    patientID = tokenisedRecord[2] ?? "Null",
                    projection = tokenisedRecord[3] ?? "Null",
                    condition = tokenisedRecord[4] ?? "Null"
                };

                // Check if the dictionary has a key for the condition, insert if it's not
                if (!conditionImageDictionary.ContainsKey(image.condition))
                    conditionImageDictionary.Add(image.condition, new List<Image>());

                // Insert the 'Image' into the dictionary
                conditionImageDictionary[image.condition].Add(image);
            }

            // Following the above filtration, write the data to disk, categorising by condition
            foreach (var conditionImagesSet in conditionImageDictionary)
            {
                // Foreach image in the condition, write to disk
                foreach (Image image in conditionImagesSet.Value)
                {
                    string sourcePath = $"{inputDataDirectory}\\{image.imageDir}\\{image.imageID}";
                    string outputPath = $"{outputDataDirectory}\\{image.condition}\\{image.patientID}\\{image.projection}\\{image.imageID}";

                    if (File.Exists(sourcePath))
                    {
                        Directory.CreateDirectory($"{outputDataDirectory}\\{image.condition}\\{image.patientID}\\{image.projection}");
                        File.Copy(sourcePath, outputPath);
                    }
                    else
                    {
                        Console.WriteLine($"WARN: Could not find image {image.imageID} at {sourcePath}");
                    }
                }
                Console.WriteLine($"Finished copying images for {conditionImagesSet.Key}");
            }
        }

        internal class Image
        {
            public string? imageID { get; set; }
            public string? imageDir { get; set; }
            public string? patientID { get; set; }
            public string? projection { get; set; }
            public string? condition { get; set; }
        }
    }
}