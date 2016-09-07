$(function () {
    t.init();
});

var t = {
    init: function () {
        t.fnBindData();
        $("#btnAdd").click(function () {
            t.fnEdit('');
        });
        $("#btnDelete").click(function () {
            t.fnDeleteNode();
        });
        $("#RouteTypeID").change(function () {
            t.fnQueryData();
        });
    },
    serverUrl: "Service/Venue.ashx?rnd=" + getRand(),
    fnBindData: function () {
        $('#tblData').datagrid({
            title: '集合地点设置',
            loadMsg: "正在加载数据...",
            height: document.documentElement.clientHeight,
            nowrap: true,
            iconCls: 'icon-reload',
            url: t.serverUrl + '&action=1',
            idField: 'ID',
            rownumbers: true, //行号 
            toolbar: "#toolbar",
            columns: [[
                { field: 'ID', checkbox: true },
                { field: 'Name', title: '集合地名称', width: 180 },
                { field: 'Departure', title: '出发地', width: 70, align: 'center' },
                { field: 'RouteType', title: '线路类型', width: 160 },
                { field: 'PickAmt', title: '接加价', width: 60, align: 'center' },
                { field: 'SendAmt', title: '送加价', width: 60, align: 'center' },
                { field: 'MeetTime', title: '集合时间', width: 120, align: "center" },
                { field: 'CreateUserName', title: '创建人', width: 90, align: "center" },
                {
                    field: 'Opt', title: "操作", width: 60, align: 'center', formatter: function (val, rec) {
                        return "<a href=\"javascript:;\" onclick=\"t.fnEdit('" + rec.ID + "')\">编辑</a>";
                    }
                }
            ]],
            singleSelect: false, //是否单选 
            pagination: false //分页控件     
        });

        $(window).resize(function () {
            $('#tblData').datagrid('resize');
        });
    },
    fnEdit: function (id) {
        var u = "/Module/Pro/VenueEdit.aspx?id=" + id;
        return openWindow({ "title": "集合地点设置", "width": "550", "height": "400", "url": u }, function () {
            $('#tblData').datagrid("reload");
        });
    },
    fnQueryData: function () { //查询
        var routeTypeID = $("#RouteTypeID option:selected").val();
        $('#tblData').datagrid("reload", { "routeTypeID": routeTypeID });
    },
    fnDeleteNode: function (id) {
        var rowID = getDataGridSelectedRow("tblData");
        if (rowID == "") {
            Alert("请选择项目后再删除");
            return false;
        }
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

