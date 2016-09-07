
$(function () {
    $("#next_01").click(function () {
        var orgName = $("#OrgName").val();
        var arrStr = [];
        if (orgName == "") {
            alert("公司名称不可为空");
            return false;
        }
        $(this).parents("tr").prev().find("ul li").each(function () {
            var deptName = $(this).children("input").val();
            if (deptName != "") {
                arrStr.push(deptName);
            }
        });

        $.ajax({
            type: "post",
            url: "Service/Init.ashx?action=1&r=" + getRand(),
            data: { "orgName": orgName, "deptName": arrStr.join(",") },
            success: function (data, textStatus) {
                selectTag('main_02', $("#next_01"));
            },
            error: function () {
                alert("请输入正确信息");
            }
        });
    });

    $("#next_02").click(function () {
        var arrStr = [];
        $(this).parents("tr").prev().find("ul li").each(function () {
            var roleName = $(this).children("input").val();
            if (roleName != "") {
                arrStr.push(roleName);
            }
        });
        $.ajax({
            type: "post",
            url: "Service/Init.ashx?action=2&r=" + getRand(),
            data: { "roleName": arrStr.join(",") },
            success: function (data, textStatus) {
                selectTag('main_03', $("#next_02"));
            },
            error: function (data, textStatus) {
                alert("请输入正确信息")
            }
        });
    });

    $("#btnSave").click(function () { 
        var arr = [];
        $("#param").find("tr").each(function () {
            var basicType = $(this).find("input[type='hidden']").val();
            if (basicType != "") {
                var sb = [];
                $(this).find("input[type='text']").each(function () {
                    var v = $(this).val();
                    if (v != "") {
                        if (v.indexOf('-') > -1) v = v.replace('-', '');
                        sb.push(basicType + '-' + v);
                    }
                })
            }
            if (sb.length > 0)
                arr.push(sb.join(","));
        });
        if (arr.length == 0) {
            alert("请填写参数");
            return;
        }
        $.ajax({
            type: "post",
            url: "Service/Init.ashx?action=3&r=" + getRand(),
            data: { "data": arr.join("@") },
            success: function (data, textStatus) {
                if (data == "1") {
                    window.location.href = "/Module/Sys/Permission.aspx";
                }
            },
            error: function (data, textStatus) {
                alert("请输入正确的参数信息")
            }
        });
    });
})


function addTag(obj) {
    var i = $(obj).parents("td").find("ul > li").index();
    var s = "<li><input type='text' class='textbox'style='height:26px;' name='text" + (i + 2) + "' /></li>";
    $(obj).parents("td").find("ul").append(s);
}

function selectTag(showTag, obj) {
    if ($(obj).attr("id").indexOf("next") > -1) {
        $("#" + showTag).css("display", "").prev().css("display", "none");
    } else if ($(obj).attr("id").indexOf("pre") > -1) {
        $("#" + showTag).css("display", "").next().css("display", "none");
    }
}


