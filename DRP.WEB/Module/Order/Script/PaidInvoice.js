$(function () {
    t.init();
});

var t = {
    init: function () {
        t.fnBindData();
    },
    serverUrl: "/Module/Fin/Service/PaidInvoice.ashx?rnd=" + getRand(),
    fnBindData: function () {
        $('#tblData').datagrid({
            loadMsg: "正在加载数据...",
            height: document.documentElement.clientHeight,
            nowrap: true,
            iconCls: 'icon-reload',
            url: t.serverUrl + '&action=1&orderID=' + request("id"),
            idField: 'ID',
            border: false,
            rownumbers: true, //行号 
            toolbar: "#toolbar",
            columns: [[
                { field: "ID", checkbox: true },
                { field: 'InvoiceAmt', title: '发票金额', width: 90 },
                { field: 'InvoiceNo', title: '发票编号', width: 90 },
                { field: 'Comment', title: '备注', width: 450 },
                { field: 'CreateUserName', title: '登记人', width: 90, align: "center" }
            ]],
            singleSelect: true, //是否单选 
            pagination: false //分页控件     
        });

        $(window).resize(function () {
            $('#tblData').datagrid('resize');
        });
    }
};

