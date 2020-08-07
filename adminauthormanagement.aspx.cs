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
	public partial class WebForm4 : System.Web.UI.Page
	{
		string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
		protected void Page_Load(object sender, EventArgs e)
		{
			AuthorTable.DataBind();
		}

		protected void Button_Go_Click(object sender, EventArgs e)
		{
			GetAuthorById();
		}

		protected void Button_Add_Click(object sender, EventArgs e)
		{
			if(CheckIfAuthorExists()) {
				Response.Write("<script>alert('Author with this ID already Exist. " +
		"You cannot add another Author with the same Author ID');</script>");
			} else {
				AddNewAuthor();
			}
		}

		protected void Button_Update_Click(object sender, EventArgs e)
		{
			if(CheckIfAuthorExists()) {
				UpdateAuthor();
			} else {
				Response.Write("<script>alert('Author does not exist');</script>");
			}
		}

		protected void Button_Delete_Click(object sender, EventArgs e)
		{
			if(CheckIfAuthorExists()) {
				DeleteAuthor();
			} else {
				Response.Write("<script>alert('Author does not exist');</script>");
			}
		}

		bool CheckIfAuthorExists() {
			try {
				SqlConnection con = new SqlConnection(strcon);
				if(con.State == ConnectionState.Closed) {
					con.Open();
				}

				SqlCommand cmd = new SqlCommand("SELECT * from dbo.author_master_tbl where author_id='" + TextBox_AuthorId.Text.Trim() + "';", con);
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

		void AddNewAuthor() {
			try{
				SqlConnection con = new SqlConnection(strcon);
				if(con.State == ConnectionState.Closed) {
					con.Open();
				}

				SqlCommand cmd = new SqlCommand("Insert Into dbo.author_master_tbl(author_id, author_name) values(@author_id, @author_name)", con);

				cmd.Parameters.AddWithValue("@author_id", TextBox_AuthorId.Text.Trim());
				cmd.Parameters.AddWithValue("@author_name", TextBox_AuthorName.Text.Trim());

				cmd.ExecuteNonQuery();
				con.Close();
				Response.Write("<script>alert('Author added Successfully');</script>");
				ClearForm();
				AuthorTable.DataBind();
			} catch(Exception ex) {
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}

		void UpdateAuthor() {
			try {
				SqlConnection con = new SqlConnection(strcon);
				if(con.State == ConnectionState.Closed) {
					con.Open();
				}

				SqlCommand cmd = new SqlCommand("Update dbo.author_master_tbl Set author_name=@author_name Where author_id='" + TextBox_AuthorId.Text.Trim() + "'", con);

				cmd.Parameters.AddWithValue("@author_name", TextBox_AuthorName.Text.Trim());

				cmd.ExecuteNonQuery();
				con.Close();
				Response.Write("<script>alert('Author Updated Successfully');</script>");
				ClearForm();
				AuthorTable.DataBind();
			} catch(Exception ex) {
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}

		void DeleteAuthor() {
			try {
				SqlConnection con = new SqlConnection(strcon);
				if(con.State == ConnectionState.Closed) {
					con.Open();
				}

				SqlCommand cmd = new SqlCommand("Delete from dbo.author_master_tbl Where author_id='" + TextBox_AuthorId.Text.Trim() + "'", con);

				cmd.ExecuteNonQuery();
				con.Close();
				Response.Write("<script>alert('Author Deleted successfully);</script>");
				ClearForm();
				AuthorTable.DataBind();
			} catch(Exception ex) {
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}

		void GetAuthorById() {
			try {
				SqlConnection con = new SqlConnection(strcon);
				if(con.State == ConnectionState.Closed) {
					con.Open();
				}

				SqlCommand cmd = new SqlCommand("Select * from dbo.author_master_tbl where author_id='" + TextBox_AuthorId.Text.Trim() + "';", con);
				SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
				DataTable dataTable = new DataTable();
				dataAdapter.Fill(dataTable);

				if(dataTable.Rows.Count >= 1) {
					TextBox_AuthorName.Text = dataTable.Rows[0][1].ToString();
				} else {
					Response.Write("<script>alert('Invalid Author ID');</script>");
				}
			} catch(Exception ex) {
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}

		void ClearForm() {
			TextBox_AuthorId.Text = "";
			TextBox_AuthorName.Text = "";
		}
	}
}