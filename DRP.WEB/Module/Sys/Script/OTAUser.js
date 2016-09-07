$(function () {
    $("#btnQuery").click(function () {
        queryData();
    })
    $('#tblData').datagrid({
        // title: '用户管理',
        loadMsg: "正在加载数据...",
        nowrap: true,
        iconCls: 'icon-reload',
        striped: true,
        border: true,
        height: document.documentElement.clientHeight - 5,
        collapsible: false, //是否可折叠的       
        url: 'Service/User.ashx?action=8&d=' + new Date().toString(),
        idField: 'ID',
        columns: [[
        { field: 'ID', checkbox: true },
        { field: 'Name', title: '姓名', width: 90 },
        { field: 'DeptName', title: '所属部门', width: 150 },
        { field: 'Mobile', title: '手机号码', width: 90 },
        {
            field: 'UID', title: '状态', width: 150, formatter: function (val, rec) {
                if (val != "") {
                    return "已绑定";
                }
                else {
                    return "<span style='color:red;'>未绑定</span>";
                }
            }
        },
        {
            field: 'Opt', title: "操作", width: 200, align: 'center', formatter: function (val, rec) {

                var otabind = "<a href=\"javascript:;\" onclick=\"fnBindOTA('" + rec.ID + "')\">平台绑定</a>";

                return otabind;
            }
        }
        ]],
        singleSelect: false, //是否单选 
        pagination: true, //分页控件  
        rownumbers: false, //行号 
        sortName: "",
        sortOrder: "",
        pageSize: 20,
        toolbar: "#toolbar"
    });
    //设置分页控件  
    var p = $('#tblData').datagrid('getPager');
    $(p).pagination({
        beforePageText: '第', //页数文本框前显示的汉字  
        afterPageText: '页    共 {pages} 页',
        displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录'
    });
    $(window).resize(function () {
        $('#tblData').datagrid('resize');
    });
});


function fnBindOTA(id) {
    var title = "OTA绑定用户";
    var url = "OTAUserList.aspx?id=" + id;
    addTab(title, url, null);
}

function queryData() {
    var key = $("#txtKey").val();
    $('#tblData').datagrid("reload", { "key": key });
}


var lock = false;
var initOTAData = function () {
    if (lock) return false;

    $.ajax({
        type: "POST",
        async: true,
        url: 'Service/User.ashx?action=7&d=' + new Date().toString(),
        data: { "OTAID": $("#ddlOTA").val() },
        beforeSend: function (XMLHttpRequest) {
            $("#btnOTA").html("正在运行");
            lock = true;
        },
        success: function (data) {
            if (data == "1") {
                Alert("初始化成功");
            }
            else { Alert("初始化失败"); }

            $("#btnOTA").html("初始化数据");
            lock = false;
        },
        complete: function (XMLHttpRequest, textStatus) {
        },
        error: function () {
            lock = false;
        }
    });

}