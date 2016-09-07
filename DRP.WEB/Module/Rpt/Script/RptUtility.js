//统计报表通用方法

require.config({
    paths: {
        echarts: '/Scripts/Plugin/eChart'
    }
});

var rpt = {
    fnAutoHeight: function (obj) {
        var h = $(document).height() - 45;
        $("#" + obj).css("height", h + "px");
        $(window).resize(function () {
            rpt.fnAutoHeight();
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
            type: "bar",
            itemStyle: {
                normal: {
                    color: 'tomato',
                    barBorderColor: 'tomato',
                    barBorderWidth: 6,
                    barBorderRadius: 0,
                    label: {
                        show: true
                    }
                }
            },
            data: []
        }]
    };
    var opt = $.extend(defaults, options);
    require(
        [
            'echarts',
            'echarts/chart/bar',
            'echarts/chart/line'
        ],
        function (ec) {
            var myChart = ec.init(document.getElementById(opt.objID));
            myChart.setOption({
                tooltip: {
                    trigger: 'axis'
                },
                legend: {
                    data: opt.legend
                },
                calculable: true,
                toolbox: {
                    show: true,
                    feature: {
                        magicType: { show: true, type: ['line', 'bar'] },
                        saveAsImage: { show: true }
                    }
                },
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