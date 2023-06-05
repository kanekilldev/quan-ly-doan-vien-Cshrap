using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDoanvien
{
    public partial class FrmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public FrmMain()
        {
            InitializeComponent();
        }
        public void Giaodien()
        {
            DevExpress.LookAndFeel.DefaultLookAndFeel giaodien = new DevExpress.LookAndFeel.DefaultLookAndFeel();
            giaodien.LookAndFeel.SkinName = "Office 2010 Blue";
        }

        // mở các from con có điều kiện
        void OppenFrom(Type typeFrom)
        {
            foreach (Form frm in MdiChildren)
            {
                if (frm.GetType() == typeFrom)
                {
                    frm.Activate();
                    return;
                }
            }
            Form f = (Form)Activator.CreateInstance(typeFrom);
            f.MdiParent = this;
            f.Show();
        }
        private void btnThoat_ItemClick(object sender, ItemClickEventArgs e)
        {
            Close();
        }

        private void btnTTTG_ItemClick(object sender, ItemClickEventArgs e)
        {
            
        }

       private void btnKhoa_ItemClick(object sender, ItemClickEventArgs e)
        {
           
        }

        private void btnTinh_ItemClick(object sender, ItemClickEventArgs e)
        {
            OppenFrom(typeof(FrmTinh));
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            OppenFrom(typeof(FrmKhoa));
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            OppenFrom(typeof(FrmDanToc));
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            OppenFrom(typeof(FrmChiDoan));
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            OppenFrom(typeof(FrmTonGiao));
        }

        private void barButtonItem12_ItemClick(object sender, ItemClickEventArgs e)
        {
            OppenFrom(typeof(FrmChucVu));
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            OppenFrom(typeof(FrmTrDoVH));
        }

        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
        {
            OppenFrom(typeof(FrmChuyenMon));
        }

        private void btnCCTH_ItemClick(object sender, ItemClickEventArgs e)
        {
            OppenFrom(typeof(FrmCCTH));
        }

        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            OppenFrom(typeof(FrmCCAV));
        }

        private void barButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
        {
            OppenFrom(typeof(FrmNamHoc));
        }

        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
            OppenFrom(typeof(FrmDV));
        }

        private void bthQLTK_ItemClick(object sender, ItemClickEventArgs e)
        {
            OppenFrom(typeof(FrmTaiKhoan));
        }

        private void barButtonItem13_ItemClick(object sender, ItemClickEventArgs e)
        {
            OppenFrom(typeof(FrmBCH));
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            Giaodien();
        }

        private void barButtonItem14_ItemClick(object sender, ItemClickEventArgs e)
        {
            OppenFrom(typeof(FrmSoDoan));
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            OppenFrom(typeof(FrmCTCM));

        }

        private void barButtonItem15_ItemClick(object sender, ItemClickEventArgs e)
        {
            OppenFrom(typeof(FrmDoanPhi));
        }
    }
}