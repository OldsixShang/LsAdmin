// Ionic Starter App

// angular.module is a global place for creating, registering and retrieving Angular modules
// 'starter' is the name of this angular module example (also set in a <body> attribute in index.html)
// the 2nd parameter is an array of 'requires'
// 'starter.services' is found in services.js
// 'starter.controllers' is found in controllers.js
angular.module('starter', ['ionic', 'MyFilter', 'starter.controllers', 'account.controllers', 'order.controllers', 'ionic-datepicker', 'starter.services', 'app-route'])

.run(function($ionicPlatform,
    $rootScope, $state, $ionicHistory, $timeout, $location,
    $ionicLoading, appService, $ionicPopup, $ionicPlatform, $interval,
    ionicDatePicker, ModalService, $cordovaFileTransfer, $cordovaFileOpener2) {
    $ionicPlatform.ready(function() {
        if (window.StatusBar) {
            // org.apache.cordova.statusbar required
            StatusBar.styleDefault();
        }
        //////////////////////////////////////
        ////判断浏览器的类型的
        //if (ionic.Platform.isAndroid()) {
        //    abp.versionType = 1;//0 touch , 1 app
        //    abp.orderSourceId = 20; //10 touch ,20 android ,30 ios 
        //    alert('android');
        //} else if (ionic.Platform.isIOS()) {
        //    abp.versionType = 1;//0 touch , 1 app
        //    abp.orderSourceId = 30; //10 touch ,20 android ,30 ios 
        //    alert('ios');
        //} else {
        //    abp.versionType = 0;//0 touch , 1 app
        //    abp.orderSourceId = 20; //10 touch ,20 android ,30 ios 
        //    alert('web');
        //}
        //////////////////////////////////////

        if (abp.versionType == 1) {
            navigator.splashscreen.hide();
            //检查网络
            $rootScope.checkConnection();
            $rootScope.checkUpdate(false);
            $rootScope.registerMyBackButton();
            cordova.plugins.Keyboard.hideKeyboardAccessoryBar(false);
        }

    });
    $rootScope.query = {
        selectedCity: {},
        selectedStation: {}
    };
    $rootScope.schOption = {
        cityId: '',
        startStation: '',
        drvDate: new Date(),
        stopName: '选择到达站点',
        stopId: ''
    };
    $rootScope.showError = function(msg, fn, time) {
        if (!time) time = 2000;
        var myPopup = $ionicPopup.show({
            title: '<b>' + msg + '</b>',
            cssClass: "er-popup",
            template: '<p style="text-align: center"><i class="icon ion-close-circled assertive" style="font-size:35px"></i></p>'
        });
        myPopup.then(function(res) {
            var back = fn || function() {};
            back();
        });
        $timeout(function() {
            myPopup.close(); // 2秒后关闭
        }, time);
    }
    $rootScope.showWarn = function(msg, fn) {
        var myPopup = $ionicPopup.show({
            title: '<b>' + msg + '</b>',
            cssClass: "er-popup",
            template: '<p style="text-align: center"><i class="icon ion-alert-circled energized" style="font-size:35px"></i></p>'
        });
        myPopup.then(function(res) {
            var back = fn || function() {};
            back();
        });
        $timeout(function() {
            myPopup.close(); // 2秒后关闭
        }, 2000);
    }
    $rootScope.showSuccess = function(msg, fn) {
        var myPopup = $ionicPopup.show({
            title: '<b>' + msg + '</b>',
            cssClass: "er-popup",
            template: '<p style="text-align: center"><ion-spinner icon="android" class="spinner-positive"></ion-spinner></p>'
        });
        myPopup.then(function(res) {
            var back = fn || function() {};
            back();
        });
        $timeout(function() {
            myPopup.close(); // 2秒后关闭
        }, 1000);
    }

    $rootScope.showTimeOut = function(htmlStr, time, fn) {
        var backAction = $rootScope.closeBackAction();
        $rootScope.timeSp = time;
        var myPopup = $ionicPopup.show({
            cssClass: "er-popup2",
            template: '<div style="text-align: center">' + htmlStr +
                '<div  style="font-size:18px;color:blue;">{{timeSp}}秒之后即将跳转</div>' +
                '</div>'
        });
        myPopup.then(function(res) {
            backAction();
            var back = fn || function() {};
            back();
        });
        var timer = $interval(function() {
            $rootScope.timeSp--;
        }, 1000);
        $timeout(function() {
            $interval.cancel(timer);
            myPopup.close();
        }, time * 1000);
    }


    $rootScope.showCitySelect = function() {
        $rootScope.showCitySelectPopup = $ionicPopup.show({
            cssClass: "er-popup2",
            template: '<div class="list">' +
                '<ion-radio class="item  item-icon-right" name="selectCity"' +
                ' ng-model="query.selectedCity" ng-repeat="item in city" ng-value="item"   ng-click="cityChange(this)">' +
                '<h2>{{item.cityName}}</h2>' +
                '<i class="icon  ion-ios-circle-outline" style="right:13px"></i>' +
                '</ion-radio>' +
                '</div>'
        });
    }

    $rootScope.showCarryStationSelect = function() {
        $rootScope.showCarryStationSelectPopup = $ionicPopup.show({
            cssClass: "er-popup2",
            template: '<div class="list">' +
                '<ion-radio class="item  item-icon-right" name="selectCarryStation"' +
                ' ng-model="query.selectedStation" ng-repeat="item in query.selectedCity.carryStations" ng-value="item"   ng-click="carrystaionChange(this)">' +
                '<h2>{{item.carryStationName}}</h2>' +
                '<i class="icon  ion-ios-circle-outline" style="right:13px"></i>' +
                '</ion-radio>' +
                '</div>'
        });
    }

    $rootScope.getImageSrc = function(pid) {
        return abp.appPath + "Profile/GetProfilePictureById?id=" + pid;
    };
    $rootScope.confirm = function(title, msg, fnOk, fnCancel) {
        var backAction = $rootScope.closeBackAction();
        var confirmPopup = $ionicPopup.confirm({
            title: title,
            template: msg,
            cssClass: "er-popup",
            okText: '确定',
            cancelText: '取消'
        });
        confirmPopup.then(function(res) {
            backAction();
            if (res) {
                fnOk = fnOk || function() {};
                fnOk();
            } else {
                fnCancel = fnCancel || function() {};
                fnCancel();
            }
        });
    };


    $rootScope.city = [];
    $rootScope.initCity = function() {
        if ($rootScope.schOption.cityId)
            return;
        $ionicLoading.show();
        appService.post('GetCityStation').then(function(result) {
            $rootScope.city = result.data.result;
            $rootScope.query.selectedCity = $rootScope.city[0];
            $rootScope.cityChange();
            console.log($rootScope.city);
        }, function(result) {

        }).finally(function(result) {
            $ionicLoading.hide();
        });
    };
    $rootScope.carrystaionChange = function() {
        $rootScope.schOption.startStation = $rootScope.query.selectedStation.carryStationId;
        var drvdate = new Date($rootScope.query.selectedStation.systemDate);
        $rootScope.schOption.drvDate = drvdate;
        var toDate = drvdate.valueOf() + $rootScope.query.selectedStation.preSaleDay * 24 * 60 * 60 * 1000;
        $rootScope.dateOptions.to = new Date(toDate);
        if ($rootScope.showCarryStationSelectPopup)
            $rootScope.showCarryStationSelectPopup.close();
    };

    $rootScope.cityChange = function() {
        $rootScope.schOption.cityId = $rootScope.query.selectedCity.cityId;
        $rootScope.query.selectedStation = $rootScope.query.selectedCity.carryStations[0];
        $rootScope.carrystaionChange();
        if ($rootScope.showCitySelectPopup)
            $rootScope.showCitySelectPopup.close();
    };

    //控制Touch版 ，浏览器刷新页面。刷新跳转到首页
    $rootScope.$on("$stateChangeStart", function(event, toState, toParams, fromState, fromParams) {
        if (toState.name == 'tab.myorder') {
            return;
        }
        if (toState.name != 'tab.home' && !$rootScope.schOption.cityId) {
            event.preventDefault();
            $state.go('tab.home');
        }
    });

    //APP 检查更新
    $rootScope.checkUpdate = function(showTip) {
        cordova.getAppVersion().then(function(version) {
            $rootScope.CurAppVersion = version;

            appService.post('AppVersion').then(function(result) {
                //console.log(JSON.stringify(result.data.result))
                if (result.data.result.appVersion != version) {
                    $rootScope.showUpdateConfirm(result.data.result);
                    //需要更新
                } else {
                    if (showTip) {
                        $rootScope.showSuccess('您已经是最新版本啦~~~');
                    }
                }
            })
        });
    };
    //Android 下载进度
    $rootScope.downloadProgress = 0;
    //弹出更新选择页面
    $rootScope.showUpdateConfirm = function(result) {
        var desc = "<div style='max-height: 80px'>" + result.appUpdateMsg + "<div>"
        var url_android = result.appUpdateUrl_android;
        var url_ios = result.appUpdateUrl_ios;
        var focused = result.appForcedUpdate;
        var obj = {
            title: '有新版本了！是否要升级？',
            subTitle: '请在wifi环境下更新',
            template: desc,
            cssClass: "er-popup",
            buttons: [{
                    text: '下一次',
                    type: 'button-default',
                    onTap: function(e) {
                        // 当点击时，e.preventDefault() 会阻止弹窗关闭。
                        return false;
                    }
                },
                {
                    text: '升级',
                    type: 'button-positive',
                    onTap: function(e) {
                        // 返回的值会导致处理给定的值。
                        return true;;
                    }
                }
            ]
        };
        if (focused) {
            obj.buttons = [{
                text: '升级',
                type: 'button-positive',
                onTap: function(e) {
                    // 返回的值会导致处理给定的值。
                    return true;;
                }
            }];
        }
        var backAction = $rootScope.closeBackAction();
        var confirmPopup = $ionicPopup.show(obj);
        confirmPopup.then(function(res) {
            backAction();
            if (res) {
                if (abp.orderSourceId == 20) {
                    $rootScope.androidUpdate(url_android);
                }
                if (abp.orderSourceId == 30) {
                    $rootScope.iosUpdate(url_ios);
                }


            } else {
                // 取消更新
            }

        });
    }

    //android更新
    $rootScope.androidUpdate = function(url) {
        $ionicLoading.show({
            template: "已经下载：0%"
        });
        var targetPath = cordova.file.externalDataDirectory + 'zhhz.apk'; //"file:///storage/sdcard0/Download/1.apk"; //APP下载存放的路径，可以使用cordova file插件进行相关配置
        var trustHosts = true
        var options = {};
        $cordovaFileTransfer.download(url, targetPath, options, trustHosts).then(function(result) {
            // 打开下载下来的APP
            $cordovaFileOpener2.open(targetPath, 'application/vnd.android.package-archive').then(function() {
                // 成功
            }, function(err) {
                console.log(JSON.stringify(err));
                // 错误
            });

            $ionicLoading.hide();
        }, function(err) {
            $rootScope.showError('下载失败');
            $ionicLoading.hide();
        }, function(progress) {
            //进度，这里使用文字显示下载百分比
            var downloadProgress = Math.floor((progress.loaded / progress.total) * 100);
            if (downloadProgress > $rootScope.downloadProgress) {
                $rootScope.downloadProgress = downloadProgress
                $timeout(function() {

                    console.log(Math.floor(downloadProgress));
                    $ionicLoading.show({
                        template: "已经下载：" + Math.floor(downloadProgress) + "%"
                    });
                    if (downloadProgress > 99) {
                        $rootScope.downloadProgress = 0;
                        $ionicLoading.hide();
                    }
                })
            }
        });
    }

    //ios 更新
    $rootScope.iosUpdate = function(url) {
        // var url ='https://itunes.apple.com/cn/app/易巴士汽车票/id931384182?mt=8';
        cordova.InAppBrowser.open(url, '_system', 'location=yes');

    }


    //检查网络
    $rootScope.checkNet = function() {};
    $rootScope.netConnection = true;
    //检查网略连接
    $rootScope.checkConnection = function() {
            if (navigator.connection) {
                var networkState = navigator.connection.type;

                var states = {};
                states[Connection.UNKNOWN] = 'Unknown connection';
                states[Connection.ETHERNET] = 'Ethernet connection';
                states[Connection.WIFI] = 'WiFi connection';
                states[Connection.CELL_2G] = 'Cell 2G connection';
                states[Connection.CELL_3G] = 'Cell 3G connection';
                states[Connection.CELL_4G] = 'Cell 4G connection';
                states[Connection.CELL] = 'Cell generic connection';
                states[Connection.NONE] = 'No network connection';
                if (networkState == Connection.NONE) {
                    $rootScope.netConnection = false;
                    var closeApp = function() {
                        ionic.Platform.exitApp();
                    }
                    $rootScope.showError('网络无法连接,即将关闭应用', closeApp, 5000);
                }
                $rootScope.netConnection = true;
            }
        }
        //android 设置是否后退
    $rootScope.gobackActionUse = true;
    // APP 物理返回按钮控制&双击退出应用
    $rootScope.registerMyBackButton = function() {
        $ionicPlatform.registerBackButtonAction(function(e) {
            e.preventDefault();
            console.log($location.path());
            console.log($rootScope.gobackActionUse);
            if (!$rootScope.gobackActionUse) {
                return false;
            }

            if ($location.path() == '/tab/home') {
                var appExit = function() {
                    ionic.Platform.exitApp();
                }
                $rootScope.confirm('<strong>退出应用?</strong>', '你确定要退出应用吗?', appExit);
            } else if ($location.path() == '/tab/account' || $location.path() == '/tab/news' || $location.path() == '/tab/myorder' ||
                $location.path() == '/tab/myorder/') {
                $state.go('tab.home');
            } else {
                $ionicHistory.goBack();
            }
            return false;
        }, 101);
    }

    $rootScope.closeBackAction = function() {
        return $ionicPlatform.registerBackButtonAction(function() { return; }, 401);
    }

    //$rootScope.backAction = $ionicPlatform.registerBackButtonAction(function() { return; }, 401)
    $rootScope.dateOptions = {
        callback: function(val) { //Mandatory
            $rootScope.schOption.drvDate = new Date(val);
            $rootScope.dateOptions.inputDate = $rootScope.schOption.drvDate;
            // console.log('Return value from the datepicker popup is : ' + val, new Date(val));
        },
        inputDate: $rootScope.schOption.drvDate,
        to: new Date(2018, 10, 30), //Optional
    };
    $rootScope.openDatePicker = function() {
        ionicDatePicker.openDatePicker($rootScope.dateOptions);
    };

    $rootScope.getWeekDate = function(drvDate) {
        var d = new Date(drvDate);
        var weekday = new Array(7)
        weekday[0] = "星期天"
        weekday[1] = "星期一"
        weekday[2] = "星期二"
        weekday[3] = "星期三"
        weekday[4] = "星期四"
        weekday[5] = "星期五"
        weekday[6] = "星期六"
        return weekday[d.getDay()];
    };
    $rootScope.isRefreshSchQuery = true; //是否刷新班次查询
    $rootScope.$on('userIntercepted', function(errorType) {
        // 跳转到登录界面，这里我记录了一个from，这样可以在登录后自动跳转到未登录之前的那个界面
        if ($state.current.name == "tab.account" || $state.current.name == 'tab.fkinfo') return;
        if ($state.current.name == "tab.myorder") {
            $state.go('tab.home');
            ModalService.show('AppPhone/templates/login.html', 'LoginCtrl', { 'from': 'tab.myorder' });
        } else if ($state.current.name == "tab.orderSch") {
            $rootScope.isRefreshSchQuery = false;
            $ionicHistory.goBack(-1);
            ModalService.show('AppPhone/templates/login.html', 'LoginCtrl', { 'from': $state.current.name });
        } else {
            $ionicHistory.goBack(-1);
            ModalService.show('AppPhone/templates/login.html', 'LoginCtrl', { 'from': $state.current.name });
        }


        //  $state.go("tab.login", { from: $state.current.name });
    });

})