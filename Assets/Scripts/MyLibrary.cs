using System.Xml;

namespace MyLibrary
{
    public class MyObject
    {
        static XmlWriterSettings settings = new();
        public static XmlWriterSettings Settings
        {
            get
            {
                settings.Indent = true;
                settings.OmitXmlDeclaration = true;
                settings.NewLineOnAttributes = true;

                return settings;
            }

            set => settings = value;
        }
    }

        public class Character
    {
        public int Health = 3, MaxHealth = 3;
        public bool Key;
    }

    public class Options
    {
        public string Language = "English";
        public bool FirstStartGame = true;
    }
}