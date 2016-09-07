<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shopping.aspx.cs" Inherits="DRP.WEB.Module.Res.Shopping" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>购物店管理</title>
    <script src="Script/ResourceUtility.js" type="text/javascript"></script>
    <script src="Script/Shopping.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="tabs" class="easyui-tabs">
            <div title="购物店管理" iconcls="icon-search">
                <div id="toolbar">
                    区域： 
                    <input class="easyui-combotree" id="RouteTypeID" style="width: 100px; height: 26px;" />
                    <input class="easyui-combotree" id="DestinationID" style="width: 150px; height: 26px;" />
                    购物店名称：<asp:TextBox ID="Name" ClientIDMode="Static" runat="server"></asp:TextBox>
                    <a id="btnQuery" class="easyui-linkbutton" iconcls="icon-search">查询</a>
                    <a id="btnAdd" class="easyui-linkbutton" iconcls="icon-add">新增购物店</a>
                    <a href="javascript:;" class="easyui-linkbutton" iconcls="icon-remove" id="btnDelete">删除</a>
                </div>
                <table id="tblData"></table>
            </div>
        </div>
    </form>
</body>
</html>
