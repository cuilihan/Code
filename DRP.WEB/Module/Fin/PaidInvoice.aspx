<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaidInvoice.aspx.cs" Inherits="DRP.WEB.Module.Fin.PaidInvoice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>供应商发票登记</title>
    <script src="Script/PaidInvoice.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="toolbar">
            <a class="easyui-linkbutton" id="btnAdd" iconcls="icon-add">供应商发票登记</a>
            <a class="easyui-linkbutton" id="btnDelete" iconcls="icon-remove">删除</a>
        </div>
        <table id="tblData"></table>
    </form>
</body>
</html>
