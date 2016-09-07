$(function () {
    t.init();
});

var t = {
    init: function () {
        t.fnBindData();
        $("#btnAdd").click(function () {
            t.fnEdit('');
        });
        $("#btnCollapse").click(function () {
            $('#tblData').treegrid("collapseAll");
        });
        $("#btnExpand").click(function () {
            $('#tblData').treegrid("expandAll");
        });
        $("#btnReload").click(function () {
            $('#tblData').treegrid("reload");
        });
    },
    serverUrl: "Service/Navigate.ashx?rnd=" + getRand(),
    fnBindData: function () {
        $('#tblData').treegrid({
            title: '菜单管理',
            loadMsg: "正在加载数据...",
            nowrap: true,
            iconCls: 'icon-reload',
            height: document.documentElement.clientHeight - 5,
            url: t.serverUrl + '&action=1',
            treeField: 'NavName',
            idField: 'ID',
            rownumbers: true, //行号 
            toolbar: "#toolbar",
            columns: [[
                { field: 'NavName', title: '菜单名称', width: 200 },
                { field: 'PageID', title: '页面ID', width: 80, align: "center" },
                { field: 'NavUrl', title: '链接地址', width: 200 },
                { field: 'OrderIndex',align:'center', title: '排序号', width: 50 },
                { field: 'NavCls', title: '样式名称', width: 70, align: "center" },
                {
                    field: 'IsVisual', title: '是否显示', width: 60, align: "center", formatter: function (val, rec) {
                        return val == "True" ? "√" : "<span style='color:red;'>×</span>";
                    }
                },
                { field: 'OrderIndex', title: '排序', width: 50, align: 'center' },
                {
                    field: 'Opt', title: "操作", width: 180, align: 'center', formatter: function (val, rec) {
                        var btnEdit = "<a href=\"javascript:;\" onclick=\"t.fnEdit('" + rec.ID + "')\">编辑</a>";
                        var btnAddSub = "<a href=\"javascript:;\" onclick=\"t.fnAddSubNode('" + rec.ID + "')\">新增子菜单</a>";
                        var btnDel = "<a href=\"javascript:;\" onclick=\"t.fnDeleteNode('" + rec.ID + "')\">删除</a>";
                        return btnEdit + "&nbsp;| " + btnAddSub + "&nbsp;| " + btnDel;
                    }
                }
            ]],
            onLoadSuccess: function (row, data) {
                $.each(data, function (i, val) { $('#tblData').treegrid('collapseAll', data[i].id) });
            }
        });
    },
    fnEdit: function (id) {
        var u = "/Module/Om/NavigateEdit.aspx?id=" + id;
        return openWindow({ "title": "编辑导航菜单", "width": 550, "height": 420, "url": u }, function () {
         //   $('#tblData').treegrid("reload");
        });
    },
    fnAddSubNode: function (pid) {
        var u = "/Module/Om/NavigateEdit.aspx?pid=" + pid;
        return openWindow({ "title": "增加导航菜单", "width": 550, "height": 420, "url": u }, function () {
           // $('#tblData').treegrid("reload");
        });
    },
    fnDeleteNode: function (id) {
        if (!confirm("确定要删除吗")) return;
        var url = t.serverUrl + "&action=2&id=" + id;
        dataService.ajaxGet(url, function (data) {
            if (data == "1") {
                Notice("删除成功");
                $('#tblData').treegrid("reload");
            }
            else { 
                alert("删除失败");
            }
        })
    }
};



