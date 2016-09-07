<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Success.aspx.cs" Inherits="DRP.WEB.Module.Pro.Success" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>订单提交成功</title>
    <style type="text/css">
        .check
        {
            background: url(../../App_Themes/Default/Images/check.png) 60px 80px;
            background-repeat: no-repeat;
            height: 400px;
            padding: 85px 0px 0px 180px;
            color: Red;
            font-size: 30px;
            font-family: 微软雅黑;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="check">
            恭喜您！订单提交成功！
        <div style="font-size: 14px; color: #000; font-weight: bold; padding-top: 8px; padding-bottom:5px; border-top: 1px solid #ccc;">
            您现在可以：
            <div style="margin-top:5px;">
                <asp:HyperLink ID="lnkOrder" Target="_blank" runat="server">查看订单</asp:HyperLink>
                <a href="../Order/OrderList.aspx?xType=2" style="margin-left:2em;">订单管理</a>
                <a href="ProNav.aspx" style="margin-left:2em;">目的地导航</a>
            </div>
        </div>
        </div>
    </form>
</body>
</html>
