using System.Windows.Forms;

namespace M04_Challenge_Project
{
    public partial class Form1 : Form
    {
        private readonly CharacterForm form2 = new();
        private readonly Model model = new();
        private readonly ImageList imageList = new();

        public Form1()
        {
            InitializeComponent();

            imageList.ImageSize = new Size(300, 300);
            listView1.LargeImageList = imageList;
            imageList.ColorDepth = ColorDepth.Depth16Bit;
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            form2.Show();
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog characterFileSelect = new();
            characterFileSelect.InitialDirectory = Directory.GetCurrentDirectory();
            characterFileSelect.Filter = "JSON files (*.json)|*.json";

            DialogResult result = characterFileSelect.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = characterFileSelect.FileName;
                string jsonString = File.ReadAllText(file);
                model.AddCharacterToParty(jsonString);

                Character character = model.GetParty().Last();
                if (character != null && character.ProfileIcon != null)
                {
                    imageList.Images.Add(character.ProfileIcon);
                    listView1.Items.Add(new ListViewItem(character.Name, imageList.Images.Count - 1) { Tag = character });
                }
            }
        }

        private void PopulateData()
        {
            if (listView1.SelectedItems.Count == 1)
            {
                Character selectedCharacter = (Character)listView1.SelectedItems[0].Tag;

                label1.Text = $"Name: {selectedCharacter.Name}";
                label2.Text = $"Class: {selectedCharacter.Class}";
                label3.Text = $"Lvl: {selectedCharacter.Level}";
                label4.Text = $"HP: {selectedCharacter.Stats?.HP}";
                label5.Text = $"Atk: {selectedCharacter.Stats?.Atk}";
                label6.Text = $"Def: {selectedCharacter.Stats?.Def}";
                label7.Text = $"Spd: {selectedCharacter.Stats?.Spd}";
                label8.Text = $"Abilities: ";
                label9.Text = "Resistances: ";
                label10.Text = "Weaknesses: ";
                label11.Text = $"Affinity: ";

                if (selectedCharacter.Abilities != null && selectedCharacter.Abilities.Length > 0)
                {
                    foreach (Ability ability in selectedCharacter.Abilities)
                    {
                        label8.Text += $"{ability.Name}, ";
                    }

                    label8.Text = label8.Text[..^2];
                }

                if (selectedCharacter.Resistance != null && selectedCharacter.Resistance.Length > 0)
                {
                    foreach (string resitance in selectedCharacter.Resistance)
                    {
                        label9.Text += $"{resitance}, ";
                    }

                    label9.Text = label9.Text[..^2];
                }

                if (selectedCharacter.Weakness != null && selectedCharacter.Weakness.Length > 0)
                {
                    foreach (string weakness in selectedCharacter.Weakness)
                    {
                        label10.Text += $"{weakness}, ";
                    }
                    label10.Text = label10.Text[..^2];
                }

                if (selectedCharacter.Affinity != null)
                {
                    label11.Text += $"{selectedCharacter.Affinity}";
                }

                pictureBox1.Image = selectedCharacter.FullImage;
                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateData();
        }
    }
}