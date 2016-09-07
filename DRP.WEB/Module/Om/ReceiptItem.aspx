<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReceiptItem.aspx.cs" Inherits="DRP.WEB.Module.Om.ReceiptItem" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>收款明细</title>
    <script src="Script/ReceiptItem.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="toolbar">
            机构名称：<asp:Literal ID="OrgName" runat="server"></asp:Literal>
            <span style="padding-left: 40px;"></span>
            收款总额：<asp:Literal ID="ReceiptAmt" runat="server"></asp:Literal>

            <span style="width: 30px; display: inline-block;"></span>
            <a href="javascript:;" id="btnAdd" class="easyui-linkbutton">收款</a>
        </div>
        <table id="tblData"></table>
    </form>
</body>
</html>
