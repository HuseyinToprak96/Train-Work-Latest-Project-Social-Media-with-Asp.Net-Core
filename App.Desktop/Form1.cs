using App.Desktop.ApiHandler;
using App.Desktop.Dtos;
using App.Desktop.Helpers;
using Newtonsoft.Json;

namespace App.Desktop
{
    public partial class Form1 : Form
    {
        private readonly RestApiHandler _restApiHandler = new RestApiHandler("http://localhost:39276/api/");
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            LoginDto loginDto = new LoginDto { Email = txt_username.Text, Password = txt_password.Text };
            var result = _restApiHandler.PostAsync("Auth/CreateToken", loginDto, "").Result;
            var response = JsonConvert.DeserializeObject<CustomResponseDto<TokenDto>>(result);
            Authorize.Token = response.Data.AccessToken;
            pnl_Login.Visible = false;
            var userList = JsonConvert.DeserializeObject<CustomResponseDto<List<UserAppDto>>>(_restApiHandler.GetAsync("ManagerUser/GetAll", Authorize.Token).Result);
            var panel = new Panel();
            panel.Width = this.Width; panel.Height = this.Height;
            panel.Top = 200;
            panel.Left = 50;
            panel.Width = 600;
            DataGridView dataGridView = new DataGridView();
            dataGridView.Width = this.Width; dataGridView.Height = this.Height;
            dataGridView.DataSource = userList.Data;
            panel.Controls.Add(dataGridView);
            this.Controls.Add(panel);
        }
    }
}