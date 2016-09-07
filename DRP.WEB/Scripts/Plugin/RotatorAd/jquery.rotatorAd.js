 (function($) {
    $.fn.extend({
        "src": function(value) {
            return this.attr("src", value);
        },
        "backgroundImage": function(value) {
            return this.css("background-image", value);
        }

    });

    $.fn.RollTitle = function (opt, callback) { //顶部文字滚动显示
        if (!opt) var opt = {};
        var _this = this;
        _this.timer = null;
        _this.lineH = _this.find("li:first").height();
        _this.line = opt.line ? parseInt(opt.line, 15) : parseInt(_this.height() / _this.lineH, 10);
        _this.speed = opt.speed ? parseInt(opt.speed, 10) : 3000, //卷动速度，数值越大，速度越慢（毫秒 
        _this.timespan = opt.timespan ? parseInt(opt.timespan, 13) : 5000; //滚动的时间间隔（毫秒 
        if (_this.line == 0) this.line = 1;
        _this.upHeight = 0 - _this.line * _this.lineH;
        _this.scrollUp = function () {
            _this.animate({
                marginTop: _this.upHeight
            }, _this.speed, function () {
                for (i = 1; i <= _this.line; i++) {
                    _this.find("li:first").appendTo(_this);
                }
                _this.css({ marginTop: 0 });
            });
        }
        _this.hover(function () {
            clearInterval(_this.timer);
        }, function () {
            _this.timer = setInterval(function () { _this.scrollUp(); }, _this.timespan);
        }).mouseout();
    };


    $.fn.ylGalScroll = function(o) { //顶部轮显广告
        o = $.extend({
            speed: 0.6, //动画速度
            interval: 3, //动画间隔
            width: null, //容器的宽度，默认从DOM中获取
            height: null,
            navBarBackgroundColor: "", //数字导航区背景颜色
            navBarOpacity: "0.6", //数字导航区透明度
            btnNumBackground: "transparent", //数字按钮的背景颜色
            btnNumBorder: "1px solid #fff", //数字按钮的边框样式
            btnNumHoverColor: "#666", //当前或鼠标移上后数字按钮的颜色
            animateMode: null, //1-从左上角滑入；2-从上方滑入；3-淡入；4-从左方滑入；5-从右方滑入；6-从低部滑入；7-从左右同时滑入；null-7总效果随机出现
            titleIsTop: false//标题是否在上方

        }, o || {});
        var imgs = new Array(), links = new Array(), titles = new Array();
        var speed = o.speed * 1000, interval = o.interval * 1000, wrap = $(this), ul = $("ul", wrap), li = $("li", ul), img = $("img:first", li), liSize = li.size(), i = 0, width = o.width || img.width(), height = o.height || img.height();
        var hasTitle = $("span", li).is("span");
        wrap.css({ "overflow": "hidden", width: width + "px" });
        ul.remove();

        li.each(function() {
            imgs[i] = $("img", this).src();
            links[i] = $("a", this).attr("href");
            hasTitle ? titles[i] = $("span", this) : null;
            i++;
        });

        var animateDiv = $("<div />").css({ "position": "relative", display: "none", width: width + "px", height: height + "px", "overflow": "hidden" });
        var imgWrap = $("<div />").css({ position: "relative", cursor: "pointer", "background-image": "url(" + imgs[0] + ")", "overflow": "hidden", width: width + "px", height: height + "px", "background-position": "top left" }).append(animateDiv);
        var navBar = $("<div />").attr("style", "opacity:" + o.navBarOpacity).css({ "text-align": "right", "line-height": "35px", height: "35px", "margin-top": "-35px", "background-color": o.navBarBackgroundColor, "opacity": o.navBarOpacity, "overflow": "hidden", "position": "relative" });

        var title = hasTitle ? $("<div />").css({ "line-height": "35px", "text-align": "center" }).html(titles[0]) : null;
        o.titleIsTop ? wrap.append(title).append(imgWrap).append(navBar) : wrap.append(imgWrap).append(navBar).append(title);

        i = 1;
        while (i <= liSize) {
            $("<a />").html(i).appendTo(navBar);

            i++;

        };
        i = 0;
        var navNum = $("a", navBar).css({ position: "relative", padding: "0 7px", "background-color": o.btnNumBackground, margin: "8px", "border": o.btnNumBorder, "font-size": "12px", color: "#fff", cursor: "pointer", display: "inline-block", "line-height": "16px" });
        navNum.eq(0).css("background-color", o.btnNumHoverColor);
        var _play = setInterval(play, interval);

        navNum.hover(function() { clearInterval(_play); }, function() { _play = setInterval(play, interval); })
                      .click(function() {
                          var curr = navNum.index($(this));

                          if (curr == i) { return; }
                          i = curr;
                          navNum.css("background-color", o.btnNumBackground);
                          $(this).css("background-color", o.btnNumHoverColor);
                          animateDiv.backgroundImage("url(" + imgs[i] + ")");
                          hasTitle ? title.html(titles[i]) : "";


                          var random = o.animateMode || parseInt(Math.random() * 7 + 1);
                          switch (random) {
                              case 1:
                                  animateDiv.show(speed, setImgWrap);
                                  break;
                              case 2:
                                  animateDiv.slideDown(speed, setImgWrap);
                                  break;
                              case 3:
                                  animateDiv.fadeIn(speed, setImgWrap);
                                  break;
                              case 4:
                                  animateDiv.width(0);
                                  animateDiv.animate({ opacity: "1", width: "100%" }, speed, setImgWrap);
                                  break;
                              case 5:
                                  animateDiv.css({ width: "0", "float": "right", "display": "block" });
                                  animateDiv.animate({ width: "100%" }, speed, setImgWrap);
                                  break;
                              case 6:
                                  animateDiv.css({ "margin-top": height + "px", "display": "block" });
                                  animateDiv.animate({ "margin-top": "0" }, speed, setImgWrap);
                                  break;
                              case 7:
                                  var div1 = $("<div />").backgroundImage("url(" + imgs[i] + ")").css({ width: "0px", float: "left", height: height + "px" });
                                  var div2 = div1.clone(true).css({ "background-position": "right top", float: "right" });
                                  animateDiv.backgroundImage("none").show();
                                  animateDiv.append(div1).append(div2);
                                  div1.animate({ width: width / 2 + "px" }, speed);
                                  div2.animate({ width: width / 2 + "px" }, speed, setImgWrap);
                                  break;
                          }
                      });
        function play() {
            navNum.eq((i + 1) % liSize).click();
        };
        function setImgWrap() {
            imgWrap.backgroundImage("url(" + imgs[i] + ")").click(function() { location.href = links[i] }); animateDiv.hide().html("").css("float", "none");
        };
    };
})(jQuery);