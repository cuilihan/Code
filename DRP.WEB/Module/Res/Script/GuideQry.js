$(function () {
    t.init();
});

var t = {
    init: function () {
        t.fnBindData();
        $("#btnQuery").click(function () {
            t.fnQuery();
        });
    },
    serverUrl: "Service/Guide.ashx?xType=1&rnd=" + getRand(),
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
                { field: 'DepartureName', title: '地区', width: 60, sortable: true, sortName: "DepartureName", align: 'center' },
                {
                    field: 'Name', title: '姓名', width: 90, sortable: true, sortName: "Name", align: 'center', formatter: function (val, rec) {
                        return "<a href='javascript:;' onclick=\"t.fnInfo('" + rec.ID + "')\">" + val + "</a>";
                    }
                },
                { field: 'Mobile', title: '手机号', width: 90, sortable: true, sortName: "Mobile", align: 'center' }
            ]],
            columns: [[
                { field: 'Sex', title: '性别', width: 40, sortable: true, sortName: "Sex", align: 'center' },
                {
                    field: 'IsIDCard', title: '导游证', align: 'center', width: 50, sortable: true, sortName: "IsIDCard", formatter: function (val, rec) {
                        return val ? "√" : "";
                    }
                },
                {
                    field: 'IsLeaderCard', title: '领队证', align: 'center', width: 50, sortable: true, sortName: "IsLeaderCard", formatter: function (val, rec) {
                        return val ? "√" : "";
                    }
                },
                { field: 'GuideLevel', title: '导游等级', align: 'center', width: 90, sortable: true, sortName: "GuideLevel" },
                { field: 'Language', title: '语种', width: 120, align: 'center', sortable: true, sortName: "Language" },
                { field: 'TradeNum', title: '合作次数', width: 70, sortable: true, sortName: "TradeNum", align: 'right' },
                {
                  field: 'TradeAmt', title: '交易金额', width: 70, sortable: true, sortName: "TradeAmt", align: 'right', formatter: function (val, rec) {
                      return "<a href='javascript:;' onclick=\"t.fnTradeOrder('" + rec.ID + "','" + rec.Name + "')\">" + val + "</a>";
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
    fnTradeOrder: function (id, name) {
        var title = "【" + name + "】订单查询";
        var url = "TradeOrder.aspx?id=" + id;
        addTab(title, url);
    },
    fnQuery: function () {
        var DepartureID = $("#DepartureID").val();
        var key = $("#Name").val();
        $('#tblData').datagrid("reload", { "DepartureID": DepartureID, "key": key });
    },
    fnInfo: function (id) {
        var url = "/Module/Res/GuideInfo.aspx?id=" + id;
        return openWindow({ "title": "查看导游信息", "width": 650, "height": 420, "url": url }, function () {
        });
    }
};

