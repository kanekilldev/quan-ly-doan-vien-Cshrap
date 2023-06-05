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
    public partial class FrmChucVu : DevExpress.XtraEditors.XtraForm
    {
        public FrmChucVu()
        {
            InitializeComponent();
        }
        Con_CRUD con = new Con_CRUD();
        string sqlR = "select * from CHUCVU";

        private void loadData()
        {
            DataTable dt = con.readData(sqlR);
            if (dt != null)
            {
                gcCV.DataSource = dt;
            }
        }
        private void FrmChucVu_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if ((txtTCV.EditValue == null) || (txtTCV.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Bạn chưa nhập tên chức vụ\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTCV.Focus();
                return;
            }
            bool checkB = false;
            string sql = "select TENCHUCVU from CHUCVU where TENCHUCVU = N'" + txtTCV.EditValue.ToString().Trim() + "'";
            DataTable dt = new DataTable();
            dt = con.readData(sql);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (txtTCV.EditValue.ToString().Trim().Equals(dr["TENCHUCVU"].ToString()))
                    {
                        checkB = true;
                        break;
                    }
                }
            }
            if (checkB)
            {
                XtraMessageBox.Show("Tên chức vụ\"" + txtTCV.EditValue.ToString() + "\" đã tồn tại\r\nVui lòng nhập tên khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLM.PerformClick();
                return;
            }
            string sqlC = "insert into CHUCVU values ('" + con.creatId("CV", sqlR) + "', N'" + txtTCV.EditValue.ToString() + "')";
            if (con.exeData(sqlC))
            {
                loadData();
                XtraMessageBox.Show("Thêm chức vụ thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLM.PerformClick();
            }
            else
            {
                XtraMessageBox.Show("Thêm chức vụ thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMCV.EditValue == null)
            {
                XtraMessageBox.Show("Bạn chưa chọn chức vụ để sửa\r\nVui lòng chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if ((txtTCV.EditValue == null) || (txtTCV.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Tên chức vụ không được phép để trống\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTCV.Focus();
                return;
            }
            bool checkB = false;
            string sql = "select TENCHUCVU from CHUCVU where TENCHUCVU = N'" + txtTCV.EditValue.ToString().Trim() + "'";
            DataTable dt = new DataTable();
            dt = con.readData(sql);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (txtTCV.EditValue.ToString().Trim().Equals(dr["TENCHUCVU"].ToString()))
                    {
                        checkB = true;
                        break;
                    }
                }
            }
            if (checkB)
            {
                XtraMessageBox.Show("Tên chức vụ \"" + txtTCV.EditValue.ToString() + "\" đã tồn tại\r\nVui lòng nhập tên khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLM.PerformClick();
                return;
            }
            if (XtraMessageBox.Show("Bạn có chắc chắn muốn sửa chức vụ đang chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sqlU = "update CHUCVU set TENCHUCVU = N'" + txtTCV.EditValue.ToString() + "' where MACHUCVU = '" + txtMCV.EditValue.ToString() + "'";
                if (con.exeData(sqlU))
                {
                    loadData();
                    XtraMessageBox.Show("Sửa chức vụ thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLM.PerformClick();
                }
                else
                {
                    XtraMessageBox.Show("Sửa chức vụ thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMCV.EditValue == null)
            {
                XtraMessageBox.Show("Bạn chưa chọn chức vụ để xoá\r\nVui lòng chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (XtraMessageBox.Show("Bạn có chắc chắn muốn xoá chức vụ đang chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sqlD = "delete from CHUCVU where MACHUCVU = '" + txtMCV.EditValue.ToString() + "'";
                if (con.exeData(sqlD))
                {
                    loadData();
                    XtraMessageBox.Show("Xoá chức vụ thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLM.PerformClick();
                }
                else
                {
                    XtraMessageBox.Show("Xoá chức vụ thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnLM_Click(object sender, EventArgs e)
        {
            txtMCV.EditValue = null;
            txtTCV.EditValue = null;
            txtMCV.Focus();
        }

        private void gcCV_MouseCaptureChanged(object sender, EventArgs e)
        {
            int row_index = gvCV.FocusedRowHandle;
            string colID = "MACHUCVU";
            string colName = "TENCHUCVU";
            if ((gvCV.GetRowCellValue(row_index, colID) != null) && (gvCV.GetRowCellValue(row_index, colName) != null))
            {
                txtMCV.EditValue = gvCV.GetRowCellValue(row_index, colID).ToString();
                txtTCV.EditValue = gvCV.GetRowCellValue(row_index, colName).ToString();
            }
        }
    }
}