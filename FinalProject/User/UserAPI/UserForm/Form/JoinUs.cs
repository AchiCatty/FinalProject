using DevExpress.XtraEditors;
using MyNamespace;
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
    public partial class JoinUs : Form
    {
        int cursorFlag = 0;
        int keyboardFalg = 0;


        string[][] key = new string[][]
        {
           new string[] {"Q","W","E","R","T","Y","U","I","O","P","A","S","D","F","G","H","J","K","L","Z","X","C","V","B","N","M" },
           new string[] {"1","2","3","4","5","6","7","8","9","0","!","@","#","$","%","^","=","*","(",")",",",".","/","?",";",":" },
           new string[] {"ㅂ","ㅈ","ㄷ","ㄱ","ㅅ","ㅛ","ㅕ","ㅑ","ㅐ","ㅔ","ㅁ","ㄴ","ㅇ","ㄹ","ㅎ","ㅗ","ㅓ","ㅏ","ㅣ","ㅋ","ㅌ","ㅊ","ㅍ","ㅠ","ㅜ","ㅡ" }
        };

        public JoinUs()
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

        private void check_Click(object sender, EventArgs e)
        {
            checkId();
        }

        private void checkId()
        {
            if (teId.Text == null)
            {
                MessageBox.Show("ID를 입력해주세요");
                return;
            }

            var customers = UserClient.CustomersClient.GetCustomersAsync().Result;
            var customersLoginId = from x in customers
                                   select x.LoginId;

            foreach (var item in customersLoginId)
            {
                if (item == teId.Text)
                {
                    MessageBox.Show("중복된 ID입니다");
                    teId.Text = null;
                    return;
                }
            }

            MessageBox.Show("사용가능한 ID입니다");
        }

        private void ClickKeyboard(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            switch (btn.Text)
            {
                case "backspace":
                    RemoveText();
                    break;
                case "CLock":
                    ChangeKey(1);
                    break;
                case "한/영":
                    ChangeKey(2);
                    break;
                case "?123":
                    ChangeKey(3);
                    break;
                default:
                    DoWrite(btn);
                    break;
            }

        }

        private void RemoveText()
        {
            TextEdit textEdit = teId;
            if (cursorFlag == 2)
                textEdit = tePwd;
            else if (cursorFlag == 3)
                textEdit = tePwdCheck;
            else if (cursorFlag == 4)
                textEdit = teName;
            else if (cursorFlag == 5)
                textEdit = tePhoneNumber;

            if (textEdit.SelectedText != "")
                textEdit.Text = textEdit.Text.Remove(textEdit.Text.Length - textEdit.SelectedText.Length, textEdit.Text.Length);
            else if (textEdit.Text.Length > 0)
                textEdit.Text = textEdit.Text.Remove(textEdit.Text.Length - 1, 1);
        }

        private void DoWrite(Button btn)
        {
            TextEdit textEdit = teId;
            if (cursorFlag == 2)
                textEdit = tePwd;
            else if (cursorFlag == 3)
                textEdit = tePwdCheck;
            else if (cursorFlag == 4)
                textEdit = teName;
            else if (cursorFlag == 5)
                textEdit = tePhoneNumber;

            textEdit.Text = textEdit.Text + btn.Text;
            
            if(cursorFlag==3)
            {
                checkPwd();
            }
        }

        private void checkPwd()
        {
            if (tePwd.Text == tePwdCheck.Text)
            {
                labelPwd.Text = "";
            }
            else
            {
                labelPwd.Text = "비밀번호가 틀렸습니다";
            }
        }

        private void ChangeKey(int selectNum)
        {
            char check = Convert.ToChar(_buttons[0].Text);
            int sum = 0;

            // 대소문자
            if (selectNum == 1)
            {
                if (keyboardFalg == 0 || keyboardFalg == 1)
                {
                    if (check > 95)
                        sum = -32;
                    else
                        sum = +32;

                    for (int i = 0; i < _buttons.Count; i++)
                    {
                        check = Convert.ToChar(_buttons[i].Text);
                        check = Convert.ToChar(check + sum);
                        _buttons[i].Text = Convert.ToString(check);
                    }

                    keyboardFalg = 1;
                }
            }
            // 한/영
            else if (selectNum == 2)
            {
                int num = 0;
                // 영어에서 한글
                if (keyboardFalg == 0 || keyboardFalg == 1)
                {
                    num = 2;
                    keyboardFalg = 2;
                }
                // 한글에서 영어
                else if (keyboardFalg == 2)
                {
                    num = 0;
                    keyboardFalg = 1;
                }
                // 기호라서 아무것도 안함
                else if (keyboardFalg == 3)
                    return;

                foreach (var item in _buttons)
                {
                    for (int i = 0; i < _buttons.Count; i++)
                    {
                        if ((Convert.ToInt32(item.Tag) - 2) == i)
                            item.Text = key[num][i];
                    }
                }
            }
            // 기호/숫자
            else if (selectNum == 3)
            {
                int num = 0;
                // 영어에서 기호/숫자로
                if (keyboardFalg == 0 || keyboardFalg == 1)
                {
                    num = 1;
                    keyboardFalg = 3;
                }
                else if (keyboardFalg == 2)
                {
                    return;
                }
                // 기호/숫자에서 영어로
                else if (keyboardFalg == 3)
                {
                    num = 0;
                    keyboardFalg = 1;
                }

                foreach (var item in _buttons)
                {
                    for (int i = 0; i < _buttons.Count; i++)
                    {
                        if ((Convert.ToInt32(item.Tag) - 2) == i)
                            item.Text = key[num][i];
                    }
                }
            }
        }

        private void textEdit_Click(object sender, EventArgs e)
        {
            TextEdit textEdit = sender as TextEdit;
            if (textEdit.Name == "teId")
                cursorFlag = 1;
            else if (textEdit.Name == "tePwd")
                cursorFlag = 2;
            else if (textEdit.Name == "tePwdCheck")
                cursorFlag = 3;
            else if (textEdit.Name == "teName")
                cursorFlag = 4;
            else if (textEdit.Name == "tePhoneNumber")
                cursorFlag = 5;
        }

        private void textEdit_DoubleClick(object sender, EventArgs e)
        {
            TextEdit textEdit = sender as TextEdit;
            if (textEdit.Name == "teId")
                cursorFlag = 1;
            else if (textEdit.Name == "tePwd")
                cursorFlag = 2;
            else if (textEdit.Name == "tePwdCheck")
                cursorFlag = 3;
            else if (textEdit.Name == "teName")
                cursorFlag = 4;
            else if (textEdit.Name == "tePhoneNumber")
                cursorFlag = 5;
            textEdit.SelectAll();
        }

        private void joinBtn_Click(object sender, EventArgs e)
        {
            if (teId.Text == null || tePwd==null||tePwdCheck==null||teName==null||tePhoneNumber==null||cbVip.Text==null)
            {
                MessageBox.Show("비어있는 항목이 있습니다");
                return;
            }

            checkId();
            checkPwd();

            var customerId = UserClient.CustomersClient.GetCustomersAsync().Result.ToList().Last();

            Customer customer = new Customer
            {
                CustomerId = customerId.CustomerId + 1,
                CustomerName = teName.Text,
                CustomerPhone = tePhoneNumber.Text,
                LoginId = teId.Text,
                Password = tePwd.Text,
                CustomerTypeId = cbVip.Text == "Yes" ? 1 : 2,
                RegistrationDate = DateTime.Now
            };

            UserClient.CustomersClient.PostCustomerAsync(customer);

            this.Close();
        }

        private void homeBtn_Click(object sender, EventArgs e)
        {
            //Login login = new Login();
            //login.Show();
            this.Close();
        }
    }
}
