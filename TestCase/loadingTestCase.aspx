<%-- 
k-Space Astronauts labeling platform
    (c) 2016 under Apache 2.0 license 
    Thomas Kuestner, Martin Schwartz, Philip Wolf
    Please refer to https://sites.google.com/site/kspaceastronauts/iqa/labelingplatform for more information    
--%>
<%@ Page Title="loadingTestcase" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="loadingTestCase.aspx.cs" Inherits="LabelingFramework.loadingTestCase" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Timer ID="Timer" OnTick="Timer_Tick" runat="server" Interval="1000">
            </asp:Timer>
    <asp:UpdatePanel ID="upLoading" UpdateMode="Conditional" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Timer" EventName="Tick" />
            </Triggers>
            <ContentTemplate>
                <asp:Label ID="lblMessage" runat="server" Text="loading test case... please wait!" Font-Bold="True" Font-Size="X-Large" ForeColor="Black"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
    <img style="align-self:center; position: relative;" alt="LoadingImage" src="../images/Loading.gif" />
</asp:Content>
