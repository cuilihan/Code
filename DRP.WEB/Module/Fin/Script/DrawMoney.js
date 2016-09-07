$(function () {
    o.init();
});

var o = {
    init: function () {
        o.bindData();
        $("#btnQuery").click(function () {
            o.fnQuery();
        });
        $("#btnSetStatus").click(function () {
            return o.fnSetStatusBatch("2");
        });
    },
    url: "Service/DrawMoney.ashx",
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
                        { field: 'ID', checkbox: true },
                        { field: 'OrderNo', title: "订单编号", width: 140, sortable: true, sortName: "OrderNo" },
                        { field: 'OrderName', title: "订单名称", width: 160, sortable: true, sortName: "OrderName" }
            ]],
            columns: [[
                        { field: 'CreateUserName', title: '提交人', sortable: true, sortName: "CreateUserName", width: 90, align: 'center' },
                        { field: 'TourDate', title: '出团日期', width: 100, align: 'center', sortable: true, sortName: "TourDate" },
                        { field: 'Amount', title: '领款金额', width: 60, align: 'right', sortable: true, sortName: "Amount" },
                        { field: 'Method', title: '领款方式', width: 60, sortable: true, sortName: "Method" },
                        { field: 'Comment', title: '领款备注', width: 140, sortable: true, sortName: "Comment" },
                        { field: 'GuideName', title: '导游名称', sortable: true, sortName: "Guide", width: 80 },
                        {
                            field: 'DataStatus', title: "领款状态", align: 'center', width: 60, sortable: true, sortName: "DataStatus", formatter: function (val, rec) {
                                return val == "1" ? "未领款" : "<span style='color:red;'>已领款</span>";
                            }
                        },
                        {
                            field: "Opt", title: "操作", width: 70, align: 'center', formatter: function (val, rec) {
                                if (rec.DataStatus == "1")
                                    return "<a href='javascript:;' onclick=\"o.fnSetStatus('" + rec.ID + "','2')\">设为领取</a>";
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
        var status = $("#ddlDataStatus option:selected").val();
        var name = $("#txtOrderName").val();
        var no = $("#txtOrderNo").val();
        var sDate = $("#sDate").val();
        var eDate = $("#eDate").val();
        $('#tblData').datagrid("reload", { "Status": status, "OrderName": name, "OrderNo": no, "sDate": sDate, "eDate": eDate });
    },
    fnSetStatus: function (id, status) {
        if (!confirm("确定要操作吗")) return false;
        var u = o.url + "?action=2&id=" + id + "&status=" + status;
        dataService.ajaxGet(u, function (data) {
            if (data == "1") {
                Notice("操作成功");
                o.fnQuery();
            }
            else {
                Alert("操作失败");
            }
        });
    },
    fnSetStatusBatch: function (datastatus) {
        var sb = [];
        var checkRight = true;
        var rows = $('#tblData').datagrid('getSelections');
        for (var i = 0; i < rows.length; i++) {
            var row = rows[i];
            sb.push(row.ID);
            var status = row.DataStatus;
            if (status != "1")
                checkRight = false;
        }
        if (sb.length == 0) {
            Alert("请选择领款项目");
            return false;
        }
        if (!checkRight) {
            Alert("只能选择未领款的项目");
            return false;
        }
        if (!confirm("确定要操作吗")) return false;
        var u = o.url + "?action=2&id=" + sb.join(",") + "&status=" + datastatus;
        dataService.ajaxGet(u, function (data) {
            if (data == "1") {
                Notice("操作成功");
                o.fnQuery();
            }
            else {
                Alert("操作失败");
            }
        });
    }
};