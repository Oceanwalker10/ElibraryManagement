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
	public partial class WebForm3 : System.Web.UI.Page
	{
		string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void Button1_Click(object sender, EventArgs e)
		{
			try {
				SqlConnection con = new SqlConnection(strcon);
				if(con.State == ConnectionState.Closed) {
					con.Open();
				}
				SqlCommand cmd = new SqlCommand("select * from dbo.admin_login_tbl where username='" + TextBox1.Text.Trim() + "' AND password='" + TextBox2.Text.Trim() + "'", con);
				SqlDataReader dataReader = cmd.ExecuteReader();
				if(dataReader.HasRows) {
					while(dataReader.Read()) {
						Response.Write("<script>alert('Successful login');</script>");
						Session["username"] = dataReader.GetValue(0).ToString();
						Session["fullname"] = dataReader.GetValue(2).ToString();
						Session["role"] = "admin";
					}
					Response.Redirect("homepage.aspx");
				} else {
					Response.Write("<script>alert('Invalid credentials');</script>");
				}
			} catch(Exception ex) {
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}
	}
}