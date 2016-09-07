<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrgInfo.aspx.cs" Inherits="DRP.WEB.Module.Om.OrgInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>机构管理</title>
    <script src="Script/OrgInfo.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="tabs" class="easyui-tabs">
            <div title="机构管理" runat="server" id="orderTitle" iconcls="icon-reload">
                <div id="toolbar">
                    <input class="textbox" id="txtKey" style="width: 200px; height: 26px; line-height: 26px;" runat="server" />
                    <a href="javascript:;" id="btnQuery" class="easyui-linkbutton" iconcls="icon-search">查询</a>
                    <a href="javascript:;" id="btnAdd" class="easyui-linkbutton" iconcls="icon-add">新增机构信息</a>
                    <asp:LinkButton ID="btnExport" CssClass="easyui-linkbutton" OnClick="btnExport_Click" OnClientClick="return o.fnSetExport()" iconcls="icon-export" runat="server">导出</asp:LinkButton>
                </div>
                <table id="tblData"></table>
            </div>
        </div>
    </form>
</body>
</html>
