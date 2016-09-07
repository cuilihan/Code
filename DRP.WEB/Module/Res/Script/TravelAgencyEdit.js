
var c = {
    init: function () {
        $("#btnAddItem").click(function () {
            res.fnAddContact();
        });

        $("#btnSave").click(function () {
            c.fnSaveData();
        });

        res.fnInitRouteType(); //加载区域

        c.fnSetValue();
        comm.fnUploadFile(); //上传附件
    },
    serverUrl: "Service/TravelAgency.ashx",
    fnSetValue: function () { //设置区域ID
        var routeTypeID = $("#hfRouteTypeID").val();
        if (routeTypeID != "") {
            $("#RouteTypeID").combobox("setValue", routeTypeID);
            var destinationID = $("#hfDestinationID").val();
            res.fnInitDestination(routeTypeID);
            if (destinationID != "") {
                $("#DestinationID").combotree("setValue", destinationID);
            }
        }
    },
    fnSaveData: function () { //保存
        var keyID = request("id");
        var routeTypeID = $("#RouteTypeID").combobox("getValue");
        var destinationID = $("#DestinationID").combotree("getValue");
        var name = $("#Name").val();
        var brand = $("#Brand").val();
        var isEnable = $("#IsEnable option:selected").val();
        var contact = $("#Contact").val();
        var title = $("#Title").val();
        var mobile = $("#Mobile").val();
        var mail = $("#Mail").val();
        var phone = $("#Phone").val();
        var fax = $("#Fax").val();
        var qq = $("#QQ").val();
        var bankName = $("#BankName").val();
        var bankAcct = $("#BankAcct").val();
        var addr = $("#Addr").val();
        var bizType = $("#BizType option:selected").val();
        var comment = $("#Remark").val();
        var fileIDs = comm.fnGetFileID();

        if (routeTypeID == "") {
            Alert("请选择线路类型");
            return false;
        }
        if (destinationID == "") {
            Alert("请选择区域");
            return false;
        }
        if (name == "") {
            Alert("请填写供应商名称");
            return false;
        }
        if (bizType == "") {
            Alert("请选择供应商类型");
            return false;
        }
        var items = res.fnGetItem();//业务联系人
        var json = { "KeyID": keyID, "RouteTypeID": routeTypeID, "DestinationID": destinationID, "Name": name, "Brand": brand, "IsEnable": isEnable, "Contact": contact, "Title": title, "Mobile": mobile, "Phone": phone, "Fax": fax, "Mail": mail, "QQ": qq, "BankName": bankName, "BankAcct": bankAcct, "Addr": addr, "BizType": bizType, "Comment": comment, "Item": items, "FileID": fileIDs };
        var u = c.serverUrl + "?action=2&r=" + getRand();
        dataService.ajaxPost(u, json, function (data) {
            if (data == "1") {
                closeTab("保存成功", "供应商维护");
            }
            else {
                Alert("保存失败");
            }
        });
    }
};


$(function () {
    c.init();
});