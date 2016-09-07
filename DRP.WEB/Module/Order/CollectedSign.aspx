<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CollectedSign.aspx.cs" Inherits="DRP.WEB.Module.Order.CollectedSign" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>收款登记</title>
    <script src="../../Scripts/Plugin/date_time/WdatePicker.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#btnSave").click(function () { 
                var strId = request("id"); 
                if (strId.split(',').length > 1) { //批量收款时，收款金额须等于未收款金额
                    var a = parseFloat(request("amt"));
                    var str = $("#CollectAmt").val();
                    var b = str == "" ? 0 : parseFloat(str); 
                    if (a != b) {
                        Alert("批量收款时，收款金额须等于未收款金额");
                        return false;
                    }
                }
                return true;
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding: 10px;">
            <table class="tblEdit">
                <tr>
                    <td class="rowlabel">订单编号：</td>
                    <td>
                        <asp:Literal ID="OrderNo" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td class="rowlabel">收款金额：</td>
                    <td>
                        <asp:TextBox ID="CollectAmt" ClientIDMode="Static" data-options="required:true,validType:'float'" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="rowlabel">收款类型：</td>
                    <td>
                        <asp:DropDownList ID="CollectType" runat="server" AppendDataBoundItems="true">
                            <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">收款银行：</td>
                    <td>
                        <asp:TextBox ID="SrcBank" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="rowlabel">收据编号：</td>
                    <td>
                        <asp:TextBox ID="CollectBill" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="rowlabel">收款日期：</td>
                    <td>
                        <asp:TextBox ID="CollectDate" onclick="WdatePicker()" data-options="required:true,validType:'date'" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="rowlabel">收款备注：</td>
                    <td>
                        <asp:TextBox ID="Comment" TextMode="MultiLine" EnableTheming="false" CssClass="textbox" ClientIDMode="Static" Width="96%" Height="60" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <div style="text-align: center; margin-top: 15px;">
                <asp:LinkButton runat="server" ID="btnSave" CssClass="easyui-linkbutton" OnClick="btnSave_Click" iconCls="icon-save" Text="保存" ClientIDMode="Static" OnClientClick="return $('#form1').form('validate');">
                </asp:LinkButton>
            </div>
        </div>
    </form>
</body>
</html>
