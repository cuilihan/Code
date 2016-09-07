<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="zToWord.aspx.cs" Inherits="DRP.WEB.Module.Order.zToWord" %>

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
            <!--start print-->
            <div style="text-align: center; margin: 10px 0px 30px 0px; font-family: 微软雅黑; font-size: 20px; font-weight: bold;">
                <asp:Literal ID="OrderName" runat="server"></asp:Literal>
            </div>
            <div style="margin-bottom: 8px; width: 200px; float: left;">
                订单编号：<asp:Literal ID="OrderNo" runat="server"></asp:Literal>
            </div>
            <div style="margin-bottom: 8px; float: right; width: 200px; text-align: right;">
                目的地：<asp:Literal ID="DestinationName" runat="server"></asp:Literal>
            </div>
            <table class="tblPrint">
                <tr>
                    <td class="title">出团日期：
                    </td>
                    <td style="width: 200px;">
                        <asp:Literal ID="TourDate" runat="server"></asp:Literal>
                    </td>
                    <td class="title">返程日期：
                    </td>
                    <td>
                        <asp:Literal ID="ReturnDate" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="title">出发地：
                    </td>
                    <td>
                        <asp:Literal ID="Departure" runat="server"></asp:Literal>
                    </td>
                    <td class="title">集合时间：
                    </td>
                    <td>
                        <asp:Literal ID="VenueName" runat="server"></asp:Literal>
                        <asp:Literal ID="CollectTime" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="title">成人人数：
                    </td>
                    <td>
                        <asp:Literal ID="AdultNum" runat="server"></asp:Literal>
                    </td>
                    <td class="title">儿童人数：
                    </td>
                    <td>
                        <asp:Literal ID="ChildNum" runat="server"></asp:Literal>
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
                    <td class="title">订单来源：
                    </td>
                    <td colspan="3">
                        <asp:Literal ID="SourceName" runat="server"></asp:Literal>
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
                <tr id="Part" style="display: none" runat="server">
                    <td class="title">参与人员：
                    </td>
                    <td colspan="3">
                        <asp:Literal ID="Participant" runat="server"></asp:Literal>
                    </td>
                </tr>
            </table>
            <div style="margin: 20px 0px 10px 0px; font-weight: bold;">
                客户信息
            </div>
            <table class="tblPrint">
                <tr>
                    <th style="width: 25px;"></th>
                    <th style="width: 60px;">姓名
                    </th>
                    <th style="width: 50px;">性别
                    </th>
                    <th style="width: 100px;">手机号
                    </th>
                    <th style="width: 150px;">身份证号
                    </th>
                    <th style="width: 100px;">公司名称</th>
                    <th>备注
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rptCustomer" OnItemDataBound="rptData_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td style="text-align: center;">
                                <asp:Literal ID="lblNo" runat="server"></asp:Literal></td>
                            <td><%# Eval("Name") %></td>
                            <td style="text-align: center;"><%# Eval("Sex") %></td>
                            <td style="text-align: center;"><%# Eval("Mobile") %></td>
                            <td style="text-align: center;"><%# Eval("IDNo") %></td>
                            <td style="text-align: center;"><%# Eval("Company") %></td>
                            <td><%# Eval("Comment") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>

                <asp:Literal ID="lblSeatNo" runat="server"></asp:Literal>

            </table>

            <div style="margin: 20px 0px 10px 0px; font-weight: bold;">
                价格明细
            </div>
            <table class="tblPrint">
                <tr>
                    <th style="width: 25px;"></th>
                    <th style="width: 80px;">价格名称
                    </th>
                    <th style="width: 180px;">销售价
                    </th>
                    <th style="width: 80px;">返佣
                    </th>
                    <th style="width: 80px;">报名人数
                    </th>
                    <th style="width: 80px;">单房差
                    </th>
                    <th style="width: 80px;">保险金额
                    </th>
                    <th style="width: 80px;">保险成本
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rptPrice" OnItemDataBound="rptData_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td style="text-align: center;">
                                <asp:Literal ID="lblNo" runat="server"></asp:Literal></td>
                            <td style="text-align: center;"><%# Eval("Name") %></td>
                            <td><%# Eval("SalePrice") %></td>
                            <td style="text-align: right;"><%# Eval("Rebate") %></td>
                            <td style="text-align: right;"><%# Eval("VisitorNum") %></td>
                            <td style="text-align: right;"><%# Eval("RoomRate") %></td>
                            <td style="text-align: right;"><%# Eval("InsuranceAmt") %></td>
                            <td style="text-align: right;"><%# Eval("InsuranceCost") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <!--end print-->
        </asp:Panel>
    </form>
</body>
</html>
