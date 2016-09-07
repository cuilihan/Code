<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoticeMrg.aspx.cs" Inherits="DRP.WEB.Module.Glo.NoticeMrg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>通知公告管理</title>
    <script src="Script/NoticeMrg.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="tabs" class="easyui-tabs">
            <div title="通知公告管理" iconcls="icon-search" style="padding: 5px 5px 0px 5px;">
                <div id="toolbar">
                    标题：<asp:TextBox ID="txtSubject" ClientIDMode="Static" runat="server"></asp:TextBox>
                    <a id="btnQuery" class="easyui-linkbutton" iconcls="icon-search">查询</a>
                    <a id="btnAdd" class="easyui-linkbutton" iconcls="icon-add">发布通知公告</a>
                    <a href="javascript:;" class="easyui-linkbutton" iconcls="icon-remove" id="btnDelete">删除</a>
                </div>
                <table id="tblData"></table>
            </div>
        </div>
    </form>
</body>
</html>
