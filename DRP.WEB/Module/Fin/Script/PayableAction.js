//付款

$(function () {
    p.init();
});

var p = {
    init: function () {
        $("#ddlDept").change(function () {
            var v = $("#ddlDept option:selected").val();
            $("#ddlCreator option").remove();
            if (v != "") {
                var u = "/Service/CommonData.ashx?action=1&deptID=" + v;
                dataService.ajaxGet(u, function (s) {
                    if (s != "") {
                        var arr = [];
                        arr.push("<option value=''>请选择</option>");
                        $(eval(s)).each(function () {
                            var opt = "<option value='" + this.ID + "'>" + this.Name + "</option>";
                            arr.push(opt);
                        });
                        $("#ddlCreator").append(arr.join(""));
                    }
                });
            }
        });
        p.bindData();
        $("#btnQuery").click(function () {
            p.fnQuery();
        });
        $("#btnSave").click(function () {
            p.fnSave();
        });
    },
    serverURL: "Service/Payable.ashx",
    bindData: function () {
        var supplierID = request("id");
        var u = p.serverURL + "?id=" + supplierID + "&action=2&r=" + getRand();
        $('#tblData').datagrid({
            loadMsg: "正在加载数据...",
            nowrap: true,
            striped: true,
            border: false,
            height: document.documentElement.clientHeight,
            collapsible: false, //是否可折叠的       
            url: u,
            frozenColumns: [[
                 { field: "ID", checkbox: true },
                        { field: 'OrderName', title: '订单名称', width: 200, sortable: true, sortName: "OrderName" },
                        { field: 'OrderNo', title: '订单编号', width: 140, sortable: true, sortName: "OrderNo" },
                        { field: 'TourDate', title: '出团日期', width: 90, align: 'center', sortable: true, sortName: "TourDate" }
            ]],
            columns: [[
                        { field: 'CustomerName', title: "客户名称", width: 80, sortable: true, sortName: "CustomerName", align: 'left' },
                        { field: 'CostAmt', title: "应付款", width: 80, sortable: true, sortName: "CostAmt", align: 'right' },
                        { field: 'PaidAmt', title: '已付款', sortable: true, sortName: "PaidAmt", width: 80, align: 'right' },
                        { field: 'Name', title: '部门', sortable: true, sortName: "DeptName", width: 80, align: 'right' },
                        { field: 'CreateUserName', title: '操作人', sortable: true, sortName: "CreateUserName", width: 80, align: 'right' },
                        {
                            field: 'UnPayable', title: '本次付款', width: 90, align: 'center', formatter: function (val, rec) {
                                return "<input type='text' value='" + rec.UnPayAmt + "' id='payAmt_" + rec.CostID + "' class='checkInt' style='width:75px;color:red; height:22px; text-align:right; padding-right:5px;' />";
                            }
                        },
                        {
                            field: 'PayDate', title: '付款日期', align: 'center', width: 90, formatter: function (val, rec) {
                                return "<input type='text' id='payDate_" + rec.CostID + "' onclick='WdatePicker()' style='width:78px; height:22px;' />";
                            }
                        },
                        {
                            field: 'Comment', title: '付款备注', width: 200, formatter: function (val, rec) {
                                return "<input type='text' id='payComment_" + rec.CostID + "' style='width:190px; height:22px;' />";
                            }
                        }

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
    fnSave: function () { //保存付款
        var supplierID = request("id");
        var supplierName = unescape(request("name"));
        var u = p.serverURL + "?action=3&r=" + getRand();
        var sb = [];
        var rows = $('#tblData').datagrid('getRows');
        for (var i = 0; i < rows.length; i++) {
            var row = rows[i];
            var costID = row.CostID;
            var orderID = row.OrderID;
            var payAmt = $("#payAmt_" + costID).val();
            var payDate = $("#payDate_" + costID).val();
            var payComment = $("#payComment_" + costID).val();

            if (payAmt != "" && payDate != "") {
                sb.push("<data>");
                sb.push("<OrderCostID>" + costID + "</OrderCostID>");
                sb.push("<OrderID>" + orderID + "</OrderID>");
                sb.push("<PayAmt>" + payAmt + "</PayAmt>");
                sb.push("<PayDate>" + payDate + "</PayDate>");
                sb.push("<Comment>" + payComment + "</Comment>");
                sb.push("</data>");
            }
        }
        var xmlData = sb.length == 0 ? "" : "<document>" + sb.join("") + "</document>";      
        if (rows.length == 0) {
            Alert("请选择付款订单");
            return false;
        }
        if (xmlData == "") {
            Alert("付款信息填写不完整，付款金额与付款日期为必填项目");
            return false;
        }
        if (!confirm("确定要保存付款信息吗")) return false;

        var json = { "SupplierID": supplierID, "SupplierName": supplierName, "XmlData": xmlData };
        dataService.ajaxPost(u, json, function (data) {
            if (data == "1") {
                alert("付款保存成功");
                var t = supplierName + "-付款";
                closeTab("", t);
            }
            else {
                Alert("付款操作失败");
            }
        });
    },
    fnQuery: function () {
        var name = $("#txtName").val();
        var sDate = $("#sDate").val();
        var eDate = $("#eDate").val();
        var userName = $("#ddlCreator option:selected").text();
        var orderNo = $("#txtOrderNo").val();
        var deptID = $("#ddlDept option:selected").val();
        if (userName == "请选择") {
            userName = "";
        }
        $('#tblData').datagrid("reload", { "Name": name, "sDate": sDate, "eDate": eDate, "UserName": userName, "OrderNo": orderNo, "DeptID": deptID });
        $("#tblData").datagrid("clearSelections");
    }
};