<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="DRP.WEB.Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>旅管家订单业务管理系统</title>
    <link href="UI/themes/login/Login.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
    <style type="text/css">
        .error
        {
            background: url(UI/themes/login/images/chose.jpg) no-repeat;
            height: 300px;
            padding: 50px 0px 0px 350px;
        }
    </style>
</head>
<body style="overflow: hidden;">
    <form id="form1" runat="server">
        <div style="background-color: #28A7E1;">
            <div class="logintop">
                <span>旅管家订单业务管理系统</span>
            </div>
        </div>
        <div class="loginbody">
            <div class="error">
                <div style="font-size: 40px; font-weight: bold;">对不起，您的页面暂时不能访问！</div>
                <div style="margin: 20px 0px 10px 0px; font-size: 22px; text-align: left; padding-left: 40px; color: red;">
                    <asp:Literal ID="lblErrInfo" runat="server"></asp:Literal>
                </div>
                <div style="margin-top: 80px; text-align: center;">
                    <a style="font-size: 15px; font-family: 微软雅黑;" target="_top" href="/">返回首页</a>  <a style="margin-left: 20px; font-size: 15px; font-family: 微软雅黑;" href="/Login.aspx">重新登录</a>
                </div>
            </div>
        </div>

        <div class="loginfooter">
            <div class="loginbm">
                Copyright © 2014. All Rights Reserved 技术支持：<a href="http://tms.58datu.com" target="_blank">苏州大途网络科技有限公司</a>
            </div>
        </div>
    </form>
</body>
</html>
