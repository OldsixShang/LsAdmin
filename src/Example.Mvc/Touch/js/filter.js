/**
 * Created by tts-kyyfzx on 2016/11/15.
 */
angular.module('MyFilter', [])
    .filter('timeSpan', function() {
        return function(input) {
            if (!input) return "";
            if (input.length <= 5) return input;
            return input.substr(0, 5);
        }
    })
    .filter('showTextlimit', function() {
        return function(input, limitNum) {
            if (!limitNum) limitNum = 4;

            if (!input) return "";
            if (input.length <= limitNum) return input;
            return input.substr(0, limitNum) + '...';
        }
    });