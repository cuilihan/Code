var CTRL_TRACE_TYPE = "";//销售线索机会类型定义

var c = {
    init: function () {
        CTRL_TRACE_TYPE = c.fnTraceType();//销售线索机会类型
        $("#btnAddItem").click(function () {
            c.fnAddTraceItem(1);
        });
        $("#Mobile").blur(function () {
            c.fnValidMobile();
        });
        $("#btnSave").click(function () {
            c.fnSaveData();
        });
        $("#btnIDAdd").click(function () {
            c.fnAddIDInfo("");
        });
        c.fnDefaultIDInfo();
    },
    serverUrl: "/Module/Crm/Service/Customer.ashx",
    fnSetDefaultTraceItem: function () { //设置默认的线索记录条数
        var idx = $("#tblItem").find("tr").size() - 3;
        if (idx <= 0) {
            c.fnAddTraceItem(-1 * idx);
        }
    },
    fnAddTraceItem: function (iCount) { //添加机会线索
        var sb = [];
        var idx = $("#tblItem").find("tr").size() + 1;
        if (CTRL_TRACE_TYPE == "") {
            CTRL_TRACE_TYPE = c.fnTraceType();
        }
        var customerName = $("#Name").val();
        for (var i = 0; i < iCount; i++) {
            sb.push("<tr>");
            var ctrlHidden = "<input type='hidden' />";
            sb.push("<td style='text-align:center;'>" + (idx++) + ctrlHidden + "</td>");
            sb.push("<td><input type='text' class='textbox' style='width:140px; height:26px;'/></td>");
            sb.push("<td>" + CTRL_TRACE_TYPE + "</td>");
            sb.push("<td><input type='text' class='textbox' style='width:90px;height:26px;' value='" + customerName + "'  /></td>");
            sb.push("<td><input type='text' class='textbox' style='width:90px;height:26px;' onclick='WdatePicker()' /></td>");
            sb.push("<td><input type='text' class='textbox' style='width:99%;height:26px;' /></td>");
            sb.push("<td>自动获取</td>");
            sb.push("<td style='text-align:center;'><a href='javascript:;' onclick='c.fnTraceDeleteRow(this)'>删除</a></td>");
            sb.push("</tr>");
        }
        $("#tblItem").append(sb.join(""));
    },
    fnTraceDelete: function (traceID, obj) { //删除销售线索      
        var u = "Service/Customer.ashx?action=6&id=" + traceID;
        dataService.ajaxGet(u, function (data) {
            if (data == "1")
                $(obj).parent().parent().remove();
            else
                Notice("删除失败");
        });
    },
    fnTraceDeleteRow: function (obj) {//删除销售线索(只删除UI的行)
        $(obj).parent().parent().remove();
    },
    fnTraceType: function () { //客户销售机会类型
        var u = c.serverUrl + "?action=3&r=" + getRand();
        var sb = [];
        sb.push("<select style='width:80px;height:26px;'>");
        sb.push("<option value=''>请选择</option>");
        dataService.ajaxGet(u, function (data) {
            if (data != "") {
                $(eval(data)).each(function () {
                    var opt = "<option value='" + this.Name + "'>" + this.Name + "</option>";
                    sb.push(opt);
                });
            }
        }, false);
        sb.push("</select>");
        return sb.join("");
    },
    fnGetTraceItem: function () { //获取销售机会
        var xml = [];
        xml.push("<document>");
        $("#tblItem").find("tr").each(function () {
            var tdArr = $(this).find("td");
            var keyID = $("input[type='hidden']", tdArr[0]).val();
            var name = $("input[type='text']", tdArr[1]).val();
            var type = $("select", tdArr[2]).val();
            var contact = $("input[type='text']", tdArr[3]).val();
            var tradeDate = $("input[type='text']", tdArr[4]).val();
            var comment = $("input[type='text']", tdArr[5]).val();
            if (name == "" && type == "" && contact == "" && tradeDate == "" && comment == "") { }
            else {
                xml.push("<data>");
                xml.push("<keyID>" + keyID + "</keyID>");
                xml.push("<itemName>" + name + "</itemName>");
                xml.push("<itemType>" + type + "</itemType>");
                xml.push("<contact>" + contact + "</contact>");
                xml.push("<tradeDate>" + tradeDate + "</tradeDate>");
                xml.push("<comment>" + comment + "</comment>");
                xml.push("</data>");
            }
        });
        xml.push("</document>");
        return xml.join("");
    },
    fnExistMobile: function () { //验证客户手机号码是否存在（手机号须唯一)
        var rec = false;
        var mobile = $("#Mobile").val();
        if (mobile != "" && mobile.length == 11) {
            var u = c.serverUrl + "?action=4&mobile=" + mobile + "&r=" + getRand();
            dataService.ajaxGet(u, function (data) {
                rec = data == "1";
            }, false);
        }
        return rec;
    },
    fnValidMobile: function () { //验证手机号并提示
        var isExit = c.fnExistMobile();
        if (isExit) {
            $("#tipWraper").removeClass("hide");
        }
        else {
            $("#tipWraper").removeClass("hide").addClass("hide");
        }
    },
    fnDefaultIDInfo: function () { //默信息认证件
        var aSize = $("#tblIDInfo").find("tr").size();

        var u = c.serverUrl + "?action=10&r=" + getRand();
        dataService.ajaxGet(u, function (data) {
            if (data != "") {
                if (aSize == 0) {
                    $(eval(data)).each(function () {
                        c.fnAddIDInfo(this.Name);
                    });
                }
            }
        }, false);
    },
    fnIDDelete: function (obj) {
        $(obj).parent().parent().remove();
    },
    fnAddIDInfo: function (itemName) { //增加证件信息
        var sb = [];
        sb.push("<tr>");
        sb.push("<td><input type='text' style='height:26px;width:190px;' class='textbox' value='" + itemName + "' /></td>");
        sb.push("<td><input type='text' style='height:26px;width:90%;' class='textbox' /></td>");
        sb.push("<td><a href='javascript:;' onclick=\"c.fnIDDelete(this)\">删除</a></td>");
        sb.push("</tr>");
        $("#tblIDInfo").append(sb.join(""));
    },
    fnGetIDInfo: function () { //证件信息 
        var xml = [];
        xml.push("<document>");
        $("#tblIDInfo").find("tr").each(function () {
            var tdArr = $(this).find("td");
            var name = $("input[type='text']", tdArr[0]).val();
            var val = $("input[type='text']", tdArr[1]).val();
            xml.push("<data>");
            xml.push("<itemName>" + name + "</itemName>");
            xml.push("<itemVal>" + val + "</itemVal>");
            xml.push("</data>");
        });
        xml.push("</document>");
        return xml.join("");
    },
    fnSaveData: function () { //保存
        var keyID = request("id");
        var Name = $("#Name").val();
        var CustomerType = $("#CustomerType option:selected").val();
        var Sex = $("#Sex :checked").val();
        var Mobile = $("#Mobile").val();
        var Fax = $("#Fax").val();
        var QQ = $("#QQ").val();
        var IDNum = $("#IDNum").val();
        var Company = $("#Company").val();
        var Addr = $("#Addr").val();
        var Comment = $("#Comment").val();
        var Phone = $("#Phone").val();
        var EngName = $("#EngName").val();
        var BirthDay = $("#BirthDay").val();
        var haveMobile = $("#haveMobile").is(":checked");

        if (Name == "") {
            Alert("客户名称不能为空"); return false;
        }
        //if (Mobile == "") {
        //    Alert("客户手机号不能为空"); return false;
        //}
        //if (Mobile.length != 11) {
        //    Alert("手机号格式填写错误");
        //    return false;
        //}
        if (!$('#form1').form('validate')) {
            return false;
        }
        if (Mobile == "" && haveMobile == false) {
            Alert("手机号不能为空");
            return false;
        }
        if (keyID == "" && Mobile!="") {
            if (c.fnExistMobile()) {
                alert("手机号码存在，不能重复录入");
                return false;
            }
        }
        var items = c.fnGetTraceItem();
        var idInfo = c.fnGetIDInfo();
        var json = { "KeyID": keyID, "Name": Name, "EngName": EngName, "CustomerType": CustomerType, "Sex": Sex, "Mobile": Mobile, "Phone": Phone, "Fax": Fax, "QQ": QQ, "IDNum": IDNum, "Company": Company, "Addr": Addr, "Comment": Comment, "Item": items, "IDInfo": idInfo, "BirthDay": BirthDay };
        var u = c.serverUrl + "?action=5&r=" + getRand();
        dataService.ajaxPost(u, json, function (data) {
            if (data == "1") {
                closeTab("保存成功", "客户信息维护");
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