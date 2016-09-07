$(function () {
    o.init();
});

var o = {
    init: function () {
        o.clickEvent();
        o.bindData();
    },
    clickEvent: function () {
        $("#btnQuery").click(function () {
            o.fnQuery();
        });
    },
    url: "Service/BalanceSettlement.ashx?action=1",
    bindData: function () {
        $('#tblData').datagrid({
            loadMsg: "正在加载数据...",
            nowrap: true,
            striped: true,
            border: false,
            height: document.documentElement.clientHeight - 35,
            collapsible: false, //是否可折叠的       
            url: o.url,
            idField: 'ID',
            frozenColumns: [[
                        { field: 'ID', checkbox: true },
                        { field: 'OrderNo', title: "编号", width: 140, sortable: true, sortName: "OrderNo", align: 'center' },
                        {
                            field: 'OrderName', title: '订单名称', width: 160, sortable: true, sortName: "OrderName", formatter: function (val, rec) {
                                var t = "<a href='/Module/Order/TeamOrderInfo.aspx?id=" + rec.OrderID + "' target='_blank'>" + val + "</a>";
                                if (rec.OrderType == "4")
                                    t = "<a href='/Module/Order/TourOrderInfo.aspx?id=" + rec.OrderID + "' target='_blank'>" + val + "</a>";
                                return t;
                            }
                        },
                        { field: 'TourDate', title: '订单日期', width: 80, align: 'center', sortable: true, sortName: "TourDate" }
            ]],
            columns: [[
                        { field: 'GuideName', title: "导游", width: 80, sortable: true, sortName: "GuideName" },
                        { field: 'GuideMobile', title: "手机号", width: 90, align: 'center', sortable: true, sortName: "GuideMobile" },
                        { field: 'DrawMoney', title: '领款金额', sortable: true, sortName: "DrawMoneyAmt", width: 70, align: 'right' },
                        { field: 'BalanceCost', title: '报账成本', sortable: true, sortName: "BalanceCost", width: 70, align: 'right' },
                        {
                            field: 'SettlementAmt', title: '结算金额', sortable: true, sortName: "SettlementAmt", width: 70, align: 'right', formatter: function (val, rec) {
                                return "<span style='color:red;'>" + val + "</span>";
                            }
                        },
                        { field: 'SettlementType', title: "结算类型", width: 70, align: 'center', sortable: true, sortName: "SettlementType" },
                        {
                            field: 'DataStatus', title: '状态', sortable: true, sortName: "DataStatus", width: 70, align: 'center', formatter: function (val, rec) {
                                return val.toString() == "1" ? "未结算" : "<span style='color:red;'>已结算</span>";
                            }
                        },
                        { field: 'CreateUserName', title: '提交人', sortable: true, sortName: "CreateUserName", width: 70, align: 'center' },
                        {
                            field: 'Opt', title: "操作", width: 90, align: 'center', formatter: function (val, rec) {
                                var a = "<a href='javascript:;'  onclick=\"o.fnSettlement('" + rec.ID + "')\" >结算</a>";
                                var b = "<a href='javascript:;'  onclick=\"o.fnViewBill('" + rec.ID + "')\" >查看</a>";
                                if (rec.DataStatus.toString() == "1") {
                                    return b + " | " + a;
                                } else {
                                    return b;
                                }
                            }
                        }
            ]],
            singleSelect: true, //是否单选 
            pagination: true, //分页控件  
            rownumbers: true, //行号  
            pageSize: 20,
            toolbar: "#toolbar"
        });

        $(window).resize(function () {
            $('#tblData').datagrid('resize');
        });
    },
    fnQuery: function () {
        var orderName = $("#txtOrderName").val();
        var orderNo = $("#txtOrderNo").val();
        var name = $("#txtGuideName").val();
        var mobile = $("#txtGuideMobile").val();
        var dataStatus = $("#ddlDataStatus option:selected").val();
        $('#tblData').datagrid("reload", { "OrderName": orderName, "OrderNo": orderNo, "Name": name, "Mobile": mobile, "Status": dataStatus });
    },
    fnViewBill: function (id) { //查看
        var u = "/Module/Fin/SettlementBill.aspx?id=" + id;
        return openWindow({ "title": "查看报账结算单", "width": 650, "height": 420, "url": u }, function () {
        });
    },
    fnSettlement: function (id) { //结算
        var u = "/Module/Fin/SettlementAction.aspx?id=" + id;
        return openWindow({ "title": "报账结算", "width": 650, "height": 280, "url": u }, function () {
            o.fnQuery();
        });
    }
};