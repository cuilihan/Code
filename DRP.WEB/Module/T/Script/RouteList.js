$(function () {
    t.init();
});

var t = {
    init: function () {
        t.fnInitDestinationTree();
        t.fnBindData();
        $("#btnQuery").click(function () {
            t.fnQuery();
        });
        $("#btnAdd").click(function () {
            t.fnRouteEdit('');
        });
    },
    serverUrl: "Service/RouteTemplate.ashx?rnd=" + getRand(),
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
                {
                    field: 'RouteName', title: '线路名称', width: 200, sortable: true, sortName: "RouteName", formatter: function (val, rec) {
                        return "<a href='RouteInfo.aspx?id=" + rec.ID + "' target='_blank'>" + val + "</a>";
                    }
                }
            ]],
            columns: [[
                { field: 'RouteType', title: '线路类型', width: 80, sortable: true, sortName: "RouteType", align: 'center' },
                { field: 'Destination', title: '目的地', width: 80, sortable: true, sortName: "Destination", align: 'center' },
                { field: 'Days', title: '行程天数', width: 60, sortable: true, sortName: "Days", align: 'center' },
                { field: 'VisitorNum', title: '人数', width: 60, sortable: true, sortName: "VisitorNum", align: 'center' },
                { field: 'Stay', title: '住宿标准', width: 80, sortable: true, sortName: "Stay", align: 'center' },
                { field: 'AvgPrice', title: '人均价格', width: 60, sortable: true, sortName: "AvgPrice", align: 'center' },
                {
                    field: 'cDate', title: '最后更新日期', width: 80, sortable: true, sortName: "CreateDate", align: 'center', formatter: function (val, rec) {
                        return val;
                    }
                },
                { field: 'CreateUserName', title: '创建人', width: 80, sortable: true, sortName: "CreateUserName" },
                {
                    field: 'Opt', title: "操作", width: 120, align: 'center', formatter: function (val, rec) {
                        var btnAdd = "<a href=\"javascript:;\" onclick=\"t.fnRouteEdit('" + rec.ID + "')\">编辑</a>";
                        var btnCopy = "<a href=\"javascript:;\" onclick=\"t.fnCopy('" + rec.ID + "')\">复制</a>";
                        var btnDel = "<a href=\"javascript:;\" onclick=\"t.fnDeleteNode('" + rec.ID + "')\">删除</a>";
                        return btnCopy + " | " + btnAdd + " | " + btnDel;
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
        var treeObj = $.fn.zTree.getZTreeObj("treeData");
        var nodes = treeObj.getSelectedNodes();
        var destinationID = "";
        if (nodes.length > 0)
            destinationID = nodes[0].id;

        var routeNo = $("#txtRouteNo").val();
        var routeName = $("#txtRouteName").val();
        var days = $("#Days option:selected").val(); 
        var avgPrice = $("#AvgPrice option:selected").val();
        var visitorNum = $("#VisitorNum option:selected").val();

        $('#tblData').datagrid("reload", { "DestinationID": destinationID, "RouteNo": routeNo, "RouteName": routeName, "Days": days, "AvgPrice": avgPrice, "VisitorNum": visitorNum });
    },
    fnRouteEdit: function (id) {
        var title = "报价单维护";
        var url = "RouteEdit.aspx?id=" + id;
        addTab(title, url, function () { t.fnBindData(); });
    },
    fnCopy: function (id) {
        var title = "复制报价单";
        var url = "RouteEdit.aspx?xType=copy&id=" + id;
        addTab(title, url, function () { t.fnBindData(); });
    },
    fnDeleteNode: function (id) {
        if (!confirm("确定要删除吗")) return;
        var url = t.serverUrl + "&action=3&id=" + id;
        dataService.ajaxGet(url, function (data) {
            if (data == "1") {
                Notice("删除成功");
                $('#tblData').datagrid("reload");
            }
            else {
                Alert("删除失败");
            }
        })
    },
    fnInitDestinationTree: function () { //创建目的地树
        var url = t.serverUrl + "&action=4";
        dataService.ajaxGet(url, function (data) {
            if (data != "") {
                var setting = {
                    check: {
                        enable: true
                    },
                    data: {
                        simpleData: {
                            enable: true
                        }
                    },
                    callback: {
                        onClick: zTreeOnClick
                    }
                };
                $.fn.zTree.init($("#treeData"), setting, eval(data));
            }
        });
    }
};


function zTreeOnClick(event, treeId, treeNode) {
    t.fnQuery();
};

