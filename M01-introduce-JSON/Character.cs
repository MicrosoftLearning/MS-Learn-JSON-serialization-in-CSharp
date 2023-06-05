using System.Text.Json.Serialization;

namespace M01_introduce_JSON
{
    public class Character
    {
        public string? Name { get; set; }
        public int Level { get; set; }
        public CharacterClass? Class { get; set; }
        public string? Affinity { get; set; }
        public CharacterStats? Stats { get; set; }
        public Ability[]? Abilities { get; set; }
        public string[]? Weakness { get; set; }
        public string[]? Resistance { get; set; }
        public string? Image { get; set; }
        public Bitmap? FullImage
        {
            get
            {
                if (Image == null) 
                    return null;

                return Shared.Properties.Resources.ResourceManager.GetObject(Image) as Bitmap;
            }
        }
        public Bitmap? ProfileIcon 
        {
            get
            {
                if (Image == null) 
                    return null;

                string icon = Image + "icon";
                return Shared.Properties.Resources.ResourceManager.GetObject(icon) as Bitmap;
            }
        }
    }

    public class CharacterStats
    {
        public int HP { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Spd { get; set; }
    }

    public class Ability
    {
        public string? Name { get; set; }
        public string? Effect { get; set; }
        public string? Element { get; set; }
        public double Accuracy { get; set; }
    }

    public enum CharacterClass
    {
        Ranger,
        Mage,
        Cleric,
        Warrior,
        Paladin,
        Summoner,
        Rogue
    }
}