$(function () {
    o.init();
});

var o = {
    init: function () {
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
            var u = "/Module/Order/CollectedSign.aspx?id=" + arr.join(",") + "&amt=" + tAmt.toFixed(2) + "&orderType=6";
            openWindow({ "title": "订单收款登记", "url": u, "width": 600, "height": 440 }, function () {
                $('#tblData').datagrid("reload");
            });
            return false;
        });
    },
    url: "Service/TicketOrder.ashx?rnd=" + getRand(),
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
                        { field: 'PNR', title: "PNR编号", width: 80, sortable: true, sortName: "PNR", align: 'center' },
                        {
                            field: 'OrderName', title: '订单名称', width: 120, sortable: true, sortName: "OrderName", formatter: function (val, rec) {
                                if(rec.ID)
                                return "<a href='TicetOrderInfo.aspx?id=" + rec.ID + "' target='_blank' title='查看订单'>" + val + "</a>";
                            }
                        },
                        {
                            field: 'ToFlightLeg', title: '行程', width: 90, sortable: true, sortName: "ToFlightLeg", align: 'center', formatter: function (val, rec) {
                                if (rec.ID) {
                                    var s = rec.FromFlightLeg;
                                    if (s != "") return val + "<div>" + s + "</div>";
                                    else return val;
                                }
                            }
                        },
                        {
                            field: 'ToFlightInfo', title: '航班信息', width: 200, sortable: true, sortName: "ToFlightInfo", align: 'center', formatter: function (val, rec) {
                                if (rec.ID) {
                                    var s = rec.FromFlightInfo;
                                    if (s != "") return val + "<div>" + s + "</div>";
                                    else return val;
                                }
                            }
                        }
            ]],
            columns: [[
                        {
                            field: 'TourDate', title: '航班日期', width: 80, align: 'center', sortable: true, sortName: "TourDate", formatter: function (val, rec) {
                                if (rec.ID) {
                                    var s = rec.ReturnDate;
                                    if (s != "") return val + "<div>" + s + "</div>";
                                    else return val;
                                }
                            }
                        },
                        { field: 'AdultNum', title: '人数', width: 40, sortable: true, sortName: "AdultNum", align: 'center' },
                        { field: 'SupplierName', title: "供应商", width: 150, sortable: true, sortName: "SupplierName" },
                        { field: 'OrderAmt', title: '应收款', sortable: true, sortName: "Receivable", width: 70, align: 'right' },
                        {
                            field: 'CollectedAmt', title: '已收款', sortable: true, sortName: "CollectedAmt", width: 60, align: 'right', formatter: function (val, rec) {
                                return "<a href='javascript:;' title='查看收款明细' onclick=\"o.fnViewCollected('" + rec.ID + "','" + rec.OrderNo + "')\">" + val + "</a>";
                            }
                        },
                        {
                            field: 'UnCollectedAmt', title: '未收款', width: 70, align: 'right', formatter: function (val, rec) {
                                var a = parseFloat(rec.OrderAmt) - parseFloat(rec.CollectedAmt);
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
                            field: 'OrderStatus', title: '订单状态', sortable: true, sortName: "OrderStatus", width: 60, align: 'center', formatter: function (val, rec) {
                                if (rec.ID) {
                                    var s = "";
                                    switch (val) {
                                        case "1": s = "待确认"; break;
                                        case "2": s = "已确认"; break;
                                        case "3": s = "已完成"; break;
                                        case "4": s = "已取消"; break;
                                    }
                                    return s;
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
                        { field: 'Contact', title: '订票联系人', sortable: true, sortName: "Contact", width: 70, align: 'center' },
                        { field: 'Company', title: '公司名称', sortable: true, sortName: "Company", width: 90, align: 'center' },
                        { field: 'ContactPhone', title: '联系电话', sortable: true, sortName: "ContactPhone", width: 90, align: 'center' },
                        { field: 'CreateUserName', title: '提交人', sortable: true, sortName: "CreateUserName", width: 70, align: 'center' },
                        { field: 'Participant', title: '参与人员', sortable: true, sortName: "Participant", width: 70, align: 'center', hidden: isHidden },
                        { field: 'CreateDate', title: '下单日期', width: 80, align: 'center', sortable: true, sortName: "CreateDate" },
                        {
                            field: 'Opt', title: "操作", width: 130, align: 'center', formatter: function (val, rec) {
                                if (rec.ID) {
                                    if (rec.OrderStatus == "4") { //已取消订单
                                        return "<span style='color:red;'>已取消订单</span>";
                                    }
                                    else {
                                        var orderType = rec.OrderType;
                                        var edit = "<a href='javascript:;' title='修改订单' onclick=\"o.fnOrderEdit('" + rec.ID + "','" + rec.OrderType + "')\" >编辑</a>";
                                        var cancel = "<a href='javascript:;' title='取消订单' onclick=\"o.fnCancel('" + rec.ID + "')\" >取消</a>";
                                        var invoice = "<a href='javascript:;' title='订单开发票' onclick=\"o.fnInvoice('" + rec.ID + "')\" >开票</a>";
                                        var sb = [];
                                        sb.push(edit);
                                        sb.push(cancel);
                                        sb.push(invoice);
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
    fnQuery: function () {
        var orderName = $("#txtOrderName").val();
        var pnr = $("#txtPNR").val();
        var contact = $("#txtContact").val();
        var supplierName = $("#txtSupplier").val();
        var sDate = $("#sDate").val();
        var eDate = $("#eDate").val();
        var csDate = $("#cSData").val();
        var ceDate = $("#cEDate").val();
        var status = $("#ddlOrderStatus option:selected").val();
        var flight = $("#txtFlightInfo").val();
        var company = $("#txtCompany").val();
        var updateUserName = $("#txtUpdateUserName").val();
        var sUnCollectedAmt = $("#sUnCollectedAmt").val();
        var eUnCollectedAmt = $("#eUnCollectedAmt").val();

        $('#tblData').datagrid("reload", { "OrderName": orderName, "PNR": pnr, "Contact": contact, "Supplier": supplierName, "sDate": sDate, "eDate": eDate, "Status": status, "FlightLeg": flight, "csDate": csDate, "ceDate": ceDate, "Company": company, "UpdateUserName": updateUserName, "sUnCollectedAmt": sUnCollectedAmt, "eUnCollectedAmt": eUnCollectedAmt });
    },
    fnViewCollected: function (orderID, orderNo) { //查看收款明细
        if (typeof (orderNo) == "undefined") orderNo = "N";
        var title = "【" + orderNo + "】订单收款明细";
        var url = "OrderCollected.aspx?id=" + orderID;
        addTab(title, url, function () { });
    },
    fnOrderEdit: function (id) { //同行散客订单修改 
        var u = "TicketOrderEdit.aspx?id=" + id;
        var t = "机票订单维护";
        addTab(t, u, function () { o.fnQuery(); });
    },
    fnPaidInvoice: function (orderNo, orderID) {
        var title = "【" + orderNo + "】付款发票查询";
        var url = "PaidInvoice.aspx?id=" + orderID;
        addTab(title, url, function () { });
    },
    fnInvoice: function (orderID) { //开票申请
        var title = "开票申请";
        var url = "OrderInvoice.aspx?xType=6&id=" + orderID;
        addTab(title, url, function () { o.bindData(); });
    },
    fnBatchInvoice: function () { //批量开票申请
        var title = "开票申请";
        var orderID = getDataGridSelectedRow('tblData');
        if (orderID == "") {
            Alert("请选择订单");
            return;
        }
        var url = "OrderInvoice.aspx?xType=6&id=" + orderID;
        addTab(title, url, function () { o.bindData(); });
    },
    fnCancel: function (orderID) {
        if (confirm("确定要取消订单吗")) {
            var url = o.url + "&action=4&OrderID=" + orderID;
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
