$(function () {
    o.init();
});

var o = {
    init: function () {
        o.clickEvent();
        o.bindData();
    },
    clickEvent: function () {
        $("#btnQuery").click(function () {
            o.fnQuery();
        });
    },
    url: "Service/RptUtility.ashx?xType=11",
    bindData: function () {
        $('#tblData').datagrid({
            title: '订单收款明细表',
            loadMsg: "正在加载数据...",
            nowrap: true,
            iconCls: "icon-reload",
            striped: true,
            border: true,
            height: document.documentElement.clientHeight - 2,
            collapsible: false, //是否可折叠的       
            url: o.url + "&action=7",
            idField: 'ID',
            frozenColumns: [[
                        { field: 'ID', checkbox: true },
                        { field: 'CollectDate', title: '收款日期', width: 80, align: 'center', sortable: true, sortName: "CollectDate" },
                        { field: 'CollectAmt', title: '收款金额', width: 80, align: 'right', sortable: true, sortName: "CollectAmt" },
                        { field: 'CollectBill', title: "收据编号", width: 70, sortable: true, sortName: "CollectBill", align: 'center' },
                        { field: 'CreateUserName', title: "收款人", width: 70, sortable: true, sortName: "CreateUserName", align: 'center' }
            ]],
            columns: [[
                      {
                          field: 'OrderType', title: "订单类型", width: 80, sortable: true, sortName: "OrderType", align: 'center', formatter: function (val, rec) {
                              var s = "";
                              switch (val.toString()) {
                                  case "1":
                                      s = "同行散客";
                                      break;
                                  case "2":
                                      s = "自主班散客";
                                      break;
                                  case "3":
                                      s = "企业团";
                                      break;
                                  case "4":
                                      s = "自主班团";
                                      break;
                                  case "5":
                                      s = "单项业务";
                                      break;
                              }
                              return s;
                          }
                      },
                      { field: 'OrderNo', title: "订单编号", width: 140, sortable: true, sortName: "OrderNo", align: 'center' },
                      { field: 'OrderName', title: '订单名称', width: 160, sortable: true, sortName: "OrderName" },
                      { field: 'TourDate', title: '订单日期', width: 80, align: 'center', sortable: true, sortName: "TourDate" },
                      { field: 'SupplierName', title: "供应商", width: 140, sortable: true, sortName: "SupplierName" },
                      { field: 'CustomerName', title: '客户', width:110,  sortable: true, sortName: "CustomerName" },
                      { field: 'DeptName', title: '业务部门', width: 80, align: 'center', sortable: true, sortName: "DeptName" },
                      { field: 'CreateDate', title: '创建日期', width: 80, align: 'center', sortable: true, sortName: "CreateDate" },
            ]],
            rowStyler: function (rowIndex, rec) {
                if (rec.OrderStatus == "4")
                    return "color:red;";
            },
            singleSelect: false, //是否单选 
            pagination: true, //分页控件  
            rownumbers: true, //行号  
            pageSize: 20,
            toolbar: "#toolbar"
        });

        $(window).resize(function () {
            $('#tblData').datagrid('resize');
        });
    },
    fnQuery: function () {
        var orderName = $("#txtOrderName").val();
        var orderNo = $("#txtOrderNo").val();
        var customer = $("#txtCustomer").val();
        var supplier = $("#txtSupplier").val();
        var dateType = $("#ddlDateType option:selected").val();
        var sDate = $("#sDate").val();
        var eDate = $("#eDate").val();
        var deptID = $("#ddlDept option:selected").val();
        var orderType = $("#ddlOrderType option:selected").val();
        $('#tblData').datagrid("reload", { "OrderType": orderType, "OrderName": orderName, "OrderNo": orderNo, "Customer": customer, "Supplier": supplier, "DateType": dateType, "sDate": sDate, "eDate": eDate, "DeptID": deptID });
    }
};