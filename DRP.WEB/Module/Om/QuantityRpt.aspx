<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuantityRpt.aspx.cs" Inherits="DRP.WEB.Module.Om.QuantityRpt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>机构开通情统计</title>
     <script src="../../Scripts/Plugin/eChart/echarts.js" type="text/javascript"></script>
    <script src="../Rpt/Script/RptUtility.js"></script>
    <script type="text/javascript">
        $(function () {
            fnCreateDay();
            $("#Year").blur(function () {
                fnCreateDay();
            });
            $("#Month").blur(function () {
                fnCreateDay();
                fnDrawRpt();
            });
            fnDrawRpt();
        });
        function fnCreateDay() {
            var y = $("#Year").val();
            var m = $("#Month").val();
            var u = "Service/OmRpt.ashx?action=2&y=" + y + "&m=" + m + "&v=1";
            dataService.ajaxGet(u, function (data) {
                $("#spDay").html(data);
                var d = $(".c_day_on").attr("d");
                fnQueryData(y, m, d);
                $("#spDay").find("a").click(function () {
                    var _y = $("#Year").val();
                    var _m = $("#Month").val();
                    var _d = $(this).attr("d");
                    $(".c_day_on").removeClass("c_day_on");
                    $(this).addClass("c_day_on");
                    fnQueryData(_y, _m, _d);
                });
            });
        }
        function fnQueryData(y, m, d) {
            var u = "Service/OmRpt.ashx?action=1&y=" + y + "&m=" + m + "&d=" + d + "&r=" + getRand();
            dataService.ajaxGet(u, function (data) {
                $("#tblData").html(data);
            });
        }

        function fnDrawRpt() { //按月度绘制图表
            var y = $("#Year").val();
            var u = "Service/OmRpt.ashx?action=3&y=" + y + "&r=" + getRand();
            dataService.ajaxGet(u, function (data) {
                if (data == "" || data == "[]") {
                    $("#rptChart").html("统计数据时发生错误");
                }
                else {
                    var totalAmt = 0;
                    var strLegend = "开通数量";
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
                    $("#rptFooter").html(strLegend + "合计:<span>" + totalAmt + "</span>");
                    fnDrawBarChart(chartSetting);
                }
            });
        }
    </script>
    <style type="text/css">
        .c_day a {
            display: inline-block;
            width: 30px;
            height: 25px;
            line-height: 25px;
            font-size: 12px;
            font-weight: bold;
            font-family: Arial;
            border: 1px solid #ccc;
            margin: 5px 0px 0px 8px;
            text-align: center;
            -webkit-border-radius: 4px;
            -moz-border-radius: 4px;
            border-radius: 4px;
            cursor: pointer;
        }

        .c_day_on {
            background-color: #f00;
            color: #fff;
        }

            .c_day_on:hover {
                color: #fff;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="easyui-panel" title="机构开通数量统计" iconcls="icon-reload" style="padding: 10px;">
            <div>
                日期：<asp:TextBox ID="Year" Width="40" ClientIDMode="Static" runat="server"></asp:TextBox>年
                    <asp:TextBox ID="Month" Width="30" ClientIDMode="Static" runat="server"></asp:TextBox>月 
            </div>
            <div class="c_day" id="spDay">
            </div>
            <table class="tblInfo" style="margin-top: 15px;">
                <tr>
                    <th style="width: 30px;">序</th>
                    <th style="width: 250px;">机构名称</th>
                    <th style="width: 80px;">联系人</th>
                    <th style="width: 80px;">联系电话</th>
                    <th style="width: 100px;">开通日期</th>
                    <th style="width: 160px;">使用有效期</th>
                    <th>开通网址</th>
                    <th style="width: 120px;">应用类型</th>
                    <th style="width: 80px;">创建人</th>
                </tr>
                <tbody id="tblData"></tbody>
            </table>
            <div id="rptChart" style="height: 400px; margin-top: 20px;"></div>
            <div id="rptFooter" style="padding-left: 70px; margin: 10px 0px; font-weight: bold; font-size: 13px;">合计：</div>
        </div>
    </form>
</body>
</html>
