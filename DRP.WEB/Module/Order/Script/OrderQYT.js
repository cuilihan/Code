//企业团订单 
$(function () {
    c.init();
});

var c = {
    init: function () {
        comm.fnDept();
        comm.fnInitRouteType();//目的地  
        $("#btnSelectCustomer").bind("click", function () { //选择客户
            comm.fnSelectCustomer(true);
        });
        //if (request("id") == "")
        //    comm.fnAddCustomer(true);
        $("#btnAddCustomer").click(function () { //增加客户 
            comm.fnAddCustomer(true);
        });
        $("#btnAddManyCustomer").bind("click", function () { //批量增加客户 
            comm.fnAddManyCustomer(true);
        });
        $("#btnSave").click(function () { //提交订单
            c.fnSave(false);
        });
        $("#btnSaveNext").click(function () { //提交订单并预算
            c.fnSave(true);
        });
        KindEditor.ready(function (K) {
            K.create('#Schedule', {
                resizeType: 1,
                height: 300,
                allowPreviewEmoticons: false,
                allowImageUpload: false,
                afterBlur: function () { this.sync(); },
                items: c.toolbarItems
            });
        });

        comm.fnUploadFile(); //上传附件
    },
    toolbarItems: ['fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline',
                  'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist',
                  'insertunorderedlist', '|', 'emoticons', 'link', '|', 'plainpaste', 'source'],
    url: "Service/OrderInfo.ashx?xType=3&r=" + getRand(),
    fnSave: function (isBudget) { //提交订单 
        var routeTypeID = $("#RouteTypeID").combobox("getValue");
        var destinationID = $("#DestinationID").combotree("getValue");
        var orderName = $("#OrderName").val();
        var tourDate = $("#TourDate").val();
        var tourDays = $("#TourDays").val();
        var sourceID = $("#SourceID option:selected").val();
        var sourceName = $("#SourceID option:selected").text();
        var venueName = $("#VenueName").val();
        var collectTime = $("#CollectTime").val();
        var customerInfo = comm.fnGetCustomer(true);
        var schedule = $("#Schedule").val();
        var remark = $("#Remark").val();
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
        if (customerInfo == "") {
            Alert("请填写客户信息");
            return false;
        }
        var keyID = request("id");
        var orderStatus = $("#OrderStatus").is(":checked");
        $.ajax({
            type: "post",
            url: c.url + "&action=7",
            data: { "ID": keyID, "OrderType": "3", "RouteTypeID": routeTypeID, "DestinationID": destinationID, "OrderName": orderName, "TourDate": tourDate, "TourDays": tourDays, "SourceID": sourceID, "SourceName": sourceName, "VenueName": venueName, "CollectTime": collectTime, "CustomerInfo": customerInfo, "Schedule": schedule, "Remark": remark, "OrderStatus": orderStatus ? "2" : "1", "Participant": participant, "DeptName": deptName, "ParticipantID": participantID, "PartDeptID": partDeptID, "FileID": fileIDs },
            beforeSend: function (XMLHttpRequest) {
                $("#btnOpt").addClass("hide");
                $("#tips").removeClass("hide");
            },
            success: function (data, textStatus) {
                $("#tips").addClass("hide");
                if (data == "0" || data == "") {
                    $("#btnOpt").removeClass("hide");
                    Alert("保存数据失败");
                }
                else {
                    if (isBudget) { //保存后编辑预算
                        window.location.href = "OrderBudget.aspx?xType=3&id=" + data;
                    } else
                        closeTab("保存成功", "企业团订单维护");
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
