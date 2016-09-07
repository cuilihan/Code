<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RouteMrg.aspx.cs" Inherits="DRP.WEB.Module.Pro.RouteMrg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>线路管理</title>
    <script src="Script/RouteMgmt.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="tabs" class="easyui-tabs">
            <div title="线路管理" iconcls="icon-search">
                <div id="toolbar">
                    线路编号：<asp:TextBox ID="RouteNo" ClientIDMode="Static" runat="server"></asp:TextBox>
                    线路名称：<asp:TextBox ID="RouteName" ClientIDMode="Static" runat="server"></asp:TextBox>
                    线路类型： 
                    <asp:DropDownList ID="RouteType" AppendDataBoundItems="true" runat="server">
                        <asp:ListItem Text="所有" Value=""></asp:ListItem>
                    </asp:DropDownList>
                    <a id="btnQuery" class="easyui-linkbutton" iconcls="icon-search">查询</a> 
                    <span style="position: absolute; right: 5px;">
                        <a id="btnAdd" class="easyui-linkbutton" iconcls="icon-add">新增线路</a>
                    </span>
                </div>
                <table id="tblData"></table>
            </div>
        </div>
    </form>
</body>
</html>
