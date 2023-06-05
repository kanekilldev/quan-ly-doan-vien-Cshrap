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
    public partial class FrmCCAV : DevExpress.XtraEditors.XtraForm
    {
        public FrmCCAV()
        {
            InitializeComponent();
        }
        Con_CRUD con = new Con_CRUD();
        string sqlR = "select * from CCANHVAN";

        private void loadData()
        {
            DataTable dt = con.readData(sqlR);
            if (dt != null)
            {
                gcCC.DataSource = dt;
            }
        }
        private void FrmCCAV_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if ((txtTCC.EditValue == null) || (txtTCC.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Bạn chưa nhập tên chứng chỉ ngoại ngữ\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTCC.Focus();
                return;
            }
            bool checkB = false;
            string sql = "select TENCCANHVAN from CCANHVAN where TENCCANHVAN = N'" + txtTCC.EditValue.ToString().Trim() + "'";
            DataTable dt = new DataTable();
            dt = con.readData(sql);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (txtTCC.EditValue.ToString().Trim().Equals(dr["TENCCANHVAN"].ToString()))
                    {
                        checkB = true;
                        break;
                    }
                }
            }
            if (checkB)
            {
                XtraMessageBox.Show("Tên chứng chỉ ngoại ngữ\"" + txtTCC.EditValue.ToString() + "\" đã tồn tại\r\nVui lòng nhập tên khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLM.PerformClick();
                return;
            }
            string sqlC = "insert into CCANHVAN values ('" + con.creatId("CCNN", sqlR) + "', N'" + txtTCC.EditValue.ToString() + "')";
            if (con.exeData(sqlC))
            {
                loadData();
                XtraMessageBox.Show("Thêm chứng chỉ ngoại ngữ thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLM.PerformClick();
            }
            else
            {
                XtraMessageBox.Show("Thêm chứng chỉ ngoại ngữ thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMCC.EditValue == null)
            {
                XtraMessageBox.Show("Bạn chưa chọn chứng chỉ ngoại ngữ để sửa\r\nVui lòng chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if ((txtTCC.EditValue == null) || (txtTCC.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Tên chứng chỉ ngoại ngữ không được phép để trống\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTCC.Focus();
                return;
            }
            bool checkB = false;
            string sql = "select TENCCANHVAN from CCANHVAN where TENCCANHVAN = N'" + txtTCC.EditValue.ToString().Trim() + "'";
            DataTable dt = new DataTable();
            dt = con.readData(sql);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (txtTCC.EditValue.ToString().Trim().Equals(dr["TENCCANHVAN"].ToString()))
                    {
                        checkB = true;
                        break;
                    }
                }
            }
            if (checkB)
            {
                XtraMessageBox.Show("Tên chứng chỉ ngoại ngữ \"" + txtTCC.EditValue.ToString() + "\" đã tồn tại\r\nVui lòng nhập tên khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLM.PerformClick();
                return;
            }
            if (XtraMessageBox.Show("Bạn có chắc chắn muốn sửa chứng chỉ ngoại ngữ đang chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sqlU = "update CCANHVAN set TENCCANHVAN = N'" + txtTCC.EditValue.ToString() + "' where MACCANHVAN = '" + txtMCC.EditValue.ToString() + "'";
                if (con.exeData(sqlU))
                {
                    loadData();
                    XtraMessageBox.Show("Sửa chứng chỉ ngoại ngữ thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLM.PerformClick();
                }
                else
                {
                    XtraMessageBox.Show("Sửa chứng chỉ ngoại ngữ thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                string sqlD = "delete from CCANHVAN where MACCANHVAN = '" + txtMCC.EditValue.ToString() + "'";
                if (con.exeData(sqlD))
                {
                    loadData();
                    XtraMessageBox.Show("Xoá chứng chỉ ngoại ngữ thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLM.PerformClick();
                }
                else
                {
                    XtraMessageBox.Show("Xoá chứng chỉ ngoại ngữ thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnLM_Click(object sender, EventArgs e)
        {
            txtMCC.EditValue = null;
            txtTCC.EditValue = null;
            txtMCC.Focus();
        }

        private void gcCC_MouseCaptureChanged(object sender, EventArgs e)
        {
            int row_index = gvCCAV.FocusedRowHandle;
            string colID = "MACCANHVAN";
            string colName = "TENCCANHVAN";
            if ((gvCCAV.GetRowCellValue(row_index, colID) != null) && (gvCCAV.GetRowCellValue(row_index, colName) != null))
            {
                txtMCC.EditValue = gvCCAV.GetRowCellValue(row_index, colID).ToString();
                txtTCC.EditValue = gvCCAV.GetRowCellValue(row_index, colName).ToString();
            }
        }
    }
}