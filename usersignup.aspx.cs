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
	public partial class usersignup : System.Web.UI.Page
	{
		string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		protected void Button1_Click(object sender, EventArgs e)
		{
			if (checkMemberExists())
			{
				Response.Write("<script>alert('Member Already Exist with this Member ID, try other ID');</script>");
			}
			else
			{
				signUpNewMember();
			}
		}

		bool checkMemberExists()
		{
			try
			{
				SqlConnection con = new SqlConnection(strcon);
				if (con.State == ConnectionState.Closed)
				{
					con.Open();
				}
				SqlCommand cmd = new SqlCommand("SELECT * from dbo.member_master_tbl where member_id='" + TextBox8.Text.Trim() + "';", con);
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

		void signUpNewMember()
		{
			try
			{
				SqlConnection con = new SqlConnection(strcon);
				if (con.State == ConnectionState.Closed)
				{
					con.Open();
				}
				SqlCommand cmd = new SqlCommand("INSERT INTO dbo.member_master_tbl" +
				"(member_id,full_name,dob,contact_no,email,state,city,zip_code,full_address," +
				"password,account_status) values(@member_id,@full_name,@dob,@contact_no,@email,@state," +
				"@city,@zip_code,@full_address,@password,@account_status)", con);
				cmd.Parameters.AddWithValue("@full_name", TextBox1.Text.Trim());
				cmd.Parameters.AddWithValue("@dob", TextBox2.Text.Trim());
				cmd.Parameters.AddWithValue("@contact_no", TextBox3.Text.Trim());
				cmd.Parameters.AddWithValue("@email", TextBox4.Text.Trim());
				cmd.Parameters.AddWithValue("@state", DropDownList1.SelectedItem.Value);
				cmd.Parameters.AddWithValue("@city", TextBox5.Text.Trim());
				cmd.Parameters.AddWithValue("@zip_code", TextBox6.Text.Trim());
				cmd.Parameters.AddWithValue("@full_address", TextBox7.Text.Trim());
				cmd.Parameters.AddWithValue("@member_id", TextBox8.Text.Trim());
				cmd.Parameters.AddWithValue("@password", TextBox9.Text.Trim());
				cmd.Parameters.AddWithValue("@account_status", "pending");
				cmd.ExecuteNonQuery();
				con.Close();
				Response.Write("<script>alert('Sign Up Successful. Go to User Login to Login');</script>");
			}
			catch (Exception ex)
			{
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}
	}
}