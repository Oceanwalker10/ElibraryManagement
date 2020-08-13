using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ElibraryManagement
{
	public partial class WebForm7 : System.Web.UI.Page
	{
		string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
		static string global_filepath;
		static int global_actual_stock, global_current_stock, global_issued_books;

		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack) {
				FillAuthorPublisherValues();
			}
			BookTable.DataBind();
		}

		protected void Button_Go_Click(object sender, EventArgs e)
		{
			GetBookByID();
		}

		protected void Button_Add_Click(object sender, EventArgs e)
		{
			if (CheckIfBookExists())
			{
				Response.Write("<script>alert('Book Already Exists, try some other Book ID');</script>");
			}
			else
			{
				AddNewBook();
			}
		}

		protected void Button_Update_Click(object sender, EventArgs e)
		{
			UpdateBookByID();
		}

		protected void Button_Delete_Click(object sender, EventArgs e)
		{
			DeleteBookByID();
		}

		void GetBookByID()
		{
			try
			{
				SqlConnection con = new SqlConnection(strcon);
				if (con.State == ConnectionState.Closed)
				{
					con.Open();
				}
				SqlCommand cmd = new SqlCommand("SELECT * from dbo.book_master_tbl WHERE book_id='" + TextBox_BookId.Text.Trim() + "';", con);
				SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
				DataTable dataTable = new DataTable();
				dataAdapter.Fill(dataTable);
				if (dataTable.Rows.Count >= 1)
				{
					TextBox_BookName.Text = dataTable.Rows[0]["book_name"].ToString();
					TextBox_Date.Text = dataTable.Rows[0]["publish_date"].ToString();
					TextBox_Edition.Text = dataTable.Rows[0]["edition"].ToString();
					TextBox_BookCost.Text = dataTable.Rows[0]["book_cost"].ToString().Trim();
					TextBox_Pages.Text = dataTable.Rows[0]["no_of_pages"].ToString().Trim();
					TextBox_ActualStock.Text = dataTable.Rows[0]["actual_stock"].ToString().Trim();
					TextBox_CurrentStock.Text = dataTable.Rows[0]["current_stock"].ToString().Trim();
					TextBox_BookDescription.Text = dataTable.Rows[0]["book_description"].ToString();
					TextBox_CurrentStock.Text = "" + (Convert.ToInt32(dataTable.Rows[0]["actual_stock"].ToString()) - Convert.ToInt32(dataTable.Rows[0]["current_stock"].ToString()));

					DropDownList_Language.SelectedValue = dataTable.Rows[0]["language"].ToString().Trim();
					DropDownList_Publisher.SelectedValue = dataTable.Rows[0]["publisher_name"].ToString().Trim();
					DropDownList_AuthorName.SelectedValue = dataTable.Rows[0]["author_name"].ToString().Trim();

					ListBox_Genre.ClearSelection();
					string[] genre = dataTable.Rows[0]["genre"].ToString().Trim().Split(',');
					for (int i = 0; i < genre.Length; i++)
					{
						for (int j = 0; j < ListBox_Genre.Items.Count; j++)
						{
							if (ListBox_Genre.Items[j].ToString() == genre[i])
							{
								ListBox_Genre.Items[j].Selected = true;

							}
						}
					}

					global_actual_stock = Convert.ToInt32(dataTable.Rows[0]["actual_stock"].ToString().Trim());
					global_current_stock = Convert.ToInt32(dataTable.Rows[0]["current_stock"].ToString().Trim());
					global_issued_books = global_actual_stock - global_current_stock;
					global_filepath = dataTable.Rows[0]["book_img_link"].ToString();

				}
				else
				{
					Response.Write("<script>alert('Invalid Book ID');</script>");
				}

			}
			catch (Exception ex)
			{

			}
		}

		void AddNewBook() {
			try {
				string genres = "";
				foreach(int i in ListBox_Genre.GetSelectedIndices()) {
					genres = genres + ListBox_Genre.Items[i] + ",";
				}
				genres = genres.Remove(genres.Length - 1);
				string filepath = "~/book_inventory/books1.png";
				string filename = Path.GetFileName(FileUpload.PostedFile.FileName);
				FileUpload.SaveAs(Server.MapPath("book_inventory/" + filename));
				filepath = "~/book_inventory/" + filename;

				SqlConnection con = new SqlConnection(strcon);
				if(con.State == ConnectionState.Closed) {
					con.Open();
				}
				SqlCommand cmd = new SqlCommand("Insert Into dbo.book_master_tbl(book_id,book_name," +
				"genre,author_name,publisher_name,publish_date,language,edition,book_cost,no_of_pages," +
				"book_description,actual_stock,current_stock,book_img_link) values(@book_id,@book_name," +
				"@genre,@author_name,@publisher_name,@publish_date,@language,@edition,@book_cost,@no_of_pages," +
				"@book_description,@actual_stock,@current_stock,@book_img_link)", con);

				cmd.Parameters.AddWithValue("@book_id", TextBox_BookId.Text.Trim());
				cmd.Parameters.AddWithValue("@book_name", TextBox_BookName.Text.Trim());
				cmd.Parameters.AddWithValue("@genre", genres);
				cmd.Parameters.AddWithValue("@author_name", DropDownList_AuthorName.SelectedItem.Value);
				cmd.Parameters.AddWithValue("@publisher_name", DropDownList_Publisher.SelectedItem.Value);
				cmd.Parameters.AddWithValue("@publish_date", TextBox_Date.Text.Trim());
				cmd.Parameters.AddWithValue("@language", DropDownList_Language.SelectedItem.Value);
				cmd.Parameters.AddWithValue("@edition", TextBox_Edition.Text.Trim());
				cmd.Parameters.AddWithValue("@book_cost", TextBox_BookCost.Text.Trim());
				cmd.Parameters.AddWithValue("@no_of_pages", TextBox_Pages.Text.Trim());
				cmd.Parameters.AddWithValue("@book_description", TextBox_BookDescription.Text.Trim());
				cmd.Parameters.AddWithValue("@actual_stock", TextBox_ActualStock.Text.Trim());
				cmd.Parameters.AddWithValue("@current_stock", TextBox_CurrentStock.Text.Trim());
				cmd.Parameters.AddWithValue("@book_img_link", filepath);
				cmd.ExecuteNonQuery();
				con.Close();
				Response.Write("<script>alert('Book added successfully.');</script>");
				BookTable.DataBind();
			} catch(Exception ex) {
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}    

    void UpdateBookByID()
    {
      if (CheckIfBookExists())
      {
        try
        {
          /*int actual_stock = Convert.ToInt32(TextBox_ActualStock.Text.Trim());
          int current_stock = Convert.ToInt32(TextBox_CurrentStock.Text.Trim());

          if (global_actual_stock == actual_stock)
          {

          }
          else
          {
            if (actual_stock < global_issued_books)
            {
              Response.Write("<script>alert('Actual Stock value cannot be less than the Issued books');</script>");
              return;
            }
            else
            {
              current_stock = actual_stock - global_issued_books;
              TextBox_CurrentStock.Text = "" + current_stock;
            }
          }*/

          string genres = "";
          foreach (int i in ListBox_Genre.GetSelectedIndices())
          {
            genres = genres + ListBox_Genre.Items[i] + ",";
          }
          genres = genres.Remove(genres.Length - 1);

					string filepath = "~/book_inventory/books1";
					string filename = Path.GetFileName(FileUpload.PostedFile.FileName);
          if (filename == "" || filename == null)
          {
            filepath = global_filepath;

          }
					else
					{
						FileUpload.SaveAs(Server.MapPath("book_inventory/" + filename));
						filepath = "~/book_inventory/" + filename;
					}

					SqlConnection con = new SqlConnection(strcon);
          if (con.State == ConnectionState.Closed)
          {
            con.Open();
          }
          SqlCommand cmd = new SqlCommand("UPDATE dbo.book_master_tbl set book_name=@book_name, " +
					"genre=@genre, author_name=@author_name, publisher_name=@publisher_name, publish_date=@publish_date, " +
					"language=@language, edition=@edition, book_cost=@book_cost, no_of_pages=@no_of_pages, " +
					"book_description=@book_description, actual_stock=@actual_stock, current_stock=@current_stock, " +
					"book_img_link=@book_img_link Where book_id='" + TextBox_BookId.Text.Trim() + "'", con);

					cmd.Parameters.AddWithValue("@book_name", TextBox_BookName.Text.Trim());
					cmd.Parameters.AddWithValue("@genre", genres);
					cmd.Parameters.AddWithValue("@author_name", DropDownList_AuthorName.SelectedItem.Value);
					cmd.Parameters.AddWithValue("@publisher_name", DropDownList_Publisher.SelectedItem.Value);
					cmd.Parameters.AddWithValue("@publish_date", TextBox_Date.Text.Trim());
					cmd.Parameters.AddWithValue("@language", DropDownList_Language.SelectedItem.Value);
					cmd.Parameters.AddWithValue("@edition", TextBox_Edition.Text.Trim());
					cmd.Parameters.AddWithValue("@book_cost", TextBox_BookCost.Text.Trim());
					cmd.Parameters.AddWithValue("@no_of_pages", TextBox_Pages.Text.Trim());
					cmd.Parameters.AddWithValue("@book_description", TextBox_BookDescription.Text.Trim());
					cmd.Parameters.AddWithValue("@actual_stock", TextBox_ActualStock.Text.Trim());
					cmd.Parameters.AddWithValue("@current_stock", TextBox_CurrentStock.Text.Trim());
					cmd.Parameters.AddWithValue("@book_img_link", filepath);


					cmd.ExecuteNonQuery();
          con.Close();
          BookTable.DataBind();
          Response.Write("<script>alert('Book Updated Successfully');</script>");
        }
        catch (Exception ex)
        {
          Response.Write("<script>alert('" + ex.Message + "');</script>");
        }
      }
      else
      {
        Response.Write("<script>alert('Invalid Book ID');</script>");
      }
    }

		void DeleteBookByID()
		{
			if (CheckIfBookExists())
			{
				try
				{
					SqlConnection con = new SqlConnection(strcon);
					if (con.State == ConnectionState.Closed)
					{
						con.Open();
					}

					SqlCommand cmd = new SqlCommand("DELETE from dbo.book_master_tbl WHERE book_id='" + TextBox_BookId.Text.Trim() + "'", con);

					cmd.ExecuteNonQuery();
					con.Close();
					Response.Write("<script>alert('Book Deleted Successfully');</script>");

					BookTable.DataBind();

				}
				catch (Exception ex)
				{
					Response.Write("<script>alert('" + ex.Message + "');</script>");
				}

			}
			else
			{
				Response.Write("<script>alert('Invalid Member ID');</script>");
			}
		}

		bool CheckIfBookExists()
		{
			try
			{
				SqlConnection con = new SqlConnection(strcon);
				if (con.State == ConnectionState.Closed)
				{
					con.Open();
				}

				SqlCommand cmd = new SqlCommand("Select * From dbo.book_master_tbl Where book_id='" +
				TextBox_BookId.Text.Trim() + "' OR book_name='" + TextBox_BookName.Text.Trim() + "';", con);
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

		void FillAuthorPublisherValues() {
			try {
				SqlConnection con = new SqlConnection(strcon);
				if(con.State == ConnectionState.Closed) {
					con.Open();
				}
				SqlCommand cmd = new SqlCommand("Select author_name from dbo.author_master_tbl;", con);
				SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
				DataTable dataTable = new DataTable();
				dataAdapter.Fill(dataTable);
				DropDownList_AuthorName.DataSource = dataTable;
				DropDownList_AuthorName.DataValueField = "author_name";
				DropDownList_AuthorName.DataBind();

				cmd = new SqlCommand("Select publisher_name from dbo.publisher_master_tbl;", con);
				dataAdapter = new SqlDataAdapter(cmd);
				dataTable = new DataTable();
				dataAdapter.Fill(dataTable);
				DropDownList_Publisher.DataSource = dataTable;
				DropDownList_Publisher.DataValueField = "publisher_name";
				DropDownList_Publisher.DataBind();
			} catch(Exception ex) {

			}
		}

	}
}