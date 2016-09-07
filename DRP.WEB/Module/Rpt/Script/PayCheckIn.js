//非订单支出登记汇总
$(function () {
    p.init();
});

var p = {
    init: function () {
        rpt.fnAutoHeight("rptBatch");
        $("#btnQuery").click(function () {
            p.fnDrawPayCheckIn();
        });
        $("input[name='rdType']").click(function () {
            p.fnDrawPayCheckIn();
        });
        p.fnDrawPayCheckIn();
    },
    serverURL: "Service/RptUtility.ashx?xType=7",
    fnDrawPayCheckIn: function () { //绘制非订单支出报表 
        var category = $("input[name='rdType']:checked").val();
        var sDate = $("#sDate").val();
        var eDate = $("#eDate").val();
        var u = p.serverURL + "&action=12&category=" + category + "&r=" + getRand();
        if (sDate != "") u += "&sDate=" + sDate;
        if (eDate != "") u += "&eDate=" + eDate;
        dataService.ajaxGet(u, function (data) {
            if (data == "" || data == "[]") {
                $("#rptChart").html("统计数据时发生错误");
            }
            else {
                var totalAmt = 0;
                var strLegend = category == "1" ? "支出类型" : "支出部门";
                var xAxisData = [];
                var yAxisData = [];
                $(eval(data)).each(function () {
                    var v = category == "1" ? this.PayType : this.DeptName;
                    xAxisData.push(v)
                    yAxisData.push(this.PayAmt);

                    totalAmt += (this.PayAmt == "" ? 0 : parseFloat(this.PayAmt));
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

