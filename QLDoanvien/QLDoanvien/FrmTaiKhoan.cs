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
using DevExpress.XtraGrid.Views.Base;
using System.Security.Cryptography;

namespace QLDoanvien
{
    public partial class FrmTaiKhoan : DevExpress.XtraEditors.XtraForm
    {
        public FrmTaiKhoan()
        {
            InitializeComponent();
        }
        Con_CRUD con = new Con_CRUD();
        string sqlR = "select * from TAIKHOAN";
        string sqlRBT = "select * from THONGTINCHUNG";
      
        private void loadData()
        {
            DataTable dt = con.readData(sqlR);
            if (dt != null)
            {
                gcTK.DataSource = dt;
            }
        }

        private void loadDV()
        {
            DataTable dt = con.readData(sqlRBT);
            if (dt != null)
            {
                lkuDV.Properties.DataSource = dt;
                lkuDV.Properties.DisplayMember = "HOTENDV";
                lkuDV.Properties.ValueMember = "MADV";
            }
        }
        private StringBuilder encrypt(String str)
        {
            StringBuilder sb = new StringBuilder();
            MD5 md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(str);
            byte[] hash = md5.ComputeHash(inputBytes);
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x"));
            }
            return sb;
        }
       
       
        private void FrmTaiKhoan_Load(object sender, EventArgs e)
        {
            loadData();
            loadDV();
            txtMK.Properties.PasswordChar = '*';

        }
        private void btnThem_Click(object sender, EventArgs e)
        {
           
            if ((txtTK.EditValue == null) || (txtTK.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Bạn chưa nhập tài khoản\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTK.Focus();
                return;
            }
            if ((txtMK.EditValue == null) || (txtMK.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Bạn chưa nhập tài khoản\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMK.Focus();
                return;
            }
            bool check = false;
            string sql = "select TAIKHOAN from TAIKHOAN";
            DataTable dt = new DataTable();
            dt = con.readData(sql);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (txtTK.EditValue.ToString().Trim().Equals(dr["TAIKHOAN"].ToString()))
                    {
                        check = true;
                        break;
                    }
                }
            }
            if (check)
            {
                XtraMessageBox.Show("Tài khoản đã tồn tại\r\nVui lòng chọn tên tài khoản khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTK.EditValue = null;
                txtTK.Focus();
                return;
            }

            string sqlC = "insert into TAIKHOAN(ID,MADV,TAIKHOAN,MATKHAU)" +

            "values (" + "N'" + con.creatId("TK", sqlR) + "'" + "," +
            "N'" + lkuDV.EditValue.ToString() + "'" + "," +
            "N'" + txtTK.EditValue.ToString() + "'" + "," +
            "N'" + encrypt(txtMK.EditValue.ToString()).ToString() + "')";

            if (con.exeData(sqlC))
            {
                loadData();
                XtraMessageBox.Show("Thêm tài khoản thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLM.PerformClick();
            }
            else
            {
                XtraMessageBox.Show("Thêm tài khoản thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvTK_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName != "MATKHAU") return;
            e.DisplayText = "********";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMTK.EditValue == null)
            {
                XtraMessageBox.Show("Bạn chưa chọn tài khoản để sửa\r\nVui lòng chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if ((txtTK.EditValue == null) || (txtTK.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Tài khoản không được phép để trống\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTK.Focus();
                return;
            }
         /*   bool checkB = false;
            string sql = "select MADV, TAIKHOAN from TAIKHOAN where MADV = '" + lkuDV.EditValue.ToString().Trim() + "' and TAIKHOAN = N'" + txtTK.EditValue.ToString().Trim() + "'";
            DataTable dt = new DataTable();
            dt = con.readData(sql);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (lkuDV.EditValue.ToString().Trim().Equals(dr["MADV"].ToString()) && txtTK.EditValue.ToString().Trim().Equals(dr["TAIKHOAN"].ToString()))
                    {
                        checkB = true;
                        break;
                    }
                }
            }
            if (checkB)
            {
                XtraMessageBox.Show("Tài khoản có tên \"" + txtTK.EditValue.ToString() + "\" thuộc đoàn viên có mã \"" + lkuDV.EditValue.ToString() + "\" đã tồn tại\r\nVui lòng nhập tên khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLM.PerformClick();
                return;
            }*/
            if (XtraMessageBox.Show("Bạn có chắc chắn muốn sửa tài khoản đang chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sqlU = "update TAIKHOAN set TAIKHOAN = N'" + txtTK.EditValue.ToString() + "',MATKHAU = N'" + encrypt(txtMK.EditValue.ToString()).ToString() + "', MADV = '" + lkuDV.EditValue.ToString() + "' where ID = '" + txtMTK.EditValue.ToString() + "'";
                if (con.exeData(sqlU))
                {
                    loadData();
                    XtraMessageBox.Show("Sửa tài khoản thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLM.PerformClick();
                }
                else
                {
                    XtraMessageBox.Show("Sửa tài khoản thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMTK.EditValue == null)
            {
                XtraMessageBox.Show("Bạn chưa chọn tài khoản để xoá\r\nVui lòng chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (XtraMessageBox.Show("Bạn có chắc chắn muốn xoá tài khoản đang chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sqlD = "delete from TAIKHOAN where ID = '" + txtMTK.EditValue.ToString() + "'";
                if (con.exeData(sqlD))
                {
                    loadData();
                    XtraMessageBox.Show("Xoá tài khoản thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLM.PerformClick();
                }
                else
                {
                    XtraMessageBox.Show("Xoá tài khoản thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnLM_Click(object sender, EventArgs e)
        {
            txtMTK.EditValue = null;
            txtTK.EditValue = null;
            txtMK.EditValue = null;
            txtTK.Focus();
            txtMK.Focus();
            lkuDV.EditValue = "";
        }

        private void gcTK_MouseCaptureChanged(object sender, EventArgs e)
        {
            int row_index = gvTK.FocusedRowHandle;
            string colID = "ID";
            string colDV = "MADV";
            string colTK = "TAIKHOAN";
            string colMK = "MATKHAU";
            if ((gvTK.GetRowCellValue(row_index, colID) != null) && (gvTK.GetRowCellValue(row_index, colDV) != null) && (gvTK.GetRowCellValue(row_index, colTK) != null) && (gvTK.GetRowCellValue(row_index, colMK) != null))
            {
                txtMTK.EditValue = gvTK.GetRowCellValue(row_index, colID).ToString();
                txtTK.EditValue = gvTK.GetRowCellValue(row_index, colTK).ToString();
                txtMK.EditValue = gvTK.GetRowCellValue(row_index, colMK).ToString();
                lkuDV.EditValue = gvTK.GetRowCellValue(row_index, colDV).ToString();
            }
        }
    }
}