<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MySms.aspx.cs" Inherits="DRP.WEB.Module.My.MySms" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>手机短信</title>
    <script src="../../Scripts/Plugin/date_time/WdatePicker.js"></script>
    <script src="Script/MySms.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="toolbar">
            消息内容：<asp:TextBox ID="txtKey" ClientIDMode="Static" runat="server"></asp:TextBox>
            日期区间：
            <asp:TextBox ID="sDate" Width="90" runat="server" onclick="WdatePicker()"></asp:TextBox>
            ～
            <asp:TextBox ID="eDate" Width="90" runat="server" onclick="WdatePicker()"></asp:TextBox>
            <a href="javascript:;" id="btnQuery" class="easyui-linkbutton" iconcls="icon-search">查询</a>
            <a href="javascript:;" id="btnSend" class="easyui-linkbutton" iconcls="icon-add">发消息</a>
        </div>
        <table id="tblData"></table>
    </form>
</body>
</html>
