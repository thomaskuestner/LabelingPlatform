<%-- 
k-Space Astronauts labeling platform
    (c) 2016 under Apache 2.0 license 
    Thomas Kuestner, Martin Schwartz, Philip Wolf
    Please refer to https://sites.google.com/site/kspaceastronauts/iqa/labelingplatform for more information    
--%>
<%@ Page Title="Log In" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="LabelingFramework.Account.Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

                  <fieldset id="loginField">
            
                    <legend> Login</legend>
    <p>
        Please enter your username and password.
        <asp:HyperLink ID="RegisterHyperLink" runat="server" EnableViewState="false" NavigateUrl="~/Account/Register.aspx">Register</asp:HyperLink> if you don't have an account.
    </p>
   
            <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification" 
                 ValidationGroup="LoginUserValidationGroup"/>
        
         <div class="login" style="vertical-align:top;">
                

                   <div style="padding:5px;vertical-align:top;">
                        <asp:Label ID="lblUsername" runat="server" AssociatedControlID="txtUsername" >Username:</asp:Label>
                        <asp:TextBox ID="txtUsername" runat="server"  Width="180px" Height="20px"  maxlength="20"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtUsername" 
                             CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required." 
                             ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                     </div>
                    <div style="padding:5px;vertical-align:top;">
                        <asp:Label ID="lblPassword" runat="server" AssociatedControlID="txtPassword">&nbsp;Password:</asp:Label>
                        <asp:TextBox ID="txtPassword" runat="server"  TextMode="Password"  Width="180px" Height="20px" maxlength="20"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtPassword" 
                             CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required." 
                             ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                   </div>
<!--                     <div>
                        <asp:CheckBox ID="RememberMe" runat="server"/>
                        <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe" CssClass="inline">Keep me logged in</asp:Label>
                    
                    </div> -->
                    </div>
                     <p>     
                         <asp:HyperLink ID="hpChangePassword" runat="server" EnableViewState="false" NavigateUrl="~/Account/ChangePassword.aspx">Change Password</asp:HyperLink> if forgotten
                    </p>
   


                     </fieldset>

                <p class="submitButton">
                    <asp:Button ID="btnLogin" runat="server" CommandName="Login" Text="Log In" ValidationGroup="LoginUserValidationGroup" OnClick="btnLogin_Click" ToolTip="Login with your Username and Password"/>
					<noscript> <font color="red">This web page relies on Javascript in order to work properly. Please enable Javascript in your browser.</font></noscript>
                </p>
				

            </div>
</asp:Content>
