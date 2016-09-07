<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayInfo.aspx.cs" Inherits="DRP.WEB.Module.CheckIn.PayInfo" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>支出信息</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel runat="server" ID="pnlContainer">
            <table class="tblEdit">

                <tr>
                    <td class="rowlabel">支出金额：
                    </td>
                    <td>
                        <asp:Literal ID="PayAmt" runat="server"></asp:Literal>
                    </td>

                    <td class="rowlabel">支出日期：
                    </td>
                    <td>
                        <asp:Literal ID="PayDate" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">支出类型：
                    </td>
                    <td>
                        <asp:Literal ID="PayType" runat="server"></asp:Literal>
                    </td>
                    <td class="rowlabel">支出部门：
                    </td>
                    <td>
                        <asp:Literal ID="DeptName" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">支出方式：
                    </td>
                    <td>
                        <asp:Literal ID="PayMethod" runat="server"></asp:Literal>
                    </td>
                    <td class="rowlabel">经办人：</td>
                    <td>
                        <asp:Literal ID="Operator" runat="server"></asp:Literal>
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
