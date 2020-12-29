using DevExpress.XtraEditors;
using MyNamespace;
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

        }
    }
}
