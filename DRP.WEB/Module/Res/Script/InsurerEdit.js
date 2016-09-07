
var c = {
    init: function () {
        $("#btnAddItem").click(function () {
            res.fnAddContact();
        });

        $("#btnSave").click(function () {
            c.fnSaveData();
        });
    },
    serverUrl: "Service/Insurance.ashx",
    fnSaveData: function () { //保存
        var keyID = request("id");
        var name = $("#Name").val();
        var isEnable = $("#IsEnable option:selected").val();
        var contact = $("#Contact").val();
        var mobile = $("#Mobile").val();
        var phone = $("#Phone").val();
        var addr = $("#Addr").val();
        var comment = $("#Remark").val();
        if (name == "") {
            Alert("请填写保险公司名称");
            return false;
        }
        var items = res.fnGetItem();//业务联系人
        var json = { "KeyID": keyID, "Name": name, "IsEnable": isEnable, "Contact": contact, "Mobile": mobile, "Phone": phone, "Addr": addr, "Comment": comment, "Item": items };
        var u = c.serverUrl + "?action=2&r=" + getRand();
        dataService.ajaxPost(u, json, function (data) {
            if (data == "1") {
                closeTab("保存成功", "保险公司维护");
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