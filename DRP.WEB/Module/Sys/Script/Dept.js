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
    serverUrl: "Service/Department.ashx?rnd=" + getRand(),
    fnBindData: function () {
        $('#tblData').treegrid({
            title: '部门管理',
            loadMsg: "正在加载数据...",
            nowrap: true,
            iconCls: 'icon-reload',
            url: t.serverUrl + '&action=1',
            treeField: 'Name',
            idField: 'ID',
            rownumbers: true, //行号 
            toolbar: "#toolbar",
            columns: [[
                { field: 'Name', title: '菜单名称', width: 250 },
                {
                    field: 'DataStatus', title: '是否启用', width: 60, align: "center", formatter: function (val, rec) {
                        return val== "1" ? "√" : "<span style='color:red;'>×</span>";
                    }
                }, 
                {
                    field: 'Opt', title: "操作", width: 180, align: 'center', formatter: function (val, rec) {
                        var btnEdit = "<a href=\"javascript:;\" onclick=\"t.fnEdit('" + rec.ID + "')\">编辑</a>";
                        var btnAddSub = "<a href=\"javascript:;\" onclick=\"t.fnAddSubNode('" + rec.ID + "')\">新增子部门</a>";
                        var btnDel = "<a href=\"javascript:;\" onclick=\"t.fnDeleteNode('" + rec.ID + "')\">删除</a>";
                        return btnEdit + "&nbsp;| " + btnAddSub + "&nbsp;| " + btnDel;
                    }
                }
            ]] 
        });
    },
    fnEdit: function (id) {
        var u = "/Module/Sys/DeptEdit.aspx?id=" + id;
        return openWindow({ "title": "编辑部门", "width": 500, "height": 280, "url": u }, function () {
            $('#tblData').treegrid("reload");
        });
    },
    fnAddSubNode: function (pid) {
        var u = "/Module/Sys/DeptEdit.aspx?pid=" + pid;
        return openWindow({ "title": "编辑部门", "width": 500, "height": 280, "url": u }, function () {
            $('#tblData').treegrid("reload");
        });
    },
    fnDeleteNode: function (id) {
        if (!confirm("确定要删除吗")) return;
        var url = t.serverUrl + "&action=2&id=" + id;
        dataService.ajaxGet(url, function (data) {
            switch (data) {
                case "1":
                    Notice("删除成功");
                    $('#tblData').treegrid("reload");
                    break;
                case "2":
                    Alert("部门下有用户信息，不能删除");
                    break;
                case "3":
                    Alert("部门下有子部门信息，不能删除");
                    break;
                default:
                    Alert("删除失败");
                    break;
            }
        })
    }
};



