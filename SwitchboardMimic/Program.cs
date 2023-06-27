using SwitchboardMimic;

public class Program
{
    static Switchboard switchboard = new Switchboard();
    static List<Appliance> appliances = new List<Appliance>();
    public static void Main(string[] args)
    {
        InitialSetup();
        DisplayMainMenu();
    }

    public static void DisplayMainMenu()
    {
        while (true)
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine("Choose an appliance from the menu to operate: ");
                int count = 1;
                appliances.ForEach(item =>
                {
                    Switch sch = switchboard.GetSwitch(item.Id);
                    if ((sch.State == SwitchState.On && item.State == DeviceState.Active) || (sch.State == SwitchState.Off && item.State == DeviceState.InActive))
                    {
                        string state = sch.State == SwitchState.On ? "ON" : "OFF";
                        Console.WriteLine($"{count++}.{item.DeviceName} is {state}");
                    }

                    else
                    {
                        Console.WriteLine($"There is inconsistency b/w device and switch. check connection for device{item.Id}");
                    };
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
                        if ((sch.State == SwitchState.On && appliance.State == DeviceState.Active) || (sch.State == SwitchState.Off && appliance.State == DeviceState.InActive))
                        {
                            ShowSubMenu(appliance.DeviceName, sch.DeviceId, sch.State);
                        }
                        else
                        {
                            Console.WriteLine($"There is inconsistency b/w device and switch.");
                        }
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
        Appliance appliance = appliances.SingleOrDefault(x => x.Id == deviceId);
        if(option == 1 && state == SwitchState.On)
        {
            switchboard.Toggle(deviceId, SwitchState.Off);
            appliance.State = DeviceState.InActive;
        }
        else if(option == 1 && state == SwitchState.Off)
        {
            switchboard.Toggle(deviceId, SwitchState.On);
            appliance.State = DeviceState.Active;
        }
        else if(option == 2)
        {
            DisplayMainMenu();
        }
        else
        {
            Console.WriteLine("invalid selection");
            ShowSubMenu(deviceName, deviceId, state);
        }
    }
    public static void AddDevice(int deviceId, string deviceName, DeviceType deviceType)
    {        
        Appliance appliance = new Appliance();
        appliance.Id = deviceId;
        appliance.DeviceName = deviceName;
        appliance.DeviceType = deviceType;
        appliance.State = DeviceState.InActive;
        appliances.Add(appliance);
    }
    public static void InitialSetup()
    {
        try
        {
            Console.WriteLine("Please enter number of fans you want to connect: ");
            int fans = int.Parse(Console.ReadLine());

            // Add Fans
            for (int i = 1; i <= fans; i++)
            {
                int deviceId = appliances.Count() + 1;
                string deviceName = $"Fan{i}";
                AddDevice(deviceId, deviceName, DeviceType.Fan);

                // Add SWITCH
                switchboard.AddSwitch(deviceId);

            }

            // Add ACs
            Console.WriteLine("Please enter number of ACs you want to connect: ");
            int ac = int.Parse(Console.ReadLine());
            for (int i = 1; i <= ac; i++)
            {
                int deviceId = appliances.Count() + 1;
                string deviceName = $"AC{i}";
                AddDevice(deviceId, deviceName, DeviceType.AC);

                // Add SWITCH
                switchboard.AddSwitch(deviceId);
            }

            // Add bulbs
            Console.WriteLine("Please enter number of Bulbs you want to connect: ");
            int bulbs = int.Parse(Console.ReadLine());
            for (int i = 1; i <= bulbs; i++)
            {
                int deviceId = appliances.Count() + 1;
                string deviceName = $"Bulb{i}";
                AddDevice(deviceId, deviceName, DeviceType.Bulb);

                // Add SWITCH
                switchboard.AddSwitch(deviceId);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            InitialSetup();
        }
    }
}
