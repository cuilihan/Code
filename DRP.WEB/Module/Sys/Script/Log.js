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
    serverUrl: "Service/Log.ashx?rnd=" + getRand(),
    fnBindData: function () {
        $('#tblData').datagrid({
            title: '用户管理',
            loadMsg: "正在加载数据...",
            height: document.documentElement.clientHeight,
            nowrap: true,
            iconCls: 'icon-reload',
            url: t.serverUrl + '&action=1',
            idField: 'ID',
            rownumbers: true, //行号 
            toolbar: "#toolbar",
            columns: [[
                { field: "ID", checkbox: true },

                {
                    field: 'Message', title: '日志内容', width: 340, sortable: true, sortName: "Message", formatter: function (val, rec) {
                        return "<a onclick=\"t.fnLogInfo('"+rec.ID+"')\" href='javascript:;'>" + val + "</a>";
                    }
                },
                { field: 'Creator', title: '操作人', width: 90, align: 'center', sortable: true, sortName: "Creator" },
                { field: 'LogDate', title: '日志记录时间', width: 120, sortable: true, sortName: "LogDate" },
                {
                    field: 'LogType', title: '日志类型', width: 70, align: 'center', sortable: true, sortName: "LogType", formatter: function (val, rec) {
                        var s = "";
                        switch (val.toString()) {
                            case "1": s = "错误";
                                break;
                            case "2": s = "崩溃";
                                break;
                            case "3": s = "调试";
                                break; c
                            case "4": s = "警告";
                                break;
                            default: s = "记录";
                                break;
                        }
                        return s;
                    }
                },
                { field: 'IP', title: '操作人IP地址', width: 110, sortable: true, sortName: "IP" },
                { field: 'Browser', title: '操作人浏览器', width: 110, sortable: true, sortName: "Browser" }
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
        var logType = $('#logType option:selected').val();
        var sDate = $("#sDate").val();
        var eDate = $("#eDate").val();

        $('#tblData').datagrid("reload", { "sDate": sDate, "eDate": eDate, "lv": logType });
    },
    fnLogInfo: function (id) {
        var u = "/Module/Sys/LogInfo.aspx?id=" + id;
        return openWindow({ "title": "查看日志", "width": 600, "height": 380, "url": u }, null);
    }
};

