$(function () {
    t.init();
});

var t = {
    init: function () {
        // t.fnBindData();
        $("#btnAdd").click(function () {
            t.fnEdit('');
        });
        $("#btnCollapse").click(function () {
            $('#tblData').treegrid("collapseAll");
        });
        $("#btnExpand").click(function () {
            $('#tblData').treegrid("expandAll");
        });
        t.fnRouteTypeChange();
    },
    serverUrl: "Service/Destination.ashx?rnd=" + getRand(),
    fnBindData: function (routeTypeID) {
        $('#tblData').treegrid({
            title: '目的地管理',
            loadMsg: "正在加载数据...",
            nowrap: true,
            iconCls: 'icon-reload',
            height: document.documentElement.clientHeight - 30,
            url: t.serverUrl + '&action=1&routeTypeID=' + routeTypeID,
            treeField: 'Name',
            idField: 'ID',
            rownumbers: true, //行号 
            toolbar: "#toolbar",
            columns: [[
                { field: 'Name', title: '目的地名称', width: 350 },
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
        var routeTypeID = $("#tabMenu").find(".at").prop("id");
        var u = "/Module/Glo/DestinationEdit.aspx?id=" + id + "&routeTypeID=" + routeTypeID;
        return openWindow({ "title": "目的地维护", "width": 500, "height": 220, "url": u }, function () {
            $('#tblData').treegrid("reload");
        });
    },
    fnAddSubNode: function (pid) {
        var routeTypeID = $("#tabMenu").find(".at").prop("id");
        var u = "/Module/Glo/DestinationEdit.aspx?pid=" + pid + "&routeTypeID=" + routeTypeID;
        return openWindow({ "title": "目的地维护", "width": 500, "height": 220, "url": u }, function () {
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
                if (data == "-1") {
                    Alert("当前目的地下有子节点，不能删除");
                }
                else {
                    Alert("删除失败");
                }
            }
        })
    },
    fnRouteTypeChange: function () { //线路类型切换 
        $("#tabMenu").find("li").click(function () {
            $(this).removeClass("not").addClass("at").siblings("li").removeClass("at").addClass("not");
            var routeTypeID = $(this).prop("id");
            t.fnBindData(routeTypeID);
        });

        var routeTypeID = $("#tabMenu").find(".at").prop("id");
        t.fnBindData(routeTypeID); 
    }
};



