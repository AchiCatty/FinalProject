using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserForm.Client;

namespace UserForm
{
    public partial class Login : Form
    {
        int CheckId = 0;
        int CheckPwd = 0;
        int CustomerId;
        public Login()
        {
            InitializeComponent();
        }

        private void LoginBtn(object sender, EventArgs e)
        {
            var customerList = UserClient.CustomersClient.GetCustomersAsync().Result;

            var findCustomer = from x in customerList
                               select new { 
                                    x.LoginId,
                                    x.Password,
                                    x.CustomerId
                               };

            foreach (var item in findCustomer)
            {
                if(item.LoginId == teId.Text )
                {
                    CheckId = 1;
                    CustomerId = item.CustomerId;
                }
                else if(item.Password == tePwd.Text)
                {
                    CheckPwd = 1;
                }
            }

            if(CheckId ==1 && CheckPwd ==1)
            {
                MessageBox.Show("로그인 성공");

                Selection form = new Selection(CustomerId);
                form.Show();
            }
            else if(CheckId == 1 && CheckPwd == 0)
            {
                MessageBox.Show("비밀번호가 틀렸습니다");
                CheckId = 0;
                teId.Text = null;
                tePwd.Text = null;
            }
            else if (CheckId == 0)
            {
                MessageBox.Show("ID가 없습니다");
                CheckPwd = 0;
                teId.Text = null;
                tePwd.Text = null;
            }


        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void KeyBoard_Click(object sender, EventArgs e)
        {

        }

        private void idtxtBox_Click(object sender, EventArgs e)
        {

        }

        private void idtxtBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void pwtxtBox_Click(object sender, EventArgs e)
        {

        }

        private void Loginbtn_Click(object sender, EventArgs e)
        {

        }

        private void SignUpbtn_Click(object sender, EventArgs e)
        {

        }

        private void button35_Click(object sender, EventArgs e)
        {

        }

        private void backspacebtn_Click(object sender, EventArgs e)
        {

        }
    }
}
