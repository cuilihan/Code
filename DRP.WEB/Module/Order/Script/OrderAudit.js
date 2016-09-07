$(function () {
    o.init();
});

var o = {
    init: function () {
        $("#btnQuery").click(function () {
            o.fnQuery();
        }); 
        o.bindData(); 
    },
    url: "Service/OrderAudit.ashx?action=1",
    bindData: function () {
        $('#tblData').datagrid({
            loadMsg: "正在加载数据...",
            nowrap: true,
            striped: true,
            border: false,
            height: document.documentElement.clientHeight - 35,
            collapsible: false, //是否可折叠的       
            url: o.url,
            frozenColumns: [[
                        { field: 'ID', checkbox: true },
                        { field: 'OrderNo', title: "编号", width: 140, sortable: true, sortName: "OrderNo", align: 'center' },
                        {
                            field: 'OrderName', title: '订单名称', width: 160, sortable: true, sortName: "OrderName", formatter: function (val, rec) {  
                                return "<a href='zOrderInfo.aspx?id="+rec.ID+"' target='_blank' title='查看订单'>" + val + "</a>";
                            }
                        },
                        { field: 'tDate', title: '出团日期', width: 80, align: 'center', sortable: true, sortName: "TourDate" }
            ]],
            columns: [[
                        { field: 'SupplierName', title: "供应商", width: 150, sortable: true, sortName: "SupplierName" },
                        {
                            field: 'CustomerName', title: '客户', width: 90, align: 'center', sortable: true, sortName: "Customer", formatter: function (val, rec) {
                                return "<a href='javascript:;' title='查看客户资料' onclick=\"o.fnViewCustomer('" + rec.CustomerID + "')\">" + val + "</a>";
                            }
                        },
                        { field: 'TourDays', title: '天数', width: 40, sortable: true, sortName: "TourDays", align: 'center' },
                        { field: 'AdultNum', title: '成人', width: 40, sortable: true, sortName: "AdultNum", align: 'center' },
                        { field: 'ChildNum', title: '儿童', width: 40, sortable: true, sortName: "ChildNum", align: 'center' },
                        { field: 'OrderAmt', title: '应收款', sortable: true, sortName: "Receivable", width: 70, align: 'right' }, 
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
                        { field: 'CreateUserName', title: '提交人', sortable: true, sortName: "CreateUserName", width: 70, align: 'center' },
                        { field: 'cDate', title: '下单日期', width: 80, align: 'center', sortable: true, sortName: "CreateDate" },
                        {
                            field: 'Opt', title: "操作", width: 90, align: 'center', formatter: function (val, rec) {
                                if (rec.OrderStatus == "4") { //已取消订单
                                    return "<span style='color:red;'>已取消订单</span>";
                                }
                                else { 
                                    var audit = "<a href='javascript:;' title='订单确认' onclick=\"o.fnOrderAudit('" + rec.ID +"')\" >确认</a>"; 
                                    var cancel = "<a href='javascript:;' title='取消订单' onclick=\"o.fnCancel('" + rec.ID + "')\" >取消</a>";
                                    return audit + " | " + cancel;
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
            toolbar: "#toolbar"
        });

        $(window).resize(function () {
            $('#tblData').datagrid('resize');
        });
    },
    fnQuery: function () {
        var orderName = $("#txtOrderName").val();
        var orderNo = $("#txtOrderNo").val();
        var customer = $("#txtCustomer").val();
        var supplierName = $("#txtSupplier").val();
        var dateType = $("#ddlDateType option:selected").val();
        var sDate = $("#sDate").val();
        var eDate = $("#eDate").val();       

        $('#tblData').datagrid("reload", { "OrderName": orderName, "OrderNo": orderNo, "Customer": customer, "Supplier": supplierName, "DateType": dateType, "sDate": sDate, "eDate": eDate });
    },
    fnViewCustomer: function (cid) { //查看客户信息
        var u = "/Module/CRM/CustomerInfo.aspx?id=" + cid;
        addTab("客户信息", u, function () { });
    },
    fnOrderAudit:function(id){ //订单确认
        var u = "/Module/Order/OrderAuditAction.aspx?id=" + id;
        return openWindow({ "title": "订单确认", "width": "500", "height": "320", "url": u }, function () {
            $('#tblData').datagrid("reload");
        });
    },
    fnCancel: function (orderID) {
        if (confirm("确定要取消订单吗")) {
            var url = o.url + "&action=2&OrderID=" + orderID;
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