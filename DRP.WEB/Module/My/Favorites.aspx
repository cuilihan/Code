<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Favorites.aspx.cs" Inherits="DRP.WEB.Module.My.Favorites" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>个人链接中心</title>
    <script src="Script/Favorites.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="toolbar">
            <a href="javascript:;" id="btnAdd" class="easyui-linkbutton" iconcls="icon-add">新增链接</a>
            <a href="/Module/Guide/Index.aspx" class="easyui-linkbutton">返回首页</a>
        </div>
        <table id="tblData"></table>
    </form>
</body>
</html>
