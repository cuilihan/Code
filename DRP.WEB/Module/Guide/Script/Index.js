require.config({
    paths: {
        echarts: '/Scripts/Plugin/eChart'
    }
});

$(function () {
    index.init();
    $(window).resize(function () {
        window.location.reload();
    });
});


var index = {
    init: function () {
        index.fnAutoHeight();
        index.fnLoadOrderData("1");
        $("#rptbar a").click(function () {
            var orderType = $(this).prop("id");
            index.fnLoadOrderData(orderType);
            $(this).addClass("rpt_bar_on").siblings().removeClass("rpt_bar_on");
        });
    },
    fnAutoHeight: function () {
        var h = $(document.documentElement).height() - 293;
        $("#rptChart").css("height", h + "px");
        $(window).resize(function () {
            index.fnAutoHeight();
        });
    },
    serverURL: "Service/Index.ashx",
    fnLoadOrderData: function (orderType) { //查询订单统计数据
        var u = index.serverURL + "?action=1&orderType=" + orderType+"&r="+getRand();
        dataService.ajaxGet(u, function (data) {
            if (data != "") {
                var xData = [];
                var amtData = [];//金额
                var pData = [];//利润 
                $(eval(data)).each(function () {
                    xData.push(this.Month);
                    amtData.push(this.OrderAmt == "" ? 0 : parseFloat(this.OrderAmt));
                    pData.push(this.Profit == "" ? 0 : parseFloat(this.Profit));
                });
                var chartSetting = {
                    objID: "rptChart", 
                    xAxisData: xData,
                    yAxisData: [{
                        name: "订单金额",
                        type: 'line',
                        itemStyle: {
                            normal: {
                                label: {
                                    show: true
                                }
                            }
                        },
                        data: amtData
                    }, {
                        name: "订单利润",
                        type: 'line',
                        itemStyle: {
                            normal: {
                                label: {
                                    show: true
                                }
                            }
                        },
                        data: pData
                    }]
                };
                fnDrawBarChart(chartSetting);
            } else {
                $("#rptChart").html("加载数据出现错误");
            }
        });
    }
};



//绘画柱图与折线图
function fnDrawBarChart(options) {
    var defaults = {
        objID: "",
        legend: [],
        xAxisData: [],
        yAxisData: [{
            name: "",
            type: "",
            data: []
        }]
    };
    var opt = $.extend(defaults, options);
    require(
        [
            'echarts',
            'echarts/chart/line'
        ],
        function (ec) {
            var myChart = ec.init(document.getElementById(opt.objID)); 
            myChart.setOption({
                tooltip: {
                    trigger: 'axis'
                },
                legend: {
                    data: ["订单金额", "订单利润"]
                },
                calculable: true,
                xAxis: [
                    {
                        type: 'category',
                        data: opt.xAxisData
                    }
                ],
                yAxis: [
                    {
                        type: 'value',
                        splitArea: { show: true }
                    }
                ],
                series: opt.yAxisData
            });
        }
    );
}