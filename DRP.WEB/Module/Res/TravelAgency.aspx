<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TravelAgency.aspx.cs" Inherits="DRP.WEB.Module.Res.TravelAgency" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>综合供应商管理</title>
    <script src="Script/ResourceUtility.js" type="text/javascript"></script>
    <script src="Script/TravalAgency.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="tabs" class="easyui-tabs">
            <div title="综合供应商管理" iconcls="icon-search">
                <div id="toolbar">
                    区域： 
                    <input class="easyui-combotree" id="RouteTypeID" style="width: 100px; height:26px;" />
                    <input class="easyui-combotree" id="DestinationID" style="width: 150px; height: 26px;" />
                    综合供应商名称或简称：<asp:TextBox ID="Name" ClientIDMode="Static" runat="server"></asp:TextBox>
                    <a id="btnQuery" class="easyui-linkbutton" iconcls="icon-search">查询</a>

                    <a id="btnAdd" class="easyui-linkbutton" iconcls="icon-add">新增供应商</a>
                    <a href="javascript:;" class="easyui-linkbutton" iconcls="icon-remove" id="btnDelete">删除</a>
                </div>
                <table id="tblData"></table>
            </div>
        </div>
    </form>
</body>
</html>
