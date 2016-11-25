<%-- 
k-Space Astronauts labeling platform
    (c) 2016 under Apache 2.0 license 
    Thomas Kuestner, Martin Schwartz, Philip Wolf
    Please refer to https://sites.google.com/site/kspaceastronauts/iqa/labelingplatform for more information    
--%>
<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Register.aspx.cs" Inherits="LabelingFramework.Account.Register" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">






                    <h2>
                        Create a new account
                    </h2>
                    <p>
                        Use the form below to create a new account.
                    </p>
                    <p>
                      
                    </p>
                    <span class="failureNotification">
                        <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                    </span>

                    
                    
                    <asp:ValidationSummary ID="RVFValidationSummaryUser" runat="server" CssClass="validation" ValidationGroup="RegisterUserValidationGroup"/>
                   
                    <div class="accountInfo"></div>
                    
                        <fieldset class="register">
                            <legend>Account Information</legend>

                            <div class="divtable">

							<div class="container">
                           <div class="contaneir-label">
                                <asp:Label ID="lblUserTitle" runat="server" AssociatedControlID="ddlUserTitle" CssClass="label">Title/Salutation:</asp:Label>
                              </div>
                             <div class="contaneir-object">
                                <asp:DropDownList ID="ddlUserTitle" runat="server" CssClass="dropdownlist">
                              
                                </asp:DropDownList>
                                 </div>
                            </div>
							
							
			                	<div class="container">
				                	<div class="contaneir-label">
                                              <asp:Label ID="lblName" runat="server" AssociatedControlID="txtName" CssClass="label">Name:</asp:Label>
                                    </div>
                                    <div class="contaneir-object">
                                                <asp:TextBox ID="txtName" runat="server"  CssClass="textBox-insert" maxlength="30"></asp:TextBox>
                                                       <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName" 
                                                          CssClass="failureNotification" ErrorMessage="Name is required." ToolTip="Name is required." 
                                                  ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                    </div>
                                  </div>
                             
                             <div class="container">
                                  <div class="contaneir-label">
                                        <asp:Label ID="lblSurname" runat="server" AssociatedControlID="txtSurname" CssClass="label">Surname:</asp:Label>
                                  </div>
                                   <div class="contaneir-object">
                                         <asp:TextBox ID="txtSurname" runat="server" CssClass="textBox-insert" maxlength="30"></asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="rfvVorName" runat="server" ControlToValidate="txtSurname" 
                                     CssClass="failureNotification" ErrorMessage="Surname is required." ToolTip="Surname is required." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                   </div>
                             </div>

                              <div class="container">
                                  <div class="contaneir-label">
                                      <asp:Label ID="lblUsername" runat="server" AssociatedControlID="txtUserName" CssClass="label">User Name:</asp:Label>
                                  </div>
                                   <div class="contaneir-object">
                                                             <asp:TextBox ID="txtUserName" runat="server" CssClass="textBox-insert" maxlength="20"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtUserName" 
                                     CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                <asp:CustomValidator ID="CVUsername" runat="server" 
                                    onservervalidate="CVUsername_ServerValidate" ValidationGroup="RegisterUserValidationGroup"  ErrorMessage="This username is already in use, please choose a different one" ForeColor="Red" Display="Dynamic">*</asp:CustomValidator>
                                   </div>
                             </div>


                              <div class="container">
                                  <div class="contaneir-label">
                                         <asp:Label ID="lblPassword" runat="server" AssociatedControlID="txtPassword" CssClass="label">Password:</asp:Label>
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
                                <asp:Label ID="lblConfirmPassword" runat="server" AssociatedControlID="txtConfirmPassword"  CssClass="label">Confirm Password:</asp:Label>
                             </div>
                             <div class="contaneir-object">
                                <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="textBox-insert" TextMode="Password" maxlength="16"></asp:TextBox>
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
                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="txtEmail" 
                                     CssClass="failureNotification" ErrorMessage="E-mail is required." ToolTip="E-mail is required." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                 <asp:RegularExpressionValidator ID="REVEmail" runat="server" ErrorMessage="Please Enter Valid Email ID"   ValidationGroup="RegisterUserValidationGroup" ControlToValidate="txtEmail" 
                                  ForeColor="Red"  ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                            </div>
                            </div>
                            <div class="container">
                           <div class="contaneir-label">
                                <asp:Label ID="lblYearsExperience" runat="server" AssociatedControlID="txtYearsExperience"  CssClass="label"> Years Experience: </asp:Label>
                               </div>
                             <div class="contaneir-object">
                                <asp:TextBox ID="txtYearsExperience" runat="server" CssClass="textBox-insert" maxlength="2"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvYearsExperience" runat="server" ControlToValidate="txtYearsExperience" 
                                     CssClass="failureNotification" ErrorMessage="Years Experience is required." ToolTip="Years Experience is required." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                           </div>
                            </div>
                              <div class="container">
                           <div class="contaneir-label">
                                <asp:Label ID="lblUserType" runat="server" AssociatedControlID="ddlUserType" CssClass="label">User Type:</asp:Label>
                              </div>
                             <div class="contaneir-object">
                                <asp:DropDownList ID="ddlUserType" runat="server" CssClass="dropdownlist">
                              
                                </asp:DropDownList>
                                 </div>
                            </div>
                        </div>
                        </fieldset>
                        
                            <asp:Button ID="CreateUserButton" CssClass="button-save" runat="server" CommandName="MoveNext" Text="Create User" 
                                 ValidationGroup="RegisterUserValidationGroup" ClientIDMode="Static" 
                                onclick="CreateUserButton_Click" ToolTip="Create a new Profile" />
							<asp:Button ID="BackToLogin" CssClass="button-save" runat="server" CommandName="MoveNext" Text="return to login" 
                                ClientIDMode="Static" 
                                onclick="BackToLogin_Click" ToolTip="Go back to Login Page"/>
                    
                   

                     <div class="success"  runat="server" id="DivSuccess" visible="false">
                            <asp:Label id="lblSuccess" runat="server" Text="Registration successfully completed!"></asp:Label>
                    </div>

                    <div class="error"  runat="server" id="DivError" visible="false">
                            <asp:Label id="lblError" runat="server" Text=""></asp:Label>
                    </div>

</asp:Content>
