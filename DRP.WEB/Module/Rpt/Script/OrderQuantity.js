//订单成交量

$(function () {
    p.init();
});

var p = {
    init: function () {
        rpt.fnAutoHeight("rptBatch");
        $("#btnQuery").click(function () {
            p.fnDrawOrderQuantity();
        });
        $("input[name='rdType']").click(function () {
            p.fnDrawOrderQuantity();
        });
        p.fnDrawOrderQuantity();
    },
    serverURL: "Service/RptUtility.ashx?xType=10",
    fnDrawOrderQuantity: function () {
        var year = $("#ddlYear option:selected").val();
        var orderType = $("#ddlOrderType option:selected").val();
        var category = $("input[name='rdType']:checked").val();
        var u = p.serverURL + "&action=8&year=" + year + "&orderType=" + orderType + "&r=" + getRand();
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
        });
    }
};

