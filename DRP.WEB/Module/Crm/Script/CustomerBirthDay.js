$(function () {
    t.init();
});

var t = {
    init: function () {
        t.fnBindData();

        $("#toolbar a").click(function () {
            $("#toolbar").find("a").removeClass("on");
            $(this).addClass("on");
            t.fnQuery();
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
        var key = $("#toolbar").find(".on").attr("v");
        $('#tblData').datagrid({
            loadMsg: "正在加载数据...",
            height: document.documentElement.clientHeight - 32,
            nowrap: true,
            border: 0,
            iconCls: 'icon-reload',
            url: t.serverUrl + '&action=9',
            idField: 'ID',
            rownumbers: true, //行号 
            toolbar: "#toolbar",
            queryParams: { "key": key },
            frozenColumns: [[
                { field: 'ID', checkbox: true },

                {
                    field: 'Name', title: '客户名称', width: 90, sortable: true, sortName: "Name", align: 'center', formatter: function (val, rec) {
                        return "<a href='javascript:;' onclick=\"t.fnCustomerInfo('" + rec.ID + "','" + rec.Name + "')\">" + val + "</a>";
                    }
                },
                { field: 'Sex', title: '性别', width: 50, sortable: true, sortName: "Sex", align: 'center' },
                { field: 'Mobile', title: '手机号', width: 100, sortable: true, sortName: "Mobile", align: 'center' }
            ]],
            columns: [[
                { field: 'IDNum', title: '身份证号', width: 150, sortable: true, sortName: "IDNum", align: 'center' },
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
                    field: 'Sms', title: '短信问候', width: 90, align: 'center', formatter: function (val, rec) {
                        var img = "<img src='/UI/themes/default/images/mobile.png' />";
                        return "<a href='javascript:;' title='发短信' onclick=\"t.fnSms('" + rec.Mobile + "')\">" + img + "发短信</a>";
                    }
                },
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
        var key = $("#toolbar").find(".on").attr("v");
        $('#tblData').datagrid("reload", { "key": key });
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
    fnTraceList: function (customerID) { //客户沟通次数
        var title = "客户销售线索明细";
        var url = "CustomerVisit.aspx?customerID=" + customerID;
        addTab(title, url, function () { });
        return false;
    },
    fnSms: function (mobile) { //发短信
        var u = "/Module/My/SendSms.aspx?m=" + mobile;
        return openWindow({ "title": "发消息", "width": "600", "height": "340", "url": u }, function () {
            t.fnQuery();
        });
    }
};

