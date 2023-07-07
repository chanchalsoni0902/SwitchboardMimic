namespace SwitchboardMimic
{
    public class Switch
    {
        public int Id;
        public int DeviceId;
        public State SwitchState;

        public Switch(int id, int deviceId, State switchState)
        {
            Id = id;
            DeviceId = deviceId;
            SwitchState = switchState;
        }
    }
}
