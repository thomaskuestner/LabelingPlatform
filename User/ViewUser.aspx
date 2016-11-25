<%-- 
k-Space Astronauts labeling platform
    (c) 2016 under Apache 2.0 license 
    Thomas Kuestner, Martin Schwartz, Philip Wolf
    Please refer to https://sites.google.com/site/kspaceastronauts/iqa/labelingplatform for more information    
--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewUser.aspx.cs" Inherits="LabelingFramework.User.ViewUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <fieldset>
    <legend>Users</legend>
    
    
    <asp:GridView ID="gvUser" runat="server" AutoGenerateColumns="false" 
        onselectedindexchanging="gvUser_SelectedIndexChanging" 
        DataKeyNames="id_user" onrowdeleting="gvUser_RowDeleting"  Width="100%"  >
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="Surname" HeaderText="Surname" />
			<asp:BoundField DataField="Username" HeaderText="Username" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:BoundField DataField="DescriptionType" HeaderText="User Type" />
            <asp:BoundField DataField="YearsOfExperience" HeaderText="Years Of Experience" />
            <asp:TemplateField>
            <ItemTemplate>
                <asp:Button ID="btnUpdateUser" text="Edit" runat="server"  CommandName ="select" />
            </ItemTemplate>            
            </asp:TemplateField>
            <asp:TemplateField>
            <ItemTemplate>
                   <asp:Button ID="btnDeleteUser" text="Delete" runat="server"  CommandName ="delete" OnClientClick="return confirm('Are you sure?');"  />
            </ItemTemplate>            
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </fieldset>


  
    <fieldset id="fUpdateUser" runat="server" visible="false">
    <legend >Update User</legend>

         <div class="divtable">
			                	<div class="container">
				                	<div class="contaneir-label">
                                        <asp:label ID="lblName" runat="server" Text="Name:" CssClass="label"></asp:label>
                                   </div>
                                    <div class="contaneir-object">
                                       <asp:TextBox ID="txtName" runat="server" Text="" Enabled="false" maxlength="30"></asp:TextBox>
                                    </div>
                                    </div>
                                   <div class="container">
				                	<div class="contaneir-label">
                                        <asp:label ID="lblSurname" runat="server" Text="Surname:" CssClass="label"></asp:label>
                                   </div>
                                    <div class="contaneir-object">
                                       <asp:TextBox ID="txtSurname" runat="server" Text="" Enabled="false" maxlength="30"></asp:TextBox>
                                    </div>
                                    </div>
                             <div class="container">
				                	<div class="contaneir-label">
                                        <asp:label ID="lblDescriptionType" runat="server" Text="Description Type:" CssClass="label"></asp:label>
                                   </div>
                                    <div class="contaneir-object">
                                      <asp:DropDownList ID="ddlUserType" runat="server" CssClass="dropdownlist"></asp:DropDownList>
                                    </div>
                                    </div>
        
                           


      <asp:HiddenField ID="hddIdUser" Value="0" runat="server"/>
      <asp:Button ID="btnUpdate" CssClass="button-save" runat="server" Text="Update" OnClick="btnUpdate_Click" />
  </div>
      <!-- Allow form submission with keyboard without duplicating the dialog button -->
  
    </fieldset>

    <fieldset id="fUserFooter" runat="server" visible="false">
                  <div class="success"  runat="server" id="DivSuccess" visible="false">
                            <asp:Label id="lblSuccess" runat="server" Text="Update successfully completed!"></asp:Label>
                    </div>

                    <div class="error"  runat="server" id="DivError" visible="false">
                            <asp:Label id="lblError" runat="server" Text=""></asp:Label>
                    </div>
                    </fieldset>
</asp:Content>
