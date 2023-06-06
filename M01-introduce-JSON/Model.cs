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
    }
}
