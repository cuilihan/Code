//订单收款认领时查询相关的订单

$(function () {
    o.init();
});

var o = {
    init: function () {
        o.bindData();
        $("#btnQuery").click(function () {
            o.fnQuery();
        });
        $("#btnSave").click(function () {
            o.fnSave();
        });
        $("#OrderType").change(function () {
            o.fnQuery();
        });
    },
    url: "Service/OrderClaim.ashx",
    bindData: function () {
        var h = parseInt(request("h")) - 280;
        $('#tblData').datagrid({
            loadMsg: "正在加载数据...",
            nowrap: true,
            striped: true,
            border: true,
            height: h,
            collapsible: false, //是否可折叠的       
            url: o.url + "?action=1",
            idField: 'ID',
            frozenColumns: [[
                        { field: 'OrderName', title: '订单名称', width: 200, sortable: true, sortName: "OrderName" },
                        { field: 'tDate', title: '出团日期', width: 80, align: 'center', sortable: true, sortName: "TourDate" }
            ]],
            columns: [[
                        { field: 'CustomerName', title: '客户', width: 90, align: 'center', sortable: true, sortName: "Customer" },
                        { field: 'OrderAmt', title: '应收款', sortable: true, sortName: "Receivable", width: 70, align: 'right' },
                        {
                            field: 'UnCollectedAmt', title: '未收款', width: 70, align: 'right', formatter: function (val, rec) {
                                var a = parseFloat(rec.OrderAmt) - parseFloat(rec.CollectedAmt) - parseFloat(rec.ToConfirmCollectedAmt);
                                return "<span style='color:red;'>" + a.toFixed(2).toString() + "</span>"
                            }
                        },
                        { field: 'CreateUserName', title: '提交人', sortable: true, sortName: "CreateUserName", width: 70, align: 'center' }
            ]],
            onSelect: function (rowIndex, rec) { //已收款=已确认收款+未确认收款
                var a = parseFloat(rec.CollectedAmt) + parseFloat(rec.ToConfirmCollectedAmt)
                $("#CollectedAmt").html(a.toFixed(2).toString());
            },
            singleSelect: true, //是否单选 
            pagination: true, //分页控件  
            rownumbers: true, //行号  
            pageSize: 10
        });
    },
    fnQuery: function () {
        var orderType = $("#OrderType option:selected").val();
        var orderName = $("#txtOrderName").val();
        var orderNo = $("#txtOrderNo").val();
        var customer = $("#txtCustomer").val();
        var dateType = $("#ddlDateType option:selected").val();
        var sDate = $("#sDate").val();
        var eDate = $("#eDate").val();
        var orderAmt = $("#txtOrderAmt").val();
        $('#tblData').datagrid("reload", { "OrderType": orderType, "OrderName": orderName, "OrderNo": orderNo, "Customer": customer, "DateType": dateType, "sDate": sDate, "eDate": eDate, "OrderAmt": orderAmt });
    },
    fnSave: function () { //保存收款认领：须先确定当前选择的收款项目未被确认
        var rows = $('#tblData').datagrid('getSelections');
        if (rows.length == 0) {
            Alert("请选择订单");
            return false;
        }
        else {
            var orderID = rows[0].ID;
            var orderType = rows[0].OrderType;
            var billNo = $("#BillNo").val();
            var claimID = request("id");
            var comment = $("#Comment").val();
            var u = o.url + "?action=2&claimID=" + claimID;
            dataService.ajaxGet(u, function (data) {
                if (data == "") {
                    Alert("验证收款认领状态时发生错误");
                    return false;
                }
                else {
                    if (data == "1") { 
                        u = o.url + "?action=3&r=" + getRand();
                        var json = { "ClaimID": claimID, "OrderID": orderID, "BillNo": billNo, "Comment": comment, "OrderType": orderType }
                        dataService.ajaxPost(u, json, function (r) {
                            closeWindow(r == "1" ? "保存成功" : "保存失败");
                        });
                    }
                    else {
                        Alert("当前收款记录已被其他人认领，请重新认领");
                        return false;
                    }
                }
            }, false);
        }
    }
};