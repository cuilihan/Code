<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaidItem.aspx.cs" Inherits="DRP.WEB.Module.Rpt.PaidItem" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>订单支出明细</title>
    <script src="../../Scripts/Plugin/date_time/WdatePicker.js"></script>
    <script src="Script/RptPaidItem.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="toolbar">
            <div style="margin-bottom: 5px;">
                订单名称：<asp:TextBox ID="txtOrderName" ClientIDMode="Static" Width="90" runat="server"></asp:TextBox>
                <span style="padding-left: 1em;">订单编号：</span>
                <asp:TextBox ID="txtOrderNo" Width="70" ClientIDMode="Static" runat="server"></asp:TextBox>
                <span style="padding-left: 1em;">供应商：</span>
                <asp:TextBox ID="txtSupplier" Width="80" ClientIDMode="Static" runat="server"></asp:TextBox>
                <span style="padding-left: 1em;">日期：</span>
                <asp:DropDownList ID="ddlDateType" ClientIDMode="Static" Width="90" runat="server">
                    <asp:ListItem Text="付款日期" Value="4"></asp:ListItem>
                    <asp:ListItem Text="订单日期" Value="1"></asp:ListItem>
                    <asp:ListItem Text="创建日期" Value="2"></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="sDate" Width="70" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                ~
                <asp:TextBox ID="eDate" Width="70" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                <span style="padding-left: 1em;">下单部门：</span>
                <asp:DropDownList ID="ddlDept" ClientIDMode="Static" AppendDataBoundItems="true" runat="server">
                    <asp:ListItem Text="所有" Value=""></asp:ListItem>
                </asp:DropDownList>
                <span style="padding-left: 1em;">
                    <a href="javascript:;" id="btnQuery" class="easyui-linkbutton" iconcls="icon-search">查询</a>
                </span>
            </div>
        </div>
        <table id="tblData">
        </table>
    </form>
</body>
</html>
