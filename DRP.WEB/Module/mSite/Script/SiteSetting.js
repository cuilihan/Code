var a = {
    init: function () {
        $("#btnSave").click(function () {
            return a.fnSave();
        });

        navTab("ul_tabs", "tab_conbox");

        a.fnLogoUpload();

        a.fnAdUpload();

        KindEditor.ready(function (K) {
            K.create('#AboutUs', {
                resizeType: 1,
                height: 300,
                allowPreviewEmoticons: false,
                allowImageUpload: false,
                afterBlur: function () { this.sync(); },
                items: a.toolbarItems
            });
        });
    },

    toolbarItems: ['fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline',
                 'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist',
                 'insertunorderedlist', '|', 'emoticons', 'link', '|', 'plainpaste', 'source'],

    addItem: function (name, url) {
        var idx = $("#tblData").find("tr").size() + 1;
        var sb = [];
        sb.push("<tr>");
        sb.push("<td style='text-align:center;'><input type='checkbox' /></td>");
        sb.push("<td><input type=\"text\" class=\"textbox\" style=\"height: 26px;\"  value='" + name + "'/></td>");
        sb.push("<td><input type=\"text\" class=\"textbox\" style=\"height: 26px; width: 90%;\" value='" + url + "' /></td>");
        sb.push("<td><input type=\"text\" class=\"textbox\" style=\"height: 26px; width: 30px;\" value='" + idx + "' /></td>");
        sb.push("</tr>");
        $("#tblData").append(sb.join(""));
    },
    delItem: function (obj) {
        $(obj).parent().parent().remove();
    },
    selectAll: function (obj) {
        var isChked = $(obj).attr("checked") == "checked";
        if (isChked) $("#tblData").find("input[type='checkbox']").attr("checked", "checked");
        else $("#tblData").find("input[type='checkbox']").removeAttr("checked");
    },
    fnGetAd: function () { //获取广告图片信息
        var sb = [];
        $("#tblAd").find("tr").each(function () {
            var tdArr = $(this).find("td");
            var pic = $(tdArr[0]).find("input[type='hidden']").val();
            var url = $(tdArr[1]).find("input[type='text']").val();
            sb.push("<data>");
            sb.push("<imgSrc>" + pic + "</imgSrc>");
            sb.push("<url>" + url + "</url>");
            sb.push("</data>");
        });
        if (sb.length > 0)
            return "<document>" + sb.join("") + "</document>"
        else
            return "";
    },
    fnSave: function () {
        var travelName = $("#TravelName").val();
        var phone = $("#Phone").val();
        if (travelName == "") {
            Alert("请填写旅行社名称");
            return false;
        }
        if (phone == "") {
            Alert("请填写报名电话");
            return false;
        }
        var isShowRoute = $("#ShowRoute :checked").val();
        var domain = $("#lblDomain").text();
        var logoUrl = $("#LogoUrl").val();
        var aboutUs = $("#AboutUs").val();
        var data = { "IsShowRoute": isShowRoute, "Domain": domain, "LogoUrl": logoUrl, "TravelName": travelName, "Phone": phone, "AboutUs": aboutUs, "AdData": a.fnGetAd() };
        dataService.ajaxPost("Service/mSite.ashx?action=1&r=" + getRand(), data, function (d) {
            if (d == "1") {
                Alert("保存成功");
            }
            else {
                Alert("保存失败");
            }
        });
    },
    fnLogoUpload: function () {
        $("#uploadify").uploadify({
            'swf': '/Scripts/Plugin/Uploadify/uploadify.swf',
            'uploader': 'Service/mSite.ashx?action=2&r=' + getRand(),
            'buttonText': '上传LOGO',
            'fileTypeDesc': '图片',
            'fileTypeExts': '*.jpg;*.png;*.gif',
            'auto': true,
            'multi': false,
            'onUploadSuccess': function (file, data, response) {
                if (data != "") {
                    $("#LogoUrl").val(data);
                    $("#imgLogo").attr("src", data);
                }
                else {
                    Alert("上传Logo失败");
                }
            },
            'onUploadError': function (file, errorCode, errorMsg, errorString) {
                alert(file.name + '上传失败，失败原因： ' + errorString);
            }
        });
    },
    fnAdUpload: function () { //上传广告图片
        $("#adFile").uploadify({
            'swf': '/Scripts/Plugin/Uploadify/uploadify.swf',
            'uploader': 'Service/mSite.ashx?action=2&r=' + getRand(),
            'buttonText': '上传广告图片',
            'fileTypeDesc': '图片',
            'fileTypeExts': '*.jpg;*.png;*.gif',
            'auto': true,
            'multi': true,
            'onUploadSuccess': function (file, data, response) {
                if (data != "") {
                    var c = "<input type='hidden' value='" + data + "' />";
                    $("#tblAd").append("<tr><td><img style='width:100px; height:60px;' src='" + data + "' />" + c + "</td><td><input type='text' style='height:26px;width:90%;' class='textbox' /></td><td style='text-align:center;'><a href='javascript:;' onclick=\"a.fnDeletePic(this)\">删除</a></td></tr>");
                }
                else {
                    Alert("上传失败");
                }
            },
            'onUploadError': function (file, errorCode, errorMsg, errorString) {
                alert(file.name + '上传失败，失败原因： ' + errorString);
            }
        });
    },
    fnDeletePic: function (o) {
        $(o).parent().parent().remove();
    }
}



$(function () {
    a.init();
});