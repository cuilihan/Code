<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RouteSearch.aspx.cs" Inherits="DRP.WEB.Module.Search.RouteSearch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>报价模板查询</title>
    <link href="Search.css" rel="stylesheet" />
    <script src="Script/RouteSearch.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="tabs" class="easyui-tabs">
            <div title="报价单查询" iconcls="icon-search">
                <div id="toolbar">
                    <div class="searcher_category">
                        客户类型：<a class="search_on" id="">全部</a><asp:Literal ID="lblItem" runat="server"></asp:Literal>
                    </div>
                    <div>
                        关键字：<asp:TextBox ID="Name" ClientIDMode="Static" runat="server"></asp:TextBox>
                        <a id="btnQuery" class="easyui-linkbutton" iconcls="icon-search">查询</a>
                    </div>
                </div>
                <table id="tblData"></table>
            </div>
        </div>
    </form>
</body>
</html>
