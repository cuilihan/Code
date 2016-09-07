<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SettlementBill.aspx.cs" Inherits="DRP.WEB.Module.Fin.SettlementBill" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>查看导游报账结算单</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel ID="pnlContainer" runat="server">
            <table class="tblEdit">
                <tr>
                    <td class="rowlabel">导游姓名：</td>
                    <td>
                        <asp:Literal ID="GuideName" runat="server"></asp:Literal></td>
                    <td class="rowlabel">导游电话：</td>
                    <td>
                        <asp:Literal ID="GuideMobile" runat="server"></asp:Literal></td>
                </tr>
                <asp:Panel ID="pnlBank" runat="server" Visible="false">
                    <tr>
                        <td class="rowlabel">开户行名：</td>
                        <td>
                            <asp:Literal ID="BankName" runat="server"></asp:Literal></td>
                        <td class="rowlabel">银行账号：</td>
                        <td>
                            <asp:Literal ID="BankAcct" runat="server"></asp:Literal></td>
                    </tr>
                </asp:Panel>
                <tr>
                    <td class="rowlabel">预领团款：</td>
                    <td>
                        <asp:Literal ID="DrawMoneyAmt" runat="server"></asp:Literal></td>
                    <td class="rowlabel">报账现收：</td>
                    <td>
                        <asp:Literal ID="BalanceIncome" runat="server"></asp:Literal></td>
                </tr>

                <tr>
                    <td class="rowlabel">报账成本：</td>
                    <td colspan="3">
                        <asp:Literal ID="BalanceCost" runat="server"></asp:Literal></td>

                </tr>
                <tr>
                    <td class="rowlabel">结算金额：</td>
                    <td style="color: red;">
                        <asp:Literal ID="SettlementAmt" runat="server"></asp:Literal></td>
                    <td class="rowlabel">结算类型：</td>
                    <td>
                        <asp:Literal ID="SettlementType" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td class="rowlabel">备注：</td>
                    <td colspan="3">
                        <asp:Literal ID="Comment" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td class="rowlabel">报账确认人：</td>
                    <td>
                        <asp:Literal ID="CreateUserName" runat="server"></asp:Literal></td>
                    <td class="rowlabel">报账确认日期：</td>
                    <td>
                        <asp:Literal ID="CreateDate" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td class="rowlabel">结算状态：</td>
                    <td style="color: red; font-weight:bold;" colspan="3">
                        <asp:Literal ID="DataStatus" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td class="rowlabel">结算人：</td>
                    <td>
                        <asp:Literal ID="Auditor" runat="server"></asp:Literal></td>
                    <td class="rowlabel">结算日期：</td>
                    <td>
                        <asp:Literal ID="AuditDate" runat="server"></asp:Literal></td>
                </tr>
            </table>
        </asp:Panel>
    </form>
</body>
</html>
