<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Insurer.aspx.cs" Inherits="DRP.WEB.Module.Res.Insurer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>保险公司管理</title>
    <script src="Script/Insurer.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="tabs" class="easyui-tabs">
            <div title="保险公司管理" iconcls="icon-search">
                <div id="toolbar">
                    保险公司名称：<asp:TextBox ID="Name" ClientIDMode="Static" runat="server"></asp:TextBox>
                    <a id="btnQuery" class="easyui-linkbutton" iconcls="icon-search">查询</a>
                    <a id="btnAdd" class="easyui-linkbutton" iconcls="icon-add">新增保险公司</a>
                    <a href="javascript:;" class="easyui-linkbutton" iconcls="icon-remove" id="btnDelete">删除</a>
                </div>
                <table id="tblData"></table>
            </div>
        </div>
    </form>
</body>
</html>
