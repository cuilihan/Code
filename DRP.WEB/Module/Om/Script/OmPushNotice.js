$(function () {
    t.init();
});

var t = {
    init: function () {
        t.fnBindData();
        $("#btnAdd").click(function () {
            t.fnEdit('');
        });
    },
    serverUrl: "Service/GloPushNotice.ashx?rnd=" + getRand(),
    fnBindData: function () {
        $('#tblData').datagrid({
            loadMsg: "正在加载数据...",
            height: document.documentElement.clientHeight - 35,
            nowrap: true,
            iconCls: 'icon-reload',
            url: t.serverUrl + '&action=1',
            idField: 'ID',
            border: false,
            rownumbers: true, //行号 
            toolbar: "#toolbar",
            columns: [[ 
                {
                    field: 'sDate', title: '消息有效期', width: 190,align:'center', sortable: true, sortName: "sDate", formatter: function (val, rec) {
                        return val + " / " + rec.eDate;
                    }
                },
                {
                    field: 'LinkUrl', title: '链接地址', width: 320, align: 'center', sortable: true, sortName: "LinkUrl", formatter: function (val, rec) {
                        return "<a href='" + val + "' target='_blank'>" + val + "</a>";
                    }
                },
                { field: 'CreateDate', title: '创建日期', width: 110, align: 'center', sortable: true, sortName: "CreateDate" },
                { field: 'Creator', title: '创建人', width: 90, align: 'center', sortable: true, sortName: "Creator" },
                {
                    field: 'Opt', title: "操作", width: 100, align: 'center', formatter: function (val, rec) {
                        var btnEdit = "<a href=\"javascript:;\" onclick=\"t.fnEdit('" + rec.ID + "')\">编辑</a>";
                        var btnDel = "<a href=\"javascript:;\" onclick=\"t.fnDeleteNode('" + rec.ID + "')\">删除</a>";
                        return btnEdit + " | " + btnDel;
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
    fnEdit: function (id) {  
        var title = "编辑消息";
        var url = "PushNoticeEdit.aspx?id=" + id;
        addTab(title, url, function () { $('#tblData').datagrid("reload"); });
    },
    fnDeleteNode: function (id) {
        if (!confirm("确定要删除吗")) return;
        var url = t.serverUrl + "&action=2&id=" + id;
        dataService.ajaxGet(url, function (data) {
            if (data == "1") {
                Notice("删除成功");
                $('#tblData').datagrid("reload");
            }
            else {
                Notice("删除失败");
            }
        })
    }
};
 


