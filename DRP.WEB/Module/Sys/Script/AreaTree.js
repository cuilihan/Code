
var lock = false;
var t = {
    init: function () {
        t.bindData();

        $("#btnOTA").click(function () {
            t.initAreaData();
        })
    },
    serviceUrl: "Service/Area.ashx?r=" + getRand(),
    bindData: function () {
        $('#tblData').treegrid({
            loadMsg: "正在加载数据...",
            nowrap: false,
            striped: true,
            iconCls: 'icon-reload',
            border: true,
            url: t.serviceUrl + '&OTAID=' + $("#ddlOTA").val() + '&action=3&d=' + new Date().toString(),
            treeField: 'Name',
            idField: 'ID',
            rownumbers: true, //行号 
            columns: [[
                { field: 'Name', title: '目的地', width: 200 },
                { field: 'OTAareaName', title: 'OTA目的地', width: 200 },
                { field: 'OTAName', title: 'OTA名称', width: 200 },
                {
                    field: 'Opt', title: "操作", width: 200, align: 'center', formatter: function (val, rec) {
                        return "<a href=\"javascript:;\" onclick=\"DelDept('" + rec.ID + "')\">解除绑定</a>&nbsp;<a href=\"javascript:;\" onclick=\"fnBindOTA('" + rec.ID + "','" + rec.Name + "')\">平台绑定</a>";
                    }
                }
            ]]
        });
    },
    initAreaData: function () {
        $.ajax({
            type: "POST",
            async: true,
            url: t.serviceUrl + '&action=5&d=' + new Date().toString(),
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


}

function loadData() {
    t.bindData();
}

function DelDept(id) {
    if (!confirm("确定要解除绑定吗")) return;
    var u = "Service/Area.ashx?action=7&r=" + getRand() + "&relId="+id;
    dataService.ajaxGet(u, function (data) {
        if (data == "1") {
            Alert("解除绑定成功");
            t.bindData();
        } else {
            alert("解除绑定失败");
        }
    })
}


function fnBindOTA(id, text) {
    var otaID = $("#ddlOTA").val();
    var _text = escape(text);

    var defaults = { "width": 260, "height": 380, "title": "选择区域" };
    var options = {};
    var opt = $.extend(defaults, options);
    $.dialog({
        cover: true,
        width: opt.width, height: opt.height, max: false,
        min: false, title: opt.title,
        content: 'url:/Module/Sys/SelectOTAArea.aspx?otdid=' + otaID + '&id=' + id + '&text=' + _text + '&r=' + getRand()
    });
    return false;


}