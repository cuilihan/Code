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

        res.fnInitRouteType(); //加载区域
    },
    serverUrl: "Service/Hotel.ashx?rnd=" + getRand(),
    fnBindData: function () {
        $('#tblData').datagrid({
            loadMsg: "正在加载数据...",
            height: document.documentElement.clientHeight - 32,
            border: 0,
            nowrap: true,
            iconCls: 'icon-reload',
            url: t.serverUrl + '&action=1',
            idField: 'ID',
            rownumbers: true, //行号 
            toolbar: "#toolbar",
            frozenColumns: [[
                { field: 'ID', checkbox: true },
                { field: 'Destination', title: '区域', width: 70, sortable: true, sortName: "Destination", align: 'center' },
                {
                    field: 'Name', title: '酒店名称', width: 180, sortable: true, sortName: "Name", formatter: function (val, rec) {
                        return "<a href='javascript:;' onclick=\"t.fnInfo('" + rec.ID + "')\">" + val + "</a>";
                    }
                },
                { field: 'Price', title: '参考价格', width: 120, sortable: true, sortName: "Price" }
            ]],
            columns: [[
                { field: 'StarLv', title: '酒店星级', width: 120, sortable: true, sortName: "StarLv" },
                { field: 'Contact', title: '负责人', width: 80, sortable: true, sortName: "Contact" },
                { field: 'Mobile', title: '手机号', width: 90, sortable: true, sortName: "Mobile" },
                { field: 'TradeNum', title: '合作次数', width: 70, sortable: true, sortName: "TradeNum", align: 'right' },
                {
                    field: 'TradeAmt', title: '交易金额', width: 70, sortable: true, sortName: "TradeAmt", align: 'right', formatter: function (val, rec) {
                        return "<a href='javascript:;' onclick=\"t.fnTradeOrderQry('" + rec.ID + "','" + rec.Name + "')\">" + val + "</a>";
                    }
                },
                {
                    field: 'VisitorNum', title: '游客人数', width: 70, sortable: true, align: 'right', sortName: "TradeAdultNum", formatter: function (val, rec) {
                        return rec.TradeAdultNum + "+<sup style='color:red;'>" + rec.TradeChildNum + "</sup>"
                    }
                },
                {
                    field: 'IsEnable', title: '状态', width: 50, align: 'center', sortable: true, sortName: "IsEnable", formatter: function (val, rec) {
                        return val ? "启用" : "<span style='color:red;'>禁用</span>";
                    }
                },
                {
                    field: 'Opt', title: "操作", width: 70, align: 'center', formatter: function (val, rec) {
                        return "<a href=\"javascript:;\" onclick=\"t.fnEdit('" + rec.ID + "')\">编辑</a>";
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
        var routeTypeID = $("#RouteTypeID").combobox("getValue");
        var destinationID = $("#DestinationID").combotree("getValue");
        var key = $("#Name").val();
        $('#tblData').datagrid("reload", { "routeTypeID": routeTypeID, "destinationID": destinationID, "key": key });
    },
    fnTradeOrderQry: function (id, name) {
        var title = "【" + name + "】订单查询";
        var url = "TradeOrder.aspx?id=" + id;
        addTab(title, url, function () { }); 
    },
    fnEdit: function (id) {
        var title = "酒店维护";
        var url = "HotelEdit.aspx?id=" + id;
        addTab(title, url, function () { t.fnBindData(); });
    },
    fnInfo: function (id) {
        var title = "查看酒店信息";
        var url = "HotelInfo.aspx?id=" + id;
        addTab(title, url, function () { });
    },
    fnDelete: function () {
        var rowID = getDataGridSelectedRow("tblData");
        if (rowID == "") {
            Alert("请选择酒店");
            return false;
        }
        if (!confirm("确定要删除吗")) return;
        var url = t.serverUrl + "&action=3&id=" + rowID;
        dataService.ajaxGet(url, function (data) {
            if (data == "1") {
                Notice("删除成功");
                t.fnBindData();
            }
            else {
                Alert("保存失败");
            }
        })
    }
};

