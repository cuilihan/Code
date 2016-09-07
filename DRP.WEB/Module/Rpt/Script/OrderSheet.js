//订单收支明细表

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
    url: "Service/RptUtility.ashx?xType=18",
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
            url: o.url + "&action=15",
            idField: 'ID',
            frozenColumns: [[
                        { field: 'ID', checkbox: true },
                        {
                            field: 'OrderNo', title: "订单编号", width: 140, sortable: true, sortName: "OrderNo", align: 'center'
                        },
                        {
                            field: 'OrderName', title: '订单名称', width: 160, sortable: true, sortName: "OrderName", formatter: function (val, rec) {
                                if (rec.ID) {
                                    switch (rec.OrderType) {
                                        case "1":
                                            //同行散客
                                            if (rec.ID) {
                                                var page = "/Module/Order/tOrderInfo.aspx";
                                                page += "?id=" + rec.ID;
                                                return "<a href='" + page + "' target='_blank' title='查看订单'>" + val + "</a>";
                                            }
                                            break;
                                        case "2":
                                            //自主班散客
                                            if (rec.ID) {
                                                var page = "/Module/Order/zOrderInfo.aspx";
                                                page += "?id=" + rec.ID;
                                                return "<a href='" + page + "' target='_blank' title='查看订单'>" + val + "</a>";
                                            }
                                            break;
                                        case "3":
                                            //企业团
                                            if (rec.ID) {
                                                var page = "/Module/Order/TeamOrderInfo.aspx";
                                                page += "?id=" + rec.ID;
                                                return "<a href='" + page + "' target='_blank' title='查看订单'>" + val + "</a>";
                                            }
                                            break;
                                        case "4":
                                            //自主班团
                                            if (rec.ID) {
                                                var page = "/Module/Order/TourOrderInfo.aspx";
                                                page += "?id=" + rec.ID;
                                                return "<a href='" + page + "' target='_blank' title='查看订单'>" + val + "</a>";
                                            }
                                            break;
                                        case "5":
                                            //单项
                                            if (rec.ID) {
                                                var page = "/Module/Order/BizOrderInfo.aspx";
                                                page += "?id=" + rec.ID;
                                                return "<a href='" + page + "' target='_blank' title='查看订单'>" + val + "</a>";
                                            }
                                            break;
                                        case "6":
                                            //机票
                                            if (rec.ID) {
                                                var page = "/Module/Order/TicetOrderInfo.aspx";
                                                page += "?id=" + rec.ID;
                                                return "<a href='" + page + "' target='_blank' title='查看订单'>" + val + "</a>";
                                            }
                                            break;
                                    }
                                }
                            }
                        },
                        { field: 'TourDate', title: '出团日期', width: 80, align: 'center', sortable: true, sortName: "TourDate" },
                        { field: 'ReturnDate', title: '回程日期', width: 80, align: 'center', sortable: true, sortName: "ReturnDate" }
            ]],
            columns: [[
                      {
                          field: 'OrderType', title: "订单类型", width: 70, sortable: true, sortName: "OrderType", align: 'center', formatter: function (val, rec) {
                              if (rec.ID) {
                                  var s = "";
                                  switch (val.toString()) {
                                      case "1":
                                          s = "散客";
                                          break;
                                      case "2":
                                          s = "散客";
                                          break;
                                      case "3":
                                          s = "团队";
                                          break;
                                      case "4":
                                          s = "团队";
                                          break;
                                      case "5":
                                          s = "单项";
                                          break;
                                      case "6":
                                          s = "机票";
                                          break;
                                  }
                                  return s;
                              }
                          }
                      },
                      {
                          field: 'SupplierName', title: "供应商", width: 140, sortable: true, sortName: "SupplierName", formatter: function (val, rec) {
                              if (rec.ID)
                                  return rec.OrderType.toString() == "2" ? "" : val;
                          }
                      },
                      { field: 'CustomerName', title: '客户', width: 110, sortable: true, sortName: "CustomerName" },
                      { field: 'AdultNum', title: '成人', width: 40, sortable: true, sortName: "AdultNum", align: 'center' },
                      { field: 'ChildNum', title: '儿童', width: 40, sortable: true, sortName: "ChildNum", align: 'center' },
                      { field: 'OrderAmt', title: '应收款', sortable: true, sortName: "Receivable", width: 80, align: 'right' },
                      { field: 'CollectedAmt', title: '已收款', sortable: true, sortName: "CollectedAmt", width: 80, align: 'right' },
                      { field: 'UnCollectedAmt', title: '未收款', sortable: true, sortName: "UnCollectedAmt", width: 80, align: 'right' },
                      { field: 'OrderCost', title: '成本', sortable: true, sortName: "OrderCost", width: 80, align: 'right' },
                      { field: 'Profit', title: '毛利', sortable: true, sortName: "Profit", width: 60, align: 'right' },
                      {
                          field: 'P', title: '毛利率', width: 60, align: 'right', formatter: function (val, rec) {
                              var oAmt = parseFloat(rec.OrderAmt);
                              var profit = parseFloat(rec.Profit);;
                              if (oAmt == 0) return "";
                              else {
                                  var rate = profit / oAmt * 100;
                                  if (rate < 5) return "<span style='color:red;'>" + rate.toFixed(2).toString() + "%</span>";
                                  else return rate.toFixed(2).toString() + "%";
                              }
                          }
                      },
                       { field: 'PaidAmt', title: '已付款', sortable: true, sortName: "PaidAmt", width: 80, align: 'right' },
                       { field: 'UnPaidAmt', title: '未付款', sortable: true, sortName: "UnPaidAmt", width: 80, align: 'right' },
                       { field: 'OrderInvoiceAmt', title: "开票金额", sortable: true, sortName: "OrderInvoiceAmt", width: 80, align: 'right' },
                       { field: 'CreateUserName', title: '提交人', sortable: true, sortName: "CreateUserName", width: 70, align: 'center' },
                       { field: 'Participant', title: '参与人', sortable: true, sortName: "Participant", width: 70, align: 'center' },
                       { field: 'OrderCreateDate', title: '订单创建日期', sortable: true, sortName: "OrderCreateDate", width: 100, align: 'center' }
            ]],
            rowStyler: function (rowIndex, rec) {
                if (rec.OrderStatus == "4")
                    return "color:red;";
            },
            singleSelect: false, //是否单选 
            pagination: true, //分页控件  
            rownumbers: true, //行号  
            pageSize: 20,
            toolbar: "#toolbar",
            showFooter: true
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
        var sDate = $("#sDate").val();
        var eDate = $("#eDate").val();
        var orderType = $("#ddlOrderType option:selected").val();
        var isQueryCanceld = $("#CanceledOrder").prop("checked");
        var createUserName = $("#txtCreateUserName").val();
        var participant = $("#txtParticipant").val();
        var sCreateDate = $("#sCreateDate").val();
        var eCreateDate = $("#eCreateDate").val();

        if (isQueryCanceld)
            status = 4; //已取消订单查询
        else
            status = 0;
        $('#tblData').datagrid("reload", { "OrderType": orderType, "OrderName": orderName, "OrderNo": orderNo, "Customer": customer, "Supplier": supplier, "sDate": sDate, "eDate": eDate, "Status": status, "CreateUserName": createUserName, "Participant": participant, "sCreateDate": sCreateDate, "eCreateDate": eCreateDate });
    }
};