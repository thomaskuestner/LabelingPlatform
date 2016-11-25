<%-- 
k-Space Astronauts labeling platform
    (c) 2016 under Apache 2.0 license 
    Thomas Kuestner, Martin Schwartz, Philip Wolf
    Please refer to https://sites.google.com/site/kspaceastronauts/iqa/labelingplatform for more information    
--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddTypeUser.aspx.cs" Inherits="LabelingFramework.User.AddTypeUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<link rel="stylesheet" href="/Scripts/jquery-ui-smoothness.css"/>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
<script type="text/javascript">
    $(function () {
        $("#tabs").tabs();
		
    });
	
	$(document).ready(function () {
		document.getElementById("MainContent_btnContScaleUpdate").addEventListener("click", function () {alert("If description has been altered, please add unicode symbol in JavaScript file 'ViewImage_.js' - > 'function getUniCodeSymbol()'");}, false);
		document.getElementById("MainContent_btnAddScaleContinuous").addEventListener("click", function () {alert("New continuous scale added, please add unicode symbol in JavaScript file 'ViewImage_.js' - > 'function getUniCodeSymbol()'");}, false);
});

  </script>
<div id="tabs">
  <ul>
    <li><a href="#tabs-1">User type</a></li>
    <li><a href="#tabs-2">Continuous scale</a></li>
    <li><a href="#tabs-3">Discrete scale</a></li>
  </ul>
  
  <style type="text/css">
    .maxWidthGrid
    {
        max-width: 300px;
        overflow: hidden;
    }
</style>
  <div id="tabs-1">
        <asp:GridView ID="gvTypeUser" runat="server" DataKeyNames="ID_Typ" AutoGenerateColumns="false" OnRowDeleting="gvTypeUser_RowDeleting" onselectedindexchanging="gvTypeUser_SelectedIndexChanging">
        <Columns>
             <asp:BoundField DataField="ID_Typ" HeaderText="ID" />
             <asp:BoundField DataField="DescriptionTypUser" HeaderText="user type" />
             <asp:TemplateField>
            <ItemTemplate>
                   <asp:Button ID="btnEditTypeUser" text="Edit" runat="server"  CommandName ="select" CommandArgument='<%# Bind("ID_Typ") %>'/>
            </ItemTemplate>            
            </asp:TemplateField>
              <asp:TemplateField>
            <ItemTemplate>
                   <asp:Button ID="btnDeleteTypeUser" text="Delete" runat="server"  CommandName ="delete" CommandArgument='<%# Bind("ID_Typ") %>' OnClientClick="return confirm('Are you sure?');"  />
            </ItemTemplate>            
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    
    <fieldset id="fAddUserType" runat="server" >
    <legend >Add new user type</legend>
    <table>
    <tr>
    <td><asp:Label ID="lblDescriptionType" runat="server" Text="User type:"></asp:Label></td>
    <td><asp:TextBox ID="txtDescriptionType" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RFVDescriptionType" runat="server" ControlToValidate="txtDescriptionType" 
                                     CssClass="failureNotification" ErrorMessage="Description Type is required." ToolTip="Description Type is required." 
                                     ValidationGroup="AddDescriptionType">*</asp:RequiredFieldValidator></td>
    </tr>
    </table> 
    <asp:Button ID="btnAddTypeUser" runat="server" Text="Add"  ValidationGroup="AddDescriptionType" onclick="btnAddTypeUser_Click" />

    </fieldset>

    <fieldset id="fUpdateTypeUser" runat="server" visible="false">
    <legend id="lUpdateTypeUser">Update user type</legend> 
    <table>
    <tr>
    <td>
    <asp:Label ID="UpdateTypeUserLabel" runat="server" Text="User type:"></asp:Label></td>
    <td><asp:TextBox ID="UpdateTypeUserText" runat="server"></asp:TextBox></td>
    </tr>
    </table>
    <asp:HiddenField ID="hddIdTypeUser" Value="0" runat="server"/>
    <asp:Button ID="btnUpdateTypeUser" CssClass="button-save" runat="server" Text="Update" OnClick="btnUpdateTypeUser_Click" />
    <asp:Button ID="btnCancelTypeUser" CssClass="button-save" runat="server" OnClick="btnCancelTypeUser_Click" Text="Cancel" />
    </fieldset>

  </div>
  <div id="tabs-2">
     <asp:GridView ID="gvTypeScaleContinuous" DataKeyNames="ID_TypScaleContinuous"  runat="server" AutoGenerateColumns="false" OnSelectedIndexChanging="gvTypeScaleContinuous_SelectedIndexChanging" OnRowDeleting="gvTypeScaleContinuous_RowDeleting" Width="100%">
        <Columns>
             <asp:BoundField DataField="ID_TypScaleContinuous" HeaderText="ID" />
             <asp:BoundField DataField="DescriptionScaleContinuous" HeaderText="continuous scale" /> 
             <asp:BoundField DataField="verScaleCont" HeaderText="version" />
             <asp:BoundField DataField="PathImageMin" HeaderText="Worst image" ItemStyle-CssClass="maxWidthGrid"  />
             <asp:BoundField DataField="PathImageMax" HeaderText="Best image" ItemStyle-CssClass="maxWidthGrid"  />
          <asp:TemplateField>         
          <ItemTemplate>
                   <asp:Button ID="btnEditScaleContinuous" text="Edit" runat="server"  CommandName ="select" CommandArgument='<%# Bind("ID_TypScaleContinuous") %>'/>
            </ItemTemplate>  
            </asp:TemplateField>  
          <asp:TemplateField> 
          <ItemTemplate>
                   <asp:Button ID="btnDeleteScaleContinuous" text="Delete" runat="server"  CommandName ="delete" CommandArgument='<%# Bind("ID_TypScaleContinuous") %>' OnClientClick="return confirm('Are you sure?');"  />
            
            </ItemTemplate>   
            </asp:TemplateField>     
          
        </Columns>
    </asp:GridView>


    <fieldset id="fAdd" runat="server" >
    <legend >Add new continuous scale</legend>
       
    <table>
    <tr>
    <td><asp:Label ID="lblScaleContinuous" runat="server" Text="continuous scale:"></asp:Label></td>
    <td><asp:TextBox ID="txtScaleContinuous" runat="server" Width="300px"></asp:TextBox>
     <asp:RequiredFieldValidator ID="rfvtxtScaleContinuous" ValidationGroup="AddScaleContinuous" runat="server" ControlToValidate="txtScaleContinuous" ForeColor="Red" ErrorMessage="Please insert continuous scale!.">*</asp:RequiredFieldValidator></td>
    </tr>
    <tr>
    <td><asp:Label ID="lVerScaleCont" runat="server" Text="version:"></asp:Label></td>
    <td><asp:TextBox ID="tVerScaleCont" runat="server" Width="300px"></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="AddScaleContinuous" runat="server" ControlToValidate="tVerScaleCont" ForeColor="Red" ErrorMessage="Please insert version!.">*</asp:RequiredFieldValidator></td>
    </tr>
     <tr>
    <td><asp:Label ID="lblLoadImageMin" runat="server" Text="Load worst image:"></asp:Label></td>
     <td><asp:FileUpload ID="fuLoadImageMin"  runat="server" Width="400px" accept=".dcm,.ima"></asp:FileUpload>
          <asp:RequiredFieldValidator ID="rfvLoadImage" ValidationGroup="AddScaleContinuous" runat="server" ControlToValidate="fuLoadImageMin" ForeColor="Red" ErrorMessage="Please load Images Min!.">*</asp:RequiredFieldValidator></td>
      </tr>
     <tr>
     <td><asp:Label ID="lblLoadImageMax" runat="server" Text="Load best image:"></asp:Label></td>
     <td><asp:FileUpload ID="fuLoadImageMax"  runat="server" Width="400px" accept=".dcm,.ima"></asp:FileUpload>
          <asp:RequiredFieldValidator ID="rfvLoadImageMax" ValidationGroup="AddScaleContinuous" runat="server" ControlToValidate="fuLoadImageMax" ForeColor="Red" ErrorMessage="Please load Images Max!.">*</asp:RequiredFieldValidator></td>
      </tr>
    </table>
    <asp:Button ID="btnAddScaleContinuous" ValidationGroup="AddScaleContinuous" runat="server" Text="Add"  onclick="btnAddScaleContinuous_Click" />
     </fieldset>
	
					
     <fieldset id="fUpdateContScale" runat="server" visible="false">
    <legend id="lUpdateContScale">Update continuous scale</legend> 
    <table>
    <tr>
    <td><asp:Label ID="lContScaleUpdate" runat="server" Text="continuous scale:"></asp:Label></td>
    <td><asp:TextBox ID="tContScaleUpdate" runat="server" Width="300px"></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="AddScaleContinuous" runat="server" ControlToValidate="tContScaleUpdate" ForeColor="Red" ErrorMessage="Please insert continuous scale!.">*</asp:RequiredFieldValidator></td>
    </tr>
    <tr>
    <td><asp:Label ID="lContScaleVerUpdate" runat="server" Text="version:"></asp:Label></td>
    <td><asp:TextBox ID="tContScaleVerUpdate" runat="server" Value="1" Width="300px"></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="AddScaleContinuous" runat="server" ControlToValidate="tContScaleVerUpdate" ForeColor="Red" ErrorMessage="Please insert version!.">*</asp:RequiredFieldValidator></td>
    </tr>
    <tr>
    <td>Just select new files if you want to change existing ones!</td>
    </tr>
     <tr>
    <td><asp:Label ID="lWorstUpdate" runat="server" Text="Load worst image:"></asp:Label></td>
     <td><asp:FileUpload ID="fuWorstUpdate"  runat="server" Width="400px" accept=".dcm,.ima"></asp:FileUpload></td>
      </tr>
     <tr>
     <td><asp:Label ID="lBestUpdate" runat="server" Text="Load best image:"></asp:Label></td>
     <td><asp:FileUpload ID="fuBestUpdate"  runat="server" Width="400px" accept=".dcm,.ima"></asp:FileUpload></td>
      </tr>
    </table>
    <asp:HiddenField ID="hddIdScaleContinuous" Value="0" runat="server"/>
    <asp:Button ID="btnContScaleUpdate" CssClass="button-save" runat="server" Text="Update" OnClick="btnUpdateContScale_Click" />
    <asp:Button ID="btnContScaleCancel" CssClass="button-save" runat="server" OnClick="btnCancelContScale_Click" Text="Cancel" />
    </fieldset>

  </div>
  
    <div id="tabs-3">

    <asp:GridView ID="gvTypeScaleDiscrete" DataKeyNames="ID_TypScaleDiscrete"  runat="server" AutoGenerateColumns="false" OnSelectedIndexChanging="gvTypeScaleDiscrete_SelectedIndexChanging" OnRowDeleting="gvTypeScaleDiscrete_RowDeleting" Width="100%">
        <Columns>
             <asp:BoundField DataField="ID_TypScaleDiscrete" HeaderText="ID" />
             <asp:BoundField DataField="DescriptionScaleDiscrete" HeaderText="discrete scale" /> 
             <asp:BoundField DataField="PathImageMin" HeaderText="Worst image" ItemStyle-CssClass="maxWidthGrid" />
             <asp:BoundField DataField="PathImageMax" HeaderText="Best image" ItemStyle-CssClass="maxWidthGrid" />
          <asp:TemplateField>         
          <ItemTemplate>
                   <asp:Button ID="btnEditScaleDiscrete" text="Edit" runat="server"  CommandName ="select" CommandArgument='<%# Bind("ID_TypScaleDiscrete") %>'/>
            </ItemTemplate>  
            </asp:TemplateField>  
          <asp:TemplateField> 
          <ItemTemplate>
                   <asp:Button ID="btnDeleteScaleDiscrete" text="Delete" runat="server"  CommandName ="delete" CommandArgument='<%# Bind("ID_TypScaleDiscrete") %>' OnClientClick="return confirm('Are you sure?');"  />
            
            </ItemTemplate>   
            </asp:TemplateField>     
          
        </Columns>
    </asp:GridView>

    <fieldset id="fAddDiscreteScale" runat="server" visible="true">
    <legend >Add new discrete scale</legend>
       
    <table>
    <tr>
    <td><asp:Label ID="lDiscreteScale" runat="server" Text="discrete scale:"></asp:Label></td>
    <td><asp:TextBox ID="tDiscreteScale" runat="server" Width="300px"></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="AddScaleDiscrete" runat="server" ControlToValidate="tDiscreteScale" ForeColor="Red" ErrorMessage="Please insert discrete scale!.">*</asp:RequiredFieldValidator></td>
    </tr>
     <tr>
    <td><asp:Label ID="lWorstImgDiscrete" runat="server" Text="Load worst image:"></asp:Label></td>
     <td><asp:FileUpload ID="fuWorstImgDiscrete"  runat="server" Width="400px" accept=".dcm,.ima"></asp:FileUpload>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="AddScaleDiscrete" runat="server" ControlToValidate="fuWorstImgDiscrete" ForeColor="Red" ErrorMessage="Please load Images Min!.">*</asp:RequiredFieldValidator></td>
      </tr>
     <tr>
     <td><asp:Label ID="lBestImgDiscrete" runat="server" Text="Load best image:"></asp:Label></td>
     <td><asp:FileUpload ID="fuBestImgDiscrete"  runat="server" Width="400px" accept=".dcm,.ima"></asp:FileUpload>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="AddScaleDiscrete" runat="server" ControlToValidate="fuBestImgDiscrete" ForeColor="Red" ErrorMessage="Please load Images Max!.">*</asp:RequiredFieldValidator></td>
      </tr>
    </table>
    <asp:Button ID="Button1" ValidationGroup="AddScaleDiscrete" runat="server" Text="Add"  onclick="btnAddScaleDiscrete_Click" />
     </fieldset>

     <fieldset id="fUpdateDisc" runat="server" visible="false">
    <legend id="lUpdateDisc">Update discrete scale</legend> 
    <table>
    <tr>
    <td><asp:Label ID="lUpDiscScale" runat="server" Text="discrete scale:"></asp:Label></td>
    <td><asp:TextBox ID="tUpDiscScale" runat="server" Width="300px"></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="AddScaleContinuous" runat="server" ControlToValidate="tUpDiscScale" ForeColor="Red" ErrorMessage="Please insert discrete scale!.">*</asp:RequiredFieldValidator></td>
    </tr>
    <tr>
    <td>Just select new files if you want to change existing ones!</td>
    </tr>
     <tr>
    <td><asp:Label ID="lWorstUpDisc" runat="server" Text="Load worst image:"></asp:Label></td>
     <td><asp:FileUpload ID="fuWorstUpdateDisc"  runat="server" Width="400px" accept=".dcm,.ima"></asp:FileUpload></td>
      </tr>
     <tr>
     <td><asp:Label ID="lBestUpDisc" runat="server" Text="Load best image:"></asp:Label></td>
     <td><asp:FileUpload ID="fuBestUpdateDisc"  runat="server" Width="400px" accept=".dcm,.ima"></asp:FileUpload></td>
      </tr>
    </table>
    <asp:HiddenField ID="hddIdScaleDiscrete" Value="0" runat="server"/>
    <asp:Button ID="btnUpdateDiscScale" CssClass="button-save" runat="server" Text="Update" OnClick="btnUpdateDiscScale_Click" />
    <asp:Button ID="btnCancelDiscScale" CssClass="button-save" runat="server" OnClick="btnCancelDiscScale_Click" Text="Cancel" />
    </fieldset>
  
   
  </div>
</div>

 
 <fieldset id="fConfigFooter" runat="server" visible="false">



         <!-- Allow form submission with keyboard without duplicating the dialog button -->
		 	    <div class="success"  runat="server" id="DivInfo" visible="false">
        <asp:Label id="lblInfo" runat="server" Text="New continuous scale added, please add unicode symbol in JavaScript file 'ViewImage_.js' - > 'function getUniCodeSymbol()'."></asp:Label>
    </div>
    <div class="success"  runat="server" id="DivSuccessConfig" visible="false">
                            <asp:Label id="lblSuccessConfig" runat="server" Text="Update successfully completed!"></asp:Label>
                    </div>

                    <div class="error"  runat="server" id="DivErrorConfig" visible="false">
                            <asp:Label id="lblErrorConfig" runat="server" Text=""></asp:Label>
                    </div>

 </fieldset>

</asp:Content>
