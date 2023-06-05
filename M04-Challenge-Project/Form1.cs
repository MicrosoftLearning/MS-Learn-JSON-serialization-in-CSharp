namespace M04_Challenge_Project
{
    public partial class Form1 : Form
    {
        CharacterForm form2 = new CharacterForm();

        public Form1()
        {
            InitializeComponent();
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            form2.Show();
        }
    }
}