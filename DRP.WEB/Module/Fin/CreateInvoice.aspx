<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateInvoice.aspx.cs" Inherits="DRP.WEB.Module.Fin.CreateInvoice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>开票</title>
    <script src="../../Scripts/Plugin/date_time/WdatePicker.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel ID="pnlContainer" runat="server">
            <table class="tblInfo">
                <tr>
                    <td colspan="4" style="font-weight: bold;">一、 开票申请信息
                    </td>
                </tr>
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
                    <td colspan="4" style="font-weight: bold;">二、 财务开票信息
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel"><span class="red">*</span>发票编号：
                    </td>
                    <td>
                        <asp:TextBox ID="InvoiceNo" runat="server" required="true"></asp:TextBox>
                    </td>
                    <td class="rowlabel">开票日期：
                    </td>
                    <td>
                        <asp:TextBox ID="InvoiceDate" onclick="WdatePicker()" data-options="required:true" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">备注：
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="AuditRemark" EnableTheming="false" CssClass="textbox" Width="90%" Height="40" TextMode="MultiLine" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <div style="text-align: center; margin-top:15px;">
                <asp:LinkButton ID="btnSave" OnClientClick="return $('#form1').form('validate');" CssClass="easyui-linkbutton" OnClick="btnSave_Click" iconcls="icon-save" runat="server">保存</asp:LinkButton>
            </div>
        </asp:Panel>
    </form>
</body>
</html>
