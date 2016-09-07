$(function () {
    p.fnUploadPhoto();
    p.fnUpdatePhoto();
});

var p = {
    fnUploadPhoto: function () {
        $("#uploadify").uploadify({
            'swf': '/Script/Uploadify/uploadify.swf',
            'uploader': 'Service/Photo.ashx?action=1&r=' + getRand(),
            'buttonText': '选择本地图片',
            'fileTypeDesc': '图片',
            'fileTypeExts': '*.jpg;*.png;*.gif',
            'auto': true,
            'multi': false,
            'onUploadSuccess': function (file, data, response) {
                if (data != "") {
                    if (data == "-1") {
                        $("#tips").html("更新头像失败");
                        alert("更新头像失败");
                    }
                    else {
                        $("#imgPhoto").attr("src", data);
                        alert("更新头像成功");
                    }
                }
                else {
                    alert("上传失败");
                }
            },
            'onUploadError': function (file, errorCode, errorMsg, errorString) {
                alert(file.name + '上传失败，失败原因： ' + errorString);
            }
        });
    },
    fnUpdatePhoto: function () {
        var u = "Service/Photo.ashx?action=2&r=" + getRand();
        $("#SysPhoto").find("img").click(function () {
            var src = $(this).attr("src");
            dataService.ajaxPost(u, { "Src": src }, function (data) {
                if (data == "") alert("更新头像失败");
                else {
                    $("#imgPhoto").attr("src", src);
                    alert("更新头像成功");
                }
            });
        });
    }
};
