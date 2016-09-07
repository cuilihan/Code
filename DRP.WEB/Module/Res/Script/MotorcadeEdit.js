
var c = {
    init: function () {
        $("#btnAddItem").click(function () {
            res.fnAddContact();
        });

        $("#btnSave").click(function () {
            c.fnSaveData();
        });
        comm.fnUploadFile(); //上传附件
    },
    serverUrl: "Service/Motorcade.ashx",
    fnSaveData: function () { //保存
        var keyID = request("id");
        var departureID = $("#DepartureID option:selected").val(); 
        var name = $("#Name").val();
        var scale = $("#Scale option:selected").val();
        var isEnable = $("#IsEnable option:selected").val();
        var contact = $("#Contact").val();
        var title = $("#Title").val();
        var mobile = $("#Mobile").val();
        var mail = $("#Mail").val();
        var phone = $("#Phone").val();
        var fax = $("#Fax").val(); 
        var addr = $("#Addr").val();
        var comment = $("#Remark").val();
        var fileIDs = comm.fnGetFileID();

        if (departureID == "") {
            Alert("请选择地区");
            return false;
        } 
        if (name == "") {
            Alert("请填写车队名称");
            return false;
        }
        var items = res.fnGetItem();//业务联系人
        var json = { "KeyID": keyID, "DepartureID": departureID, "Name": name, "Scale": scale, "IsEnable": isEnable, "Contact": contact, "Title": title, "Mobile": mobile, "Phone": phone, "Fax": fax, "Mail": mail, "Addr": addr, "Comment": comment, "Item": items, "FileID": fileIDs };
        var u = c.serverUrl + "?action=2&r=" + getRand();
        dataService.ajaxPost(u, json, function (data) {
            if (data == "1") {
                closeTab("保存成功", "车队维护");
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