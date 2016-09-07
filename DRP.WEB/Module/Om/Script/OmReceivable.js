//订单应收款统计

$(function () {
    p.init();
});

var p = {
    init: function () {
        rpt.fnAutoHeight("rptBatch");
        $("#btnQuery").click(function () {
            p.fnOrderStatistic();
        });
        $("input[name='rdType']").click(function () {
            p.fnOrderStatistic();
        });
        p.fnOrderStatistic();
    },
    serverURL: "Service/OmRpt.ashx",
    fnOrderStatistic: function () {
        var sDate = $("#sDate").val();
        var eDate = $("#eDate").val();
        var xType = $("input[name='rdType']:checked").val();
        var u = p.serverURL + "?action=5&sDate=" + sDate + "&eDate=" + eDate + "&r=" + getRand();
        dataService.ajaxGet(u, function (data) {
            if (data != "") {
                var totalAmt = 0;
                var strLegend = xType == "1" ? "游客人数" : (xType == "2" ? "订单数量" : "订单金额");
                var xAxisData = [];
                var yAxisData = [];
                $(eval(data)).each(function () {
                    var v = xType == "1" ? this.VisitorNum : (xType == "2" ? this.OrderNum : this.OrderAmt);
                    xAxisData.push(this.Month)
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
            else {
                $("#rptChart").html("加载数据出现错误");
            }
        });
    }
}

