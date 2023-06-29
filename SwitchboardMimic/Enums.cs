using System.ComponentModel;
using System.Reflection;

namespace SwitchboardMimic
{
    public enum DeviceType
    {
        [Description("Fan")]
        Fan = 1,

        [Description("AC")]
        AC = 2,

        [Description("Bulb")]
        Bulb = 3
    }

    public enum State
    {
        [Description("OFF")]
        Off = 0,

        [Description("ON")]
        On = 1
    }
}

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        FieldInfo field = value.GetType().GetField(value.ToString());
        DescriptionAttribute attribute = field.GetCustomAttribute<DescriptionAttribute>();
        return attribute.Description;
    }
}
