<%-- 
k-Space Astronauts labeling platform
    (c) 2016 under Apache 2.0 license 
    Thomas Kuestner, Martin Schwartz, Philip Wolf
    Please refer to https://sites.google.com/site/kspaceastronauts/iqa/labelingplatform for more information    
--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProfileView.aspx.cs" Inherits="LabelingFramework.ProfileView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 
  <asp:ValidationSummary ID="RVFValidationSummaryUser" runat="server" CssClass="validation" ValidationGroup="RegisterUserValidationGroup"/>

    <fieldset class="register">
                            <legend>Account Change</legend>
                            <asp:HiddenField ID="iduser" runat="server" />
                            <div class="divtable">
			                	<div class="container">
				                	<div class="contaneir-label">
                                              <asp:Label ID="lblName" runat="server" AssociatedControlID="txtName" CssClass="label">Name:</asp:Label>
                                    </div>
                                    <div class="contaneir-object">
                                                <asp:TextBox ID="txtName" runat="server"  CssClass="textBox-insert" maxlength="30"></asp:TextBox>
                                    </div>
                                  </div>
                             
                             <div class="container">
                                  <div class="contaneir-label">
                                        <asp:Label ID="lblSurname" runat="server" AssociatedControlID="txtSurname" CssClass="label">Surname:</asp:Label>
                                  </div>
                                   <div class="contaneir-object">
                                         <asp:TextBox ID="txtSurname" runat="server" CssClass="textBox-insert" maxlength="30"></asp:TextBox>
                                              
                                   </div>
                             </div>

                              <div class="container">
                                  <div class="contaneir-label">
                                      <asp:Label ID="lblUsername" runat="server" AssociatedControlID="txtUserName" CssClass="label">User Name:</asp:Label>
                                  </div>
                                   <div class="contaneir-object">
                                           <asp:TextBox ID="txtUserName" Enabled="false" runat="server" CssClass="textBox-insert" maxlength="20"></asp:TextBox>
                                           
                                    </div>
                             </div>
							 
							 
							<div class="container">
                                  <div class="contaneir-label">
                                         <asp:Label ID="lblPassword" runat="server" AssociatedControlID="txtPassword" CssClass="label">New Password:</asp:Label>
                                  </div>
                                   <div class="contaneir-object">
                                                              <asp:TextBox ID="txtPassword" runat="server"  CssClass="textBox-insert" TextMode="Password" maxlength="20"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtPassword" 
                                     CssClass="failureNotification" ErrorMessage="Password is required and Password has to be longer than 6 characters" ToolTip="Password is required." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                    <asp:CustomValidator ID="CVPassword" runat="server" 
                                    onservervalidate="CVPassword_ServerValidate" ValidationGroup="RegisterUserValidationGroup"  ErrorMessage="Password has to be longer than 6 characters" ForeColor="Red" Display="Dynamic">*</asp:CustomValidator>
                                   </div>
                             </div>
                          
                           <div class="container">
                           <div class="contaneir-label">
                                <asp:Label ID="lblConfirmPassword" runat="server" AssociatedControlID="txtConfirmPassword"  CssClass="label">Confirm New Password:</asp:Label>
                             </div>
                             <div class="contaneir-object">
                                <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="textBox-insert" TextMode="Password" maxlength="20"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtConfirmPassword" CssClass="failureNotification" Display="Dynamic" 
                                     ErrorMessage="Confirm Password is required." ID="ConfirmPasswordRequired" runat="server" 
                                     ToolTip="Confirm Password is required." ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" 
                                     CssClass="failureNotification" Display="Dynamic" ErrorMessage="The Password and Confirmation Password must match."
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:CompareValidator>
                              </div>
                            </div>
                          
                          
                             <div class="container">
                           <div class="contaneir-label">
                                <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail"  CssClass="label">E-mail:</asp:Label>
                                </div>
                             <div class="contaneir-object">
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="textBox-insert" maxlength="100"></asp:TextBox>
                                 <asp:RegularExpressionValidator ID="REVEmail" runat="server" ErrorMessage="Please Enter Valid Email ID"   ValidationGroup="RegisterUserValidationGroup" ControlToValidate="txtEmail" 
                                  ForeColor="Red"  ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                            </div>
                            </div>
                           
                                           
                        
                        </div>
                        </fieldset>
                        
                            <asp:Button ID="CreateUserButton" CssClass="button-save" runat="server" CommandName="MoveNext" Text="Update User" 
                                 ValidationGroup="RegisterUserValidationGroup" ClientIDMode="Static" 
                                onclick="UpdateButton_Click"/>
                    
                   

                     <div class="success"  runat="server" id="DivSuccess" visible="false">
                            <asp:Label id="lblSuccess" runat="server" Text="Update successfully completed!"></asp:Label>
                    </div>

                    <div class="error"  runat="server" id="DivError" visible="false">
                            <asp:Label id="lblError" runat="server" Text=""></asp:Label>
                    </div>

                   

                   

      
</asp:Content>
