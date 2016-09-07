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
    },
    serverUrl: "Service/User.ashx?rnd=" + getRand(),
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
                { field: 'ID', checkbox: true },
                { field: 'Name', title: '用户名', width: 60, sortable: true, sortName: "Name" },
                { field: 'DeptName', title: '部门名称', width: 150, sortable: true, sortName: "DeptID" },
                { field: 'Mobile', title: '手机号', width: 90, align: 'center', sortable: true, sortName: "Mobile" },
                {
                    field: 'Email', title: '电子邮件', width: 140, sortable: true, sortName: "Email", formatter: function (val, rec) {
                        return "<a href='mailto:" + val + "'>" + val + "</a>";
                    }
                },
                { field: 'IDNo', title: '身份证号', width: 110, sortable: true, sortName: "IDNo" },
                { field: 'AcctID', title: '登录账号', width: 80, sortable: true, sortName: "AcctID" },
                {
                    field: 'DataStatus', title: '账号状态', width: 60, align: "center", sortable: true, sortName: "DataStatus",
                    formatter: function (val, rec) {
                        return val == "1" ? "启用" : "<span style='color:red;'>禁用</span>";
                    }
                },
                {
                    field: 'Opt', title: "操作", width: 150, align: 'center', formatter: function (val, rec) {
                        var btnEdit = "<a href=\"javascript:;\" onclick=\"t.fnEdit('" + rec.ID + "')\">编辑</a>";
                        var btnDel = "<a href=\"javascript:;\" onclick=\"t.fnDeleteNode('" + rec.ID + "')\">删除</a>";
                        var btnPermission = "<a href=\"javascript:;\" onclick=\"t.fnPermissionQry('" + rec.ID + "')\">权限查询</a>";
                        return btnEdit + " | " + btnDel + " | " + btnPermission;
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
        var u = "/Module/Sys/UserEdit.aspx?id=" + id;
        return openWindow({ "title": "编辑用户信息", "width": 700, "height": 450, "url": u }, function () {
            $('#tblData').datagrid("reload");
        });
    },
    fnPermissionQry: function (userID) {
        var u = "/Module/Sys/UserPermission.aspx?id=" + userID;
        return openWindow({ "title": "用户权限查询", "width": 700, "height": 500, "url": u }, function () {
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

