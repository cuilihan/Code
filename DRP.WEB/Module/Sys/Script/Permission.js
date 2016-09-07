//权限管理
var p = {
    serverUrl: "Service/Permission.ashx?rnd=" + getRand(),
    init: function () {
        p.fnBindData();
        $("#btnSelectAll").click(function () {
            var treeObj = $.fn.zTree.getZTreeObj("treeData");
            treeObj.checkAllNodes(true);
        });
        $("#btnUnSelectAll").click(function () {
            var treeObj = $.fn.zTree.getZTreeObj("treeData");
            treeObj.checkAllNodes(false);
        });
        $("#btnCollapse").click(function () {
            var zTree = $.fn.zTree.getZTreeObj("treeData");
            zTree.expandAll(false);
        });
        $("#btnExpand").click(function () {
            var zTree = $.fn.zTree.getZTreeObj("treeData");
            zTree.expandAll(true);
        });
        $("#rdRoleID").find("input[type='radio']").click(function () {
            var v = $(this).val();
            p.fnSetRolePermission(v);
        });
        $("#btnSave").click(function () { //保存
            p.fnSave();
        });
        p.fnSetNavHeight();
    },
    fnSetNavHeight: function () {
        var h = $(window).height();
        $("#navPermissionID").css("height", h - 120);
        $(window).resize(function () {
            p.fnSetNavHeight();
        });
    },
    fnBindData: function () {
        var setting = {
            check: {
                enable: true
            },
            data: {
                simpleData: {
                    enable: true
                }
            }
        };

        var u = p.serverUrl + "&action=1";
        dataService.ajaxGet(u, function (data) {
            if (data != "") {
                var zNodes = [];
                $(eval(data)).each(function () {
                    var node = { id: this.ID, pId: this.ParentID, name: this.NavName, checked: false, open: false };
                    zNodes.push(node);
                });
                $.fn.zTree.init($("#treeData"), setting, zNodes);
            }
        });
    },
    fnGetRoleID: function () { //角色ID
        var v = $("#rdRoleID").find("input[type='radio']:checked").val();
        if (typeof (v) == "undefined") v = "";
        return v;
    },
    fnGetDataPermission: function () { //数据权限
        var v = $("#rdDataPermissionID").find("input[type='radio']:checked").val();
        if (typeof (v) == "undefined") v = "";
        return v;
    },
    fnGetCrmPermission: function () { //客户管理操作权限
        var total = 0;
        $("#chkCrmPermission").find("input").each(function () {
            var isChked = $(this).is(":checked");
            if (isChked) {
                var v = $(this).val();
                if (typeof (v) != "undefined" && v != "")
                    total += parseInt(v);
            }
        });
        return total;
    },
    fnGetOrderPermission: function () { //订单权限
        var arr = [];
        $("#chkDataPermissionID").find("input").each(function () {
            var isChked = $(this).is(":checked");
            if (isChked) {
                var v = $(this).val();
                arr.push(v);
            }
        });
        return arr.join(",");
    },
    fnGetDataModulePermission: function () { //数据模块显示操作权限
        var total = 0;
        $("#chkDataModuleID").find("input").each(function () {
            var isChked = $(this).is(":checked");
            if (isChked) {
                var v = $(this).val();
                if (typeof (v) != "undefined" && v != "")
                    total += parseInt(v);
            }
        });
        return total;
    },
    fnGetNavPermission: function () { //功能权限
        var arr = [];
        var zTree = $.fn.zTree.getZTreeObj("treeData");
        var checkedTree = zTree.getCheckedNodes(true);
        for (var i = 0; i < checkedTree.length; i++) {
            arr.push(checkedTree[i].id);
        }
        return arr.join(",");
    },
    fnSetRolePermission: function (roleID) { //设置角色权限
        var u = p.serverUrl + "&action=3&roleID=" + roleID + "&r=" + getRand();
        dataService.ajaxGet(u, function (data) {
            if (data != "") {
                var json = eval("(" + data + ")");
                console.log(json)
                var dataPermission = json.DataPermission;
                var orderPermission = json.OrderPermission;
                var navPermission = json.NavPermission;
                var crmPermission = json.CrmPermission;
                var dataModulePermission = json.DataModulePermission;
                 
                //数据权限
                if (!dataPermission) dataPermission = "0";
                $("input[value='" + dataPermission + "']", "#rdDataPermissionID").attr("checked", 'true');

                //客户管理操作Button权限                
                $("#chkCrmPermissionID").find("input").each(function () {
                    var v = $(this).val();
                    var isChked = crmPermission.indexOf(v) > -1;
                    if (isChked) $(this).attr("checked", "checked");
                    else $(this).removeAttr("checked");
                });

                //订单权限
                $("#chkDataPermissionID").find("input").each(function () { 
                    var v = $(this).val();
                    var isChked = orderPermission.indexOf(v) > -1;
                    if (isChked) $(this).attr("checked", "checked");
                    else $(this).removeAttr("checked");
                });

                //数据模块显示
                $("#chkDataModuleID").find("input").each(function () {
                    var v = $(this).val();
                    var isChked = orderPermission.indexOf(v) > -1;
                    if (isChked) $(this).attr("checked", "checked");
                    else $(this).removeAttr("checked");
                });

                //功能菜单
                var treeObj = $.fn.zTree.getZTreeObj("treeData");
                treeObj.checkAllNodes(false);
                $(navPermission).each(function () {
                    treeObj.checkNode(treeObj.getNodeByParam("id", this.NavID, null), true, false);
                });

                //数据模块显示操作权限          
                $("#chkDataModuleID").find("input").each(function () {
                    var v = $(this).val();
                    var isChked = dataModulePermission.indexOf(v) > -1;
                    if (isChked) $(this).attr("checked", "checked");
                    else $(this).removeAttr("checked");
                });
            }
            else {
                $("input[value='0']", "#rdDataPermissionID").attr("checked", 'true');
                $("#chkDataPermissionID").find("input").removeAttr("checked");
                var treeObj = $.fn.zTree.getZTreeObj("treeData");
                treeObj.checkAllNodes(false);
            }
        });
    },
    fnSave: function () { //保存权限
        var roleID = p.fnGetRoleID();
        var dataPermission = p.fnGetDataPermission();
        var orderPermisson = p.fnGetOrderPermission();
        var navPermission = p.fnGetNavPermission();
        var crmPermission = p.fnGetCrmPermission();
        var dataModulePermission = p.fnGetDataModulePermission();
      
        if (roleID == "") {
            Alert("请选择角色");
            return false;
        }
        if (navPermission == "") {
            Alert("请选择功能菜单");
            return false;
        }
        var json = { "RoleID": roleID, "DataPermissoin": dataPermission, "CrmPermission": crmPermission, "OrderPermission": orderPermisson, "NavPermission": navPermission, "DataModulePermission": dataModulePermission };
        var u = p.serverUrl + "&action=2";
        dataService.ajaxPost(u, json, function (data) {
            if (data == "1") {
                Alert("保存成功");
                return false;
            }
            else {
                Alert("保存失败");
                return false;
            }
        });
    }
};


$(function () {
    p.init();
});


