//订单来源统计
$(function () {
    p.init();
});

var p = {
    init: function () {
        rpt.fnAutoHeight("rptBatch");
        $("#btnQuery").click(function () {
            p.fnDrawOrderSource();
        });
        $("input[name='rdType']").click(function () {
            p.fnDrawOrderSource();
        });
        p.fnDrawOrderSource();
    },
    serverURL: "Service/RptUtility.ashx?xType=8",
    fnDrawOrderSource: function () { //绘制订单来源
        var category = $("input[name='rdType']:checked").val();
        var sDate = $("#sDate").val();
        var eDate = $("#eDate").val();
        var u = p.serverURL + "&action=10&r=" + getRand();
        if (sDate != "") u += "&sDate=" + sDate;
        if (eDate != "") u += "&eDate=" + eDate;
        dataService.ajaxGet(u, function (data) {
            if (data == "" || data == "[]") {
                $("#rptChart").html("统计数据时发生错误");
            }
            else {
                var totalAmt = 0;
                var strLegend = category == "1" ? "订单毛利" : (category == "2" ? "订单数量" : "订单金额");
                var xAxisData = [];
                var yAxisData = [];
                $(eval(data)).each(function () {
                    var v = category == "1" ? this.Profit : (category == "2" ? this.OrderNum : this.OrderAmt);
                    xAxisData.push(this.SourceName)
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
};

