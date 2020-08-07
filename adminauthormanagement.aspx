<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="adminauthormanagement.aspx.cs" Inherits="ElibraryManagement.WebForm4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<script type="text/javascript">
		$(document).ready(function () {
			$(".table").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
		});
   </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="container">
			<div class="row">
				<div class="col-md-5">
					<div class="card">
						<div class="card-body">
						
							<div class="row">
								<div class="col">
									<center>
										<h4>Author Details</h4>
									</center>
								</div>
							</div>

							<div class="row">
								<div class="col">
									<center>
										<img width="100px" src="imgs/generaluser.png" />
									</center>
								</div>
							</div>

							<div class="row">
								<div class="col">
									<hr />
								</div>
							</div>

							<div class="row">
								<div class="col-md-4">
									<label>Author ID</label>
									<div class="form-group">
										<div class="input-group">
											<asp:TextBox CssClass="form-control" ID="TextBox_AuthorId" runat="server" placeholder="ID"></asp:TextBox>
											<asp:Button CssClass="btn btn-primary" ID="Button_Go" runat="server" Text="Go" OnClick="Button_Go_Click" />
										</div>
									</div>
								</div>

								<div class="col-md-8">
									<label>Author Name</label>
									<div class="form-group">
										<asp:TextBox CssClass="form-control" ID="TextBox_AuthorName" runat="server" placeholder="Author Name"></asp:TextBox>
									</div>
								</div>
							</div>

							<div class="row">
								<div class="col-4">
									<asp:Button ID="Button_Add" class="btn btn-lg btn-block btn-success" runat="server" Text="Add" OnClick="Button_Add_Click" />
								</div>
								<div class="col-4">
									<asp:Button ID="Button_Update" class="btn btn-lg btn-block btn-warning" runat="server" Text="Update" OnClick="Button_Update_Click" />
								</div>
								<div class="col-4">
									<asp:Button ID="Button_Delete" class="btn btn-lg btn-block btn-danger" runat="server" Text="Delete" OnClick="Button_Delete_Click" />
								</div>
							</div>
						</div>
				</div>
				<a href="homepage.aspx"><< Back to Home</a><br /><br />
			</div>
			
			<div class="col-md-7">
				<div class="card">
					<div class="card-body">
						<div class="row">
							<div class="col">
								<center>
									<h4>Author List</h4>
								</center>
							</div>
						</div>

						<div class="row">
							<div class="col">
								<hr />
							</div>
						</div>

						<div class="row">
							<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=DESKTOP-T60FASN\SQLEXPRESS;
							Initial Catalog=elibraryDB;Integrated Security=True" ProviderName="System.Data.SqlClient" 
							SelectCommand="SELECT * FROM [author_master_tbl]"></asp:SqlDataSource>
							
							<div class="col">
								<asp:GridView class="table table-striped table-bordered" ID="AuthorTable" runat="server" 
								AutoGenerateColumns="False" DataKeyNames="author_id" DataSourceID="SqlDataSource1">
									<Columns>
										<asp:BoundField DataField="author_id" HeaderText="author_id" ReadOnly="true" SortExpression="author_id" />
										<asp:BoundField DataField="author_name" HeaderText="author_name" SortExpression="author_name" />
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
