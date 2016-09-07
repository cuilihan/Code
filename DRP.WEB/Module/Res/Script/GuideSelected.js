$(function () {
    t.init();
});

var t = {
    init: function () {
        t.fnBindData();
        $("#btnQuery").click(function () {
            t.fnQuery();
        });
        $("#btnOk").click(function () {
            t.fnSelected();
        });
    },
    serverUrl: "Service/Guide.ashx?xType=3&Status=1&rnd=" + getRand(),
    fnBindData: function () {
        $('#tblData').datagrid({
            loadMsg: "正在加载数据...",
            height: 455,
            border: true,
            nowrap: true,
            iconCls: 'icon-reload',
            url: t.serverUrl + '&action=1',
            idField: 'ID',
            rownumbers: true, //行号 
            toolbar: "#toolbar",
            frozenColumns: [[
                { field: 'ID', checkbox: true },
                { field: 'DepartureName', title: '地区', width: 60, sortable: true, sortName: "DepartureName", align: 'center' },
                {
                    field: 'Name', title: '姓名', width: 80, sortable: true, sortName: "Name", align: 'center', formatter: function (val, rec) {
                        return "<a href='javascript:;' onclick=\"t.fnInfo('" + rec.ID + "')\">" + val + "</a>";
                    }
                },
                { field: 'Mobile', title: '手机号', width: 90, sortable: true, sortName: "Mobile", align: 'center' }
            ]],
            columns: [[
                { field: 'Sex', title: '性别', width: 40, sortable: true, sortName: "Sex", align: 'center' },
                { field: 'GuideLevel', title: '导游等级', align: 'center', width: 90, sortable: true, sortName: "GuideLevel" },
                {
                    field: 'IsIDCard', title: '导游证', align: 'center', width: 50, sortable: true, sortName: "IsIDCard", formatter: function (val, rec) {
                        return val ? "√" : "";
                    }
                },
                {
                    field: 'IsLeaderCard', title: '领队证', align: 'center', width: 50, sortable: true, sortName: "IsLeaderCard", formatter: function (val, rec) {
                        return val ? "√" : "";
                    }
                }
            ]],
            singleSelect: false, //是否单选 
            pagination: true, //分页控件    
            pageSize: 20
        });

        $(window).resize(function () {
            $('#tblData').datagrid('resize');
        });
    },
    fnQuery: function () {
        var DepartureID = $("#Departure").val();
        var key = $("#GuideName").val();
        $('#tblData').datagrid("reload", { "DepartureID": DepartureID, "key": key });
    },
    fnSelected: function () {
        var arr = [];
        var rows = $('#tblData').datagrid('getSelections');
        for (var i = 0; i < rows.length; i++) {
            var json = { "Name": rows[i].Name, "Mobile": rows[i].Mobile, "ID": rows[i].ID };
            arr.push(json);
        }
        if (arr.length == 0) {
            alert("请选择导游");
            return false;
        }
        var api = frameElement.api;
        var W = api.opener;
        W.fnReceiveGuideData(arr);
        api.close();
    }
};

