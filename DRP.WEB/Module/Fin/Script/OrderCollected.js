//订单收款明细

$(function () {
    o.init();
});

var o = {
    init: function () {
        o.bindData();
        //$("#btnSave").click(function () { //收款批量确认
        //    o.fnCollectedConfirmedBatch();
        //});
    },
    url: "Service/OrderCollected.ashx",
    bindData: function () {
        $('#tblData').datagrid({
            loadMsg: "正在加载数据...",
            nowrap: true,
            striped: true,
            border: false,
            height: document.documentElement.clientHeight,
            collapsible: false, //是否可折叠的       
            url: o.url + "?action=1&orderID=" + request("id"),
            idField: 'ID',
            frozenColumns: [[
                        { field: 'ID', checkbox: true },
                        { field: 'CollectAmt', title: '收款金额', align: 'right', width: 70, sortable: true, sortName: "CollectAmt" },
                        { field: 'SrcBank', title: '收款银行', sortable: true, sortName: "SrcBank", width: 90 },
                        { field: 'CollectDate', title: '收款日期', sortable: true, sortName: "CollectDate", width: 70, align: 'center' }
            ]],
            columns: [[
                        { field: 'CreateDate', title: "操作日期", width: 80, align: 'center', sortable: true, sortName: "CreateDate" },
                        { field: 'CollectBill', title: '收据编号', sortable: true, sortName: "CollectBill", width: 60 },
                        { field: 'CollectType', title: '收款方式', sortable: true, sortName: "CollectType", width: 80 },
                        { field: 'Comment', title: '认领备注', width: 200, sortable: true, sortName: "Comment" },
                        {
                            field: 'CollectStatus', title: '收款状态', width: 60, align: 'center', sortable: true, sortName: "CollectStatus", formatter: function (val, rec) {
                                var s = "";
                                switch (val.toString()) {
                                    case "2":
                                        s = "待确认";
                                        break;
                                    case "3":
                                        s = "<span style='color:red;'>已确认</span>";
                                        break;
                                    case "4":
                                        s = "已取消";
                                        break;
                                }
                                return s;
                            }
                        },
                        { field: 'CreateUserName', title: '认领人', width: 70, align: 'center', sortable: true, sortName: "CreateUserName" },
                        { field: 'Auditor', title: "确认人", width: 70, align: 'center', sortable: true, sortName: "Auditor" },
                        { field: 'AuditDate', title: '确认日期', sortable: true, sortName: "AuditDate", width: 80, align: 'center' },
                        {
                            field: 'Opt', title: '操作', width: 150, align: 'center', formatter: function (val, rec) {
                                var isSign = rec.ClaimID == "00000000-0000-0000-0000-000000000000";//是否是登记模式,非认领模式无取消认领操作 
                                if (isSign) { //手动登记
                                    var confirm = "<a href='javscript:;' onclick=\"o.fnCollectedConfirmed('" + rec.ID + "','1')\">收款确认</a>";
                                    var cancelConfirmed = "<a href='javascript:;' style='color:blue;' onclick=\"o.fnCancelConfirmed('" + rec.ID + "','1')\">取消确认</a>";
                                    var dataDelete = "<a href='javascript:;' style='color:blue;' onclick=\"o.fnDelete('" + rec.ID + "','1')\">删除</a>";
                                    var s = "";
                                    switch (rec.CollectStatus.toString()) {
                                        case "2"://认领时可以确认收款也可以取消认领
                                            s = confirm + " | " + dataDelete;
                                            break;
                                        case "3"://已确认时可以取消确认
                                            s = cancelConfirmed;
                                            break;
                                    }
                                    return s;
                                }
                                else { //认领模式
                                    var confirm = "<a href='javscript:;' onclick=\"o.fnCollectedConfirmed('" + rec.ClaimID + "','0')\">收款确认</a>";
                                    var cancelClaim = "<a href='javascript:;' style='color:blue;' onclick=\"o.fnCancelClaim('" + rec.ClaimID + "')\">取消认领</a>";
                                    var cancelConfirmed = "<a href='javascript:;' style='color:blue;' onclick=\"o.fnCancelConfirmed('" + rec.ID + "','0')\">取消确认</a>";
                                    var dataDelete = "<a href='javascript:;' style='color:blue;' onclick=\"o.fnDeleteClaim('" + rec.ID + "','" + rec.ClaimID + "','0','" + rec.CollectStatus.toString() + "')\">删除</a>";
                                    var s = "";
                                    switch (rec.CollectStatus.toString()) {
                                        case "2"://认领时可以确认收款也可以取消认领
                                            s = confirm + " | " + cancelClaim + " | " + dataDelete;
                                            break;
                                        case "3"://已确认时可以取消确认
                                            s = cancelConfirmed;
                                            break;
                                    }
                                    if (s == cancelConfirmed) {
                                        return s;
                                    }
                                    else {
                                        return s;
                                    } 
                                }
                            }
                        }

            ]],
            rowStyler: function (rowIndex, rec) {
                if (rec.CollectStatus == "4")
                    return "color:red;";
            },
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
    fnCancelClaim: function (id) { //取消认领
        if (confirm("确定要取消认领吗")) {
            var u = "Service/CollectedItem.ashx?action=5&id=" + id;
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
    fnCollectedConfirmed: function (id, isSign) { //收款确认 
        if (confirm("确定要确认收款吗")) {
            var u = "Service/CollectedItem.ashx?src=Fin&action=2&id=" + id + "&r=" + getRand() + "&isSign=" + isSign;
            dataService.ajaxGet(u, function (data) {
                if (data == "1") {
                    o.bindData();
                    Notice("确认完成"); 
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
            sb.push(row.ClaimID);
            var status = row.CollectStatus;
            if (status != "2")
                checkRight = false;
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
            var u = "Service/CollectedItem.ashx?action=2&id=" + sb.join(",") + "&r=" + getRand();
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
    fnCancelConfirmed: function (id, isSign) { //取消确认
        if (confirm("取消确认回到未认领状态，确定要取消吗")) {
            var u = "Service/CollectedItem.ashx?action=3&id=" + id + "&r=" + getRand() + "&isSign=" + isSign;
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
    fnDelete: function (id,isSign) {//删除
        if (confirm("确定要删除吗？")) {
            var u = "Service/CollectedItem.ashx?action=7&id=" + id + "&r=" + getRand() + "&isSign=" + isSign;
            dataService.ajaxGet(u, function (data) {
                if (data == "1") {
                    Notice("删除成功");
                    $('#tblData').datagrid("reload");
                } else {
                    Alert("删除失败");
                }
            });
        }
    },
    fnDeleteClaim: function (id, ClaimID, isSign, CollectStatus) {//删除
        if (confirm("确定要删除吗？")) {
            var u = "Service/CollectedItem.ashx?action=6&id=" + id + "&ClaimID=" + ClaimID + "&isSign=" + isSign + "&CollectStatus" + CollectStatus;
            dataService.ajaxGet(u, function (data) {
                if (data == "1") {
                    Notice("删除成功");
                    $('#tblData').datagrid("reload");
                } else {
                    Alert("删除失败");
                }
            });
        }
    }
};