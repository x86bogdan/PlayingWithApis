using ClassLibrary1;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnFetchDog_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
            DogApi api = new DogApi();
            DogApiResponse result = api.GetRandomDog("affenpinscher");
            if (result != null)
            {
                pbDogImage.ImageLocation = result.Message;
            }
        }
    }
}