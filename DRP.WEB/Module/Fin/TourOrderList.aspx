<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TourOrderList.aspx.cs" Inherits="DRP.WEB.Module.Fin.TourOrderList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>自主班团订单</title>
    <script src="../../Scripts/Plugin/date_time/WdatePicker.js"></script>
    <script src="../Order/Script/OrderUtility.js"></script>
    <script src="Script/TourOrderList.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="tabs" class="easyui-tabs">
            <div title="自主班团订单管理" runat="server" id="orderTitle" iconcls="icon-reload">
                <div id="toolbar">
                    <div style="margin-bottom: 5px;">
                        订单名称：<asp:TextBox ID="txtOrderName" ClientIDMode="Static" Width="90" runat="server"></asp:TextBox>
                        <span style="padding-left: 1em;">编号：</span>
                        <asp:TextBox ID="txtOrderNo" Width="90" ClientIDMode="Static" runat="server"></asp:TextBox>
                        <span style="padding-left: 1em;">出团日期：</span>
                        <asp:TextBox ID="sDate" Width="80" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                        ~
                        <asp:TextBox ID="eDate" Width="80" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                        目的地：
                        <input class="easyui-combotree" id="RouteTypeID" style="height: 26px; width: 90px;" runat="server" />
                        <input class="easyui-combotree" id="DestinationID" style="height: 26px; width: 120px;" runat="server" />
                        <span style="padding-left: 1em;">最后操作人：</span>
                        <asp:TextBox ID="txtUpdateUserName" Width="90" ClientIDMode="Static" runat="server"></asp:TextBox>
                        <a href="javascript:;" id="btnQuery" class="easyui-linkbutton" iconcls="icon-search">查询</a>
                        <asp:LinkButton ID="btnExport" CssClass="easyui-linkbutton" OnClick="btnExport_Click" iconcls="icon-export" runat="server">导出</asp:LinkButton> 
                    </div>
                </div>
                <table id="tblData">
                </table>
            </div>
        </div>
    </form>
</body>
</html>
