<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QQ.aspx.cs" Inherits="DRP.WEB.Module.Om.QQ" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>在线客服管理</title>
    <script src="Script/QQ.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="toolbar">
            <a href="javascript:;" id="btnAdd" class="easyui-linkbutton" iconcls="icon-add">新增QQ</a>
            <a href="javascript:;" id="btnDelete" class="easyui-linkbutton" iconcls="icon-remove">删除</a>
        </div>
        <table id="tblData"></table>
    </form>
</body>
</html>
