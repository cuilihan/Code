<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderDate.aspx.cs" Inherits="DRP.WEB.Module.Fin.OrderDate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>修改订单日期</title>
    <script src="../../Scripts/Plugin/date_time/WdatePicker.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#btnSave").click(function () {
                var sDate = $("#TourDate").val();
                var eDate = $("#CreateDate").val();
                if (sDate == "") {
                    Alert("请选择订单日期");
                    return false;
                }
                if (eDate == "") {
                    Alert("请选择订单创建日期");
                    return false;
                }
                return true;
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding: 2px 5px;">
            <table class="tblEdit">
                <tr>
                    <td class="rowlabel">订单名称：
                    </td>
                    <td>
                        <asp:Literal ID="OrderName" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">出团日期：
                    </td>
                    <td>
                        <asp:TextBox ID="TourDate" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">创建日期：
                    </td>
                    <td>
                        <asp:TextBox ID="CreateDate" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="color: red;">注：修改订单日期为了结转订单的绩效周期，实现不同时间点的绩效统计。
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="padding: 5px; text-align: center;">
                        <asp:LinkButton ID="btnSave" OnClick="btnSave_Click" ClientIDMode="Static" CssClass="easyui-linkbutton" iconcls="icon-save" runat="server">保存</asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
