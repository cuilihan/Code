<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrgOrderRpt.aspx.cs" Inherits="DRP.WEB.Module.Om.OrgOrderRpt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>机构订单数统计</title>
    <script src="../../Scripts/Plugin/date_time/WdatePicker.js"></script>
    <script type="text/javascript">
        function fnQuery() {
            var name = $("#Name").val();
            var sDate = $("#sDate").val();
            var eDate = $("#eDate").val();
            var u = "Service/OmRpt.ashx?action=4&r=" + getRand();
            if (name != "") {
                u += "&name=" + encodeURI(name);
            }
            u += "&sDate=" + sDate + "&eDate=" + eDate;
            dataService.ajaxGet(u, function (data) {
                $("#tblData").html(data);
            });
        }
        $(function () {
            fnQuery();
            $("#btnSearch").click(function () {
                fnQuery();
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="easyui-panel" iconcls="icon-reload" title="机构订单量统计" style="padding: 10px;">
            <table class="tblEdit">
                <tr>
                    <td class="rowlabel">机构名称：
                    </td>
                    <td style="width: 160px;">
                        <asp:TextBox ID="Name" ClientIDMode="Static" runat="server"></asp:TextBox>
                    </td>
                    <td class="rowlabel">下单日期：
                    </td>
                    <td style="width: 230px;">
                        <asp:TextBox ID="sDate" Width="90" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                        ~
                    <asp:TextBox ID="eDate" Width="90" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <a class="easyui-linkbutton" iconcls="icon-search" id="btnSearch">查询</a>
                    </td>
                </tr>
            </table>
            <table class="tblEdit" style="margin-top: 20px;">
                <thead>
                    <tr>
                        <th colspan="6">订单最多的机构</th>
                        <th colspan="6">订单最少的机构</th>
                    </tr>
                    <tr>
                        <th style="width: 30px;">序</th>
                        <th>机构名称</th>
                        <th style="width: 160px;">联系人</th>
                        <th style="width: 110px;">开通日期</th>
                        <th style="width: 110px;">到期日期</th>
                        <th style="width: 60px;">订单数量</th>
                        <th style="width: 30px;">序</th>
                        <th>机构名称</th>
                        <th style="width: 160px;">联系人</th>
                        <th style="width: 110px;">开通日期</th>
                        <th style="width: 110px;">到期日期</th>
                        <th style="width: 60px;">订单数量</th>
                    </tr>
                </thead>
                <tbody id="tblData"></tbody>
            </table>
        </div>
    </form>
</body>
</html>
