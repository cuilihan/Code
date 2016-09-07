<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderSearch.aspx.cs" Inherits="DRP.WEB.Module.Search.OrderSearch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>订单查询</title>
    <link href="Search.css" rel="stylesheet" />
    <script src="../../Scripts/Plugin/date_time/WdatePicker.js"></script>
    <script src="Script/OrdSearch.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="tabs" class="easyui-tabs">
            <div title="订单查询" iconcls="icon-search">
                <div id="toolbar">
                    <div class="searcher_category">
                        订单类型：<a class="search_on" itemtype="0">全部</a><a itemtype="1">同行散客</a><a itemtype="2">自主班散客</a><a itemtype="3">企业团</a><a itemtype="5">单项业务</a>
                    </div>
                    <div>
                        订单编号或名称：<asp:TextBox ID="Name" ClientIDMode="Static" runat="server"></asp:TextBox> 
                        出团日期：<asp:TextBox ID="TourDate" onclick="WdatePicker()" Width="90" ClientIDMode="Static" runat="server"></asp:TextBox>
                        <a id="btnQuery" class="easyui-linkbutton" iconcls="icon-search">查询</a>
                    </div>
                </div>
                <table id="tblData"></table>
            </div>
        </div>
    </form>
</body>
</html>
