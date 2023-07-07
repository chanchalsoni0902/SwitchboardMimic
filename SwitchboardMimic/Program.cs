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
                WriteLine(ex.Message);
            }
        }
    }

    public static void DisplayMenu()
    {
        WriteLine("\nChoose from the option: ");
        switchboard.Switches.ForEach(sch =>
        {
            Appliance appliance = appliances.SingleOrDefault(device => device.Id == sch.DeviceId);
            if (appliance.DeviceState == sch.SwitchState)
            {
                WriteLine($"{sch.Id}.{appliance.DeviceName} is {sch.SwitchState.ToString()}");
            }
            else
            {
                WriteLine("There is inconsistency b/w device and switch. Check your connection.");
            }
        });
        WriteLine("Press 0 for exit");
        WriteLine();
    }

    public static void DoOperation()
    {
        try
        {
            int switchId = int.Parse(Console.ReadLine());

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
                    WriteLine("device not found");
                }
                else
                {
                    if ((sch.SwitchState == appliance.DeviceState))
                    {
                        ShowSubMenu(appliance.DeviceName, sch.DeviceId, sch.SwitchState);
                    }
                    else
                    {
                        WriteLine($"There is inconsistency b/w device and switch.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            WriteLine($"{ex.Message}");
        }
    }

    public static void ShowSubMenu(string deviceName, int deviceId, State state)
    {
        WriteLine();
        State deviceSwitch = state == State.On ? State.Off : State.On;
        WriteLine($"1. Switch {deviceName} {deviceSwitch}");
        WriteLine($"2. Back");

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
            WriteLine("invalid selection");
            ShowSubMenu(deviceName, deviceId, state);
        }
    }

    public static void AddDevice(int deviceId, string deviceName, DeviceType deviceType)
    {
        appliances.Add(new Appliance(deviceId, deviceType, deviceName, State.Off));
        //Linking switch with the device (it is mandatory thats why we are linking here)
        switchboard.LinkSwitch(deviceId);
    }

    public static void InitialSetup()
    {
        try
        {
            WriteLine("Please enter number of fans you want to connect: ");
            int fans = int.Parse(Console.ReadLine());

            // Add Fans
            for (int i = 1; i <= fans; i++)
            {
                AddDevice(appliances.Count() + 1, $"Fan{i}", DeviceType.Fan);
            }

            // Add ACs
            WriteLine("Please enter number of ACs you want to connect: ");
            int ac = int.Parse(Console.ReadLine());
            for (int i = 1; i <= ac; i++)
            {
                AddDevice(appliances.Count() + 1, $"AC{i}", DeviceType.AC);
            }

            // Add bulbs
            WriteLine("Please enter number of Bulbs you want to connect: ");
            int bulbs = int.Parse(Console.ReadLine());
            for (int i = 1; i <= bulbs; i++)
            {
                AddDevice(appliances.Count() + 1, $"Bulb{i}", DeviceType.Bulb);
            }
        }
        catch (Exception ex)
        {
            WriteLine(ex.Message);
            InitialSetup();
        }
    }

    static void WriteLine(string message = "")
    {
        Console.WriteLine(message);
    }
}
