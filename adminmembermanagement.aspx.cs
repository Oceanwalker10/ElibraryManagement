using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ElibraryManagement
{
	public partial class adminmembermanagement : System.Web.UI.Page
	{
		string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
		protected void Page_Load(object sender, EventArgs e)
		{
			MemberTable.DataBind();
		}

		protected void LinkButton_Go_Click(object sender, EventArgs e)
		{
			GetMemberById();
		}

		protected void LinkButton_Active_Click(object sender, EventArgs e)
		{
			UpdateMemberStatusById("active");
		}

		protected void LinkButton_Pending_Click(object sender, EventArgs e)
		{
			UpdateMemberStatusById("pending");
		}

		protected void LinkButton_Deactive_Click(object sender, EventArgs e)
		{
			UpdateMemberStatusById("deactive");
		}

		protected void Button_DeleteUser_Click(object sender, EventArgs e)
		{
			DeleteMemberById();
		}

		void GetMemberById() {
			try {
				SqlConnection con = new SqlConnection(strcon);
				if(con.State == ConnectionState.Closed) {
					con.Open();
				}
				SqlCommand cmd = new SqlCommand("Select * From dbo.member_master_tbl Where member_id='"
				+ TextBox_MemberId.Text.Trim() + "';", con);
				SqlDataReader dataReader = cmd.ExecuteReader();
				if(dataReader.HasRows) {
					while(dataReader.Read()) {
						TextBox_FullName.Text = dataReader.GetValue(1).ToString();
						TextBox_AccountStatus.Text = dataReader.GetValue(10).ToString();
						TextBox_DOB.Text = dataReader.GetValue(2).ToString();
						TextBox_ContactNo.Text = dataReader.GetValue(3).ToString();
						TextBox_EmailID.Text = dataReader.GetValue(4).ToString();
						TextBox_State.Text = dataReader.GetValue(5).ToString();
						TextBox_City.Text = dataReader.GetValue(6).ToString();
						TextBox_ZipCode.Text = dataReader.GetValue(7).ToString();
						TextBox_Address.Text = dataReader.GetValue(8).ToString();
					}
				} else {
					Response.Write("<script>alert('Invalid credentails');</script>");
				} 
			} catch(Exception ex) {
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}

		void UpdateMemberStatusById(string status) {
			if(CheckIfMemberExists()) {
				try {
					SqlConnection con = new SqlConnection(strcon);
					if(con.State == ConnectionState.Closed) {
						con.Open();
					}
					SqlCommand cmd = new SqlCommand("Update dbo.member_master_tbl Set account_status='"
					+ status + "' Where member_id='" + TextBox_MemberId.Text.Trim() + "'", con);
					cmd.ExecuteNonQuery();
					con.Close();
					MemberTable.DataBind();
					Response.Write("<script>alert('Member Status Updated');</script>");
				} catch(Exception ex) {
					Response.Write("<script>alert('" + ex.Message + "');</script>");
				}
			} else {
				Response.Write("<script>alert('Invalid Member Id');</script>");
			}
		}

		void DeleteMemberById() {
			if(CheckIfMemberExists()) {
				try {
					SqlConnection con = new SqlConnection(strcon);
					if(con.State == ConnectionState.Closed) {
						con.Open();
					}

					SqlCommand cmd = new SqlCommand("Delete From dbo.member_master_tbl Where member_id='" 
					+ TextBox_MemberId.Text.Trim() + "'", con);

					cmd.ExecuteNonQuery();
					con.Close();
					Response.Write("<script>alert('Member Deleted Successfully');</script>");
					ClearForm();
					MemberTable.DataBind();
				} catch(Exception ex) {
					Response.Write("<script>alert('" + ex.Message + "');</script>");
				}
			} else {
				Response.Write("<script>alert('Invalid Member Id');</script>");
			}
		}

		bool CheckIfMemberExists() {
			try {
				SqlConnection con = new SqlConnection(strcon);
				if(con.State == ConnectionState.Closed) {
					con.Open();
				}

				SqlCommand cmd = new SqlCommand("Select * From dbo.member_master_tbl where member_id='"
				+ TextBox_MemberId.Text.Trim() + "';", con);
				SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
				DataTable dataTable = new DataTable();
				dataAdapter.Fill(dataTable);

				if(dataTable.Rows.Count >= 1) {
					return true;
				} else {
					return false;
				}
			} catch(Exception ex) {
				Response.Write("<script>alert('" + ex.Message + "');</script>");
				return false;
			}
		}

		void ClearForm() {
			TextBox_MemberId.Text = "";
			TextBox_FullName.Text = "";
			TextBox_AccountStatus.Text = "";
			TextBox_DOB.Text = "";
			TextBox_ContactNo.Text = "";
			TextBox_EmailID.Text = "";
			TextBox_State.Text = "";
			TextBox_City.Text = "";
			TextBox_ZipCode.Text = "";
			TextBox_Address.Text = "";
		}

	}
}