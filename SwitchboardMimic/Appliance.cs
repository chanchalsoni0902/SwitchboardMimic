namespace SwitchboardMimic
{
    public class Appliance
    {
        public int Id;
        public DeviceType DeviceType;
        public string DeviceName;
        public State DeviceState;

        public Appliance(int id, DeviceType deviceType, string deviceName, State deviceState)
        {
            Id = id;
            DeviceType = deviceType;
            DeviceName = deviceName;
            DeviceState = deviceState;
        }
    }
}