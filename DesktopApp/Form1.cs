namespace DesktopApp
{
    public partial class Form1 : Form
    {
        protected List<Human> Humans { get; set; } = new List<Human>();
        protected int CurrentIndex { get;set; } = 0;

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

            FillData();
        }

        private void FillData()
        {
            if (Humans.Count == 0)
                return;

            // cand trecem de dimensiunea colectiei o luam de la inceput
            if (CurrentIndex > Humans.Count - 1)
                CurrentIndex = 0;

            var human = Humans[CurrentIndex];
            txtFirstname.Text = human.FirstName;
            txtLastName.Text = human.LastName;
            txtDogBreed.Text = human.DogBreed;
            pbDogImage.ImageLocation = human.DogImage;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            CurrentIndex++;
            FillData();
        }
    }
}