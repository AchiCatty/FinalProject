﻿using MyNamespace;
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
    public partial class Payment : Form
    {
        List<GridItem> gridLists = new List<GridItem>();
        

        int MemberId;
        List<int> StorageList = new List<int>();
        public Payment(int memberId, List<int> storageList)
        {
            InitializeComponent();
            MemberId = memberId;
            StorageList = storageList;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            bdsList.DataSource = CreateDataSource(StorageList);
            gridView1.Columns[0].Caption = "보관함 번호";
            gridView1.Columns[1].Caption = "입고 시간";
            gridView1.Columns[2].Caption = "출고 예정 시간";
            gridView1.Columns[3].Caption = "가격";

        }

        private List<GridItem> CreateDataSource(List<int> storageList)
        {
            foreach (var item in storageList)
            {
                GridItem gridList = new GridItem();
                gridList.StorageId = item;
                gridList.InTime = DateTime.Now;
                gridList.OutTime = gridList.InTime.AddHours(6);
                gridList.Amount = 6000;

                gridLists.Add(gridList);
            }
         
            return gridLists;   
            
        }

        private void btnCardPay_Click(object sender, EventArgs e)
        {
            // binding 된 data 가져오기
            // 바인딩 했다는 소리는 수정된 것도 자동으로 가지고 있음
            bdsList.EndEdit();
            List<GridItem> items = (List<GridItem>)bdsList.DataSource;

            var purchaseIdNum = (UserClient.PurchasesClient.GetPurchasesAsync().Result.ToList()).Last().PurchaseId +1;
            List<PurchaseItem> purchaseItems = new List<PurchaseItem>();
            int purchaseAmount = 0;

            for (int i = 0; i < StorageList.Count; i++)
            {
                double hour = DateTime.Now.Hour;
                double min = DateTime.Now.Minute;

                // smalldatetime으로 변환시켜주는거
                string dt = Convert.ToDateTime(gridView1.GetRowCellValue(i, "InTime")).ToShortDateString();
                DateTime ndt = (Convert.ToDateTime(dt)).AddHours(hour).AddMinutes(min);

                PurchaseItem purchaseItem = new PurchaseItem
                {
                    StorageId = items[i].StorageId,
                    //StorageId = Convert.ToInt32(gridView1.GetRowCellValue(i, "StorageId")),
                    InTime = ndt,
                    //InTime = Convert.ToDateTime(gridView1.GetRowCellValue(i, "InTime")),
                    //OutTime = Convert.ToDateTime(gridView1.GetRowCellValue(i, "OutTime")),
                    OutTime = ndt.AddHours(6),
                    PurchaseItemId = (UserClient.PurchaseItemsClient.GetPurchaseItemsAsync().Result.ToList()).Last().PurchaseItemId +i+ 1,
                    //PurchaseItemId = (UserClient.PurchaseItemsClient.GetPurchaseItemsAsync().Result.ToList()).Last().PurchaseItemId +i+ 1,
                    //PurchaseId = purchaseIdNum
                    PurchaseId = purchaseIdNum
                };
                purchaseItems.Add(purchaseItem);
                purchaseAmount += Convert.ToInt32(gridView1.GetRowCellValue(i, "Amount"));
            }

            for (int i = 0; i < purchaseItems.Count; i++)
            {
                UserClient.PurchaseItemsClient.PostPurchaseItemAsync(purchaseItems[i]);
            }

            Purchase purchase = new Purchase();
            purchase.PurchaseId = purchaseIdNum;
            purchase.PurchaseTime = DateTimeOffset.Now;
            purchase.OtherDetails = "정상";
            purchase.CustomerId = MemberId;
            purchase.PurchaseAmount = purchaseAmount;
            purchase.TransactionId = 77;

            UserClient.PurchasesClient.PostPurchaseAsync(purchase);

        }
    }

    public class GridItem
    {
        public int StorageId { get; set; }
        public DateTime InTime { get; set; }
        public DateTime OutTime { get; set; }
        public int Amount { get; set; }

        //int StorageId;
        //DateTime InTime = DateTime.Now;
        //DateTime OutTime;
        //int Amount = 0;

        //public GirdList(int storageId, int outTime)
        //{
        //    StorageId = storageId;
        //    OutTime = InTime.AddHours(outTime);
        //}

    }

}
