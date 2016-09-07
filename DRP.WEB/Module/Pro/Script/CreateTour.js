$(function () {
    t.init();
});

var t = {
    init: function () {
        t.fnTourRuleChange();
        t.fnSetDefaultPrice();
        checkInt();
        $("#btnSave").click(function () {
            t.fnSaveData();
        });
    },
    //#region << 开班规则 >>
    fnTourRuleChange: function () {
        $("#ul_tabs").find("li:first").addClass("thistab").show();
        $("#ul_tabs").find("li").bind("click", function () {
            $(this).addClass("thistab").siblings("li").removeClass("thistab");
            var v = $(this).prop("tabindex").toString();
            switch (v) {
                case "1"://按天数
                    $("#tagContent").html("");
                    str = "每隔 <input type='text' value='0' class='textbox checkInt' style='width:30px; height:26px;text-align:center;' id='txtIntervalDay'  />";
                    str += " 天发团，如每隔0天发团则表示天天都发团";
                    $("#tagContent").html(str);
                    checkInt();
                    break;
                case "2"://按周几
                    $("#tagContent").html("");
                    str = t.fnWeekDayItem("1", "周一");
                    str += t.fnWeekDayItem("2", "周二");
                    str += t.fnWeekDayItem("3", "周三");
                    str += t.fnWeekDayItem("4", "周四");
                    str += t.fnWeekDayItem("5", "周五");
                    str += t.fnWeekDayItem("6", "周六");
                    str += t.fnWeekDayItem("0", "周日");
                    $("#tagContent").html(str);
                    checkInt();
                    break;
                case "3"://按日期
                    $("#tagContent").html("");
                    t.fnTourCalendar();
                    break;
            }
            return false;
        });
    },
    fnWeekDayItem: function (v, t) {
        return "<span style='display:inline-block; padding-right:2em;'><input type='checkbox' value='" + v + "'/>" + t + "</span>";
    },
    fnGetSelectedWeekDay: function () { //获取已选择周几
        var arrWeek = [];
        $("#tagContent").find("input[type='checkbox']:checked").each(function () {
            arrWeek.push($(this).val());
        });
        return arrWeek.join("");
    },
    fnTourCalendar: function (cDate) {
        var st = $("#txtStDate").val();
        if (typeof (cDate) == "undefined") cDate = "";
        if (cDate != "") st = cDate;

        var _u = "Service/TourCalendar.ashx?st=" + st + "&rnd=" + Math.random();
        dataService.ajaxGet(_u, function (data) {
            $("#tagContent").html(data);
            t.fnSelectDate();
        });
    },
    fnGoToCalendar: function (cDate) { //日历的上个月与下个月
        t.fnTourCalendar(cDate);
    },
    fnDateDiff: function (sDate, eDate) { //日期比较
        var arr = sDate.split("-");
        var startDate = new Date(arr[0], arr[1], arr[2]);
        var startTimes = startDate.getTime();

        var arr2 = eDate.split("-");
        var endDate = new Date(arr2[0], arr2[1], arr2[2]);
        var endTimes = endDate.getTime();

        return startTimes - endTimes;
    },
    fnSelectDate: function () { //选择日期
        $("#tagContent td table tr td").each(function () {
            $(this).click(function () {
                var cDate = $(this).attr("tag");
                var sDate = $("#txtStDate").val();
                var eDate = $("#txtEtDate").val();
                if (sDate != "" && eDate != "") {
                    var delt1 = t.fnDateDiff(cDate, sDate);
                    var delt2 = t.fnDateDiff(cDate, eDate);
                    if (delt1 >= 0 && delt2 <= 0) {
                        var className = $(this).attr("class");
                        if (className == "TourDay")
                            $(this).removeClass("TourDay");
                        else $(this).addClass("TourDay");
                    }
                    else {
                        Alert(cDate + "不在有效日期范围内");
                    }
                }
                else {
                    Alert("请先选择发团日期区间");
                }
            });
        });
    },
    fnGetSelectedDate: function () { //获取已选择的日期
        var arrDate = [];
        $("#tagContent").find(".TourDay").each(function () {
            arrDate.push($(this).attr("tag"));
        });
        return arrDate.join(",");
    },
    //#endregion

    //#region << 集合地点 >>
    fnAddVenue: function () { //增加临时上车地点
        var id = $("#Departure option:selected").val();
        var name = $("#Departure option:selected").text();
        if (id == "") {
            Alert("请选择出发地");
            return false;
        }
        var sb = [];
        sb.push("<tr>");
        sb.push("<td style='text-align:center;'><input type=\"checkbox\" name=\"chkItem\" value=\"1\" /></td>");
        sb.push("<td style='text-align:center;'>" + name + "<input type=\"hidden\" value='" + id + "' /></td>");
        sb.push("<td><input type=\"text\" class=\"textbox\" value='' style=\"width: 99%; height: 26px;\" /></td>");
        sb.push("<td><input type=\"text\" class=\"textbox\" value='' style=\"width: 190px; height: 26px;\" /></td>");
        sb.push("<td><input type=\"text\" class=\"checkInt textbox\" value='' style=\"width: 100px; text-align: center; height: 26px;\" /></td>");
        sb.push("<td><input type=\"text\" class=\"checkInt textbox\" value='' style=\"width: 100px; text-align: center; height: 26px;\" /></td>");
        sb.push("</tr>");

        $("#tblTourVenue").append(sb.join(""));
        checkInt();
    },
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
    //#endregion

    //#region << 价格策略 >>
    fnSetDefaultPrice: function () { //设置默认价格体系
        t.fnLoadPrice("成人价");
        t.fnLoadPrice("儿童价");
    },
    fnLoadPrice: function (name) { //添加价格策略
        var sb = [];
        if (typeof (name) == undefined)
            name = "";
        var idx = $("#tblPrice").find("tr").size();
        sb.push("<tr>");
        sb.push("<td style=\"text-align:center; font-family:Arial;\">" + (idx + 1) + "</td>");
        sb.push("<td><input type=\"text\" class='textbox' style=\"width:98%; height:26px;\" value='" + name + "' /></td>");
        sb.push("<td><input type=\"text\" class=\"checkInt textbox\" style=\"width:110px;height:26px; padding-right:10px;text-align:right;\" /></td>");
        sb.push("<td><input type=\"text\" class=\"checkInt textbox\" style=\"width:70px;height:26px; padding-right:10px;text-align:right;\" /></td>");
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
            var name = $("input[type='text']", tdArr[1]).val();
            var salePrice = $("input[type='text']", tdArr[2]).val();
            var rebate = $("input[type='text']", tdArr[3]).val();
            var roomRate = $("input[type='text']", tdArr[4]).val();
            var isSeat = $("input[type='checkbox']", tdArr[5]).is(':checked');
            var isChild = $("input[type='checkbox']", tdArr[6]).is(':checked');
            var isDefault = $("input[type='radio']", tdArr[7]).is(':checked');
            xml.push("<data>");
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

    //#region << 保存 >>
    fnSaveData: function () {
        var routeID = request("id");
        var name = $("#txtTourName").val();
        var planNum = $("#txtPlanNum").val();
        var clusterNum = $("#txtClusterNum").val();
        var stDate = $("#txtStDate").val();
        var etDate = $("#txtEtDate").val();
        var cbExpiryDate = $("#ddlExpiryDate option:selected").val();
        var seatNum = $("#txtSeatNum").val();
        var tourNum = $("#txtTourNum").val();
        var ruleType = $(".thistab", "#ul_tabs").prop("tabindex").toString();
        var ruleData = "";
        switch (ruleType) {
            case "1"://按天开班
                ruleData = $("#txtIntervalDay").val();
                break;
            case "2"://按周几                
                ruleData = t.fnGetSelectedWeekDay();
                break;
            case "3"://按日期
                ruleData = t.fnGetSelectedDate();
                break;
        }

        var defaultPrice = t.fnGetDefaultPrice();
        var xmlVenue = t.fnGetTourVenue();
        var xmlPrice = t.fnGetTourPrice();
        if (name == "") {
            Alert("请填写团队名称");
            return false;
        }
        if (planNum == "") {
            Alert("请填写计划人数");
            return false;
        }
        if (stDate == "") {
            Alert("请选择开班的开始日期");
            return false;
        }
        if (etDate == "") {
            Alert("请选择开班的结束日期");
            return false;
        }
        if (ruleData == "") {
            Alert("开班规则不完整");
            return false;
        }
        if (xmlVenue == "") {
            Alert("请选择集合（上车）地点");
            return false;
        }
        if (xmlPrice == "") {
            Alert("请填写价格体系");
            return false;
        }
        if (defaultPrice == "") {
            Alert("请选择默认显示价格");
            return false;
        }
        var data = { "RouteID": routeID, "TourName": name, "PlanNum": planNum, "ClusterNum": clusterNum, "StDate": stDate, "EtDate": etDate, "ExpiryDate": cbExpiryDate, "SeatNum": seatNum, "TourNum": tourNum, "RuleType": ruleType, "RuleData": ruleData, "XmlVenue": xmlVenue, "XmlPrice": xmlPrice, "DefaultPrice": defaultPrice };
        var u = "Service/TourInfo.ashx?action=1&r=" + getRand();
        dataService.ajaxPost(u, data, function (v) {
            if (v == "1") {
                closeTab("保存成功", "开班发团");
            }
            else {
                Alert("保存失败");
                return false;
            }
        });
    },
    //#endregion

};



