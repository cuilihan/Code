<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TravelAgencyQry.aspx.cs" Inherits="DRP.WEB.Module.Res.TravelAgencyQry" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>综合供应商查询</title>
    <script src="Script/ResourceUtility.js" type="text/javascript"></script>
    <script src="Script/TravalAgencyQry.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="tabs" class="easyui-tabs">
            <div title="供应商查询" iconcls="icon-search">
                <div id="toolbar">
                    区域： 
                    <input class="easyui-combotree" id="RouteTypeID" style="width: 100px; height: 26px;" />
                    <input class="easyui-combotree" id="DestinationID" style="width: 150px; height: 26px;" />
                    供应商名称或简称：<asp:TextBox ID="Name" ClientIDMode="Static" runat="server"></asp:TextBox>
                    <a id="btnQuery" class="easyui-linkbutton" iconcls="icon-search">查询</a>
                    <span style="position: absolute; right: 5px; display: inline-block; padding-top: 5px;"><a class="icon_grid" href="TravelAgencyTile.aspx">切换至简洁视图</a></span>
                </div>
                <table id="tblData"></table>
            </div>
        </div>
    </form>
</body>
</html>
