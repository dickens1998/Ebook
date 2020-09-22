function bindSelectedSidebar() {
    var path = window.location.pathname;
    var sidebarArray = ["MySchedule", "MyOrderSchedule", "Authorize", "AuditTrail", "Teaching", "ExportMeetingSchedule", "ExportAllUnCompleteSchedule", "GetWorkTimeInOutForms", "ExportCalculateMeetingSchedules"];

    for (var i = 0; i < sidebarArray.length; i++) {
        if (path.toLowerCase().indexOf(sidebarArray[i].toLowerCase()) > -1) {

            $("." + sidebarArray[i]).addClass("selected");

            ////selectedTab = document.getElementById(sidebarArray[i]);
            ////selectedTab.className = "selected";
            //break;
        }
        else {
            $("." + sidebarArray[i]).removeClass("selected");
        }
    }
}

function unauthorized_error(e) {
    if (e.xhr.status === 401 || e.xhr.statusText == "Unauthorized") {
        window.location = "/Error/Unauthorized";
    }
}

//参数分别为日期对象，增加的类型，增加的数量
function dateAdd(date, strInterval, Number) {
    var dtTmp = date;
    switch (strInterval) {
        case 'second':
            return new Date(Date.parse(dtTmp) + (1000 * Number));
        case 'minute':
            return new Date(Date.parse(dtTmp) + (60000 * Number));
        case 'hour':
            return new Date(Date.parse(dtTmp) + (3600000 * Number));
        case 'day':
            return new Date(Date.parse(dtTmp) + (86400000 * Number));
        case 'week':
            return new Date(Date.parse(dtTmp) + ((86400000 * 7) * Number));
        case 'month':
            return new Date(dtTmp.getFullYear(), (dtTmp.getMonth()) + Number, dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());
        case 'year':
            return new Date((dtTmp.getFullYear() + Number), dtTmp.getMonth(), dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());
    }
}

function dateSubtract(date, strInterval, Number) {
    var dtTmp = date;
    switch (strInterval) {
        case 'second':
            return new Date(Date.parse(dtTmp) - (1000 * Number));
        case 'minute':
            return new Date(Date.parse(dtTmp) - (60000 * Number));
        case 'hour':
            return new Date(Date.parse(dtTmp) - (3600000 * Number));
        case 'day':
            return new Date(Date.parse(dtTmp) - (86400000 * Number));
        case 'week':
            return new Date(Date.parse(dtTmp) - ((86400000 * 7) * Number));
        case 'month':
            return new Date(dtTmp.getFullYear(), (dtTmp.getMonth()) - Number, dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());
        case 'year':
            return new Date((dtTmp.getFullYear() - Number), dtTmp.getMonth(), dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());
    }
}

//获取缩减后的字符
function getShortenedName(val, len) {
    var valLen = getCNStringLength(val);
    if (valLen > len)
        return val.substring(0, len) + "..."
    else
        return val;

}

//获取中文字符的长度
function getCNStringLength(val) {
    var len = 0;
    if (val) {
        for (var i = 0; i < val.length; i++) {
            var a = val.charAt(i);
            if (a.match(/[^\x00-\xff]/ig) != null)
                len += 2;
            else
                len += 1;
        }
    }
    return len;
}