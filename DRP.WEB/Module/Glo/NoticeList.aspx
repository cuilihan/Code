<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoticeList.aspx.cs" Inherits="DRP.WEB.Module.Glo.NoticeList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>通知公告</title>
    <script src="Script/NoticeList.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="tabs" class="easyui-tabs">
            <div title="通知公告管理" iconcls="icon-search" style="padding: 5px 5px 0px 5px;">
                <div id="toolbar">
                    标题：<asp:TextBox ID="txtSubject" ClientIDMode="Static" runat="server"></asp:TextBox>
                    <a id="btnQuery" class="easyui-linkbutton" iconcls="icon-search">查询</a>
                </div>
                <table id="tblData"></table>
            </div>
        </div>
    </form>
</body>
</html>
