using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CreateBtnList();

        }

        // 사용하는 리스트
        private List<Button> _buttons = new List<Button>();

        // CreateLabelList - _labels를 만들기 위해 reflection 하는 메서드
        private void CreateBtnList()
        {
            Type type = GetType();
            FieldInfo[] fieldInfos =
            type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var info in fieldInfos)
            {
                Button btn = info.GetValue(this) as Button;
                if (btn == null)
                    continue;
                if (btn.Tag == "1")
                    continue;
                _buttons.Add(btn);
            }
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

        private void ClickKeyboard(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            switch (btn.Text)
            {
                case "backspace":
                    break;
                case "CLock":
                    ChangeKey();
                    break;
                case "한/영":
                    break;
                default:
                    DoWrite(btn);
                    break;
            }

            
        }

        private void DoWrite(Button btn)
        {
            char btnText = Convert.ToChar(btn.Text);

            // 아스키코드 A~Z 65~90
            if (btnText > 64 && btnText < 91)
            {

            }
            // 아스키코드 a~z 97~122
            else if (btnText > 96 && btnText < 123)
            {

            }
            //else if()
            //{

            //}
        }

        private void ChangeKey()
        {
            char check = Convert.ToChar(_buttons[0].Text);
            int sum = 0;
            if(check >95)
            {
                sum = -32;
            }
            else
            {
                sum = +32;
            }

            for (int i = 0; i < _buttons.Count; i++)
            {
                check = Convert.ToChar(_buttons[i].Text);
                check = Convert.ToChar(check + sum);
                _buttons[i].Text = Convert.ToString(check);
            }

        }
    }
}
