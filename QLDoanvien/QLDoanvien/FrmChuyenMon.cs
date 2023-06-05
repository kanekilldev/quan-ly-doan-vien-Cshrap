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
    public partial class FrmChuyenMon : DevExpress.XtraEditors.XtraForm
    {
        public FrmChuyenMon()
        {
            InitializeComponent();
        }
        Con_CRUD con = new Con_CRUD();
        string sqlR = "select * from TRDCHUYENMON";

        private void loadData()
        {
            DataTable dt = con.readData(sqlR);
            if (dt != null)
            {
                gcTDCM.DataSource = dt;
            }
        }
        private void FrmChuyenMon_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if ((txtTCM.EditValue == null) || (txtTCM.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Bạn chưa nhập tên trình độ chuyên môn\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTCM.Focus();
                return;
            }
            bool checkB = false;
            string sql = "select TENTRDCHUYENMON from TRDCHUYENMON where TENTRDCHUYENMON = N'" + txtTCM.EditValue.ToString().Trim() + "'";
            DataTable dt = new DataTable();
            dt = con.readData(sql);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (txtTCM.EditValue.ToString().Trim().Equals(dr["TENTRDCHUYENMON"].ToString()))
                    {
                        checkB = true;
                        break;
                    }
                }
            }
            if (checkB)
            {
                XtraMessageBox.Show("Tên trình độ chuyên môn \"" + txtTCM.EditValue.ToString() + "\" đã tồn tại\r\nVui lòng nhập tên khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLM.PerformClick();
                return;
            }
            string sqlC = "insert into TRDCHUYENMON values ('" + con.creatId("TDCM", sqlR) + "', N'" + txtTCM.EditValue.ToString() + "')";
            if (con.exeData(sqlC))
            {
                loadData();
                XtraMessageBox.Show("Thêm trình độ chuyên môn thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLM.PerformClick();
            }
            else
            {
                XtraMessageBox.Show("Thêm trình độ chuyên môn thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMCM.EditValue == null)
            {
                XtraMessageBox.Show("Bạn chưa chọn trình độ chuyên môn để sửa\r\nVui lòng chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if ((txtTCM.EditValue == null) || (txtTCM.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Tên trình độ chuyên môn không được phép để trống\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTCM.Focus();
                return;
            }
            bool checkB = false;
            string sql = "select TENTRDCHUYENMON from TRDCHUYENMON where TENTRDCHUYENMON = N'" + txtTCM.EditValue.ToString().Trim() + "'";
            DataTable dt = new DataTable();
            dt = con.readData(sql);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (txtTCM.EditValue.ToString().Trim().Equals(dr["TENTRDCHUYENMON"].ToString()))
                    {
                        checkB = true;
                        break;
                    }
                }
            }
            if (checkB)
            {
                XtraMessageBox.Show("Tên trình độ chuyên môn \"" + txtTCM.EditValue.ToString() + "\" đã tồn tại\r\nVui lòng nhập tên khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLM.PerformClick();
                return;
            }
            if (XtraMessageBox.Show("Bạn có chắc chắn muốn sửa trình độ chuyên môn đang chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sqlU = "update TRDCHUYENMON set TENTRDCHUYENMON = N'" + txtTCM.EditValue.ToString() + "' where MATRDCHUYENMON = '" + txtMCM.EditValue.ToString() + "'";
                if (con.exeData(sqlU))
                {
                    loadData();
                    XtraMessageBox.Show("Sửa trình độ chuyên môn thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLM.PerformClick();
                }
                else
                {
                    XtraMessageBox.Show("Sửa trình độ chuyên môn thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMCM.EditValue == null)
            {
                XtraMessageBox.Show("Bạn chưa chọn trình độ chuyên môn để xoá\r\nVui lòng chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (XtraMessageBox.Show("Bạn có chắc chắn muốn xoá trình độ chuyên môn đang chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sqlD = "delete from TRDCHUYENMON where MATRDCHUYENMON = '" + txtMCM.EditValue.ToString() + "'";
                if (con.exeData(sqlD))
                {
                    loadData();
                    XtraMessageBox.Show("Xoá tình độ chuyên môn thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLM.PerformClick();
                }
                else
                {
                    XtraMessageBox.Show("Xoá trình độ chuyên môn thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnLM_Click(object sender, EventArgs e)
        {
            txtMCM.EditValue = null;
            txtTCM.EditValue = null;
            txtMCM.Focus();
        }

        private void gcTDCM_MouseCaptureChanged(object sender, EventArgs e)
        {
            int row_index = gvTDCM.FocusedRowHandle;
            string colID = "MATRDCHUYENMON";
            string colName = "TENTRDCHUYENMON";
            if ((gvTDCM.GetRowCellValue(row_index, colID) != null) && (gvTDCM.GetRowCellValue(row_index, colName) != null))
            {
                txtMCM.EditValue = gvTDCM.GetRowCellValue(row_index, colID).ToString();
                txtTCM.EditValue = gvTDCM.GetRowCellValue(row_index, colName).ToString();
            }
        }
    }
}