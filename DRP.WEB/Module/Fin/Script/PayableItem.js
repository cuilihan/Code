//付款

$(function () {
    p.init();
});

var p = {
    init: function () {
        p.bindData();
        $("#btnQuery").click(function () {
            p.fnQuery();
        });
    },
    serverURL: "Service/PayableItem.ashx",
    bindData: function () {
        var supplierID = request("id");
        var u = p.serverURL + "?id=" + supplierID + "&action=1&r=" + getRand();
        $('#tblData').datagrid({
            loadMsg: "正在加载数据...",
            nowrap: true,
            striped: true,
            border: false,
            height: document.documentElement.clientHeight,
            collapsible: false, //是否可折叠的       
            url: u,
            frozenColumns: [[
                        { field: 'OrderNo', title: '订单编号', width: 140, sortable: true, sortName: "OrderNo" },
                        { field: 'OrderName', title: '订单名称', width: 240, sortable: true, sortName: "OrderName" }
            ]],
            columns: [[
                        { field: 'TourDate', title: '出团日期', width: 90, align: 'center', sortable: true, sortName: "TourDate" },
                        { field: 'CostAmt', title: "应付款", width: 80, sortable: true, sortName: "CostAmt", align: 'right' },
                        { field: 'PaidAmt', title: '已付款', sortable: true, sortName: "PaidAmt", width: 80, align: 'right' },
                        { field: 'UnPayAmt', title: '未付款', sortable: true, sortName: "UnPayAmt", width: 80, align: 'right' }

            ]],
            singleSelect: true, //是否单选 
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
        var name = $("#txtName").val();
        var sDate = $("#sDate").val();
        var eDate = $("#eDate").val();
        $('#tblData').datagrid("reload", { "Name": name, "sDate": sDate, "eDate": eDate });
    }
};