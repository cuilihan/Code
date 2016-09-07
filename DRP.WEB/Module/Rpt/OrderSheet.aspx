<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderSheet.aspx.cs" Inherits="DRP.WEB.Module.Rpt.OrderSheet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>订单收支明细表</title>
    <script src="../../Scripts/Plugin/date_time/WdatePicker.js"></script>
    <script src="Script/OrderSheet.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="toolbar">
            <div style="margin-bottom: 5px;">
                订单名称：<asp:TextBox ID="txtOrderName" ClientIDMode="Static" Width="90" runat="server"></asp:TextBox>
                <span style="padding-left: 2em;">订单编号：</span>
                <asp:TextBox ID="txtOrderNo" Width="90" ClientIDMode="Static" runat="server"></asp:TextBox>
                <span style="padding-left: 1em;">客户名称：</span><asp:TextBox ID="txtCustomer" Width="100" ClientIDMode="Static" runat="server"></asp:TextBox>

                <span style="padding-left: 1em;">订单日期：</span> 
                <asp:TextBox ID="sDate" Width="80" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                ~<asp:TextBox ID="eDate" Width="80" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                <span style="padding-left: 1em;">订单创建日期：</span> 
                <asp:TextBox ID="sCreateDate" Width="80" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                ~<asp:TextBox ID="eCreateDate" Width="80" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
            </div>
            <div>
                订单类型：<asp:DropDownList ID="ddlOrderType" Width="95" ClientIDMode="Static" AppendDataBoundItems="true" runat="server">
                    <asp:ListItem Text="所有" Value="0"></asp:ListItem>
                    <asp:ListItem Text="同行散客" Value="1"></asp:ListItem>
                    <asp:ListItem Text="自主班散客" Value="2"></asp:ListItem>
                    <asp:ListItem Text="企业团" Value="3"></asp:ListItem>
                    <asp:ListItem Text="自主班团" Value="4"></asp:ListItem>
                    <asp:ListItem Text="单项业务" Value="5"></asp:ListItem>
                    <asp:ListItem Text="机票订单" Value="6"></asp:ListItem>
                </asp:DropDownList>
                <span style="padding-left: 1em;">供应商名称：</span>
                <asp:TextBox ID="txtSupplier" Width="90" ClientIDMode="Static" runat="server"></asp:TextBox>
                <span style="padding-left: 1em;">提交人：</span>
                <asp:TextBox ID="txtCreateUserName" Width="90" ClientIDMode="Static" runat="server"></asp:TextBox>
                <span style="padding-left: 1em;">参与人：</span>
                <asp:TextBox ID="txtParticipant" Width="90" ClientIDMode="Static" runat="server"></asp:TextBox>
                <span style="padding-left: 1em;">
                    <a href="javascript:;" id="btnQuery" class="easyui-linkbutton" iconcls="icon-search">查询</a>
                    <asp:LinkButton ID="btnExport" CssClass="easyui-linkbutton" OnClick="btnExport_Click" iconcls="icon-export" runat="server">导出</asp:LinkButton> 
                    <asp:CheckBox runat="server" ID="CanceledOrder" ClientIDMode="Static" Text="已取消的订单" />
                    <span style="padding-left:20px;">注：当天订单不计算统计，延迟一天</span>
                </span>
            </div>
        </div>
        <table id="tblData">
        </table>
    </form>
</body>
</html>
