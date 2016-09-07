$(function () {
    t.init();
});

var t = {
    init: function () {
        t.fnBindData();
        $("#btnQuery").click(function () {
            t.fnQuery();
        });
        $("#btnOk").click(function () { //确定选择的客户 
            t.fnSelected();
        });
    },
    serverUrl: "Service/Customer.ashx?xType=1&rnd=" + getRand(),
    fnBindData: function () {
        $('#tblData').datagrid({
            loadMsg: "正在加载数据...",
            height: 457,
            nowrap: true,
            border: true,
            iconCls: 'icon-reload',
            url: t.serverUrl + '&action=11',
            idField: 'ID',
            rownumbers: true, //行号 
            toolbar: "#toolbar",
            frozenColumns: [[
                { field: 'ID', checkbox: true },
                {
                    field: 'Name', title: '客户名称', width: 80, sortable: true, sortName: "Name", align: 'center', formatter: function (val, rec) {
                        var ticketType = request("t");
                        var name = val;
                        if (ticketType == "1" && rec.EngName != "") { //国际机票选择 
                            name = rec.EngName;
                        }
                        return name;
                    }
                },
                { field: 'Sex', title: '性别', width: 40, sortable: true, sortName: "Sex", align: 'center' },
                { field: 'Mobile', title: '手机号', width: 100, sortable: true, sortName: "Mobile", align: 'center' }
            ]],
            columns: [[
                { field: 'Company', title: '公司名称', width: 150, sortable: true, sortName: "Company" },
                {
                    field: 'IDNum', title: '证件信息', width: 130, sortable: true, sortName: "IDNum", formatter: function (val, rec) {
                        var ticketType = request("t");
                        if (ticketType == "1") { //国际机票选择
                            return rec.CustomerCertificate;
                        }
                        else { //通用性选择客户，以身份证为主
                            return val;
                        }
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
        var key = $("#txtName").val();
        var mobile = $("#txtMobile").val();
        $('#tblData').datagrid("reload", { "key": key, "mobile": mobile });
    },
    fnSelected: function () {
        var arr = [];
        var rows = $('#tblData').datagrid('getSelections');
        for (var i = 0; i < rows.length; i++) {
            var json = { "Name": rows[i].Name, "EngName": rows[i].EngName, "Mobile": rows[i].Mobile, "ID": rows[i].ID, "Sex": rows[i].Sex, "Company": rows[i].Company, "IDNum": rows[i].IDNum };
            arr.push(json);
        }
        if (arr.length == 0) {
            alert("请选择客户");
            return false;
        }
        var api = frameElement.api;
        var W = api.opener;
        var xType = request("xType");//1:企业团
        W.fnReceiveCustomerData(arr, xType);
        api.close();
    }
};

