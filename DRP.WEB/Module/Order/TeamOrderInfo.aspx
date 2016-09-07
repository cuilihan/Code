<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeamOrderInfo.aspx.cs" Inherits="DRP.WEB.Module.Order.TeamOrderInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>企业团订单</title>
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
                    <td class="title">集合地点：
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
                    <th style="width: 20px;"></th>
                    <th style="width: 80px;">姓名
                    </th>
                    <th style="width: 50px;">性别
                    </th>
                    <th style="width: 100px;">手机号
                    </th>
                    <th style="width: 150px;">公司名称
                    </th>
                    <th style="width: 50px;">联系人</th>
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
                            <td><%# Eval("Company") %></td>
                            <td style="text-align: center;"><%# Convert.ToBoolean(Eval("IsLeader"))?"是":"否" %></td>
                            <td><%# Eval("Comment") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>

            <div style="margin: 20px 0px 10px 0px; font-weight: bold;">
                线路行程及标准
            </div>
            <div style="border: 1px solid #000; padding: 10px; margin-bottom: 15px;">
                <asp:Literal ID="Schedule" runat="server"></asp:Literal>
            </div>

            <div style="margin: 20px 0px 10px 0px; font-weight: bold;">
                附件
            </div>
            <table class="tblPrint" cellpadding="1" cellspacing="1">
                <tr>
                    <th>文件名称
                    </th>
                    <th style="width: 90px;">文件类型
                    </th>
                    <th style="width: 90px;">文件大小
                    </th>
                    <th style="width: 120px;">上传日期
                    </th>
                    <th style="width: 80px;">上传人
                    </th>
                </tr>
                <tbody id="tblFile">
                    <asp:Repeater runat="server" ID="rptFile">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <input type="hidden" value='<%# Eval("ID") %>' />
                                    <a href='<%# Eval("FilePath") %>' target='_blank'>
                                        <%# Eval("FileName") %></a>
                                </td>
                                <td style="text-align: center;">
                                    <%# Eval("FileType") %>
                                </td>
                                <td style="text-align: center;">
                                    <%# Eval("FileSize") %>
                                </td>
                                <td style="text-align: center;">
                                    <%# Eval("CreateDate") %>
                                </td>
                                <td style="text-align: center;">
                                    <%# Eval("CreateUserName") %>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <div style="text-align: right; margin-top: 20px;">
                打印人：<asp:Literal ID="UserName" runat="server"></asp:Literal>
                &nbsp;&nbsp;&nbsp;打印日期：<asp:Literal ID="PrintDate" runat="server"></asp:Literal>
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
