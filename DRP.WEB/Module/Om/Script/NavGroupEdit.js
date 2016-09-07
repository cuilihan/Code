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
function fnLoadNavigate() {
    var u = "Service/NavGroup.ashx?action=3&nid=" + request("id")+"&r="+getRand();
    dataService.ajaxGet(u, function (data) {
        if (data != "") {
            var zNodes = []; 
            $(eval(data)).each(function () {
                var nid = this.NavID;
                var chk = true;
                if (typeof (nid) == "undefined" || nid == "")
                    chk = false; 
                var node = { id: this.ID, pId: this.ParentID, name: this.NavName, checked: chk, open: false };
                zNodes.push(node);
            }); 
            $.fn.zTree.init($("#treeData"), setting, zNodes);
        }
    });
}

//全选
function fnCheckAllNodes() {
    var treeObj = $.fn.zTree.getZTreeObj("treeData");
    treeObj.checkAllNodes(true);
}

//全取消
function fnCancelAllNodes() {
    var treeObj = $.fn.zTree.getZTreeObj("treeData");
    treeObj.checkAllNodes(false);
}


function fnGetNavID() {
    var arr = [];
    var zTree = $.fn.zTree.getZTreeObj("treeData");
    var checkedTree = zTree.getCheckedNodes(true);
    for (var i = 0; i < checkedTree.length; i++) {
        arr.push(checkedTree[i].id);
    }
    return arr.join(",");
}


function fnSave() {
    var v = $('#form1').form('validate');
    if (v) {
        var navIDs = fnGetNavID();
    }
    var navGroup = $("#NavGroup").val();
    var comment = $("#Comment").val();
    var orderIndex = $("#OrderIndex").val();
    if (navGroup == "") {
        Alert("导航名称不能为空");
        return false;
    }
    if (navIDs == "") {
        if (!confirm("导航组的功能菜单没有选择，确定要保存吗"))
            return false;
    }
    var u = "Service/NavGroup.ashx?action=4&rnd=" + getRand();
    var data = { "NavGroupID": request("id"), "NavGroup": navGroup, "Comment": comment, "OrderIndex": orderIndex, "NavigateID": navIDs };
    dataService.ajaxPost(u, data, function (data) {
        if (data == "1") {
            closeWindow("保存成功");
        } else {
            Alert("保存失败");
        }
    });
}

$(function () {
    fnLoadNavigate();
    $("#btnSave").click(function () {
        return fnSave();
    });
    $("#btnSelectAll").click(function () {
        fnCheckAllNodes();
    });
    $("#btnUnSelect").click(function () {
        fnCancelAllNodes();
    });
});