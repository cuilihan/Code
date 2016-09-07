<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderCheckAccount.aspx.cs" Inherits="DRP.WEB.Module.Order.OrderCheckAccount" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>导游报账单列表</title>
    <script type="text/javascript">
        function fnConfirmed(id) { //报账确认
            //var u = "Service/OrderInfo.ashx?id=" + id + "&xType=" + request("xType") + "&action=16&r=" + getRand();
            //dataService.ajaxGet(u, function (data) {
            //    if (data == "1") {
            //        alert("确认成功");
            //        window.location.reload();
            //    }
            //    else {
            //        alert("确认失败");
            //    }
            //});
            var u = "/Module/Order/OrderCheckAcctBill.aspx?id=" + id;
            return openWindow({ "title": "报账单确认", "width": "680", "height": "450", "url": u }, function () {              
                window.location.reload();
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" style="padding: 10px;">
        <table class="tblEdit">
            <tr>
                <th style="width: 30px;"></th>
                <th>导游名称
                </th>

                <th>手机号（报账账号）
                </th>
                <th>报账密码
                </th>
                <th>报账状态
                </th>
                <th style="width: 120px;">操作
                </th>
            </tr>
            <asp:Repeater runat="server" ID="rptData" OnItemDataBound="rptData_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td style="text-align: center;">
                            <asp:Literal ID="lblNo" runat="server"></asp:Literal>
                        </td>
                        <td style="text-align: center;">
                            <%# Eval("GuideName") %>
                        </td>
                        <td style="text-align: center;">
                            <%# Eval("Mobile") %>
                        </td>
                        <td style="text-align: center;">
                            <%# Eval("AcctPwd") %>
                        </td>
                        <td style="text-align: center;">
                            <asp:Literal ID="lblStatus" runat="server"></asp:Literal>
                        </td>
                        <td style="text-align: center;">
                            <asp:Literal ID="lblAction" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </form>
</body>
</html>
