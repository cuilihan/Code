//自主班散客订单修改
var p = {
    init: function () {
        checkInt();
        comm.fnDept();
        $("#btnSelectCustomer").bind("click", function () { //选择客户
            comm.fnSelectCustomer();
        });
        $("#btnAddCustomer").click(function () { //增加客户 
            comm.fnAddCustomer();
        });
        var departureID = $("#Departure option:selected").val();
        p.fnBindVenue(departureID);
        $("#Departure").change(function () { //上车地点
            var departureID = $("#Departure option:selected").val();
            p.fnBindVenue(departureID);
        });
        p.fnDrawSeatChart();//短线座位号 
        p.fnCalculate();//计算团款
        p.fnSetCalculateEvent();//团款计算事件
        $("#btnSave").click(function () {
            p.fnSave();
        });

        comm.fnUploadFile(); //上传附件
    },
    serverURL: "/Module/Order/Service/OrderInfo.ashx?xType=2",
    fnDrawSeatChart: function () { //短线座位号
        var strSeatNum = $("#hfSeatNum").val();
        var seatNum = strSeatNum == "" ? 0 : parseInt(strSeatNum);
        if (seatNum > 0) {
            $("#cellSeat").removeClass("hide");
            var tourID = $("#TourID").val();
            var u = p.serverURL + "&action=3&seatNum=" + seatNum + "&tourID=" + tourID + "&orderID=" + request("id") + "&r=" + getRand();
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
        var u = p.serverURL + "&action=4&departureID=" + departureID + "&tourID=" + $("#TourID").val();
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
        var strVenueAtm = $("#lblBusAmt").text();
        var venueAmt = strVenueAtm == "" ? 0 : parseFloat(strVenueAtm);
        totalAmt += venueAmt;
        //调整金额
        var strAdjustAmt = $("#AdjustAmt").val();
        var adjustAmt = strAdjustAmt == "" ? 0 : parseInt(strAdjustAmt);

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
    fnSave: function () {
        var orderID = request("id");
        var orderSourceID = $("#OrderSource option:selected").val();
        var orderSource = $("#OrderSource option:selected").text();
        var xmlPrice = p.fnGetPrice();
        var xmlVisitor = comm.fnGetCustomer();
        var comment = $("#Comment").val();
        var adjustAmt = $("#AdjustAmt").val();
        var orderAmt = $.trim($("#OrderAmt").text());
        var orderStatus = $("#hfOrderStatus").val();
        var venueName = $("#VenueName").val();
        var collectTime = $("#CollectTime").val();
        var pickAmt = $("#PickAmt").val();
        var sendAmt = $("#SendAmt").val();
        var participantID = $("#Employee").combobox("getValue");
        var partDeptID = $("#Dept").combotree("getValue");
        var participant = $("#Employee").combobox("getText");
        var deptName = $("#Dept").combotree("getText");
        var fileIDs = comm.fnGetFileID();


        //如果报名人数和人员数量不等需要提醒

        var visitorCount = $("#tblCustomer").find("tr").size();
        var __visitorCount = function () {
            var __c = 0;
            $("#tblPrice").find("tr").each(function () {
                var tdarr = $(this).find("td");
                __c += parseInt($("input", tdarr[5]).val());
            });
            return __c;
        }
        var tourVenue = venueName + "," + collectTime + "," + pickAmt + "," + sendAmt;
        if (xmlPrice == "") {
            Alert("请填写报名信息");
            return false;
        }
        if (xmlVisitor == "") {
            Alert("请填写游客信息"); return false;
        }
        //座位号
        var strSeatNum = $("#hfSeatNum").val();
        var seatNum = strSeatNum == "" ? 0 : parseInt(strSeatNum);
        var seatNo = p.fnGetSeat();
        if (seatNum > 0) {
            if (seatNo == "") {
                Alert("请选择座位号");
                return false;
            }
        }

        if (visitorCount != __visitorCount()) {
            if (!confirm("报名人数和游客人数不相等，是否确认提交")) {
                return false;
            }
        }
        $.ajax({
            type: "post",
            url: p.serverURL + "&action=5&r=" + getRand(),
            data: { "ID": orderID, "TourVenue": tourVenue, "SourceID": orderSourceID, "SourceName": orderSource, "CustomerInfo": xmlVisitor, "PriceInfo": xmlPrice, "Remark": comment, "OrderAmt": orderAmt, "SeatNo": seatNo, "AdjustAmt": adjustAmt, "OrderStatus": orderStatus, "Participant": participant, "DeptName": deptName, "ParticipantID": participantID, "PartDeptID": partDeptID, "FileID": fileIDs },
            beforeSend: function (XMLHttpRequest) {
                $("#btnOpt").addClass("hide");
                $("#tips").removeClass("hide");
            },
            success: function (data, textStatus) {
                $("#tips").addClass("hide");
                if (data != "") {
                    window.location.href = "/Module/Pro/Success.aspx?id=" + data;
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