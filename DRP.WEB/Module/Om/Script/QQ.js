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
            t.fnDelete();
        });

    },
    serverUrl: "Service/QQ.ashx?rnd=" + getRand(),
    fnBindData: function () {
        $('#tblData').datagrid({
            title: '在线QQ设置',
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
                { field: 'QQ', title: 'QQ号码', width: 100 },
                { field: 'Name', title: '客服人员', width: 90 },
                { field: 'Comment', title: '备注', width: 190 },
                { field: 'OrderIndex', title: '排序号', width: 60,align:'center' },
                {
                    field: 'Opt', title: "操作", width: 50, align: 'center', formatter: function (val, rec) {
                        var btnEdit = "<a href=\"javascript:;\" onclick=\"t.fnEdit('" + rec.ID + "')\">编辑</a>";
                        return btnEdit;
                    }
                }
            ]],
            singleSelect: true, //是否单选 
            pagination: false //分页控件     
        });

        $(window).resize(function () {
            $('#tblData').datagrid('resize');
        });
    },
    fnEdit: function (id) {
        var u = "/Module/Om/QQEdit.aspx?id=" + id;
        return openWindow({ "title": "在线QQ设置", "width": "450", "height": "250", "url": u }, function () {
            $('#tblData').datagrid("reload");
        });
    },
    fnDelete: function () {
        var id = getDataGridSelectedRow("tblData");
        if (id == "") {
            Alert("请选择QQ");
            return false;
        }
        if (!confirm("确定要删除吗")) return;
        var url = t.serverUrl + "&action=2&id=" + id;
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

