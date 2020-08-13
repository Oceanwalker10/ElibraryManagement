<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="adminbookinventory.aspx.cs" Inherits="ElibraryManagement.WebForm7" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <script type="text/javascript">
		$(document).ready(function () {
			$(".table").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
		});

		function readURL(input) {
			if (input.files && input.files[0]) {
				var reader = new FileReader();

				reader.onload = function (e) {
					$('#imgview').attr('src', e.target.result);
				};

				reader.readAsDataURL(input.files[0]);
			}
		}
   </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="container-fluid">
		<div class="row">
			<div class="col-md-5">
				<div class="card">
					<div class="card-body">
						<div class="row">
							<div class="col">
								<center>
									<h4>Book Details</h4>
									<asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
								</center>
							</div>
						</div>

						<div class="row">
							<div class="col">
								<center>
									<img id="imgview" Height="150px" Width="100px" src="book_inventory/books1.png" />
								</center>
							</div>
						</div>

						<div class="row">
							<div class="col">
								<hr />
							</div>
						</div>
					</div>

					<div class="row">
						<div class="col">
              <asp:FileUpload onchange="readURL(this);" class="form-control" ID="FileUpload" runat="server" />
            </div>
					</div>

					<div class="row">
						<div class="col-md-4">
							<label>Book ID</label>
							<div class="form-group">
								<div class="input-group">
									<asp:TextBox CssClass="form-control" ID="TextBox_BookId" runat="server" placeholder="Book ID"></asp:TextBox>
									<asp:Button class="form-control btn btn-primary" ID="Button_Go" runat="server" Text="Go" OnClick="Button_Go_Click"></asp:Button>
								</div>
							</div>
						</div>

						<div class="col-md-8">
							<label>Book Name</label>
							<div class="form-group">
								<asp:TextBox CssClass="form-control" ID="TextBox_BookName" runat="server" placeholder="Book Name"></asp:TextBox>
							</div>
						</div>
					</div>

					<div class="row">
						<div class="col-md-4">
							<label>Language</label>
							<div class="form-group">
								<asp:DropDownList class="form-control" ID="DropDownList_Language" runat="server">
									<asp:ListItem Text="English" Value="English"></asp:ListItem>
									<asp:ListItem Text="Hindi" Value="Hindi"></asp:ListItem>
									<asp:ListItem Text="Marathi" Value="Marathi"></asp:ListItem>
									<asp:ListItem Text="French" Value="French"></asp:ListItem>
									<asp:ListItem Text="German" Value="German"></asp:ListItem>
									<asp:ListItem Text="Urdu" Value="Urdu"></asp:ListItem>
								</asp:DropDownList>
							</div>
							<label>Publisher Name</label>
							<div class="form-group">
								<asp:DropDownList class="form-control" ID="DropDownList_Publisher" runat="server">
									<asp:ListItem Text="Publisher 1" Value="Publisher 1"></asp:ListItem>
                  <asp:ListItem Text="Publisher 2" Value="Publisher 2"></asp:ListItem>
								</asp:DropDownList>
							</div>
						</div>

						<div class="col-md-4">
              <label>Author Name</label>
              <div class="form-group">
                  <asp:DropDownList class="form-control" ID="DropDownList_AuthorName" runat="server">
                    <asp:ListItem Text="A1" Value="a1" />
                    <asp:ListItem Text="A2" Value="a2" />
                  </asp:DropDownList>
              </div>
              <label>Publish Date</label>
              <div class="form-group">
                  <asp:TextBox CssClass="form-control" ID="TextBox_Date" runat="server" placeholder="Date" TextMode="Date"></asp:TextBox>
              </div>
            </div>

            <div class="col-md-4">
              <label>Genre</label>
              <div class="form-group">
                  <asp:ListBox CssClass="form-control" ID="ListBox_Genre" runat="server" SelectionMode="Multiple" Rows="5">
                    <asp:ListItem Text="Action" Value="Action" />
                    <asp:ListItem Text="Adventure" Value="Adventure" />
                    <asp:ListItem Text="Comic Book" Value="Comic Book" />
                    <asp:ListItem Text="Self Help" Value="Self Help" />
                    <asp:ListItem Text="Motivation" Value="Motivation" />
                    <asp:ListItem Text="Healthy Living" Value="Healthy Living" />
                    <asp:ListItem Text="Wellness" Value="Wellness" />
                    <asp:ListItem Text="Crime" Value="Crime" />
                    <asp:ListItem Text="Drama" Value="Drama" />
                    <asp:ListItem Text="Fantasy" Value="Fantasy" />
                    <asp:ListItem Text="Horror" Value="Horror" />
                    <asp:ListItem Text="Poetry" Value="Poetry" />
                    <asp:ListItem Text="Personal Development" Value="Personal Development" />
                    <asp:ListItem Text="Romance" Value="Romance" />
                    <asp:ListItem Text="Science Fiction" Value="Science Fiction" />
                    <asp:ListItem Text="Suspense" Value="Suspense" />
                    <asp:ListItem Text="Thriller" Value="Thriller" />
                    <asp:ListItem Text="Art" Value="Art" />
                    <asp:ListItem Text="Autobiography" Value="Autobiography" />
                    <asp:ListItem Text="Encyclopedia" Value="Encyclopedia" />
                    <asp:ListItem Text="Health" Value="Health" />
                    <asp:ListItem Text="History" Value="History" />
                    <asp:ListItem Text="Math" Value="Math" />
                    <asp:ListItem Text="Textbook" Value="Textbook" />
                    <asp:ListItem Text="Science" Value="Science" />
                    <asp:ListItem Text="Travel" Value="Travel" />
                  </asp:ListBox>
              </div>
            </div>
					</div>

					<div class="row">
            <div class="col-md-4">
              <label>Edition</label>
              <div class="form-group">
                  <asp:TextBox CssClass="form-control" ID="TextBox_Edition" runat="server" placeholder="Edition"></asp:TextBox>
              </div>
            </div>
            <div class="col-md-4">
              <label>Book Cost(per unit)</label>
              <div class="form-group">
                  <asp:TextBox CssClass="form-control" ID="TextBox_BookCost" runat="server" placeholder="Book Cost(per unit)" TextMode="Number"></asp:TextBox>
              </div>
            </div>
            <div class="col-md-4">
              <label>Pages</label>
              <div class="form-group">
                  <asp:TextBox CssClass="form-control" ID="TextBox_Pages" runat="server" placeholder="Pages" TextMode="Number"></asp:TextBox>
              </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-4">
              <label>Actual Stock</label>
              <div class="form-group">
                  <asp:TextBox CssClass="form-control" ID="TextBox_ActualStock" runat="server" placeholder="Actual Stock" TextMode="Number"></asp:TextBox>
              </div>
            </div>
            <div class="col-md-4">
              <label>Current Stock</label>
              <div class="form-group">
                  <asp:TextBox CssClass="form-control" ID="TextBox_CurrentStock" runat="server" placeholder="Current Stock" TextMode="Number" ReadOnly="True"></asp:TextBox>
              </div>
            </div>
            <div class="col-md-4">
              <label>Issued Books</label>
              <div class="form-group">
                  <asp:TextBox CssClass="form-control" ID="TextBox_IssuedBooks" runat="server" placeholder="Issued Book" TextMode="Number" ReadOnly="True"></asp:TextBox>
              </div>
            </div>
        </div>

        <div class="row">
            <div class="col-12">
              <label>Book Description</label>
              <div class="form-group">
                  <asp:TextBox CssClass="form-control" ID="TextBox_BookDescription" runat="server" placeholder="Book Description" TextMode="MultiLine" Rows="2"></asp:TextBox>
              </div>
            </div>
        </div>

        <div class="row">
            <div class="col-4">
              <asp:Button ID="Button_Add" class="btn btn-lg btn-block btn-success" runat="server" Text="Add" OnClick="Button_Add_Click"/>
            </div>
            <div class="col-4">
              <asp:Button ID="Button_Update" class="btn btn-lg btn-block btn-warning" runat="server" Text="Update" OnClick="Button_Update_Click"/>
            </div>
            <div class="col-4">
              <asp:Button ID="Button_Delete" class="btn btn-lg btn-block btn-danger" runat="server" Text="Delete" OnClick="Button_Delete_Click"/>
            </div>
        </div>
        <a href="homepage.aspx"><< Back to Home</a><br><br>
      </div>
    </div>

    <div class="col-md-7">
      <div class="card">
        <div class="card-body">
          <div class="row">
            <div class="col">
              <center>
                  <h4>Book Inventory List</h4>
              </center>
            </div>
          </div>
          <div class="row">
            <div class="col">
              <hr>
            </div>
          </div>
          <div class="row">
            <div class="col">
              <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="Data Source=Labo\SQLEXPRESS;
              Initial Catalog=elibraryDB;Integrated Security=True" ProviderName="System.Data.SqlClient" 
              SelectCommand="SELECT * FROM [book_master_tbl]"></asp:SqlDataSource>
              <asp:GridView class="table table-striped table-bordered" ID="BookTable" runat="server" AutoGenerateColumns="False" 
              DataKeyNames="book_id" DataSourceID="SqlDataSource">
								<Columns>
									<asp:BoundField DataField="book_id" HeaderText="book_id" ReadOnly="True" SortExpression="book_id" />
									<asp:BoundField DataField="book_name" HeaderText="book_name" SortExpression="book_name" />
									<asp:BoundField DataField="genre" HeaderText="genre" SortExpression="genre" />
									<asp:BoundField DataField="author_name" HeaderText="author_name" SortExpression="author_name" />
									<asp:BoundField DataField="publisher_name" HeaderText="publisher_name" SortExpression="publisher_name" />
									<asp:BoundField DataField="publish_date" HeaderText="publish_date" SortExpression="publish_date" />
									<asp:BoundField DataField="language" HeaderText="language" SortExpression="language" />
									<asp:BoundField DataField="edition" HeaderText="edition" SortExpression="edition" />
									<asp:BoundField DataField="book_cost" HeaderText="book_cost" SortExpression="book_cost" />
									<asp:BoundField DataField="no_of_pages" HeaderText="no_of_pages" SortExpression="no_of_pages" />
									<asp:BoundField DataField="book_description" HeaderText="book_description" SortExpression="bookh_description" />
									<asp:BoundField DataField="actual_stock" HeaderText="actual_stock" SortExpression="actual_stoc  k" />
									<asp:BoundField DataField="current_stock" HeaderText="current_stock" SortExpression="current_stock" />
									<asp:BoundField DataField="book_img_link" HeaderText="book_img_link" SortExpression="book_img_link" />
								</Columns>
							</asp:GridView>
            </div>
          </div>
        </div>

			</div>
		</div>
	</div>
	</div>
</asp:Content>
