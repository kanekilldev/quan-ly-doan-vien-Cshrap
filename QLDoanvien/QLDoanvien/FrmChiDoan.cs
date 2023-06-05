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
    public partial class FrmChiDoan : DevExpress.XtraEditors.XtraForm
    {
        public FrmChiDoan()
        {
            InitializeComponent();
        }
        Con_CRUD con = new Con_CRUD();
        string sqlR = "select * from CHIDOAN";
        string sqlRBT = "select * from KHOA";

        private void loadData()
        {
            DataTable dt = con.readData(sqlR);
            if (dt != null)
            {
                gcChiDoan.DataSource = dt;
            }
        }

        private void loadkhoa()
        {
            DataTable dt = con.readData(sqlRBT);
            if (dt != null)
            {
                luMKhoa.Properties.DataSource = dt;
                luMKhoa.Properties.DisplayMember = "TENKHOA";
                luMKhoa.Properties.ValueMember = "MAKHOA";
            }
        }
        private void FrmChiDoan_Load(object sender, EventArgs e)
        {
            loadData();
            loadkhoa();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            
            if ((txtTCĐ.EditValue == null) || (txtTCĐ.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Bạn chưa nhập tên Chi đoàn\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTCĐ.Focus();
                return;
            }
            // checkB = true thì kiểm tra dữ liệu tên chi đoàn vs khoa đã tồn tại hay ch 

            bool checkB = false;
            string sql = "select MAKHOA, TENCHIDOAN from CHIDOAN where MAKHOA = '" + luMKhoa.EditValue.ToString().Trim() + "' and TENCHIDOAN = N'" + txtTCĐ.EditValue.ToString().Trim() + "'";
            DataTable dt = new DataTable();
            dt = con.readData(sql);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (luMKhoa.EditValue.ToString().Trim().Equals(dr["MAKHOA"].ToString()) && txtTCĐ.EditValue.ToString().Trim().Equals(dr["TENCHIDOAN"].ToString()))
                    {
                        checkB = true;
                        break;
                    }
                }
            }
            if (checkB)
            {
                XtraMessageBox.Show("Chi đoàn có tên \"" + txtTCĐ.EditValue.ToString() + "\" thuộc khoa có mã \"" + luMKhoa.EditValue.ToString() + "\" đã tồn tại\r\nVui lòng nhập tên khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLM.PerformClick();
                return;
            }

            string sqlC = "insert into CHIDOAN values ('" + con.creatId("CĐ", sqlR) + "', '" + luMKhoa.EditValue.ToString() + "', N'" + txtTCĐ.EditValue.ToString() + "')";
            if (con.exeData(sqlC))
            {
                loadData();
                XtraMessageBox.Show("Thêm Chi đoàn thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLM.PerformClick();
            }
            else
            {
                XtraMessageBox.Show("Thêm Chi đoàn thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMCĐ.EditValue == null)
            {
                XtraMessageBox.Show("Bạn chưa chọn chi đoàn để sửa\r\nVui lòng chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if ((txtTCĐ.EditValue == null) || (txtTCĐ.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Tên Chi đoàn không được phép để trống\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTCĐ.Focus();
                return;
            }
         
            bool checkB = false;
            string sql = "select MAKHOA, TENCHIDOAN from CHIDOAN where MAKHOA = '" + luMKhoa.EditValue.ToString().Trim() + "' and TENCHIDOAN = N'" + txtTCĐ.EditValue.ToString().Trim() + "'";
            DataTable dt = new DataTable();
            dt = con.readData(sql);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (luMKhoa.EditValue.ToString().Trim().Equals(dr["MAKHOA"].ToString()) && txtTCĐ.EditValue.ToString().Trim().Equals(dr["TENCHIDOAN"].ToString()))
                    {
                        checkB = true;
                        break;
                    }
                }
            }
            if (checkB)
            {
                XtraMessageBox.Show("Chi đoàn có tên \"" + txtTCĐ.EditValue.ToString() + "\" thuộc khoa có mã \"" + luMKhoa.EditValue.ToString() + "\" đã tồn tại\r\nVui lòng nhập tên khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLM.PerformClick();
                return;
            }
            if (XtraMessageBox.Show("Bạn có chắc chắn muốn sửa Chi đoàn đang chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sqlU = "update CHIDOAN set  MAKHOA = '" + luMKhoa.EditValue.ToString() + "', TENCHIDOAN = N'" + txtTCĐ.EditValue.ToString() + "' where MACHIDOAN = '" + txtTCĐ.EditValue.ToString() + "'";
                if (con.exeData(sqlU))
                {
                    loadData();
                    XtraMessageBox.Show("Sửa Chi đoàn thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLM.PerformClick();
                }
                else
                {
                    XtraMessageBox.Show("Sửa Chi đoàn thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMCĐ.EditValue == null)
            {
                XtraMessageBox.Show("Bạn chưa chọn Chi đoàn để xoá\r\nVui lòng chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (XtraMessageBox.Show("Bạn có chắc chắn muốn xoá Chi đoàn đang chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sqlD = "delete from CHIDOAN where MACHIDOAN = '" + txtMCĐ.EditValue.ToString() + "'";
                if (con.exeData(sqlD))
                {
                    loadData();
                    XtraMessageBox.Show("Xoá Chi đoàn thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLM.PerformClick();
                }
                else
                {
                    XtraMessageBox.Show("Xoá Chi đoàn thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnLM_Click(object sender, EventArgs e)
        {
            txtMCĐ.EditValue = null;
            txtTCĐ.EditValue = null;
            txtTCĐ.Focus();
            luMKhoa.EditValue = "";
        }

        private void gcChiDoan_MouseCaptureChanged(object sender, EventArgs e)
        {
            int row_index = gvChiDoan.FocusedRowHandle;
            string colID = "MACHIDOAN";
            string colName = "TENCHIDOAN";
            string colK= "MAKHOA";
            if ((gvChiDoan.GetRowCellValue(row_index, colID) != null) && (gvChiDoan.GetRowCellValue(row_index, colName) != null) &&  (gvChiDoan.GetRowCellValue(row_index, colK) != null))
            {
                txtMCĐ.EditValue = gvChiDoan.GetRowCellValue(row_index, colID).ToString();
                txtTCĐ.EditValue = gvChiDoan.GetRowCellValue(row_index, colName).ToString();
                luMKhoa.EditValue = gvChiDoan.GetRowCellValue(row_index, colK).ToString();
            }
        }
    }
}