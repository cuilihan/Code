<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TourToWord.aspx.cs" Inherits="DRP.WEB.Module.Order.TourToWord" %>

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
<body style="margin: 0 auto; background-color: #EEEEEE; color:#000 !important">
    <form id="form1" runat="server">
        <asp:Panel ClientIDMode="Static" ID="pnlWraper" CssClass="wrapper" runat="server">
            <div style="text-align: center; margin: 10px 0px 30px 0px; font-family: 微软雅黑; font-size: 20px; font-weight: bold;">
                <asp:Literal ID="TourName" runat="server"></asp:Literal>
            </div>
            <div style="margin-bottom: 8px; width: 200px; float: left;">
                订单编号：<asp:Literal ID="TourNo" runat="server"></asp:Literal>
            </div>
            <div style="margin-bottom: 8px; float: right; width: 200px; text-align: right;">
                目的地：<asp:Literal ID="Destination" runat="server"></asp:Literal>
            </div>
            <table class="tblPrint">
                <tr>
                    <td class="title">出团日期：
                    </td>
                    <td>
                        <asp:Literal ID="TourDate" runat="server"></asp:Literal>
                    </td>
                    <td class="title">返程日期：
                    </td>
                    <td>
                        <asp:Literal ID="ReturnDate" runat="server"></asp:Literal>
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
                </tr>  <tr>
                    <td class="title">订单毛利：
                    </td>
                    <td>
                        <asp:Literal ID="OrderProfit" runat="server"></asp:Literal>
                    </td>
                    <td class="title">毛利率：
                    </td>
                    <td>
                        <asp:Literal ID="ProfitRate" runat="server"></asp:Literal>%
                    </td>
                </tr>
                <tr>
                    <td class="title">导游：
                    </td>
                    <td colspan="3">
                        <asp:Literal ID="GuideName" runat="server"></asp:Literal>
                    </td>
                </tr>
            </table>
            <div style="margin: 20px 0px 10px 0px; font-weight: bold;">
                线路行程及标准
            </div>
            <div style="border: 1px solid #000; padding: 10px; margin-bottom: 15px;">
                <asp:Repeater ID="rptData" runat="server">
                    <ItemTemplate>
                        <div style="background-color: #F3F3F3; border: 1px solid #ccc; font-weight: bold; height: 25px; line-height: 25px; padding: 5px;">
                            第<%# Eval("DayNum") %>天：<%# Eval("Title") %>
                        </div>
                        <div style="border: 1px solid #ccc; border-top: 0; padding: 5px; line-height: 24px;">
                            <%# Eval("Schedule") %>
                        </div>
                        <div style="border: 1px solid #ccc; border-top: 0; padding: 5px; line-height: 24px;">
                            用餐：<%# Eval("Dinner") %>
                        </div>
                        <div style="border: 1px solid #ccc; border-top: 0; border-bottom: 0; padding: 5px; line-height: 24px;">
                            住宿：<%# Eval("Stay") %>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                        <div style="border-top: 1px solid #ccc;"></div>
                    </FooterTemplate>
                </asp:Repeater>
            </div>

            <table class="tblPrint">
                <tr>
                    <th>费用包含
                    </th>
                    <th>费用不包含
                    </th>
                </tr>
                <tr>
                    <td style="vertical-align: top;">
                        <asp:Literal ID="PriceInclude" runat="server"></asp:Literal>
                    </td>
                    <td style="vertical-align: top;">
                        <asp:Literal ID="PriceNonIncude" runat="server"></asp:Literal>
                    </td>
                </tr>
            </table>


            <asp:Literal ID="lblComment" runat="server"></asp:Literal>
            <!--end print-->
        </asp:Panel>
    </form>
</body>
</html>
