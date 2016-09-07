$(function () {
    t.init();
});

var t = {
    init: function () {
        t.fnBindData();
        $("#btnQuery").click(function () {
            t.fnQuery();
        });
        $("#btnSetStatus").click(function () {
            t.fnSetStatus();
        });
    },
    serverUrl: "Service/Message.ashx?rnd=" + getRand(),
    fnBindData: function () {
        var status = $("#ddlStatus option:selected").val();
        var sDate = $("#sDate").val();
        var eDate = $("#eDate").val();
        var u = t.serverUrl + "&action=1&status=" + status + "&sDate=" + sDate + "&eDate=" + eDate;
        $('#tblData').datagrid({
            loadMsg: "正在加载数据...",
            title: "消息中心",
            height: document.documentElement.clientHeight,
            nowrap: true,
            border: 0,
            iconCls: 'icon-reload',
            url: u,
            idField: 'ID',
            rownumbers: true, //行号 
            toolbar: "#toolbar",
            frozenColumns: [[
                { field: 'ID', checkbox: true },
                {
                    field: 'MsgTitle', title: '消息标题', width: 380, sortable: true, sortName: "MsgTitle", formatter: function (val, rec) {
                        return "<a href='" + rec.URL + "' target='" + rec.Target + "'>" + val + "</a>";
                    }
                }
            ]],
            columns: [[
                { field: 'CreateDate', title: '发送日期', width: 120, sortable: true, sortName: "CreateDate", align: 'center' },
                { field: 'SendUserName', title: '发送人', width: 80, sortable: true, sortName: "SendUserName", align: 'center' },
                {
                    field: 'DataStatus', title: '状态', align: 'center', width: 70, sortable: true, sortName: "DataStatus", formatter: function (val, rec) {
                        return val == "2" ? "<span style='color:#ccc;'>已读</span>" : "未读";
                    }
                },
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
        t.fnBindData();
    },
    fnSetStatus: function () {
        var id = getDataGridSelectedRow("tblData");
        if (id == "") {
            Alert("请选择消息");
            return false;
        }
        if (!confirm("确定要设为已读吗")) return;
        var url = t.serverUrl + "&action=2&id=" + id;
        dataService.ajaxGet(url, function (data) {
            if (data == "1") {
                Notice("操作成功");
                $('#tblData').datagrid("reload");
            }
            else {
                Notice("操作失败");
            }
        })
    }
};

