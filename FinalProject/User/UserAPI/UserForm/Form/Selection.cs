using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserForm
{
    public partial class Selection : Form
    {
        int MemberId;
        int FacilityId;
        public Selection(int memberId, int facilityId)
        {
            InitializeComponent();

            MemberId = memberId;
            FacilityId = facilityId;
        }

        private void StorageBtn(object sender, EventArgs e)
        {
            InputStorageForm form = new InputStorageForm(MemberId, FacilityId);
            form.Show();
        }

        private void ReleaseBtn(object sender, EventArgs e)
        {
            Release form = new Release(MemberId, FacilityId);
            form.Show();
        }
    }
}
