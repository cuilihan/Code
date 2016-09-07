//非订单支出登记汇总
$(function () {
    p.init();
});

var p = {
    init: function () {
        rpt.fnAutoHeight("rptBatch");
        $("#btnQuery").click(function () {
            p.fnDrawIncomeCheckIn();
        });
        $("input[name='rdType']").click(function () {
            p.fnDrawIncomeCheckIn();
        });
        p.fnDrawIncomeCheckIn();
    },
    serverURL: "Service/RptUtility.ashx?xType=7",
    fnDrawIncomeCheckIn: function () { //绘制非订单收入报表 
        var category = $("input[name='rdType']:checked").val();
        var sDate = $("#sDate").val();
        var eDate = $("#eDate").val();
        var u = p.serverURL + "&action=11&category=" + category + "&r=" + getRand();
        if (sDate != "") u += "&sDate=" + sDate;
        if (eDate != "") u += "&eDate=" + eDate; 
        dataService.ajaxGet(u, function (data) { 
            if (data == "" || data=="[]") {
                $("#rptChart").html("统计数据时发生错误");
            }
            else {
                var totalAmt = 0;
                var strLegend = category == "1" ? "收入类型" : "收入部门";
                var xAxisData = [];
                var yAxisData = [];
                $(eval(data)).each(function () {
                    var v = category == "1" ? this.IncomeType : this.DeptName;
                    xAxisData.push(v)
                    yAxisData.push(this.IncomeAmt);

                    totalAmt += (this.IncomeAmt == "" ? 0 : parseFloat(this.IncomeAmt));
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

