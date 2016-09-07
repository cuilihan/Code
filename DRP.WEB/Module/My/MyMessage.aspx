<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyMessage.aspx.cs" Inherits="DRP.WEB.Module.My.MyMessage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>我的消息中心</title>
    <script src="../../Scripts/Plugin/date_time/WdatePicker.js"></script>
    <script src="Script/Message.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="toolbar">
            消息状态：<asp:DropDownList ID="ddlStatus" ClientIDMode="Static" runat="server">
                <asp:ListItem Text="所有" Value=""></asp:ListItem>
                <asp:ListItem Text="未读" Value="1" Selected="True"></asp:ListItem>
                <asp:ListItem Text="已读" Value="2"></asp:ListItem>
            </asp:DropDownList>
            日期区间：
            <asp:TextBox ID="sDate" Width="90" runat="server" onclick="WdatePicker()"></asp:TextBox>
            ～
            <asp:TextBox ID="eDate" Width="90" runat="server" onclick="WdatePicker()"></asp:TextBox>
            <a href="javascript:;" id="btnQuery" class="easyui-linkbutton" iconcls="icon-search">查询</a>
            <a href="javascript:;" id="btnSetStatus" class="easyui-linkbutton">标为已读</a>
        </div>
        <table id="tblData"></table>
    </form>
</body>
</html>
