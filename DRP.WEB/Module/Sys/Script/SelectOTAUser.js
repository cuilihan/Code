var c = {
    initData: function () {
        c.bindData();
        $("#btnQuery").click(function () {
            c.fnQuery();
        });
        $("#btnOk").click(function () {
            c.fnSave();
        });
    },
    bindData: function () {
        $('#tblData').datagrid({
            // title: '用户管理',
            loadMsg: "正在加载数据...",
            nowrap: true,
            iconCls: 'icon-reload',
            striped: true,
            border: true,
            height: document.documentElement.clientHeight - 5,
            collapsible: false, //是否可折叠的       
            url: 'Service/User.ashx?action=10&id=' + request("id") + '&d=' + new Date().toString(),
            idField: 'UserID',
            columns: [[
            { field: 'UserID', checkbox: true },
            { field: 'UserAccount', title: '账号', width: 240 },
            { field: 'Contact', title: '联系人', width: 240 }
            ]],
            singleSelect: true, //是否单选 
            sortName: "",
            sortOrder: "",
            toolbar: "#toolbar"
        });
        $(window).resize(function () {
            $('#tblData').datagrid('resize');
        });
    },
    fnQuery: function () {
        var supplier = $("#ddlSupplier option:selected").val();

        $('#tblData').datagrid("reload", { "Supplier": supplier });
    },
    fnSave: function () {
        var row = $('#tblData').datagrid('getSelections');

        if (row[0] == null) {
            return;
        }
        var supplier = $("#ddlSupplier option:selected").val();

        $.ajax({
            type: "post",
            url: "Service/User.ashx?action=12",
            data: { "otauid": row[0].UserID, "otaName": row[0].Contact + "[" + row[0].UserAccount + "]", "otdid": supplier, "name": "", "uid": request("id") },
            success: function (data, textStatus) {
                if (data == "0") {
                    alert("保存数据失败");
                }
                else {
                    closeWindow("新增成功");
                }
            },
            error: function () {
                Alert("操作失败");
            }
        });
    }
}

$(function () {
    c.initData();
});