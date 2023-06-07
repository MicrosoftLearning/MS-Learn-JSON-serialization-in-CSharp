using System.Text.Json.Serialization;
using System.Text.Json;
using static System.Windows.Forms.Design.AxImporter;

namespace M04_Challenge_Project
{
    public class Model
    {
        //Save character data
        //Load character data
        //Load default characters

        private readonly List<Character> characters;
        private readonly List<Character> party;

        public Model()
        {
            characters = new List<Character>();
            party = new List<Character>();
            InitCharacters();
            DeserializeCharacters();
        }
        
        public List<Character> GetCharacters()
        {
            return characters;
        }

        public List<Character> GetParty()
        {
            return party;
        }

        public void AddCharacterToParty(string jsonString)
        {
            JsonSerializerOptions options = new()
            {
                Converters = { new JsonStringEnumConverter() },
                PropertyNameCaseInsensitive = true
            };
            Character? data = JsonSerializer.Deserialize<Character>(jsonString, options);
            if (data != null)
            {
                party.Add(data);
            }
        }

        private void InitCharacters()
        {

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

        public static void SerializeCharacter(Character character)
        {
            string fileName = $"{character.Name}.json";
            JsonSerializerOptions options = new() { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(character, options);
            File.WriteAllText(fileName, jsonString);
        }
    }
}
