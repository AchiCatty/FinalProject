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
    public partial class InputStorageForm : XtraForm
    {
        private int MemberID;

        public InputStorageForm(int memberID)
        {
            InitializeComponent();
            MemberID = memberID;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CreateLabelList();
            CheckBoxType(15);
            CheckRedBox(15);
        }

        private void CheckBoxType(int facilityId)
        {
            var storage = UserClient.StoragesClient.GetStoragesAsync().Result;
            var fare = UserClient.FaresClient.GetFaresAsync().Result;

            var storageList = from x in storage
                              join y in fare on x.FareId equals y.FareId
                              where x.FacilityId == facilityId
                              select new
                              {
                                  x.StorageId,
                                  y.StorageTypeId
                              };

            foreach (var item in _labels)
            {
                foreach (var storageItem in storageList)
                {
                    if (item.Text == Convert.ToString(storageItem.StorageId))
                    {
                        if (storageItem.StorageTypeId == 1)
                        {
                            item.BackColor = Color.Gray;
                        }
                    }
                }

            }
        }

        // 사용하는 리스트
        private List<Label> _labels = new List<Label>();
        
        // CreateLabelList - _labels를 만들기 위해 reflection 하는 메서드
        private void CreateLabelList()
        {
            Type type = GetType();
            FieldInfo[] fieldInfos =
            type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var info in fieldInfos)
            {
                Label label = info.GetValue(this) as Label;
                if (label == null)
                    continue;

                _labels.Add(label);
            }
        }

        // CheckRedBox - 사용중인 박스 찾아서 Red로 표현해주는 메서드
        private void CheckRedBox(int facilityId)
        {
            var storage = UserClient.StoragesClient.GetStoragesAsync().Result;
            var purcahaseItems = UserClient.PurchaseItemsClient.GetPurchaseItemsAsync().Result;

            var storageList = from x in storage where x.FacilityId == facilityId
                       join y in purcahaseItems on x.StorageId equals y.StorageId
                       select new 
                       {
                            x.StorageId,
                            type = y.OutTime>=DateTime.Now?1:2
                       };

            foreach (var item in storageList)
            {
                foreach (var label in _labels)
                {
                    if (item.type == 1 && label.Text == Convert.ToString(item.StorageId))
                    {
                        label.BackColor = Color.Red;
                    }
                }
                
            }
        }

        // ClickLabel 
        public void ClickLabel(object sender, EventArgs e)
        {
            Label labelBox = (Label)sender;

            // 만약 누른 라벨의 배경색이 하얀거라면 노랗게 만들고
            if (labelBox.BackColor == Color.White)
            {
                labelBox.BackColor = Color.Yellow;
                labelBox.Tag = 1;
            }
            else if(labelBox.BackColor == Color.Gray)
            {
                labelBox.BackColor = Color.Yellow;
                labelBox.Tag = 2;
            }
            // 노란건 이미 선택된거기 때문에 다시 하얀색으로
            else if (labelBox.BackColor == Color.Yellow)
            {
                if (Convert.ToInt32(labelBox.Tag) == 1)
                {
                    labelBox.BackColor = Color.White;
                }
                else if (Convert.ToInt32(labelBox.Tag) == 2)
                {
                    labelBox.BackColor = Color.Gray;
                }
            }
        }

        // PayBtnClick - 버튼 누르면 결제창르로 이동
        public void PayBtnClick(object sender, EventArgs e)
        {
            List<int> storageList = new List<int>();
            foreach (var item in _labels)
            {
                if (item.BackColor == Color.Yellow)
                {
                    storageList.Add(Convert.ToInt32(item.Text));
                }
            }
            
            Payment payment = new Payment(MemberID, storageList);
            payment.Show();
            
        }

        // ExitBtn - 처음으로 버튼
        //private void ExitBtn(object sender, EventArgs e)
        //{
        //    // 메인화면 띄우기
        //    // todo 생성자에 멤버아이디 넣어주기
        //    Selection selection = new Selection(MemberID);
        //    selection.Show();
        //    Close();
        //}
    }
}
