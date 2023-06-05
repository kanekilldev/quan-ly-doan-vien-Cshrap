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
    public partial class FrmTinh : DevExpress.XtraEditors.XtraForm
    {
        public FrmTinh()
        {
            InitializeComponent();
        }
        Con_CRUD con = new Con_CRUD();
        string sqlR = "select * from TINH";

        private void loadData()
        {
            DataTable dt = con.readData(sqlR);
            if (dt != null)
            {
                gcTinh.DataSource = dt;
            }
        }
        private void FrmTinh_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if ((txtTTinh.EditValue == null) || (txtTTinh.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Bạn chưa nhập tên tỉnh\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTTinh.Focus();
                return;
            }
            bool checkB = false;
            string sql = "select TENTINH from TINH where TENTINH = N'" + txtTTinh.EditValue.ToString().Trim() + "'";
            DataTable dt = new DataTable();
            dt = con.readData(sql);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (txtTTinh.EditValue.ToString().Trim().Equals(dr["TENTINH"].ToString()))
                    {
                        checkB = true;
                        break;
                    }
                }
            }
            if (checkB)
            {
                XtraMessageBox.Show("Tên tỉnh \"" + txtTTinh.EditValue.ToString() + "\" đã tồn tại\r\nVui lòng nhập tên khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLM.PerformClick();
                return;
            }
            string sqlC = "insert into TINH values ('" + con.creatId("TT", sqlR) + "', N'" + txtTTinh.EditValue.ToString() + "')";
            if (con.exeData(sqlC))
            {
                loadData();
                XtraMessageBox.Show("Thêm tỉnh thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLM.PerformClick();
            }
            else
            {
                XtraMessageBox.Show("Thêm tỉnh thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMTinh.EditValue == null)
            {
                XtraMessageBox.Show("Bạn chưa chọn tỉnh để sửa\r\nVui lòng chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if ((txtTTinh.EditValue == null) || (txtTTinh.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Tên tỉnh không được phép để trống\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTTinh.Focus();
                return;
            }
            bool checkB = false;
            string sql = "select TENTINH from TINH where TENTINH = N'" + txtTTinh.EditValue.ToString().Trim() + "'";
            DataTable dt = new DataTable();
            dt = con.readData(sql);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (txtTTinh.EditValue.ToString().Trim().Equals(dr["TENTINH"].ToString()))
                    {
                        checkB = true;
                        break;
                    }
                }
            }

            if (checkB)
            {
                XtraMessageBox.Show("Tên tỉnh \"" + txtTTinh.EditValue.ToString() + "\" đã tồn tại\r\nVui lòng nhập tên khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLM.PerformClick();
                return;
            }
            if (XtraMessageBox.Show("Bạn có chắc chắn muốn sửa tỉnh đang chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sqlU = "update TINH set TENTINH = N'" + txtTTinh.EditValue.ToString() + "' where MATINH = '" + txtMTinh.EditValue.ToString() + "'";
                if (con.exeData(sqlU))
                {
                    loadData();
                    XtraMessageBox.Show("Sửa tỉnh thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLM.PerformClick();
                }
                else
                {
                    XtraMessageBox.Show("Sửa tỉnh thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtTTinh.EditValue == null)
            {
                XtraMessageBox.Show("Bạn chưa chọn tỉnh để xoá\r\nVui lòng chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (XtraMessageBox.Show("Bạn có chắc chắn muốn xoá tỉnh đang chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sqlD = "delete from TINH where MATINH = '" + txtMTinh.EditValue.ToString() + "'";
                if (con.exeData(sqlD))
                {
                    loadData();
                    XtraMessageBox.Show("Xoá tỉnh thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLM.PerformClick();
                }
                else
                {
                    XtraMessageBox.Show("Xoá tỉnh thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnLM_Click(object sender, EventArgs e)
        {
            txtMTinh.EditValue = null;
            txtTTinh.EditValue = null;
            txtTTinh.Focus();
        }

        private void gcTinh_MouseCaptureChanged(object sender, EventArgs e)
        {
            int row_index = gvTinh.FocusedRowHandle;
           string colID = "MATINH";
            string colName = "TENTINH";
            if ((gvTinh.GetRowCellValue(row_index, colName) != null)&&(gvTinh.GetRowCellValue(row_index, colName) != null))
            {
                txtMTinh.EditValue = gvTinh.GetRowCellValue(row_index, colID).ToString();
                txtTTinh.EditValue = gvTinh.GetRowCellValue(row_index, colName).ToString();
            }
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}