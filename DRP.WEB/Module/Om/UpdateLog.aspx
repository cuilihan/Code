<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateLog.aspx.cs" Inherits="DRP.WEB.Module.Om.UpdateLog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>系统更新日志</title>
    <script src="Script/UpdateLog.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="toolbar">
            <a href="javascript:;" id="btnAdd" class="easyui-linkbutton" iconcls="icon-add">新增</a>
        </div>
        <table id="tblData"></table>
    </form>
</body>
</html>
