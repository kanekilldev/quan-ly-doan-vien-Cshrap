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
    public partial class FrmNamHoc : DevExpress.XtraEditors.XtraForm
    {
        public FrmNamHoc()
        {
            InitializeComponent();
        }
        Con_CRUD con = new Con_CRUD();
        string sqlR = "select * from NAMHOC";

        private void loadData()
        {
            DataTable dt = con.readData(sqlR);
            if (dt != null)
            {
                gcNH.DataSource = dt;
            }
        }
        private void FrmNamHoc_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if ((txtTCC.EditValue == null) || (txtTCC.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Bạn chưa nhập tên năm học\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTCC.Focus();
                return;
            }
            bool checkB = false;
            string sql = "select TENNAMHOC from NAMHOC where TENNAMHOC = N'" + txtTCC.EditValue.ToString().Trim() + "'";
            DataTable dt = new DataTable();
            dt = con.readData(sql);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (txtTCC.EditValue.ToString().Trim().Equals(dr["TENNAMHOC"].ToString()))
                    {
                        checkB = true;
                        break;
                    }
                }
            }
            if (checkB)
            {
                XtraMessageBox.Show("Tên năm học\"" + txtTCC.EditValue.ToString() + "\" đã tồn tại\r\nVui lòng nhập tên khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLM.PerformClick();
                return;
            }
            string sqlC = "insert into NAMHOC values ('" + con.creatId("NH", sqlR) + "', N'" + txtTCC.EditValue.ToString() + "')";
            if (con.exeData(sqlC))
            {
                loadData();
                XtraMessageBox.Show("Thêm năm học thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLM.PerformClick();
            }
            else
            {
                XtraMessageBox.Show("Thêm năm học thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMCC.EditValue == null)
            {
                XtraMessageBox.Show("Bạn chưa chọn năm học để sửa\r\nVui lòng chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if ((txtTCC.EditValue == null) || (txtTCC.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Tên năm học không được phép để trống\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTCC.Focus();
                return;
            }
            bool checkB = false;
            string sql = "select TENNAMHOC from NAMHOC where TENNAMHOC = N'" + txtTCC.EditValue.ToString().Trim() + "'";
            DataTable dt = new DataTable();
            dt = con.readData(sql);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (txtTCC.EditValue.ToString().Trim().Equals(dr["TENNAMHOC"].ToString()))
                    {
                        checkB = true;
                        break;
                    }
                }
            }
            if (checkB)
            {
                XtraMessageBox.Show("Tên năm học \"" + txtTCC.EditValue.ToString() + "\" đã tồn tại\r\nVui lòng nhập tên khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLM.PerformClick();
                return;
            }
            if (XtraMessageBox.Show("Bạn có chắc chắn muốn sửa năm học đang chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sqlU = "update NAMHOC set TENNAMHOC = N'" + txtTCC.EditValue.ToString() + "' where MANAMHOC = '" + txtMCC.EditValue.ToString() + "'";
                if (con.exeData(sqlU))
                {
                    loadData();
                    XtraMessageBox.Show("Sửa năm học thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLM.PerformClick();
                }
                else
                {
                    XtraMessageBox.Show("Sửa năm học thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMCC.EditValue == null)
            {
                XtraMessageBox.Show("Bạn chưa chọn chứng chỉ ngoại ngữ để xoá\r\nVui lòng chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (XtraMessageBox.Show("Bạn có chắc chắn muốn xoá chứng chỉ ngoại ngữ đang chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sqlD = "delete from NAMHOC where MANAMHOC = '" + txtMCC.EditValue.ToString() + "'";
                if (con.exeData(sqlD))
                {
                    loadData();
                    XtraMessageBox.Show("Xoá năm học thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLM.PerformClick();
                }
                else
                {
                    XtraMessageBox.Show("Xoá năm học thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnLM_Click(object sender, EventArgs e)
        {
            txtMCC.EditValue = null;
            txtTCC.EditValue = null;
            txtMCC.Focus();
        }

        private void gcNH_MouseCaptureChanged(object sender, EventArgs e)
        {
            int row_index = gvNH.FocusedRowHandle;
            string colID = "MANAMHOC";
            string colName = "TENNAMHOC";
            if ((gvNH.GetRowCellValue(row_index, colID) != null) && (gvNH.GetRowCellValue(row_index, colName) != null))
            {
                txtMCC.EditValue = gvNH.GetRowCellValue(row_index, colID).ToString();
                txtTCC.EditValue = gvNH.GetRowCellValue(row_index, colName).ToString();
            }
        }
    }
}