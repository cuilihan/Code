<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderCollected.aspx.cs" Inherits="DRP.WEB.Module.Fin.OrderCollected" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>订单收款明细</title>
    <script src="Script/OrderCollected.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
       <%-- <div id="toolbar">
            <a class="easyui-linkbutton" id="btnSave" iconcls="icon-ok">收款批量确认</a>
        </div>--%>
        <table id="tblData">
        </table>
    </form>
</body>
</html>
