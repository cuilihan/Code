<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResourceSearch.aspx.cs" Inherits="DRP.WEB.Module.Search.ResourceSearch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>资源查询</title>
    <link href="Search.css" rel="stylesheet" />
    <script src="Script/ResSearch.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="tabs" class="easyui-tabs">
            <div title="资源查询" iconcls="icon-search">
                <div id="toolbar">
                    <div class="searcher_category">
                        资源类型：<a class="search_on" itemtype="0">全部</a>
                        <a itemtype="1">供应商</a><a itemtype="2">景点门票</a><a itemtype="3">导游</a><a itemtype="4">酒店</a><a itemtype="5">车队</a><a itemtype="6">签证机构</a><a itemtype="7">保险公司</a><a itemtype="8">购物店</a><a itemtype="9">票务机构</a><a itemtype="10">其他资源</a>
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
