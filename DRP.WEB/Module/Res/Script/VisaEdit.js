
var c = {
    init: function () {
        $("#btnAddItem").click(function () {
            res.fnAddContact();
        });

        $("#btnSave").click(function () {
            c.fnSaveData();
        });
    },
    serverUrl: "Service/Visa.ashx",
    fnSaveData: function () { //保存
        var keyID = request("id");
        var name = $("#Name").val();
        var isEnable = $("#IsEnable option:selected").val();
        var contact = $("#Contact").val();
        var mobile = $("#Mobile").val();
        var phone = $("#Phone").val();
        var addr = $("#Addr").val();
        var comment = $("#Remark").val();
        var qq = $("#QQ").val();
        var wechat = $("#Wechat").val();
        var bizType = $("#BizType").val();
        var bankInfo = $("#BankInfo").val();

        if (name == "") {
            Alert("请填写签证机构名称");
            return false;
        }
        var items = res.fnGetItem();//业务联系人
        var json = { "KeyID": keyID, "Name": name, "IsEnable": isEnable, "Contact": contact, "Mobile": mobile, "Phone": phone, "Addr": addr, "Comment": comment, "Item": items, "QQ": qq, "Wechat": wechat, "BizType": bizType, "BankInfo": bankInfo };
        var u = c.serverUrl + "?action=2&r=" + getRand();
        dataService.ajaxPost(u, json, function (data) {
            if (data == "1") {
                closeTab("保存成功", "签证机构维护");
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