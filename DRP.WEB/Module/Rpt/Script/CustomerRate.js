//客户增长率统计
$(function () {
    p.init();
});

var p = {
    init: function () {
        rpt.fnAutoHeight("rptBatch");
        $("#ddlYear").change(function () {
            p.fnDrawCustomerRate();
        });
        p.fnDrawCustomerRate();
    },
    serverURL: "Service/RptUtility.ashx?xType=9",
    fnDrawCustomerRate: function () {
        var year = $("#ddlYear option:selected").val();
        var u = p.serverURL + "&action=9&year=" + year + "&r=" + getRand();
        dataService.ajaxGet(u, function (data) {
            if (data == "" || data == "[]") {
                $("#rptChart").html("统计数据时发生错误");
            }
            else {
                var totalAmt = 0;
                var strLegend = "客户增长率统计";
                var xAxisData = [];
                var yAxisData = [];
                $(eval(data)).each(function () {
                    xAxisData.push(this.Month)
                    yAxisData.push(this.Data);
                    totalAmt += (this.Data == "" ? 0 : parseFloat(this.Data));
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
                $("#rptFooter").html("客户数量合计:<span>" + totalAmt + "</span>");
                fnDrawBarChart(chartSetting);
            }
        });
    }
};

