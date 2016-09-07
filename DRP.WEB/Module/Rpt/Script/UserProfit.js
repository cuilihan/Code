//订单来源统计
$(function () {
    p.init();
});

var p = {
    init: function () {
        $("#btnQuery").click(function () {
            p.fnBindData();
        });
        p.fnBindData();
    },
    serverURL: "Service/RptUtility.ashx?xType=15",
    fnBindData: function () {
        p.fnDrawRpt();
        p.fnTableRpt();
    },
    fnDrawRpt: function () {
        var sDate = $("#sDate").val();
        var eDate = $("#eDate").val();
        var sCreateDate = $("#sCreateDate").val();
        var eCreateDate = $("#eCreateDate").val();
        var u = p.serverURL + "&action=4&r=" + getRand();
        if (sDate != "") u += "&sDate=" + sDate;
        if (eDate != "") u += "&eDate=" + eDate;
        if (sCreateDate != "") u += "&sCreateDate=" + sCreateDate;
        if (eCreateDate != "") u += "&eCreateDate=" + eCreateDate;
        dataService.ajaxGet(u, function (data) {
            if (data == "" || data == "[]") {
                $("#rptChart").html("未查询到符合条件的数据");
            }
            else {
                var totalAmt = 0;
                var strLegend = "订单毛利";
                var xAxisData = [];
                var yAxisData = [];
                var idx = 0;
                $(eval(data)).each(function () {
                    var v = this.Profit == "" ? 0 : parseFloat(this.Profit);
                    if (idx < 15) {
                        xAxisData.push(this.CreateUserName)
                        yAxisData.push(v);
                    }
                    idx++;
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
                fnDrawBarChart(chartSetting);
            }
        });
    },
    fnTableRpt: function () { //表格报表
        var u = p.serverURL + "&action=1&r=" + getRand();
        var sDate = $("#sDate").val();
        var eDate = $("#eDate").val();
        var sCreateDate = $("#sCreateDate").val();
        var eCreateDate = $("#eCreateDate").val();
        if (sDate != "") u += "&sDate=" + sDate;
        if (eDate != "") u += "&eDate=" + eDate;
        if (sCreateDate != "") u += "&sCreateDate=" + sCreateDate;
        if (eCreateDate != "") u += "&eCreateDate=" + eCreateDate;
        dataService.ajaxGet(u, function (data) {
            $("#tblData").html(data);
        });
    }
};

