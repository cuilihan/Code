﻿$(function () {
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
        $("#btnAdd").click(function () {
            o.fnOrderEdit('', "1");
            return false;
        });
        $("#ddlDept").change(function () {
            var v = $("#ddlDept option:selected").val();
            $("#ddlCreator option").remove();
            if (v != "") {
                var u = "/Service/CommonData.ashx?action=1&deptID=" + v;
                dataService.ajaxGet(u, function (s) {
                    if (s != "") {
                        var arr = [];
                        arr.push("<option value=''>请选择</option>");
                        $(eval(s)).each(function () {
                            var opt = "<option value='" + this.ID + "'>" + this.Name + "</option>";
                            arr.push(opt);
                        });
                        $("#ddlCreator").append(arr.join(""));
                    }
                });
            }
        });
        $("#btnCollectedSign").click(function () {
            var tAmt = 0;
            var arr = [];
            var rows = $('#tblData').datagrid('getSelections');
            for (var i = 0; i < rows.length; i++) {
                var row = rows[i];
                arr.push(row.ID);
                var a = parseFloat(row.OrderAmt) - parseFloat(row.CollectedAmt) - parseFloat(row.ToConfirmCollectedAmt);
                tAmt += a;//所选择的未收款总额
            }
            if (arr.length == 0) {
                Alert("请选择订单");
                return false;
            }
            if (tAmt == 0) {
                Alert("所选择的订单未收款总额等于0不能收款");
                return false;
            }
            var u = "/Module/Order/CollectedSign.aspx?id=" + arr.join(",") + "&amt=" + tAmt.toFixed(2) + "&orderType=1";
            openWindow({ "title": "订单收款登记", "url": u, "width": 600, "height": 440 }, function () {
                $('#tblData').datagrid("reload");
            });
            return false;
        });
    },
    url: "Service/OrderInfo.ashx?xType=" + request("xType"),
    bindData: function () {
        var isHidden = true;
        if ($("#hidePart").val() == "1")
            isHidden = false;
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
                        {
                            field: 'OrderName', title: '订单名称', width: 160, sortable: true, sortName: "OrderName", formatter: function (val, rec) {
                                if (rec.ID) {
                                    var page = rec.OrderType == "1" ? "tOrderInfo.aspx" : "zOrderInfo.aspx";
                                    page += "?id=" + rec.ID;
                                    return "<a href='" + page + "' target='_blank' title='查看订单'>" + val + "</a>";
                                }
                            }
                        },
                        { field: 'tDate', title: '出团日期', width: 80, align: 'center', sortable: true, sortName: "TourDate" }
            ]],
            columns: [[
                        { field: 'SupplierName', title: "供应商", width: 150, sortable: true, sortName: "SupplierName" },
                        {
                            field: 'CustomerName', title: '客户', width: 90, align: 'center', sortable: true, sortName: "Customer", formatter: function (val, rec) {
                                if (rec.ID)
                                    return "<a href='javascript:;' title='查看客户资料' onclick=\"o.fnViewCustomer('" + rec.CustomerID + "')\">" + val + "</a>";
                            }
                        },
                        { field: 'Company', title: '公司名称', width: 90, sortable: true, sortName: "Company", align: 'center' },
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
                                if (oAmt == 0) return "";
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
                            field: 'OrderStatus', title: '订单状态', sortable: true, sortName: "OrderStatus", width: 120, align: 'center', formatter: function (val, rec) {
                                if (rec.ID) {
                                    var s = "";
                                    var a = "<a href='javascript:;' onclick=\"o.fnChangeStatus('" + rec.ID + "','2')\">确认订单</a>";
                                    switch (val) {
                                        case "1": s = "待确认"; break;
                                        case "2": s = "已确认";
                                            a = "<a href='javascript:;' onclick=\"o.fnChangeStatus('" + rec.ID + "','3')\">完成订单</a>";
                                            break;
                                        case "3": s = "<spa style='color:red;'>已完成</span>"; a = ""; break;
                                        case "4": s = "已取消"; a = ""; break;
                                    }
                                    if (a == "") return s;
                                    else return s + " | " + a;
                                }
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
                        { field: 'CreateUserName', title: '提交人', sortable: true, sortName: "CreateUserName", width: 70, align: 'center' },
                        { field: 'Participant', hidden: isHidden, title: '参与人员', sortable: true, sortName: "Participant", width: 70, align: 'center' },
                        { field: 'cDate', title: '下单日期', width: 80, align: 'center', sortable: true, sortName: "CreateDate" },
                        {
                            field: 'SourceName', title: '订单来源', width: 80, align: 'center', sortable: true, sortName: "SourceName", formatter: function (val, rec) {
                                if (val=="请选择") {
                                    return "";
                                }
                                else {
                                    return val;
                                }
                            }
                        },
                        {
                            field: 'Opt', title: "操作", width: 260, align: 'center', formatter: function (val, rec) {
                                if (rec.ID) {
                                    if (rec.OrderStatus == "4") { //已取消订单
                                        return "<span style='color:red;'>已取消订单</span>";
                                    }
                                    else {
                                        var orderType = rec.OrderType;
                                        var edit = "<a href='javascript:;' title='修改订单' onclick=\"o.fnOrderEdit('" + rec.ID + "','" + rec.OrderType + "')\" >编辑</a>";
                                        var notice = "<a href='sTourNotice.aspx?id=" + rec.ID + "' target='_blank' title='出团通知书' >出团单</a>";
                                        var cancel = "<a href='javascript:;' title='取消订单' onclick=\"o.fnCancel('" + rec.ID + "')\" >取消</a>";
                                        var invoice = "<a href='javascript:;' title='订单开发票' onclick=\"o.fnInvoice('" + rec.ID + "')\" >开票</a>";
                                        var sb = [];
                                        sb.push(edit);
                                        if (rec.OrderStatus == "1") { //待确认 
                                            sb.push(cancel);
                                        }
                                        else {
                                            if (orderType == "2") {
                                                sb.push(notice);
                                            }
                                            sb.push(cancel);
                                            sb.push(invoice);
                                        }
                                        return sb.join(" | ");
                                    }
                                }
                            }
                        }
            ]],
            rowStyler: function (rowIndex, rec) {
                if (rec.OrderStatus == "4")
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
    fnSetExport: function () {
        var deptID = $("#ddlDept option:selected").val();
        $("#hideDept").val(deptID)
        var userID = $("#ddlCreator option:selected").val();
        $("#hideCreator").val(userID)
        var orderSource = $("#ddlOrderSource option:selected").text();
        $("#hideOrderSource").val(orderSource)
    },
    fnQuery: function () {
        var orderName = $("#txtOrderName").val();
        var orderNo = $("#txtOrderNo").val();
        var customer = $("#txtCustomer").val();
        var supplierName = $("#txtSupplier").val();
        var dateType = $("#ddlDateType option:selected").val();
        var sDate = $("#sDate").val();
        var eDate = $("#eDate").val();
        var status = $("#ddlOrderStatus option:selected").val();
        var routeTypeID = $("#RouteTypeID").combotree("getValue");
        var destinationID = $("#DestinationID").combotree("getValue");
        var deptID = $("#ddlDept option:selected").val();
        var userID = $("#ddlCreator option:selected").val();
        var orderSource = $("#ddlOrderSource option:selected").text();
        var isQueryCanceld = $("#CanceledOrder").is(":checked");
        if (isQueryCanceld) status = 4; //已取消订单查询
        var partStatus = 0;
        var isPart = $("#Part").is(":checked");
        if (isPart) {
            partStatus = 1;
        }
        var company = $("#txtCompany").val();
        var updateUserName = $("#txtUpdateUserName").val();
        var sUnCollectedAmt = $("#sUnCollectedAmt").val();
        var eUnCollectedAmt = $("#eUnCollectedAmt").val();

        $('#tblData').datagrid("reload", { "OrderName": orderName, "OrderNo": orderNo, "Customer": customer, "Supplier": supplierName, "DateType": dateType, "sDate": sDate, "eDate": eDate, "Status": status, "RouteTypeID": routeTypeID, "DestinationID": destinationID, "DeptID": deptID, "UserID": userID, "OrderSourceID": orderSource, "PartStatus": partStatus, "Company": company, "UpdateUserName": updateUserName, "sUnCollectedAmt": sUnCollectedAmt, "eUnCollectedAmt": eUnCollectedAmt });
    },
    fnViewCustomer: function (cid) { //查看客户信息
        var u = "/Module/CRM/CustomerInfo.aspx?id=" + cid;
        addTab("客户信息", u, function () { });
    },
    fnViewCollected: function (orderID, orderNo) { //查看收款明细
        var title = "【" + orderNo + "】订单收款明细";
        var url = "OrderCollected.aspx?id=" + orderID;
        addTab(title, url, function () { });
    },
    fnOrderEdit: function (id, orderType) { //同行散客订单修改 
        var u = "";
        var t = "同行订单维护";
        if (orderType == "1") {
            u = "OrderTHSK.aspx?id=" + id + "&xType=1";
        }
        else {
            u = "OrderZZB.aspx?id=" + id + "&xType=2";
            t = "自主班订单维护";
        }
        addTab(t, u, function () { o.fnQuery(); });
    },
    fnChangeStatus: function (oid, status) {
        var s = "";
        switch (status) {
            case "1": s = ""; break;
            case "2": s = "确认"; break;
            case "3": s = "完成"; break;
            case "4": s = "取消"; break;
        }
        if (confirm("确定要" + s + "订单吗")) {
            var url = o.url + "&action=18&status=" + status + "&OrderID=" + oid;
            dataService.ajaxGet(url, function (data) {
                var msg = data == "1" ? "操作成功" : "操作失败";
                Notice(msg);
                if (data == "1") {
                    $('#tblData').datagrid("reload");
                }
            });
        }
    },
    fnPaidInvoice: function (orderNo, orderID) {
        var title = "【" + orderNo + "】付款发票查询";
        var url = "PaidInvoice.aspx?id=" + orderID;
        addTab(title, url, function () { });
    },
    fnInvoice: function (orderID) { //开票申请
        var title = "开票申请";
        var url = "OrderInvoice.aspx?xType=" + request("xType") + "&id=" + orderID;
        addTab(title, url, function () { o.bindData(); });
    },
    fnBatchInvoice: function () { //批量开票申请
        var title = "开票申请";
        var orderID = getDataGridSelectedRow('tblData');
        if (orderID == "") {
            Alert("请选择订单");
            return;
        }
        var url = "OrderInvoice.aspx?xType=" + request("xType") + "&id=" + orderID;
        addTab(title, url, function () { o.bindData(); });
    },
    fnCancel: function (orderID) {
        if (confirm("确定要取消订单吗")) {
            var url = o.url + "&action=10&OrderID=" + orderID;
            dataService.ajaxGet(url, function (data) {
                var msg = data == "1" ? "操作成功" : "操作失败";
                Notice(msg);
                if (data == "1") {
                    $('#tblData').datagrid("reload");
                }
            });
        }
    }
};