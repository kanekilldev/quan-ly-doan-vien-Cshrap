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
    public partial class FrmTonGiao : DevExpress.XtraEditors.XtraForm
    {
        public FrmTonGiao()
        {
            InitializeComponent();
        }
        Con_CRUD con = new Con_CRUD();
        string sqlR = "select * from TONGIAO";

        private void loadData()
        {
            DataTable dt = con.readData(sqlR);
            if (dt != null)
            {
                gcTG.DataSource = dt;
            }
        }
        private void FrmTonGiao_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if ((txtTTG.EditValue == null) || (txtTTG.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Bạn chưa nhập tên tôn giáo\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTTG.Focus();
                return;
            }
            bool checkB = false;
            string sql = "select TENTONGIAO from TONGIAO where TENTONGIAO = N'" + txtTTG.EditValue.ToString().Trim() + "'";
            DataTable dt = new DataTable();
            dt = con.readData(sql);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (txtTTG.EditValue.ToString().Trim().Equals(dr["TENTONGIAO"].ToString()))
                    {
                        checkB = true;
                        break;
                    }
                }
            }
            if (checkB)
            {
                XtraMessageBox.Show("Tên tôn giáo \"" + txtTTG.EditValue.ToString() + "\" đã tồn tại\r\nVui lòng nhập tên khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLM.PerformClick();
                return;
            }
            string sqlC = "insert into TONGIAO values ('" + con.creatId("TG", sqlR) + "', N'" + txtTTG.EditValue.ToString() + "')";
            if (con.exeData(sqlC))
            {
                loadData();
                XtraMessageBox.Show("Thêm tôn giáo thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLM.PerformClick();
            }
            else
            {
                XtraMessageBox.Show("Thêm tôn giáo thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMTG.EditValue == null)
            {
                XtraMessageBox.Show("Bạn chưa chọn tôn giáo để sửa\r\nVui lòng chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if ((txtTTG.EditValue == null) || (txtTTG.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Tên dân tôc không được phép để trống\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTTG.Focus();
                return;
            }
            bool checkB = false;
            string sql = "select TENTONGIAO from TONGIAO where TENTONGIAO = N'" + txtTTG.EditValue.ToString().Trim() + "'";
            DataTable dt = new DataTable();
            dt = con.readData(sql);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (txtTTG.EditValue.ToString().Trim().Equals(dr["TENTONGIAO"].ToString()))
                    {
                        checkB = true;
                        break;
                    }
                }
            }
            if (checkB)
            {
                XtraMessageBox.Show("Tên tôn giáo \"" + txtTTG.EditValue.ToString() + "\" đã tồn tại\r\nVui lòng nhập tên khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLM.PerformClick();
                return;
            }
            if (XtraMessageBox.Show("Bạn có chắc chắn muốn sửa tôn giáo đang chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sqlU = "update TONGIAO set TENTONGIAO = N'" + txtTTG.EditValue.ToString() + "' where MATONGIAO = '" + txtMTG.EditValue.ToString() + "'";
                if (con.exeData(sqlU))
                {
                    loadData();
                    XtraMessageBox.Show("Sửa tôn giáo thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLM.PerformClick();
                }
                else
                {
                    XtraMessageBox.Show("Sửa tôn giáo thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMTG.EditValue == null)
            {
                XtraMessageBox.Show("Bạn chưa chọn tôn giáo để xoá\r\nVui lòng chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (XtraMessageBox.Show("Bạn có chắc chắn muốn xoá tôn giáo đang chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sqlD = "delete from TONGIAO where MATONGIAO = '" + txtMTG.EditValue.ToString() + "'";
                if (con.exeData(sqlD))
                {
                    loadData();
                    XtraMessageBox.Show("Xoá tôn giáo thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLM.PerformClick();
                }
                else
                {
                    XtraMessageBox.Show("Xoá tôn giáo thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnLM_Click(object sender, EventArgs e)
        {
            txtMTG.EditValue = null;
            txtTTG.EditValue = null;
            txtMTG.Focus();
        }

        private void gcTG_MouseCaptureChanged(object sender, EventArgs e)
        {
            int row_index = gvTG.FocusedRowHandle;
            string colID = "MATONGIAO";
            string colName = "TENTONGIAO";
            if ((gvTG.GetRowCellValue(row_index, colID) != null) && (gvTG.GetRowCellValue(row_index, colName) != null))
            {
                txtMTG.EditValue = gvTG.GetRowCellValue(row_index, colID).ToString();
                txtTTG.EditValue = gvTG.GetRowCellValue(row_index, colName).ToString();
            }
        }
    }
}