var c = {
    initData: function () {
        c.bindData();
        c.clickEvent();
    },
    clickEvent: function () {
        $("#btnAdd").click(function () {
            c.fnOTAEdit('');
        });
        $("#btnQuery").click(function () {
            c.fnQuery();
        });
    },
    fnOTAEdit: function (id) { //散客订单修改
        if (id == "") {
            return openWindow({ "title": "OTA接口新增", "width": "650", "height": "350", "url": "/Module/Sys/OTAEdit.aspx" }, function () {
                $('#tblData').datagrid("reload");
            });
        }
        else {
            return openWindow({ "title": "OTA接口修改", "width": "650", "height": "350", "url": "/Module/Sys/OTAEdit.aspx?id=" + id }, function () {
                $('#tblData').datagrid("reload");
            });
        }
    },
    url: "Service/OTASetting.ashx?r=" + getRand(),
    bindData: function () {
        $('#tblData').datagrid({
            loadMsg: "正在加载数据...",
            nowrap: true,
            striped: true,
            border: true,
            height: document.documentElement.clientHeight - 45,
            collapsible: false, //是否可折叠的       
            url: c.url + "&action=1",
            idField: 'ID',
            columns: [[
                        { field: 'OTAName', title: '平台', width: 150, sortable: true, sortName: "OTAName", align: 'center' },
                        { field: 'AcctID', title: '主账号', width: 80, align: 'center' },
                        { field: 'AppId', title: 'AppID', width: 80, sortable: true, sortName: "AppID", align: 'center' },
                        { field: 'AppKey', title: 'AppKey', width: 300, align: 'right' },
                        { field: 'OTA', title: '名称', width: 70, align: 'right' },
                        { field: 'OTAServiceUrl', title: 'ApiUrl', width: 200, align: 'center' },
                        {
                            field: 'SyncType', title: '是否同步新增订单', width: 100, align: 'center', formatter: function (val, rec) {
                                if (rec.SyncType == "1") {
                                    return "<span style='color:blue;'>是</span>";
                                }
                                else {
                                    return "<span>否</span>";
                                }
                            }
                        },
                        {
                            field: 'Opt', title: "操作", width: 180, align: 'center', formatter: function (val, rec) {
                                if (rec.ID) {
                                    var btns = [];
                                    var edit = "<a href='javascript:;' title='OTA接口修改' onclick=\"c.fnOTAEdit('" + rec.ID + "')\" >编辑</a>";
                                    var del = "<a href='javascript:;' title='OTA接口删除' onclick=\"c.fnDelete('" + rec.ID + "')\" >删除</a>";
                                    btns.push(edit);
                                    btns.push(del);

                                    return btns.join("&nbsp;");
                                }
                            }
                        }
            ]],
            singleSelect: false, //是否单选 
            pagination: true, //分页控件  
            rownumbers: true, //行号  
            showFooter: true,
            sort: "OTAName",
            order: "desc",
            toolbar: "#toolbar",
            pageSize: 20
        });

        $(window).resize(function () {
            $('#tblData').datagrid('resize');
        });
    },
    fnDelete: function (id) {
        if (!confirm("确定要删除吗")) return;
        var url = c.url + "&action=2&id=" + id;
        dataService.ajaxGet(url, function (data) {
            var msg = data == "1" ? "操作成功" : "操作失败";
            Notice(msg);
            if (data == "1") {
                $('#tblData').datagrid("reload");
            }
        });
    },
    fnQuery: function () {
        var supplier = $("#ddlSupplier option:selected").val();

        $('#tblData').datagrid("reload", { "Supplier": supplier });
    }
}

function toCheckName() {
    $.ajax({
        type: 'POST',
        dataType: 'json',
        url: "Service/OTASetting.ashx?r=" + getRand() + "&action=3" + "&id=" + $("#hfID").val(),
        data: 'OTA=' + $("#OTA").val(),
        async: false,
        success: function (json) {
            if (json.verify == '1') {
                $("#lblError").text('名称已存在！');
                $("#hfOTAisHave").val(1);
            }
            else {
                $("#hfOTAisHave").val(0);
            }
        }
    });
}