$(function () {
    t.init();
});

var t = {
    init: function () {
        t.fnBindData();
        $("#btnQuery").click(function () {
            t.fnBindData();
        });
        $(".searcher_category").find("a").click(function () {
            $(this).addClass("search_on").siblings().removeClass("search_on");
            t.fnBindData();
        });
    },
    serverUrl: "Service/Searcher.ashx?action=2&rnd=" + getRand(),
    fnBindData: function () {
        var itemtype = $(".search_on").attr("itemtype");
        var key = $("#Name").val();
        var tourDate = $("#TourDate").val();
        var u = t.serverUrl + "&itemtype=" + itemtype + "&key=" + encodeURI(key) + "&tourDate=" + tourDate;

        $('#tblData').datagrid({
            loadMsg: "正在加载数据...",
            height: document.documentElement.clientHeight - 32,
            border: 0,
            nowrap: true,
            iconCls: 'icon-reload',
            url: u,
            idField: 'ID',
            rownumbers: true, //行号 
            toolbar: "#toolbar",
            frozenColumns: [[
                        { field: 'ID', checkbox: true },
                        {
                            field: 'OrderType', title: '订单类型', width: 60, sortable: true, sortName: "OrderType", formatter: function (val, rec) {
                                var s = "";
                                switch (val.toString()) {
                                    case "1": s = "同行散客"; break;
                                    case "2": s = "自主班散客"; break;
                                    case "3": s = "企业团"; break;
                                    case "5": s = "单项业务"; break;
                                }
                                return s;
                            }
                        },
                        { field: 'OrderNo', title: "订单编号", width: 140, sortable: true, sortName: "OrderNo", align: 'center' },
                        {
                            field: 'OrderName', title: '订单名称', width: 160, sortable: true, sortName: "OrderName", formatter: function (val, rec) {
                                var u = "";
                                switch (rec.OrderType.toString())
                                {
                                    case "1": u = "tOrderInfo.aspx"; break;
                                    case "2": u = "zOrderInfo.aspx"; break;
                                    case "3": u = "TeamOrderInfo.aspx"; break;
                                    case "5": u = "BizOrderInfo.aspx"; break;
                                }  
                                var page ="/Module/Order/"+ u+ "?id=" + rec.ID;
                                return "<a href='" + page + "' target='_blank' title='查看订单'>" + val + "</a>";
                            }
                        },
                        { field: 'TourDate', title: '出团日期', width: 80, align: 'center', sortable: true, sortName: "TourDate" }
            ]],
            columns: [[
                        { field: 'SupplierName', title: "供应商", width: 130, sortable: true, sortName: "SupplierName" },
                        { field: 'CustomerName', title: '客户', width: 90, align: 'center', sortable: true, sortName: "Customer" },
                        { field: 'TourDays', title: '天数', width: 40, sortable: true, sortName: "TourDays", align: 'center' },
                        { field: 'AdultNum', title: '成人', width: 40, sortable: true, sortName: "AdultNum", align: 'center' },
                        { field: 'ChildNum', title: '儿童', width: 40, sortable: true, sortName: "ChildNum", align: 'center' },
                        { field: 'OrderAmt', title: '应收款', sortable: true, sortName: "Receivable", width: 70, align: 'right' },
                        { field: 'CollectedAmt', title: '已收款', sortable: true, sortName: "CollectedAmt", width: 60, align: 'right'},
                        {
                            field: 'UnCollectedAmt', title: '未收款', width: 70, align: 'right', formatter: function (val, rec) {
                                var a = parseFloat(rec.OrderAmt) - parseFloat(rec.CollectedAmt) - parseFloat(rec.ToConfirmCollectedAmt);
                                return a.toFixed(2).toString();
                            }
                        },
                        { field: 'OrderCost', title: '成本', width: 60, align: 'right' },
                        {
                            field: 'Profit', title: '毛利', width: 60, align: 'right', formatter: function (val, rec) {
                                var t = parseFloat(rec.OrderAmt) - parseFloat(rec.OrderCost);
                                return t.toFixed(2).toString();
                            }
                        },
                        {
                            field: 'P', title: '毛利率', width: 60, align: 'right', formatter: function (val, rec) {
                                var oAmt = parseFloat(rec.OrderAmt);
                                var oCost = parseFloat(rec.OrderCost);
                                var profit = oAmt - oCost;
                                if (oAmt == 0) return "";
                                else {
                                    var rate = profit / oAmt * 100;
                                    if (rate < 5) return "<span style='color:red;'>" + rate.toFixed(2).toString() + "%</span>";
                                    else return rate.toFixed(2).toString() + "%";
                                }
                            }
                        },
                        { field: 'CostInvoiceAmt', title: '供应商发票金额', width: 90, align: 'right', sortable: true, sortName: "CostInvoiceAmt" },
                        {
                            field: 'OrderInvoiceAmt', title: "开票状态", sortable: true, sortName: "OrderInvoiceAmt", width: 70, align: 'center', formatter: function (val, rec) {
                                var s = "未开";
                                var invoiceAmt = parseFloat(rec.OrderInvoiceAmt);
                                var orderAmt = parseFloat(rec.OrderAmt);
                                if (invoiceAmt == 0) s = "未开";
                                else {
                                    var amt = orderAmt - invoiceAmt;
                                    if (amt == 0) s = "已开发票";
                                    else {
                                        if (amt > 0) s = "部分开票";
                                        else s = "超额开票";
                                    }
                                }
                                return s;
                            }
                        }
            ]],
            rowStyler: function (rowIndex, rec) {
                if (rec.DataStatus == "4")
                    return "text-decoration:line-through; color:red;";
            },
            singleSelect: true, //是否单选 
            pagination: true, //分页控件    
            pageSize: 20
        });

        $(window).resize(function () {
            $('#tblData').datagrid('resize');
        });
    }
};

