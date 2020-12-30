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
    public partial class Release : Form
    {
        int MemberId;
        int FacilityId;
        public Release(int memberId, int facilityId)
        {
            InitializeComponent();
            MemberId = memberId;
            FacilityId = facilityId;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CreateLabelList();
            ConvertRedBox(FacilityId);
            CheckCustomerBox(FacilityId);
        }

        private void ConvertRedBox(int facilityId)
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
                    
                     item.BackColor = Color.Red;
                    
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
        private void CheckCustomerBox(int facilityId)
        {
            // DB 많아지면 오래걸릴듯
            var storage = UserClient.StoragesClient.GetStoragesAsync().Result;
            var purchase = from x in UserClient.PurchasesClient.GetPurchasesAsync().Result
                           where x.CustomerId == 1
                           select x;
            var check = purchase.ToList();
            var purcahaseItems = UserClient.PurchaseItemsClient.GetPurchaseItemsAsync().Result;

            var storageList = from x in storage
                              where x.FacilityId == facilityId
                              join y in purcahaseItems on x.StorageId equals y.StorageId
                              where y.PurchaseId == check.Last().PurchaseId && (DateTime.Now-y.InTime).Hours<24
                              select new
                              {
                                  x.StorageId,
                              };

            foreach (var item in storageList)
            {
                foreach (var label in _labels)
                {
                    if (label.Text == item.StorageId.ToString())
                    {
                        label.BackColor = Color.White;
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
            // 노란건 이미 선택된거기 때문에 다시 하얀색으로
            else if (labelBox.BackColor == Color.Yellow)
            {
                labelBox.BackColor = Color.White;
            }
        }

        List<int> storageNumList = new List<int>();

        private void ReleaseBtn(object sender, EventArgs e)
        {
            foreach (var item in _labels)
            {
                if(item.BackColor == Color.Yellow)
                {
                    storageNumList.Add(Convert.ToInt32(item.Text));
                }
            }

            // 고객 아이디와 보관함번호 맞춰서 linq로 찾고 put(update) outTime
            
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
