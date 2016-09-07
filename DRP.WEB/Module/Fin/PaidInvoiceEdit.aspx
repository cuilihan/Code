<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaidInvoiceEdit.aspx.cs" Inherits="DRP.WEB.Module.Fin.PaidInvoiceEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>付款发票登记</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel runat="server" ID="pnlContainer">
            <table class="tblEdit"> 
                <tr>
                    <td class="rowlabel"><span class="red">*</span>发票金额：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="InvoiceAmt" data-options="required:true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">发票编号：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="InvoiceNo"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">备注：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="Comment" Width="90%" ClientIDMode="Static"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <div class="statusbar">
            <asp:LinkButton runat="server" ID="btnSave" CssClass="easyui-linkbutton" iconCls="icon-save" Text="保存" ClientIDMode="Static" OnClick="btnSave_Click" OnClientClick="return $('#form1').form('validate');">
            </asp:LinkButton>
        </div>
    </form>
</body>
</html>
