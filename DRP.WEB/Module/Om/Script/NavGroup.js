$(function () {
    t.init();
});

var t = {
    init: function () {
        t.fnBindData();
        $("#btnAdd").click(function () {
            t.fnEdit('');
        });
    },
    serverUrl: "Service/NavGroup.ashx?rnd=" + getRand(),
    fnBindData: function () {
        $('#tblData').datagrid({
            title: '导航组管理',
            loadMsg: "正在加载数据...",
            height: document.documentElement.clientHeight,
            nowrap: true,
            iconCls: 'icon-reload',
            url: t.serverUrl + '&action=1',
            idField: 'ID',
            rownumbers: true, //行号 
            toolbar: "#toolbar",
            columns: [[
                { field: 'NavGroup', title: '导航组名称', width: 200, sortable: true, sortName: "NavGroup" },
                { field: 'Comment', title: '备注说明', width: 500 },
                { field: 'CreateUserName', title: '创建人', width: 70, align: "center" },
                {
                    field: 'Opt', title: "操作", width: 90, align: 'center', formatter: function (val, rec) {
                        var btnEdit = "<a href=\"javascript:;\" onclick=\"t.fnEdit('" + rec.ID + "')\">编辑</a>";
                        var btnDel = "<a href=\"javascript:;\" onclick=\"t.fnDeleteNode('" + rec.ID + "')\">删除</a>";
                        return btnEdit + "&nbsp;| " + btnDel;
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
        var u = "/Module/Om/NavGroupEdit.aspx?id=" + id;
        return openWindow({ "title": "编辑导航组信息", "width": "750", "height": "480", "url": u }, function () {
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

