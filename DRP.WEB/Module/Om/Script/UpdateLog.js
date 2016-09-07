$(function () {
    t.init();
});

var t = {
    init: function () {
        t.fnBindData();
        $("#btnAdd").click(function () {
            t.fnEdit('');
        });
        $("#btnQuery").click(function () {
            t.fnQuery();
        });
    },
    serverUrl: "Service/UpdateLog.ashx?rnd=" + getRand(),
    fnBindData: function () {
        $('#tblData').datagrid({
            title: '更新日志',
            loadMsg: "正在加载数据...",
            height: document.documentElement.clientHeight,
            nowrap: true,
            iconCls: 'icon-reload',
            url: t.serverUrl + '&action=1',
            idField: 'ID',
            rownumbers: true, //行号 
            toolbar: "#toolbar",
            columns: [[
                { field: 'Summary', title: '日志内容', width: 470, sortable: true, sortName: "Summary" },
                { field: 'CreateDate', title: '更新日期', width: 90, sortable: true, sortName: "CreateDate" },
                { field: 'CreateUserName', title: '更新人', width: 70, sortable: true, sortName: "CreateUserName" },
                {
                    field: 'Opt', title: "操作", width: 80, align: 'center', formatter: function (val, rec) {
                        var btnEdit = "<a href=\"javascript:;\" onclick=\"t.fnEdit('" + rec.ID + "')\">编辑</a>";
                        var btnDel = "<a href=\"javascript:;\" onclick=\"t.fnDeleteNode('" + rec.ID + "')\">删除</a>";
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
    fnEdit: function (id) {
        var u = "/Module/Om/UpdateLogEdit.aspx?id=" + id;
        return openWindow({ "title": "编辑日志信息", "width": 600, "height": 400, "url": u }, function () {
            $('#tblData').datagrid("reload");
        });
    }, 
    fnDeleteNode: function (id) {
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

