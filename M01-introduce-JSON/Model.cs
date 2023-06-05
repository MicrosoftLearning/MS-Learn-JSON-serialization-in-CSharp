using System.Text.Json;
using System.Text.Json.Serialization;

namespace M01_introduce_JSON
{
    internal class Model
    {
        private readonly List<Character> characters;

        public Model() 
        { 
            characters = new List<Character>();
            ParseCharacters();
            SerializeCharacterTest();
        }

        private void ParseCharacters()
        {
            string jsonString = File.ReadAllText("Characters.json");
            JsonSerializerOptions options = new() { 
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

        public void SerializeCharacterTest()
        {
            Ability ability = new Ability() { Accuracy = 1, Name = "Glimmer", Effect = "Something", Element = "Light" };
            Character character = new Character();
            character.Name = "Reina";
            character.Level = 12;
            character.Image = "rogue02";
            character.Abilities = new[] { ability }; 
            character.Resistance = new[] { "Ice", "Water", "Lightning"};

            string fileName = "SerializedCharacter.json";
            JsonSerializerOptions options = new() { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(new[] { character, character }, options);
            File.WriteAllText(fileName, jsonString);
        }

    }
}
