<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CollectItemEdit.aspx.cs" Inherits="DRP.WEB.Module.Fin.CollectItemEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>新增收款流水项目</title>
    <script src="../../Scripts/Plugin/date_time/WdatePicker.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel ID="pnlContainer" runat="server">
            <table class="tblEdit">
                <tr>
                    <td class="rowlabel"><span class="red">*</span>交易日期：
                    </td>
                    <td>
                        <asp:TextBox ID="TradeDate" data-options="required:true" ClientIDMode="Static" runat="server" onclick="WdatePicker()"></asp:TextBox>
                    </td>
                    <td class="rowlabel">交易时间：
                    </td>
                    <td>
                        <asp:TextBox ID="TradeTime" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel"><span class="red">*</span>收入金额：
                    </td>
                    <td>
                        <asp:TextBox ID="IncomeAmt" data-options="required:true,validType:'float'" ClientIDMode="Static" runat="server"></asp:TextBox>
                    </td>
                    <td class="rowlabel"><span class="red">*</span>收款银行：
                    </td>
                    <td>
                        <asp:TextBox ID="BankName" data-options="required:true" ClientIDMode="Static" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">摘要：
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="Summary" Width="90%" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">交易行名：
                    </td>
                    <td>
                        <asp:TextBox ID="FromBank" runat="server"></asp:TextBox>
                    </td>

                    <td class="rowlabel">对方户名：
                    </td>
                    <td>
                        <asp:TextBox ID="FromAcct" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: center;">
                        <asp:LinkButton ID="btnSave" OnClientClick="return $('#form1').form('validate');" CssClass="easyui-linkbutton" OnClick="btnSave_Click" iconcls="icon-save" runat="server">保存</asp:LinkButton>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </form>
</body>
</html>
