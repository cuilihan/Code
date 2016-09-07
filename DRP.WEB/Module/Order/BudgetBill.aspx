<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeBehind="BudgetBill.aspx.cs" Inherits="DRP.WEB.Module.Order.BudgetBill" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>团队预决算</title>
    <script type="text/javascript">
        $(function () {
            fnLoadBudget();
        });

        var fnLoadBudget = function () {
            var u = "Service/OrderInfo.ashx?xType=" + request("xType") + "&action=15&id=" + request("id") + "&r=" + getRand();
            dataService.ajaxGet(u, function (data) {
                $("#tblData").html(data);
            });
        };
    </script>
</head>
<body style="margin: 0 auto; background-color: #EEEEEE; color:#000 !important">
    <form id="form1" runat="server">
        <div style="padding: 3px 10px; text-align: center; margin: 10px;">
            <a id="btnPrint" onclick="printScope()" href="javascript:;" class="easyui-linkbutton"
                iconcls="icon-print">打印</a>
        </div>
        <asp:Panel ClientIDMode="Static" ID="pnlWraper" CssClass="wrapper" runat="server">
            <!--start print-->
            <div style="text-align: center; margin: 10px 0px 30px 0px; font-family: 微软雅黑; font-size: 20px; font-weight: bold;">
                <asp:Literal ID="OrderName" runat="server"></asp:Literal>预决算单
            </div>
            <div style="margin-bottom: 8px; width: 200px; float: left;">
                订单编号：<asp:Literal ID="TourNo" runat="server"></asp:Literal>
            </div>
            <div style="margin-bottom: 8px; float: right; width: 200px; text-align: right;">
                业务员：<asp:Literal ID="Sales" runat="server"></asp:Literal>
            </div>
            <table class="tblPrint" id="tblData">
            </table>
            <div style="text-align: right; margin-top: 20px;">
                打印人：<asp:Literal ID="UserName" runat="server"></asp:Literal>
                &nbsp;&nbsp;&nbsp;打印时间：<asp:Literal ID="printDate" runat="server"></asp:Literal>
            </div>
            <!--end print-->
        </asp:Panel>
    </form>
</body>
</html>
