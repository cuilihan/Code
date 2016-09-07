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
    serverUrl: "Service/Role.ashx?rnd=" + getRand(),
    fnBindData: function () {
        $('#tblData').datagrid({
            title: '角色管理',
            loadMsg: "正在加载数据...",
            height: document.documentElement.clientHeight,
            nowrap: true,
            iconCls: 'icon-reload',
            url: t.serverUrl + '&action=1',
            idField: 'ID',
            rownumbers: true, //行号 
            toolbar: "#toolbar",
            columns: [[
                { field: 'RoleName', title: '角色名称', width: 150, sortable: true, sortName: "RoleName" },
                { field: 'Comment', title: '备注说明', width: 500 },
                { field: 'CreateUserName', title: '创建人', width: 90, align: "center" },
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
        var u = "/Module/Sys/RoleEdit.aspx?id=" + id;
        return openWindow({ "title": "编辑角色信息", "width": "770", "height": "470", "url": u }, function () {
            $('#tblData').datagrid("reload");
        });
    },
    fnDeleteNode: function (id) {
        if (!confirm("确定要删除吗")) return;
        var url = t.serverUrl + "&action=5&id=" + id;
        dataService.ajaxGet(url, function (data) {
            if (data == "1") {
                Notice("删除成功");
                t.fnBindData();
            }
            else {
                Notice("删除失败");
            }
        })
    }
};

