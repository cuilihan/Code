$(function () {
    t.init();
});

var t = {
    init: function () {
        t.fnBindData();
        $("#btnQuery").click(function () {
            t.fnQuery();
        });
    },
    serverUrl: "Service/Customer.ashx?rnd=" + getRand() + "&customerID=" + request("id"),
    fnBindData: function () {
        $('#tblData').datagrid({
            loadMsg: "正在加载数据...",
            height: document.documentElement.clientHeight,
            nowrap: true,
            border: 0,
            iconCls: 'icon-reload',
            url: t.serverUrl + '&action=8',
            idField: 'ID',
            rownumbers: true, //行号 
            toolbar: "#toolbar",
            frozenColumns: [[
                     { field: 'ID', checkbox: true },
                     { field: 'OrderNo', title: "编号", width: 140, sortable: true, sortName: "OrderNo", align: 'center' },
                     {
                         field: "OrderType", title: "订单类型", width: 80, sortabel: true, sortName: "OrderType", align: 'center', formatter: function (val, rec) {
                             var s = "";
                             switch (val.toString()) {
                                 case "1": s = "同行散客"; break;
                                 case "2": s = "自主班散客"; break;
                                 case "3": s = "企业团"; break;
                                 case "4": s = "自主班团"; break;
                                 case "5": s = "单项业务"; break;
                                 case "6": s = "机票"; break;
                             }
                             return s;
                         }
                     },
                     {
                         field: 'OrderName', title: '订单名称', width: 160, sortable: true, sortName: "OrderName", formatter: function (val, rec) {
                             return val;
                         }
                     },
                     { field: 'TourDate', title: '出团日期', width: 90, align: 'center', sortable: true, sortName: "TourDate" }
            ]],
            columns: [[
                        { field: 'TourDays', title: '天数', width: 40, sortable: true, sortName: "TourDays", align: 'center' },
                        { field: 'AdultNum', title: '成人', width: 40, sortable: true, sortName: "AdultNum", align: 'center' },
                        { field: 'ChildNum', title: '儿童', width: 40, sortable: true, sortName: "ChildNum", align: 'center' },
                        { field: 'OrderAmt', title: '订单金额', sortable: true, sortName: "Receivable", width: 90, align: 'right' },
                        { field: 'CollectedAmt', title: '已收款', sortable: true, sortName: "CollectedAmt", width: 80, align: 'right' },
                        {
                            field: 'Profit', title: '毛利', width: 80, align: 'right', formatter: function (val, rec) {
                                var t = parseFloat(rec.OrderAmt) - parseFloat(rec.OrderCost);
                                return t.toFixed(2).toString();
                            }
                        },
                        {
                            field: 'OrderInvoiceAmt', title: "开票状态", sortable: true, sortName: "OrderInvoiceAmt", width: 90, align: 'center', formatter: function (val, rec) {
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
                        },
                        { field: 'CreateUserName', title: '提交人', sortable: true, sortName: "CreateUserName", width: 80, align: 'center' },
                        { field: 'CreateDate', title: '下单日期', width: 80, align: 'center', sortable: true, sortName: "CreateDate" }
            ]],
            rowStyler: function (rowIndex, rec) {
                if (rec.OrderStatus == "4")
                    return "text-decoration:line-through; color:red;";
            },
            singleSelect: false, //是否单选 
            pagination: true, //分页控件    
            pageSize: 20
        });

        $(window).resize(function () {
            $('#tblData').datagrid('resize');
        });
    },
    fnQuery: function () {
        var key = $("#txtName").val();
        var sDate = $("#sDate").val();
        var eDate = $("#eDate").val();
        $('#tblData').datagrid("reload", { "key": key, "sDate": sDate, "eDate": eDate });
    }
};

