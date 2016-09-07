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
    serverURL: "Service/RptUtility.ashx?xType=14",
    fnBindData: function () {
        p.fnDrawRpt();
        p.fnTableRpt();
    },
    fnDrawRpt: function () {
        var sDate = $("#sDate").val();
        var eDate = $("#eDate").val();
        var sCreateDate = $("#sCreateDate").val();
        var eCreateDate = $("#eCreateDate").val();
        var u = p.serverURL + "&action=3&r=" + getRand();
        if (sDate != "") u += "&sDate=" + sDate;
        if (eDate != "") u += "&eDate=" + eDate;
        if (sCreateDate != "") u += "&sCreateDate=" + sCreateDate;
        if (eCreateDate != "") u += "&eCreateDate=" + eCreateDate;
        dataService.ajaxGet(u, function (data) {
            if (data == "" || data == "[]") {
                $("#rptChart").html("统计数据时发生错误");
            }
            else {
                var strLegend = "订单毛利";
                var xAxisData = [];
                var yAxisData = [];
                $(eval(data)).each(function () {
                    var v = this.Profit == "" ? 0 : parseFloat(this.Profit);
                    xAxisData.push(this.Name)
                    yAxisData.push(v);
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
        var u = p.serverURL + "&action=2&r=" + getRand();
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

