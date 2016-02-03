using PainLogUWP.Attributes;

namespace PainLogUWP.Enums
{
    public class BodyPart
    {
        public string Head = Windows.ApplicationModel.Resources.Core.ResourceManager.Current.MainResourceMap.GetValue("Resources/BodyPart_Head").ValueAsString;
        public static string Leg;
        public static string Arm;
        public static string Back;
        public static string Stomach;
    }
}