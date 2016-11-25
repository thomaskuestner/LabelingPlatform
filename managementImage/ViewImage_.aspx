<%-- 
k-Space Astronauts labeling platform
    (c) 2016 under Apache 2.0 license 
    Thomas Kuestner, Martin Schwartz, Philip Wolf
    Please refer to https://sites.google.com/site/kspaceastronauts/iqa/labelingplatform for more information    
--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master"validateRequest="false" AutoEventWireup="true" CodeBehind="ViewImage_.aspx.cs" Inherits="LabelingFramework.managementImage.ViewImage_" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" href="/Scripts/jquery-ui-smoothness.css"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<%-- <asp:Label ID="Label1" Font-Size="Small" runat="server" ForeColor="Red" Text="(When finished, log out before closing the page!)"></asp:Label>--%>

<div id="popup1" class="overlay">
	<div class="popup">
		<header2>Report an error to the admin</header2>
		<a class="close" id="closeMessageBox">×</a>
		<div class="content">
            <form class="form" id="contact">
		
                <textarea id="message" placeholder="Insert your message..." ></textarea><br/>
                <br/>
				<input id="sendButton" type="button" class="button-save" value="Send message" ToolTip="Send Message to the Administrator">
            </form>
    
		</div>
	</div>
</div>

<div id="popup3" class="overlay">
	<div class="popup" id="popup3Scroll">
		<header3>Information <img src="imgHelp/tutorial.png" width="35" height="35"></header3>
		<a class="close" id="closeTutorial">×</a>
		<div class="content" id="TutorialPage" width="100%" height="360px">
		</div>
	</div>
</div>

<div id="popup2" class="overlay">
	<div class="popup" id="popup2Scroll">
		<header2>Shortcuts</header2>
		<a class="close" id="closeHelp">×</a>
		<div class="content">
		
<table width="600px">
<tr><td/><td/><td width="23%"></td><td width="22%"></td></tr>
<tr>
<td width="35%"></td>
<td width="20%" align="center">mouse</td>
<td width="40%" align="center" colspan="2">keyboard</td>
</tr>
<tr>
<td>move image</td>
<td align="center"><img src="imgHelp/mouse_move.png" height="200px"></td>
<td align="center" colspan="2">[m] + [&#8593; | &#8595; | &#8592; | &#8594;]</td>
</tr>
<tr>
<td>zoom image</td>
<td align="center"><img src="imgHelp/mouse_zoom.png" height="200px"></td>
<td align="center" colspan="2">[z] + [&#8592; | &#8594;]</td>
</tr>
<tr>
<td>scroll through image slices</td>
<td align="center"><img src="imgHelp/mouse_slice.png" height="200px"></td>
<td align="right">next slice:<br/>previous slice:</td>
<td align="left">[2 | (NUM) 2 | &#8595;]<br/>[1 | (NUM) 1 | &#8593;]</td>
</tr>
<tr>
<td>brightness/contrast scaling</td>
<td align="center"><img src="imgHelp/mouse_bc.png" height="200px"></td>
<td align="right">brightness up:<br/>brightness down:<br/>contrast up:<br/>contrast down:</td>
<td align="left">[b] + [&#8594;]<br/>[b] + [&#8592;]<br/>[c] + [&#8594;]<br/>[c] + [&#8592;]</td>
</tr>
<tr>
<td>rotate image (90&#176; counterclockwise)</td>
<td align="center"><img src="imgHelp/mouse_rotate.png" height="200px"></td>
<td align="center" colspan="2">[CTRL] + [r]</td>
</tr>
<tr>
<td>reset view</td>
<td align="center"><img src="imgHelp/mouse_reset.png" height="200px"></td>
<td align="center" colspan="2">[CTRL] + [ALT]</td>
</tr>
<tr>
<td>link images</td>
<td align="center"><img src="imgHelp/mouse_link.png" height="200px"></td>
<td align="center" colspan="2">[CTRL] + [L]</td>
</tr>
<tr>
<td>enter/exit fullscreen mode</td>
<td align="center">press &#x239a</td>
<td align="center" colspan="2">[CTRL] + [ENTER]</td>
</tr>
<tr>
<td>change page</td>
<td align="center">press [<<|<|>|>>]</td>
<td align="right">next page:<br/>previous page:</td>
<td align="left">[&#8594;]<br/>[&#8592;]</td>
</tr>
</table>
		
		
		
		</div>
	</div>
</div>

<div>
    <div id="idInformationContainer" >   
	<table width="100%">                             
		<tr>
        <td width="75%"><fieldset>     
            <div>
                <b> <asp:Label ID="lblTextNameTestCase" runat="server" Text="Name Test Case:"></asp:Label></b>
                <asp:Label ID="lblNameTestCase" runat="server" Text=""></asp:Label>
            </div>
            <div>
                <b><asp:Label ID="lblTextTestQuestion" runat="server" Text="Test Question:"></asp:Label></b>
                <asp:Label ID="lblTestQuestion" runat="server" Text=""></asp:Label>
            </div>
            <div>
                <b><asp:Label ID="lblTextGeneralInfo" runat="server" Text="General Information:"></asp:Label></b>
                <asp:Label ID="lblGeneralInfo" runat="server" Text=""></asp:Label>
            </div>
        </fieldset></td>
		<td width="25%">
		
	
	
	<div style="display:none">
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" CausesValidation="False"/>
    </div>
	

    <asp:UpdatePanel ID="upLoading" UpdateMode="Conditional" runat="server" >
	
            <Triggers>
                 <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"/> 
            </Triggers>
			<ContentTemplate>
<asp:GridView ID="gvGlobalReference" runat="server" DataKeyNames="ID_TypScale" OnDataPropertyChanged="gvTestCase_OnDataPropertyChanged" AutoGenerateColumns="false" Width="100%" HorizontalAlign="Center" >
    <Columns>
	
        <asp:BoundField DataField="DescriptionScale" HeaderText="description" />

		<asp:TemplateField HeaderText="best">
             <ItemTemplate>	
             <asp:Image ID="imageWorst" CssClass="imgReferenceBest" runat="server"  ImageUrl='<%#Eval("PathImageMax")%>' />
			</ItemTemplate>
         <ItemStyle CssClass="gvItemCenter"/>
         <HeaderStyle CssClass="gvItemCenter" />
         </asp:TemplateField>
		 
		 		<asp:TemplateField HeaderText="worst">
             <ItemTemplate>	
             <asp:Image ID="imageWorst" CssClass="imgReferenceWorst" runat="server"  ImageUrl='<%#Eval("PathImageMin")%>' />
			</ItemTemplate>
         <ItemStyle CssClass="gvItemCenter"/>
         <HeaderStyle CssClass="gvItemCenter" />
         </asp:TemplateField>
		
    </Columns>
</asp:GridView>	
</ContentTemplate>
        </asp:UpdatePanel>
		
	
	
		</td>
			</tr>					
</table>  		
    </div>
    <div id="idSliderContainer" oncontextmenu="return false;" align="center">

        <div align="right" style="height: 95px;">	
		<center>
			<table>  
			<tr>
			<!-- <td><input id="btnreset" type="button" class="customButton" src="/imgHelp/reset.png" value="reset all" ToolTip="Reset all Images"></td> -->
			<td><input id="btnEndTestcase" type="image" class="customButton" src="imgHelp/closed.png" width="48" height="48" title="End Testcase"></td>
			<td><input id="btnHelp" type="image" class="customButton" src="imgHelp/help.png" width="35" height="35" title="Show Help"></td>
			<td><input id="btnTutorial" type="image" class="customButton" src="imgHelp/tutorial_passiv.png" width="35" height="35" title="Show tutorial page"></td>
			<td><input id="btnreport" type="image" class="customButton" src="imgHelp/error.png" width="35" height="35" title="Report an Error to the Admin"></td>
			<td><input id="btnreset" type="image" class="customButton" src="imgHelp/reset.png" width="35" height="35" title="Reset all Images"></td>
			<td><input id="CheckMerge" type="image" class="customButton" src="imgHelp/link_passiv.png" width="35" height="35" title="Link all images"></td>

<!-- 			<td></td>
			<td></td>
			<td></td>
			<td></td> -->
			</tr>
<!-- 				<tr>
					<td><asp:Button ID="btnEndTestcase" runat="server" CssClass="button-save" ClientIDMode ="Static" index="0"  Text="End testcase" ToolTip="End Testcase" /></td>
					<td><input id="btnreset" type="button" class="button-save" value="reset all" ToolTip="Reset all Images"></td>
				</tr>
				<tr>
					<td><asp:Button ID="btnTutorial" runat="server" CssClass="button-save" ClientIDMode ="Static" index="0"  Text="tutorial" ToolTip="Show tutorial page"/></td>
					<td><input id="btnreport" type="button" class="button-save" value="report error" ToolTip="Report an Error to the Admin"></td>
					<td><input type="checkbox" id="CheckMerge" ToolTip="Linkl all Images" style="position: relative;"><label style="position: relative;">&nbsplinked</label></td>
				</tr> -->
					
			</table>  
         </center>
			
	  </div>
    </div>     
</div>

<div id="idWrapperContainer" oncontextmenu="return false;" align="center">


</div>

<%--<div id="output"></div>--%>
<asp:HiddenField id="hddLenght" runat="server" ClientIDMode="Static" Value="68" ></asp:HiddenField>
<asp:HiddenField id="hddTutorial" runat="server" ClientIDMode="Static" Value="" ></asp:HiddenField>
<asp:HiddenField id="hddPath" runat="server"  ClientIDMode="Static" ></asp:HiddenField>
<asp:HiddenField id="hddIdGroupHasImage" runat="server"  ClientIDMode="Static"  Value="0"></asp:HiddenField>
<asp:HiddenField id="hddNumberImagePage" runat="server"  ClientIDMode="Static"  Value="0"></asp:HiddenField>
<asp:HiddenField id="hddNumberImagePatient" runat="server"  ClientIDMode="Static"  Value="0"></asp:HiddenField>
<asp:HiddenField id="logoutTime" runat="server" ClientIDMode="Static" Value="" ></asp:HiddenField>
<asp:HiddenField id="disableTutorial" runat="server" ClientIDMode="Static" Value="" ></asp:HiddenField>

<asp:Literal ID="ltVoteDiskrete" runat="server"> </asp:Literal>
<asp:Literal ID="ltVoteContinuous" runat="server"> </asp:Literal>
<center><div class="ForwardBackwardButton" id="ImageGroupButtons" style="width:70%; bottom:0px; position:relative">
	<asp:Button ID="btnPreviousUnlabelled" runat="server" CssClass="button-save" ClientIDMode ="Static" index="0"  Text="<<" ToolTip="Go to previous Page with unlabelled Images" />
    <asp:Button ID="btnBack" runat="server" CssClass="button-save" ClientIDMode ="Static" index="0"   Text="<" ToolTip="Go to previous Page" />
	<asp:Button ID="btnProgress" runat="server" CssClass="button-save" ClientIDMode ="Static" index="0"  Text="-" disabled ToolTip="Current labelling Progress"/>
    <asp:Button ID="btnForward" runat="server" CssClass="button-save" ClientIDMode ="Static" index="0"  Text=">" ToolTip="Go to next Page"/>
	<asp:Button ID="btnNextUnlabelled" runat="server" CssClass="button-save" ClientIDMode ="Static" index="0"  Text=">>" ToolTip="Go to next Page with unlabelled Images"/>
</div></center>

<%-- javascript for slider control & more --%>
<script type="text/javascript" src="/Scripts/jquery-ui-1.11.4/jquery-1.11.3.min.js"></script>
<script type="text/javascript" src="/Scripts/jquery-ui-1.11.4/jquery-ui.min.js"></script>
<script type="text/javascript" src="/Scripts/jquery-ui-1.11.4/jquery-ui.js"></script>

<%-- css for slider control --%>
<link id="Link1" rel="Stylesheet" runat="server" media="screen" href="/Scripts/jquery-ui-1.11.4/jquery-ui.css" />

<%-- javascript for this page --%>
<script type="text/javascript" src="/Scripts/javaScript/class_Image.js"></script>
<script type="text/javascript" src="/Scripts/javaScript/ViewImage_.js"></script>
</asp:Content>