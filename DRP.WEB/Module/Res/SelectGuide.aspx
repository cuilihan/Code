<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectGuide.aspx.cs" Inherits="DRP.WEB.Module.Res.SelectGuide" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>选择导游</title>
    <script src="Script/GuideSelected.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="toolbar">
            导游名称：<asp:TextBox ID="GuideName" ClientIDMode="Static" runat="server" Width="90"></asp:TextBox>
            地区：<asp:DropDownList ID="Departure" AppendDataBoundItems="true" ClientIDMode="Static" runat="server">
                <asp:ListItem Text="所有" Value=""></asp:ListItem>
            </asp:DropDownList>
            <a href="javascript:;" class="easyui-linkbutton" id="btnQuery" iconcls="icon-search">查询</a>
            <a href="javascript:;" class="easyui-linkbutton" id="btnOk" iconcls="icon-ok">确定选择</a>
        </div>
        <table id="tblData"></table>
    </form>
</body>
</html>
