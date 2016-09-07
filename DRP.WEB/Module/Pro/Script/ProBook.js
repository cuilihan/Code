//产品预订
var p = {
    init: function () {
        checkInt();
        comm.fnAddCustomer();//默认有一个客户填写栏目
        $("#btnSelectCustomer").bind("click", function () { //选择客户
            comm.fnSelectCustomer();
        });
        $("#btnAddCustomer").click(function () { //增加客户 
            comm.fnAddCustomer();
        });
        p.fnDrawSeatChart();//短线座位号
        $("#Departure").change(function () { //上车地点
            var departureID = $("#Departure option:selected").val();
            p.fnBindVenue(departureID);
        });
        p.fnSetCalculateEvent();//团款计算事件
        $("#btnSave").click(function () {
            p.fnSave();
        });
    },
    serverURL: "/Module/Order/Service/OrderInfo.ashx?xType=2",
    fnDrawSeatChart: function () { //短线座位号
        var strSeatNum = $("#hfSeatNum").val();
        var seatNum = strSeatNum == "" ? 0 : parseInt(strSeatNum);
        if (seatNum > 0) {
            $("#cellSeat").removeClass("hide");
            var u = p.serverURL + "&action=3&seatNum=" + seatNum + "&tourID=" + request("id") + "&r=" + getRand();
            dataService.ajaxGet(u, function (data) {
                $("#seatNum").html(data);
                $("#seatNum").find("td").click(function () {
                    var clsName = $(this).attr("class");
                    var seatNo = $.trim($(this).text());
                    switch (clsName) {
                        case "seat":
                            $(this).removeClass("seat").addClass("bczt");
                            $(this).html("");
                            $(this).attr("tag", seatNo);
                            $(this).attr("title", "已选择座位" + seatNo);
                            break;
                        case "bczt":
                            $(this).removeClass("bczt").addClass("seat");
                            var tag = $(this).attr("tag");
                            $(this).text(tag);
                            $(this).attr("title", "");
                            break;
                    }
                });
            });
        }
        else {
            $("#cellSeat").addClass("hide");
        }
    },
    fnBindVenue: function (departureID) { //上车地点
        var u = p.serverURL + "&action=4&departureID=" + departureID + "&tourID=" + request("id");
        dataService.ajaxGet(u, function (data) {
            $("#TourVenue option").remove();
            $("#TourVenue").append("<option value=''>请选择</option>");
            if (data != "") {
                $(eval(data)).each(function () {
                    var v = this.Name + "," + this.MeetTime + "," + this.PickAmt + "," + this.SendAmt;
                    var t = this.Name + " " + this.MeetTime + "(接加价：" + this.PickAmt + "送加价：" + this.SendAmt + ")"
                    var opt = "<option value='" + v + "'>" + t + "</option>";
                    $("#TourVenue").append(opt);
                });
            }
        });
    },
    fnSetCalculateEvent: function () { //注册要计算事件 
        $("#tblPrice").find("input[name='calculate']").blur(function () {
            p.fnCalculate();
        });
        $("#TourVenue").change(function () {
            p.fnCalculate();
        });
        $("#AdjustAmt").blur(function () {
            p.fnCalculate();
        });
    },
    fnCalculate: function () { //团款计算 
        var totalAmt = 0;
        $("#tblPrice").find("tr").each(function () {
            var tdArr = $(this).find("td");
            var strPrice = $.trim($(tdArr[3]).text());
            var strRoomPrice = $.trim($(tdArr[4]).text());
            var price = strPrice == "" ? 0 : parseInt(strPrice);
            var roomPrice = strRoomPrice == "" ? 0 : parseInt(strRoomPrice);

            var strVisitorNum = $(tdArr[5]).find("input[type='text']").val();
            var strRoomRate = $(tdArr[6]).find("input[type='text']").val();
            var strInsuranceAmt = $(tdArr[7]).find("input[type='text']").val();
            var visitorNum = strVisitorNum == "" ? 0 : parseInt(strVisitorNum);
            var roomRate = strRoomRate == "" ? 0 : parseInt(strRoomRate);
            var insureanceAmt = strInsuranceAmt == "" ? 0 : parseInt(strInsuranceAmt);

            var sum = price * strVisitorNum + roomRate + insureanceAmt;
            totalAmt += sum;
            $(tdArr[8]).html(sum.toString());
        });
        //接送金额
        var strVenue = $("#TourVenue option:selected").val();
        var pickAmt = 0;
        var sendAmt = 0;
        if (strVenue != "") {
            var arr = strVenue.split(',');
            var strPickAmt = arr[2];
            var strSendAmt = arr[3];
            pickAmt = strPickAmt == "" ? 0 : parseInt(strPickAmt);
            sendAmt == strSendAmt == "" ? 0 : parseInt(strSendAmt);
        }
        var venueAmt = pickAmt + sendAmt;
        totalAmt += venueAmt;
        //调整金额
        var strAdjustAmt = $("#AdjustAmt").val();
        var adjustAmt = strAdjustAmt == "" ? 0 : parseInt(strAdjustAmt);


        $("#lblBusAmt").html(venueAmt.toString());
        $("#lblOrderAmt").html(totalAmt.toString());
        var d = totalAmt + adjustAmt;
        $("#OrderAmt").html(d.toString());
    },
    fnGetPrice: function () { //获取价格
        var sb = [];
        $("#tblPrice").find("tr").each(function () {
            var tdArr = $(this).find("td");
            var tourPriceID = $(tdArr[0]).find("input[type='hidden']").val();

            var visitorNum = $(tdArr[5]).find("input[type='text']").val();
            var roomRate = $(tdArr[6]).find("input[type='text']").val();
            var insuranceAmt = $(tdArr[7]).find("input[type='text']").val();
            var insuranceCost = $(tdArr[9]).find("input[type='text']").val();
            if (visitorNum != "") {
                sb.push("<data>");
                sb.push("<TourPriceID>" + tourPriceID + "</TourPriceID>");
                sb.push("<VisitorNum>" + visitorNum + "</VisitorNum>");
                sb.push("<RoomRate>" + roomRate + "</RoomRate>");
                sb.push("<InsuranceAmt>" + insuranceAmt + "</InsuranceAmt>");
                sb.push("<InsuranceCost>" + insuranceCost + "</InsuranceCost>");
                sb.push("</data>");
            }
        });
        if (sb.length == 0) return "";
        else return "<document>" + sb.join("") + "</document>";
    },
    fnGetSeat: function () { //座位号
        var arr = [];
        $("#seatNum").find(".bczt").each(function () {
            var t = $(this).attr("tag");
            arr.push(t);
        });
        return arr.join(",");
    },
    fnGetOccupySeatVisitorNum: function () { //获取占座人数
        var t = 0;
        $("#tblPrice").find("tr").each(function () {
            var tdArr = $(this).find("td");
            var isSeat = $(tdArr[2]).find("input[type='hidden']").val();
            if (isSeat == "True") {
                var n = $(tdArr[5]).find("input[type='text']").val();
                if (n != "") t += parseInt(n);
            }
        });
        return t;
    },
    fnSave: function () {
        var tourID = request("id");
        var departureID = $("#Departure option:selected").val();
        var departureName = $("#Departure option:selected").text();
        var tourVenue = $("#TourVenue option:selected").val();
        var orderSourceID = $("#OrderSource option:selected").val();
        var orderSource = $("#OrderSource option:selected").text();
        var xmlPrice = p.fnGetPrice();
        var xmlVisitor = comm.fnGetCustomer();
      
        var comment = $("#Comment").val();
        var adjustAmt = $("#AdjustAmt").val();
        var orderAmt = $.trim($("#OrderAmt").text());
        var orderStatus = $("#hfOrderStatus").val();
        if (departureID == "") {
            Alert("请选择出发地"); return false;
        }
        if (tourVenue == "") {
            Alert("请选择上车地点"); return false;
        }
        if (xmlPrice == "") {
            Alert("请填写报名信息");
            return false;
        }
        if (xmlVisitor == "") {
            Alert("请填写游客信息"); return false;
        }
        //座位号
        var occupySeatNum = p.fnGetOccupySeatVisitorNum();//报名人数占座位数 
        var strSeatNum = $("#hfSeatNum").val();
        var seatNum = strSeatNum == "" ? 0 : parseInt(strSeatNum);
        if (seatNum > 0) {
            var seatNo = p.fnGetSeat();
            if (seatNo == "") {
                Alert("请选择座位号");
                return false;
            }
            if (occupySeatNum != seatNo.split(',').length) {
                Alert("报名人数与座位数不一致");
                return false;
            }
        }
        //报名人数是否超过计划人数
        var _vUrl = p.serverURL + "&action=17&tourID=" + tourID + "&vNum=" + occupySeatNum + "&r=" + getRand();
        var isOver = false;
        dataService.ajaxGet(_vUrl, function (data) {
            isOver = (data == "1");
        }, false);
        if (isOver) {
            alert("报名人数超过计划人数，不能报名");
            return;
        }
        $.ajax({
            type: "post",
            url: p.serverURL + "&action=5&r=" + getRand(),
            data: { "TourID": tourID, "DepartureID": departureID, "DepartureName": departureName, "TourVenue": tourVenue, "SourceID": orderSourceID, "SourceName": orderSource, "CustomerInfo": xmlVisitor, "PriceInfo": xmlPrice, "Remark": comment, "OrderAmt": orderAmt, "SeatNo": seatNo, "AdjustAmt": adjustAmt, "OrderStatus": orderStatus },
            beforeSend: function (XMLHttpRequest) {
                $("#btnOpt").addClass("hide");
                $("#tips").removeClass("hide");
            },
            success: function (data, textStatus) {
                $("#tips").addClass("hide");
                if (data != "") {
                    window.location.href = "Success.aspx?id=" + data;
                }
                else {
                    $("#btnOpt").removeClass("hide");
                    Alert("订单提交失败");
                }
            },
            error: function () {
                $("#btnOpt").removeClass("hide");
                $("#tips").addClass("hide");
                Alert("操作失败");
            }
        });
    }
};



$(function () {
    p.init();
});