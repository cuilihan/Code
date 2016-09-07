<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderAudit.aspx.cs" Inherits="DRP.WEB.Module.Order.OrderAudit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>自主班散客订单审核</title>
    <script src="../../Scripts/Plugin/date_time/WdatePicker.js"></script>
    <script src="Script/OrderAudit.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="tabs" class="easyui-tabs">
            <div title="订单审核" runat="server" id="orderTitle" iconcls="icon-reload">
                <div id="toolbar">
                    <div style="margin-bottom: 5px;">
                        订单名称：<asp:TextBox ID="txtOrderName" ClientIDMode="Static" Width="90" runat="server"></asp:TextBox>
                        <span style="padding-left: 1em;">编号：</span>
                        <asp:TextBox ID="txtOrderNo" Width="90" ClientIDMode="Static" runat="server"></asp:TextBox>
                        <span style="padding-left: 1em;">客户名称：</span><asp:TextBox ID="txtCustomer" Width="90" ClientIDMode="Static" runat="server"></asp:TextBox>
                        <span style="padding-left: 1em;">供应商名称：</span>
                        <asp:TextBox ID="txtSupplier" Width="90" ClientIDMode="Static" runat="server"></asp:TextBox>
                        <span style="padding-left: 1em;">日期：</span>
                        <asp:DropDownList ID="ddlDateType" Width="90" runat="server">
                            <asp:ListItem Text="录入日期" Value="1"></asp:ListItem>
                            <asp:ListItem Text="出发日期" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="sDate" Width="80" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                        ~
                        <asp:TextBox ID="eDate" Width="80" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                    </div> 
                    <div style="height: 25px; line-height: 25px; margin-top: 5px;">
                        <a href="javascript:;" id="btnQuery" class="easyui-linkbutton" iconcls="icon-search">查询</a> 
                    </div>
                </div>
                <table id="tblData">
                </table>
            </div>
        </div>
    </form>
</body>
</html>
