angular.module("app-route", [])
    .factory('UserInterceptor', ["$q", "$rootScope", "localStorageService",
        function($q, $rootScope, localStorageService) {
            return {
                request: function(config) {
                    if (abp.versionType == 1) {
                        //config.headers["X-XSRF-TOKEN"] = null;
                        if ($rootScope.token) {
                            config.headers["Authorization"] = "bearer " + $rootScope.token;
                            return config;
                        }
                        var token = localStorageService.get('token');
                        if (token) {
                            $rootScope.token = token;
                            config.headers["Authorization"] = "bearer " + token;
                        }
                    }
                    return config;
                },
                responseError: function(response) {
                    var data = response.data;
                    // 判断错误码，如果是未登录                   
                    if (!response.data) return $q.reject(response);
                    if (response.data.unAuthorizedRequest) {
                        //说明没有登录
                        // 清空用户本地token存储的信息，如果
                        $rootScope.token = null;
                        localStorageService.set('token', $rootScope.token);
                        // 全局事件，方便其他view获取该事件，并给以相应的提示或处理
                        $rootScope.$emit("userIntercepted", "notLogin", response);
                    }
                    return $q.reject(response);
                }
            };
        }
    ])
    .config(function($stateProvider, $ionicConfigProvider, $urlRouterProvider, $httpProvider, ionicDatePickerProvider) {
        // $httpProvider.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';
        $httpProvider.interceptors.push('UserInterceptor');
        //  $ionicConfigProvider.scrolling.jsScrolling(false);
        $ionicConfigProvider.platform.default.tabs.position('bottom');
        $ionicConfigProvider.platform.default.tabs.style('standard');
        $ionicConfigProvider.platform.default.tabs.position('bottom');
        $ionicConfigProvider.platform.default.backButton.previousTitleText('返回').icon('ion-ios-arrow-back');
        $ionicConfigProvider.platform.default.navBar.alignTitle('center');
        $ionicConfigProvider.platform.default.views.transition('ios');

        $ionicConfigProvider.platform.ios.tabs.position('bottom');
        $ionicConfigProvider.platform.ios.tabs.style('standard');
        $ionicConfigProvider.platform.ios.tabs.position('bottom');
        $ionicConfigProvider.platform.ios.backButton.previousTitleText('返回').icon('ion-ios-arrow-back');
        $ionicConfigProvider.platform.ios.navBar.alignTitle('center');
        $ionicConfigProvider.platform.ios.views.transition('ios');
        $ionicConfigProvider.platform.ios.tabs.position('bottom');

        $ionicConfigProvider.platform.android.tabs.position('bottom');
        $ionicConfigProvider.platform.android.tabs.style('standard');
        $ionicConfigProvider.platform.android.tabs.position('bottom');
        $ionicConfigProvider.platform.android.backButton.previousTitleText('返回').icon('ion-ios-arrow-back');
        $ionicConfigProvider.platform.android.navBar.alignTitle('center');
        $ionicConfigProvider.platform.android.views.transition('ios');
        $ionicConfigProvider.platform.android.tabs.position('bottom');





        var datePickerObj = {
            inputDate: new Date(),
            setLabel: '确定',
            todayLabel: '今天',
            closeLabel: '关闭',
            mondayFirst: false,
            weeksList: ["周日", "周一", "周二", "周三", "周四", "周五", "周六"],
            monthsList: ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"],
            templateType: 'modal',
            from: new Date(),
            showTodayButton: true,
            dateFormat: 'yyyy-MM-dd',
            closeOnSelect: true
        };
        ionicDatePickerProvider.configDatePicker(datePickerObj);

        // Ionic uses AngularUI Router which uses the concept of states
        // Learn more here: https://github.com/angular-ui/ui-router
        // Set up the various states which the app can be in.
        // Each state's controller can be found in controllers.js
        $stateProvider

        // setup an abstract state for the tabs directive
            .state('tab', {
            url: '/tab',
            controller: 'TabCtrl',
            abstract: true,
            templateUrl: 'AppPhone/templates/tabs.html'
        })

        // Each tab has its own nav history stack:sch tab.login
        .state('tab.login', {
                url: '/login/:from',
                views: {
                    'tab-home': {
                        templateUrl: 'AppPhone/templates/login.html',
                        controller: 'LoginCtrl'
                    }
                }
            })
            .state('tab.regist', {
                url: '/regist/:from',
                views: {
                    'tab-home': {
                        templateUrl: 'AppPhone/templates/regist.html',
                        controller: 'RegistCtrl'
                    }
                }
            })
            .state('tab.forgetpw', {
                url: '/regist/:from',
                views: {
                    'tab-home': {
                        templateUrl: 'AppPhone/templates/account/forgetPassWord.html',
                        controller: 'ForgetPwCtrl'
                    }
                }
            })
            .state('tab.home', {
                url: '/home',
                views: {
                    'tab-home': {
                        templateUrl: 'AppPhone/templates/home/index.html',
                        controller: 'HomeCtrl'
                    }
                }
            })
            .state('tab.myorder', {
                url: '/myorder/:orderId',
                views: {
                    'tab-myorder': {
                        templateUrl: 'AppPhone/templates/order/index.html',
                        controller: 'OrderCtrl'
                    }
                }
            })
            //订单 详情 tab.orderDetail
            .state('tab.orderDetail', {
                url: '/myorder/detail/:id',
                views: {
                    'tab-myorder': {
                        templateUrl: 'AppPhone/templates/order/detail.html',
                        controller: 'OrderDetailCtrl'
                    }
                }
            })
            .state('tab.tuikuan', {
                url: '/myorder/detail/tuikuan',
                views: {
                    'tab-myorder': {
                        templateUrl: 'AppPhone/templates/order/tuikuan.html',
                        controller: 'TuikuanCtrl'
                    }
                }
            })
            .state('tab.invoiceAddress', {
                url: '/myorder/detail/invoiceAddress/:orderId',
                views: {
                    'tab-myorder': {
                        templateUrl: 'AppPhone/templates/order/invoiceAddress.html',
                        controller: 'InvoiceAddressCtrl'
                    }
                }
            })
            .state('tab.refundNotice', {
                url: '/myorder/detail/refundNotice/',
                views: {
                    'tab-myorder': {
                        templateUrl: 'AppPhone/templates/order/refundNotice.html',
                        controller: 'RefundNoticeCtrl'
                    }
                }
            })
            .state('tab.stop', {
                url: '/stop',
                views: {
                    'tab-home': {
                        templateUrl: 'AppPhone/templates/home/stop.html',
                        controller: 'StopCtrl'
                    }
                }
            })
            //班次列表
            .state('tab.sch', {
                url: '/sch',
                views: {
                    'tab-home': {
                        templateUrl: 'AppPhone/templates/home/sch.html',
                        controller: 'SchCtrl'
                    }
                }
            })
            //orderSch 预定班次
            .state('tab.orderSch', {
                url: '/orderSch',
                views: {
                    'tab-home': {
                        templateUrl: 'AppPhone/templates/home/orderSch.html',
                        controller: 'OrderSchCtrl'
                    }
                }
            })
            //tab.ticketNotice
            .state('tab.ticketNotice', {
                url: '/orderSch/ticketNotice',
                views: {
                    'tab-home': {
                        templateUrl: 'AppPhone/templates/home/ticketNotice.html',
                        controller: 'TicketNoticeCtrl'
                    }
                }
            })
            //选择保险
            .state('tab.chooseBaoxian', {
                url: '/orderSch/chooseBaoxian',
                views: {
                    'tab-home': {
                        templateUrl: 'AppPhone/templates/home/chooseBaoxian.html',
                        controller: 'ChooseBaoxianCtrl'
                    }
                }
            })
            .state('tab.baoXianNotice', {
                url: '/orderSch/baoXianNotice',
                views: {
                    'tab-home': {
                        templateUrl: 'AppPhone/templates/home/baoXianNotice.html',
                    }
                }
            })
            //选择乘客
            .state('tab.choosePassengers', {
                url: '/orderSch/choosePassengers',
                views: {
                    'tab-home': {
                        templateUrl: 'AppPhone/templates/home/choosePassengers.html',
                        controller: 'ChoosePassengersCtrl'
                    }
                }
            })
            //添加乘客
            .state('tab.createOrUpdatePassenger', {
                url: '/orderSch/createOrUpdatePassenger/:passengerId',
                views: {
                    'tab-home': {
                        templateUrl: 'AppPhone/templates/home/createOrUpdatePassenger.html',
                        controller: 'CreateOrUpdatePassengerCtrl'
                    }
                }
            })
            //placeOrder 预定班次
            .state('tab.placeOrder', {
                url: '/placeOrder',
                views: {
                    'tab-home': {
                        templateUrl: 'AppPhone/templates/home/placeOrder.html',
                        controller: 'PlaceOrderCtrl'
                    }
                }
            })
            .state('tab.account', {
                url: '/account',
                views: {
                    'tab-account': {
                        templateUrl: 'AppPhone/templates/account/index.html',
                        controller: 'AccountCtrl'
                    }
                }
            })
            .state('tab.personl', { //用户中心
                url: '/account/personl',
                views: {
                    'tab-account': {
                        templateUrl: 'AppPhone/templates/account/personl.html',
                        controller: 'PersonlCtrl'
                    }
                }
            })
            .state('tab.modifyPassword', { //修改密码
                url: '/account/personl/modifyPassword',
                views: {
                    'tab-account': {
                        templateUrl: 'AppPhone/templates/account/modifyPassword.html',
                        controller: 'ModifyPasswordCtrl'
                    }
                }
            })
            .state('tab.mypassenger', { //常用旅客
                url: '/account/mypassenger',
                views: {
                    'tab-account': {
                        templateUrl: 'AppPhone/templates/account/mypassenger.html',
                        controller: 'PassengersCtrl'
                    }
                }
            })
            .state('tab.modifyPassenger', { //修改旅客
                url: '/account/mypassenger/modifyPassenger',
                views: {
                    'tab-account': {
                        templateUrl: 'AppPhone/templates/account/modifyPassenger.html',
                        controller: 'ModifyPassengerCtrl'
                    }
                }
            })
            .state('tab.point', { //积分
                url: '/account/point',
                views: {
                    'tab-account': {
                        templateUrl: 'AppPhone/templates/account/point.html',
                        controller: 'PointCtrl'
                    }
                }
            })
            .state('tab.returnrequest', { //退款请求
                url: '/account/returnrequest',
                views: {
                    'tab-account': {
                        templateUrl: 'AppPhone/templates/account/returnrequest.html',
                        controller: 'ReturnRequestCtrl'
                    }
                }
            })
            .state('tab.mianze', { //免责声明
                url: '/account/mianze',
                views: {
                    'tab-account': {
                        templateUrl: 'AppPhone/templates/account/mianze.html',
                        controller: 'MianzeCtrl'
                    }
                }
            })
            .state('tab.aboutus', { //关于我们中心
                url: '/account/aboutus',
                views: {
                    'tab-account': {
                        templateUrl: 'AppPhone/templates/account/aboutus.html',
                        controller: 'AboutCtrl'
                    }
                }
            })
            .state('tab.suggest', { //投诉建议
                url: '/account/suggest',
                views: {
                    'tab-account': {
                        templateUrl: 'AppPhone/templates/account/suggestlist.html',
                        controller: 'SuggestCtrl'
                    }
                }
            })

        .state('tab.suggestadd', { //投诉建议
            url: '/account/suggestadd',
            views: {
                'tab-account': {
                    templateUrl: 'AppPhone/templates/account/suggest.html',
                    controller: 'SuggestaddCtrl'
                }
            }
        })

        .state('tab.vesion', { //版本信息
            url: '/account/vesion',
            views: {
                'tab-account': {
                    templateUrl: 'AppPhone/templates/account/vesion.html',
                    controller: 'VesionCtrl'
                }
            }
        })


        .state('tab.news', {
                url: '/news',
                views: {
                    'tab-news': {
                        templateUrl: 'AppPhone/templates/news/index.html',
                        controller: 'NewsCtrl'
                    }
                }
            })
            .state('tab.newsDetail', {
                url: '/newsDetail/:id',
                views: {
                    'tab-news': {
                        templateUrl: 'AppPhone/templates/news/detail.html',
                        controller: 'NewsDetailCtrl'
                    }
                }
            })
            .state('tab.homeNewsDetail', {
                url: '/homeNewsDetail/:id',
                views: {
                    'tab-home': {
                        templateUrl: 'AppPhone/templates/news/detail.html',
                        controller: 'NewsDetailCtrl'
                    }
                }
            })




        // if none of the above states are matched, use this as the fallback
        $urlRouterProvider.otherwise('/tab/home');
    });