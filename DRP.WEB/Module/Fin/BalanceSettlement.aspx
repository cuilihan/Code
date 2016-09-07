<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BalanceSettlement.aspx.cs" Inherits="DRP.WEB.Module.Om.BalanceSettlement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>导游报账结算管理</title>
    <script src="Script/BalanceSettlement.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="tabs" class="easyui-tabs">
            <div title="导游报账结算" runat="server" id="orderTitle" iconcls="icon-reload">
                <div id="toolbar">
                    <div style="margin-bottom: 5px;">
                        订单名称：<asp:TextBox ID="txtOrderName" ClientIDMode="Static" Width="90" runat="server"></asp:TextBox>
                        <span style="padding-left: 1em;">订单编号：</span>
                        <asp:TextBox ID="txtOrderNo" Width="90" ClientIDMode="Static" runat="server"></asp:TextBox>
                        <span style="padding-left: 1em;">导游姓名：</span><asp:TextBox ID="txtGuideName" Width="90" ClientIDMode="Static" runat="server"></asp:TextBox>
                        <span style="padding-left: 1em;">导游手机号：</span><asp:TextBox ID="txtGuideMobile" Width="90" ClientIDMode="Static" runat="server"></asp:TextBox>
                        结算状态：
                        <asp:DropDownList ID="ddlDataStatus" Width="80" ClientIDMode="Static" AppendDataBoundItems="true" runat="server">
                            <asp:ListItem Text="所有" Value="0"></asp:ListItem>
                            <asp:ListItem Text="未结算" Value="1"></asp:ListItem>
                            <asp:ListItem Text="已结算" Value="2"></asp:ListItem>
                        </asp:DropDownList>
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
