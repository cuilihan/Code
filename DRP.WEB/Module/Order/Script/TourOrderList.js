$(function () {
    o.init();
});

var o = {
    init: function () {
        comm.fnInitRouteType();
        o.clickEvent();
        o.bindData();
    },
    clickEvent: function () {
        $("#btnQuery").click(function () {
            o.fnQuery();
        });
    },
    url: "Service/OrderInfo.ashx?xType=4",
    bindData: function () {
        $('#tblData').datagrid({
            loadMsg: "正在加载数据...",
            nowrap: true,
            striped: true,
            border: false,
            height: document.documentElement.clientHeight - 35,
            collapsible: false, //是否可折叠的       
            url: o.url + "&action=1",
            idField: 'ID',
            frozenColumns: [[
                        { field: 'ID', checkbox: true },
                        { field: 'OrderNo', title: "编号", width: 140, sortable: true, sortName: "OrderNo", align: 'center' },
                        { field: 'DestinationName', title: '目的地', width: 80, align: 'center', sortable: true, sortName: "DestinationName" },
                        {
                            field: 'OrderName', title: '订单名称', width: 160, sortable: true, sortName: "OrderName", formatter: function (val, rec) {
                                if (rec.ID)
                                    return "<a href='TourOrderInfo.aspx?id=" + rec.ID + "' target='_blank'>" + val + "</a>";
                            }
                        },
                        { field: 'tDate', title: '出团日期', width: 80, align: 'center', sortable: true, sortName: "TourDate" }
            ]],
            columns: [[
                        {
                            field: 'N', title: '计划/报名', width: 70, align: 'center', formatter: function (val, rec) {
                                if (rec.ID)
                                    return rec.PlanNum + " / " + rec.VisitorNum;
                            }
                        },
                        { field: 'TourDays', title: '天数', width: 40, sortable: true, sortName: "TourDays", align: 'center' },
                        { field: 'AdultNum', title: '成人', width: 40, sortable: true, sortName: "AdultNum", align: 'center' },
                        { field: 'ChildNum', title: '儿童', width: 40, sortable: true, sortName: "ChildNum", align: 'center' },
                        { field: 'OrderAmt', title: '应收款', sortable: true, sortName: "Receivable", width: 70, align: 'right' },
                        {
                            field: 'CollectedAmt', title: '已收款', sortable: true, sortName: "CollectedAmt", width: 60, align: 'right', formatter: function (val, rec) {
                                return "<a href='javascript:;' title='查看收款明细' onclick=\"o.fnViewCollected('" + rec.ID + "','" + rec.OrderNo + "')\">" + val + "</a>";
                            }
                        },
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
                                if (oAmt == 0) return "-";
                                else {
                                    var rate = profit / oAmt * 100;
                                    if (rate < 5) return "<span style='color:red;'>" + rate.toFixed(2).toString() + "%</span>";
                                    else return rate.toFixed(2).toString() + "%";
                                }
                            }
                        },
                         {
                             field: 'CostInvoiceAmt', title: '供应商发票金额', width: 90, align: 'right', sortable: true, sortName: "CostInvoiceAmt", formatter: function (val, rec) {
                                 return "<a href='javascript:;' onclick=\"o.fnPaidInvoice('" + rec.OrderNo + "','" + rec.ID + "')\">" + val + "</a>";
                             }
                         },
                        {
                            field: 'OrderInvoiceAmt', title: "开票状态", sortable: true, sortName: "OrderInvoiceAmt", width: 70, align: 'center', formatter: function (val, rec) {
                                if (rec.ID) {
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
                        },
                        {
                            field: 'BudgetStatus', title: '预决算状态', sortable: true, sortName: "BudgetStatus", width: 110, align: 'center', formatter: function (val, rec) {
                                if (rec.ID) {
                                    var yjs = "<a href='BudgetBill.aspx?id=" + rec.ID + "&xType=4' title='查看预决算' target='_blank'>预决算</a>";
                                    return comm.fnConvertBudgetStatus(val) + " | " + yjs;
                                }
                            }
                        },
                        {
                            field: 'GuideName', title: '安排导游', width: 80, align: 'center', sortable: true, sortName: "GuideName", formatter: function (val, rec) {
                                if (rec.ID) {
                                    var s = val == "" ? "安排导游" : val;
                                    return "<a href='javascript:;' onclick=\"o.fnSetGuide('" + rec.ID + "')\" >" + s + "</a>";
                                }
                            }
                        },
                        {
                            field: 'IsCheckAccount', title: '报账状态', width: 60, align: 'center', sortable: true, sortName: "IsCheckAccount", formatter: function (val, rec) {
                                var a = "<a href='javascript:;' title='查看报账单' onclick=\"o.fnCheckAccount('" + rec.ID + "','" + rec.OrderNo + "')\">已报账</a>";
                                return val == "True" ? a : "";
                            }
                        },
                        {
                            field: 'IsCloseCollected', title: '余款结清', sortable: true, sortName: "IsCloseCollected", width: 60, align: 'center', formatter: function (val, rec) {
                                if (rec.ID)
                                    return val.toString() == "1" ? "√" : "";
                            }
                        },
                        {
                            field: 'Opt', title: "操作", width: 260, align: 'center', formatter: function (val, rec) {
                                if (rec.ID) {
                                    if (rec.OrderStatus == "4") { //已取消订单
                                        return "<span style='color:red;'>已取消订单</span>";
                                    }
                                    else {
                                        var ddlb = "<a href='TourOrders.aspx?id=" + rec.ID + "' target='_blank'>订单列表</a>";
                                        var ykjq = "<a href='javascript:;' onclick=\"o.fnCollectionClosed('" + rec.ID + "')\" >余款结清</a>";
                                        var ys = "<a href='javascript:;' onclick=\"o.fnOrderBudget('" + rec.ID + "','" + rec.OrderNo + "')\" >预算</a>";
                                        var js = "<a href='javascript:;' onclick=\"o.fnOrderBudgetFinal('" + rec.ID + "','" + rec.OrderNo + "')\" >决算</a>";
                                        var yjs = "<a href='BudgetBill.aspx?id=" + rec.ID + "&xType=4' title='查看预决算' target='_blank'>预决算</a>";
                                        var ctd = "<a href='zTourNotice.aspx?id=" + rec.ID + "' target='_blank' title='出团通知书' >出团单</a>";
                                        var sb = [];
                                        sb.push(ddlb);
                                        if (rec.IsCloseCollected != "1")
                                            sb.push(ykjq);
                                        if (rec.BudgetStatus == "1")//未预算
                                            sb.push(ys);
                                        else {
                                            if (rec.BudgetStatus == "7")//财务确认后只能查看预决算
                                                sb.push(yjs);
                                            else {
                                                if (rec.BudgetStatus == "5")//已决算不能再编辑预算
                                                    sb.push(js);
                                                else {
                                                    sb.push(ys);
                                                    sb.push(js);
                                                }
                                            }
                                        }
                                        sb.push(ctd);
                                        return sb.join(" | ");
                                    }
                                }
                            }
                        }
            ]],
            rowStyler: function (rowIndex, rec) {
                if (rec.DataStatus == "4")
                    return "text-decoration:line-through; color:red;";
            },
            singleSelect: false, //是否单选 
            pagination: true, //分页控件  
            rownumbers: true, //行号  
            pageSize: 20,
            toolbar: "#toolbar",
            showFooter: true
        });

        $(window).resize(function () {
            $('#tblData').datagrid('resize');
        });
    },
    fnQuery: function () {
        var orderName = $("#txtOrderName").val();
        var orderNo = $("#txtOrderNo").val();
        var dateType = "2";//出团日期
        var sDate = $("#sDate").val();
        var eDate = $("#eDate").val();
        var routeTypeID = $("#RouteTypeID").combotree("getValue");
        var destinationID = $("#DestinationID").combotree("getValue");
        var updateUserName = $("#txtUpdateUserName").val();

        $('#tblData').datagrid("reload", { "OrderName": orderName, "OrderNo": orderNo, "DateType": dateType, "sDate": sDate, "eDate": eDate, "RouteTypeID": routeTypeID, "DestinationID": destinationID, "UpdateUserName": updateUserName });
    },
    fnOrderBudget: function (orderID, orderNo) { //订单预算
        var title = "【" + orderNo + "】预算";
        var url = "OrderBudget.aspx?xType=4&id=" + orderID + "&orderNo=" + orderNo;
        addTab(title, url, function () {
            o.bindData();
        });
    },
    fnOrderBudgetFinal: function (orderID, orderNo) { //订单决算
        var title = "【" + orderNo + "】决算";
        var url = "OrderBudgetFinal.aspx?xType=4&id=" + orderID + "&orderNo=" + orderNo;
        addTab(title, url, function () {
            o.bindData();
        });
    },
    fnCheckAccount: function (id, orderNo) { //查看报账单
        var title = "【" + orderNo + "】导游报账单";
        var url = "OrderCheckAccount.aspx?id=" + id + "&xType=3";
        addTab(title, url, function () { });
    },
    fnViewCollected: function (orderID, orderNo) { //查看收款明细
        var title = "【" + orderNo + "】订单收款明细";
        var url = "OrderCollected.aspx?xType=4&id=" + orderID;
        addTab(title, url, function () { });
    },
    fnPaidInvoice: function (orderNo, orderID) {
        var title = "【" + orderNo + "】付款发票查询";
        var url = "PaidInvoice.aspx?id=" + orderID;
        addTab(title, url, function () { });
    },
    fnSetGuide: function (orderID) { //安排导游
        var title = "安排导游";
        var url = "OrderGuide.aspx?xType=4&id=" + orderID;
        addTab(title, url, function () { o.bindData(); });
    },
    fnCollectionClosed: function (orderID) { //余款结清
        if (!confirm("确定要余款结清吗"))
            return false;
        var u = o.url + "&action=12&id=" + orderID;
        dataService.ajaxGet(u, function (data) {
            if (data == "1") {
                o.fnQuery();
                Notice("操作成功");
            }
            else {
                Alert("操作失败");
            }
        });
    }
};

