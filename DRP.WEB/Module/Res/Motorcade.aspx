<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Motorcade.aspx.cs" Inherits="DRP.WEB.Module.Res.Motorcade" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>车队管理</title> 
    <script src="Script/Motorcade.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="tabs" class="easyui-tabs">
            <div title="车队管理" iconcls="icon-search" >
                <div id="toolbar">
                    地区：<asp:DropDownList ID="DepartureID" ClientIDMode="Static" AppendDataBoundItems="true" runat="server">
                        <asp:ListItem Text="所有" Value=""></asp:ListItem>
                       </asp:DropDownList>  
                    车队名称：<asp:TextBox ID="Name" ClientIDMode="Static" runat="server"></asp:TextBox>
                    <a id="btnQuery" class="easyui-linkbutton" iconcls="icon-search">查询</a> 
                    <a id="btnAdd" class="easyui-linkbutton" iconcls="icon-add">新增车队</a>
                    <a href="javascript:;" class="easyui-linkbutton" iconcls="icon-remove" id="btnDelete">删除</a>
                </div>
                <table id="tblData"></table>
            </div>
        </div>
    </form>
</body>
</html>
