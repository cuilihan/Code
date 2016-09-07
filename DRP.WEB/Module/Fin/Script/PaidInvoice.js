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
    serverUrl: "Service/PaidInvoice.ashx?rnd=" + getRand(),
    fnBindData: function () {
        $('#tblData').datagrid({
            loadMsg: "正在加载数据...",
            height: document.documentElement.clientHeight,
            nowrap: true,
            iconCls: 'icon-reload',
            url: t.serverUrl + '&action=1&orderID=' + request("id"),
            idField: 'ID',
            border: false,
            rownumbers: true, //行号 
            toolbar: "#toolbar",
            columns: [[
                { field: "ID", checkbox: true },
                { field: 'InvoiceAmt', title: '发票金额', width: 90 },
                { field: 'InvoiceNo', title: '发票编号', width: 90 },
                { field: 'Comment', title: '备注', width: 420 },
                { field: 'CreateUserName', title: '登记人', width: 90, align: "center" },
                {
                    field: 'Opt', title: "操作", width: 50, align: 'center', formatter: function (val, rec) {
                        return "<a href=\"javascript:;\" onclick=\"t.fnEdit('" + rec.ID + "')\">编辑</a>";
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
        var u = "/Module/Fin/PaidInvoiceEdit.aspx?id=" + id + "&orderID=" + request("id");
        return openWindow({ "title": "付款发票登记", "width": "450", "height": "230", "url": u }, function () {
            $('#tblData').datagrid("reload");
        });
    },
    fnDelete: function () {
        var id = getDataGridSelectedRow("tblData");
        if (id == "") {
            Alert("请选择参数");
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

