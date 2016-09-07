<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IncomeCheckIn.aspx.cs" Inherits="DRP.WEB.Module.Rpt.IncomeCheckIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>非订单收入登记汇总</title>
    <script src="../../Scripts/Plugin/date_time/WdatePicker.js"></script>
    <script src="../../Scripts/Plugin/eChart/echarts.js" type="text/javascript"></script>
    <script src="Script/RptUtility.js" type="text/javascript"></script>
    <script src="Script/IncomeCheckIn.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="tabs" class="easyui-panel" title="非订单收入汇总" iconcls="icon-search" style="padding: 6px;">
            <div id="rptBatch">
                <table class="tblInfo">
                    <tr>
                        <td class="rowlabel">收入日期范围：
                        </td>
                        <td style="width: 220px;">
                            <asp:TextBox ID="sDate" Width="90" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                            ~
                            <asp:TextBox ID="eDate" Width="90" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                        </td>
                        <td class="rowlabel">统计类型：
                        </td>
                        <td style="width: 200px;">
                            <asp:RadioButtonList ID="rdType" name="rdType" runat="server" ClientIDMode="Static" RepeatDirection="Horizontal">
                                <asp:ListItem Text="按收入类型" Selected="True" Value="1"></asp:ListItem>
                                <asp:ListItem Text="按收入部门" Value="2"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <a class="easyui-linkbutton" iconcls="icon-search" id="btnQuery">统计</a>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;" colspan="5">
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
