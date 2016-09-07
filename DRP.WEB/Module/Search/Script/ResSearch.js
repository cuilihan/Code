$(function () {
    t.init();
});

var t = {
    init: function () {
        t.fnBindData();
        $("#btnQuery").click(function () {
            t.fnBindData();
        });
        $(".searcher_category").find("a").click(function () {
            var itemType = $(this).attr("itemtype");
            $(this).addClass("search_on").siblings().removeClass("search_on");
            t.fnBindData();
        });
    },
    serverUrl: "Service/Searcher.ashx?action=1&rnd=" + getRand(),
    fnBindData: function () {
        var itemType = $(".search_on").attr("itemtype");
        var key = $("#Name").val();
        var u = t.serverUrl + "&itemType=" + itemType + "&key=" + encodeURI(key);
        $('#tblData').datagrid({
            loadMsg: "正在加载数据...",
            height: document.documentElement.clientHeight - 32,
            border: 0,
            nowrap: true,
            iconCls: 'icon-reload',
            url: u,
            idField: 'ID',
            rownumbers: true, //行号 
            toolbar: "#toolbar",
            frozenColumns: [[
                {
                    field: 'xType', title: '类型', width: 90, sortable: true, sortName: "xType", align: 'center', formatter: function (val, rec) {
                        var a = "";
                        switch (val.toString()) {
                            case "1": a = "供应商"; break;
                            case "2": a = "景点门票"; break;
                            case "3": a = "导游"; break;
                            case "4": a = "酒店"; break;
                            case "5": a = "车队"; break;
                            case "6": a = "签证机关"; break;
                            case "7": a = "保险公司"; break;
                            case "8": a = "购物店"; break;
                            case "9": a = "票务机构"; break;
                            case "10": a = "其他"; break; 
                        }
                        return a;
                    }
                },
                {
                    field: 'Name', title: '名称', width: 250, sortable: true, sortName: "Name", formatter: function (val, rec) {
                        return "<a href='javascript:;' onclick=\"t.fnInfo('" + rec.ID + "')\">" + val + "</a>";
                    }
                }
            ]],
            columns: [[
                { field: 'Contact', title: '负责人', width: 80, sortable: true, sortName: "Contact" },
                { field: 'Mobile', title: '手机号', width: 110, sortable: true, sortName: "Mobile" },
                { field: 'TradeNum', title: '合作次数', width: 70, sortable: true, sortName: "TradeNum", align: 'right' },
                {
                    field: 'TradeAmt', title: '交易金额', width: 90, sortable: true, sortName: "TradeAmt", align: 'right', formatter: function (val, rec) {
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
        var url = "/Module/Res/TradeOrder.aspx?id=" + id;
        addTab(title, url);
    },
    fnInfo: function (id) {
        var title = "查看资源信息";
        var url = "/Module/Res/HotelInfo.aspx?id=" + id;
        addTab(title, url, function () { });
    }
};

