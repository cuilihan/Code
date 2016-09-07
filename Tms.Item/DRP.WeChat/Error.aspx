<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="DRP.WeChat.Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="color:red; text-align:center; font-family:微软雅黑; font-size:14px;">
        <asp:Literal Text="发生未知错误" ID="lblError" runat="server"></asp:Literal>
    </div>
</asp:Content>
