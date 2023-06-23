namespace SwitchboardMimic
{
    public class Switchboard
    {
        public int Id;
        public List<Switch> Switches = new List<Switch>();


        public void AddSwitch(int deviceId)
        {
            try
            {
                // Add new switch
                Switch sch = new Switch();
                sch.Id = Switches.Count() + 1;
                sch.DeviceId = deviceId;
                sch.State = SwitchState.Off;
                Switches.Add(sch);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public void Toggle(int deviceId, SwitchState state)
        {
            try
            {
                // Change state to ON/OFF
                Switch sch = Switches.SingleOrDefault(item => item.DeviceId == deviceId);
                if (sch == null)
                {
                    Console.WriteLine("invalid switch");
                }
                else
                {
                    sch.State = state;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

        }

        public Switch GetSwitch(int deviceId)
        {
            try
            {
                // Get Single switch using Device Id
                Switch sch = Switches.SingleOrDefault(item => item.DeviceId == deviceId);
                if (sch == null)
                {
                    Console.WriteLine("invalid switch");
                }
                return sch;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}