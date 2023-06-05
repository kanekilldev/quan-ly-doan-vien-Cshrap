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
    public partial class FrmDanToc : DevExpress.XtraEditors.XtraForm
    {
        public FrmDanToc()
        {
            InitializeComponent();
        }
        Con_CRUD con = new Con_CRUD();
        string sqlR = "select * from DANTOC";

        private void loadData()
        {
            DataTable dt = con.readData(sqlR);
            if (dt != null)
            {
                gcDT.DataSource = dt;
            }
        }

        private void FrmDanToc_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if ((txtTDT.EditValue == null) || (txtTDT.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Bạn chưa nhập tên dân tộc\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTDT.Focus();
                return;
            }
            bool checkB = false;
            string sql = "select TENDANTOC from DANTOC where TENDANTOC = N'" + txtTDT.EditValue.ToString().Trim() + "'";
            DataTable dt = new DataTable();
            dt = con.readData(sql);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (txtTDT.EditValue.ToString().Trim().Equals(dr["TENDANTOC"].ToString()))
                    {
                        checkB = true;
                        break;
                    }
                }
            }
            if (checkB)
            {
                XtraMessageBox.Show("Tên dân tộc \"" + txtTDT.EditValue.ToString() + "\" đã tồn tại\r\nVui lòng nhập tên khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLM.PerformClick();
                return;
            }
            string sqlC = "insert into DANTOC values ('" + con.creatId("DT", sqlR) + "', N'" + txtTDT.EditValue.ToString() + "')";
            if (con.exeData(sqlC))
            {
                loadData();
                XtraMessageBox.Show("Thêm dân tộc thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLM.PerformClick();
            }
            else
            {
                XtraMessageBox.Show("Thêm dân tộc thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMDT.EditValue == null)
            {
                XtraMessageBox.Show("Bạn chưa chọn dân tộc để sửa\r\nVui lòng chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if ((txtTDT.EditValue == null) || (txtTDT.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Tên dân tôc không được phép để trống\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTDT.Focus();
                return;
            }
            bool checkB = false;
            string sql = "select TENDANTOC from DANTOC where TENDANTOC = N'" + txtTDT.EditValue.ToString().Trim() + "'";
            DataTable dt = new DataTable();
            dt = con.readData(sql);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (txtTDT.EditValue.ToString().Trim().Equals(dr["TENDANTOC"].ToString()))
                    {
                        checkB = true;
                        break;
                    }
                }
            }
            if (checkB)
            {
                XtraMessageBox.Show("Tên dân tộc \"" + txtTDT.EditValue.ToString() + "\" đã tồn tại\r\nVui lòng nhập tên khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLM.PerformClick();
                return;
            }
            if (XtraMessageBox.Show("Bạn có chắc chắn muốn sửa dân tộc đang chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sqlU = "update DANTOC set TENDANTOC = N'" + txtTDT.EditValue.ToString() + "' where MADANTOC = '" + txtMDT.EditValue.ToString() + "'";
                if (con.exeData(sqlU))
                {
                    loadData();
                    XtraMessageBox.Show("Sửa dân tộc thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLM.PerformClick();
                }
                else
                {
                    XtraMessageBox.Show("Sửa dân tộc thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMDT.EditValue == null)
            {
                XtraMessageBox.Show("Bạn chưa chọn dân tộc để xoá\r\nVui lòng chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (XtraMessageBox.Show("Bạn có chắc chắn muốn xoá dân tộc đang chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sqlD = "delete from DANTOC where MADANTOC = '" + txtMDT.EditValue.ToString() + "'";
                if (con.exeData(sqlD))
                {
                    loadData();
                    XtraMessageBox.Show("Xoá dân tộc thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLM.PerformClick();
                }
                else
                {
                    XtraMessageBox.Show("Xoá dân tộc thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnLM_Click(object sender, EventArgs e)
        {
            txtMDT.EditValue = null;
            txtTDT.EditValue = null;
            txtMDT.Focus();
        }

        private void gcDT_MouseCaptureChanged(object sender, EventArgs e)
        {
            int row_index = gvDT.FocusedRowHandle;
            string colID = "MADANTOC";
            string colName = "TENDANTOC";
            if ((gvDT.GetRowCellValue(row_index, colID) != null) && (gvDT.GetRowCellValue(row_index, colName) != null))
            {
                txtMDT.EditValue = gvDT.GetRowCellValue(row_index, colID).ToString();
                txtTDT.EditValue = gvDT.GetRowCellValue(row_index, colName).ToString();
            }
        }
    }
}