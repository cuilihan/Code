
var c = {
    init: function () {
        $("#btnAddItem").click(function () {
            res.fnAddContact();
        });

        $("#btnSave").click(function () {
            c.fnSaveData();
        });
    },
    serverUrl: "Service/TicketAgency.ashx",
    fnGetTicketType: function () {
        var sb = [];
        $("#t").find("input").each(function () {
            if ($(this).is(":checked"))
                sb.push($(this).val());
        });
        return sb.join(",");
    },
    fnSaveData: function () { //保存
        var keyID = request("id");
        var name = $("#Name").val();
        var isEnable = $("#IsEnable option:selected").val();
        var ticketType = c.fnGetTicketType();
        var contact = $("#Contact").val();
        var mobile = $("#Mobile").val();
        var phone = $("#Phone").val();
        var addr = $("#Addr").val();
        var comment = $("#Remark").val();
        if (name == "") {
            Alert("请填写票务机构名称");
            return false;
        } 
        var items = res.fnGetItem();//业务联系人
        var json = { "KeyID": keyID, "Name": name, "TicketType": ticketType, "IsEnable": isEnable, "Contact": contact, "Mobile": mobile, "Phone": phone, "Addr": addr, "Comment": comment, "Item": items };
        var u = c.serverUrl + "?action=2&r=" + getRand();
        dataService.ajaxPost(u, json, function (data) {
            if (data == "1") {
                closeTab("保存成功", "票务机构维护");
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