using System.Text.Json;
using System.Text.Json.Serialization;

namespace M02_Implement_Serialization
{
    internal class Model
    {
        private readonly List<Character> characters;

        public Model()
        {
            characters = new List<Character>();
            InitCharacters();
            SerializeCharacters();
        }

        private void DeserializeCharacters()
        {
            string jsonString = File.ReadAllText("Characters.json");
            JsonSerializerOptions options = new()
            {
                Converters = { new JsonStringEnumConverter() },
                PropertyNameCaseInsensitive = true
            };
            if (jsonString != null && jsonString.Length > 0)
            {
                if (jsonString[0] == '[')
                {
                    Character[]? data = JsonSerializer.Deserialize<Character[]>(jsonString, options);
                    if (data != null)
                    {
                        characters.AddRange(data);
                    }
                }
                else
                {
                    Character? data = JsonSerializer.Deserialize<Character>(jsonString, options);
                    if (data != null)
                    {
                        characters.Add(data);
                    }
                }
            }
        }

        public List<Character> GetCharacters()
        {
            return characters;
        }

        private void InitCharacters()
        {
            Ability ability = new() { Accuracy = 1, Name = "Glimmer", Effect = "Something", Element = "Light" };
            Character character = new()
            {
                Name = "Reina",
                Level = 12,
                Image = "rogue02",
                Abilities = new[] { ability },
                Resistance = new[] { "Ice", "Water", "Lightning" }
            };
            characters.Add(character);
        }

        private void SerializeCharacters()
        {
            string fileName = "SerializedCharacter.json";
            JsonSerializerOptions options = new() { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(characters, options);
            File.WriteAllText(fileName, jsonString);
        }
    }
}
