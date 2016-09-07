<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GuideQry.aspx.cs" Inherits="DRP.WEB.Module.Res.GuideQry" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>导游查询</title>
    <script src="Script/GuideQry.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="tabs" class="easyui-tabs">
            <div title="导游查询" iconcls="icon-search">
                <div id="toolbar">
                    地区：
                    <asp:DropDownList ID="DepartureID" AppendDataBoundItems="true" ClientIDMode="Static" runat="server">
                        <asp:ListItem Text="所有" Value=""></asp:ListItem>
                    </asp:DropDownList>
                    导游名称：<asp:TextBox ID="Name" ClientIDMode="Static" runat="server"></asp:TextBox>
                    <a id="btnQuery" class="easyui-linkbutton" iconcls="icon-search">查询</a>
                    <span style="position: absolute; right: 5px; display: inline-block; padding-top: 5px;"><a class="icon_grid" href="GuideTile.aspx">切换至简洁视图</a></span>
                </div>
                <table id="tblData"></table>
            </div>
        </div>
    </form>
</body>
</html>
