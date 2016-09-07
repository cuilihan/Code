<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TradeOrder.aspx.cs" Inherits="DRP.WEB.Module.Crm.TradeOrder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>客户的订单查询</title>
    <script src="../../Scripts/Plugin/date_time/WdatePicker.js"></script>
    <script src="Script/TradeOrder.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="toolbar">
            订单名称：
                    <asp:TextBox ID="txtName" Width="90" runat="server"></asp:TextBox>
            <span style="padding-left: 1em;">出团日期：</span>
            <asp:TextBox ID="sDate" Width="80" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
            ~
                        <asp:TextBox ID="eDate" Width="80" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>

            <span style="padding-left: 1em;"><a class="easyui-linkbutton" iconcls="icon-search" id="btnQuery">查询</a></span>
        </div>

        <table id="tblData"></table>
    </form>
</body>
</html>
