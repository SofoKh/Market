using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperMarket2
{
    public partial class Form1 : Form
    {
        List<User> Users = new List<User>();

        public Form1()
        {
            InitializeComponent();
        }

       

        private void SignUp_Click(object sender, EventArgs e)
        {
            if (SignUpName.Text == "" || SignUpSurname.Text == "" || SignUpEmail.Text == "" || SignUpPassword.Text == "" || SignUpAge.Text == "" || SignUpAddress.Text == "" || SignUpPhoneNumber.Text == "")
            {
                MessageBox.Show("Please,don't leave anything blank");
            }
            if (!SignUpEmail.Text.Contains("@"))
            {
                MessageBox.Show("Email does not exist");
            }
            if(SignUpPhoneNumber.Text.Length != 9)
            {
                MessageBox.Show("Phone Number is invalid");
            }
            if(SignUpPassword.Text.Length < 8 || SignUpPassword.Text.Length > 20)
            {
                MessageBox.Show("Please change your password");
            }
            else
            { 
            SQLprocedures.InsertUsers(SignUpName.Text, SignUpSurname.Text, SignUpEmail.Text, SignUpPassword.Text, Convert.ToInt32(SignUpAge.Text), SignUpAddress.Text, Convert.ToInt32(SignUpPhoneNumber.Text));
            MessageBox.Show("Account created successfully:)");
                SignUpAddress.Clear();
                SignUpAge.Clear();
                SignUpEmail.Clear();
                SignUpName.Clear();
                SignUpPassword.Clear();
                SignUpPhoneNumber.Clear();
                SignUpSurname.Clear();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            SignUpPassword.PasswordChar = '*';
            LogInPassword.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void LoadUsers()
        {
            DataTable table = SQLprocedures.SelectUsers();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                var row = table.Rows[i];
                User user = new User();
                user.ID = Convert.ToInt32(row["UserID"]);
                user.Name = Convert.ToString(row["UserName"]);
                user.Surname = Convert.ToString(row["UserSurname"]);
                user.Email = Convert.ToString(row["UserEmail"]);
                user.Password = Convert.ToString(row["UserPassword"]);
                user.Age = Convert.ToInt32(row["UserAge"]);
                user.Address = Convert.ToString(row["UserAddress"]);
                user.PhoneNumber = Convert.ToInt32(row["UserPhoneNumber"]);
                Users.Add(user);

            }
        }
        private void LogIn_Click(object sender, EventArgs e)
        {
            bool isLoggedIn = false;
            LoadUsers();
            foreach(User user in Users)
            {
                if(user.Email == LogInEmail.Text && user.Password == LogInPassword.Text)
                {
                    isLoggedIn = true;
                    MessageBox.Show("Logged in successfully!");
                    ShopForm shop = new ShopForm();
                    shop.Show();
                    this.Hide();
                    ///forAdmin
                }
            }
            if(!isLoggedIn)
            {
                MessageBox.Show("Email or Password incorrrect");
            }
        }
    }
}
