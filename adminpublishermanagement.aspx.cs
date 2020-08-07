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
	public partial class WebForm5 : System.Web.UI.Page
	{
		string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
		protected void Page_Load(object sender, EventArgs e)
		{
			PublisherTable.DataBind();
		}

		protected void Button_Go_Click(object sender, EventArgs e)
		{
			GetPublisherById();
		}

		protected void Button_Add_Click(object sender, EventArgs e)
		{
			if(CheckIfPublisherExists()) {
				Response.Write("<script>alert('Publisher Already Exist with this Id.');</script>");
			} else {
				AddNewPublisher();
			}
		}

		protected void Button_Update_Click(object sender, EventArgs e)
		{
			if(CheckIfPublisherExists()) {
				UpdatePublisherById();
			} else {
				Response.Write("<script>alert('Publisher with this Id does not exist');</script>");
			}
		}

		protected void Button_Delete_Click(object sender, EventArgs e)
		{
			if(CheckIfPublisherExists()) {
				DeletePublisherById();
			} else {
				Response.Write("<script>alert('Publisher with this Id does not exist');</script>");
			}
		}

		void GetPublisherById()
		{
			try
			{
				SqlConnection con = new SqlConnection(strcon);
				if (con.State == ConnectionState.Closed)
				{
					con.Open();
				}

				SqlCommand cmd = new SqlCommand("Select * from dbo.publisher_master_tbl where publisher_id='" + TextBox_PublisherId.Text.Trim() + "';", con);
				SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
				DataTable dataTable = new DataTable();
				dataAdapter.Fill(dataTable);

				if (dataTable.Rows.Count >= 1)
				{
					TextBox_PublisherName.Text = dataTable.Rows[0][1].ToString();
				}
				else
				{
					Response.Write("<script>alert('Publisher with this ID does not exist');</script>");
				}
			}
			catch (Exception ex)
			{
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}

		void AddNewPublisher()
		{
			try
			{
				SqlConnection con = new SqlConnection(strcon);
				if (con.State == ConnectionState.Closed)
				{
					con.Open();
				}

				SqlCommand cmd = new SqlCommand("Insert Into dbo.publisher_master_tbl(publisher_id, publisher_name) values(@publisher_id, @publisher_name)", con);

				cmd.Parameters.AddWithValue("@publisher_id", TextBox_PublisherId.Text.Trim());
				cmd.Parameters.AddWithValue("@publisher_name", TextBox_PublisherName.Text.Trim());

				cmd.ExecuteNonQuery();
				con.Close();
				Response.Write("<script>alert('Author added Successfully');</script>");
				ClearForm();
				PublisherTable.DataBind();
			}
			catch (Exception ex)
			{
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}

		void UpdatePublisherById()
		{
			try
			{
				SqlConnection con = new SqlConnection(strcon);
				if (con.State == ConnectionState.Closed)
				{
					con.Open();
				}

				SqlCommand cmd = new SqlCommand("Update dbo.publisher_master_tbl Set publisher_name=@publisher_name Where publisher_id='" + TextBox_PublisherId.Text.Trim() + "'", con);

				cmd.Parameters.AddWithValue("@publisher_name", TextBox_PublisherName.Text.Trim());
				int result = cmd.ExecuteNonQuery();
				con.Close();
				if(result > 0) {
					Response.Write("<script>alert('Publisher Updated Successfully');</script>");
					PublisherTable.DataBind();
				} else {
					Response.Write("<script>alert('Publisher Id does not Exist');</script>");
				}
			}
			catch (Exception ex)
			{
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}

		void DeletePublisherById()
		{
			try
			{
				SqlConnection con = new SqlConnection(strcon);
				if (con.State == ConnectionState.Closed)
				{
					con.Open();
				}

				SqlCommand cmd = new SqlCommand("Delete from dbo.publisher_master_tbl Where publisher_id='" + TextBox_PublisherId.Text.Trim() + "'", con);
				int result = cmd.ExecuteNonQuery();
				con.Close();
				if(result > 0) {
					Response.Write("<script>alert('Publisher Deleted Successfully');</script>");
					ClearForm();
					PublisherTable.DataBind();
				} else {
					Response.Write("<script>alert('Publisher Id does not Exist');</scriot>");
				}
			}
			catch (Exception ex)
			{
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}

		bool CheckIfPublisherExists()
		{
			try
			{
				SqlConnection con = new SqlConnection(strcon);
				if (con.State == ConnectionState.Closed)
				{
					con.Open();
				}

				SqlCommand cmd = new SqlCommand("SELECT * from dbo.publisher_master_tbl where publisher_id='" + TextBox_PublisherId.Text.Trim() + "';", con);
				SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
				DataTable dataTable = new DataTable();
				dataAdapter.Fill(dataTable);

				if (dataTable.Rows.Count >= 1)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			catch (Exception ex)
			{
				Response.Write("<script>alert('" + ex.Message + "');</script>");
				return false;
			}
		}

		void ClearForm()
		{
			TextBox_PublisherId.Text = "";
			TextBox_PublisherName.Text = "";
		}

	}
}