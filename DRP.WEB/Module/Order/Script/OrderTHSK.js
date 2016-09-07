//同行散客订单fnGetCostItem
$(function () {
    c.init();
});

var c = {
    init: function () {
        comm.fnInitRouteType();//目的地 
        comm.fnDept();
        c.fnDefaultItem();
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

        comm.fnUploadFile(); //上传附件
    },
    url: "Service/OrderInfo.ashx?xType=" + request("xType") + "&r=" + getRand(),
    fnDefaultItem: function () { //设置默认值
        if (request("id") == "") {
           // comm.fnAddCustomer();
            comm.fnAddCostItem("1", "供应商");
            comm.fnAddCostItem("7", "保险公司");
        }
    }, 
    fnSave: function () { //提交订单 
        var routeTypeID = $("#RouteTypeID").combobox("getValue");
        var destinationID = $("#DestinationID").combotree("getValue");
        var orderName = $("#OrderName").val();
        var tourDate = $("#TourDate").val();
        var tourDays = $("#TourDays").val();
        var sourceID = $("#SourceID option:selected").val();
        var sourceName = $("#SourceID option:selected").text();
        var customerInfo = comm.fnGetCustomer();
        var costInfo = comm.fnGetCostItem();
        var remark = $("#Remark").val();
        var adultNum = $("#AdultNum").val()
        var childNum = $("#ChildNum").val();
        var orderAmt = $("#OrderAmt").val();
        var participantID = $("#Employee").combobox("getValue");
        var partDeptID = $("#Dept").combotree("getValue");
        var participant = $("#Employee").combobox("getText");
        var deptName = $("#Dept").combotree("getText");
        var fileIDs = comm.fnGetFileID();

        if (routeTypeID == "") {
            Alert("请选择目的地所属线路类型");
            return false;
        }
        if (destinationID == "") {
            Alert("请选择目的地");
            return false;
        }
        if (orderName == "") {
            Alert("请填写订单名称");
            return false;
        }
        if (tourDate == "") {
            Alert("请选择出团日期");
            return false;
        }
        if (tourDays == "") {
            Alert("请填写行程天数");
            return false;
        }
        if (customerInfo == "") {
            Alert("请填写客户信息");
            return false;
        }
        if (costInfo == "") {
            Alert("请填写成本项目");
            return false;
        }
        if (adultNum == "") {
            Alert("请填写成人人数");
            return false;
        }
        if (orderAmt == "") {
            Alert("请填写应收款");
            return false;
        }
        var keyID = request("id");
        var orderStatus = $("#OrderStatus").is(":checked");
        $.ajax({
            type: "post",
            url: c.url + "&action=2",
            data: { "ID": keyID, "OrderType": request("xType"), "RouteTypeID": routeTypeID, "DestinationID": destinationID, "OrderName": orderName, "TourDate": tourDate, "TourDays": tourDays, "SourceID": sourceID, "SourceName": sourceName, "CustomerInfo": customerInfo, "CostItem": costInfo, "Remark": remark, "AdultNum": adultNum, "ChildNum": childNum, "OrderAmt": orderAmt, "OrderStatus": orderStatus ? "2" : "1", "Participant": participant, "DeptName": deptName, "ParticipantID": participantID, "PartDeptID": partDeptID, "FileID": fileIDs },
            beforeSend: function (XMLHttpRequest) {
                $("#btnOpt").addClass("hide");
                $("#tips").removeClass("hide");
            },
            success: function (data, textStatus) {
                $("#tips").addClass("hide");
                if (data == "1") {
                    closeTab("保存成功", "同行订单维护");
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
