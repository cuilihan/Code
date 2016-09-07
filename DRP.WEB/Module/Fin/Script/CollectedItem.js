
$(function () {
    c.initData();
});


var c = {
    initData: function () {
        c.bindData();
        c.clickEvent();
    },
    clickEvent: function () {
        $("#btnQuery").click(function () {
            c.fnQuery();
        });
        $("#btnExcel").click(function () {
            c.fnImport();
        });
        $("#btnDel").click(function () {
            return c.fnDelete();
        });
        $("#btnAdd").click(function () {
            return c.fnEdit('');
        });
        $("#btnConfirmed").click(function () {
            return c.fnCollectedConfirmedBatch();
        });
    },
    url: "Service/CollectedItem.ashx?r=" + getRand(),
    bindData: function () {
        $('#tblData').datagrid({
            loadMsg: "正在加载数据...",
            nowrap: true,
            striped: true,
            border: false,
            height: document.documentElement.clientHeight - 33,
            collapsible: false, //是否可折叠的       
            url: c.url + "&action=1",
            idField: 'ID',
            columns: [[
                        { field: "ID", checkbox: true },
                        { field: 'BankName', title: '收款银行', width: 90, sortable: true, sortName: "BankName", align: 'left' },
                        { field: 'TradeDate', title: '交易日期', width: 90, sortable: true, sortName: "TradeDate", align: 'center' },
                        { field: 'TradeTime', title: '交易时间', width: 70, sortable: true, sortName: "TradeTime", align: 'center' },
                        { field: 'Summary', title: '摘要', width: 130, sortable: true, sortName: "Summary", align: 'left' },
                        { field: 'IncomeAmt', title: '收入金额', width: 80, sortable: true, sortName: "IncomeAmt", align: 'right' },
                        { field: 'FromBank', title: '交易行名', width: 90, sortable: true, sortName: "FromBank", align: 'left' },
                        { field: 'FromAcct', title: '对方户名', width: 90, sortable: true, sortName: "FromAcct", align: 'left' },
                        {
                            field: 'DataStatus', title: '状态', width: 50, sortable: true, sortName: "DataStatus", align: 'center', formatter: function (val, rec) {
                                var s = "";
                                switch (val) {
                                    case "1": s = "未认领"; break;
                                    case "2": s = "<span style='color:red;'>已认领</span>"; break;
                                    case "3": s = "<span style='color:blue;'>已确认</span>"; break;
                                }
                                return s;
                            }
                        },
                        { field: 'ClaimUser', title: '认领人', width: 60, sortable: true, sortName: "ClaimUser", align: 'center' },
                        { field: 'BillNo', title: '收款编号', width: 70, sortable: true, sortName: "BillNo", align: 'center' },
                        {
                            field: 'OrderNo', title: '订单编号', width: 120, sortable: true, sortName: "OrderNo", align: 'center', formatter: function (val, rec) {
                                return "<a href='javascript:;' title='查看订单收款明细' onclick=\"c.fnOrderCollect('" + rec.OrderID + "')\">" + val + "</a>";
                            }
                        },
                        {
                            field: 'opt', title: '操作', width: 120, align: 'center', formatter: function (val, rec) {
                                var a = "<a href='javascript:;' onclick=\"c.fnEdit('" + rec.ID + "')\">编辑</a>";
                                var b = "<a href='javascript:;' onclick=\"c.fnCollectedConfirmed('" + rec.ID + "')\">收款确认</a>";
                                var d = "<a href='javascript:;' style='color:red;' onclick=\"c.fnCancelClaim('" + rec.ID + "')\">取消认领</a>";
                                var c = "<a href='javascript:;' style='color:red;' onclick=\"c.fnCollectedCanceled('" + rec.ID + "')\">取消确认</a>";
                                if (rec.DataStatus == "1") { //未认领
                                    return a;
                                }
                                else {
                                    if (rec.DataStatus == "2") { //已认领
                                        return b + " | " + d;
                                    }
                                    else { //已确认
                                        return c;
                                    }
                                }
                            }
                        }
            ]],
            singleSelect: false, //是否单选 
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
    fnEdit: function (id) {
        var r = "/Module/Fin/CollectItemEdit.aspx?id=" + id;
        return openWindow({ "title": "银行收款项目", "width": "650", "height": "300", "url": r }, function () {
            $('#tblData').datagrid("reload");
        });
    },
    fnImport: function () { //Excel导入
        var url = "CollectedItemImport.aspx";
        addTab("银行收款明细导入", url, function () {
            c.bindData();
        });
    },
    fnOrderCollect: function (orderID) { //已收款明细
        var url = "OrderCollected.aspx?id=" + orderID;
        addTab("已收款明细", url, function () {
            $('#tblData').datagrid("reload");
        });
    },
    fnCancelClaim: function (id) { //取消认领
        if (confirm("确定要取消认领吗")) {
            var u = c.url + "&action=5&isSign=1&id=" + id + "&r=" + getRand();
            dataService.ajaxGet(u, function (data) {
                if (data == "1") {
                    Notice("取消完成");
                    $('#tblData').datagrid("reload");
                } else {
                    Alert("取消认领失败");
                }
            });
        }
    },
    fnCollectedConfirmed: function (id) { //收款确认
        if (confirm("确定要确认收款吗")) {
            var u = c.url + "&action=2&src=Fin&id=" + id;
            dataService.ajaxGet(u, function (data) {
                if (data == "1") {
                    Notice("确认完成");
                    $('#tblData').datagrid("reload");
                } else {
                    Alert("确认收款失败");
                }
            });
        }
    },
    fnCollectedConfirmedBatch: function () { //批量收款确认
        var sb = [];
        var checkRight = true;
        var rows = $('#tblData').datagrid('getSelections');
        for (var i = 0; i < rows.length; i++) {
            var row = rows[i];
            sb.push(row.ID);
            var status = row.DataStatus;
            if (status != "2") checkRight = false;
        }
        if (!checkRight) {
            Alert("只能选择已认领的收款项目");
            return false;
        }
        if (sb.length == 0) {
            Alert("请选择认领的收款项目");
            return false;
        }
        if (confirm("确定要确认收款吗")) {
            for (var i = 0; i < sb.length; i++) {
                var _id = sb[i];
                c.fnCollectedConfirmed(_id);
            }
        }
    },
    fnCollectedCanceled: function (id) { //取消收款确认
        if (confirm("取消确认回到未认领状态，确定要取消吗")) {
            var u = c.url + "&action=3&src=Fin&id=" + id;
            dataService.ajaxGet(u, function (data) {
                if (data == "1") {
                    Notice("取消收款完成");
                    $('#tblData').datagrid("reload");
                } else {
                    Alert("取消收款失败");
                }
            });
        }
    },
    fnDelete: function () {
        var arr = [];
        var hasClaim = false;
        var rows = $('#tblData').datagrid('getSelections');
        for (var i = 0; i < rows.length; i++) {
            var row = rows[i];
            if (row.DataStatus == "1")
                arr.push(row.ID);
            else
                hasClaim = true;
        }
        if (hasClaim) {
            Alert("不能删除已认领或已确认的收款");
            return false;
        }
        if (arr.length == 0) {
            Alert("请选择收款后再删除");
            return false;
        }
        if (!confirm("确定要删除吗")) return false;
        var u = c.url + "&action=4&id=" + arr.join(",");
        dataService.ajaxGet(u, function (data) {
            if (data == "1") {
                $('#tblData').datagrid("unselectAll");
                $('#tblData').datagrid("reload");
                Alert("删除成功"); 
            } else {
                Alert("删除数据失败");
            }
        });
    }
};

