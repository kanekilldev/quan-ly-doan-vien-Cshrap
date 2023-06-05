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
    public partial class FrmKhoa : DevExpress.XtraEditors.XtraForm
    {
        public FrmKhoa()
        {
            InitializeComponent();
        }
        Con_CRUD con = new Con_CRUD();
        string sqlR = "select * from KHOA";

        private void loadData()
        {
            DataTable dt = con.readData(sqlR);
            if (dt != null)
            {
                gcKhoa.DataSource = dt;
            }
        }

        private void FrmKhoa_Load(object sender, EventArgs e)
        {
            loadData();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {

            if ((txtTKhoa.EditValue == null) || (txtTKhoa.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Bạn chưa nhập tên khoa\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTKhoa.Focus();
                return;
            }
            bool checkB = false;
            string sql = "select TENKHOA from KHOA where TENKHOA = N'" + txtTKhoa.EditValue.ToString().Trim() + "'";
            DataTable dt = new DataTable();
            dt = con.readData(sql);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (txtTKhoa.EditValue.ToString().Trim().Equals(dr["TENKHOA"].ToString()))
                    {
                        checkB = true;
                        break;
                    }
                }
            }
            if (checkB)
            {
                XtraMessageBox.Show("Tên khoa \"" + txtTKhoa.EditValue.ToString() + "\" đã tồn tại\r\nVui lòng nhập tên khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLM.PerformClick();
                return;
            }
            string sqlC = "insert into KHOA values ('" + con.creatId("DK", sqlR) + "', N'" + txtTKhoa.EditValue.ToString() + "')";
            if (con.exeData(sqlC))
            {
                loadData();
                XtraMessageBox.Show("Thêm khoa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLM.PerformClick();
            }
            else
            {
                XtraMessageBox.Show("Thêm khoa thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMKhoa.EditValue == null)
            {
                XtraMessageBox.Show("Bạn chưa chọn khoa để sửa\r\nVui lòng chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if ((txtTKhoa.EditValue == null) || (txtTKhoa.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Tên khoa không được phép để trống\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTKhoa.Focus();
                return;
            }
            bool checkB = false;
            string sql = "select TENKHOA from KHOA where TENKHOA = N'" + txtTKhoa.EditValue.ToString().Trim() + "'";
            DataTable dt = new DataTable();
            dt = con.readData(sql);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (txtTKhoa.EditValue.ToString().Trim().Equals(dr["TENKHOA"].ToString()))
                    {
                        checkB = true;
                        break;
                    }
                }
            }
            if (checkB)
            {
                XtraMessageBox.Show("Tên khoa \"" + txtTKhoa.EditValue.ToString() + "\" đã tồn tại\r\nVui lòng nhập tên khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLM.PerformClick();
                return;
            }
            if (XtraMessageBox.Show("Bạn có chắc chắn muốn sửa khoa đang chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sqlU = "update KHOA set TENKHOA = N'" + txtTKhoa.EditValue.ToString() + "' where MAKHOA = '" + txtMKhoa.EditValue.ToString() + "'";
                if (con.exeData(sqlU))
                {
                    loadData();
                    XtraMessageBox.Show("Sửa khoa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLM.PerformClick();
                }
                else
                {
                    XtraMessageBox.Show("Sửa khoa thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMKhoa.EditValue == null)
            {
                XtraMessageBox.Show("Bạn chưa chọn khoa để xoá\r\nVui lòng chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (XtraMessageBox.Show("Bạn có chắc chắn muốn xoá khoa đang chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sqlD = "delete from KHOA where MAKHOA = '" + txtMKhoa.EditValue.ToString() + "'";
                if (con.exeData(sqlD))
                {
                    loadData();
                    XtraMessageBox.Show("Xoá khoa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLM.PerformClick();
                }
                else
                {
                    XtraMessageBox.Show("Xoá khoa thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void btnLM_Click(object sender, EventArgs e)
        {
            txtMKhoa.EditValue = null;
            txtTKhoa.EditValue = null;
            txtMKhoa.Focus();
        }

        private void gcKhoa_MouseCaptureChanged(object sender, EventArgs e)
        {
            int row_index = gvKhoa.FocusedRowHandle;
            string colID = "MAKHOA";
            string colName = "TENKHOA";
            if ((gvKhoa.GetRowCellValue(row_index, colID) != null) && (gvKhoa.GetRowCellValue(row_index, colName) != null))
            {
                txtMKhoa.EditValue = gvKhoa.GetRowCellValue(row_index, colID).ToString();
                txtTKhoa.EditValue = gvKhoa.GetRowCellValue(row_index, colName).ToString();
            }
        }
    }
}