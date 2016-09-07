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
    },
    serverUrl: "Service/OmArea.ashx?rnd=" + getRand(),
    fnBindData: function () {
        $('#tblData').treegrid({
            title: '行政区域管理',
            loadMsg: "正在加载数据...",
            nowrap: true,
            iconCls: 'icon-reload',
            url: t.serverUrl + '&action=1',
            treeField: 'AreaName',
            idField: 'ID',
            rownumbers: true, //行号 
            toolbar: "#toolbar",
            columns: [[
                { field: 'AreaName', title: '区域名称', width: 350 },  
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
        var u = "/Module/Om/OmAreaEdit.aspx?id=" + id;
        return openWindow({ "title": "编辑区域", "width":500, "height": 240, "url": u }, function () {
            $('#tblData').treegrid("reload");
        });
    },
    fnAddSubNode: function (pid) {
        var u = "/Module/Om/OmAreaEdit.aspx?pid=" + pid;
        return openWindow({ "title": "编辑区域", "width": 500, "height": 240, "url": u }, function () {
            $('#tblData').treegrid("reload");
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
                Aotice("删除失败");
            }
        })
    } 
};



