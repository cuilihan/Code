<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SettlementAction.aspx.cs" Inherits="DRP.WEB.Module.Fin.SettlementAction" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>导游报账结算</title>
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
            </table>
        </asp:Panel>
        <div style="text-align:center; margin-top:15px;">
            <asp:LinkButton ID="btnSave" OnClientClick="return confirm('确定要结算吗')" OnClick="btnSave_Click" runat="server" CssClass="easyui-linkbutton" iconcls="icon-ok">确认已结算</asp:LinkButton>
        </div>
    </form>
</body>
</html>
