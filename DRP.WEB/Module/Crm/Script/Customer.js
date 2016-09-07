$(function () {
    t.init();
});

var t = {
    init: function () {
        t.fnBindData();
        $("#btnQuery").click(function () {
            t.fnQuery();
        });
        $("#btnAdd").click(function () {
            t.fnEdit('');
        });
        $("#btnDel").click(function () {
            return t.fnDeleteNode();
        });
        $("#btnImp").click(function () { //导入客户资料 
            t.fnImportCustomer();
            return false;
        });
        $("#btnExp").click(function () { //导出客户资料
            t.fnExportCustomer();
        });
        $("#ddlDeptID").change(function () {
            $("#ddlUserID option").remove();
            t.fnQueryUser();
        });
        $("#btnSMS").click(function () {
            var arr = [];
            var rows = $('#tblData').datagrid('getSelections');
            if (rows.length > 5) {
                Alert("一键最多群发5条信息！");
            }
            else {
                for (var i = 0; i < rows.length; i++) {
                    var row = rows[i];
                    arr.push(row.Mobile);
                }
                var u = "/Module/My/SendSms.aspx?m=" + arr.join(",");
                return openWindow({ "title": "发消息", "width": "600", "height": "340", "url": u }, function () {
                    t.fnQuery();
                });
            }
        });
    },
    serverUrl: "Service/Customer.ashx?rnd=" + getRand(),
    fnBindData: function () {
        $('#tblData').datagrid({
            loadMsg: "正在加载数据...",
            height: document.documentElement.clientHeight - 32,
            nowrap: true,
            border: 0,
            iconCls: 'icon-reload',
            url: t.serverUrl + '&action=1',
            idField: 'ID',
            rownumbers: true, //行号 
            toolbar: "#toolbar",
            frozenColumns: [[
                { field: 'ID', checkbox: true },
                {
                    field: 'Sms', title: '短信', width: 40, align: 'center', formatter: function (val, rec) {
                        var img = "<img src='/UI/themes/default/images/mobile.png' />";
                        return "<a href='javascript:;' title='发短信' onclick=\"t.fnSms('" + rec.Mobile + "')\">" + img + "</a>";
                    }
                },
                {
                    field: 'Name', title: '客户名称', width: 90, sortable: true, sortName: "Name", align: 'center', formatter: function (val, rec) {
                        return "<a href='javascript:;' onclick=\"t.fnCustomerInfo('" + rec.ID + "','" + rec.Name + "')\">" + val + "</a>";
                    }
                },
                 { field: 'EngName', title: '英文名', width: 80, sortable: true, sortName: "EngName", align: 'center' },
                { field: 'Sex', title: '性别', width: 50, sortable: true, sortName: "Sex", align: 'center' },
                { field: 'Mobile', title: '手机号', width: 100, sortable: true, sortName: "Mobile", align: 'center' },
            ]],
            columns: [[
                { field: 'CustomerType', title: '客户类型', width: 80, sortable: true, sortName: "CustomerType", align: 'center' },
                { field: 'Company', title: '公司名称', width: 120, sortable: true, sortName: "Company" },
                { field: 'TradeNum', title: '消费次数', width: 60, align: 'right', sortable: true, sortName: "TradeNum" },
                {
                    field: 'TradeAmt', title: '消费金额', width: 80, align: 'right', sortable: true, sortName: "TradeAmt", formatter: function (val, rec) {
                        return "<a href=\"javascript:;\" onclick=\"t.fnOrderList('" + rec.ID + "','" + rec.Name + "')\">" + val + "</a>";
                    }
                },
                {
                    field: 'CommunicateNum', title: '沟通次数', width: 60, align: 'right', sortable: true, sortName: "CommunicateNum", formatter: function (val, rec) {
                        return "<a href=\"javascript:;\" onclick=\"t.fnTraceList('" + rec.ID + "')\">" + val + "</a>";
                    }
                },
                { field: 'CreateUserName', title: '创建人', width: 80, sortable: true, align: 'center', sortName: "CreateUserID" },
                 { field: 'CreateDate', title: '创建日期', width: 80, sortable: true, align: 'center', sortName: "CreateDate" },
                {
                    field: 'Opt', title: "操作", width: 120, align: 'center', formatter: function (val, rec) {
                        var btnEdit = "<a href=\"javascript:;\" onclick=\"t.fnEdit('" + rec.ID + "')\">编辑</a>";
                        var btnTrace = "<a href=\"javascript:;\" onclick=\"t.fnAddTrace('" + rec.ID + "')\">添加销售线索</a>";
                        return btnEdit + " | " + btnTrace;
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
        var deptID = $("#ddlDeptID option:selected").val();
        var userID = $("#ddlUserID option:selected").val();
        var customerType = $("#ddlCustomerType option:selected").val();
        var company = $("#txtCompany").val();
        $('#tblData').datagrid("reload", { "key": key, "mobile": mobile, "deptID": deptID, "userID": userID, "customerType": customerType, "Company": company });
    },
    fnCustomerInfo: function (id, name) {
        var title = "查看客户信息-" + name;
        var url = "CustomerInfo.aspx?id=" + id;
        addTab(title, url);
    },
    fnOrderList: function (customerID, customerName) { //查询客户的消费情况       
        var title = "【" + customerName + "】订单查询";
        var url = "TradeOrder.aspx?id=" + customerID;
        addTab(title, url);
    },
    fnAddTrace: function (customerID) { //添加销售线索
        var u = "/Module/Crm/TraceEdit.aspx?id=&customerID=" + customerID;
        return openWindow({ "title": "客户销售线索维护", "width": "550", "height": "350", "url": u }, function () {
            $('#tblData').datagrid("reload");
        });
    },
    fnQueryUser: function () { //根据部门绑定对应的用户
        var deptID = $("#ddlDeptID option:selected").val();
        if (deptID != "") {
            var data = commData.fnQueryUser(deptID);
            if (data != "") {
                var sb = [];
                sb.push("<option value=''>所有</option>")
                $(eval(data)).each(function () {
                    var opt = "<option value='" + this.ID + "'>" + this.Name + "</option>";
                    sb.push(opt);
                });
                $("#ddlUserID").append(sb.join(""));
            }
        }
    },
    fnEdit: function (id) {
        var title = "客户信息维护";
        var url = "CustomerEdit.aspx?id=" + id;
        addTab(title, url, function () { t.fnBindData(); });
    },
    fnImportCustomer: function () { //导入客户资料
        var title = "导入客户资料";
        var url = "CustomerImp.aspx";
        addTab(title, url, function () { t.fnBindData(); });
        return false;
    },
    fnTraceList: function (customerID) { //客户沟通次数
        var title = "客户销售线索明细";
        var url = "CustomerVisit.aspx?customerID=" + customerID;
        addTab(title, url, function () { });
        return false;
    },
    fnExportCustomer: function () { //导出客户资料
        var title = "导出客户资料";
        var url = "CustomerExp.aspx";
        addTab(title, url, function () { });
        return false;
    },
    fnSms: function (mobile) { //发短信
        var u = "/Module/My/SendSms.aspx?m=" + mobile;
        return openWindow({ "title": "发消息", "width": "600", "height": "340", "url": u }, function () {
            t.fnQuery();
        });
    },
    fnDeleteNode: function () {
        var id = getDataGridSelectedRow("tblData");
        if (id == "") {
            Alert("请选择客户");
            return false;
        }
        if (!confirm("确定要删除吗")) return;
        var url = t.serverUrl + "&action=2&id=" + id;
        dataService.ajaxGet(url, function (data) {
            if (data == "1") {
                Notice("删除成功");
                $('#tblData').datagrid("reload");
            }
            else {
                Notice("删除失败");
            }
        })
    }
};

