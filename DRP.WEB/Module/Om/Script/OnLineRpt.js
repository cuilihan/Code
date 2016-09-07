//订单来源统计
$(function () {
    p.init();
});

var p = {
    init: function () {
        $("input[name='rdType']").click(function () {
            p.fnDrawRpt();
        });
        p.fnDrawRpt();
    },
    serverURL: "Service/OnLineRpt.ashx?r=" + getRand(),
    fnDrawRpt: function () {
        var category = $("input[name='rdType']:checked").val();
        var u = p.serverURL + "&action=1&c=" + category();
        dataService.ajaxGet(u, function (data) {
            if (data == "" || data == "[]") {
                $("#rptChart").html("统计数据时发生错误");
            }
            else {
                var totalAmt = 0;
                var strLegend = "用户登录次数";
                var xAxisData = [];
                var yAxisData = [];
                $(eval(data)).each(function () {
                    var v = this.iCount == "" ? 0 : parseFloat(this.iCount);
                    xAxisData.push(this.Month)
                    yAxisData.push(v);
                    totalAmt += v;
                });
                var chartSetting = {
                    objID: "rptChart",
                    legend: [strLegend],
                    xAxisData: xAxisData,
                    yAxisData: [{
                        name: strLegend,
                        type: 'line',
                        data: yAxisData
                    }]
                };
                $("#rptFooter").html(strLegend + "合计:<span>" + totalAmt + "</span>");
                fnDrawBarChart(chartSetting);
            }
        });
    }
};

