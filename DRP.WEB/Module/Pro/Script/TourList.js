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
        $("#btnDelete").click(function () {
            t.fnDelete();
        });
        $("#btnBatchUpdate").click(function () { //批量修改
            t.fnBatchUpdate();
        });
    },
    serverUrl: "Service/TourInfo.ashx?rnd=" + getRand() + "&routeID=" + request("id"),
    fnBindData: function () {
        $('#tblData').datagrid({
            loadMsg: "正在加载数据...",
            height: document.documentElement.clientHeight,
            nowrap: true,
            border: 0,
            iconCls: 'icon-reload',
            url: t.serverUrl + '&action=2',
            idField: 'ID',
            rownumbers: true, //行号 
            toolbar: "#toolbar",
            frozenColumns: [[
                { field: 'ID', checkbox: true },
                { field: 'TourNo', title: '班次编号', width: 140, sortable: true, sortName: "TourNo", align: 'center' },
                { field: 'TourName', title: '班次名称', width: 200, sortable: true, sortName: "TourName" },
                {
                    field: 'tDate', title: '出团日期', width: 100, align: 'center', sortable: true, sortName: "TourDate", formatter: function (val, rec) {
                        return val + " (<span style='color:blue;'>" + rec.tDW + "</span>)";
                    }
                }
            ]],
            columns: [[
                {
                    field: 'eDate', title: '报名截止日期', width: 100, sortable: true, sortName: "ExpiryDate", align: 'center', formatter: function (val, rec) {
                        return val + " (<span style='color:blue;'>" + rec.eDW + "</span>)";
                    }
                },
                { field: 'DefaultPrice', title: '销售价', width: 60, sortable: true, sortName: "DefaultPrice", align: 'right' },
                { field: 'PlanNum', title: '计划', width: 50, sortable: true, sortName: "PlanNum", align: 'right' },
                { field: 'ClusterNum', title: '成团', width: 50, sortable: true, sortName: "ClusterNum", align: 'right' },
                { field: 'VisitorNum', title: '报名', width: 50, sortable: true, sortName: "VisitorNum", align: 'right' },
                { field: 'SurplusNum', title: '剩余', width: 50, sortable: true, sortName: "SurplusNum", align: 'right' },
                {
                    field: 'TourStatus', title: '状态', width: 50, sortable: true, sortName: "TourStatus", align: 'center', formatter: function (val, rec) {
                        return val == "1" ? "销售" : "<span style='color:red;'>停售</span>";
                    }
                },
                {
                    field: 'Opt', title: "操作", width: 120, align: 'center', formatter: function (val, rec) {
                        var btnEdit = "<a href=\"javascript:;\" onclick=\"t.fnEdit('" + rec.ID + "')\">修改</a>";
                        var btnSeat = "<a href=\"javascript:;\" onclick=\"t.fnSeat('" + rec.ID + "')\">预留座</a>";
                        var status = rec.TourStatus == "1" ? "2" : "1";
                        var a = rec.TourStatus == "1" ? "<span style='color:red;'>停售</span>" : "销售";
                        var btnStatus = "<a href=\"javascript:;\" onclick=\"t.fnChangeStatus('" + rec.ID + "','" + status + "')\">" + a + "</a>";
                        return btnEdit + " | " + btnSeat + " | " + btnStatus;
                    }
                }
            ]],
            singleSelect: false, //是否单选 
            pagination: true, //分页控件    
            pageSize: 20
        });

        $(window).resize(function () {
            $('#tblData').datagrid('resize');
        });
    },
    fnQuery: function () {
        var effectiveDays = $("#IsExpire").is(":checked");
        var sDate = $("#sDate").val();
        var eDate = $("#eDate").val();
        $('#tblData').datagrid("reload", { "sDate": sDate, "eDate": eDate, "effectiveDays": effectiveDays ? "1" : "0" });
    },
    fnEdit: function (id) {
        var u = "/Module/Pro/TourEdit.aspx?id=" + id;
        var h = 500;
        return openWindow({ "title": "团次修改", "width": "750", "height": h, "url": u }, function () {
            $('#tblData').datagrid("reload");
        });
    },
    fnSeat: function (id) {
        var u = "/Module/Pro/OccupySeat.aspx?id=" + id;
        return openWindow({ "title": "预留座位设置", "width": "300", "height": "500", "url": u }, function () {
            $('#tblData').datagrid("reload");
        });
    },
    fnBatchUpdate: function () {
        var ids = getDataGridSelectedRow("tblData");
        if (ids == "") {
            Alert("请选择团次");
            return false;
        }
        var u = "/Module/Pro/TourEdit.aspx?id=" + ids;
        var h = document.documentElement.clientHeight;

        return openWindow({ "title": "团次修改", "width": "750", "height": 500, "url": u }, function () {
            $('#tblData').datagrid("reload");
        });
    },
    fnChangeStatus: function (id, status) { //停止销售 
        if (confirm("确定要操作吗")) {
            var u = "Service/TourInfo.ashx?action=6&tourID=" + id + "&status=" + status + "&r=" + getRand();
            dataService.ajaxGet(u, function (data) {
                if (data == "1") {
                    $('#tblData').datagrid("reload");
                    Notice("操作完成");
                    return;
                } else {
                    Alert("操作失败"); return false;
                }
            });
        }
    },
    fnDelete: function () {
        var id = getDataGridSelectedRow("tblData");
        if (id == "") {
            Alert("请选择班次后再删除");
            return false;
        }
        if (!confirm("确定要删除吗")) return;
        var url = t.serverUrl + "&action=4&id=" + id;
        dataService.ajaxGet(url, function (data) {
            switch (data) {
                case "0":
                    Notice("删除成功");
                    $('#tblData').datagrid("reload");
                    break;
                case "2":
                    Alert("部份团次有订单没有删除");
                    $('#tblData').datagrid("reload");
                    break;
                default:
                    Alert("删除失败");
                    break;
            }
        })
    }
};

