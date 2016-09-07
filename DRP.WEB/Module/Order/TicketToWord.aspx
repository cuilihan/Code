<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TicketToWord.aspx.cs" Inherits="DRP.WEB.Module.Order.TicketToWord" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
         <title>导出Word</title>
    <style type="text/css">
        body {
            font-size: 14px;
        }

        .subject {
            text-align: right;
            font-size: 13px;
            font-family: 微软雅黑;
            font-weight: bold;
        }

        .wrapSchdule {
            font-weight: bold;
            padding: 3px 0px 3px 5px;
            background-color: #F3F3F3;
            border-bottom: 1px solid #E6E6E6;
        }

            .wrapSchdule span {
                padding: 0px 3px 0px 3px;
                font-weight: bold;
            }

        .text {
            line-height: 24px;
        }

        .tblPrint {
            width: 100%;
            border-top: 1px solid #000;
            border-left: 1px solid #000;
        }

            .tblPrint td, .tblPrint th {
                padding: 5px;
                line-height: 22px;
                border-right: 1px solid #000;
                border-bottom: 1px solid #000;
            }

            .tblPrint .label {
                text-align: right;
            }
    </style>
</head>
<body style="margin: 0 auto; background-color: #EEEEEE; color: #000 !important">
    <form id="form1" runat="server">
        <asp:Panel ClientIDMode="Static" ID="pnlWraper" CssClass="wrapper" runat="server">
            <div style="text-align: center; margin: 10px 0px 30px 0px; font-family: 微软雅黑; font-size: 20px; font-weight: bold;">
                <asp:Literal ID="OrderName" runat="server"></asp:Literal>
            </div>
            <div style="margin-bottom: 8px; width: 200px; float: left;">
                PNR订座编号：<asp:Literal ID="PNR" runat="server"></asp:Literal>
            </div>
            <table class="tblPrint">
                <tr>
                    <td class="title">去程日期：
                    </td>
                    <td style="width: 200px;">
                        <asp:Literal ID="TourDate" runat="server"></asp:Literal>
                    </td>
                    <td class="title">回程日期：
                    </td>
                    <td>
                        <asp:Literal ID="ReturnDate" runat="server"></asp:Literal>
                    </td>
                </tr>

                <tr>
                    <td class="title">订票数量：
                    </td>
                    <td>
                        <asp:Literal ID="AdultNum" runat="server"></asp:Literal>
                    </td>
                    <td class="title">机票类型：
                    </td>
                    <td>
                        <asp:Literal ID="TicketType" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="title">订单金额：
                    </td>
                    <td>
                        <asp:Literal ID="OrderAmt" runat="server"></asp:Literal>
                    </td>
                    <td class="title">订单成本：
                    </td>
                    <td>
                        <asp:Literal ID="OrderCost" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="title">订单已收款：
                    </td>
                    <td>
                        <asp:Literal ID="OrderCollectedAmt" runat="server"></asp:Literal>
                    </td>
                    <td class="title">订单未收款：
                    </td>
                    <td>
                        <asp:Literal ID="OrderUnCollected" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="title">订单毛利：
                    </td>
                    <td>
                        <asp:Literal ID="OrderProfit" runat="server"></asp:Literal>
                    </td>
                    <td class="title">订单毛利率：
                    </td>
                    <td>
                        <asp:Literal ID="ProfitRate" runat="server"></asp:Literal>%
                    </td>
                </tr>
                <tr>
                    <td class="title">订票联系人：
                    </td>
                    <td>
                        <asp:Literal ID="Contact" runat="server"></asp:Literal>
                    </td>
                    <td class="title">联系人电话：
                    </td>
                    <td>
                        <asp:Literal ID="ContactPhone" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="title">供应商：
                    </td>
                    <td colspan="3">
                        <asp:Literal ID="SupplierName" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="title">订单备注：
                    </td>
                    <td colspan="3">
                        <asp:Literal ID="Remark" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="title">业务员：
                    </td>
                    <td>
                        <asp:Literal ID="CreateUserName" runat="server"></asp:Literal>
                    </td>
                    <td class="title">提交日期：
                    </td>
                    <td>
                        <asp:Literal ID="CreateDate" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr id="pa" style="display: none" runat="server">
                    <td class="title">参与人员：
                    </td>
                    <td colspan="3">
                        <asp:Literal ID="Participant" runat="server"></asp:Literal>
                    </td>
                </tr>
            </table>
            <div style="margin: 20px 0px 10px 0px; font-weight: bold;">
                航班信息
            </div>
            <table class="tblPrint">
                <tr>
                    <th style="width: 40px;"></th>
                    <th style="width: 80px;">行程
                    </th>
                    <th style="width: 180px;">航班信息
                    </th>
                    <th style="width: 80px;">出发/到达机场
                    </th>
                    <th style="width: 80px;">航空公司
                    </th>
                    <th style="width: 50px;">舱位
                    </th>
                    <th style="width: 50px;">票价
                    </th>
                </tr>
                <tbody>
                    <asp:Literal ID="lblFlight" runat="server"></asp:Literal>
                </tbody>
            </table>
            <div style="margin: 20px 0px 10px 0px; font-weight: bold;">
                客户信息
            </div>
            <table class="tblPrint">
                <tr>
                    <th style="width: 25px;"></th>
                    <th style="width: 60px;">姓名
                    </th>
                    <th style="width: 30px;">性别
                    </th>
                    <th style="width: 100px;">手机号
                    </th>
                    <th style="width: 70px;">证件类型
                    </th>
                    <th style="width: 150px;">证件号码
                    </th>
                    <th style="width: 100px;">公司名称</th>
                    <th>备注
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rptCustomer" OnItemDataBound="rptCustomer_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td style="text-align: center;">
                                <asp:Literal ID="lblNo" runat="server"></asp:Literal></td>
                            <td><%# Eval("Name") %></td>
                            <td style="text-align: center;"><%# Eval("Sex") %></td>
                            <td style="text-align: center;"><%# Eval("Mobile") %></td>
                            <td style="text-align: center;"><%# Eval("IDType") %></td>
                            <td style="text-align: center;"><%# Eval("IDNo") %></td>
                            <td style="text-align: center;"><%# Eval("Company") %></td>
                            <td><%# Eval("Comment") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>

                <asp:Literal ID="lblSeatNo" runat="server"></asp:Literal>

            </table>
            <div style="margin: 20px 0px 10px 0px; font-weight: bold;">
                成本信息
            </div>
            <table class="tblPrint">
                <tr>
                    <th style="width: 25px;"></th>
                    <th style="width: 80px;">供应商类型
                    </th>
                    <th style="width: 180px;">供应商名称
                    </th>
                    <th style="width: 80px;">应付款金额
                    </th>
                    <th style="width: 80px;">已付款金额
                    </th>
                    <th>备注
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rptCost" OnItemDataBound="rptData_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td style="text-align: center;">
                                <asp:Literal ID="lblNo" runat="server"></asp:Literal></td>
                            <td style="text-align: center;"><%# Eval("ItemName") %></td>
                            <td><%# Eval("Supplier") %></td>
                            <td style="text-align: right;"><%# Eval("CostAmt") %></td>
                            <td style="text-align: right;"><%# Eval("PaidAmt") %></td>
                            <td><%# Eval("Comment") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </asp:Panel>
    </form>
</body>
</html>
