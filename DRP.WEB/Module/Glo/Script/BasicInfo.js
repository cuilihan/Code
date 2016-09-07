$(function () {
    t.init();
});

var t = {
    init: function () {
        t.fnBindData();
        $("#btnAdd").click(function () {
            t.fnEdit('');
        });
        $("#btnDelete").click(function () {
            t.fnDeleteNode();
        });

    },
    serverUrl: "Service/BasicInfo.ashx?rnd=" + getRand(),
    fnBindData: function () {
        $('#tblData').datagrid({
            title: '基础参数设置',
            loadMsg: "正在加载数据...",
            height: document.documentElement.clientHeight,
            nowrap: true,
            iconCls: 'icon-reload',
            url: t.serverUrl + '&action=1&xType=' + request("xType"),
            idField: 'ID',
            rownumbers: true, //行号 
            toolbar: "#toolbar",
            columns: [[
                { field: "ID", checkbox: true },
                { field: 'Name', title: '名称', width: 250 },
                { field: 'OrderIndex', title: '排序', width: 70, align: "center" },
                { field: 'CreateUserName', title: '创建人', width: 90, align: "center" },
                {
                    field: 'Opt', title: "操作", width: 50, align: 'center', formatter: function (val, rec) {
                        var btnEdit = "<a href=\"javascript:;\" onclick=\"t.fnEdit('" + rec.ID + "')\">编辑</a>";
                        return btnEdit;
                    }
                }
            ]],
            singleSelect: false, //是否单选 
            pagination: false //分页控件  
        });

        $(window).resize(function () {
            $('#tblData').datagrid('resize');
        });
    },
    fnEdit: function (id) {
        var u = "/Module/Glo/BasicInfoEdit.aspx?id=" + id + "&xType=" + request("xType");
        return openWindow({ "title": "编辑参数信息", "width": "450", "height": "200", "url": u }, function () {
            $('#tblData').datagrid("reload");
        });
    },
    fnDeleteNode: function () {
        var id = getDataGridSelectedRow("tblData");
        if (id == "") {
            Alert("请选择参数");
            return false;
        }
        if (!confirm("确定要删除吗")) return;
        var url = t.serverUrl + "&action=2&id=" + id + "&xType=" + request("xType");
        dataService.ajaxGet(url, function (data) {
            if (data == "1") {
                Notice("删除成功");
                t.fnBindData();
            }
            else {
                Alert("删除失败");
            }
        })
    }
};

