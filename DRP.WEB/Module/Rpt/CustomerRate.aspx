<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerRate.aspx.cs" Inherits="DRP.WEB.Module.Rpt.CustomerRate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>客户增长率</title>
    <script src="../../Scripts/Plugin/eChart/echarts.js" type="text/javascript"></script>
    <script src="Script/RptUtility.js" type="text/javascript"></script>
    <script src="Script/CustomerRate.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="tabs" class="easyui-panel" title="客户增长率统计" iconcls="icon-search" style="padding: 6px;">
            <div id="rptBatch">
                <table class="tblInfo">
                    <tr>
                        <td class="rowlabel">统计年份：
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlYear" ClientIDMode="Static" AppendDataBoundItems="true" runat="server">
                            </asp:DropDownList>
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
