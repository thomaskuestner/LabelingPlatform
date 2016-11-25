<%-- 
k-Space Astronauts labeling platform
    (c) 2016 under Apache 2.0 license 
    Thomas Kuestner, Martin Schwartz, Philip Wolf
    Please refer to https://sites.google.com/site/kspaceastronauts/iqa/labelingplatform for more information    
--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="LabelingFramework.Account.ChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<asp:ValidationSummary ID="RVFValidationSummaryUser" runat="server" CssClass="validation" ValidationGroup="RegisterUserValidationGroup"/>

    <fieldset class="register">
                            <legend>Change Password:</legend>
                            <asp:HiddenField ID="iduser" runat="server" />
                            <div class="divtable">
                          
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
                           
                                           
                        
                        </div>
                        </fieldset>
                        
                            <asp:Button ID="CreateUserButton" CssClass="button-save" runat="server" CommandName="MoveNext" Text="Change Password" ToolTip="Request Password change"
                                 ValidationGroup="RegisterUserValidationGroup" Width="200px" ClientIDMode="Static" 
                                onclick="UpdateButton_Click"/>
								
							 <asp:Button ID="btnTornaLogin" CssClass="button-save" runat="server" Text="Return to Login"  ToolTip="Go back to Login Page"
                                Width="200px" ClientIDMode="Static" 
                                onclick="Login_Click"/>
                    
                   

                     <div class="success"  runat="server" id="DivSuccess" visible="false">
                            <asp:Label id="lblSuccess" runat="server" Text="Password was reset, please check your emails."></asp:Label>
                    </div>

                    <div class="error"  runat="server" id="DivError" visible="false">
                            <asp:Label id="lblError" runat="server" Text=""></asp:Label>
                    </div>




</asp:Content>
