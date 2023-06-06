namespace M02_Implement_Serialization
{
    public partial class Form1 : Form
    {
        private readonly ImageList imageList = new();
        private readonly Model model = new();

        public Form1()
        {
            InitializeComponent();
            imageList.ImageSize = new Size(300, 300);
            listView1.LargeImageList = imageList;
            imageList.ColorDepth = ColorDepth.Depth16Bit;
            PopulateCharacters();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
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

        private void PopulateCharacters()
        {
            foreach (Character character in model.GetCharacters())
            {
                if (character != null && character.ProfileIcon != null)
                {
                    imageList.Images.Add(character.ProfileIcon);
                    listView1.Items.Add(new ListViewItem(character.Name, imageList.Images.Count - 1) { Tag = character });
                }
            }
        }
    }
}