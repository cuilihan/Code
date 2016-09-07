<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderCheckAcctBill.aspx.cs" Inherits="DRP.WEB.Module.Order.OrderCheckAcctBill" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>导游报账单余款确认</title>
    <script type="text/javascript">

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding: 10px;">
            <table class="tblEdit">
                <tr>
                    <td colspan="4" style="font-weight: bold;">一、报账信息</td>
                </tr>
                <tr>
                    <td class="rowlabel">导游名称：
                    </td>
                    <td>
                        <asp:Literal ID="lblGuide" runat="server"></asp:Literal>
                    </td>
                    <td class="rowlabel">导游电话：
                    </td>
                    <td style="width: 180px;">
                        <asp:Literal ID="lblMobile" Text="13606216450" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">预领团款：
                    </td>
                    <td>
                        <asp:Literal ID="lblDrawMoney" runat="server"></asp:Literal>
                    </td>
                    <td class="rowlabel">报账收入：
                    </td>
                    <td colspan="3">
                        <asp:Literal ID="lblCheckAmt" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">报账支出：
                    </td>
                    <td colspan="3">
                        <asp:Literal ID="lblCostAmt" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="font-weight: bold;">二、导游结算信息</td>
                </tr>
                <tr>
                    <td class="rowlabel">结算类型：                       
                    </td>
                    <td>
                        <asp:Literal ID="lblBalanceType" runat="server"></asp:Literal>
                    </td>
                    <td class="rowlabel">
                        <asp:Literal ID="lblAmtText" runat="server"></asp:Literal>
                    </td>
                    <td>
                        <asp:Literal ID="lblBalanceAmt" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tbody id="tblData" runat="server" visible="false">
                    <tr>
                        <td class="rowlabel">导游开户行：
                        </td>
                        <td>
                            <asp:TextBox ID="txtBankName" runat="server"></asp:TextBox>
                        </td>
                        <td class="rowlabel">导游账号：
                        </td>
                        <td>
                            <asp:TextBox ID="txtBankAcct" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </tbody>
                <tr>
                    <td class="rowlabel">备注：
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtComment" TextMode="MultiLine" ClientIDMode="Static" EnableTheming="false" CssClass="textbox" Width="90%" Height="60" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: center; padding: 4px;">
                        <asp:LinkButton ID="btnSave" CssClass="easyui-linkbutton" OnClick="btnSave_Click" iconcls="icon-save" OnClientClick="return confirm('确定要确认吗')" runat="server">确认导游报账单</asp:LinkButton> 
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
