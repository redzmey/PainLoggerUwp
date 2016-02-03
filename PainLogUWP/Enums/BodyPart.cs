using Windows.ApplicationModel.Resources;
using PainLogUWP.Attributes;

namespace PainLogUWP.Enums
{
    public class BodyPart
    {
        private static readonly ResourceLoader _loader = new ResourceLoader();

        public static string Head => _loader.GetString("Head");
        public static string Leg => _loader.GetString("Leg");
        public static string Arm => _loader.GetString("Arm");
        public static string Back => _loader.GetString("Back");
        public static string Stomach => _loader.GetString("Stomach");
    }
}