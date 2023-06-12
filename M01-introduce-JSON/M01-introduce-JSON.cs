using System.Windows.Forms;

namespace M01_introduce_JSON
{
    public partial class Form1 : Form
    {
        private readonly ImageList imageList = new();
        private readonly Model model = new();

        public Form1()
        {
            InitializeComponent();
            imageList.ImageSize = new Size(220, 220);
            listView1.LargeImageList = imageList;
            listView1.View = View.Tile;
            listView1.TileSize = new Size(230, 230);
            imageList.ColorDepth = ColorDepth.Depth16Bit;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
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

                tableLayoutPanel1.Controls.Clear();
                tableLayoutPanel2.Controls.Clear();
                tableLayoutPanel3.Controls.Clear();
                tableLayoutPanel4.Controls.Clear();
                tableLayoutPanel5.Controls.Clear();

                tableLayoutPanel1.Controls.Add(new Label() { Text = "Lvl", AutoSize = true }, 0, 0);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "HP", AutoSize = true }, 1, 0);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "Atk", AutoSize = true }, 2, 0);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "Def", AutoSize = true }, 3, 0);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "Spd", AutoSize = true }, 4, 0);

                tableLayoutPanel1.Controls.Add(new Label() { Text = $"{selectedCharacter.Level}", AutoSize = true }, 0, 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = $"{selectedCharacter.Stats?.HP}", AutoSize = true }, 1, 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = $"{selectedCharacter.Stats?.Atk}", AutoSize = true }, 2, 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = $"{selectedCharacter.Stats?.Def}", AutoSize = true }, 3, 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = $"{selectedCharacter.Stats?.Spd}", AutoSize = true }, 4, 1);

                if (selectedCharacter.Abilities != null && selectedCharacter.Abilities.Length > 0)
                {
                    label8.Text = $"Abilities: ";
                    int row = 0;

                    foreach (Ability ability in selectedCharacter.Abilities)
                    {
                        Label label = new()
                        {
                            Text = $"{ability.Name}",
                            AutoSize = true
                        };
                        tableLayoutPanel2.Controls.Add(label, 0, row++);
                        ToolTip tip = new();
                        tip.SetToolTip(label, ability.Effect);
                    }
                }

                if (selectedCharacter.Resistance != null && selectedCharacter.Resistance.Length > 0)
                {
                    tableLayoutPanel4.ColumnCount = selectedCharacter.Resistance.Length;
                    label9.Text = "Resistances: ";
                    int col = 0;

                    foreach (string resistance in selectedCharacter.Resistance)
                    {
                        tableLayoutPanel4.Controls.Add(new Label() { Text = resistance, AutoSize = true }, col++, 0);
                    }
                }

                if (selectedCharacter.Weakness != null && selectedCharacter.Weakness.Length > 0)
                {
                    tableLayoutPanel5.ColumnCount = selectedCharacter.Weakness.Length;
                    label10.Text = "Weaknesses: ";
                    int col = 0;

                    foreach (string weakness in selectedCharacter.Weakness)
                    {
                        tableLayoutPanel5.Controls.Add(new Label() { Text = weakness, AutoSize = true }, col++, 0);
                    }
                }

                label11.Text = $"Affinity";
                if (selectedCharacter.Affinity != null)
                {
                    tableLayoutPanel3.Controls.Add(new Label() { Text = selectedCharacter.Affinity, AutoSize = true });
                }

                pictureBox1.Image = selectedCharacter.FullImage;
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