$(function () {
    o.init();
});

var o = {
    init: function () {
        o.bindData();
        $("#btnQuery").click(function () {
            o.fnQuery();
        });
    },
    url: "Service/CollectedClaim.ashx",
    bindData: function () {
        $('#tblData').datagrid({
            loadMsg: "正在加载数据...",
            nowrap: true,
            striped: true,
            border: false,
            height: document.documentElement.clientHeight - 35,
            collapsible: false, //是否可折叠的       
            url: o.url + "?action=1",
            idField: 'ID',
            frozenColumns: [[
                        { field: 'TradeDate', title: '收款日期', align: 'center', width: 100, sortable: true, sortName: "TradeDate" },
                        { field: 'IncomeAmt', title: "收款金额", width: 70, align: 'center', sortable: true, sortName: "IncomeAmt" }

            ]],
            columns: [[
                        { field: 'BankName', title: "收款银行", width: 130, sortable: true, sortName: "BankName" },
                        { field: 'TradeTime', title: '交易时间', sortable: true, sortName: "TradeTime", width: 70, align: 'center' },
                        { field: 'Summary', title: '摘要', sortable: true, sortName: "Summary", width: 90 },
                        { field: 'FromBank', title: '交易行名', width: 110, sortable: true, sortName: "FromBank" },
                        { field: 'FromAcct', title: '对方户名', width: 90, align: 'center', sortable: true, sortName: "FromAcct" },
                        {
                            field: 'DataStatus', title: '状态', width: 60, align: 'center', sortable: true, sortName: "DataStatus", formatter: function (val, rec) {
                                var s = "";
                                switch (val) {
                                    case "1":
                                        s = "<span>未认领</span>";
                                        break;
                                    case "2":
                                        s = "<spna style='color:red;'>已认领</span>";
                                        break;
                                    case "3":
                                        s = "<spna style='color:blue;'>已确认</span>";
                                        break;
                                    default:
                                        s = "未知";
                                        break;
                                }
                                return s;
                            }
                        },
                        { field: 'ClaimUser', title: '认领人', width: 60, align: 'center', sortable: true, sortName: "ClaimUser" },
                        { field: 'BillNo', title: '收据编号', width: 60, align: 'center', sortable: true, sortName: "BillNo" },
                        {
                            field: 'Opt', title: '操作', width: 60, align: 'center', formatter: function (val, rec) {
                                var a = "<a href='javascript:;' onclick=\"o.fnClaim('" + rec.ID + "','" + rec.IncomeAmt + "')\">认领</a>";
                                if (rec.DataStatus == "1") {
                                    return a;
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
        var sDate = $("#sDate").val();
        var eDate = $("#eDate").val();
        var bank = $("#BankName").val();
        var status = $("#DataStatus option:selected").val();
        var minIncome = $("#MinIncome").val();
        var maxIncome = $("#MaxIncome").val();
        var fromBank = $("#FromBank").val();
        var fromAcct = $("#FromAcct").val();
        $('#tblData').datagrid("reload", { "sDate": sDate, "eDate": eDate, "Status": status, "BankName": bank, "MinAmt": minIncome, "MaxAmt": maxIncome, "FromBank": fromBank, "FromAcct": fromAcct });
    },
    fnClaim: function (id, amt) { //认领  
        var h = 600;
        if (h > $(window).height() - 30)
            h = $(window).height() - 30;
        var u = "/Module/Order/OrderClaim.aspx?id=" + id + "&amt=" + amt + "&h=" + h;
        return openWindow({ "title": "订单收款认领", "width": "700", "height": h, "url": u }, function () {
            $('#tblData').datagrid("reload");
        });
    }
};