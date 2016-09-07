<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TraceEdit.aspx.cs" Inherits="DRP.WEB.Module.Crm.TraceEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>销售线索</title>
    <script src="../../Scripts/Plugin/date_time/WdatePicker.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel runat="server" ID="pnlContainer" Style="padding: 10px;">
            <table class="tblEdit">
                <tr>
                    <td class="rowlabel">客户名称：
                    </td>
                    <td>
                        <asp:TextBox ID="Contact" data-options="required:true" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel"><span class="red">*</span> 销售线索类型：
                    </td>
                    <td>
                        <asp:DropDownList ID="ItemType" data-options="required:true" runat="server" AppendDataBoundItems="true">
                            <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">销售线索名称：
                    </td>
                    <td>
                        <asp:TextBox ID="ItemName" Width="300" runat="server"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td class="rowlabel">备注：
                    </td>
                    <td>
                        <asp:TextBox ID="Comment" EnableTheming="false" CssClass="textbox" TextMode="MultiLine" Height="60" Width="300" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">预计成单日期：
                    </td>
                    <td>
                        <asp:TextBox ID="TradeDate" onclick="WdatePicker()" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center;">
                        <asp:LinkButton ID="btnSave" CssClass="easyui-linkbutton" iconcls="icon-save" runat="server" OnClick="btnSave_Click" OnClientClick="return $('#form1').form('validate');">保存</asp:LinkButton>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </form>
</body>
</html>
