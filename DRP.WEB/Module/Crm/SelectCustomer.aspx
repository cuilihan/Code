<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectCustomer.aspx.cs" Inherits="DRP.WEB.Module.Crm.SelectCustomer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>选择客户</title>
    <script src="Script/SelectedCustomer.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="toolbar">
            姓名：<asp:TextBox ID="txtName" Width="90" ClientIDMode="Static" runat="server"></asp:TextBox>
            手机号：<asp:TextBox ID="txtMobile" ClientIDMode="Static" Width="80" runat="server"></asp:TextBox>
            <a href="javascript:;" class="easyui-linkbutton" id="btnQuery" iconcls="icon-search">查询</a>
            <a href="javascript:;" class="easyui-linkbutton" id="btnOk" iconcls="icon-ok">确定选择</a>
        </div>
        <table id="tblData"></table>
    </form>
</body>
</html>
