String.prototype.Trim = function () { return this.replace(/(^\s*)|(\s*$)/g, ""); };
String.prototype.LTrim = function () { return this.replace(/(^\s*)/g, ""); };
String.prototype.RTrim = function () { return this.replace(/(\s*$)/g, ""); };
String.Format = function () { var s = arguments[0]; var m = 0; for (m; m < arguments.length - 1; m++) { var reg = new RegExp("\\{" + m + "\\}", "gm"); s = s.replace(reg, arguments[m + 1]); } return s; };
String.prototype.endsWith = function (suffix) { return (this.substr(this.length - suffix.length) === suffix); };
String.prototype.startsWith = function (prefix) { return (this.substr(0, prefix.length) === prefix); };