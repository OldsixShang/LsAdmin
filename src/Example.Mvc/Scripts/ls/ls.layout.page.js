/// <reference path="ls.common.js" />
(function ($) {
	$.ls = $.ls || {};
	$.extend($.ls, {
		getContentContainerHeight: function () {
			return $(window).height() - $(".container-btn").height() - $(".container-btn").height() - 145;
		},
		getContentContainerWidth: function () {
			return $(window).width() - 20;
		}
	});
})(jQuery)