<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderAuditAction.aspx.cs" Inherits="DRP.WEB.Module.Order.OrderAuditAction" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>订单确认</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding: 10px;">
            <table class="tblEdit">
                <tr>
                    <td class="rowlabel">订单名称：
                    </td>
                    <td>
                        <asp:Literal ID="OrderName" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">订单状态：
                    </td>
                    <td>
                        <asp:Literal ID="OrderStatus" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">确认备注：
                    </td>
                    <td>
                        <asp:TextBox ID="AuditComment" TextMode="MultiLine" EnableTheming="false" CssClass="textbox" ClientIDMode="Static" Width="96%" Height="60" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="padding: 5px; text-align: center;">
                        <asp:LinkButton ID="btnSave" OnClientClick="return confirm('确定要确认吗')" CssClass="easyui-linkbutton" iconcls="icon-ok" OnClick="btnSave_Click" runat="server">确认</asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
