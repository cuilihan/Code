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
        $("#btnConfirmed").click(function () { //订单收款确认
            var id = getDataGridSelectedRow('tblData');
            if (id == "") {
                Alert("请选择待收款确认的订单");
                return false;
            }
            if (confirm("确定要确认收款吗")) {
                var url = o.url + "&action=2&id=" + id + "&r=" + getRand();
                dataService.ajaxGet(url, function (s) {
                    if (s == "1") {
                        Alert("确认成功");
                        o.fnQuery();
                    } else Alert("确认失败");
                });
            }
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
    },
    url: "Service/OrderInfo.ashx?xType=3",
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
                                if (rec.ID)
                                return "<a href='/Module/Order/TeamOrderInfo.aspx?id=" + rec.ID + "' target='_blank'>" + val + "</a>";
                            }
                        },
                        { field: 'tDate', title: '出团日期', width: 80, align: 'center', sortable: true, sortName: "TourDate" }
            ]],
            columns: [[
                        { field: 'SupplierName', title: "供应商", width: 130, sortable: true, sortName: "SupplierName" },
                        {
                            field: 'CustomerName', title: '客户', width: 130, sortable: true, sortName: "Customer", formatter: function (val, rec) {
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
                         {
                             field: 'ToConfirmCollectedAmt', title: '待确认收款', sortable: true, sortName: "ToConfirmCollectedAmt", width: 80, align: 'right', formatter: function (val, rec) {
                                 return "<a href='javascript:;' style='color:red;' title='查看收款明细' onclick=\"o.fnViewCollected('" + rec.ID + "','" + rec.OrderNo + "')\">" + val + "</a>";
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
                            field: 'OrderStatus', title: '订单状态', sortable: true, sortName: "OrderStatus", width: 60, align: 'center', formatter: function (val, rec) {
                                var s = "";
                                switch (val) {
                                    case "1": s = "待确认"; break;
                                    case "2": s = "已确认"; break;
                                    case "3": s = "已完成"; break;
                                    case "4": s = "已取消"; break;
                                }
                                return s;
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
                         { field: 'GuideName', title: '导游', width: 80, align: 'center', sortable: true, sortName: "GuideName" },
                         {
                             field: 'IsCheckAccount', title: '报账状态', width: 60, align: 'center', sortable: true, sortName: "IsCheckAccount", formatter: function (val, rec) {
                                 var a = "<a href='javascript:;' title='查看报账单' onclick=\"o.fnCheckAccount('" + rec.ID + "','" + rec.OrderNo + "')\">已报账</a>";
                                 return val == "True" ? a : "";
                             }
                         },
                        {
                            field: 'IsCloseCollected', title: '余款结清', sortable: true, sortName: "IsCloseCollected", width: 60, align: 'center', formatter: function (val, rec) {
                                if(rec.ID)
                                return val.toString() == "1" ? "√" : "";
                            }
                        },
                        {
                            field: 'BudgetStatus', title: '预决算状态', sortable: true, sortName: "BudgetStatus", width: 110, align: 'center', formatter: function (val, rec) {
                                if (rec.ID) {
                                    var yjs = "<a href='/Module/Order/BudgetBill.aspx?id=" + rec.ID + "&xType=3' title='查看预决算' target='_blank'>预决算</a>";
                                    return comm.fnConvertBudgetStatus(val) + " | " + yjs;
                                }
                            }
                        },
                        { field: 'CreateUserName', title: '提交人', sortable: true, sortName: "CreateUserName", width: 70, align: 'center' },
                        { field: 'Participant', title: '参与人员', sortable: true, sortName: "Participant", width: 70, align: 'center', hidden: isHidden },
                        { field: 'cDate', title: '下单日期', width: 80, align: 'center', sortable: true, sortName: "CreateDate" },
                        {
                            field: 'SourceName', title: '订单来源', width: 80, align: 'center', sortable: true, sortName: "SourceName", formatter: function (val, rec) {
                                if (val == "请选择") {
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
                                        var js = "<a href='javascript:;' onclick=\"o.fnOrderFinal('" + rec.ID + "','" + rec.OrderNo + "')\" >决算</a>";
                                        var yjs = "<a href='/Module/Order/BudgetBill.aspx?id=" + rec.ID + "&xType=3' title='查看预决算' target='_blank'>预决算</a>";
                                        var fp = "<a href='javascript:;'  onclick=\"o.fnInvoiceSign('" + rec.ID + "')\" >供应商发票登记</a>";
                                        var ddrq = "<a href='javascript:;'  onclick=\"o.fnEditDate('" + rec.ID + "')\" >修改订单日期</a>";
                                        var sb = [];
                                        if (rec.BudgetStatus != "1")//已预算 
                                        {
                                            if (rec.BudgetStatus == "7")//财务确认后只能查看预决算
                                                sb.push(yjs);
                                            else {
                                                if (rec.BudgetStatus == "5")//已决算不能再编辑预算
                                                    sb.push(js);
                                            }
                                        }
                                        sb.push(fp);
                                        sb.push(ddrq);
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
    fnEditDate: function (oid) { //修改订单日期
        var u = "/Module/Fin/OrderDate.aspx?ID=" + oid;
        return openWindow({ "title": "修改订单日期", "width": "450", "height": "260", "url": u }, function () {
            $('#tblData').datagrid("reload");
        });
    },
    fnViewCollected: function (orderID, orderNo) { //查看收款明细
        var title = "【" + orderNo + "】订单收款明细";
        var url = "OrderCollected.aspx?id=" + orderID;
        addTab(title, url, function () { });
    },
    fnCheckAccount: function (id, orderNo) { //查看报账单
        var title = "【" + orderNo + "】导游报账单";
        var url = "/Module/Order/OrderCheckAccount.aspx?id=" + id + "&xType=3";
        addTab(title, url, function () { });
    },
    fnOrderFinal: function (orderID, orderNo) { //订单决算
        var title = "【" + orderNo + "】决算";
        var url = "OrderBudgetFinal.aspx?xType=3&id=" + orderID + "&orderNo=" + orderNo;
        addTab(title, url, function () {
            o.bindData();
        });
    },
    fnPaidInvoice: function (orderNo, orderID) {
        var title = "【" + orderNo + "】付款发票查询";
        var url = "PaidInvoice.aspx?id=" + orderID;
        addTab(title, url, function () { });
    },
    fnInvoiceSign: function (orderID) { //供应商发票登记
        var u = "/Module/Fin/PaidInvoiceEdit.aspx?id=&orderID=" + orderID;
        return openWindow({ "title": "付款发票登记", "width": "450", "height": "230", "url": u }, function () {
            $('#tblData').datagrid("reload");
        });
    }
};