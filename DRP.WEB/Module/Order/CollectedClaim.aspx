<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CollectedClaim.aspx.cs" Inherits="DRP.WEB.Module.Order.CollectedClaim" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>收款认领</title>
    <script src="../../Scripts/Plugin/date_time/WdatePicker.js" type="text/javascript"></script>
    <script src="Script/CollectedClaim.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="tabs" class="easyui-tabs">
            <div title="收款认领" runat="server" id="orderTitle" iconcls="icon-reload">
                <div id="toolbar">
                    <div>
                        交易日期范围：                       
                        <asp:TextBox ID="sDate" Width="80" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                        ~
                         <asp:TextBox ID="eDate" Width="80" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                        <span style="padding-left: 1em;">认领状态：</span>
                        <asp:DropDownList ID="DataStatus" Width="85" ClientIDMode="Static" AppendDataBoundItems="true"
                            runat="server"> 
                            <asp:ListItem Text="未认领" Value="1" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="已认领" Value="2"></asp:ListItem>
                            <asp:ListItem Text="已确认" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                        <span style="padding-left: 1em;">收款银行：</span>
                        <asp:TextBox ID="BankName" ClientIDMode="Static" Width="80" runat="server"></asp:TextBox>
                        <span style="padding-left: 1em;">交易行名：</span>
                        <asp:TextBox ID="FromBank" ClientIDMode="Static" Width="80" runat="server"></asp:TextBox>
                    </div>
                    <div style="padding: 5px 0px;">
                        收入金额范围： 
                        <asp:TextBox ID="MinIncome" Width="80" ClientIDMode="Static" runat="server"></asp:TextBox>
                        ~
                         <asp:TextBox ID="MaxIncome" Width="80" ClientIDMode="Static" runat="server"></asp:TextBox>
                        <span style="padding-left: 1em;">对方户名：</span>
                        <asp:TextBox ID="FromAcct" ClientIDMode="Static" Width="80" runat="server"></asp:TextBox>
                        <span style="padding-left: 1em;">
                            <a href="javascript:;" id="btnQuery" class="easyui-linkbutton" iconcls="icon-search">查询</a>
                        </span>
                    </div>
                </div>
                <table id="tblData">
                </table>
            </div>
        </div>
    </form>
</body>
</html>
