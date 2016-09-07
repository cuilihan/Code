<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DrawMoney.aspx.cs" Inherits="DRP.WEB.Module.Fin.DrawMoney" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>导游领款</title>
    <script src="Script/DrawMoney.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="tabs" class="easyui-tabs">
            <div title="导游领款管理" runat="server" id="orderTitle" iconcls="icon-reload">
                <div id="toolbar">
                    订单名称：<asp:TextBox ID="txtOrderName" ClientIDMode="Static" Width="90" runat="server"></asp:TextBox>
                    <span style="padding-left: 1em;">编号：</span>
                    <asp:TextBox ID="txtOrderNo" Width="90" ClientIDMode="Static" runat="server"></asp:TextBox>
                    <span style="padding-left: 1em;">出团日期：</span>
                    <asp:TextBox ID="sDate" Width="80" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                    ~
                        <asp:TextBox ID="eDate" Width="80" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                    <span style="padding-left: 1em;">状态：</span>
                    <asp:DropDownList ID="ddlDataStatus" ClientIDMode="Static" runat="server">
                        <asp:ListItem Text="所有" Value=""></asp:ListItem>
                        <asp:ListItem Text="未领款" Value="1"></asp:ListItem>
                        <asp:ListItem Text="已领款" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                    <a href="javascript:;" id="btnQuery" class="easyui-linkbutton" iconcls="icon-search">查询</a>
                    <a class="easyui-linkbutton" id="btnSetStatus" iconcls="icon-add">标为已领取</a>

                </div>
                <table id="tblData">
                </table>
            </div>
        </div>
    </form>
</body>
</html>
