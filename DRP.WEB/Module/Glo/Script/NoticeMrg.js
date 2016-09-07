$(function () {
    t.init();
});

var t = {
    init: function () {
        t.fnBindData();
        $("#btnQuery").click(function () {
            t.fnQuery();
        });
        $("#btnAdd").click(function () {
            t.fnEdit('');
        });

        $("#btnDelete").click(function () {
            t.fnDelete();
        });
    },
    serverUrl: "Service/NoticeMrg.ashx?rnd=" + getRand(),
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
                    field: 'Subject', title: '标题名称', width: 350, sortable: true, sortName: "Subject", formatter: function (val, rec) {
                        return "<a href='NoticeInfo.aspx?id=" + rec.ID + "' target='_blank'>" + val + "</a>";
                    }
                },
                { field: 'CreateUserName', title: '发布人', width: 80, sortable: true, align: 'center', sortName: "CreateUserName" },
                { field: 'cDate', title: '发布日期', width: 100, sortable: true, align: 'center', sortName: "CreateDate" },
                {
                    field: 'Opt', title: "操作", width: 70, align: 'center', formatter: function (val, rec) {
                        return "<a href=\"javascript:;\" onclick=\"t.fnEdit('" + rec.ID + "')\">编辑</a>";
                    }
                }
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
    },
    fnEdit: function (id) {
        var title = "通知公告维护";
        var url = "NoticeEdit.aspx?id=" + id;
        addTab(title, url, function () { t.fnBindData(); });
    },
    fnDelete: function () {
        var rowID = getDataGridSelectedRow("tblData");
        if (rowID == "") {
            Alert("请选择通知公告");
            return false;
        }
        if (!confirm("确定要删除吗")) return;
        var url = t.serverUrl + "&action=2&id=" + rowID;
        dataService.ajaxGet(url, function (data) {
            if (data == "1") {
                Notice("删除成功");
                t.fnBindData();
            }
            else {
                Alert("保存失败");
            }
        })
    }
};

