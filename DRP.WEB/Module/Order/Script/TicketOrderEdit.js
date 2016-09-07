//机票订单


$(function () {
    c.init();
});

var c = {
    init: function () {
        comm.fnDept();
        $("#btnSelectCustomer").bind("click", function () { //选择客户
            c.fnSelectCustomer();
        });
        $("#btnAddCustomer").click(function () { //增加客户 
            c.fnAddCustomer();
        });
        $("#btnSave").click(function () { //提交订单
            c.fnSave();
        });
        c.fnDefaultFlight();
        c.fnRegCalculateEvent();
        $("#btnAddCostItem").click(function () {
            var itemType = $("#ddlCostItemType option:selected").val();
            var itemName = $("#ddlCostItemType option:selected").text();
            if (itemType == "") {
                Alert("请选择项目类型");
            } else {
                comm.fnAddCostItem(itemType, itemName);
            }
        });
        c.fnDefaultCostItem();

        comm.fnUploadFile(); //上传附件
    },
    fnDefaultCostItem: function () { //默认成本项目
        if (request("id") == "") {
            comm.fnAddCostItem(9, "票务机构");
            comm.fnAddCostItem(7, "保险公司");
        }
    },
    url: "Service/TicketOrder.ashx?r=" + getRand(),
    fnDefaultFlight: function () { //默认航班信息
        if (request("id") == "") {
            c.fnAddFlightInfo("去程");
            c.fnAddFlightInfo("回程");
        }
    },
    fnAddFlightInfo: function (name) { //航班信息
        var sb = [];
        sb.push("<tr>");
        sb.push("<td style=\"text-align: center; font-weight: bold;\">" + name + "</td>");
        sb.push("<td><input type=\"text\" style=\"width: 90px; height: 26px;\" class=\"textbox\" /></td>");
        sb.push("<td><input type=\"text\" style=\"width: 96%; height: 26px;\" class=\"textbox\" /></td>");
        sb.push("<td><input type=\"text\" style=\"width: 80px; height: 26px;\" class=\"textbox\" /></td>");
        sb.push("<td><input type=\"text\" style=\"width: 130px; height: 26px;\" class=\"textbox\" /></td>");
        sb.push("<td><input type=\"text\" style=\"width: 70px; height: 26px;\" class=\"textbox\" value='经济舱' /></td>");
        sb.push("<td><input type=\"text\" name='txtPrice' style=\"width: 60px; height: 26px;\" class=\"textbox\" /></td>");
        sb.push("<td style='text-align:center;'><a href='javascript:;' onclick=\"c.fnDeleteFlight(this)\">删除</a></td>");
        sb.push("</tr>");
        $("#tblFlight").append(sb.join(""));
    },
    fnGetFlightInfo: function () { //获取航班信息
        var sb = [];
        $("#tblFlight").find("tr").each(function () {
            var tdArr = $(this).find("td");
            var strDir = $.trim($(tdArr[0]).text());
            var leg = $(tdArr[1]).find("input[type='text']").val();
            var flight = $(tdArr[2]).find("input[type='text']").val();
            var airport = $(tdArr[3]).find("input[type='text']").val();
            var ariline = $(tdArr[4]).find("input[type='text']").val();
            var cabin = $(tdArr[5]).find("input[type='text']").val();
            var price = $(tdArr[6]).find("input[type='text']").val();
            if (strDir == "去程") {
                sb.push("<to>");
                sb.push("<flightleg>" + leg + "</flightleg>");
                sb.push("<flight>" + flight + "</flight>");
                sb.push("<airport>" + airport + "</airport>");
                sb.push("<airline>" + ariline + "</airline>");
                sb.push("<cabin>" + cabin + "</cabin>");
                sb.push("<price>" + price + "</price>");
                sb.push("</to>");
            }
            else {
                sb.push("<from>");
                sb.push("<flightleg>" + leg + "</flightleg>");
                sb.push("<flight>" + flight + "</flight>");
                sb.push("<airport>" + airport + "</airport>");
                sb.push("<airline>" + ariline + "</airline>");
                sb.push("<cabin>" + cabin + "</cabin>");
                sb.push("<price>" + price + "</price>");
                sb.push("</from>");
            }
        });
        if (sb.length == 0) return "";
        return "<document>" + sb.join("") + "</document>";
    },
    fnDeleteFlight: function (obj) {
        $(obj).parent().parent().remove();
    },
    fnRegCalculateEvent: function () { //注册计算成本、毛利事件
        $("#AdultNum").blur(function () {
            c.fnCalculate();
        });
        $("#tblFlight").find("input[name='txtPrice']").blur(function () {
            c.fnCalculate();
        });
    },
    fnCalculate: function () { //计算订单 
        var t = 0;
        $("#tblFlight").find("tr").each(function () {
            var tdArr = $(this).find("td");
            var str = $(tdArr[6]).find("input[type='text']").val();
            t += (str == "" ? 0 : parseFloat(str));
        });
        var strNum = $("#AdultNum").val();
        var num = strNum == "" ? 0 : parseInt(strNum);
        var orderAmt = num * t;
        var __strOrderAmt = $("#OrderAmt").val(); 
        $("#OrderAmt").val(orderAmt.toString());
    },
    fnSelectCustomer: function () { //弹出窗口选择客户 
        var ticketType = $("#TicketType option:selected").val();
        var t = ticketType == "国内" ? "0" : "1";
        $.dialog({
            cover: true, lock: true,
            width: 600, height: 460, max: false,
            min: false, title: "选择客户",
            content: 'url:/Module/Crm/SelectCustomer.aspx?t=' + t
        });
        return false;
    },
    fnCertificate: function () { //证件类型
        var ticketType = $("#TicketType option:selected").val();
        var t = ticketType == "国内" ? "0" : "1";
        var sb = [];
        sb.push("<select style='width:120px;height:26px;' onchange=\"c.fnGetCertificate('" + t + "',this)\">");
        if (t == "0") {
            sb.push("<option value='身份证'>身份证</option>");
        }
        else {
            sb.push("<option value=''>请选择</option>");
            sb.push("<option value='护照'>护照</option>");
            sb.push("<option value='台胞证'>台胞证</option>");
            sb.push("<option value='回乡证'>回乡证</option>");
            sb.push("<option value='港澳通行证'>港澳通行证</option>");
            sb.push("<option value='海员证'>海员证</option>");
            sb.push("<option value='大陆往来台湾通行证'>大陆往来台湾通行证</option>");
            sb.push("<option value='其他'>其他</option>");
        }
        sb.push("</select>");
        return sb.join("");
    },
    fnGetCertificate: function (t, obj) { //获取证件号码
        if (t == "1") { //国际机票
            var row = $(obj).parent().parent();
            var id = row.find("input[type='hidden']").val();
            var v = $(obj).val();
            if (id != "") {
                var u = c.url + "&action=2&t=" + encodeURI(v) + "&cid=" + id;
                dataService.ajaxGet(u, function (data) {
                    var tdArr = row.find("td");
                    $(tdArr[5]).find("input[type='text']").val(data);
                });
            }
        }
    },
    fnAddCustomer: function () { //增加客户 
        var sb = [];
        var i = $("#tblCustomer").find("tr").size() + 1;
        var cid = "<input type='hidden'/>";
        sb.push("<tr>");
        sb.push("<td style='text-align:center;'>" + i.toString() + cid + "</td>");
        sb.push("<td><input type='text' class='textbox' style='height:26px; width:110px;' /></td>");
        sb.push("<td><select style='width:50px; height:26px;'><option value=\"男\">男</option><option value=\"女\">女</option></select></td>");
        sb.push("<td><input type='text' class='textbox' style='height:26px; width:100px;' /></td>");
        sb.push("<td>" + c.fnCertificate() + "</td>");
        sb.push("<td><input type='text' class='textbox' style='height:26px; width:170px;' /></td>");
        sb.push("<td><input type='text' class='textbox' style='height:26px; width:98%;' /></td>");
        sb.push("<td><input type='text' class='textbox' style='height:26px; width:98%;' /></td>");
        sb.push("<td><a href='javascript:;' onclick=\"c.fnDeleteCustomer(this)\">删除</a></td>");
        sb.push("</tr>");
        $("#tblCustomer").append(sb.join(""));
    },
    fnDeleteCustomer: function (obj) { //删除客户
        $(obj).parent().parent().remove();
    },
    fnGetCustomer: function () { //获取客户信息  
        var sb = [];
        $("#tblCustomer").find("tr").each(function () {
            var tdArr = $(this).find("td");
            var customerID = $(tdArr[0]).find("input[type='hidden']").val();
            var name = $(tdArr[1]).find("input").eq(0).val();
            var sex = $("select", tdArr[2]).val();
            var mobile = $(tdArr[3]).find("input").eq(0).val();
            var cardType = $("select", tdArr[4]).val();
            var idcard = $(tdArr[5]).find("input").eq(0).val();
            var comment = $(tdArr[6]).find("input").eq(0).val();

            if (name == "" && mobile == "" && cardType == "" && idcard == "" && comment == "") { }
            else {
                sb.push("<data>");
                sb.push("<CustomerID>" + customerID + "</CustomerID>");
                sb.push("<Name>" + name + "</Name>");
                sb.push("<Sex>" + sex + "</Sex>");
                sb.push("<Mobile>" + mobile + "</Mobile>");
                sb.push("<CardType>" + cardType + "</CardType>");
                sb.push("<IDNo>" + idcard + "</IDNo>");
                sb.push("<Comment>" + comment + "</Comment>");
                sb.push("</data>");
            }
        });
        if (sb.length == 0) return ""; 
        return "<document>" + sb.join("") + "</document>";
    },
    fnSave: function () { //提交订单      
        var orderName = $("#OrderName").val();
        var ticketType = $("#TicketType option:selected").val();
        var pnr = $("#PNR").val();
        var tourDate = $("#TourDate").val();
        var returnDate = $("#ReturnDate").val();
        var adultNum = $("#AdultNum").val();
        var contact = $("#Contact").val();
        var contactPhone = $("#ContactPhone").val();
        var flight = c.fnGetFlightInfo();
        var customer = c.fnGetCustomer();
        var costInfo = comm.fnGetCostItem();
        var participantID = $("#Employee").combobox("getValue");
        var partDeptID = $("#Dept").combotree("getValue");
        var participant = $("#Employee").combobox("getText");
        var deptName = $("#Dept").combotree("getText");
        var company = $("#Company").val();

        //var supplierID = $("#SupplierName option:selected").val();
        //var supplierName = $("#SupplierName option:selected").text();
        //var orderCost = $("#OrderCost").val();
        var orderAmt = $("#OrderAmt").val();
        var remark = $("#Remark").val();
        var orderCostID = $("#OrderCostID").val();
        var fileIDs = comm.fnGetFileID();

        if (!$('#form1').form('validate'))
            return false;
        var keyID = request("id");
        if (customer == "") {
            Alert("客户信息不能为空");
            return false;
        }
        if (costInfo == "") {
            Alert("请填写成本信息");
            return false;
        }
        if (flight == "") {
            return confirm("航班信息为空，确定要保存吗");
        }
        $.ajax({
            type: "post",
            url: c.url + "&action=3",
            data: { "ID": keyID, "OrderName": orderName, "TicketType": ticketType, "PNR": pnr, "TourDate": tourDate, "ReturnDate": returnDate, "AdultNum": adultNum, "Contact": contact, "ContactPhone": contactPhone, "FlightInfo": flight, "CustomerInfo": customer, "CostInfo": costInfo, "Remark": remark, "OrderAmt": orderAmt, "Participant": participant, "DeptName": deptName, "ParticipantID": participantID, "PartDeptID": partDeptID, "Company": company, "FileID": fileIDs },
            beforeSend: function (XMLHttpRequest) {
                $("#btnOpt").addClass("hide");
                $("#tips").removeClass("hide");
            },
            success: function (data, textStatus) {
                $("#tips").addClass("hide");
                if (data == "1") {
                    closeTab("保存成功", "机票订单维护");
                }
                else {
                    $("#btnOpt").removeClass("hide");
                    Alert("保存数据失败");
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


//接收客户信息
function fnReceiveCustomerData(jsonArr, xType) {
    var sb = [];
    var ticketType = $("#TicketType option:selected").val();
    var t = ticketType == "国内" ? "0" : "1";
    var i = $("#tblCustomer").find("tr").size() + 1;
    if (jsonArr != null && typeof (jsonArr) != "undefined") {
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
            var name = t == "0" ? this.Name : this.EngName;
            if (name == "") name = this.Name;
            sb.push("<td><input type='text' class='textbox' style='height:26px; width:110px;' value='" + name + "' /></td>");
            sb.push("<td>" + sex + "</td>");
            sb.push("<td><input type='text' class='textbox' style='height:26px; width:100px;' value='" + this.Mobile + "' /></td>");
            sb.push("<td>" + c.fnCertificate() + "</td>");

            var idNum = t == "0" ? this.IDNum : "";
            sb.push("<td><input type='text' class='textbox' style='height:26px; width:170px;' value='" + idNum + "' /></td>");
            sb.push("<td><input type='text' class='textbox' style='height:26px; width:98%;'  value='" + this.Company + "' /></td>");
            sb.push("<td><input type='text' class='textbox' style='height:26px; width:98%;'  /></td>");
            sb.push("<td><a href='javascript:;' onclick=\"c.fnDeleteCustomer(this)\">删除</a></td>");
            sb.push("</tr>");
            i++;
        });
    }
    if (sb.length > 0) {
        $("#tblCustomer").append(sb.join(""));
    }
}
