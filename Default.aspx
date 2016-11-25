<%-- 
k-Space Astronauts labeling platform
    (c) 2016 under Apache 2.0 license 
    Thomas Kuestner, Martin Schwartz, Philip Wolf
    Please refer to https://sites.google.com/site/kspaceastronauts/iqa/labelingplatform for more information    
--%>
<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="LabelingFramework._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Welcome <asp:Label ID="lblNominativo" runat="server"></asp:Label>   
    <asp:Label ID="lblMessage" Font-Size="Small" runat="server" ForeColor="Red" Text="(When finished, log out before closing the page!)"></asp:Label>
    
    </h2>
  
    <p>
        
    </p>

   <asp:GridView ID="gvTestCase" runat="server" DataKeyNames="IDTestcase" AutoGenerateColumns="false" OnSelectedIndexChanging="gvTestCase_SelectedIndexChanging" Width="75%" HorizontalAlign="Center">
        <Columns>
            <asp:BoundField DataField="NameTestCase" HeaderText="Test Case" />
            <asp:BoundField DataField="TestQuestion" HeaderText="Test Question" />
            <%--<asp:BoundField DataField="DescrState" HeaderText="Description State" />--%>
            <asp:BoundField DataField="Percentual" HeaderText="Percentage" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btnSelect" runat="server" CommandName="select" Text="Start" ToolTip="Start labelling"/>
            
                </ItemTemplate>
            <ItemStyle CssClass="gvItemCenter"/>
            <HeaderStyle CssClass="gvItemCenter" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>


    <%--<asp:Label ID="lblMessageInfo" runat="server" Text="Attention: the loading of the testcase after selecting 'to work' may take a few seconds!"></asp:Label>--%>


   

</asp:Content>
