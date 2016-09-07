<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TourOrders.aspx.cs" Inherits="DRP.WEB.Module.Order.TourOrders" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>自主班团队订单列表</title>
    <script type="text/javascript">
        $(function () {
            var u = "Service/OrderInfo.ashx?xType=4&action=13&id=" + request("id") + "&r=" + getRand();
            dataService.ajaxGet(u, function (data) {
                $("#tblData").html(data);
            });
        });
    </script>
</head>
<body style="margin: 0 auto; background-color: #EEEEEE; color:#000 !important">
    <form id="form1" runat="server">
        <div style="padding: 3px 10px; text-align: center; margin: 10px;">
            <a id="btnPrint" onclick="printScope()" href="javascript:;" class="easyui-linkbutton"
                iconcls="icon-print">打印</a>
            <asp:HyperLink ID="lnkToWord" CssClass="easyui-linkbutton" iconCls="icon-word" runat="server" Target="_blank">导出Word</asp:HyperLink>
        </div>
        <asp:Panel ClientIDMode="Static" ID="pnlWraper" CssClass="wrapper" runat="server">
            <!--start print-->
            <div style="text-align: center; margin: 10px 0px 30px 0px; font-family: 微软雅黑; font-size: 20px; font-weight: bold;">
                <asp:Literal ID="OrderName" runat="server"></asp:Literal>
            </div>
            <div style="margin-bottom: 8px; width: 300px; float: left;">
                出团日期：<asp:Literal ID="TourDate" runat="server"></asp:Literal>
                <i style="padding-left: 1em;"></i>返程日期：<asp:Literal ID="ReturnDate" runat="server"></asp:Literal>
            </div>
            <div style="margin-bottom: 8px; float: right; width: 200px; text-align: right;">
                目的地：<asp:Literal ID="DestinationName" runat="server"></asp:Literal>
            </div>
            <table class="tblPrint">
                <tr>
                    <th rowspan="2" style="width: 130px;">订单信息
                    </th>
                    <th rowspan="2" style="width: 110px;">上车地点</th>
                    <th rowspan="2" style="width: 80px;">人数</th>
                    <th colspan="4">游客信息</th>
                </tr>
                <tr>
                    <th style="width: 40px;">姓名</th>
                    <th style="width: 70px;">手机号</th>
                    <th style="width: 90px;">身份证号</th>
                    <th>备注</th>
                </tr>
                <tbody id="tblData">
                    <tr>
                        <td colspan="7">正在加载数据...</td>
                    </tr>
                  
                </tbody>

            </table>
            <div style="text-align: right; margin-top: 20px;">
                打印人：<asp:Literal ID="UserName" runat="server"></asp:Literal>
                &nbsp;&nbsp;&nbsp;打印时间：<asp:Literal ID="printDate" runat="server"></asp:Literal>
            </div>
            <!--end print-->
        </asp:Panel>
    </form>
</body>
</html>
