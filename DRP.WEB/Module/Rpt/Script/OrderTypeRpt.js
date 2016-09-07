//订单类型统计

$(function () {
    p.init();
});

var p = {
    init: function () {
        $("#btnQuery").click(function () {
            p.fnBindRpt();
        });
        p.fnBindRpt();
    },
    serverURL: "Service/RptUtility.ashx?xType=16",
    fnBindRpt: function () {
        var sDate = $("#sDate").val();
        var eDate = $("#eDate").val();
        var sCreateDate = $("#sCreateDate").val();
        var eCreateDate = $("#eCreateDate").val();
        var u = p.serverURL + "&action=13&r=" + getRand();
        if (sDate != "") u += "&sDate=" + sDate;
        if (eDate != "") u += "&eDate=" + eDate;
        if (sCreateDate != "") u += "&sCreateDate=" + sCreateDate;
        if (eCreateDate != "") u += "&eCreateDate=" + eCreateDate;
        dataService.ajaxGet(u, function (data) {
            if (data == "" || data == "[]") {
                $("#tblData").html("<td colspan='6'>统计数据时发生错误</td>");
            }
            else {
                p.fnDrawChart(data);
                p.fnTableRpt(data);
            }
        });
    },
    fnDrawChart: function (data) { //图表
        var strLegend = "订单毛利";
        var xAxisData = [];
        var yAxisData = [];
        $(eval(data)).each(function () {
            var v = this.Profit == "" ? 0 : parseFloat(this.Profit);
            var name = "";
            switch (parseInt(this.OrderType)) {
                case 1: name = "同行散客"; break;
                case 2: name = "自主班散客"; break;
                case 3: name = "企业团订单"; break;
                case 4: name = "自主班团订单"; break;
                case 5: name = "单项业务订单"; break;
                case 6: name = "机票订单"; break;
            }
            xAxisData.push(name)
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
    },
    fnTableRpt: function (data) { //表格报表
        var sb = [];
        var tOrderAmt = 0;
        var tAdultNum = 0;
        var tChildNum = 0;
        var tOrderCost = 0;
        var tProfit = 0;
        $(eval(data)).each(function () {
            var OrderType = parseInt(this.OrderType);
            var OrderAmt = parseFloat(this.OrderAmt);
            var OrderCost = parseFloat(this.OrderCost);
            var Profit = parseFloat(this.Profit);
            var AdultNum = parseFloat(this.AdultNum);
            var ChildNum = parseFloat(this.ChildNum);
            tOrderAmt += OrderAmt;
            tAdultNum += AdultNum;
            tChildNum += ChildNum;
            tOrderCost += OrderCost;
            tProfit += Profit;

            var rate = "N";
            if (OrderAmt != 0)
                rate = (Profit / OrderAmt * 100).toFixed(2).toString() + "%";
            var name = "";
            switch (OrderType) {
                case 1: name = "同行散客"; break;
                case 2: name = "自主班散客"; break;
                case 3: name = "企业团订单"; break;
                case 4: name = "自主班团订单"; break;
                case 5: name = "单项业务订单"; break;
                case 6: name = "机票订单"; break;
            }
            sb.push("<tr>");
            sb.push("<td style='text-align:center;'>" + name + "</td>");
            sb.push("<td style='text-align:right;'>" + AdultNum + "<sup style='color:red;'>+" + ChildNum + "</sup>" + "</td>");
            sb.push("<td style='text-align:right;'>" + OrderAmt + "</td>");
            sb.push("<td style='text-align:right;'>" + OrderCost + "</td>");
            sb.push("<td style='text-align:right;'>" + Profit + "</td>");
            sb.push("<td style='text-align:right;'>" + rate + "</td>");
            sb.push("</tr>");
        });
        rate = "N";
        if (tOrderAmt != 0)
            rate = (tProfit / tOrderAmt * 100).toFixed(2).toString() + "%";
        sb.push("<tr>");
        sb.push("<td style='text-align:center;'>合计</td>");
        sb.push("<td style='text-align:right;'>" + tAdultNum + "<sup style='color:red;'>+" + tChildNum + "</sup>" + "</td>");
        sb.push("<td style='text-align:right;'>" + tOrderAmt + "</td>");
        sb.push("<td style='text-align:right;'>" + tOrderCost + "</td>");
        sb.push("<td style='text-align:right;'>" + tProfit + "</td>");
        sb.push("<td style='text-align:right;'>" + rate + "</td>");
        sb.push("</tr>");
        $("#tblData").html(sb.join(""));
    }
};
