<%-- 
k-Space Astronauts labeling platform
    (c) 2016 under Apache 2.0 license 
    Thomas Kuestner, Martin Schwartz, Philip Wolf
    Please refer to https://sites.google.com/site/kspaceastronauts/iqa/labelingplatform for more information    
--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewTestCase.aspx.cs" Inherits="LabelingFramework.TestCase.ViewTestCase" MaintainScrollPositionOnPostback="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


<fieldset>
<legend >Testcases</legend>
<asp:Button ID="btnInsert" runat="server" text="Insert new testcase" CssClass="button-save" onclick="btnInsert_Click" ValidationGroup="labeling" width="200px"/>
<p></p>
<asp:HiddenField ID="idTestCase" runat="server" Value="" />
<asp:GridView ID="gvTestCase" runat="server" DataKeyNames="IDTestcase" AutoGenerateColumns="false" OnSelectedIndexChanging="gvTestCase_SelectedIndexChanging" Width="100%">
        <Columns>
            <asp:BoundField DataField="NameTestCase" HeaderText="Name Testcase" />
            <asp:BoundField DataField="TestQuestion" HeaderText="Test Question" />
            <%--<asp:BoundField DataField="DescrState" HeaderText="Description State" />--%>
            <asp:TemplateField>
                  <ItemTemplate>
                          <asp:Button ID="btnSelect" text="Info" runat="server"  CommandName ="select" />
                </ItemTemplate>  
             </asp:TemplateField>
             <asp:TemplateField>
                  <ItemTemplate>
                          <asp:Button ID="btnExportTestcase" text="Export" runat="server"  CommandArgument='<%# Bind("IDTestcase") %>' CommandName ="ExportTestcase" OnClick="BtnExportTestCase_Click" />
                </ItemTemplate>  
             </asp:TemplateField>
             <asp:TemplateField>
                  <ItemTemplate>
                          <asp:Button ID="btnUpdate" text="Edit" runat="server" CommandArgument='<%# Bind("IDTestcase") %>'  CommandName ="Change" OnClick="BtnUpdateTestCase_Click" />
                </ItemTemplate>  
             </asp:TemplateField>
             <asp:BoundField DataField="isActive" HeaderText="opened/active" />
             <asp:TemplateField>          
                  <ItemTemplate>
                          <asp:Button ID="btnToggle" text="Toggle" runat="server" CommandArgument='<%# Bind("IDTestcase") %>'  CommandName ="Toggle" OnClick="BtnToggleTestCase_Click" />
                </ItemTemplate>  
             </asp:TemplateField>
			   <asp:TemplateField>          
                  <ItemTemplate>
                          <asp:Button ID="btnStart" text="Start" runat="server" CommandArgument='<%# Bind("IDTestcase") %>'  CommandName ="Start" OnClick="StartTestCase_Click" />
                </ItemTemplate>  
             </asp:TemplateField>
			 			   <asp:TemplateField>          
                  <ItemTemplate>
                          <asp:Button ID="btnDelete" text="Delete" runat="server" CommandArgument='<%# Bind("IDTestcase") %>'  CommandName ="Delete" OnClick="Delete_Click" />
                </ItemTemplate>  
             </asp:TemplateField>
        </Columns>
         <SelectedRowStyle BackColor="LightGray" />
    </asp:GridView>
</fieldset>

    <br />
    <br />

<fieldset  id="fuser" runat="server">
<legend id="lTestcase" runat="server">Testcase</legend>
<asp:GridView ID="gvUser" runat="server" DataKeyNames="Id_user" AutoGenerateColumns="false" OnSelectedIndexChanging="gvUser_SelectedIndexChanging" Width="100%">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Name User" />
            <asp:BoundField DataField="Surname" HeaderText="Surname User" />
            <asp:BoundField DataField="DescriptionType" HeaderText="Description User" />
             <asp:BoundField DataField="Email" HeaderText="Email User" />
              <asp:BoundField DataField="Percentual" HeaderText="Percentage" />
              <asp:TemplateField>
                  <ItemTemplate>
                          <asp:Button ID="btnSelect" text="Info" runat="server"  CommandName ="Select" />
                </ItemTemplate>  
             </asp:TemplateField>
                <asp:TemplateField>
                  <ItemTemplate>
                          <asp:Button ID="btnEsporta" text="Export" runat="server"  CommandName ="Export" CommandArgument='<%# Bind("Id_user") %>' OnClick="Export_Click" />
                </ItemTemplate>  
             </asp:TemplateField>          
        </Columns>
        <SelectedRowStyle BackColor="LightGray" />
        <EmptyDataTemplate>
        <p>No User Found</p>
        </EmptyDataTemplate>
    </asp:GridView>
   

   </fieldset>
   <br />
   <br />

   <fieldset id="flable" runat="server">
<legend id="lUser" runat="server">Label of user</legend>
<asp:GridView ID="gvLable" runat="server" DataKeyNames="IdLable" AutoGenerateColumns="false"  Width="100%">
        <Columns>
            <asp:BoundField DataField="Description" HeaderText="Description Scale" />            
            <asp:BoundField DataField="PathImage" HeaderText="Image" />
              <asp:BoundField DataField="lable" HeaderText="Lable" />

              <asp:TemplateField>
                  <ItemTemplate>
                          <asp:Button ID="btnSelect" text="Select" runat="server"  CommandName ="Select" Visible="false" />
                </ItemTemplate>  
             </asp:TemplateField>
         </Columns>
          <EmptyDataTemplate>
        <p>No labels found</p>
        </EmptyDataTemplate>
    </asp:GridView>

</fieldset>
</asp:Content>

