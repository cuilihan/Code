<%@ Page Title="选择订单-导游报账" Language="C#" MasterPageFile="~/Module/CheckAccount/ChkAcct.Master" AutoEventWireup="true" CodeBehind="OrderSelect.aspx.cs" Inherits="DRP.WEB.Module.CheckAccount.OrderSelect" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CheckAccount.css" rel="stylesheet" />
    <script type="text/javascript">
        function fnQueryOrder(xType) {
            var u = "Service/OrderCheckAccount.ashx?action=" + xType + "&r=" + getRand();
            dataService.ajaxGet(u, function (data) {
                var id = xType == 0 ? "tblData" : "tblDataScope";
                var sb = [];
                if (data == "") {
                    sb.push("<tr>");
                    sb.push("<td colspan='6'>无</td>");
                    sb.push("</tr>");
                }
                else {
                    $(eval(data)).each(function (i) {
                        sb.push("<tr>");
                        sb.push("<td style='text-align:center;'>" + (i + 1).toString() + "</td>");
                        sb.push("<td style='text-align:center;'>" + this.OrderNo + "</td>");
                        sb.push("<td>" + this.OrderName + "</td>");
                        sb.push("<td style='text-align:center;'>" + this.TourDate + "</td>");
                        var s = "未报账";
                        var a = "<a href='Index.aspx?id=" + this.ID + "&xType=" + this.OrderType + "'>报账</a>";

                        switch (this.IsOver.toString()) {
                            case "1":
                                s = "<span style='color:red;'>已报账</span>";
                                a = "<a href='Index.aspx?id=" + this.ID + "&orderBalanceID=" + this.OrderBalanceID + "&xType=" + this.OrderType + "'>修改报账</a>";
                                a += "&nbsp;&nbsp;<a href='ViewBill.aspx?id=" + this.ID + "&xType=" + this.OrderType + "&orderBalanceID=" + this.OrderBalanceID + "' target='_blank'>查看报账</a>";
                                break;
                            case "2":
                                s = "<span style='color:blue;'>已确认</span>";
                                a = "<a href='ViewBill.aspx?id=" + this.ID + "&xType=" + this.OrderType + "&orderBalanceID=" + this.OrderBalanceID + "' target='_blank'>查看报账</a>"; 
                                break;
                        }
                        sb.push("<td style='text-align:center;'>" + s + "</td>");
                        sb.push("<td style='text-align:center;'>" + a + "</td>");
                        sb.push("</tr>");
                    });
                }
                $("#" + id).html(sb.join(""));
            });
        }
        $(function () {
            fnQueryOrder(0);
            fnQueryOrder(1);
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="subject">
        <div class="subject_t">
            <span>近一个月内订单
            </span>
        </div>
        <table class="tblEdit">
            <tr>
                <th style="width: 30px;"></th>
                <th style="width: 150px;">订单编号
                </th>
                <th>订单名称
                </th>
                <th style="width: 110px;">出团日期
                </th>
                <th style="width: 90px;">报账状态
                </th>
                <th style="width: 150px;">操作
                </th>
            </tr>
            <tbody id="tblData">
            </tbody>
        </table>
        <div class="subject_t" style="margin-top: 50px;">
            <span>一月以前订单
            </span>
        </div>
        <table class="tblEdit">
            <tr>
                <th style="width: 30px;"></th>
                <th style="width: 150px;">订单编号
                </th>
                <th>订单名称
                </th>
                <th style="width: 110px;">出团日期
                </th>
                <th style="width: 90px;">报账状态
                </th>
                <th style="width: 120px;">操作
                </th>
            </tr>
            <tbody id="tblDataScope">
            </tbody>
        </table>
    </div>
</asp:Content>
