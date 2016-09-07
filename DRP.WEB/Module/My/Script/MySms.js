$(function () {
    t.init();
});

var t = {
    init: function () {
        t.fnBindData();
        $("#btnQuery").click(function () {
            t.fnQuery();
        });
        $("#btnSend").click(function () {
            t.fnSendSms();
        });
    },
    serverUrl: "Service/Message.ashx?rnd=" + getRand(),
    fnBindData: function () {
        var u = t.serverUrl + "&action=3";
        $('#tblData').datagrid({
            loadMsg: "正在加载数据...",
            title: "手机短消息",
            height: document.documentElement.clientHeight,
            nowrap: true,
            border: 0,
            iconCls: 'icon-reload',
            url: u,
            idField: 'ID',
            rownumbers: true, //行号 
            toolbar: "#toolbar",
            columns: [[
                 { field: 'ID', checkbox: true },
                 {
                     field: 'MsgContent', title: '消息内容', width: 500, sortable: true, sortName: "MsgContent", formatter: function (val, rec) {
                         return "<a href='javascript:;' onclick=\"t.fnViewSms('" + rec.ID + "')\">" + val + "</a>";
                     }
                 },
                 { field: 'RecMobile', title: '接收手机号', width: 100, sortable: true, sortName: "RecMobile", align: 'center' },
                { field: 'CreateDate', title: '发送日期', width: 100, sortable: true, sortName: "CreateDate", align: 'center' }
            ]],
            singleSelect: true, //是否单选 
            pagination: true, //分页控件    
            pageSize: 20
        });

        $(window).resize(function () {
            $('#tblData').datagrid('resize');
        });
    },
    fnQuery: function () {
        var key = $("#txtKey").val();
        var sDate = $("#sDate").val();
        var eDate = $("#eDate").val();
        $('#tblData').datagrid("reload", { "key": key, "sDate": sDate, "eDate": eDate });
    },
    fnViewSms: function (id) {
        var u = "/Module/My/SmsView.aspx?id=" + id;
        return openWindow({ "title": "查看消息", "width": "600", "height": "300", "url": u }, function () { 
        });
    },
    fnSendSms: function () {
        var u = "/Module/My/SendSms.aspx";
        return openWindow({ "title": "发消息", "width": "600", "height": "340", "url": u }, function () {
            t.fnQuery();
        });
    }
};

