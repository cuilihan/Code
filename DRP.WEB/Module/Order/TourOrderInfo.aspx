<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TourOrderInfo.aspx.cs" Inherits="DRP.WEB.Module.Order.TourOrderInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>自主班团订单</title>
    <style type="text/css">
        .title {
            width: 120px;
            text-align: right;
        }
    </style>
</head>
<body style="margin: 0 auto; background-color: #EEEEEE; color: #000 !important">
    <form id="form1" runat="server">
        <div style="padding: 3px 10px; text-align: center; margin: 10px;">
            <a id="btnPrint" onclick="printScope()" href="javascript:;" class="easyui-linkbutton"
                iconcls="icon-print">打印</a>
            <asp:HyperLink ID="lnkToWord" CssClass="easyui-linkbutton" iconCls="icon-word" runat="server" Target="_blank">导出Word</asp:HyperLink>
        </div>
        <asp:Panel ClientIDMode="Static" ID="pnlWraper" CssClass="wrapper" runat="server">
            <!--start print-->
            <div style="text-align: left;">
                <img src="" runat="server" id="img" style="width: 100%;" />
            </div>
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
                </tr>
                <tr>
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
            <div style="text-align: right; margin-top: 20px;">
                打印人：<asp:Literal ID="UserName" runat="server"></asp:Literal>
                &nbsp;&nbsp;&nbsp;打印时间：<asp:Literal ID="printDate" runat="server"></asp:Literal>
            </div>
            <!--end print-->
            <div style="margin: 20px 0px 10px 0px; font-weight: bold;">
                订单操作日志
            </div>
            <table class="tblPrint">
                <tr>
                    <th style="width: 25px;"></th>
                    <th>操作内容
                    </th>
                    <th style="width: 70px;">操作人
                    </th>
                    <th style="width: 120px;">操作时间
                    </th>
                    <th style="width: 120px;">操作人机器IP
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rptLog" OnItemDataBound="rptData_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td style="text-align: center;">
                                <asp:Literal ID="lblNo" runat="server"></asp:Literal></td>
                            <td><%# Eval("Title") %></td>
                            <td style="text-align: center;"><%# Eval("CreateUserName") %></td>
                            <td style="text-align: center;"><%# Eval("CreateDate") %></td>
                            <td style="text-align: center;"><%# Eval("OpIP") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>

        </asp:Panel>
    </form>
</body>
</html>
