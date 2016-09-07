$(function () {
    t.init();
});

var t = {
    init: function () {
        t.fnBindData();
        $("#btnQuery").click(function () {
            t.fnBindData();
        });
        $(".searcher_category").find("a").click(function () {
            var itemType = $(this).attr("id");
            $(this).addClass("search_on").siblings().removeClass("search_on");
            t.fnBindData();
        });
    },
    serverUrl: "Service/Searcher.ashx?action=3&rnd=" + getRand(),
    fnBindData: function () {
        var id = $(".search_on").attr("id");
        var key = $("#Name").val();
        var u = t.serverUrl + "&id=" + encodeURI(id) + "&key=" + encodeURI(key);
        $('#tblData').datagrid({
            loadMsg: "正在加载数据...",
            height: document.documentElement.clientHeight - 32,
            border: 0,
            nowrap: true,
            iconCls: 'icon-reload',
            url: u,
            idField: 'ID',
            rownumbers: true, //行号 
            toolbar: "#toolbar",
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
                { field: 'CustomerType', title: '客户类型', width: 120, sortable: true, sortName: "CustomerType", align: 'center' },
                { field: 'Company', title: '公司名称', width: 150, sortable: true, sortName: "Company" },
                { field: 'TradeNum', title: '消费次数', width: 80, align: 'right', sortable: true, sortName: "TradeNum" },
                {
                    field: 'TradeAmt', title: '消费金额', width: 80, align: 'right', sortable: true, sortName: "TradeAmt", formatter: function (val, rec) {
                        return "<a href=\"javascript:;\" onclick=\"t.fnOrderList('" + rec.ID + "','" + rec.Name + "')\">" + val + "</a>";
                    }
                },
                {
                    field: 'CommunicateNum', title: '沟通次数', width: 80, align: 'right', sortable: true, sortName: "CommunicateNum", formatter: function (val, rec) {
                        return "<a href=\"javascript:;\" onclick=\"t.fnTraceList('" + rec.ID + "')\">" + val + "</a>";
                    }
                },
                { field: 'CreateUserName', title: '创建人', width: 80, sortable: true, align: 'center', sortName: "CreateUserID" }
            ]],
            singleSelect: true, //是否单选 
            pagination: true, //分页控件    
            pageSize: 20
        });

        $(window).resize(function () {
            $('#tblData').datagrid('resize');
        });
    },
    fnCustomerInfo: function (id, name) {
        var title = "查看客户信息-" + name;
        var url = "/Module/Crm/CustomerInfo.aspx?id=" + id;
        addTab(title, url);
    },
    fnTraceList: function (customerID) { //客户沟通次数
        var title = "客户销售线索明细";
        var url = "/Module/Crm/CustomerVisit.aspx?customerID=" + customerID;
        addTab(title, url, function () { });
        return false;
    },
    fnOrderList: function (customerID, customerName) { //查询客户的消费情况       
        var title = "【" + customerName + "】订单查询";
        var url = "/Module/Crm/TradeOrder.aspx?id=" + customerID;
        addTab(title, url);
    },
};

