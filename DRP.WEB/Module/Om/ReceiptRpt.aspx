<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReceiptRpt.aspx.cs" Inherits="DRP.WEB.Module.Om.ReceiptRpt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>收款统计</title>
    <script src="../../Scripts/Plugin/eChart/echarts.js" type="text/javascript"></script>
    <script src="../Rpt/Script/RptUtility.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#Year").change(function () {
                fnDrawRpt();
            });
            fnDrawRpt();
        });

        function fnDrawRpt() { //按月度绘制图表
            var y = $("#Year option:selected").val();
            var u = "Service/OmRpt.ashx?action=6&y=" + y + "&r=" + getRand();
            dataService.ajaxGet(u, function (data) {
                if (data == "" || data == "[]") {
                    $("#rptChart").html("统计数据时发生错误");
                }
                else {
                    var totalAmt = 0;
                    var strLegend = "收款金额";
                    var xAxisData = [];
                    var yAxisData = [];
                    $(eval(data)).each(function () {
                        var v = this.Num;
                        xAxisData.push(this.M)
                        yAxisData.push(v);
                        totalAmt += (v == "" ? 0 : parseFloat(v));
                    });
                    var chartSetting = {
                        objID: "rptChart",
                        legend: [strLegend],
                        xAxisData: xAxisData,
                        yAxisData: [{
                            name: strLegend,
                            type: 'line',
                            itemStyle: {
                                normal: {
                                    label: {
                                        show: true
                                    }
                                }
                            },
                            data: yAxisData
                        }]
                    };
                    $("#rptFooter").html(y + "年" + strLegend + "合计:<span style='padding:0px 5px; color:red; font-size:14;'>" + totalAmt + "</span> 元");
                    fnDrawBarChart(chartSetting);
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="easyui-panel" title="机构开通数量统计" iconcls="icon-reload" style="padding: 10px;">
            <div style="padding-left:50px;">
                <asp:DropDownList runat="server" ID="Year" ClientIDMode="Static"></asp:DropDownList>
            </div>
            <div id="rptChart" style="height: 400px; margin-top: 20px;"></div>
            <div id="rptFooter" style="padding-left: 70px;  margin: 10px 0px; font-weight: bold; font-size: 13px;">合计：</div>
        </div>
    </form>
</body>
</html>
