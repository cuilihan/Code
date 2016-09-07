<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderQuantity.aspx.cs" Inherits="DRP.WEB.Module.Rpt.OrderQuantity" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>订单成交量分析表</title>
    <script src="../../Scripts/Plugin/date_time/WdatePicker.js"></script>
    <script src="../../Scripts/Plugin/eChart/echarts.js" type="text/javascript"></script>
    <script src="Script/RptUtility.js" type="text/javascript"></script>
    <script src="Script/OrderQuantity.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="tabs" class="easyui-panel" title="订单成交量分析表" iconcls="icon-search" style="padding: 6px;">
            <div id="rptBatch">
                <table class="tblInfo">
                    <tr>
                        <td class="rowlabel">出团年份：
                        </td>
                        <td style="width: 90px;">
                            <asp:DropDownList ID="ddlYear" ClientIDMode="Static" AppendDataBoundItems="true" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td class="rowlabel">订单类型：
                        </td>
                        <td style="width: 150px;">
                            <asp:DropDownList ID="ddlOrderType" runat="server" ClientIDMode="Static">
                                <asp:ListItem Text="所有" Selected="True" Value="0"></asp:ListItem>
                                <asp:ListItem Text="同行散客" Value="1"></asp:ListItem>
                                <asp:ListItem Text="自主班散客" Value="2"></asp:ListItem>
                                <asp:ListItem Text="企业团订单" Value="3"></asp:ListItem>
                                <asp:ListItem Text="自主班团订单" Value="4"></asp:ListItem>
                                <asp:ListItem Text="单项业务订单" Value="5"></asp:ListItem>
                                <asp:ListItem Text="机票订单" Value="6"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="rowlabel">统计类型：
                        </td>
                        <td style="width: 250px;">
                            <asp:RadioButtonList ID="rdType" name="rdType" runat="server" ClientIDMode="Static" RepeatDirection="Horizontal">
                                <asp:ListItem Text="订单毛利" Selected="True" Value="1"></asp:ListItem>
                                <asp:ListItem Text="订单数量" Value="2"></asp:ListItem>
                                <asp:ListItem Text="订单金额" Value="3"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <a class="easyui-linkbutton" iconcls="icon-search" id="btnQuery">统计</a>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;" colspan="7">
                            <div id="rptChart" style="height: 400px;"></div>
                            <div id="rptFooter" style="padding-left: 70px; margin: 10px 0px; font-weight: bold; font-size: 13px;">合计：</div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
