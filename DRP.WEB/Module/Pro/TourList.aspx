<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TourList.aspx.cs" Inherits="DRP.WEB.Module.Pro.TourList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>班次查询</title>
    <script src="Script/TourList.js" type="text/javascript"></script>
    <script src="../../Scripts/Plugin/date_time/WdatePicker.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="toolbar">
            班次日期范围：<asp:TextBox ID="sDate" onclick="WdatePicker()" Width="90" ClientIDMode="Static" runat="server"></asp:TextBox>
            ~
                    <asp:TextBox ID="eDate" Width="90" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
            <asp:CheckBox ID="IsExpire" ClientIDMode="Static" Text="显示已过期的班次" runat="server" /> 
            <a id="btnQuery" class="easyui-linkbutton" iconcls="icon-search">查询</a>

            <a id="btnBatchUpdate" class="easyui-linkbutton" iconcls="icon-add">批量修改班次</a>
            <a id="btnDelete" class="easyui-linkbutton" iconcls="icon-remove">删除</a>

        </div>
        <table id="tblData"></table>
    </form>
</body>
</html>
