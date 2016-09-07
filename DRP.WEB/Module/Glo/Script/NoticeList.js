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
    serverUrl: "Service/NoticeQuery.ashx?rnd=" + getRand(),
    fnBindData: function () {
        $('#tblData').datagrid({
            loadMsg: "正在加载数据...",
            height: document.documentElement.clientHeight - 37,
            nowrap: true,
            iconCls: 'icon-reload',
            url: t.serverUrl + '&action=1',
            idField: 'ID',
            rownumbers: true, //行号 
            toolbar: "#toolbar",
            columns: [[
                { field: 'ID', checkbox: true },
                {
                    field: 'Subject', title: '标题名称', width: 400, sortable: true, sortName: "Subject", formatter: function (val, rec) {
                        return "<a href='NoticeInfo.aspx?id=" + rec.ID + "' target='_blank'>" + val + "</a>";
                    }
                },
                { field: 'CreateUserName', title: '发布人', width: 80, sortable: true, align: 'center', sortName: "CreateUserName" },
                { field: 'cDate', title: '发布日期', width: 100, sortable: true, align: 'center', sortName: "CreateDate" } 
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
        var key = $("#txtSubject").val();
        $('#tblData').datagrid("reload", { "key": key });
    } 
};

