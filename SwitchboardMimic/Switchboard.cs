using SwitchboardMimic;

namespace SwitchboardMimic
{
    public class Switchboard
    {
        public int Id;
        public List<Switch> Switches = new List<Switch>();

        public void LinkSwitch(int deviceId)
        {
            try
            {
                // Add new switch
                int id = Switches.Count() + 1;
                Switch sch = new Switch(id, deviceId, State.Off);
                Switches.Add(sch);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public void Toggle(int id, State switchState)
        {
            try
            {
                // Change state to ON/OFF
                Switch sch = Switches.SingleOrDefault(item => item.Id == id);
                if (sch == null)
                {
                    Console.WriteLine("invalid switch");
                }
                else
                {
                    sch.SwitchState = switchState;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

        }

        public Switch GetSwitch(int id)
        {
            try
            {
                // Get Single switch
                Switch sch = Switches.SingleOrDefault(item => item.Id == id);
                if (sch == null)
                {
                    Console.WriteLine("invalid data");
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
