using DevExpress.XtraEditors;
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
        int memberId;
        int cursorFlag = 0;
        int keyboardFalg = 0;
        int facilityId = 15;

        string[][] key = new string[][]
        {
           new string[] {"Q","W","E","R","T","Y","U","I","O","P","A","S","D","F","G","H","J","K","L","Z","X","C","V","B","N","M" },
           new string[] {"1","2","3","4","5","6","7","8","9","0","!","@","#","$","%","^","=","*","(",")",",",".","/","?",";",":" },
           new string[] {"ㅂ","ㅈ","ㄷ","ㄱ","ㅅ","ㅛ","ㅕ","ㅑ","ㅐ","ㅔ","ㅁ","ㄴ","ㅇ","ㄹ","ㅎ","ㅗ","ㅓ","ㅏ","ㅣ","ㅋ","ㅌ","ㅊ","ㅍ","ㅠ","ㅜ","ㅡ" }
        };

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
                    memberId = item.CustomerId;
                    
                    if (item.Password == tePwd.Text)
                    {
                        CheckPwd = 1;
                    }
                }
            }

            if(CheckId ==1 && CheckPwd ==1)
            {
                MessageBox.Show("로그인 성공");
                
                Selection form = new Selection(memberId, facilityId);
                form.Show();
                this.Close();
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

            textEdit.Text = textEdit.Text + btn.Text;
        }

        private void ChangeKey(int selectNum)
        {
            char check = Convert.ToChar(_buttons[0].Text);
            int sum = 0;

            // 대소문자
            if (selectNum == 1)
            {
                if (keyboardFalg ==0 || keyboardFalg ==1)
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
            else if(selectNum ==2)
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
            else if(selectNum == 3)
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
        }

        private void textEdit_DoubleClick(object sender, EventArgs e)
        {
            TextEdit textEdit = sender as TextEdit;
            if (textEdit.Name == "teId")
                cursorFlag = 1;
            else if (textEdit.Name == "tePwd")
                cursorFlag = 2;
            textEdit.SelectAll();
        }

        private void join_Click(object sender, EventArgs e)
        {
            Close();
            JoinUs form = new JoinUs();
            form.Show();
        }
    }
}
