//已付款明细

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
    serverURL: "Service/PaidItem.ashx",
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
                        { field: 'Amount', title: '付款金额', sortable: true, sortName: "Amount", width: 80, align: 'right' },
                        { field: 'PayDate', title: "付款日期", width: 80, sortable: true, sortName: "PayDate", align: 'right' }
            ]],
            columns: [[
                        { field: 'Comment', title: '付款备注', width: 200},
                        { field: 'OrderName', title: '订单名称', width: 240, sortable: true, sortName: "OrderName" },
                        { field: 'TourDate', title: '出团日期', width: 90, align: 'center', sortable: true, sortName: "TourDate" },
                        { field: 'CreateUserName', title: '付款人', width: 90, align: 'center', sortable: true, sortName: "CreateUserName" },
                        { field: 'CreateDate', title: '操作日期', width: 90, align: 'center', sortable: true, sortName: "CreateDate" },
                        {
                            field: 'Opt', title: '操作', width: 50, align: 'center', formatter: function (val, rec) {
                                console.log(rec)
                                var cancel = "<a href='javascript:;' title='删除付款' onclick=\"p.fnDelete('" + rec.ID + "')\" >删除</a>";

                                var sb = [];
                                sb.push(cancel);
                                return sb.join();
                            }
                        }
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
        $("#tblData").datagrid("clearSelections");
    },
    fnDelete: function (ID) {
        if (confirm("确定要删除付款吗")) {
            var u = p.serverURL + "?id=" + ID + "&action=2&r=" + getRand();
            dataService.ajaxGet(u, function (data) {
                var msg = data == "1" ? "操作成功" : "操作失败";
                Notice(msg);
                if (data == "1") {
                    $('#tblData').datagrid("reload");
                }
            });
        }
    }
}; 