<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sTourNotice.aspx.cs" Inherits="DRP.WEB.Module.Order.sTourNotice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>自主班散客出团书</title>
    <style type="text/css">
        .title
        {
            width: 120px;
            text-align: right;
        }
    </style>
</head>
<body style="margin: 0 auto; background-color: #EEEEEE; color:#000 !important">
    <form id="form1" runat="server">
        <div style="padding: 3px 10px; text-align: center; margin: 10px;">
            <a id="btnPrint" onclick="printScope()" href="javascript:;" class="easyui-linkbutton"
                iconcls="icon-print">打印</a>
        </div>
        <asp:Panel ClientIDMode="Static" ID="pnlWraper" CssClass="wrapper" runat="server">
            <!--start print-->
             <%--<div style="text-align: left;">
                <img src="" runat="server" id="img" style="width: 100%;" />
            </div>--%>
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
                    <td class="title">上车地点：
                    </td>
                    <td>
                        <asp:Literal ID="VenueName" runat="server"></asp:Literal>
                    </td>
                    <td class="title">集合时间：
                    </td>
                    <td>
                        <asp:Literal ID="CollectTime" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">订单备注：
                    </td>
                    <td>
                        <asp:Literal ID="Remark" runat="server"></asp:Literal>
                    </td>
                    <td class="title">订单实收款：
                    </td>
                    <td>
                        <asp:Literal ID="OrderAmt" runat="server"></asp:Literal>
                    </td>
                </tr>
            </table>

            
            <div style="margin: 20px 0px 10px 0px; font-weight: bold;">
                客户信息
            </div>

            <table class="tblPrint">
                <tr>
                    <th style="width: 25px;">序
                    </th>
                    <th style="width: 80px;">客户名称  
                    </th>
                    <th style="width: 50px;">性别
                    </th>
                    <th style="width: 100px;">手机号
                    </th>
                    <th style="width: 150px;">身份证号
                    </th>
                    <th>备注
                    </th>
                </tr>
                <asp:Repeater ID="rptData" runat="server" OnItemDataBound="rptData_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td style="text-align: center;">
                                <asp:Literal ID="lblNo" runat="server"></asp:Literal></td>
                            <td style="text-align: center;"><%# Eval("Name") %></td>
                            <td style="text-align: center;"><%# Eval("Sex") %></td>
                            <td style="text-align: center;"><%# Eval("Mobile") %></td>
                            <td><%# Eval("IDNo") %></td>
                            <td><%# Eval("Comment") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Literal runat="server" ID="lblSeat"></asp:Literal>
            </table>
            <div style="margin: 20px 0px 10px 0px; font-weight: bold;">
                线路行程
            </div>

            <div style="border: 1px solid #000; padding: 10px; margin-bottom: 15px;">
                <asp:Repeater ID="rptSchedule" runat="server">
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

            <asp:Literal ID="lblRemind" runat="server"></asp:Literal>
            <asp:Literal ID="lblComment" runat="server"></asp:Literal>

            <div style="text-align: right; margin-top: 20px;">
                打印人：<asp:Literal ID="UserName" runat="server"></asp:Literal>
                &nbsp;&nbsp;&nbsp;打印时间：<asp:Literal ID="printDate" runat="server"></asp:Literal>
            </div>
            <!--end print-->
        </asp:Panel>
    </form>
</body>
</html>
