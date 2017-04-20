//数组插入
Array.prototype.insert = function (index, o) {
    this.splice(index, 0, o);
};
Array.prototype.contains = function (elem) {
    if (this.length == 0) return false;
    for (var i = 0; i < this.length; i++) {
        if (this[i] == elem) return true;
    }
    return false;
};
Array.prototype.indexOf = function (val) {
    for (var i = 0; i < this.length; i++) {
        if (this[i] == val) {
            return i;
        }
    }
    return -1;
};
Array.prototype.remove = function (val) {
    var index = this.indexOf(val);
    if (index > -1) {
        this.splice(index, 1);
    }
};
Array.prototype.convertToString = function () {
    var str = "";
    for (var i = 0; i < this.length; i++) {
        str += this[i];
        str += "|";
    }
    return str.length > 0 ? str.substring(0, str.length - 1) : str;
};
//序列化
Array.prototype.Serialize = function () {
    var str = "";
    str += "[";
    for (var i = 0; i < this.length; i++) {
        str += "{";
        for (var o in this[i]) {
            var v = this[i][o];
            v = isNullOrEmpty(v) ? '\"\"' : v;
            str += "\"" + o + "\":\"" + this[i][o] + "\",";
        }
        if (str.substr(str.length - 1) == ",")
            str = str.substr(0, str.length - 1);
        str += "},";
    }
    if (str.substr(str.length - 1) == ",")
        str = str.substr(0, str.length - 1);
    str += "]";
    return str;
};
Array.prototype.toSpliceString = function () {
    var str = "";
    for (var i = 0; i < this.length; i++) {
        str += this[i] + ",";
    }
    if (str.substr(str.length - 1) == ",")
        str = str.substr(0, str.length - 1);
    return str;
};
var orderBy = function (filedName, reverse, primer) {
    reverse = (reverse) ? -1 : 1;
    return function (a, b) {
        a = a[filedName];
        b = b[filedName];

        if (typeof (primer) != "undefined") {
            a = primer(a);
            b = primer(b);
        }

        if (a < b) {
            return reverse * -1;
        }
        if (a > b) {
            return reverse * 1;
        }
    };
};
//数组排序 顺序
Array.prototype.OrderByAsc = function (filedName, primer) {
    this.sort(orderBy(filedName, false, primer));
    return this;
};
//数组排序 倒序
Array.prototype.OrderByDesc = function (filed, primer) {
    this.sort(orderBy(filedName, true, primer));
    return this;
};





//字符串去除空格
String.prototype.Trim = function () {
    return this.replace(/(^\s*)|(\s*$)/g, "");
};
String.prototype.LTrim = function () {
    return this.replace(/(^\s*)/g, "");
};
String.prototype.RTrim = function () {
    return this.replace(/(\s*$)/g, "");
};
String.prototype.Decode = function () {
    if (this == null || this == "" || typeof (this) == 'undefined') {
        return this;
    }
    return decodeURIComponent(decodeURIComponent(this));
};
String.prototype.htmlEncode = function () {
    var res = "";
    if (this.length == 0) return "";
    res = this.replace(/&/g, "&gt;");
    res = res.replace(/</g, "&lt;");
    res = res.replace(/>/g, "&gt;");
    res = res.replace(/ /g, "&nbsp;");
    res = res.replace(/\'/g, "&#39;");
    res = res.replace(/\"/g, "&quot;");
    res = res.replace(/\n/g, "<br>");
    return res;
}