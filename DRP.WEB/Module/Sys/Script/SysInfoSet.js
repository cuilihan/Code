var a = {
    init: function () {
        $("#btnSave").click(function () {
            return a.fnSave();
        });
        $("#btnDel").click(function () {
            return a.fnDel();
        });
        a.fnLogoUpload();
    },
    fnSave: function () {
        var logoUrl = $("#LogoUrl").val();
        var data = { "LogoUrl": logoUrl};
        dataService.ajaxPost("Service/SysInfoSet.ashx?action=1&r=" + getRand(), data, function (d) {
            if (d == "1") {
                Alert("保存成功");
            }
            else {
                Alert("保存失败");
            }
        });
    },
    fnDel: function () {
        var data = {  };
        dataService.ajaxPost("Service/SysInfoSet.ashx?action=3&r=" + getRand(), data,function (d) {
            if (d == "1") {
                window.location.reload();
                Alert("清空成功");
            }
            else {
                Alert("清空失败");
            }
        });
    },
    fnLogoUpload: function () {
        $("#uploadify").uploadify({
            'swf': '/Scripts/Plugin/Uploadify/uploadify.swf',
            'uploader': 'Service/SysInfoSet.ashx?action=2&r=' + getRand(),
            'buttonText': '上传LOGO',
            'fileTypeDesc': '图片',
            'fileTypeExts': '*.jpg;*.png;*.gif',
            'fileSizeLimit': '2MB',
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
    }
}



$(function () {
    a.init();
});