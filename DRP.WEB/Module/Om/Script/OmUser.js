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
    serverUrl: "Service/OmUser.ashx?rnd=" + getRand(),
    fnBindData: function () {
        $('#tblData').datagrid({
            title: '运维用户管理',
            loadMsg: "正在加载数据...",
            height: document.documentElement.clientHeight,
            nowrap: true,
            iconCls: 'icon-reload',
            url: t.serverUrl + '&action=1',
            idField: 'ID',
            rownumbers: true, //行号 
            toolbar: "#toolbar",
            columns: [[
                { field: 'UserName', title: '用户名', width: 90, sortable: true, sortName: "UserName" },
                { field: 'UserAcct', title: '登录账号', width: 90, sortable: true, sortName: "UserAcct" },
                { field: 'OrgName', title: '机构名称', width: 210, sortable: true, sortName: "OrgID" },
                {
                    field: 'DataStatus', title: '账号状态', width: 90, align: "center", sortable: true, sortName: "DataStatus",
                    formatter: function (val, rec) {
                        return val == "1" ? "启用" : "<span style='color:red;'>禁用</span>";
                    }
                },
                { field: 'CreateDate', title: '创建日期', width: 120, align: 'center', sortable: true, sortName: "CreateDate" }, 
                {
                    field: 'Opt', title: "操作", width: 100, align: 'center', formatter: function (val, rec) {
                        var btnEdit = "<a href=\"javascript:;\" onclick=\"t.fnEdit('" + rec.ID + "')\">编辑</a>";
                        var btnDel = "<a href=\"javascript:;\" onclick=\"t.fnDeleteNode('" + rec.ID + "')\">删除</a>";
                        return btnEdit + "&nbsp;| " + btnDel;
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
    fnQuery: function () {
        var key = $("#txtKey").val();
        $('#tblData').datagrid("reload", { "key": key });
    },
    fnEdit: function (id) {
        var u = "/Module/Om/OmUserEdit.aspx?id=" + id;
        return openWindow({ "title": "编辑用户信息", "width": 500, "height": 320, "url": u }, function () {
            $('#tblData').datagrid("reload");
        });
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

