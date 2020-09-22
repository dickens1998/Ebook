var hrc = window.hrc || {};

/*定义折叠动作*/

hrc.tabAction = {}
//设置tab选中，如果网站有自定义的全局导航时可以使用样式即可 
hrc.tabAction.setTab = function (subNavtext) {
    $(".hrc-selectedSubNav").removeClass("hrc-selectedSubNav");
    $(".hrc-leftNavDiv a:contains('" + subNavtext + "')").each(function () {
        $(this).parent().addClass("hrc-selectedSubNav");
    });
}

//设置二级导航
hrc.tabAction.setSubNaviTab = function (subNavtext) {
    $("#hrc-snc a").removeClass("hrc-selectedsnc");
    $("#hrc-snc a:contains('" + subNavtext + "')").each(function () {
        $(this).addClass("hrc-selectedsnc");
    });
}


//折叠某一个元素，传入该元素的ID
hrc.tabAction.panelSwitch = function (tid) {
    $("#" + tid).slideToggle();
}

//展开/折叠 arguments: tid:控制展开的元素ID  did:需要展开的层
hrc.tabAction.foldSwitch = function (tid, did) {
    $("#" + did).slideToggle();
    if ($("#" + tid).hasClass("hrc-collapse")) {
        $("#" + tid).removeClass("hrc-collapse")
        $("#" + tid).addClass("hrc-expand");
    }
    else {
        $("#" + tid).removeClass("hrc-expand")
        $("#" + tid).addClass("hrc-collapse");
    }
}
//折叠左侧区域 arguments: tid:控制折叠的元素ID 
hrc.tabAction.foldLeftSide = function (tid) {
    $("#" + tid).toggle();
    if (document.getElementById(tid).style.display == "none") {
        document.getElementById("TLFrightlayer").style.width = "958px";
        $(".hrc-TLFleftlayer").css("width", "30");
        $(".hrc-TLFleftlayer a").addClass("close");
    }
    else {
        document.getElementById("TLFrightlayer").style.width = "787px";
        $(".hrc-TLFleftlayer").css("width", "200");
        $(".hrc-TLFleftlayer a").removeClass("close");
    }
}
//定义高级查询区域的折叠和展开 arguments:  panelID:需要展开的层的ID controlID:控制展开的元素ID 
hrc.tabAction.searchSwitch = function (panelID, controlID) {
    $("#" + panelID).toggle();
    if ($("#" + controlID).hasClass("hrc-advanceselect")) {
        $("#" + controlID).removeClass("hrc-advanceselect");
        $("#" + controlID).removeClass("hrc-tableoperationOpen");
        $(".hrc-tableoperation").removeClass("hrc-tableoperationOpen");
    }
    else {
        $("#" + controlID).addClass("hrc-advanceselect");
        $("#" + controlID).addClass("hrc-tableoperationOpen");
        $(".hrc-tableoperation").addClass("hrc-tableoperationOpen");
    }
}
//定义某一个tab的显示的隐藏 arguments:  tab_name:需要隐藏的层 ID 
hrc.tabAction.showTab = function (tab_name) {
    $(".hrc-tabselect").removeClass("hrc-tabselect");
    $("#" + tab_name).parent().addClass("hrc-tabselect");

    $(".hrc-tbdiv").hide();
    $("#" + tab_name + "Content").show();
}
//显示高级查询过滤条件 arguments:  tid:需要显示的层 ID 
hrc.tabAction.setFilter = function (tid) {
    $("#" + tid).show();
}
//清除过滤条件  arguments:  tid:需要隐藏的层 ID 
hrc.tabAction.clearFilter = function (tid) {
    $("#" + tid).hide();
}


/*定义公共组件*/
hrc.common = {}
hrc.common.hrcUrl = "http://hrc.oa.com";
hrc.common.rhrcUrl = "http://r.hrc.oa.com";

/*加载头部 arguments(对象) 接受参数：(可空/可不传)
username: string 当前登录用户
logoutUrl: string 退出登录地址
noneNav: true/false bool 是否显示导航
systemname: string  系统名称 olive/portal/zhaopin/kernel/hrmove/null(无任何选中tab)
submenu: object(二级导航)
    接受参数: title: string 二级导航标题
             href: string 导航链接
             onclick: function 响应函数
*/
hrc.common.hrcHeader = function (obj) {
    if ($(".hrc-header").length != 0)
        return;
    /*作obj判断*/
    if (obj == null || obj.username == null || obj.logoutUrl == null) {
        alert("HRC-头部缺少参数！");
        return;
    }

    $("body").prepend('<div class="hrc-header"></div>');
    $(".hrc-header").append('<div class="hrc-logoArea"></div>');
    $(".hrc-logoArea").append('<div id="hrc-logo" class="hrc-logo"></div>');
    //$(".hrc-logoArea").append('<div class="hrc-top-feedback "><a href="javascript:void(0);" class="hrc_feedbacktext">意见反馈</a></div>');

    if (obj.noneNav == null || obj.noneNav == false) {

        $(".hrc-header").append('<div class="hrc-nav"></div>');

        var headerdata = [{ "url": "http://183.58.24.145:8082/Schedule/MySchedule", "name": "MyOA" },
                          { "url": "", "name": "Foundation" },
                          { "url": "", "name": "人事管理" },
                          { "url": "", "name": "考勤管理" },
                          { "url": "", "name": "薪酬福利" },
                          { "url": "http://183.58.24.145:8081/DefaultWeb.aspx", "name": "Dev Management" }]

        var headerHtml = '<ul>';

        $.each(headerdata, function (key, value) {
            //console.log(key + " " + value.url + " " + value.name);
            var isSelected = value.name == obj.systemname ? 'class="hrc-selectedNav"' : '';
            var item = '<li><a href="' + value.url + '" ' + isSelected + '>' + value.name + '</a></li>';
            headerHtml += item;
        });

        headerHtml += '</ul>';

        $(".hrc-nav").append(headerHtml);
        
        //if (obj.submenu != null) {
        //    $(".hrc-header").append('<div class="hrc-secondNav"></div>');
        //    var submenu = '<ul id="hrc-snc">';
        //    var str = '';
        //    for (var i = 0; i < obj.submenu.length; i++) {
        //        var href = obj.submenu[i].href == null ? 'javascript:void(0)' : obj.submenu[i].href;
        //        var click = obj.submenu[i].onclick == null ? 'javascript:void(0)' : obj.submenu[i].onclick;
        //        var title = obj.submenu[i].title == null ? '未定义' : obj.submenu[i].title;

        //        str += ' <li><a href="' + href + '" onclick=\'' + click + '\' >' + title + '</a></li>';
        //        if (i < obj.submenu.length - 1)
        //            str += ' | ';
        //    }
        //    submenu += str + '</ul>';
        //    $(".hrc-secondNav").append(submenu);
        //}
    }
    /*global Top menu*/
    //$("body").prepend('<div class="hrc-topNav"></div>');
    //$(".hrc-topNav").append('<div class="hrc-navContent"></div>');
    //$(".hrc-navContent").append('<div class="hrc-home"></div>');
    //$(".hrc-navContent").append('<div class="hrc-personalInform"></div>');
    //$(".hrc-personalInform").append('<div id="hrc-currentLogonStaff"></div>');
    //var engName = obj.username.split('(')[0];
    //$("#hrc-currentLogonStaff").append('<img style="width:20px; height:20px;" src="http://r.hrc.oa.com/photo/48/' + engName + '.png" alt="' + obj.username + '" /><a href="http://hrstaff.oa.com/hr/HRStaff">' + obj.username + '</a>');    
    //$("#hrc-currentLogonStaff").append(obj.username);
    //$(".hrc-personalInform").append('<div id="hrc-exit"></div>');
    //$("#hrc-exit").append('<a href="' + obj.logoutUrl + '">退出</a>');

    $("body").prepend('<div class="navbar navbar-inverse"></div>');
    $(".navbar-inverse").append('<div class="container"></div>');
    $(".navbar-inverse").css("border-radius", "0px");
    $(".navbar-inverse").css("margin-bottom", "0px");
    $(".container").append('<div class="navbar-header"><button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse"><span class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span></button></div>')
    $(".container").append('<div class="navbar-collapse collapse"></div>');
    $(".navbar-collapse").append('<ul class="nav navbar-nav navbar-right"></ul>');
    $(".navbar-right").append('<li class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">你好，' + obj.username + '</a></li>');
    $(".dropdown").append('<ul class="dropdown-menu"></ul>');
    $(".dropdown-menu").append('<li><a href="http://localhost:47062">帐号设置</a></li>');
    //$(".dropdown-menu").append('<li><a href="http://183.58.24.145:8088/">帐号设置</a></li>');
    $(".dropdown-menu").append('<li><a href="' + obj.logoutUrl + '">注销</a></li>');

    // $.cookie('name')
    //要动态加载cookies和data的js文件
    //$.ajax({
    //    url: hrc.common.hrcUrl + '/v1.1/pages/guider/guiderData/hrcreleasedata.js',
    //    dataType: 'jsonp',
    //    jsonpCallback: 'hrc_GetReleasedata',
    //    success: function (guiderdata) {
    //        var href = hrc.common.getCurrentDomain();
    //        var now = new Date();
    //        var month = now.getMonth() + 1;
    //        var date = now.getFullYear() + "-" + (month > 9 ? month : "0" + month) + "-" + (now.getDate() > 9 ? now.getDate() : "0" + now.getDate());
    //        for (var i = 0 ; i < guiderdata.length; i++) {
    //            if (href.indexOf(guiderdata[i].url.toLowerCase()) != -1 && guiderdata[i].endDate > date) {
    //                $("body").prepend("<div class='hrc-warning' style='text-align:center; '>" + guiderdata[i].data[0].text + "</div>");
    //                break;
    //            }
    //        }
    //    },
    //    error: function (e) {
    //        //alert('获取发布TIPS出错');
    //    }
    //});

    //请求水印
    //$.ajax({
    //    url: 'http://watermark.sec.tencent.com/watermark/webpage.watermark.php?tpye=qj&els=mainContent&front_x=20&front_y=20&color=00000&mask_txt=' + obj.username + '&mask_img=water.png&mask_type=txt&width=130&height=80&font_size=18&front_font=微软雅黑&front_background_alpha=0&front_txt_alpha=0.1&angle=15&front_x_space=150&front_y_space=100&background_color=00000&front_rows=20&front_cols=20',
    //    dataType: 'jsonp',
    //    success: function (data) {
    //    },
    //    error: function (e) {
    //    }
    //});
}
hrc.common.getCurrentDomain = function () {
    var key = window.location.href.toLowerCase();
    var qindex = key.indexOf('?');
    if (qindex > 0)
        key = key.substring(0, qindex);
    var qindex = key.indexOf('#');
    if (qindex > 0)
        key = key.substring(0, qindex);
    return key;
};
/*js create left menu*/
hrc.common.hrcLeftMenu = function (obj) {
    /*
    {
       [
       title:'',
       submenu: {
                [
                    title:'',
                    href: '',
                    onclick: ''
                ],
                }
       ],
    }
    */
    if ($(".hrc-leftNavDiv").length != 0)
        return;
    if (obj == null) {
        alert("缺少对象！");
        return;
    }
    $(".hrc-leftMenuMain").append('<div class="hrc-leftNavDiv"></div>');

    for (var i = 0 ; i < obj.length; i++) {
        var icon = obj[i].icon == null ? 'task' : obj[i].icon;
        $(".hrc-leftNavDiv").append('<div class="hrc-leftNavTitleArea"><a href="javascript:void(0);" onfocus="this.blur();" onclick="javascript:hrc.tabAction.panelSwitch(\'d_' + i + '\');"><i class="hrc-icon-' + icon + '"></i>' + obj[i].title + '</a></div>');
        $(".hrc-leftNavDiv").append('<div id="d_' + i + '" ></div>');

        var str = '<ul>';
        for (var j = 0; j < obj[i].submenu.length; j++) {
            var href = obj[i].submenu[j].href == null ? 'javascript:void(0)' : obj[i].submenu[j].href;
            var click = obj[i].submenu[j].onclick == null ? 'javascript:void(0)' : obj[i].submenu[j].onclick;
            var title = obj[i].submenu[j].title == null ? '未定义' : obj[i].submenu[j].title;
            str += ' <li><a href="' + href + '" onclick=\'' + click + '\' >' + title + '</a></li>';
        }
        str += '</ul> ';

        $('#d_' + i).append(str);
    }
    /*初始化左右侧等高*/
    $('.hrc-TLFrightlayer').css("min-height", $(".hrc-TLFleftlayer").height());
    /*初始化折叠*/
    $(".hrc-TLFfold").css("height", $(".hrc-TLFleftlayer").height());
    $(".hrc-TLFfold a").css("margin-top", $(".hrc-TLFleftlayer").height() / 2 - 50);


}

/*js create footer*/
hrc.common.hrcFooter = function (obj) {

    if ($(".hrc-footerDiv").length != 0)
        return;
    if (obj == null || obj.footerStr == null) {
        alert("HRC-底部缺少参数！");
        return;
    }
    $("body").append('<div class="hrc-footerDiv"></div>');
    $(".hrc-footerDiv").append('<div class="hrc-footer"></div>');
    $(".hrc-footer").append('<div class="hrc-supportInform">' + obj.footerStr + '技术支持：<span>8002</span>(人力资源IT热线)</div>');
    $(".hrc-footer").append('<div class="hrc-systemSign">' + 'Copyright &copy; 1998 - ' + new Date().getFullYear() + 'Tencent. All Rights Reserved. ' + '</div>');
}
/*hrc反馈封装*/
hrc.common.Feedback = function (url) {
    if ($(".hrc-float-feedback").length == 0)
        hrc.Feedback(url);
}

/*定义错误页的JS加载方式*/
hrc.common.error = function (obj) {
    if (obj == null || obj.container == null) {
        return;
    }
    var home = obj.homePage ? obj.homePage : "http://hr.oa.com";
    $(obj.container).append("<div id='hrc-error' class='hrc-commonPage'></div>");
    $("#hrc-error").append(" <div class='hrc-commonPageIcon-404'></div><div id='hrc-errorMain' class='hrc-commonPageMain'></div>");
    $("#hrc-errorMain").append("<span>很抱歉，出错了</span><p class='hrc-commonPageSubTitle'>小Q留校罚站，我们会尽快处理这个问题~</p><p class='hrc-commonPageOperation'>您现在可以：<a href='javascript:void(0);' onclick='javascript:history.go(-1);'>返回上一页</a> 或者 <a id='errorHome' href='#'>返回主页</a></p>");
    $("#errorHome").attr("href", home);
    $("#hrc-error").append('<div class="hrc-clearBoth"></div><div id="errorInfo" class="errorInfo"></div><div class="hrc-commonPageOthers"><span>去看看其它地方~</span><ul class="hrc-infoGuide"><li><a href="http://holiday.oa.com/holiday/Evaluate/HDEvaluateMain.aspx"><i class="hrc-entrance-q7"></i><span>我要休假</span></a></li> <li><a href="http://holiday.oa.com/v2"><i class="hrc-entrance-q7"></i><span>我的剩余假期</span> </a></li><li><a href="http://zhaopin.oa.com/resume/pages/recommend/RecommendNewResume.aspx"><i class="hrc-entrance-q5"></i><span>伯乐推荐</span> </a></li><li><a href="https://pay.oa.com/sbc/default.aspx"><i class="hrc-entrance-q8"></i><span>月薪查询</span> </a></li><li><a href="http://zhaopin.oa.com/zhaopin/Facade/InnerMarketPostList.aspx"><i class="hrc-entrance-q6"></i><span>内部人才市场</span> </a></li></ul><div class="hrc-clearBoth"></div></div>');
    var error = obj.error ? obj.error : "";
    $("#errorInfo").append(error);
}
/*定义无权限页的JS加载方式*/
hrc.common.deny = function (obj) {
    if (obj == null || obj.container == null) {
        return;
    }
    var home = obj.homePage ? obj.homePage : "http://hr.oa.com";
    $(obj.container).append("<div id='hrc-deny' class='hrc-commonPage'></div>");
    $("#hrc-deny").append(" <div class='hrc-commonPageIcon-noright'></div><div id='hrc-denyMain' class='hrc-commonPageMain'></div>");
    $("#hrc-denyMain").append("<span>很抱歉，出错了</span><p class='hrc-commonPageSubTitle'>您暂时没有访问该页面的权限，努力吃菜，快高长大~</p><p class='hrc-commonPageOperation'>您现在可以：<a href='javascript:void(0);' onclick='javascript:history.go(-1);'>返回上一页</a> 或者 <a id='denyHome' href='#'>返回主页</a></p>");
    $("#denyHome").attr("href", home);
    $("#hrc-deny").append('<div class="hrc-clearBoth"></div><div id="errorInfo" class="errorInfo"></div><div class="hrc-commonPageOthers"><span>去看看其它地方~</span><ul class="hrc-infoGuide"><li><a href="http://holiday.oa.com/holiday/Evaluate/HDEvaluateMain.aspx"><i class="hrc-entrance-q7"></i><span>我要休假</span></a></li> <li><a href="http://holiday.oa.com/v2"><i class="hrc-entrance-q7"></i><span>我的剩余假期</span> </a></li><li><a href="http://zhaopin.oa.com/resume/pages/recommend/RecommendNewResume.aspx"><i class="hrc-entrance-q5"></i><span>伯乐推荐</span> </a></li><li><a href="https://pay.oa.com/sbc/default.aspx"><i class="hrc-entrance-q8"></i><span>月薪查询</span> </a></li><li><a href="http://zhaopin.oa.com/zhaopin/Facade/InnerMarketPostList.aspx"><i class="hrc-entrance-q6"></i><span>内部人才市场</span> </a></li></ul><div class="hrc-clearBoth"></div></div>');
    var error = obj.error ? obj.error : "";
    $("#errorInfo").append(error);
}

/*定义弹出层系列*/
hrc.box = {};
/*初始化popbox函数，通过定义class hrccbwidth hrccbheight hrccbtitle使用弹出层*/
hrc.box.init = function () {
    var objs = $(".hrc-colorbox");

    for (var i = 0; i < objs.length; i++) {
        var cbWidth = $(objs[i]).attr("hrccbwidth") || 600;//default 600 if width is not set 
        var cbheight = $(objs[i]).attr("hrccbheight") || 400; //default 400 if height is not set
        var cbTitle = $(objs[i]).attr("hrccbtitle") || $(objs[i]).attr("title") || "预览";
        var cbiframe = $(objs[i]).attr("hrccbiframe") != null && $(objs[i]).attr("hrccbiframe") == 'false' ? false : true; //by default not using iframe
        var cbinline = !cbiframe; //永远都是和iframe相反

        cb_width = (cbWidth * 1) + 30;
        cb_height = (cbheight * 1) + 40;

        $(objs[i]).popbox({ inline: cbinline, iframe: cbiframe, width: cb_width, height: cbheight, title: cbTitle, fixed: true, overlayClose: false, escKey: false, fastIframe: false });
        
    }

}

hrc.box.normal = function (obj) {
    if (obj.html)
        jQuery.popbox({ title: obj.title, width: obj.width, scrolling: obj.scrolling, html: obj.html, height: obj.height, overlayClose: false, escKey: obj.escKey, fastIframe: false });
    else {
        jQuery.popbox({ title: obj.title, width: obj.width, scrolling: obj.scrolling, href: obj.href, iframe: true, height: obj.height, overlayClose: false, escKey: obj.escKey, fastIframe: false });
    }

}

hrc.box.close = function () {
    (parent || top).$.popbox.close();
}

hrc.box.closeCurrent = function () {
    $.popbox.close();
}

/*定义block ui，弹出等待函数*/
hrc.box.startBlock = function (obj) {
    if (!obj)
        obj = {};

    if (!obj.message) {
        obj.message = "正在加载中，请稍后...";
    }


    var alertHtml = '<i class="hrc-loading"></i>' + obj.message;
    $.blockUI({
        css: {
            padding: '40px 50px 40px 20px',
            width: '260px',
            margin: 'auto',
            left: ($(window).width() - 330) / 2 + 'px',
            top: ($(window).height() - 120) / 2 + 'px',
            textAlign: 'center',
            color: '#999',
            border: '5px solid #ccc',
            backgroundColor: '#fff',
            cursor: 'wait'
        }, message: alertHtml
    });
}

hrc.box.closeBlock = function () {
    $.unblockUI();
}

/*定义各种alert函数*/
hrc.box.messageBox = function (obj) {
    hrc.box.closeBlock();
    if (!obj)
        obj = {};
    /*type: success warning error information*/
    if (!obj.type) {
        obj.type = "success";
    }
    if (!obj.title) {
        obj.title = "确定这么做吗？";
    }
    if (!obj.message) {
        obj.message = "";
    }
    if (!obj.confirmText) {
        obj.confirmText = "确定";
    }
    if (!obj.cancelText) {
        obj.cancelText = "取消";
    }
    if (!obj.confirmCallBack) {
        obj.confirmCallBack = function () {
            $.popbox.close();
        };
    }
    var alertElement = document.createElement("div");
    alertElement.setAttribute("class", "hrc-alert");
    alertElement.setAttribute("className", "hrc-alert");

    var alertHtml = "";
    switch (obj.type) {
        case "success":
            {
                alertHtml = '<table class="hrc-noborderTable" ><tr><td width="45px"><div class="hrc-successalert"></div></td><td><span class="hrc-alertTitle">' + obj.title + '</span></td></tr><tr><td></td><td>' + obj.message + '</td></tr><tr><td></td><td><a class="hrc-btn-submit"><span>' + obj.confirmText + '</span> </a></td></tr></table>';
                break;
            }
        case "warning":
            {
                alertElement.setAttribute("class", "hrc-alert hrc-alert-warning");
                alertElement.setAttribute("className", "hrc-alert hrc-alert-warning");
                alertHtml = '<table class="hrc-noborderTable" ><tr><td width="45px"><div class="hrc-warnalert"></div></td><td><span class="hrc-alertTitle">' + obj.title + '</span></td></tr><tr><td></td><td>' + obj.message + '</td></tr><tr><td></td><td><a class="hrc-btn-submit" ><span>' + obj.confirmText + '</span> </a><a class="hrc-btn-common"><span>' + obj.cancelText + '</span> </a></td></tr></table>';
                break;
            }
        case "error":
            {
                alertHtml = '<table class="hrc-noborderTable" ><tr><td width="45px"><div class="hrc-erroralert"></div></td><td><span class="hrc-alertTitle">' + obj.title + '</span></td></tr><tr><td></td><td>' + obj.message + '</td></tr><tr><td></td><td><a class="hrc-btn-submit"><span>' + obj.confirmText + '</span> </a></td></tr></table>';
                break;
            }
        case "information":
            {
                alertHtml = '<table class="hrc-noborderTable" ><tr><td width="45px"><div class="hrc-notialert"></div></td><td><span class="hrc-alertTitle">' + obj.title + '</span></td></tr><tr><td></td><td>' + obj.message + '</td></tr><tr><td></td><td><a class="hrc-btn-submit"><span>' + obj.confirmText + '</span> </a></td></tr></table>';
                break;
            }
        case "detail":
            {
                alertElement.setAttribute("style", "width:400px;");
                alertElement.style.width = "400px";
                alertHtml = '<table class="hrc-noborderTable"><tr><td width="45px"><div class="hrc-warnalert"></div></td><td><span class="hrc-alertTitle">' + obj.title + '</span></td></tr><tr><td></td><td  ><div style="width:100%; height:200px; overflow-y:scroll; margin:10px 0px;">' + obj.message + '</div></td></tr><tr><td>&nbsp;</td><td><a class="hrc-btn-submit" ><span>' + obj.confirmText + '</span> </a></td></tr></table>';
                break;
            }
        default:
            {
                alertHtml = '<table class="hrc-noborderTable" ><tr><td width="45px"><div class="hrc-successalert"></div></td><td><span class="hrc-alertTitle">' + obj.title + '</span></td></tr><tr><td></td><td>' + obj.message + '</td></tr><tr><td></td><td><a class="hrc-btn-submit"><span>' + obj.confirmText + '</span> </a></td></tr></table>';
                break;
            }
    }

    alertElement.innerHTML = alertHtml;

    jQuery.popbox({ html: alertElement, fixed: true, overlayClose: false, escKey: false, fastIframe: false });
    if ((typeof obj.confirmCallBack == 'function') && obj.confirmCallBack.constructor == Function) {
        $('.hrc-alert').delegate('.hrc-btn-submit', 'click', function () {
            obj.confirmCallBack();
            if (obj.type == 'warning' && obj.keepWarning) { }
            else
                $.popbox.close();
        });
    }
    else if ((typeof obj.confirmCallBack == 'string') && obj.confirmCallBack.constructor == String) {
        //if (window[obj.confirmCallBack]) {
        $('.hrc-alert').delegate('.hrc-btn-submit', 'click', function () {
            //alert();
            window[eval(obj.confirmCallBack)];
            if (obj.type == 'warning' && obj.keepWarning) { }
            else
                $.popbox.close();
        });
        //}
    }


    //hrc.box.callBack(alertElement, obj.confirmCallBack, obj);
    if (obj.type == 'warning') {
        $('.hrc-alert').delegate('.hrc-btn-common', 'click', function () {
            $.popbox.close();
        });
    }
}

hrc.box.dismissBox = function (obj) {
    hrc.box.closeBlock();
    if (!obj)
        obj = {};
    if (!obj.time) {
        obj.time = 2;
    }
    if (!obj.title) {
        obj.title = "操作成功！";
    }

    var alertHtml = '<div class="hrc-alert" style="width:140px;"><i class="hrc-ok"></i><span class="hrc-alertTitle">' + obj.title + '</span></div>';

    jQuery.popbox({ overlayClose: false, escKey: false, html: alertHtml });
    $("#hrcboxClose").css("display", "none");
    $("#hrcboxTitle").css("display", "none");

    setTimeout(function () {
        $.popbox.close();
    }, obj.time * 1000);
}
 

/*定义tips*/
var hrcTips = {
    $: function (ele) {
        if (typeof (ele) == "object") return ele;
        else if (typeof (ele) == "string" || typeof (ele) == "number") return document.getElementById(ele.toString());
        return null;
    },
    mousePos: function (e) {
        var x, y;
        var e = e || window.event;
        return {
            x: e.clientX + document.body.scrollLeft + document.documentElement.scrollLeft,
            y: e.clientY + document.body.scrollTop + document.documentElement.scrollTop
        };
    },
    start: function (obj) {
        var self = this;
        var t = self.$("hrcTips");
        obj.onmousemove = function (e) {
            var mouse = self.mousePos(e);
            t.style.left = mouse.x + 10 + 'px';
            t.style.top = mouse.y + 10 + 'px';
            if (obj.getAttribute("hrcTips") != null)
                t.innerHTML = obj.getAttribute("hrcTips");
            if (obj.getAttribute("hrcTips") != null && t.innerHTML != '')
                t.style.display = '';
        };
        obj.onmouseout = function () {
            t.style.display = 'none';
        };
    }
}

/*全局初始化，包括tips 表格和等高的左右侧效果*/
hrc.init = function () {

    /*初始化左右侧等高*/
    $('.hrc-TLFrightlayer').css("min-height", $(".hrc-TLFleftlayer").height());
    $('.hrc-TRFrightlayer').css("min-height", $(".hrc-TRFleftlayer").height());
    /*初始化折叠*/
    $(".hrc-TLFfold").css("height", $(".hrc-TLFleftlayer").height());
    $(".hrc-TLFfold a").css("margin-top", $(".hrc-TLFleftlayer").height() / 2 - 50);

    //alert("init ");
    //if ($("#hrcTips").length == 0) {
    var t = $('<div id="hrcTips" class="hrc-tips" style="position:absolute;left:0;top:0;display:none;"></div>');
    $("body").append(t);
    var btns = $(".hrcTips");
    btns.mouseover(function () { hrcTips.start(this); });
    //}
    /*初始化popbox弹出*/
    hrc.box.init();

    /*设置表格鼠标滑过换色*/
   /* $(".hrc-listTable tr").mouseover(function () {
        $(this).addClass("hrc-tableHover");
    })
    $(".hrc-listTable tr").mouseout(function () {
        $(this).removeClass("hrc-tableHover");
    })*/
    /*初始化输入框 placehoder*/
    //$('input[placeholder], textArea[placeholder]').placeholder();

    /*初始化鼠标滑过效果*/
    $(".hrc-toTop").mouseover(function () {
        $(".hrc-toTop").css("background-color", "#e2e2e2");
    });
    $(".hrc-toTop").mouseout(function () {
        $(".hrc-toTop").css("background-color", "transparent");
    });
    $(".hrc-feedback-follow").mouseover(function () {
        $(".hrc-feedback-follow").css("background-color", "#e2e2e2");
    });
    $(".hrc-feedback-follow").mouseout(function () {
        $(".hrc-feedback-follow").css("background-color", "transparent");
    });


    $('a,input[type="button"]').focus(function () {
        $(this).blur();      //在获取焦点的时候马上失去焦点；
    });
}



$(document).ready(function () {

    hrc.init();
    /*初始化不可用按钮*/
    $('a[disabled]:has(span)').each(function () {
        $(this).removeAttr('onclick');
        $(this).removeAttr('href');

        if ($(this).hasClass("hrc-btn-submit")) {
            $(this).addClass('hrc-submit-disabled');
        }
        if ($(this).hasClass("hrc-btn-common")) {
            $(this).addClass('hrc-common-disabled');
        }
        if ($(this).hasClass("hrc-btn-delete")) {
            $(this).addClass('hrc-delete-disabled');
        }
        if ($(this).hasClass("hrc-btn-filterSearch")) {
            $(this).addClass('hrc-filterSearch-disabled');
        }
        if ($(this).hasClass("hrc-btn-search")) {
            $(this).addClass('hrc-search-disabled');
        }
    });
    $('a[disabled]').each(function () {
        $(this).removeAttr('onclick');
        $(this).removeAttr('href');
        $(this).addClass('hrc-link-disabled');

    });
    /*初始化首页脚本*/
    $(".hrc-taskList .hrc-taskListOff").hover(function () {
        var index = $(".hrc-taskList .hrc-taskListOff").index(this);
        $(".hrc-taskList .hrc-taskListOff").removeClass("hrc-taskListOn");
        $('.hrc-taskSubContent').hide();

        $(".hrc-taskList .hrc-taskListOff").eq(index).addClass("hrc-taskListOn");
        $(".hrc-taskList li").eq(index).css("border-bottom-color", "#ccc");
        $('.hrc-taskSubContent').eq(index).css("top", (index + 1) * 30 + index + 1 + "px");
        $('.hrc-taskSubContent').eq(index).show();
    },
            function () {
                var index = $(".hrc-taskList .hrc-taskListOff").index(this);
                $(".hrc-taskList .hrc-taskListOff").removeClass("hrc-taskListOn");
                $(".hrc-taskList li").eq(index).css("border-bottom-color", "#f9f9f9");
                $('.hrc-taskSubContent').hide();
            });

    $(".hrc-taskSubContent").hover(function () {
        var index = $(".hrc-taskSubContent").index(this);
        $(".hrc-taskList .hrc-taskListOff").eq(index).addClass("hrc-taskListOn");
        $(".hrc-taskList li").eq(index).css("border-bottom-color", "#ccc");
        $('.hrc-taskSubContent').eq(index).css("top", (index + 1) * 30 + 1 + index + "px");
        $('.hrc-taskSubContent').eq(index).show();
    },
    function () {
        var index = $(".hrc-taskSubContent").index(this);
        $(".hrc-taskList .hrc-taskListOff").eq(index).removeClass("hrc-taskListOn");
        $(".hrc-taskList li").eq(index).css("border-bottom-color", "#f9f9f9");
        $('.hrc-taskSubContent').eq(index).hide();
    });

});
