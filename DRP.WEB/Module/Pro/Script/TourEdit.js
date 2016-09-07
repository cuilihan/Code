var t = {
    fnGetTourVenue: function () { //获取团次的集合地点
        var xml = [];
        $("#tblTourVenue").find("tr").each(function () {
            var arrCell = $(this).find("td");
            var isChk = $("input[type='checkbox']", arrCell[0]).is(':checked');
            var departure = $.trim($(arrCell[1]).text());
            var departureID = $("input[type='hidden']", arrCell[1]).val();
            var venueName = $("input[type='text']", arrCell[2]).val();
            var meetTime = $("input[type='text']", arrCell[3]).val();
            var pickAmt = $("input[type='text']", arrCell[4]).val();
            var sendAmt = $("input[type='text']", arrCell[5]).val();
            if (isChk) {
                xml.push("<data>");
                xml.push("<departure>" + departure + "</departure>");
                xml.push("<departureID>" + departureID + "</departureID>");
                xml.push("<venueName>" + venueName + "</venueName>");
                xml.push("<meetTime>" + meetTime + "</meetTime>");
                xml.push("<pickAmt>" + pickAmt + "</pickAmt>");
                xml.push("<sendAmt>" + sendAmt + "</sendAmt>");
                xml.push("</data>");
            }
        });
        if (xml.length > 0) {
            return "<document>" + xml.join("") + "</document>";
        }
        else return "";
    },
    fnSelectAllVenue: function (self) { //集合地点全选 
        var isChked = $(self).is(':checked');
        if (isChked)
            $("#tblTourVenue").find("input[type='checkbox']").attr("checked", "checked");
        else
            $("#tblTourVenue").find("input[type='checkbox']").removeAttr("checked");
    },
    //#region << 价格策略 >> 
    fnLoadPrice: function (name) { //添加价格策略
        var sb = [];
        if (typeof (name) == undefined)
            name = "";
        var idx = $("#tblPrice").find("tr").size();
        var hid = "<input type='hidden' />";
        sb.push("<tr>");
        sb.push("<td style=\"text-align:center; font-family:Arial;\">" + (idx + 1) + hid + "</td>");
        sb.push("<td><input type=\"text\" class='textbox' style=\"width:98%; height:26px;\" value='" + name + "' /></td>");
        sb.push("<td><input type=\"text\" class=\"checkInt textbox\" style=\"width:60px;height:26px; padding-right:10px;text-align:right;\" /></td>");
        sb.push("<td><input type=\"text\" class=\"checkInt textbox\" style=\"width:60px;height:26px; padding-right:10px;text-align:right;\" /></td>");
        sb.push("<td><input type=\"text\" class=\"checkInt textbox\" style=\"width:60px;height:26px; padding-right:10px;text-align:right;\" /></td>");
        sb.push("<td style=\"text-align:center;\"><input type=\"checkbox\" checked=\"checked\" /></td>");
        var chd = "<input type=\"checkbox\" />";
        if (name.indexOf('儿童') > -1)
            chd = "<input type=\"checkbox\" checked='checked' />";
        sb.push("<td style=\"text-align:center;\">" + chd + "</td>");
        var radio = "<input type=\"radio\" name='p' />";
        if (idx == 0)
            radio = "<input type=\"radio\" name='p' checked=\"checked\" />";
        sb.push("<td style=\"text-align:center;\">" + radio + "</td>");
        sb.push("<td style='text-align:center;'><a href='javascript:;' onclick=\"t.fnDeletePrice(this)\">删除</a></td>");
        sb.push("</tr>");
        $("#tblPrice").append(sb.join(""));
        checkInt();
    },
    fnDeletePrice: function (obj) { //删除价格
        $(obj).parent().parent().remove();
    },
    fnGetTourPrice: function () { //获取价格体系
        var xml = [];
        $("#tblPrice").find("tr").each(function () {
            var tdArr = $(this).find("td");
            var keyID = $(tdArr[0]).find("input[type='hidden']").val();
            var name = $("input[type='text']", tdArr[1]).val();
            var salePrice = $("input[type='text']", tdArr[2]).val();
            var rebate = $("input[type='text']", tdArr[3]).val();
            var roomRate = $("input[type='text']", tdArr[4]).val();
            var isSeat = $("input[type='checkbox']", tdArr[5]).is(':checked');
            var isChild = $("input[type='checkbox']", tdArr[6]).is(':checked');
            var isDefault = $("input[type='radio']", tdArr[7]).is(':checked');
            xml.push("<data>");
            xml.push("<id>" + keyID + "</id>");
            xml.push("<name>" + name + "</name>");
            xml.push("<salePrice>" + salePrice + "</salePrice>");
            xml.push("<rebate>" + rebate + "</rebate>");
            xml.push("<roomRate>" + roomRate + "</roomRate>");
            xml.push("<isSeat>" + (isSeat ? "true" : "false") + "</isSeat>");
            xml.push("<isChild>" + (isChild ? "true" : "false") + "</isChild>");
            xml.push("<isDefault>" + (isDefault ? "true" : "false") + "</isDefault>");
            xml.push("</data>");
        });
        if (xml.length > 0)
            return "<document>" + xml.join("") + "</document>";
        else
            return "";
    },
    fnGetDefaultPrice: function () { //获取默认显示价格
        var ctrl = $("input[type='radio']:checked", "#tblPrice").parent().parent();
        var strPrice = "";
        if (ctrl) {
            strPrice = $("input[type='text']", ctrl.find("td").eq(2)).val();
            if (typeof (strPrice) == "undefined") strPrice = "";
        }
        return strPrice;
    },
    //#endregion
    fnSave: function () {
        var tourID = request("id");
        var TourName = $("#TourName").val();
        var PlanNum = $("#PlanNum").val();
        var ClusterNum = $("#ClusterNum").val();
        var ExpiryDate = $("#ExpiryDate").val();
        var SeatNum = $("#SeatNum").val();
        var xmlVenue = t.fnGetTourVenue();
        var xmlPrice = t.fnGetTourPrice();
        if (TourName == "") {
            Alert("团次名称不能为空");
            return false;
        }
        if (PlanNum == "" || PlanNum == "0") {
            Alert("计划人数不能为空");
            return false;
        }
        if (ExpiryDate == "") {
            Alert("报名截止日期不能为空");
            return false;
        }
        if (xmlPrice == "") {
            Alert("价格体系不能为空");
            return false;
        }
        if (xmlVenue == "") {
            Alert("请选择上车地点");
            return false;
        }
        var u = "Service/TourInfo.ashx?action=5&r" + getRand();
        var json = { "TourID": tourID, "TourName": TourName, "PlanNum": PlanNum, "ClusterNum": ClusterNum, "ExpiryDate": ExpiryDate, "SeatNum": SeatNum, "XmlPrice": xmlPrice, "XmlVenue": xmlVenue };
        dataService.ajaxPost(u, json, function (data) {
            if (data == "1") {
                closeWindow("团次更新成功");
            } else Alert("团次更新失败");
        });
    }
};