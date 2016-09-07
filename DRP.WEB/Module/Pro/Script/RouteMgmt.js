$(function () {
    t.init();
});

var t = {
    init: function () {
        t.fnBindData();
        $("#btnQuery").click(function () {
            t.fnQuery();
        });
        $("#btnAdd").click(function () {
            t.fnEdit('');
        });
    },
    serverUrl: "Service/RouteMrg.ashx?rnd=" + getRand(),
    fnBindData: function () {
        $('#tblData').datagrid({
            loadMsg: "正在加载数据...",
            height: document.documentElement.clientHeight - 32,
            nowrap: true,
            border: 0,
            iconCls: 'icon-reload',
            url: t.serverUrl + '&action=1',
            idField: 'ID',
            rownumbers: true, //行号 
            toolbar: "#toolbar",
            frozenColumns: [[
                { field: 'RouteNo', title: '线路编号', width: 90, sortable: true, sortName: "RouteNo", align: 'center' },
                { field: 'RouteType', title: '线路类型', width: 90, sortable: true, sortName: "RouteType", align: 'center' },
                {
                    field: 'RouteName', title: '线路名称', width: 350, sortable: true, sortName: "RouteName", formatter: function (val, rec) {
                        return "<a href='RouteInfo.aspx?id=" + rec.ID + "' target='_blank'>" + val + "</a>";
                    }
                }
            ]],
            columns: [[
                { field: 'ScheduleDays', title: '行程天数', width: 60, sortable: true, sortName: "ScheduleDays", align: 'center' },
                { field: 'Destination', title: '目的地', width: 90, sortable: true, sortName: "Destination" },
                {
                    field: 'TourCount', title: '团次数', width: 80, align: 'center', sortable: true, sortName: "TourCount", formatter: function (val, rec) {
                        return "<a href='javascript:;' onclick=\"t.fnTourList('" + rec.ID + "')\" title='查看班次列表'>" + val + "</a>";
                    }
                },
                { field: 'CreateUserName', title: '创建人', width: 80, sortable: true, sortName: "CreateUserName" },
                {
                    field: 'Opt', title: "操作", width: 160, align: 'center', formatter: function (val, rec) {
                        var btnAdd = "<a href=\"javascript:;\" onclick=\"t.fnEdit('" + rec.ID + "')\">编辑</a>";
                        var btnCopy = "<a href=\"javascript:;\" onclick=\"t.fnCopy('" + rec.ID + "')\">复制</a>";
                        var btnTour = "<a href=\"javascript:;\" onclick=\"t.fnOpenTour('" + rec.ID + "')\">开班</a>";
                        var btnDel = "<a href=\"javascript:;\" onclick=\"t.fnDeleteNode('" + rec.ID + "','" + rec.TourCount + "')\">删除</a>";
                        return btnAdd + " | " + btnCopy + " | " + btnTour + " | " + btnDel;
                    }
                }
            ]],
            singleSelect: true, //是否单选 
            pagination: true, //分页控件    
            pageSize: 20
        });

        $(window).resize(function () {
            $('#tblData').datagrid('resize');
        });
    },
    fnQuery: function () {
        var routeTypeID = $("#RouteType option:selected").val();
        var routeNo = $("#RouteNo").val();
        var routeName = $("#RouteName").val();
        $('#tblData').datagrid("reload", { "routeTypeID": routeTypeID, "RouteNo": routeNo, "RouteName": routeName });
    },
    fnEdit: function (id) {
        var title = "线路信息维护";
        var url = "RouteEdit.aspx?id=" + id;
        addTab(title, url, function () { t.fnBindData(); });
    },
    fnCopy: function (id) {
        var title = "复制线路信息";
        var url = "RouteEdit.aspx?xType=copy&id=" + id;
        addTab(title, url, function () { t.fnBindData(); });
    },
    fnOpenTour: function (routeID) {
        var title = "开班发团";
        var url = "OpenTour.aspx?id=" + routeID;
        addTab(title, url, function () { t.fnBindData(); });
    },
    fnTourList: function (routeID) { //查询团次列表
        var title = "班次查询";
        var url = "TourList.aspx?id=" + routeID;
        addTab(title, url, function () { t.fnBindData(); });
    },
    fnDeleteNode: function (id, tourCount) {
        if (!confirm("确定要删除吗")) return;
        if (tourCount > 0) {
            Alert("线路下有团次不能删除");
            return false;
        } else {
            var url = t.serverUrl + "&action=3&id=" + id;
            dataService.ajaxGet(url, function (data) {
                switch (data) {
                    case "0":
                        Alert("删除失败");
                        break;
                    case "1":
                        Notice("删除成功");
                        $('#tblData').datagrid("reload");
                        break;
                }
            })
        }
    }
};

