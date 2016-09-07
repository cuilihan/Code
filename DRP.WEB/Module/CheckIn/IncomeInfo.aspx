<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IncomeInfo.aspx.cs" Inherits="DRP.WEB.Module.CheckIn.IncomeInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel runat="server" ID="pnlContainer">
            <table class="tblEdit">

                <tr>
                    <td class="rowlabel">收入金额：
                    </td>
                    <td>
                        <asp:Literal ID="IncomeAmt" runat="server"></asp:Literal>
                    </td>

                    <td class="rowlabel">收入日期：
                    </td>
                    <td>
                        <asp:Literal ID="IncomeDate" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">收入类型：
                    </td>
                    <td>
                        <asp:Literal ID="IncomeType" runat="server"></asp:Literal>
                    </td>
                    <td class="rowlabel">收入部门：
                    </td>
                    <td>
                        <asp:Literal ID="DeptName" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">收入方式：
                    </td>
                    <td>
                        <asp:Literal ID="IncomeMethod" runat="server"></asp:Literal>

                    </td>
                    <td class="rowlabel">经办人：</td>
                    <td>
                        <asp:Literal ID="Operator" runat="server"></asp:Literal>
                    </td>
                </tr>

                <tr>
                    <td class="rowlabel">收入来源：
                    </td>
                    <td colspan="3">
                        <asp:Literal ID="IncomeSource" runat="server"></asp:Literal>
                    </td>
                </tr>

                <tr>
                    <td class="rowlabel">备注：
                    </td>
                    <td colspan="3">
                        <asp:Literal ID="Comment" runat="server"></asp:Literal>
                    </td>
                </tr>

            </table>
        </asp:Panel>
    </form>
</body>
</html>
