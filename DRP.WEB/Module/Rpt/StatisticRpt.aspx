<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatisticRpt.aspx.cs" Inherits="DRP.WEB.Module.Rpt.StatisticRpt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>运营报表</title>
    <script src="../../Scripts/Plugin/eChart/echarts.js" type="text/javascript"></script>
    <script src="Script/RptUtility.js"></script>
    <script src="../../Scripts/Plugin/date_time/WdatePicker.js"></script>
    <script src="Script/OrderTypeRpt.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="tabs" class="easyui-panel" title="订单类型统计" iconcls="icon-search" style="padding: 6px 30px 10px 10px;">
            <div id="rptBatch">
                <table class="tblInfo">
                    <tr>
                        <td class="rowlabel">订单日期范围：
                        </td>
                        <td style="width: 220px;">
                            <asp:TextBox ID="sDate" Width="90" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                            ~
                            <asp:TextBox ID="eDate" Width="90" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                        </td>
                        <td class="rowlabel">订单创建日期：
                        </td>
                        <td style="width: 220px;">
                            <asp:TextBox ID="sCreateDate" Width="90" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                            ~
                            <asp:TextBox ID="eCreateDate" Width="90" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <a class="easyui-linkbutton" iconcls="icon-search" id="btnQuery">统计</a>
                        </td>
                    </tr>
                </table>

                <table class="tblPrint" style="margin-top:20px;">
                    <tr>
                        <th>订单类型</th>
                        <th style="width: 80px;">人数</th>
                        <th style="width: 80px;">应收款</th>
                        <th style="width: 80px;">成本</th>
                        <th style="width: 80px;">毛利</th>
                        <th style="width: 80px;">毛利率</th>
                    </tr>
                    <tbody id="tblData"></tbody>
                </table>

                <div id="rptChart" style="height: 400px;">
                </div>
            </div>
        </div>
    </form>
</body>
</html>
