//安排导游

$(function () {
    g.fnInit();
});

var g = {
    fnInit: function () {
        $("#btnSelect").click(function () {
            g.fnSelectGuide();
        });
        $("#btnAdd").click(function () {
            g.fnAddItem();
        });
        $("#btnSave").click(function () {
            g.fnSave();
        });
    },
    fnAddItem: function () {
        var sb = [];
        var p = g.fnCreatePwd();
        sb.push("<tr>");
        var hID = "<input type='hidden' value=''/>";
        sb.push("<td style='text-align:center;'>" + ($("#tblData").find("tr").size() + 1) + hID + "</td>");
        sb.push("<td><input type='text' class='textbox' style='width:98%; height:26px;' /></td>");
        sb.push("<td><input type='text' class='textbox' style='width:98%; height:26px;' /></td>");
        sb.push("<td><input type='text' class='textbox' style='width:98%; height:26px;' value='" + p + "' /></td>");
        sb.push("<td style='text-align:center;'><a href='javascript:;' onclick='g.fnDelete(this)'>删除</a></td>");
        sb.push("</tr>");
        $("#tblData").append(sb.join(""));
    },
    fnCreatePwd: function () {
        var arr = "1,2,3,4,5,6,7,8,9,0,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z".split(',');
        var sb = [];
        while (sb.length < 4) {
            var r = g.getRandom(arr.length - 1);
            if (r < arr.length) {
                sb.push(arr[r]);
            }
        }
        return sb.join("");
    },
    getRandom: function (n) {
        return Math.floor(Math.random() * n + 1)
    },
    fnDelete: function (obj) {
        $(obj).parent().parent().remove();
    },
    fnSelectGuide: function () {
        $.dialog({
            cover: true, lock: true,
            width: 540, height: 460, max: false,
            min: false, title: "选择导游",
            content: 'url:/Module/Res/SelectGuide.aspx'
        });
        return false;
    },
    fnSave: function () {
        var i = $("#tblData").find("tr").size();
        if (i == 0) {
            Alert("导游不能为空");
            return;
        }
        var sb = [];
        $("#tblData").find("tr").each(function () {
            var tdArr = $(this).find("td");
            var guideID = $(tdArr[0]).find("input[type='hidden']").val();
            var name = $(tdArr[1]).find("input[type='text']").val();
            var mobile = $(tdArr[2]).find("input[type='text']").val();
            var pwd = $(tdArr[3]).find("input[type='text']").val();
            if (name != "" && mobile != "" && pwd != "") {
                sb.push("<data>");
                sb.push("<GuideID>" + guideID + "</GuideID>");
                sb.push("<GuideName>" + name + "</GuideName>");
                sb.push("<GuideMobile>" + mobile + "</GuideMobile>");
                sb.push("<GuidePwd>" + pwd + "</GuidePwd>");
                sb.push("</data>");
            }
        });
        if (sb.length == 0) {
            Alert("导游资料填写不完整");
            return;
        }
        var xmlData = "<document>" + sb.join("") + "</document>";
        var orderID = request("id");
        var xType = request("xType");
        var json = { "OrderID": orderID, "xmlData": xmlData };
        var u = "Service/OrderInfo.ashx?action=14&xType=" + xType + "&r=" + getRand();
        dataService.ajaxPost(u, json, function (data) {
            if (data == "1") {
                closeTab("保存成功", "安排导游");
            }
            else {
                Alert("保存失败"); return;
            }
        });
    }
};

//接受导游数据
function fnReceiveGuideData(json) {
    var sb = [];
    $(json).each(function () {
        var p = g.fnCreatePwd();
        sb.push("<tr>");
        var hID = "<input type='hidden' value='" + this.ID + "'/>";
        sb.push("<td style='text-align:center;'>" + ($("#tblData").find("tr").size() + 1) + hID + "</td>");
        sb.push("<td><input type='text' class='textbox' style='width:98%; height:26px;' value='" + this.Name + "' /></td>");
        sb.push("<td><input type='text' class='textbox' style='width:98%; height:26px;' value='" + this.Mobile + "'  /></td>");
        sb.push("<td><input type='text' class='textbox' style='width:98%; height:26px;' value='" + p + "' /></td>");
        sb.push("<td style='text-align:center;'><a href='javascript:;' onclick='g.fnDelete(this)'>删除</a></td>");
        sb.push("</tr>");
    });
    $("#tblData").append(sb.join(""));
}