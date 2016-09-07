//同行散客订单fnGetCostItem
$(function () {
    c.init();
});

var c = {
    init: function () {
        comm.fnDept();
        $("#btnSelectCustomer").bind("click", function () { //选择客户
            comm.fnSelectCustomer();
        });
        $("#btnAddCustomer").click(function () { //增加客户 
            comm.fnAddCustomer();
        });
        $("#btnAddCostItem").click(function () { //增加成本
            var itemType = $("#ddlCostItemType option:selected").val();
            var itemName = $("#ddlCostItemType option:selected").text();
            if (itemType == "") {
                Alert("请选择项目类型");
            } else {
                comm.fnAddCostItem(itemType, itemName);
            }
        });
        $("#btnSave").click(function () { //提交订单
            c.fnSave();
        });
        $("#SourceID").change(function () {
            c.fnDefaultItem();
        });
       
        comm.fnUploadFile(); //上传附件
    },
    url: "Service/OrderInfo.ashx?xType=" + request("xType") + "&r=" + getRand(),
    fnDefaultItem: function () { //设置默认值
        if (request("id") == "") { 
            var itemType = 9;
            var itemName = "票务机构";
            var xType = $("#SourceID option:selected").text();
            if (xType.indexOf("签证") > -1) {
                itemType = 6;
                itemName = "签证机构";
            }
            if (xType.indexOf("门票") > -1) {
                itemType = 2;
                itemName = "景点门票";
            }
            if (xType.indexOf("保险") > -1) {
                itemType = 7;
                itemName = "保险公司";
            }
            if (xType.indexOf("订车") > -1) {
                itemType = 5;
                itemName = "车队";
            } 
            $("#tblCostItem").html("");
            comm.fnAddCostItem(itemType, itemName);
        }
    },
    fnSave: function () { //提交订单  
        var orderName = $("#OrderName").val();
        var tourDate = $("#TourDate").val();
        var sourceID = $("#SourceID option:selected").val();
        var sourceName = $("#SourceID option:selected").text();
        var customerInfo = comm.fnGetCustomer();
        var costInfo = comm.fnGetCostItem();
       
        var remark = $("#Remark").val();
        var orderAmt = $("#OrderAmt").val();
        var participantID = $("#Employee").combobox("getValue");
        var partDeptID = $("#Dept").combotree("getValue");
        var participant = $("#Employee").combobox("getText");
        var deptName = $("#Dept").combotree("getText");
        var fileIDs = comm.fnGetFileID();

        if (orderName == "") {
            Alert("请填写订单名称");
            return false;
        }
        if (orderAmt == "") {
            Alert("请填写应收款");
            return false;
        }
        if (tourDate == "") {
            Alert("请选择订单日期");
            return false;
        }
        if (costInfo == "") {
            Alert("请填写成本项目");
            return false;
        }
        if (customerInfo == "") {
            Alert("请填写客户信息");
            return false;
        }

        var keyID = request("id");
        var orderStatus = $("#OrderStatus").is(":checked");
        $.ajax({
            type: "post",
            url: c.url + "&action=6",
            data: { "ID": keyID, "OrderName": orderName, "TourDate": tourDate, "SourceID": sourceID, "SourceName": sourceName, "CustomerInfo": customerInfo, "CostItem": costInfo, "Remark": remark, "OrderAmt": orderAmt, "OrderStatus": orderStatus ? "2" : "1", "Participant": participant, "DeptName": deptName, "ParticipantID": participantID, "PartDeptID": partDeptID, "FileID": fileIDs },
            beforeSend: function (XMLHttpRequest) {
                $("#btnOpt").addClass("hide");
                $("#tips").removeClass("hide");
            },
            success: function (data, textStatus) {
                $("#tips").addClass("hide");
                if (data == "1") {
                    closeTab("保存成功", "单项业务订单维护");
                }
                else {
                    $("#btnOpt").removeClass("hide");
                    Alert("保存数据失败");
                }
            },
            error: function () {
                $("#btnOpt").removeClass("hide");
                $("#tips").addClass("hide");
                Alert("操作失败");
            }
        });
    }
};
