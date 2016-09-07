//编辑角色
var r = {
    serverUrl: "Service/Role.ashx?r=" + getRand(),
    init: function () {
        r.fnBindDept();
        r.fnQueryRoleMember();
        $("#btnSave").click(function () {
            return r.fnSave();
        });
        $(".lst_toright_single").click(function () {
            r.fnMove("SrcUser", "DesUser");
        });
        $(".lst_toright_all").click(function () {
            r.fnMoveAll("SrcUser", "DesUser");
        });
        $(".lst_toleft_single").click(function () {
            r.fnMove("DesUser", "SrcUser");
        });
        $(".lst_toleft_all").click(function () {
            r.fnMoveAll("DesUser", "SrcUser");
        });
    },
    fnBindDept: function () { //绑定部门
        var url = r.serverUrl + "&action=2";
        $("#txtDept").combotree({
            "url": url,
            onSelect: function (node) {
                var id = node.id;
                r.fnQueryUser(id);
            }
        });
    },
    fnQueryUser: function (deptID) { //查询部门用户
        var url = r.serverUrl + "&action=3&deptID=" + deptID;
        dataService.ajaxGet(url, function (data) {
            if (data != "") {
                $(eval(data)).each(function () {
                    $("#SrcUser").append("<option value='" + this.ID + "'>" + this.Name + "</option>");
                });
            }
        });
    },
    fnQueryRoleMember: function () { //查询角色成员
        var url = r.serverUrl + "&action=6&roleID=" + request("id");
        dataService.ajaxGet(url, function (data) {
            if (data != "") {
                $(eval(data)).each(function () {
                    $("#DesUser").append("<option value='" + this.UserID + "'>" + this.UserName + "</option>");
                });
            }
        });
    },
    fnMove: function (src, des) { //listbox 
        var vSelect = $("#" + src + " option:selected");
        vSelect.clone().appendTo("#" + des);
        vSelect.remove();
    },
    fnMoveAll: function (src, des) { //listbox 
        var vSelect = $("#" + src + " option");
        vSelect.clone().appendTo("#" + des);
        vSelect.remove();
    },
    fnClearItem: function (src) { //清空listbox
        var vSelect = $("#" + src + " option");
        vSelect.remove();
    },
    fnGetListBoxValue: function () { //获取已选择用户
        var arr = [];
        $("#DesUser option").each(function () {
            var v = $(this).val();
            var t = $(this).text();
            var u = v + "|" + t;
            arr.push(u);
        });
        return arr.join(",");
    },
    fnSave: function () { //保存角色
        var roleID = request("id");
        var roleName = $("#RoleName").val();
        var comment = $("#Comment").val();
        var userIDs = r.fnGetListBoxValue();
        if (!$('#form1').form('validate'))
            return false;
        if (roleName == "") {
            Alert("角色名称不能为空");
        }
        if (userIDs == "") {
            if (!confirm("角色成员没有选择，确定要保存吗")) {
                return false;
            }
        }
        var jsonData = { "RoleID": roleID, "RoleName": roleName, "Comment": comment, "UserID": userIDs };
        dataService.ajaxPost(r.serverUrl + "&action=4", jsonData, function (data) {
            if (data == "1") {
                closeWindow("保存成功");
            }
            else {
                Alert("保存失败");
            }
        });
    }
};


$(function () {
    r.init();
});