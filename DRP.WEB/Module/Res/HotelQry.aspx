<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HotelQry.aspx.cs" Inherits="DRP.WEB.Module.Res.HotelQry" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>酒店查询</title>
    <script src="Script/ResourceUtility.js" type="text/javascript"></script>
    <script src="Script/HotelQry.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="tabs" class="easyui-tabs">
            <div title="酒店查询" iconcls="icon-search">
                <div id="toolbar">
                    区域： 
                    <input class="easyui-combotree" id="RouteTypeID" style="width: 100px; height: 26px;" />
                    <input class="easyui-combotree" id="DestinationID" style="width: 150px; height: 26px;" />
                    酒店名称：<asp:TextBox ID="Name" ClientIDMode="Static" runat="server"></asp:TextBox>
                    <a id="btnQuery" class="easyui-linkbutton" iconcls="icon-search">查询</a> 
                      <span style="position: absolute; right: 5px; display: inline-block;padding-top:5px; "><a class="icon_grid" href="HotelTile.aspx">切换至简洁视图</a></span>
                </div>
                <table id="tblData"></table>
            </div>
        </div>
    </form>
</body>
</html>
