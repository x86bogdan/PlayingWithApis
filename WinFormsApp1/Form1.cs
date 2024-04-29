using ClassLibrary1;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private readonly DogApi _dogApi;
        public Form1()
        {
            InitializeComponent();
            _dogApi = new DogApi();
        }

        private void btnFetchDog_Click(object sender, EventArgs e)
        {
            DogApiResponse result = _dogApi.GetRandomDog("affenpinscher");
            if (result != null)
            {
                pbDogImage.ImageLocation = result.Message;
            }
        }

        private async void btnFetchDogAsync_Click(object sender, EventArgs e)
        {
            DogApiResponse result = await _dogApi.GetRandomDogAsync("affenpinscher");
            if (result != null)
            {
                pbDogImage.ImageLocation = result.Message;
            }
        }
    }
}