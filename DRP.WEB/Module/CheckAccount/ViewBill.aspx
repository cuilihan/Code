<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewBill.aspx.cs" Inherits="DRP.WEB.Module.CheckAccount.ViewBill" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>查看报账单</title>
    <script src="../../Scripts/jquery-1.7.1.min.js"></script>
    <script src="../../Scripts/drp.core.js"></script>
    <script type="text/javascript">
        var fnLoadData = function () { //查询已报账的数据
            var keyID = request("orderBalanceID");//报账单ID  Ord_OrderBalance表主键
            var url = "Service/OrderCheckAccount.ashx?action=5&r=" + getRand() + "&id=" + keyID;
            dataService.ajaxGet(url, function (s) {
                $("#tblItem").html(s);
            });
        }
        $(function () {
            fnLoadData();
        });
    </script>
</head>
<body style="margin: 0 auto; background-color: #EEEEEE;">
    <form id="form1" runat="server">

        <asp:Panel ClientIDMode="Static" ID="pnlWraper" CssClass="wrapper" Style="margin-top: 20px;" runat="server">

            <div style="margin: 10px 0px 20px 0px;">
                <div style="border:4px solid red; color:#808080; font-style:italic; font-size:17px; text-align:center; font-family:微软雅黑; width:90px; height:40px; line-height:40px; float:right;">
                    <asp:Literal ID="lblStatus" runat="server"></asp:Literal>
                </div>
                <div style="padding-top: 30px;font-family: 微软雅黑; text-align: center; font-size: 20px; ">
                    <asp:Literal ID="OrderName" runat="server"></asp:Literal>
                </div>

            </div>

            <div style="width: 140px; height: 30px; float: right;">
                <span style="font-weight: bold;">出团日期</span>：
            <asp:Literal ID="TourDate" Text="" runat="server"></asp:Literal>
            </div>
            <table class="tblPrint" style="margin-top: 5px;">
                <tbody id="tblItem">
                </tbody>
            </table>
            <div style="height: 5px;"></div>
            <div style="width: 200px; height: 30px; float: left;">
                导游名称：
            <asp:Label ClientIDMode="Static" ID="GuideName" runat="server"></asp:Label>
                <asp:Label ID="GuidePhone" runat="server" Text=""></asp:Label>
            </div>
            <div style="width: 140px; height: 30px; float: right;">
                报账日期：
            <asp:Literal ID="CreateDate" Text="" runat="server"></asp:Literal>
            </div>
        </asp:Panel>

    </form>
</body>
</html>
