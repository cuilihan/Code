<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Departure.aspx.cs" Inherits="DRP.WEB.Module.Glo.Departure" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>出发地</title>
    <script src="Script/Departure.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="toolbar">
            <a href="javascript:;" id="btnAdd" class="easyui-linkbutton" iconcls="icon-add">新增出发地</a>
        </div>
        <table id="tblData"></table>
    </form>
</body>
</html>
