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
                                    return "<a href='/Module/Order/TicetOrderInfo.aspx?id=" + rec.ID + "' target='_blank' title='查看订单'>" + val + "</a>";
                            }
                        },
                        {
                            field: 'ToFlightLeg', title: '行程', width: 90, sortable: true, sortName: "ToFlightLeg", align: 'center', formatter: function (val, rec) {
                                if (rec.ID) {
                                    var s = rec.FromFlightLeg;
                                    if (s != "") return val + "<div>" + s + "</div>";
                                    else return s;
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
                            field: 'ToConfirmCollectedAmt', title: '待确认收款', sortable: true, sortName: "ToConfirmCollectedAmt", width: 80, align: 'right', formatter: function (val, rec) {
                                return "<a href='javascript:;' style='color:red;' title='查看收款明细' onclick=\"o.fnViewCollected('" + rec.ID + "','" + rec.OrderNo + "')\">" + val + "</a>";
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
                        { field: 'Contact', title: '订票联系人', sortable: true, sortName: "Contact", width: 70, align: 'center' },
                        { field: 'Company', title: '公司名称', width: 90, sortable: true, sortName: "Company", align: 'center' },
                        { field: 'ContactPhone', title: '联系电话', sortable: true, sortName: "ContactPhone", width: 90, align: 'center' },
                        { field: 'CreateUserName', title: '提交人', sortable: true, sortName: "CreateUserName", width: 70, align: 'center' },
                        { field: 'Participant', title: '参与人员', sortable: true, sortName: "Participant", width: 70, align: 'center', hidden: isHidden },
                        { field: 'CreateDate', title: '下单日期', width: 80, align: 'center', sortable: true, sortName: "CreateDate" },
                        {
                            field: 'Opt', title: "操作", width: 180, align: 'center', formatter: function (val, rec) {
                                if (rec.ID) {
                                    if (rec.OrderStatus == "4") { //已取消订单
                                        return "<span style='color:red;'>已取消订单</span>";
                                    }
                                    else {
                                        var btnB = "<a href='javascript:;'  onclick=\"o.fnEditDate('" + rec.ID + "')\" >修改订单日期</a>";
                                        var btnA = "<a href='javascript:;'  onclick=\"o.fnInvoiceSign('" + rec.ID + "')\" >供应商发票登记</a>";
                                        return btnA + " | " + btnB;
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
    fnEditDate: function (oid) { //修改订单日期
        var u = "/Module/Fin/OrderDate.aspx?orderType=6&ID=" + oid;
        return openWindow({ "title": "修改订单日期", "width": "450", "height": "260", "url": u }, function () {
            $('#tblData').datagrid("reload");
        });
    },
    fnViewCollected: function (orderID, orderNo) { //查看收款明细
        var title = "【" + orderNo + "】订单收款明细";
        var url = "OrderCollected.aspx?id=" + orderID;
        addTab(title, url, function () { });
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