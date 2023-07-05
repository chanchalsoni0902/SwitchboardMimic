using SwitchboardMimic;

public class Program
{
    static Switchboard switchboard = new Switchboard();
    static List<Appliance> appliances = new List<Appliance>();
    public static void Main()
    {
        // get input from user for no. of appliances
        InitialSetup();

        while (true)
        {
            try
            {
                DisplayMenu();
                DoOperation();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public static void DisplayMenu()
    {
        Console.WriteLine("\nChoose from the option: ");
        switchboard.Switches.ForEach(sch =>
        {
            Appliance appliance = appliances.SingleOrDefault(device => device.Id == sch.DeviceId);
            if (appliance.DeviceState == sch.SwitchState)
            {
                Console.WriteLine($"{sch.Id}.{appliance.DeviceName} is {sch.SwitchState.ToString()}");
            }
            else
            {
                Console.WriteLine("There is inconsistency b/w device and switch. Check your connection.");
            }
        });
        Console.WriteLine("Press 0 for exit");
        Console.WriteLine();
    }

    public static void DoOperation()
    {
        try
        {
            int switchId = int.Parse(Console.ReadLine());
            Console.WriteLine();

            if (switchId == 0)
            {
                Environment.Exit(0);
            }
            else
            {
                Switch sch = switchboard.GetSwitch(switchId);
                Appliance appliance = appliances.SingleOrDefault(x => x.Id == sch.DeviceId);
                if (appliance == null)
                {
                    Console.WriteLine("device not found");
                }
                else
                {
                    if ((sch.SwitchState == appliance.DeviceState))
                    {
                        ShowSubMenu(appliance.DeviceName, sch.DeviceId, sch.SwitchState);
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
            Console.WriteLine($"{ex.Message}");
        }
    }

    public static void ShowSubMenu(string deviceName, int deviceId, State state)
    {
        Console.WriteLine();
        if (state == State.On)
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
        if (option == 1 && state == State.On)
        {
            switchboard.Toggle(deviceId, State.Off);
            appliance.DeviceState = State.Off;
        }
        else if (option == 1 && state == State.Off)
        {
            switchboard.Toggle(deviceId, State.On);
            appliance.DeviceState = State.On;
        }
        else if (option == 2)
        {
            // To get back to menu
            DisplayMenu();
        }
        else
        {
            Console.WriteLine("invalid selection");
            ShowSubMenu(deviceName, deviceId, state);
        }
    }

    public static void AddDevice(int deviceId, string deviceName, DeviceType deviceType)
    {
        Appliance appliance = new Appliance(deviceId, deviceType, deviceName, State.Off);
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

                //  SWITCH
                switchboard.LinkSwitch(deviceId);

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
                switchboard.LinkSwitch(deviceId);
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
                switchboard.LinkSwitch(deviceId);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            InitialSetup();
        }
    }
}
