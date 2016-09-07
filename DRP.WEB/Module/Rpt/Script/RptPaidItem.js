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
    url: "Service/RptUtility.ashx?xType=12",
    bindData: function () {
        $('#tblData').datagrid({
            title: '订单付款明细表',
            loadMsg: "正在加载数据...",
            nowrap: true,
            iconCls: "icon-reload",
            striped: true,
            border: true,
            height: document.documentElement.clientHeight - 2,
            collapsible: false, //是否可折叠的       
            url: o.url + "&action=6",
            idField: 'ID',
            frozenColumns: [[
                        { field: 'ID', checkbox: true },
                        { field: 'Amount', title: '付款金额', width: 80, align: 'right', sortable: true, sortName: "Amount" },
                        { field: 'PayDate', title: '付款日期', width: 80, align: 'center', sortable: true, sortName: "PayDate" },
                        { field: 'SupplierName', title: "供应商", width: 140, sortable: true, sortName: "SupplierName" }
            ]],
            columns: [[
                      { field: 'CreateUserName', title: "付款人", width: 80, sortable: true, sortName: "CreateUserName", align: 'center' },
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
                      { field: 'DeptName', title: '业务部门', width: 80, align: 'center', sortable: true, sortName: "DeptName" },
                      { field: 'CreateDate', title: '订单创建日期', width: 100, align: 'center', sortable: true, sortName: "CreateDate" }
            ]],
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
        var supplierType = $("#ddlSupplierType option:selected").val();
        var supplier = $("#txtSupplier").val();
        var dateType = $("#ddlDateType option:selected").val();
        var sDate = $("#sDate").val();
        var eDate = $("#eDate").val();
        var deptID = $("#ddlDept option:selected").val();
        $('#tblData').datagrid("reload", { "OrderName": orderName, "OrderNo": orderNo, "Supplier": supplier, "DateType": dateType, "sDate": sDate, "eDate": eDate, "DeptID": deptID });
    }
};