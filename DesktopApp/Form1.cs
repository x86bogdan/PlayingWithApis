namespace DesktopApp
{
    public partial class Form1 : Form
    {
        public List<Human> Humans { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void btnCallApi_Click(object sender, EventArgs e)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7189/");
            var response = client.GetAsync($"human").Result;
            var result = response.Content.ReadAsStringAsync().Result;
            Humans = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Human>>(result) ?? new List<Human>();
            
            var human = Humans.FirstOrDefault();

            txtFirstname.Text = human.FirstName;
            txtLastName.Text = human.LastName;
            txtDogBreed.Text = human.DogBreed;
            pbDogImage.ImageLocation = human.DogImage;

        }

        private void btnNext_Click(object sender, EventArgs e)
        {

        }
    }
}