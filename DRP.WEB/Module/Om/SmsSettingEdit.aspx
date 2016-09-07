<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SmsSettingEdit.aspx.cs" Inherits="DRP.WEB.Module.Om.SmsSettingEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>短信充值设置</title>
    <script type="text/javascript">
        $(function () {
            $("#Amount").blur(function () {
                fnCalculate();
            });
            $("#UnitPrice").blur(function () {
                fnCalculate();
            });
        });

        function fnCalculate() {
            var strAmt = $("#Amount").val();
            var strPrice = $("#UnitPrice").val();
            var amt = strAmt == "" ? 0 : parseInt(strAmt);
            var price = strPrice == "" ? 0 : parseFloat(strPrice);
            if (price != 0) {
                var a = Math.abs(amt / price).toFixed(0).toString();
                $("#SmsCount").val(a);
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding: 10px;">
            <asp:Panel runat="server" ID="pnlControl">
                <table class="tblEdit">
                    <tr>
                        <td class="rowlabel">
                            <span class="red">*</span>充值金额
                        </td>
                        <td>
                            <asp:TextBox ID="Amount" ClientIDMode="Static" data-options="required:true,validType:'int'" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="rowlabel">
                            <span class="red">*</span>短信单价
                        </td>
                        <td>
                            <asp:TextBox ID="UnitPrice" Text="0.08" ClientIDMode="Static" data-options="required:true,validType:'float'" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="rowlabel">
                            <span class="red">*</span>可发短信条数
                        </td>
                        <td>
                            <asp:TextBox ID="SmsCount" ClientIDMode="Static" data-options="required:true,validType:'int'" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="rowlabel">备注
                        </td>
                        <td>
                            <asp:TextBox ID="Comment" ClientIDMode="Static" EnableTheming="false" CssClass="textbox" TextMode="MultiLine" Height="60" Width="90%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center; padding: 5px;">
                            <asp:LinkButton ID="btnSave" CssClass="easyui-linkbutton" OnClientClick="return $('#form1').form('validate');" OnClick="btnSave_Click" iconcls="icon-save" runat="server">保存</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
