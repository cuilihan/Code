<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsurerQry.aspx.cs" Inherits="DRP.WEB.Module.Res.InsurerQry" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>保险公司查询</title>
    <script src="Script/InsurerQry.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="tabs" class="easyui-tabs">
            <div title="保险公司查询" iconcls="icon-search">
                <div id="toolbar">
                    保险公司名称：<asp:TextBox ID="Name" ClientIDMode="Static" runat="server"></asp:TextBox>
                    <a id="btnQuery" class="easyui-linkbutton" iconcls="icon-search">查询</a> 
                </div>
                <table id="tblData"></table>
            </div>
        </div>
    </form>
</body>
</html>
