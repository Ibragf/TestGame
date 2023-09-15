using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        public LoginForm(Form form)
        {
            InitializeComponent();
            form.Close();
        }

        private async void LoginButton_Click(object sender, EventArgs e)
        {
            LoginButton.Enabled = false;
            var name = UserNameTextBox.Text;
            var password = PasswordTextBox.Text;

            using(var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync($"http://localhost:7071/api/login?name={name}&password={password}");
                    response.EnsureSuccessStatusCode();

                    var content = await response.Content.ReadAsStringAsync();
                    GeneralData.Token = content;

                    var gameForm = new GameForm(this);
                    gameForm.Show();
                }
                catch(HttpRequestException ex)
                {
                    MessageBox.Show("Неыерный логин или пароль");
                }
            }

            LoginButton.Enabled = true;
        }
    }
}
