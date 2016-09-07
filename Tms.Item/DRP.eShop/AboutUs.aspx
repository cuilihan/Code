<%@ Page Title="关于我们" EnableViewState="false"  Language="C#" MasterPageFile="~/eShop.Master" AutoEventWireup="true" CodeBehind="AboutUs.aspx.cs" Inherits="DRP.eShop.AboutUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="padding: 15px 10px; line-height: 25px; font-family: 微软雅黑;">
        <asp:Literal ID="lblAboutUs" runat="server"></asp:Literal>
    </div>
</asp:Content>
