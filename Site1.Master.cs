﻿using System;

namespace ElibraryManagement
{
	public partial class Site1 : System.Web.UI.MasterPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			
		}

		protected void LinkButton1_Click(object sender, EventArgs e)
		{
			Response.Redirect("viewbooks.aspx");
		}

		protected void LinkButton2_Click(object sender, EventArgs e)
		{
			Response.Redirect("userlogin.aspx");
		}

		protected void LinkButton3_Click(object sender, EventArgs e)
		{
			Response.Redirect("usersignup.aspx");
		}

		//logout button
		protected void LinkButton4_Click(object sender, EventArgs e)
		{

		}

		protected void LinkButton6_Click(object sender, EventArgs e)
		{
			Response.Redirect("adminlogin.aspx");
		}

		protected void LinkButton7_Click(object sender, EventArgs e)
		{
			Response.Redirect("adminauthormanagement.aspx");
		}

		protected void LinkButton8_Click(object sender, EventArgs e)
		{
			Response.Redirect("adminpublishermanagement.aspx");
		}

		protected void LinkButton9_Click(object sender, EventArgs e)
		{
			Response.Redirect("adminbookinventory.aspx");
		}

		protected void LinkButton10_Click(object sender, EventArgs e)
		{
			Response.Redirect("adminbookissuing.aspx");
		}

		protected void LinkButton11_Click(object sender, EventArgs e)
		{
			Response.Redirect("adminmembermanagement.aspx");
		}

		//view profile
	}
}