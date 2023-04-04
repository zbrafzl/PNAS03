

function CallAction(type, url, data) {
    var result;
    $.ajax({
        type: type,
        url: url,
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (resp) {
            result = resp;
        },
        error: function (resp) {
            alert(resp);
        }
    });
    return result;

}

function CallActionGet(url) {

    var result;

    $.ajax({
        type: "GET",
        url: url,
        data: {},
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (resp) {
            result = resp;
        },
        error: function (resp) {
            alert(resp);
        }
    });

    return result;

}

//  contentType: "application/json; charset=utf-8",

function CallActionPost(url, obj) {
    var result;
    $.ajax({
        type: "POST",
        url: url,
        data: obj,
        dataType: "json",
        async: false,
        success: function (resp) {
            result = resp;
        },
        error: function (resp) {
            alert(resp);
        }
    });

    return result;

}

function GetCurrentDate() {
    return JsonToDate(CallActionGet("/Common/GetCurrentDateTime"));
}

function CreateGuid() {


    var dtStr = GetCurrentDate();

    var dt = new Date(dtStr);

    var dd = dt.getDate();
    var mm = Date.locale.month_names_short[dt.getMonth()]; // dt.getMonth() + 1; //January is 0!
    var yyyy = dt.getFullYear();

    var seconds = dt.getSeconds();
    var minutes = dt.getMinutes();
    var hour = dt.getHours();

    var guid = dd + "" + mm + "" + yyyy + "" + hour + "" + minutes + "" + seconds;

    return guid;

}

function FormatNumberToLength(num, length) {
    var r = "" + num;
    while (r.length < length) {
        r = "0" + r;
    }
    return r;
}

function DDLSetToDefault(ddlId, defaultText, defaultValue) {

    if (defaultText == "") defaultText = "Loading...";
    $("#" + ddlId).empty().append("<option selected='selected' value='" + defaultValue + "'>" + defaultText + "</option>");

}

function DDLSetItem(ddlId, defaultText, defaultValue) {

    if (defaultText == "") defaultText = "Loading...";
    if (defaultValue == "0") defaultValue = "-1";

    $("#" + ddlId).empty().append("<option selected='selected' value='" + defaultValue + "'>" + defaultText + "</option>");

}

function ConvertToInt(inputVal) {

    var outputVal = 0;
    try {
        if (isNaN(inputVal)) {
            outputVal = 0;
        }
        else if (inputVal === undefined) {
            outputVal = 0;
        }
        else if (inputVal == "") {
            outputVal = 0;
        }
        else if (inputVal == "NaN") {
            outputVal = 0;
        }
        else if (inputVal == "undefined") {
            outputVal = 0;
        }
        else if (inputVal == null) {
            outputVal = 0;
        } else {

            outputVal = parseInt(inputVal);
        }
    } catch (e) {
        outputVal = 0;
    }
    return outputVal;
}

function ConvertToDecimal(inputVal) {

    var outputVal = 0;
    try {
        if (isNaN(inputVal)) {
            outputVal = 0;
        }
        else if (inputVal === undefined) {
            outputVal = 0;
        }
        else if (inputVal == "") {
            outputVal = 0;
        }
        else if (inputVal == "NaN") {
            outputVal = 0;
        }
        else if (inputVal == "undefined") {
            outputVal = 0;
        }
        else if (inputVal == null) {
            outputVal = 0;
        } else {

            outputVal = inputVal.replace(/[^0-9\.]/g, '');

            if (outputVal.indexOf('.') > 0)
                outputVal = parseFloat(outputVal).toFixed(2);

        }
    } catch (e) {
        outputVal = 0;
    }
    return outputVal;
}

function ConvertToString(inputVal) {

    var outputVal = "";
    try {
        //if (isNaN(inputVal)) {
        //    outputVal = "";
        //}
        //else
        if (inputVal === undefined) {
            outputVal = "";
        }

        else if (inputVal == "NaN") {
            outputVal = "";
        }
        else if (inputVal == "undefined") {
            outputVal = "";
        }
        else if (inputVal == null) {
            outputVal = "";
        } else {

            outputVal = inputVal;
        }
    } catch (e) {
        outputVal = "";
    }


    return outputVal;
}


String.prototype.TooOnlyNumbers = function () {

    var txt = this.toString();

    var numberPattern = /\d+/g;

    txt = txt.match(numberPattern)

    return txt;

};

function ExtractOnlyNumbers(value) {
    var numberPattern = /\d+/g;
    value = value.match(numberPattern)
    return value;
}

function JsonToDate(dateStr) {
    ///Date(
    var dateTicks = parseInt(dateStr.TooOnlyNumbers());
    var dt = new Date(dateTicks);
    return dt;
}


Date.locale = {
    month_names: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
    month_names_short: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
};


function TooDateFormatDDMMYYYHHMMSS(dtStr) {

    var dt = new Date(dtStr);
    var dd = dt.getDate();
    var mm = Date.locale.month_names_short[dt.getMonth()]; // dt.getMonth() + 1; //January is 0!
    var yyyy = dt.getFullYear();

    var seconds = dt.getSeconds();
    var minutes = dt.getMinutes();
    var hour = dt.getHours();

    return dd + " " + mm + " " + yyyy + " " + hour + ":" + minutes + ":" + seconds;
};

function ToDateFormatDDMMYYY(dtStr) {

    var dt = new Date(dtStr);
    var dd = dt.getDate();
    var mm = Date.locale.month_names_short[dt.getMonth()]; // dt.getMonth() + 1; //January is 0!
    var yyyy = dt.getFullYear();

    return dd + " " + mm + " " + yyyy;
}

function ToDateFormatDDMMYYYWithSlash(dtStr) {

    var dt = new Date(dtStr);
    var dd = dt.getDate();
    var mm = dt.getMonth() + 1;
    var yyyy = dt.getFullYear();

    return dd + "/" + mm + "/" + yyyy;
}

function ToMaxLength(value, length) {

    var result = "";
    try {
        var len = ConvertToInt(value.length);
        if (len > length)
            value = value.substring(0, length);
    } catch (e) {

    } finally {
        result = value;
    }
    return result;
}

$(document).on("keydown", ".dated", function (e) {

    var result = this.value;

    try {
        result = $.trim(result);
        result = result.replace(/[^0-9/]/g, '');
    } catch (e) {

    } finally {
        this.value = result;
    }
});

$(document).on("input", ".reqfd", function () {

    var result = this.value;

    try {

        result = $.trim(result);
        if (result.length == 0) {
            $(this).addClass("req-fld");
        }
        else $(this).removeClass("req-fld");

    } catch (e) {

    } finally {

    }

});

$(document).on("change", ".reqfd", function () {

    var result = ConvertToInt($(this).val());

    try {
        result = $.trim(result);
        if (result == 0) {
            $(this).addClass("req-fld");
        }
        else $(this).removeClass("req-fld");

    } catch (e) {

    } finally {

    }

});

$(document).on("input", ".lenth", function () {

    var result = this.value;

    try {
        result = $.trim(result);
        var len = ConvertToInt($(this).attr("len"));
        if (result.length > len)
            result = result.substring(0, len);
    } catch (e) {

    } finally {
        this.value = result;
    }

});



$(document).on("textarea", ".lenth", function () {

    var result = this.value;

    try {
        result = result;
        var len = ConvertToInt($(this).attr("len"));
        if (result.length > len)
            result = result.substring(0, len);
    } catch (e) {

    } finally {
        this.value = result;
    }

});

$(document).on("input", ".alpha", function () {

    var result = this.value;

    try {
        //result = $.trim(result);
        result = result.replace(/[^a-zA-Z\s ]/g, '');
    } catch (e) {

    } finally {
        this.value = result;
    }

});

$(document).on("input", ".numeric", function () {

    var result = this.value.replace(/\D/g, '');

    try {
        result = $.trim(result);
        var len = ConvertToInt($(this).attr("len"));
        if (result.length > len)
            result = result.substring(0, len);
    } catch (e) {

    } finally {
        this.value = result;
    }

});

$(document).on("focusout", ".decimal", function () {

    var result = 0;

    try {
        result = $.trim(result);
        var len = ConvertToInt($(this).attr("len"));
        result = DecimalConverision(this.value, len);

    } catch (e) {

    } finally {
        this.value = result;
    }

});

function ValidateEmail(emailId) {

    var isValid = false;

    try {

        var format = /^[A-Z0-9._%+-]+@([A-Z0-9-]+\.)+[A-Z]{2,4}$/i;
        if (format.test(emailId))
            isValid = true;
        else
            isValid = false;

    } catch (e) {
        isValid = false;
    }

    return isValid
}

function ValidateDate(attrId, year) {

    year = ConvertToInt(year);
    if (year == 0) year = 1947;

    var spnAttr = attrId.replace("txt", "spn");

    $("#" + spnAttr).hide();

    var result = true;
    try {

        var value = $("#" + attrId).val();
        var arr = value.split('/');

        var dd = ConvertToInt(arr[0]);
        var mm = ConvertToInt(arr[1]);
        var yy = ConvertToInt(arr[2]);

        if (dd <= 0 && dd > 31)
            result = false;
        if (mm <= 0 && mm > 13)
            result = false;
        if (yy < year)
            result = false;

    } catch (e) {
        result = false;
    }

    if (result == false)
        $("#" + spnAttr).show();

    return result;
}

function DecimalConverision(input, len) {

    var result = 0;
    try {

        result = input.replace(/[^0-9\.]/g, '');

        if (len > 0 && result.length > len)
            result = result.substring(0, len);

        if (result.indexOf('.') > 0)
            result = parseFloat(result).toFixed(2);

    } catch (e) {
        result = 0;
    }

    return result;
}

function GetQuerystringValues(param) {

    var result = "";
    try {

        var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < url.length; i++) {
            var urlparam = url[i].split('=');
            if (urlparam[0] == param) {
                result = urlparam[1];
            }
        }

    } catch (e) {
        result = "";
    }
    if (result == "undefine")
        result = "";

    return result;

}

function UploadImage(uploaderId, applicantId, imageType) {

    var imageName = "";
    try {



        // Checking whether FormData is available in browser
        if (window.FormData !== undefined) {

            var fileUpload = $("#" + uploaderId).get(0);
            var files = fileUpload.files;

            // Create FormData object
            var fileData = new FormData();

            // Looping over all files and add it to FormData object
            for (var i = 0; i < files.length; i++) {
                fileData.append(files[i].name, files[i]);
            }

            // Adding one more key to FormData object
            fileData.append('applicantId', applicantId);
            fileData.append('imageType', imageType);
            fileData.append('inductionId', 9);

            $.ajax({
                url: '/Common/UploadImage',
                type: "POST",
                contentType: false, // Not to set any content header
                processData: false, // Not to process data
                async: false,
                data: fileData,
                success: function (result) {

                    imageName = "";
                    if (result.id > 0) {
                        imageName = result.msg;
                    }
                    else {
                        alert(result.msg);
                    }
                },
                error: function (err) {
                    alert(err.statusText);
                }
            });
        } else {

            imageName = "";
            alert("FormData is not supported.");

        }
    } catch (e) {
        imageName = "";
    }

    return imageName;
}

function UploadImageAdmin(uploaderId, applicantId, imageType, inductionId, folderPath) {

    var imageName = "";
    try {



        // Checking whether FormData is available in browser
        if (window.FormData !== undefined) {

            var fileUpload = $("#" + uploaderId).get(0);
            var files = fileUpload.files;

            // Create FormData object
            var fileData = new FormData();

            // Looping over all files and add it to FormData object
            for (var i = 0; i < files.length; i++) {
                fileData.append(files[i].name, files[i]);
            }

            // Adding one more key to FormData object
            fileData.append('applicantId', applicantId);
            fileData.append('imageType', imageType);
            fileData.append('folderPath', folderPath);
            fileData.append('inductionId', inductionId);

            $.ajax({
                url: '/CommonFunctionsAdmin/UploadImage',
                type: "POST",
                contentType: false, // Not to set any content header
                processData: false, // Not to process data
                async: false,
                data: fileData,
                success: function (result) {

                    imageName = "";
                    if (result.id > 0) {
                        imageName = result.msg;
                    }
                    else {
                        alert(result.msg);
                    }
                },
                error: function (err) {
                    alert(err.statusText);
                }
            });
        } else {

            imageName = "";
            alert("FormData is not supported.");

        }
    } catch (e) {
        imageName = "";
    }

    return imageName;
}

var dateFormat = function () {
    var token = /d{1,4}|m{1,4}|yy(?:yy)?|([HhMsTt])\1?|[LloSZ]|"[^"]*"|'[^']*'/g,
        timezone = /\b(?:[PMCEA][SDP]T|(?:Pacific|Mountain|Central|Eastern|Atlantic) (?:Standard|Daylight|Prevailing) Time|(?:GMT|UTC)(?:[-+]\d{4})?)\b/g,
        timezoneClip = /[^-+\dA-Z]/g,
        pad = function (val, len) {
            val = String(val);
            len = len || 2;
            while (val.length < len) val = "0" + val;
            return val;
        };

    // Regexes and supporting functions are cached through closure
    return function (date, mask, utc) {
        var dF = dateFormat;

        // You can't provide utc if you skip other args (use the "UTC:" mask prefix)
        if (arguments.length == 1 && Object.prototype.toString.call(date) == "[object String]" && !/\d/.test(date)) {
            mask = date;
            date = undefined;
        }

        // Passing date through Date applies Date.parse, if necessary
        date = date ? new Date(date) : new Date;
        if (isNaN(date)) throw SyntaxError("invalid date");

        mask = String(dF.masks[mask] || mask || dF.masks["default"]);

        // Allow setting the utc argument via the mask
        if (mask.slice(0, 4) == "UTC:") {
            mask = mask.slice(4);
            utc = true;
        }

        var _ = utc ? "getUTC" : "get",
            d = date[_ + "Date"](),
            D = date[_ + "Day"](),
            m = date[_ + "Month"](),
            y = date[_ + "FullYear"](),
            H = date[_ + "Hours"](),
            M = date[_ + "Minutes"](),
            s = date[_ + "Seconds"](),
            L = date[_ + "Milliseconds"](),
            o = utc ? 0 : date.getTimezoneOffset(),
            flags = {
                d: d,
                dd: pad(d),
                ddd: dF.i18n.dayNames[D],
                dddd: dF.i18n.dayNames[D + 7],
                m: m + 1,
                mm: pad(m + 1),
                mmm: dF.i18n.monthNames[m],
                mmmm: dF.i18n.monthNames[m + 12],
                yy: String(y).slice(2),
                yyyy: y,
                h: H % 12 || 12,
                hh: pad(H % 12 || 12),
                H: H,
                HH: pad(H),
                M: M,
                MM: pad(M),
                s: s,
                ss: pad(s),
                l: pad(L, 3),
                L: pad(L > 99 ? Math.round(L / 10) : L),
                t: H < 12 ? "a" : "p",
                tt: H < 12 ? "am" : "pm",
                T: H < 12 ? "A" : "P",
                TT: H < 12 ? "AM" : "PM",
                Z: utc ? "UTC" : (String(date).match(timezone) || [""]).pop().replace(timezoneClip, ""),
                o: (o > 0 ? "-" : "+") + pad(Math.floor(Math.abs(o) / 60) * 100 + Math.abs(o) % 60, 4),
                S: ["th", "st", "nd", "rd"][d % 10 > 3 ? 0 : (d % 100 - d % 10 != 10) * d % 10]
            };

        return mask.replace(token, function ($0) {
            return $0 in flags ? flags[$0] : $0.slice(1, $0.length - 1);
        });
    };
}();

// Some common format strings
dateFormat.masks = {
    "default": "ddd mmm dd yyyy HH:MM:ss",
    shortDate: "m/d/yy",
    mediumDate: "mmm d, yyyy",
    longDate: "mmmm d, yyyy",
    fullDate: "dddd, mmmm d, yyyy",
    shortTime: "h:MM TT",
    mediumTime: "h:MM:ss TT",
    longTime: "h:MM:ss TT Z",
    isoDate: "yyyy-mm-dd",
    isoTime: "HH:MM:ss",
    isoDateTime: "yyyy-mm-dd'T'HH:MM:ss",
    isoUtcDateTime: "UTC:yyyy-mm-dd'T'HH:MM:ss'Z'"
};

// Internationalization strings
dateFormat.i18n = {
    dayNames: [
        "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat",
        "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"
    ],
    monthNames: [
        "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec",
        "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"
    ]
};

// For convenience...
Date.prototype.format = function (mask, utc) {
    return dateFormat(this, mask, utc);
};