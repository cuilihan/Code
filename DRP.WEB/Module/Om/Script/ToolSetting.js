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
    serverUrl: "Service/ToolSetting.ashx?rnd=" + getRand(),
    fnBindData: function () {
        $('#tblData').datagrid({
            title: '推送工具设置',
            loadMsg: "正在加载数据...",
            height: document.documentElement.clientHeight,
            nowrap: true,
            iconCls: 'icon-reload',
            url: t.serverUrl + '&action=1',
            idField: 'ID',
            rownumbers: true, //行号 
            toolbar: "#toolbar",
            columns: [[
                { field: 'Name', title: '工具名称', width: 180, sortable: true, sortName: "Name" },
                { field: 'URL', title: '链接地址', width: 180, sortable: true, sortName: "URL" },
                { field: 'Comment', title: '描述', width: 350, sortable: true, sortName: "Comment" },
                {
                    field: 'Opt', title: "操作", width: 100, align: 'center', formatter: function (val, rec) {
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
    fnQuery: function () {
        t.fnBindData();
    },
    fnEdit: function (id) {
        var u = "/Module/Om/ToolSettingEdit.aspx?id=" + id;
        return openWindow({ "title": "工具设置", "width": 600, "height": 370, "url": u }, function () {
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

