<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Income.aspx.cs" Inherits="DRP.WEB.Module.Rpt.Income" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>订单收入明细</title>
    <script src="../../Scripts/Plugin/date_time/WdatePicker.js"></script>
    <script src="Script/RptIncome.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="toolbar">
            <div style="margin-bottom: 5px;">
                订单名称：<asp:TextBox ID="txtOrderName" ClientIDMode="Static" Width="90" runat="server"></asp:TextBox>
                <span style="padding-left: 2em;">订单编号：</span>
                <asp:TextBox ID="txtOrderNo" Width="90" ClientIDMode="Static" runat="server"></asp:TextBox>
                <span style="padding-left: 1em;">客户名称：</span><asp:TextBox ID="txtCustomer" Width="100" ClientIDMode="Static" runat="server"></asp:TextBox>

                <span style="padding-left: 1em;">日期：</span>
                <asp:DropDownList ID="ddlDateType" ClientIDMode="Static" Width="90" runat="server">
                    <asp:ListItem Text="收款日期" Value="3"></asp:ListItem>
                    <asp:ListItem Text="订单日期" Value="1"></asp:ListItem>
                    <asp:ListItem Text="创建日期" Value="2"></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="sDate" Width="80" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                ~<asp:TextBox ID="eDate" Width="80" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
            </div>
            <div>
                订单类型：<asp:DropDownList ID="ddlOrderType" Width="95" ClientIDMode="Static" AppendDataBoundItems="true" runat="server">
                    <asp:ListItem Text="所有" Value="0"></asp:ListItem>
                    <asp:ListItem Text="同行散客" Value="1"></asp:ListItem>
                    <asp:ListItem Text="自主班散客" Value="2"></asp:ListItem>
                    <asp:ListItem Text="企业团" Value="3"></asp:ListItem>
                    <asp:ListItem Text="单项业务" Value="5"></asp:ListItem>
                    <asp:ListItem Text="机票订单" Value="6"></asp:ListItem>
                </asp:DropDownList>
                <span style="padding-left: 1em;">供应商名称：</span>
                <asp:TextBox ID="txtSupplier" Width="90" ClientIDMode="Static" runat="server"></asp:TextBox>
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
