
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
    },
    serverUrl: "Service/Shopping.ashx",
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
        var isEnable = $("#IsEnable option:selected").val();
        var contact = $("#Contact").val();
        var mobile = $("#Mobile").val();
        var phone = $("#Phone").val();
        var addr = $("#Addr").val();
        var comment = $("#Remark").val();
        if (routeTypeID == "") {
            Alert("请选择区域");
            return false;
        }
        if (destinationID == "") {
            Alert("请选择区域");
            return false;
        }
        if (name == "") {
            Alert("请填写购物店名称");
            return false;
        }
        var items = res.fnGetItem();//业务联系人
        var json = { "KeyID": keyID, "RouteTypeID": routeTypeID, "DestinationID": destinationID, "Name": name, "IsEnable": isEnable, "Contact": contact, "Mobile": mobile, "Phone": phone, "Addr": addr, "Comment": comment, "Item": items };
        var u = c.serverUrl + "?action=2&r=" + getRand();
        dataService.ajaxPost(u, json, function (data) {
            if (data == "1") {
                closeTab("保存成功", "购物店维护");
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