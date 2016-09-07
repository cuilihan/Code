//订单通用操作
var comm = {
    serverURL: "/Module/Order/Service/OrderUtiltity.ashx",
    fnInitRouteType: function () { //线路类型
        $("#RouteTypeID").combobox({
            mode: 'remote',
            url: comm.serverURL + "?action=1",
            valueField: 'id',
            textField: 'text',
            onSelect: function (rec) {
                var routeTypeID = rec.id;
                if (routeTypeID != "") {
                    comm.fnInitDestination(routeTypeID);
                }
            }
        });
        var rid = $("#hfRouteTypeID").val();
        if (rid != "") {
            $("#RouteTypeID").combobox("setValue", rid);
            comm.fnInitDestination(rid);
        }
    },
    fnInitDestination: function (routeTypeID) { //创建目的地树 
        $('#DestinationID').combotree({
            url: comm.serverURL + "?action=2&routeTypeID=" + routeTypeID + "&r=" + getRand(),
            idField: 'id', textField: 'text'
        });
        var destinationID = $("#hfDestinationID").val();
        if (destinationID != "") {
            $('#DestinationID').combotree("setValue", destinationID);
        }
    },
    fnAddManyCustomer: function (isQYT) { //弹出窗口批量添加客户
        $.dialog({
            cover: true, lock: true,
            width: 450, height: 260, max: false,
            min: false, title: "批量添加客户",
            content: 'url:/Module/Order/BatchAddCustomer.aspx'
        });
        return false;
    },
    fnSelectCustomer: function (isQYT) { //弹出窗口选择客户
        if (typeof (isQYT) == "undefined" || isQYT == "")
            isQYT = false;//是否是企业团
        $.dialog({
            cover: true, lock: true,
            width: 600, height: 460, max: false,
            min: false, title: "选择客户",
            content: 'url:/Module/Crm/SelectCustomer.aspx?xType=' + (isQYT ? "1" : "0")
        });
        return false;
    },
    fnAddCustomer: function (isQYT) { //增加客户
        if (typeof (isQYT) == "undefined" || isQYT == "")
            isQYT = false;//是否是企业团
        var sb = [];
        var i = $("#tblCustomer").find("tr").size() + 1;
        var cid = "<input type='hidden' value=''/>";
        sb.push("<tr>");
        sb.push("<td style='text-align:center;'>" + i.toString() + cid + "</td>");
        sb.push("<td><input type='text' class='textbox' style='height:26px; width:110px;' /></td>");
        sb.push("<td><select style='width:50px; height:26px;'><option value=\"未知\">未知</option><option value=\"男\">男</option><option value=\"女\">女</option></select></td>");
        sb.push("<td><input type='text' class='textbox' style='height:26px; width:130px;' /></td>");
        sb.push("<td><input type='text' class='textbox' style='height:26px; width:170px;' /></td>");
        if (isQYT) { //企业团：公司与是否领队
            sb.push("<td><input type='text' class='textbox' style='height:26px; width:140px;' /></td>");
            sb.push("<td style='text-align:center;'><input type='checkbox'  /></td>");
        }
        else {
            sb.push("<td><input type='text' class='textbox' style='height:26px; width:98%;' /></td>");
        }
        
        sb.push("<td><input type='text' class='textbox' style='height:26px; width:98%;' /></td>");
        sb.push("<td><a href='javascript:;' onclick=\"comm.fnDeleteCustomer(this)\">删除</a></td>");
        sb.push("</tr>");
        $("#tblCustomer").append(sb.join(""));
    },
    fnDeleteCustomer: function (obj) { //删除客户
        $(obj).parent().parent().remove();
    },
    fnGetCustomer: function (isQYT) { //获取客户信息
        if (typeof (isQYT) == "undefined" || isQYT == "")
            isQYT = false;//是否是企业团
        var sb = [];
        $("#tblCustomer").find("tr").each(function () {
            var tdArr = $(this).find("td");
            var customerID = $(tdArr[0]).find("input").eq(0).val();
            var name = $(tdArr[1]).find("input").eq(0).val();
            var sex = $("select", tdArr[2]).val();
            var mobile = $(tdArr[3]).find("input").eq(0).val();
            var idcard = $(tdArr[4]).find("input").eq(0).val();
            var company = "";
            var isLeader = false;
            var comment = "";
            if (isQYT) { //企业团
                company = $(tdArr[5]).find("input").eq(0).val();
                isLeader = $(tdArr[6]).find("input[type='checkbox']").eq(0).is(":checked");
                comment = $(tdArr[7]).find("input").eq(0).val();
            } else {
                var company = $(tdArr[5]).find("input").eq(0).val();
                comment = $(tdArr[6]).find("input").eq(0).val();
            }
            if (name == "" && mobile == "" && comment == "" && idcard == "") { }
            else {
                sb.push("<data>");
                sb.push("<CustomerID>" + customerID + "</CustomerID>");
                sb.push("<Name>" + name + "</Name>");
                sb.push("<Sex>" + sex + "</Sex>");
                sb.push("<Mobile>" + mobile + "</Mobile>");
                sb.push("<IDNo>" + idcard + "</IDNo>");
                sb.push("<Company>" + company + "</Company>");
                sb.push("<IsLeader>" + isLeader + "</IsLeader>");
                sb.push("<Comment>" + comment + "</Comment>");
                sb.push("</data>");
            }
        });
        if (sb.length == 0) return "";
        console.log(sb.join(""))
        return "<document>" + sb.join("") + "</document>";
    },
    fnQuerySupplier: function (itemType) { //查询成本供应商(资源)
        var u = "/Module/Res/Service/Resource.ashx?action=3&itemType=" + itemType;
        var strData = "";
        dataService.ajaxGet(u, function (data) {
            var sb = [];
            sb.push("<select name='supplier' style='width:240px; height:26px;'>");
            sb.push("<option value=''>请选择</option>");
            if (data != "") {
                $(eval(data)).each(function () {
                    var opt = "<option value='" + this.ID + "'>" + this.Spell + "-" + this.Name + "</option>";
                    sb.push(opt);
                });
            }
            sb.push("</select>");
            strData = sb.join("");
        }, false);
        return strData;
    },
    fnAddCostItem: function (itemType, itemName) { //增加成本 
        var arr = [];
        var iSize = $("#tblCostItem").find("tr").size() + 1;
        var hfItemType = "<input type='hidden' value='" + itemType + "' />";//资源类型
        var hfCostItemID = "<input type='hidden' />";//成本表主键ID
        arr.push("<tr>");
        arr.push("<td style='text-align:center;'>" + iSize.toString() + hfCostItemID + "</td>");
        arr.push("<td>" + itemName + hfItemType + "</td>");
        arr.push("<td>" + comm.fnQuerySupplier(itemType) + "</td>");
        arr.push("<td><input name='amt' style='width:90px;height:26px; text-align:right; padding-right:10px;' class='checkInt textbox' /></td>");
        arr.push("<td><input style='width:96%; height:26px;' type='text' class='textbox' /></td>");
        arr.push("<td style='text-align:center;'><a onclick='comm.fnDeleteCostItem(this)' href='javascript:;'>删除</a></td>");
        arr.push("</tr>");
        $("#tblCostItem").append(arr.join(""));
        checkInt();
    },
    fnAddCustomCostItem: function (itemType, itemName) { //增加成本 
        var arr = [];
        var iSize = $("#tblCostItem").find("tr").size() + 1;
        var hfItemType = "<input type='hidden' value='" + itemType + "' />";//资源类型
        var hfCostItemID = "<input type='hidden' />";//成本表主键ID
        arr.push("<tr>");
        arr.push("<td style='text-align:center;'>" + iSize.toString() + hfCostItemID + "</td>");
        arr.push("<td>" + itemName + hfItemType + "</td>");
        arr.push("<td><input name='supplier' style='width:230px; height:26px;' class='checkInt textbox' /></td>");
        arr.push("<td><input name='amt' style='width:90px;height:26px; text-align:right; padding-right:10px;' class='checkInt textbox' /></td>");
        arr.push("<td><input style='width:96%; height:26px;' type='text' class='textbox' /></td>");
        arr.push("<td style='text-align:center;'><a onclick='comm.fnDeleteCostItem(this)' href='javascript:;'>删除</a></td>");
        arr.push("</tr>");
        $("#tblCostItem").append(arr.join(""));
        checkInt();
    },
    fnDeleteCostItem: function (obj) { //删除成本项目
        var tdArr = $(obj).parent().parent().find("td");
        var id = $(tdArr[0]).find("input[type='hidden']").val();
        if (typeof (id) == "undefined") id = "";
        if (id != "") { //从服务器上删除
            var u = "/Module/Order/Service/OrderUtiltity.ashx?action=3&costItemID=" + id;
            dataService.ajaxGet(u, function (data) {
                if (data == "1")
                    $(obj).parent().parent().remove();
                else
                    Alert("删除失败");
            });
        } else
            $(obj).parent().parent().remove();
    },
    fnGetCostItem: function () { //获取订单成本项目
        var xml = [];
        $("#tblCostItem").find("tr").each(function () {
            var tdArr = $(this).find("td");
            var itemID = $("input[type='hidden']", tdArr[0]).val();
            var itemType = $("input[type='hidden']", tdArr[1]).val();
            var itemName = $.trim($(tdArr[1]).text());
            var supplier = $(tdArr[2]).find("select").eq(0).find("option:selected").text();
            var supplierID = $("select", tdArr[2]).val();
            var costAmt = $(tdArr[3]).find("input").eq(0).val();
            var comment = $(tdArr[4]).find("input").eq(0).val();
            if (supplier == "") {
                supplier = $(tdArr[2]).find("input").eq(0).val();
                if (typeof (supplier) == "undefined") {
                    supplier = "";
                }
            }

            if (supplierID == "" && costAmt == "" && comment == "") { }
            else {
                xml.push("<data>");
                xml.push("<ID>" + itemID + "</ID>");
                xml.push("<ItemType>" + itemType + "</ItemType>");
                xml.push("<ItemName>" + itemName + "</ItemName>");
                xml.push("<SupplierID>" + supplierID + "</SupplierID>");
                xml.push("<Supplier>" + supplier + "</Supplier>");
                xml.push("<CostAmt>" + costAmt + "</CostAmt>");
                xml.push("<Comment>" + comment + "</Comment>");
                xml.push("</data>");
            }
        });
        if (xml.length == 0) return "";
        return "<document>" + xml.join("") + "</document>";
    },
    fnUploadFile: function () {  //上传附件
        $("#uploadify").uploadify({
            'swf': '/Scripts/Plugin/Uploadify/uploadify.swf?rnd=' + getRand(),
            'uploader': '/Service/UploadFile.ashx?action=1&r=' + getRand(),
            'buttonText': '上传文件',
            'fileTypeDesc': 'doc,Excel,txt,jpg,png,docx,PDF',
            'fileTypeExts': '*.doc;*.xls;*.txt;*.jpg;*.png;*.jpge;*.gif;*.png;*.docx,*.pdf',
            'fileSizeLimit': '5MB',
            'auto': true,
            'multi': true,
            'onUploadSuccess': function (file, data, response) {
                if (data != "") {
                    var obj = eval("(" + data + ")");
                    if (obj == "1") {
                        Alert("您无权限，请联系管理员!");
                        return false;
                    }
                    var sb = [];
                    var fid = "<input type='hidden' value='" + obj.ID + "' />";
                    sb.push("<tr>");
                    sb.push("<td>" + fid + obj.FileName + "</td>");
                    sb.push("<td style='text-align:center;'>" + obj.FileType + "</td>");
                    sb.push("<td style='text-align:center;'>" + obj.FileSize + "</td>");
                    sb.push("<td style='text-align:center;'>" + obj.CreateDate + "</td>");
                    sb.push("<td style='text-align:center;'>" + obj.CreateUserName + "</td>");
                    sb.push("<td style='text-align:center;'><a href='javascript:;' onclick='comm.fnDeleteFile(this)'>删除</a></td>");
                    sb.push("</tr>");
                    $("#tblFile").append(sb.join(""));
                }
                else {
                    Alert("上传附件失败");
                }
            },
            'onUploadError': function (file, errorCode, errorMsg, errorString) {
                alert(file.name + '上传失败，失败原因： ' + errorString);
            }
        });
    },
    fnDeleteFile: function (obj) { //删除附件行记录
        $(obj).parent().parent().remove();
    },
    fnGetFileID: function () { //获取附件的ID
        var arr = [];
        $("#tblFile").find("input[type='hidden']").each(function () {
            arr.push($(this).val());
        });
        return arr.join(",");
    },
    fnConvertBudgetStatus: function (status) { //预决算状态
        var s = "";
        switch (status) {
            case "1": s = "未预算"; break;
            case "2": s = "已预算"; break;
            case "3": s = "已预算"; break;
            case "4": s = "已预算"; break;
            case "5": s = "已决算"; break;
            case "6": s = "已决算"; break;
            case "7": s = "决算确认"; break;
        }
        return s;
    },
    fnDept: function () { //创建部门树
        $("#Dept").combotree({
            url: comm.serverURL + "?action=4",
            valueField: 'id',
            textField: 'text',
            onSelect: function (rec) {
                var DeptID = rec.id;
                if (DeptID != "") {
                    comm.fnEmployee(DeptID);
                }
            }
        });
        var rid = $("#hfDept").val();
        if (rid != "") {
            $("#Dept").combotree("setValue", rid);
            comm.fnEmployee(rid);
        }
    },
    fnEmployee: function (DeptID) { //创建部门人员树 
        $('#Employee').combobox({
            url: comm.serverURL + "?action=5&DeptID=" + DeptID + "&r=" + getRand(),
            valueField: 'id', textField: 'text'
        });
        var destinationID = $("#hfEmployee").val();
        if (destinationID != "") {
            $('#Employee').combobox("setValue", destinationID);
        }
    }
};


//接收客户信息
function fnReceiveCustomerData(jsonArr, xType) {
    var isQYT = true;
    if (typeof (xType) == "undefined" || xType == "" || xType == "0")
        isQYT = false;

    var sb = [];
    var i = $("#tblCustomer").find("tr").size() + 1;
    if (jsonArr != null && typeof (jsonArr) != "undefined") {
        var sb = [];
        $(jsonArr).each(function () {
            var sex = "<select style='width:50px; height:26px;'>";
            if (this.Sex == "男") {
                sex += "<option value=\"男\" selected='selected'>男</option><option value=\"女\">女</option>";
            }
            else {
                sex += "<option value=\"男\">男</option><option selected='selected' value=\"女\">女</option>";
            }
            sex += "</select>";
            var cid = "<input type='hidden' value='" + this.ID + "'/>";
            sb.push("<tr>");
            sb.push("<td style='text-align:center;'>" + i.toString() + cid + "</td>");
            sb.push("<td><input type='text' class='textbox' style='height:26px; width:110px;' value='" + this.Name + "' /></td>");
            sb.push("<td>" + sex + "</td>");
            sb.push("<td><input type='text' class='textbox' style='height:26px; width:130px;' value='" + this.Mobile + "' /></td>");
            sb.push("<td><input type='text' class='textbox' style='height:26px; width:170px;' value='" + this.IDNum + "' /></td>");
            if (isQYT) { //企业团
                sb.push("<td><input type='text' class='textbox' style='height:26px; width:140px;' value='" + this.Company + "' /></td>");
                sb.push("<td style='text-align:center;'><input type='checkbox'  /></td>");
            }
            else
                sb.push("<td><input type='text' class='textbox' style='height:26px; width:98%;' value='" + this.Company + "' /></td>");
            sb.push("<td><input type='text' class='textbox' style='height:26px; width:98%;'  /></td>");
            sb.push("<td><a href='javascript:;' onclick=\"comm.fnDeleteCustomer(this)\">删除</a></td>");
            sb.push("</tr>");
            i++;
        });
    }
    if (sb.length > 0) {
        $("#tblCustomer").append(sb.join(""));
    }
}
