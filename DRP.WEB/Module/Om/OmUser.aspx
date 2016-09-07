<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OmUser.aspx.cs" Inherits="DRP.WEB.Module.Om.OmUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>运维用户管理</title>
    <script src="Script/OmUser.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="toolbar">
            用户名称或机构名称搜索：<input class="textbox" id="txtKey" style="width: 200px; height: 26px;" />

            <a href="javascript:;" id="btnQuery" class="easyui-linkbutton" iconcls="icon-search">查询</a> 
            <a href="javascript:;" id="btnAdd" class="easyui-linkbutton" iconcls="icon-add" onclick="t.fnEdit('')">新增运维用户</a>
        </div>
        <table id="tblData"></table>
    </form>
</body>
</html>
