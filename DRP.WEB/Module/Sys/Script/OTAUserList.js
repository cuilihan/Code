$(function () {

    $("#btnAdd").click(function () {
        fnOTAEdit();
    });
    c.bindData();
});

var c = {
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
            url: 'Service/User.ashx?action=9&id=' + request("id") + '&d=' + new Date().toString(),
            idField: 'ID',
            columns: [[
            { field: 'ID', checkbox: true },
            { field: 'Name', title: '姓名', width: 90 },
            { field: 'OTAUName', title: 'OTA绑定姓名', width: 90 },
            { field: 'OTAName', title: 'OTA名称', width: 90 },
            { field: 'DeptName', title: '所属部门', width: 150 },
            {
                field: 'Opt', title: "操作", width: 280, align: 'center', formatter: function (val, rec) {
                    var otabind = "<a href=\"javascript:;\" onclick=\"fnDelOTA('" + rec.CID + "')\">解除绑定</a>";
                    return otabind;
                }
            }
            ]],
            singleSelect: false, //是否单选 
            sortName: "",
            sortOrder: "",
            toolbar: "#toolbar"
        });
    }
}

function fnOTAEdit() {
    var id = request("id");
    var u = "/Module/Sys/SelectOTAUser.aspx?id=" + id;

    return openWindow({ "title": "OTA绑定账号新增", "width": "650", "height": "350", "url": u }, function () {
        $('#tblData').datagrid("reload");
    });
}

function fnDelOTA(id) {
    if (!confirm("确定要解除绑定吗")) return;
    var url = "Service/User.ashx?action=11&relId=" + id + "&d=" + getRand() + "";
    dataService.ajaxGet(url, function (data) {
        var msg = data == "1" ? "解绑成功" : "解绑失败";
        Notice(msg);
        if (data == "1") {
            location.reload();
        }
    }, true);
}