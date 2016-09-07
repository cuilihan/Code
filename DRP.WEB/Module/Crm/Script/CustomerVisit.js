$(function () {
    t.init();
});

var t = {
    init: function () {
        t.fnBindData();
        $("#btnDelete").click(function () {
            return t.fnDeleteNode();
        });
    },
    serverUrl: "Service/CustomerVisit.ashx?rnd=" + getRand(),
    fnBindData: function () {
        $('#tblData').datagrid({
            loadMsg: "正在加载数据...",
            height: document.documentElement.clientHeight-10,
            nowrap: true,
            iconCls: 'icon-reload',
            url: t.serverUrl + '&action=1&customerID=' + request("customerID"),
            idField: 'ID',
            rownumbers: true, //行号 
            toolbar: "#toolbar",
            frozenColumns: [[
                { field: 'ID', checkbox: true },
                { field: 'Contact', title: '客户名称', width: 150, sortable: true, sortName: "Contact", align: 'center' },
                { field: 'ItemType', title: '线索类型', width: 90, sortable: true, sortName: "ItemType", align: 'center' },
                { field: 'ItemName', title: '线索名称', width: 100, sortable: true, sortName: "ItemName", align: 'center' }
            ]],
            columns: [[
                { field: 'Comment', title: '备注', width: 280, sortable: true, sortName: "Comment" },
                {
                    field: 'TradeDate', title: '预计成单日期', width: 90, sortable: true, sortName: "TradeDate", formatter: function (val, rec) {
                        if (val != "") {
                            var arr = val.split(' ');
                            if (arr.length > 0)
                                return arr[0];
                        }
                    }
                },
                { field: 'CreateUserName', title: '创建人', width: 80, sortable: true, align: 'center', sortName: "CreateUserID" },
                { field: 'CreateDate', title: '创建日期', width: 120, sortable: true,  sortName: "CreateDate" }
            ]],
            singleSelect: false, //是否单选 
            pagination: true, //分页控件    
            pageSize: 20
        });

        $(window).resize(function () {
            $('#tblData').datagrid('resize');
        });
    },
    fnEdit: function (id) {
        var title = "客户信息维护";
        var url = "CustomerEdit.aspx?id=" + id;
        addTab(title, url, function () { t.fnBindData(); });
    },
    fnCustomerInfo: function (id) {
        var title = "查看客户信息";
        var url = "CustomerInfo.aspx?id=" + id;
        addTab(title, url, function () { });
    },
    fnDeleteNode: function () {
        var id = getDataGridSelectedRow("tblData");
        if (id == "") {
            Alert("请选择客户");
            return false;
        }
        if (!confirm("确定要删除吗")) return;
        var url = t.serverUrl + "&action=2&id=" + id;
        dataService.ajaxGet(url, function (data) {
            if (data == "1") {
                Notice("删除成功");
                $('#tblData').datagrid("reload");
            }
            else {
                Notice("删除失败");
            }
        })
    }
};

