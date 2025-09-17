using DataRetrieval.Administrator.Services;

namespace DataRetrieval.Administrator
{
    public class AdminApplication
    {
        private readonly IClientApiService _clientApiService;
        

        public AdminApplication(IClientApiService clientApiService)
        {
            _clientApiService = clientApiService;
        }

        public async Task RunAsync()
        {
            Console.WriteLine("Data Retrieval Administrator Console");

            while (true)
            {
                DisplayMenu();
                var choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            await DisplayScreenRecordings();
                            break;
                        case "2":
                            await DisplayEnvironmentRecordings();
                            break;
                        case "3":
                            await DisplayUsbActivities();
                            break;
                        case "4":
                            await GetSpecificRecord();
                            break;
                        case "5":
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        private void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. View Screen Recordings");
            Console.WriteLine("2. View Environment Recordings");
            Console.WriteLine("3. View USB Activities");
            Console.WriteLine("4. Get Specific Record");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice (1-5): ");
        }

        private async Task DisplayScreenRecordings()
        {
            Console.WriteLine("\nRetrieving Screen Recordings...");
            var recordings = await _clientApiService.GetScreenRecordingsAsync();
            
            Console.WriteLine($"\nFound {recordings.Count()} screen recordings:");
            Console.WriteLine(new string('-', 80));
            Console.WriteLine($"{"ID",-12} {"Timestamp",-20} {"File Name",-25} {"Duration",-10} {"Status",-10}");
            Console.WriteLine(new string('-', 80));

            foreach (var recording in recordings.Take(10))
            {
                Console.WriteLine($"{recording.Id,-12} {recording.Timestamp:yyyy-MM-dd HH:mm,-20} {recording.FileName,-25} {recording.DurationSeconds + "s",-10} {recording.Status,-10}");
            }

            if (recordings.Count() > 10)
                Console.WriteLine($"... and {recordings.Count() - 10} more records");
        }

        private async Task DisplayEnvironmentRecordings()
        {
            Console.WriteLine("\nRetrieving Environment Recordings...");
            var recordings = await _clientApiService.GetEnvironmentRecordingsAsync();
            
            Console.WriteLine($"\nFound {recordings.Count()} environment recordings:");
            Console.WriteLine(new string('-', 80));
            Console.WriteLine($"{"ID",-12} {"Timestamp",-20} {"Device",-15} {"Duration",-10} {"Quality",-10}");
            Console.WriteLine(new string('-', 80));

            foreach (var recording in recordings.Take(10))
            {
                Console.WriteLine($"{recording.Id,-12} {recording.Timestamp:yyyy-MM-dd HH:mm,-20} {recording.DeviceName,-15} {recording.DurationSeconds + "s",-10} {recording.AudioQuality,-10}");
            }

            if (recordings.Count() > 10)
                Console.WriteLine($"... and {recordings.Count() - 10} more records");
        }

        private async Task DisplayUsbActivities()
        {
            Console.WriteLine("\nRetrieving USB Activities...");
            var activities = await _clientApiService.GetUsbActivitiesAsync();
            
            Console.WriteLine($"\nFound {activities.Count()} USB activities:");
            Console.WriteLine(new string('-', 80));
            Console.WriteLine($"{"ID",-12} {"Timestamp",-20} {"Device",-15} {"Action",-12} {"Serial",-12}");
            Console.WriteLine(new string('-', 80));

            foreach (var activity in activities.Take(10))
            {
                Console.WriteLine($"{activity.Id,-12} {activity.Timestamp:yyyy-MM-dd HH:mm,-20} {activity.DeviceName,-15} {activity.Action,-12} {activity.SerialNumber,-12}");
            }

            if (activities.Count() > 10)
                Console.WriteLine($"... and {activities.Count() - 10} more records");
        }

       private async Task GetSpecificRecord()
        {
            Console.WriteLine("\nGet Specific Record");
            Console.WriteLine("1. Screen Recording");
            Console.WriteLine("2. Environment Recording");
            Console.WriteLine("3. USB Activity");
            Console.Write("Choose type (1-3): ");
            
            var typeChoice = Console.ReadLine();
            Console.Write("Enter ID: ");
            var id = Console.ReadLine();

            if (string.IsNullOrEmpty(id))
            {
                Console.WriteLine("Invalid ID provided.");
                return;
            }

            switch (typeChoice)
            {
                case "1":
                    var screenRec = await _clientApiService.GetScreenRecordingByIdAsync(id);
                    if (screenRec != null)
                    {
                        Console.WriteLine($"\nScreen Recording Details:");
                        Console.WriteLine($"ID: {screenRec.Id}");
                        Console.WriteLine($"File: {screenRec.FileName}");
                        Console.WriteLine($"Resolution: {screenRec.Resolution}");
                        Console.WriteLine($"Duration: {screenRec.DurationSeconds}s");
                        Console.WriteLine($"Status: {screenRec.Status}");
                    }
                    else
                    {
                        Console.WriteLine("Screen recording not found.");
                    }
                    break;
                case "2":
                    var envRec = await _clientApiService.GetEnvironmentRecordingByIdAsync(id);
                    if (envRec != null)
                    {
                        Console.WriteLine($"\nEnvironment Recording Details:");
                        Console.WriteLine($"ID: {envRec.Id}");
                        Console.WriteLine($"File: {envRec.AudioFileName}");
                        Console.WriteLine($"Device: {envRec.DeviceName}");
                        Console.WriteLine($"Quality: {envRec.AudioQuality}");
                        Console.WriteLine($"Volume: {envRec.VolumeLevel}%");
                    }
                    else
                    {
                        Console.WriteLine("Environment recording not found.");
                    }
                    break;
                case "3":
                    var usbAct = await _clientApiService.GetUsbActivityByIdAsync(id);
                    if (usbAct != null)
                    {
                        Console.WriteLine($"\nUSB Activity Details:");
                        Console.WriteLine($"ID: {usbAct.Id}");
                        Console.WriteLine($"Device: {usbAct.DeviceName}");
                        Console.WriteLine($"Action: {usbAct.Action}");
                        Console.WriteLine($"Vendor ID: {usbAct.VendorId}");
                        Console.WriteLine($"Serial: {usbAct.SerialNumber}");
                    }
                    else
                    {
                        Console.WriteLine("USB activity not found.");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
}