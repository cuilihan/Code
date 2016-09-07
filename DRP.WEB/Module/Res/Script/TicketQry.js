//票务机构

$(function () {
    t.init();
});

var t = {
    init: function () {
        t.fnBindData();
        $("#btnQuery").click(function () {
            t.fnQuery();
        }); 
        res.fnInitRouteType(); //加载区域
    },
    serverUrl: "Service/TicketAgency.ashx?xType=1&rnd=" + getRand(),
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
                {
                    field: 'Name', title: '票务机构名称', width: 180, sortable: true, sortName: "Name", formatter: function (val, rec) {
                        return "<a href='javascript:;' onclick=\"t.fnInfo('" + rec.ID + "')\">" + val + "</a>";
                    }
                },
                { field: 'TicketType', title: '票务类型', width: 120, sortable: true, sortName: "TicketType" }
            ]],  
            columns: [[ 
                { field: 'Contact', title: '负责人', width: 80, sortable: true, sortName: "Contact" },
                { field: 'Mobile', title: '手机号', width: 90, sortable: true, sortName: "Mobile" },
                { field: 'Phone', title: '电话', width: 90, sortable: true, sortName: "Mobile" },
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
        var key = $("#Name").val();
        var ticketType = $("#TicketType");
        $('#tblData').datagrid("reload", { "ticketType": ticketType, "key": key });
    }, 
    fnInfo: function (id) {
        var title = "查看票务信息";
        var url = "TicketInfo.aspx?id=" + id;
        addTab(title, url, function () {  });
    } 
};

