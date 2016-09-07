<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DRP.WeChat.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />

    <title>旅管家业务管理系统</title>

    <!-- Bootstrap core CSS -->
    <link href="/Script/bootstrap/css/bootstrap.min.css" rel="stylesheet" />

    <!-- Custom styles for this template -->
    <link href="/UI/wap.css" rel="stylesheet" />

    <!-- Just for debugging purposes. Don't actually copy these 2 lines! -->
    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="http://cdn.bootcss.com/html5shiv/3.7.0/html5shiv.js"></script>
      <script src="http://cdn.bootcss.com/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <div class="container">
        <form id="form1" runat="server" class="form-signin" role="form">
            <h2 class="form-signin-heading">
                <img src="/UI/image/logo.png" title="TMS" alt="旅管家业务管理系统" /></h2>
            <h4 class="form-signin-heading" style="color: #90C42B; float: right">——<asp:Literal ID="lblOrgInfo" Text="旅管家" runat="server"></asp:Literal></h4>
            
            <asp:TextBox ID="txtUserAcct" runat="server" ClientIDMode="Static" CssClass="form-control" placeholder="请输入登录帐号"></asp:TextBox>
            <asp:TextBox ID="txtUserPwd" TextMode="Password" runat="server" ClientIDMode="Static" CssClass="form-control" placeholder="请输入登录密码"></asp:TextBox>

            <div class="checkbox" style="color: red;">
                <asp:Literal ID="lblTips" runat="server"></asp:Literal>
            </div>
            <asp:Button runat="server" ID="btnLogin" CssClass="btn btn-lg btn-primary btn-block" ClientIDMode="Static" OnClick="btnLogin_Click" Text="登录" />
        </form>
    </div>
</body>
</html>
