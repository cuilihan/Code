<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeptProfit.aspx.cs" Inherits="DRP.WEB.Module.Rpt.DeptProfit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>部门利润汇总表</title>
    <script src="../../Scripts/Plugin/date_time/WdatePicker.js"></script>
    <script src="../../Scripts/Plugin/eChart/echarts.js" type="text/javascript"></script>
    <script src="Script/RptUtility.js" type="text/javascript"></script>
    <script src="Script/DeptProfit.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="tabs" class="easyui-panel" title="部门业务统计" iconcls="icon-search" style="padding: 6px 30px 10px 6px;">
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
                    <tr>
                        <td style="vertical-align: top;" colspan="5">
                            <div id="rptChart" style="height: 400px;"></div>
                            <div style="padding: 30px 50px;">
                                <table class="tblPrint" id="tblData">
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
