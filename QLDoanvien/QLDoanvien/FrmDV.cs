using DevExpress.XtraEditors;
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
    public partial class FrmDV : DevExpress.XtraEditors.XtraForm
    {
        public FrmDV()
        {
            InitializeComponent();
        }
        Con_CRUD con = new Con_CRUD();
        string sqlR = "select * from THONGTINCHUNG";
        string sql1 = "select * from TINH";
        string sql2 = "select * from TRDVANHOA";
        string sql3 = "select * from DANTOC";
        string sql4 = "select * from TONGIAO";
        string sql5 = "select * from CHIDOAN";
        string sql6 = "select * from CCTINHOC";
        string sql7 = "select * from CCANHVAN";


        private void loadData()
        {
            DataTable dt = con.readData(sqlR);
            if (dt != null)
            {
                gcDV.DataSource = dt;
            }
        }

        private void LoadTinh()
        {
            DataTable dt = con.readData(sql1);
            if (dt != null)
            {
                lkuTT.Properties.DataSource = dt;
                lkuTT.Properties.DisplayMember = "TENTINH";
                lkuTT.Properties.ValueMember = "MATINH";
            }
        }
        private void LoadTDVH()
        {
            DataTable dt = con.readData(sql2);
            if (dt != null)
            {
                lkuVH.Properties.DataSource = dt;
                lkuVH.Properties.DisplayMember = "TENTRDOVH";
                lkuVH.Properties.ValueMember = "MATRDOVH";
            }
        }
        private void LoadDT()
        {
            DataTable dt = con.readData(sql3);
            if (dt != null)
            {
                lkuDT.Properties.DataSource = dt;
                lkuDT.Properties.DisplayMember = "TENDANTOC";
                lkuDT.Properties.ValueMember = "MADANTOC";
            }
        }
        private void LoadTG()
        {
            DataTable dt = con.readData(sql4);
            if (dt != null)
            {
                lkuTG.Properties.DataSource = dt;
                lkuTG.Properties.DisplayMember = "TENTONGIAO";
                lkuTG.Properties.ValueMember = "MATONGIAO";
            }
        }
        private void LoadCD()
        {
            DataTable dt = con.readData(sql5);
            if (dt != null)
            {
                lkuCD.Properties.DataSource = dt;
                lkuCD.Properties.DisplayMember = "TENCHIDOAN";
                lkuCD.Properties.ValueMember = "MACHIDOAN";
            }
        }
        private void LoadTH()
        {
            DataTable dt = con.readData(sql6);
            if (dt != null)
            {
                lkuTH.Properties.DataSource = dt;
                lkuTH.Properties.DisplayMember = "TENCCTINHOC";
                lkuTH.Properties.ValueMember = "MACCTINHOC";
            }
        }
        private void LoadNN()
        {
            DataTable dt = con.readData(sql7);
            if (dt != null)
            {
                lkuNN.Properties.DataSource = dt;
                lkuNN.Properties.DisplayMember = "TENCCANHVAN";
                lkuNN.Properties.ValueMember = "MACCANHVAN";
            }
        }
        private void FrmDV_Load(object sender, EventArgs e)
        {
            loadData();
            LoadTinh();
            LoadTDVH();
            LoadCD();
            LoadDT();
            LoadNN();
            LoadTH();
            LoadTG();
        }
        private void CheckNhap()
        {
           /* if ((txtMCĐ.EditValue == null) || (txtMCĐ.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Bạn chưa nhập mã đoàn viên\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTDV.Focus();
                return;
            }*/
            if ((txtTDV.EditValue == null) || (txtTDV.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Bạn chưa nhập tên đoàn viên\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTDV.Focus();
                return;
            }
            if ((txtGT.EditValue == null) || (txtGT.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Bạn chưa chọn giới tính\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCCCD.Focus();
                return;
            }
            if ((txtNS.EditValue == null) || (txtNS.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Bạn chưa nhập ngày sinh\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCCCD.Focus();
                return;
            }
            if ((txtCCCD.EditValue == null) || (txtCCCD.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Bạn chưa nhập căn cước công dân\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCCCD.Focus();
                return;
            }
            if ((txtDV.EditValue == null) || (txtDV.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Bạn chưa đảng viên\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCCCD.Focus();
                return;
            }

            if ((txtNVD.EditValue == null) || (txtNVD.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Bạn chưa nhập ngày vào đoàn\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCCCD.Focus();
                return;
            }
            if ((txtSDT.EditValue == null) || (txtSDT.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Bạn chưa nhập số điện thoại\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSDT.Focus();
                return;
            }
            if ((txtEmail.EditValue == null) || (txtEmail.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Bạn chưa nhập email\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEmail.Focus();
                return;
            }
            if ((txtNN.EditValue == null) || (txtNN.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Bạn chưa nhập nghề nghiệp\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNN.Focus();
                return;
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            CheckNhap();
            bool check = false;
             string sql = "select MADV from THONGTINCHUNG";
             DataTable dt = new DataTable();
             dt = con.readData(sql);
             if (dt != null)
             {
                 foreach (DataRow dr in dt.Rows)
                 {
                     if (txtMCĐ.EditValue.ToString().Trim().Equals(dr["MADV"].ToString()))
                     {
                         check = true;
                         break;
                     }
                 }
             }
            if (check)
            {
                XtraMessageBox.Show("Mã đoàn viên đã tồn tại\r\nVui lòng chọn tên tài khoản khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMCĐ.EditValue = null;
                txtMCĐ.Focus();
                return;
            }
            
            string sqlC = "insert into THONGTINCHUNG(MADV,MATINH,MACCANHVAN,MATONGIAO,MATRDOVH,MADANTOC,MACHIDOAN,MACCTINHOC,HOTENDV,GIOITINH,NGAYSINH,CCCD,DANGVIEN,NGAYVAODOAN,SODIENTHOAI,EMAIL,NGHENGHIEP)" +

            "values (" + "N'" + con.creatId("DV", sqlR) + "'" + "," +
            "N'" + lkuTT.EditValue.ToString() + "'" + "," +
            "N'" + lkuNN.EditValue.ToString() + "'" + "," +
            "N'" + lkuTG.EditValue.ToString() + "'" + "," +
            "N'" + lkuVH.EditValue.ToString() + "'" + "," +
            "N'" + lkuDT.EditValue.ToString() + "'" + "," +
            "N'" + lkuCD.EditValue.ToString() + "'" + "," +
            "N'" + lkuTH.EditValue.ToString() + "'" + "," +
            "N'" + txtTDV.EditValue.ToString() + "'" + "," +
            "N'" + txtGT.EditValue.ToString() + "'" + "," +
            "N'" + txtNS.EditValue.ToString() + "'" + "," +
            "N'" + txtCCCD.EditValue.ToString() + "'" + "," +
            "N'" + txtDV.EditValue.ToString() + "'" + "," +
            "N'" + txtNVD.EditValue.ToString() + "'" + "," +
            "N'" + txtSDT.EditValue.ToString() + "'" + "," +
            "N'" + txtEmail.EditValue.ToString() + "'" + "," +
            "N'" + txtNN.EditValue.ToString() + "')";

            if (con.exeData(sqlC))
            {
                loadData();
                XtraMessageBox.Show("Thêm đoàn viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLM.PerformClick();
            }
            else
            {
                XtraMessageBox.Show("Thêm đoàn viên thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sqlU = "update THONGTINCHUNG set MATINH = N'" + lkuTT.EditValue.ToString() + "', MACCANHVAN = N'" + lkuNN.EditValue.ToString() + "',  MATONGIAO = N'" + lkuTG.EditValue.ToString() + "', MATRDOVH = N'" + lkuVH.EditValue.ToString() + "', MADANTOC = N'" + lkuDT.EditValue.ToString() + "', MACHIDOAN = N'" + lkuCD.EditValue.ToString() + "', MACCTINHOC = N'" + lkuTH.EditValue.ToString() + "',HOTENDV = N'" + txtTDV.EditValue.ToString() + "', GIOITINH = N'" + txtGT.EditValue.ToString() + "', NGAYSINH = N'" + txtNS.EditValue.ToString() + "', CCCD = N'" + txtCCCD.EditValue.ToString() + "', DANGVIEN = N'" + txtDV.EditValue.ToString() + "', NGAYVAODOAN = N'" + txtNVD.EditValue.ToString() + "',SODIENTHOAI = N'" + txtSDT.EditValue.ToString() + "', EMAIL = N'" + txtEmail.EditValue.ToString() + "', NGHENGHIEP = N'" + txtNN.EditValue.ToString() + "' where MADV = '" + txtMCĐ.EditValue.ToString() + "'";
            if (con.exeData(sqlU))
            {
                loadData();
                XtraMessageBox.Show("Sửa thông tin đoàn viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLM.PerformClick();
            }
            else
            {
                XtraMessageBox.Show("Sửa thông tin đoàn viên thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gcDV_MouseCaptureChanged(object sender, EventArgs e)
        {
            int row_index = gvDV.FocusedRowHandle;
            string colID = "MADV";
            string col1 = "MATINH";
            string col2 = "MACCANHVAN";
            string col3 = "MATONGIAO";
            string col4 = "MATRDOVH";
            string col5 = "MADANTOC";
            string col6 = "MACHIDOAN";
            string col7 = "MACCTINHOC";
            string col8 = "HOTENDV";
            string col9 = "GIOITINH";
            string col10 = "NGAYSINH";
            string col11 = "CCCD";
            string col12 = "DANGVIEN";
            string col13 = "NGAYVAODOAN";
            string col14 = "SODIENTHOAI";
            string col15 = "EMAIL";
            string col16 = "NGHENGHIEP";
            if ((gvDV.GetRowCellValue(row_index, colID) != null) && (gvDV.GetRowCellValue(row_index, col1) != null) && (gvDV.GetRowCellValue(row_index, col2) != null &&
                gvDV.GetRowCellValue(row_index, col3) != null) && (gvDV.GetRowCellValue(row_index, col4) != null) && (gvDV.GetRowCellValue(row_index, col5) != null &&
                gvDV.GetRowCellValue(row_index, col6) != null) && (gvDV.GetRowCellValue(row_index, col7) != null) && (gvDV.GetRowCellValue(row_index, col8) != null &&
                gvDV.GetRowCellValue(row_index, col9) != null) && (gvDV.GetRowCellValue(row_index, col10) != null) && (gvDV.GetRowCellValue(row_index, col11) != null &&
                gvDV.GetRowCellValue(row_index, col12) != null) && (gvDV.GetRowCellValue(row_index, col13) != null) && (gvDV.GetRowCellValue(row_index, col14) != null && (gvDV.GetRowCellValue(row_index, col15) != null) && (gvDV.GetRowCellValue(row_index, col16) != null)))
            {

                txtMCĐ.EditValue = gvDV.GetRowCellValue(row_index, colID).ToString();
                lkuTT.EditValue = gvDV.GetRowCellValue(row_index, col1).ToString();
                lkuNN.EditValue = gvDV.GetRowCellValue(row_index, col2).ToString();
                lkuTG.EditValue = gvDV.GetRowCellValue(row_index, col3).ToString();
                lkuVH.EditValue = gvDV.GetRowCellValue(row_index, col4).ToString();
                lkuDT.EditValue = gvDV.GetRowCellValue(row_index, col5).ToString();
                lkuCD.EditValue = gvDV.GetRowCellValue(row_index, col6).ToString();
                lkuTH.EditValue = gvDV.GetRowCellValue(row_index, col7).ToString();
                txtTDV.EditValue = gvDV.GetRowCellValue(row_index, col8).ToString();
                txtGT.EditValue = gvDV.GetRowCellValue(row_index, col9).ToString();
                txtNS.EditValue = gvDV.GetRowCellValue(row_index, col10).ToString();
                txtCCCD.EditValue = gvDV.GetRowCellValue(row_index, col11).ToString();
                txtDV.EditValue = gvDV.GetRowCellValue(row_index, col12).ToString();
                txtNVD.EditValue = gvDV.GetRowCellValue(row_index, col13).ToString();
                txtSDT.EditValue = gvDV.GetRowCellValue(row_index, col14).ToString();
                txtEmail.EditValue = gvDV.GetRowCellValue(row_index, col15).ToString();
                txtNN.EditValue = gvDV.GetRowCellValue(row_index, col16).ToString();

            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMCĐ.EditValue == null)
            {
                XtraMessageBox.Show("Bạn chưa chọn đoàn viên để xoá\r\nVui lòng chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (XtraMessageBox.Show("Bạn có chắc chắn muốn xoá đoàn viên đang chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sqlD = "delete from THONGTINCHUNG where MADV = '" + txtMCĐ.EditValue.ToString() + "'";
                if (con.exeData(sqlD))
                {
                    loadData();
                    XtraMessageBox.Show("Xoá đoàn viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLM.PerformClick();
                }
                else
                {
                    XtraMessageBox.Show("Xoá đoàn viên thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnLM_Click(object sender, EventArgs e)
        {

            txtMCĐ.EditValue = null;
            txtTDV.EditValue = null; 
            txtGT.EditValue = null;
            txtNS.EditValue = null; 
            txtCCCD.EditValue = null; 
            txtDV.EditValue = null;
            txtNVD.EditValue = null;
            txtSDT.EditValue = null;
            txtEmail.EditValue = null;
            txtNN.EditValue = null;
            txtTDV.Focus(); 
            txtGT.Focus();
            txtNS.Focus();
            txtCCCD.Focus();
            txtDV.Focus();
            txtNVD.Focus();
            txtSDT.Focus();
            txtEmail.Focus();
            txtNN.Focus();
            lkuTT.EditValue = "";
            lkuNN.EditValue = "";
            lkuTG.EditValue = "";
            lkuVH.EditValue = "";
            lkuDT.EditValue = "";
            lkuCD.EditValue = "";
            lkuTH.EditValue = "";
        }
    }

}
