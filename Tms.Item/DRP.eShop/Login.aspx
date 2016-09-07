<%@ Page Title="" Language="C#" MasterPageFile="~/eShop.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DRP.eShop.Login1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#btnLogin").click(function () {
                var mobile = $("#txtUserAcct").val();
                var pwd = $("#txtUserPwd").val();
                if (mobile == "") {
                    $("#lblTips").text("手机号码不能为空");
                    return false;
                }
                if (pwd == "") {
                    $("#lblTips").text("登录密码不能为空");
                    return false;
                }
                return true;
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="form-signin-heading" style="text-align: center;">
        <asp:Image ID="ImgLogo" runat="server" />
    </h2>
    <h4 class="form-signin-heading" style="text-align: center;">
        <asp:Literal ID="lblOrgInfo" Text="" runat="server"></asp:Literal></h4>
    <div style="margin: 10px 5px;">
        <asp:TextBox ID="txtUserAcct" runat="server" ClientIDMode="Static" CssClass="form-control" placeholder="请输入手机号"></asp:TextBox>
        <asp:TextBox ID="txtUserPwd" TextMode="Password" runat="server" ClientIDMode="Static" CssClass="form-control" placeholder="请输入登录密码"></asp:TextBox>

        <div class="checkbox" style="color: red;">
            <asp:Label ClientIDMode="Static" ID="lblTips" runat="server"></asp:Label>
        </div>
        <asp:Button runat="server" ID="btnLogin" CssClass="btn btn-lg btn-primary btn-block" ClientIDMode="Static" OnClick="btnLogin_Click" Text="登录" />
        <div style="text-align:center; margin-top:10px;">
            如果您还没有登录账号，请点击此处“<asp:Literal ID="lblRegUrl" runat="server"></asp:Literal>”
        </div>
    </div>
    
</asp:Content>
