using SwitchboardMimic;

public class Program
{
    static Switchboard switchboard = new Switchboard();
    static List<Appliance> appliances = new List<Appliance>();
    public static void Main(string[] args)
    {
        AddAppliances();
        DisplayMainMenu();
    }

    public static void DisplayMainMenu()
    {
        while (true)
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine("Chose an appliance from the menu to operate: ");
                int count = 1;
                appliances.ForEach(item =>
                {
                    Switch sch = switchboard.GetSwitch(item.Id);
                    // ternary operator
                    string state = sch.State == SwitchState.On ? "ON" : "OFF";
                    Console.WriteLine($"{count++}. {item.DeviceName} is {state}");
                });

                Console.WriteLine("Press 0 for exit");
                Console.WriteLine();
                int selectedDeviceId = int.Parse(Console.ReadLine());
                Console.WriteLine();

                if (selectedDeviceId == 0)
                {
                    Environment.Exit(0);
                }
                else
                {
                    Switch sch = switchboard.GetSwitch(selectedDeviceId);
                    Appliance appliance = appliances.SingleOrDefault(x => x.Id == selectedDeviceId);
                    if (appliance == null)
                    {
                        Console.WriteLine("invalid device selected");
                    }
                    else
                    {
                        ShowSubMenu(appliance.DeviceName, sch.DeviceId, sch.State);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                DisplayMainMenu();
            }
        }
    }

    public static void ShowSubMenu(string deviceName, int deviceId, SwitchState state)
    {
        Console.WriteLine();
        if (state == SwitchState.On)
        {
            Console.WriteLine($"1. Switch {deviceName} OFF");
        }
        else
        {
            Console.WriteLine($"1. Switch {deviceName} ON");
        }
        Console.WriteLine($"2. Back");
        Console.WriteLine();

        int option = int.Parse(Console.ReadLine());

        if (option == 1 && state == SwitchState.On)
        {
            switchboard.Toggle(deviceId, SwitchState.Off);
        }
        else if (option == 1 && state == SwitchState.Off)
        {
            switchboard.Toggle(deviceId, SwitchState.On);
        }
        else if (option == 2)
        {
            DisplayMainMenu();
        }
        else
        {
            Console.WriteLine("invalid selection");
            ShowSubMenu(deviceName, deviceId, state);
        }
    }

    public static void AddAppliances()
    {
        try
        {
            Console.WriteLine("Please enter number of fans you want to connect: ");
            int fans = int.Parse(Console.ReadLine());

            // Add Fans
            for (int i = 1; i <= fans; i++)
            {
                // Add Appliance
                int deviceId = appliances.Count() + 1;
                Appliance appliance = new Appliance();
                appliance.Id = deviceId;
                appliance.DeviceType = DeviceType.Fan;
                appliance.DeviceName = $"Fan {i}";
                appliances.Add(appliance);

                // Add SWITCH
                switchboard.AddSwitch(deviceId);

            }

            // Add ACs
            Console.WriteLine("Please enter number of ACs you want to connect: ");
            int ac = int.Parse(Console.ReadLine());
            for (int i = 1; i <= ac; i++)
            {
                // Add Appliance
                int deviceId = appliances.Count() + 1;
                Appliance appliance = new Appliance();
                appliance.Id = deviceId;
                appliance.DeviceType = DeviceType.AC;
                appliance.DeviceName = $"AC {i}";
                appliances.Add(appliance);

                // Add SWITCH
                switchboard.AddSwitch(deviceId);
            }

            // Add bulbs
            Console.WriteLine("Please enter number of Bulbs you want to connect: ");
            int bulbs = int.Parse(Console.ReadLine());
            for (int i = 1; i <= bulbs; i++)
            {
                // Add Appliance
                int deviceId = appliances.Count() + 1;
                Appliance appliance = new Appliance();
                appliance.Id = deviceId;
                appliance.DeviceType = DeviceType.Bulb;
                appliance.DeviceName = $"Bulb {i}";
                appliances.Add(appliance);

                // Add SWITCH
                switchboard.AddSwitch(deviceId);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            AddAppliances();
        }
    }
}
