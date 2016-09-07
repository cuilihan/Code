<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="DRP.WEB.Module.Sys.User" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>用户管理</title>
    <script src="Script/User.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="toolbar">
            用户姓名或部门名称：<input  id="txtKey" class="textbox" style="height:26px;" type="text" />
            <a href="javascript:;" id="btnQuery" class="easyui-linkbutton" iconcls="icon-search">查询</a>
            <a href="javascript:;" id="btnAdd" class="easyui-linkbutton" iconcls="icon-add">新增用户</a>
        </div>
        <table id="tblData"></table>
    </form>
</body>
</html>
