angular.module('order.controllers', [])
    //订单列表页
    .controller('OrderCtrl', function($scope, appService, $rootScope, $ionicLoading, $state, $stateParams, $ionicHistory) {
        var vm = $scope;
        var orderId = $stateParams.orderId;
        if (orderId) {
            $state.go('tab.orderDetail', { id: orderId });
        }

        vm.items = [];
        vm.requestParams = {
            orderStatus: '',
            maxResultCount: 10,
            skipCount: 0
        }
        vm.getAll = function() {
            $ionicLoading.show();
            appService.post('OrderList', vm.requestParams).then(function(result) {
                vm.totalCount = result.data.result.totalCount;
                if (vm.requestParams.skipCount == 0) {
                    vm.items = result.data.result.items;
                } else {
                    vm.items = vm.items.concat(result.data.result.items);
                }
            }, function(result) {
                if (result.data && 　!result.data.unAuthorizedRequest)
                    vm.showError(result.data.error.message);
            }).finally(function(result) {
                $scope.$broadcast('scroll.refreshComplete');
                $scope.$broadcast("scroll.infiniteScrollComplete");
                $ionicLoading.hide();
            });
        };

        vm.queryBill = function(state) {
            vm.requestParams.orderStatus = state;
            vm.refresh();
            return true;
        };

        vm.refresh = function() {
            vm.requestParams.skipCount = 0;
            vm.getAll();
        };
        vm.moreDataCanBeLoaded = function() {
            if (vm.items.length == 0) return false;
            return (vm.totalCount != vm.items.length);
        };
        vm.loadMore = function() {
            vm.requestParams.skipCount = (vm.requestParams.skipCount + vm.requestParams.maxResultCount);
            vm.getAll();
        };

        vm.orderDetail = function(item) {
            $state.go('tab.orderDetail', { id: item.id })
        }

        //vm.$on("$stateChangeSuccess", function (event, toState, toParams, fromState, fromParams) {
        //    alert(123);
        //    if (fromState.name == 'tab.login' || fromState.name == 'tab.regist') {
        //        //只有来自注册和登陆页的时候才会重新加载乘客信息             
        //        vm.getAll();
        //    }
        //});
        vm.init = function() {
            vm.initCity();
        };
        vm.init();
        $scope.$on("$ionicView.afterEnter", function() {
            vm.getAll();
            $ionicHistory.clearHistory();
        });

    })
    //订单详情页
    .controller('OrderDetailCtrl', function($scope, $rootScope, appService, $ionicLoading, $state, $stateParams, $interval, $timeout) {
        var vm = $scope;
        vm.id = $stateParams.id;
        vm.init = function() {
            $ionicLoading.show();
            appService.post('GetOrderById', { id: vm.id }).then(function(result) {
                vm.data = result.data.result;
                showPayAgainButtonFunc(new Date(vm.data.creationTime), new Date(vm.data.serverNowTime), 600);
            }, function(result) {
                vm.showError(result.data.error.message);
            }).finally(function() {
                $scope.$broadcast('scroll.refreshComplete');
                $ionicLoading.hide();
            });
        };

        vm.refresh = function() {
            vm.init();
        };

        //再次购买
        vm.querySch = function() {
            $rootScope.schOption.cityId = vm.data.cityId;
            $rootScope.schOption.startStation = vm.data.carryStaId;
            $rootScope.schOption.stopId = vm.data.stopId;;
            $rootScope.schOption.stopName = vm.data.stopName;
            var nowTime = new Date(vm.data.serverNowTime);
            nowTime = new Date(nowTime.getFullYear(), nowTime.getMonth(), nowTime.getDate());
            $rootScope.schOption.drvDate = nowTime;
            $state.go('tab.sch');
        };
        //取消订单
        vm.cancelOrder = function() {
            vm.confirm('提示', '您确定要取消此订单吗？', function() {
                $ionicLoading.show();
                appService.post('cancelOrder', vm.data).then(function(result) {
                    vm.showSuccess('取消成功', function() {
                        vm.init();
                    });
                }, function(result) {
                    vm.showError(result.data.error.message);
                }).finally(function(result) {
                    $ionicLoading.hide();
                });
            });
        };

        //去支付
        vm.gotoPayAgan = function() {
            var item = vm.data;
            if (abp.versionType == 1) {
                $rootScope.gobackActionUse = false;
                console.log('set gobackActionUse false')
                $ionicLoading.show();
                appService.post('OrderRePay').then(function(result) {
                    if (item.paymentMethodSystemName == 'AliPayMethod') {
                        navigator.alipay.payV2({
                            "sign": result.data.result.sign,
                            "info": result.data.result.info
                        }, function(msgCode) {
                            $rootScope.showTimeOut('<h4>【支付宝】支付,等待返回支付结果</h4>', 5, vm.init);
                            $rootScope.gobackActionUse = true;
                        }, function(msg) {
                            $rootScope.showTimeOut('<h4>【支付宝】支付,等待返回支付结果</h4>', 5, vm.init);
                            $rootScope.gobackActionUse = true;
                        });
                    } else if (item.paymentMethodSystemName == 'WeiXinMethod') {
                        console.log(JSON.stringify(result.data.result));
                        wxpay.payment(
                            result.data.result,
                            function(msgCode) {
                                $rootScope.showTimeOut('<h4>【微信】支付,等待返回支付结果</h4>', 5, vm.init);
                                $rootScope.gobackActionUse = true;
                            },
                            function(msg) {
                                $rootScope.showTimeOut('<h4>【微信】支付,等待返回支付结果</h4>', 5, vm.init);
                                $rootScope.gobackActionUse = true;
                            }
                        );
                    } else {
                        //积分支付
                        $rootScope.showTimeOut('<h3>积分支付 支付</h3>', 5, vm.init);
                        $rootScope.gobackActionUse = true;
                    }

                }, function(result) {
                    $rootScope.gobackActionUse = true;
                    vm.showError(result.data.error.message);
                }).then(function(result) {
                    $ionicLoading.hide();
                });
            } else {
                window.location.href = '/pay';
            }
        }

        //显示去支付按键
        var showPayAgainButtonFunc = function(orderTime, nowTime, myActiveTime) {
            myActiveTime = myActiveTime - parseInt((nowTime.valueOf() - orderTime.valueOf()) / 1000);
            if (myActiveTime > 0) {
                $scope.ShowPayAgainButton = true;
                var s_to_ms = function(snum) {
                    var mi = parseInt(snum / 60);
                    if (mi < 10)
                        mi = '0' + mi;
                    var ss = snum % 60;
                    if (ss < 10)
                        ss = '0' + ss;
                    return mi + ':' + ss;
                }
                var timerCount = myActiveTime;
                $scope.payAgainBtnText = s_to_ms(timerCount);
                var locallastnowTime = new Date().valueOf();
                $scope.mycounter = $interval(function() {
                    var localnowTime = new Date().valueOf();
                    var timerCount = parseInt((localnowTime - locallastnowTime) / 1000);
                    timerCount = myActiveTime - timerCount;
                    $scope.payAgainBtnText = s_to_ms(timerCount);
                }, 1000);

                $scope.mytimeout = $timeout(function() {
                    $scope.ShowPayAgainButton = false;
                    $interval.cancel($scope.mycounter);
                }, myActiveTime * 1000);
            } else {
                $scope.ShowPayAgainButton = false;
            }
        };

        //判断是否可退票
        vm.isShowTuiKuan = function() {
            if (vm.data && (vm.data.orderStatusId == '待处理' || vm.data.orderStatusId == '取消')) return false;
            return true;
        };

        //退票说明
        vm.refundNotice = function() {
            $state.go('tab.refundNotice');
        };

        //退票
        vm.tuikuan = function(item) {
            $rootScope.tickItem = item;
            $state.go('tab.tuikuan');
        };
        // 关闭计时器
        vm.$on("$ionicView.beforeLeave", function() {
            if ($scope.mytimeout)
                $timeout.cancel($scope.mytimeout);
            if ($scope.mycounter)
                $interval.cancel($scope.mycounter);
        });

        // 刷新
        vm.$on("$ionicView.afterEnter", function() {
            if ($scope.mytimeout)
                $timeout.cancel($scope.mytimeout);
            if ($scope.mycounter)
                $interval.cancel($scope.mycounter);
            vm.init();
        });
    })
    .controller('TuikuanCtrl', function($scope, $rootScope, appService, $ionicLoading, $ionicHistory) {
        $scope.save = function() {
            $ionicLoading.show();
            appService.post('ReturnRequest', $rootScope.tickItem).then(function(result) {
                $scope.showSuccess('申请成功,请耐心等待我们的审核', function() {
                    $ionicHistory.goBack(-1);
                });

            }, function(result) {
                $scope.showError(result.data.error.message);
            }).finally(function(result) {
                $ionicLoading.hide();
            });
        };
    })
    .controller('RefundNoticeCtrl', function($scope, $rootScope, $ionicLoading, appService, $sce) {
        var init = function() {
            $ionicLoading.show();
            appService.post('RefundTicketNotice').then(function(result) {
                $scope.notice = $sce.trustAsHtml(result.data.result);
                console.log($scope.notice);
            }, function(result) {

            }).finally(function(result) {
                $ionicLoading.hide();
            });
        };
        init();
    })
    .controller('InvoiceAddressCtrl', function($scope, $rootScope, appService, $ionicLoading, $state, $stateParams, $ionicHistory) {
        var vm = $scope;
        vm.formModel = {
            orderId: $stateParams.orderId
        };
        vm.save = function() {
            $ionicLoading.show();
            appService.post("InvoiceAddress", vm.formModel).then(function(result) {
                vm.showSuccess('申请成功', function() {
                    $ionicHistory.goBack(-1);
                });
            }, function(result) {
                $scope.showError(result.data.error.message);
            }).finally(function(result) {
                $ionicLoading.hide();
            });
        };

    })