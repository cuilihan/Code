<%@ Page Title="" Language="C#" MasterPageFile="~/eShop.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="DRP.eShop.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Script/reg.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="form-signin-heading" style="font-size: 18px; text-align: center;">
        <asp:Literal ID="lblCompany" runat="server"></asp:Literal>—用户注册
    </h2>
    <div style="margin: 10px 5px;">
        <asp:TextBox ID="txtMobile" runat="server" ClientIDMode="Static" CssClass="form-control" placeholder="请输入手机号"></asp:TextBox>
        <a href="javascript:;" id="btnSendSms" style="margin: 10px 5px; display: inline-block;">发送验证码</a>
        <span style="display: inline-block;" id="spTimer"></span>
        <asp:TextBox ID="txtCode" runat="server" ClientIDMode="Static" CssClass="form-control" placeholder="请输入验证码"></asp:TextBox>
        <div class="checkbox" style="color: red;">
            <asp:Literal ID="lblTips" runat="server"></asp:Literal>
        </div>
        <div style="margin: 10px 0px;">
            <asp:Button runat="server" ID="btnLogin" CssClass="btn btn-lg btn-primary btn-block" OnClick="btnLogin_Click" ClientIDMode="Static" Text="立即注册" />

        </div>
        <div style="text-align: center;">
            如果您已有登录账号，请“<asp:Literal ID="lblLoginUrl" runat="server"></asp:Literal>”
        </div>
    </div>
</asp:Content>
