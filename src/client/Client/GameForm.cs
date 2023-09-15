using Client.Responses;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class GameForm : Form
    {
        public GameForm(LoginForm form)
        {
            InitializeComponent();
            ShowStatisticsAsync();
            form.Hide();
        }

        private async void GuessButton_Click(object sender, EventArgs e)
        {
            GuessButton.Enabled = false;
            using(var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GeneralData.Token);
                    var response = await client.GetAsync($"http://localhost:7071/api/games/target/guess?value={GuessTextBox.Text}");
                    response.EnsureSuccessStatusCode();

                    var content = await response.Content.ReadAsStringAsync();
                    var guessResponse = JsonConvert.DeserializeObject<GuessResponse>(content);

                    switch(guessResponse.GuessingResult)
                    {
                        case GuessingResult.Less:
                            GuessResultLabel.Text = "Меньше";
                            break;
                        case GuessingResult.More:
                            GuessResultLabel.Text = "Больше";
                            break;
                        case GuessingResult.Guessed:
                            GuessResultLabel.Text = "Угадали";
                            ShowStatisticsAsync();
                            break;
                    }

                    GuessResultLabel.Visible = true;
                }
                catch(HttpRequestException ex)
                {
                    MessageBox.Show("Ошибка валидации. Значение должно быть между 0 и 100");
                }
            }

            GuessButton.Enabled = true;
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            GeneralData.Token = null;

            var loginForm = new LoginForm(this);
            loginForm.Show();
        }

        private async Task ShowStatisticsAsync()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GeneralData.Token);
                    var response = await client.GetAsync("http://localhost:7071/api/games/count");
                    response.EnsureSuccessStatusCode();

                    var content = await response.Content.ReadAsStringAsync();
                    var gamesCountResponse = JsonConvert.DeserializeObject<GamesCountResponse>(content);

                    UserNameLabel.Text = gamesCountResponse.UserName;
                    GamesCountLabel.Text =$"Игр сыграно: {gamesCountResponse.GamesCount}";

                    UserNameLabel.Visible = true;
                    GamesCountLabel.Visible = true;
                }
                catch (HttpRequestException ex)
                {

                }
            }
        }
    }
}
