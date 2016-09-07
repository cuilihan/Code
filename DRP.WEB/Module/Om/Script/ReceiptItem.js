$(function () {
    t.init();
});

var t = {
    init: function () {
        t.fnBindData();
        $("#btnAdd").click(function () {
            t.fnReceipt('');
        });
    },
    serverUrl: "Service/OrgInfo.ashx?rnd=" + getRand() + "&orgID=" + request("orgID"),
    fnBindData: function () {
        $('#tblData').datagrid({
            loadMsg: "正在加载数据...",
            height: document.documentElement.clientHeight,
            nowrap: true,
            iconCls: 'icon-reload',
            url: t.serverUrl + '&action=6',
            idField: 'ID',
            rownumbers: true, //行号 
            toolbar: "#toolbar",
            columns: [[
                 { field: 'PaidAmt', title: '收款金额', align: 'right', width: 90, sortable: true, sortName: "PaidAmt" },
                 { field: 'UserCount', title: '用户数', align: 'center', width: 50, sortable: true, sortName: "UserCount" },
                 {
                     field: 'eDate', title: '授权日期范围', align: 'center', width: 160, sortable: true, sortName: "eDate", formatter: function (val, rec) {
                         return rec.sDate + " / <span style='color:red;'>" + rec.eDate + "</span>";
                     }
                 },
                { field: 'Receiver', title: '收款人', align: 'center', width: 90, sortable: true, sortName: "Receiver" },
                { field: 'ReceiveDate', title: '收款日期', align: 'center', width: 120, sortable: true, sortName: "ReceiveDate" },
                { field: 'Comment', title: '备注', width: 350 },
                {
                    field: 'Opt', title: '操作', width: 90, align: 'center', formatter: function (val, rec) {
                        var btnEdit = "<a href=\"javascript:;\" onclick=\"t.fnReceipt('" + rec.ID + "')\">编辑</a>";
                        var btnDel = "<a href=\"javascript:;\" onclick=\"t.fnDelete('" + rec.ID + "')\">删除</a>";
                        return btnEdit + " | " + btnDel;
                    }
                }
            ]],
            singleSelect: true, //是否单选 
            pagination: false //分页控件     
        });

        $(window).resize(function () {
            $('#tblData').datagrid('resize');
        });
    },
    fnReceipt: function (id) { //收款
        var u = "/Module/Om/Receipt.aspx?id=" + id + "&orgID=" + request("orgID");
        return openWindow({ "title": "收款", "width": "480", "height": "400", "url": u }, function () {
            $('#tblData').datagrid("reload");
        });
    },
    fnDelete: function (id) {
        if (!confirm('确定要删除收款明细吗'))
            return;
        var u = "Service/OrgInfo.ashx?rnd=" + getRand() + "&action=7&id=" + id;
        dataService.ajaxGet(u, function (data) {
            if (data == "1") {
                Notice("删除收款明细成功");
                $('#tblData').datagrid("reload");
            } else {
                Alert("删除数据失败");
            }
        });
    }
};

