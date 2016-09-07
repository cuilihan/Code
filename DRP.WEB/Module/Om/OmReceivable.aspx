<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OmReceivable.aspx.cs" Inherits="DRP.WEB.Module.Om.OmReceivable" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>订单应收款统计</title>
    <script src="../../Scripts/Plugin/date_time/WdatePicker.js"></script>
    <script src="../../Scripts/Plugin/eChart/echarts.js" type="text/javascript"></script>
    <script src="../Rpt/Script/RptUtility.js"></script>
    <script src="Script/OmReceivable.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="tabs" class="easyui-panel" title="订单应收款统计" iconcls="icon-search" style="padding: 6px;">
            <div id="rptBatch">
                <table class="tblInfo">
                    <tr>
                        <td class="rowlabel">创建日期：
                        </td>
                        <td style="width: 230px;">
                            <asp:TextBox ID="sDate" runat="server" Width="90" ClientIDMode="Static"></asp:TextBox>
                            ~
                            <asp:TextBox ID="eDate" runat="server" Width="90" ClientIDMode="Static"></asp:TextBox>
                        </td> 
                         <td class="rowlabel">统计类型：
                        </td>
                        <td style="width:250px;">
                            <asp:RadioButtonList ID="rdType" name="rdType" runat="server" ClientIDMode="Static" RepeatDirection="Horizontal">
                                <asp:ListItem Text="游客人数" Value="1"></asp:ListItem>
                                <asp:ListItem Text="订单数量" Value="2"></asp:ListItem>
                                <asp:ListItem Text="订单金额" Selected="True" Value="3"></asp:ListItem>
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
