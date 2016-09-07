<%@ Page Language="C#" EnableViewState="false"  AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="DRP.eShop.Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>错误提示</title>
    <style type="text/css">
        .error {
            display: inline-block;
            height: 30px;
            width: 90%;
            margin: 10px 10px 5px 10px;
            -webkit-border-radius: 8px;
            -moz-border-radius: 8px;
            border-radius: 8px; 
            text-align: center;
            line-height: 30px;
            font-size: 20px;
            text-decoration:none;
            font-family: 微软雅黑;
            background: #25A6DE url(UI/image/grad-overlay.png) repeat-x;
            color: #fff;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="font-size: 20px;   font-family: 微软雅黑; color:#f00; text-align: center;">
            <asp:Literal ID="lblMessage" runat="server"></asp:Literal>
        </div>
        <div style="font-size: 15px; text-align: center; margin: 15px 10px;">
            <a href="#" class="error" onclick="window.location.href=document.referrer;">退回上一操作</a>
        </div>
    </form>
</body>
</html>
