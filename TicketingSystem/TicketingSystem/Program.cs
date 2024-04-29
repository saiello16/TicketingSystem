using System;
using System.Collections.Generic;
using System.IO;

class TicketManager
{
    private const string FILE_NAME = "tickets.csv";
    private static readonly string[] HEADERS = { "TicketID", "Summary", "Status", "Priority", "Submitter", "Assigned", "Watching" };

    static void Main()
    {
        bool running = true;

        while (running)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Read data from file");
            Console.WriteLine("2. Create file from data");
            Console.WriteLine("3. Exit");
            Console.Write("Enter your choice: ");
            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid choice. Please enter a number between 1 and 3.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    ReadDataFromFile();
                    break;
                case 2:
                    CreateFileFromData();
                    break;
                case 3:
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 3.");
                    break;
            }
        }
    }

    static void ReadDataFromFile()
    {
        try
        {
            using (StreamReader reader = new StreamReader(FILE_NAME))
            {
                string line;
                bool isFirstLine = true;
                while ((line = reader.ReadLine()) != null)
                {
                    if (isFirstLine)
                    {
                        isFirstLine = false;
                        continue; // Skip the header line
                    }
                    Console.WriteLine(line);
                }
            }
        }
        catch (IOException e)
        {
            Console.WriteLine("Error reading data from file: " + e.Message);
        }
    }

    static void CreateFileFromData()
    {
        List<string> data = new List<string>();
        Console.WriteLine("Enter ticket details (TicketID, Summary, Status, Priority, Submitter, Assigned, Watching), or 'done' to finish:");
        while (true)
        {
            Console.Write("Enter ticket details: ");
            string input = Console.ReadLine();
            if (input.Equals("done", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }
            data.Add(input);
        }
        WriteToFile(data);
    }

    static void WriteToFile(List<string> data)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(FILE_NAME))
            {
                writer.WriteLine(string.Join(",", HEADERS));
                foreach (string line in data)
                {
                    writer.WriteLine(line);
                }
                Console.WriteLine("File created successfully.");
            }
        }
        catch (IOException e)
        {
            Console.WriteLine("Error creating file: " + e.Message);
        }
    }
}

