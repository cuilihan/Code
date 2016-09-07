//订单收款明细

$(function () {
    o.init();
});

var o = {
    init: function () {
        o.bindData();
    },
    url: "/Module/Fin/Service/OrderCollected.ashx",
    bindData: function () {
        $('#tblData').datagrid({
            loadMsg: "正在加载数据...",
            nowrap: true,
            striped: true,
            border: false,
            height: document.documentElement.clientHeight,
            collapsible: false, //是否可折叠的       
            url: o.url + "?action=1&orderID=" + request("id"),
            idField: 'ID',
            frozenColumns: [[
                        { field: 'ID', checkbox: true },
                        { field: 'CollectAmt', title: '收款金额', align: 'right', width:80, sortable: true, sortName: "CollectAmt" },
                        { field: 'SrcBank', title: '收款银行', sortable: true, sortName: "SrcBank", width: 90 },
                        { field: 'CollectDate', title: '收款日期', sortable: true, sortName: "CollectDate", width: 90, align: 'center' }
            ]],
            columns: [[
                        { field: 'CreateDate', title: "收款日期", width: 90, align: 'center', sortable: true, sortName: "CreateDate" },
                        { field: 'CollectBill', title: '收据编号', sortable: true, sortName: "CollectBill", width: 80 },
                        { field: 'Comment', title: '收款备注', width: 200, sortable: true, sortName: "Comment" },
                        {
                            field: 'CollectStatus', title: '收款状态', width: 60, align: 'center', sortable: true, sortName: "CollectStatus", formatter: function (val, rec) {
                                var s = "";
                                switch (val.toString()) {
                                    case "2":
                                        s = "待确认";
                                        break;
                                    case "3":
                                        s = "<span style='color:red;'>已确认</span>";
                                        break;
                                    case "4":
                                        s = "已取消";
                                        break;
                                }
                                return s;
                            }
                        },
                        { field: 'CreateUserName', title: '收款人', width: 70, align: 'center', sortable: true, sortName: "CreateUserName" },
                        { field: 'Auditor', title: "确认人", width: 70, align: 'center', sortable: true, sortName: "Auditor" },
                        { field: 'AuditDate', title: '确认日期', sortable: true, sortName: "AuditDate", width: 90, align: 'center' } 

            ]],
            rowStyler: function (rowIndex, rec) {
                if (rec.CollectStatus == "4")
                    return "color:red;";
            },
            singleSelect: true, //是否单选 
            pagination: true, //分页控件  
            rownumbers: true, //行号  
            pageSize: 20,
            toolbar: "#toolbar"
        });

        $(window).resize(function () {
            $('#tblData').datagrid('resize');
        });
    }
};