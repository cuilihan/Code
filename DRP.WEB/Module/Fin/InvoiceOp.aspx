<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvoiceOp.aspx.cs" Inherits="DRP.WEB.Module.Fin.InvoiceOp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>发票作废或退回开票申请</title>
    <script type="text/javascript">
        $(function () {
            $("#btnSave").click(function () {
                var c = $("#AuditRemark").val();
                if (c == "") {
                    Alert("请填写原因");
                    return false;
                }
                else
                    return true;
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel ID="pnlContainer" runat="server">
            <table class="tblInfo">
                <tr>
                    <td class="rowlabel">发票抬头：
                    </td>
                    <td>
                        <asp:Literal ID="InvoiceName" runat="server"></asp:Literal>
                    </td>
                    <td class="rowlabel">发票金额：
                    </td>
                    <td>
                        <asp:Literal ID="InvoiceAmt" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">发票内容：
                    </td>
                    <td>
                        <asp:Literal ID="InvoiceItem" runat="server"></asp:Literal>
                    </td>
                    <td class="rowlabel">领取方式：
                    </td>
                    <td>
                        <asp:Literal ID="FetchType" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">开票单位：
                    </td>
                    <td colspan="3">
                        <asp:Literal ID="InvoiceUnit" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">备注：
                    </td>
                    <td colspan="3">
                        <asp:Literal ID="OrderName" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">
                       <span class="red">*</span> <asp:Literal ID="lblAction" runat="server"></asp:Literal>：
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="AuditRemark" ClientIDMode="Static" data-options="required:true" EnableTheming="false" CssClass="textbox" Width="90%" Height="60" TextMode="MultiLine" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <div style="text-align: center; margin-top: 10px;">
                <asp:LinkButton ID="btnSave" ClientIDMode="Static" CssClass="easyui-linkbutton" OnClick="btnSave_Click" iconcls="icon-save" runat="server">保存</asp:LinkButton>
            </div>
        </asp:Panel>
    </form>
</body>
</html>
