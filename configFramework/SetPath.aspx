<%-- 
k-Space Astronauts labeling platform
    (c) 2016 under Apache 2.0 license 
    Thomas Kuestner, Martin Schwartz, Philip Wolf
    Please refer to https://sites.google.com/site/kspaceastronauts/iqa/labelingplatform for more information    
--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SetPath.aspx.cs" Inherits="LabelingFramework.managementImage.SetPath" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
    // Martin - changed method .live to .on (live is deprecated or removed in newer jquery libraries)
    $("[src*=plus]").on("click", function () {
        $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
        $(this).attr("src", "../managementImg/imgHelp/minus.png");
    });
    $("[src*=minus]").on("click", function () {
        $(this).attr("src", "../managementImg/imgHelp/plus.png");
        $(this).closest("tr").next().remove();
    });
</script>

<fieldset >
<legend>Image databases</legend>
<asp:GridView ID="gvPathLabeling" runat="server" DataKeyNames="Id_path" Width="100%" AutoGenerateColumns="false" OnRowDeleting="gvPathLabeling_RowDeleting" onselectedindexchanging="gvPathLabeling_SelectedIndexChanging" OnRowUpdating="gvPathLabeling_RowUpdating" > 
        <Columns>
             <asp:BoundField DataField="DatabasePath" HeaderText="Database Path" HeaderStyle-Width="80%" />
             <asp:TemplateField>
             <ItemTemplate>
                <asp:Button ID="btnRefresh" runat="server" text="Refresh" CommandName = "update" OnClick="btnRefresh_Click" />
             </ItemTemplate>            
             </asp:TemplateField>
             <asp:TemplateField>
             <ItemTemplate>
                  <asp:Button ID="btnChangePathLabeling" text="Edit" runat="server"  CommandName ="select"/>      
             </ItemTemplate>            
             </asp:TemplateField>
             <asp:TemplateField>
             <ItemTemplate>
                <asp:Button ID="btnDeletePathLabeling" text="Delete" runat="server"  CommandName ="delete" OnClientClick="return confirm('Are you sure?');"  />
             </ItemTemplate>            
             </asp:TemplateField>
        </Columns>
    </asp:GridView>
</fieldset>
    
<fieldset id="addPath" runat="server" visible="true">
   <legend>Add new image database</legend>
    <table>
    <tr>
    <td><asp:Label ID="lblPathLabeling" runat="server" Text="Folder Path: "></asp:Label></td>
    <td><asp:TextBox ID="txtPathLabeling" runat="server" Width="600px" Height="25px" ></asp:TextBox>
        <asp:RequiredFieldValidator ID="RFVPathLabeling" runat="server" ControlToValidate="txtPathLabeling" ForeColor="Red" ValidationGroup="labeling" ErrorMessage=" Image Folder Path is required!!">*</asp:RequiredFieldValidator></td>
        </tr>
        </table>
        <asp:Button ID="btnAdd" runat="server" text="Add" onclick="btnAdd_Click" ValidationGroup="labeling" />
 </fieldset>    
    

    <fieldset id="fEditPath" runat="server" visible="false">
    <legend >Edit Database Path</legend>

         <table>
         <tr><td><asp:label ID="lblName" runat="server" Text="Folder Path: "></asp:label></td>
         <td><asp:TextBox ID="txtPathLabelingName" runat="server" Text="" Width="600px" Height="25px"></asp:TextBox></td>
         </tr></table>                                     

      <asp:HiddenField ID="hddIdPath" Value="0" runat="server"/>
      <asp:Button ID="btnUpdate" CssClass="button-save" runat="server" OnClick="btnUpdate_Click" Text="Update" />
      <asp:Button ID="btnCancel" CssClass="button-save" runat="server" OnClick="btnCancel_Click" Text="Cancel" />
  </div>
      <!-- Allow form submission with keyboard without duplicating the dialog button -->
  
    </fieldset>
                  <div class="success"  runat="server" id="DivSuccess" visible="false">
                            <asp:Label id="lblSuccess" runat="server" Text="Update successfully completed!"></asp:Label>
                    </div>

                    <div class="error"  runat="server" id="DivError" visible="false">
                            <asp:Label id="lblError" runat="server" Text=""></asp:Label>
                    </div>

   
  <fieldset id="fDatabaseFolders" runat="server" visible="false">
  <legend id="lDatabaseFolders" runat="server">Database</legend>
  <asp:GridView ID="gvDirectory" runat="server" GridLines="None" AutoGenerateColumns="false" Visible="false">
    <Columns>   
        <asp:TemplateField>
            <ItemTemplate>
                 <img alt = "" style="cursor: pointer" src="../managementImg/imgHelp/plus.png" />
               
                 <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                  <asp:GridView ID="gvsubDirectory" runat="server"  DataSource='<%#DataBinder.Eval(Container.DataItem, "SubDirectory") %>' AutoGenerateColumns="false" GridLines="None">
                    <Columns>                   
                         <asp:TemplateField>
                               <ItemTemplate>
                                     <asp:Label ID="lblNamesubDirectory" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NameDirectory") %>'></asp:Label>
                                </ItemTemplate>
                         </asp:TemplateField>
                     </Columns>
                    </asp:GridView>
                </asp:Panel>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                     <asp:Label ID="lblNameDirectory" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NameDirectory") %>'></asp:Label>
            </ItemTemplate>        
        </asp:TemplateField>
    </Columns>
  </asp:GridView>
 
  </fieldset>

   

</asp:Content>
