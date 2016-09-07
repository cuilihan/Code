<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Venue.aspx.cs" Inherits="DRP.WEB.Module.Pro.Venue" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>上车地点设置</title>
    <script src="Script/Venue.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="toolbar">
             线路类型： 
            <asp:DropDownList ID="RouteTypeID" AppendDataBoundItems="true" ClientIDMode="Static" runat="server">
                <asp:ListItem Text="所有" Value=""></asp:ListItem>
            </asp:DropDownList>
            <a href="javascript:;" id="btnAdd" class="easyui-linkbutton" iconcls="icon-add">新增集合地点</a>
            <a href="javascript:;" id="btnDelete" class="easyui-linkbutton" iconcls="icon-remove">删除</a>
        </div>
        <table id="tblData"></table>
    </form>
</body>
</html>
