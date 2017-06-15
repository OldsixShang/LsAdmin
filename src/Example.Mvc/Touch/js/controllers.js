angular.module('starter.controllers', [])
    //首页
    .controller('HomeCtrl', function($scope, $rootScope, $timeout, appService, $state, $ionicLoading, $ionicSlideBoxDelegate, $ionicHistory) {
        var vm = $scope;

        vm.requestParams = {
            maxResultCount: 5,
            skipCount: 0
        }
        var timer = $timeout(function() {
            $ionicSlideBoxDelegate.slide(0);;
        });
        $scope.slideChange = function() {
            $timeout.cancel(timer);
            if (vm.items.length == $ionicSlideBoxDelegate.currentIndex() + 1) {
                timer = $timeout(function() {
                    $ionicSlideBoxDelegate.slide(0);;
                }, 3000);
            } else {
                timer = $timeout(function() {
                    $ionicSlideBoxDelegate.next();
                }, 3000);

            }
        };



        vm.getStops = function() {
            appService.post('GetBusStop', {
                CityId: vm.query.selectedCity.cityId,
                CarrryStationId: vm.schOption.startStation
            }).then(function(result) {
                $rootScope.stops = result.data.result;
            }, function(result) {}).finally(function(result) {

            });;
        };

        $rootScope.$watch("schOption.startStation", function() {
            vm.getStops();
            $rootScope.schOption.stopName = '选择到达站点';
            $rootScope.schOption.stopId = '';
        });

        vm.gostop = function() {
            $state.go('tab.stop');
        }
        vm.init = function() {
            vm.initCity();

            appService.post('NewsList', vm.requestParams).then(function(result) {
                vm.items = result.data.result.items;
                $ionicSlideBoxDelegate.update();
                if (vm.items.length > 0) {
                    $ionicSlideBoxDelegate.slide(0);;
                    if (vm.items.length > 1) {
                        timer = $timeout(function() {
                            $ionicSlideBoxDelegate.next(0);;
                        }, 3000);
                    }
                };
            }, function(result) {
                vm.showError(result.data.error.message);
            });
        };


        vm.searchSch = function() {
            if (!$rootScope.schOption.startStation) {
                $rootScope.showWarn("请选择乘车站");
                return;
            }
            if (!$rootScope.schOption.drvDate) {
                $rootScope.showWarn("请选择乘车日期");
                return;
            }
            if (!$rootScope.schOption.stopId) {
                $rootScope.showWarn("请选择目的站");
                return;
            }
            $state.go('tab.sch')
        };

        $scope.$on("$ionicView.beforeEnter", function() {
            if (!$rootScope.city || $rootScope.city.length == 0) {
                vm.init();
            }
            //  $ionicHistory.clearCache();
            $ionicHistory.clearHistory();
        });
        //$scope.$on('$ionicView.beforeEnter', function () {
        //    $ionicHistory.clearHistory();
        //});
    })
    //tab页
    .controller('TabCtrl', function($scope, $rootScope, $state) {
        $scope.chanState = function() {
            var statename = $state.current.name;
            //tabs中存在的主页面不需要隐藏，hidetabs=false
            if (statename === 'tab.home' ||
                statename === 'tab.myorder' ||
                statename === 'tab.account' ||
                statename === 'tab.news'
            ) {
                $rootScope.hideTabs = false;
            } else {
                $rootScope.hideTabs = true;
            }
        };
        $rootScope.$on('$ionicView.afterEnter', function() {
            $scope.chanState();
        });
        $rootScope.$on('$ionicView.beforeEnter', function() {
            $scope.chanState();
        });
    })
    //目的站选择
    .controller('StopCtrl', function($scope, $rootScope, $state, localStorageService) {
        var vm = $scope;

        vm.historyItems = [];
        var ary = localStorageService.getObject('historyItems');
        if (ary instanceof Array) {
            vm.historyItems = ary;
        }

        vm.slectStop = function(item) {
            $rootScope.schOption.stopName = item.stopName;
            $rootScope.schOption.stopId = item.stopId;

            var exit = false;
            for (var i in vm.historyItems) {
                if (vm.historyItems[i].stopId == item.stopId) {
                    exit = true;
                    //先删除
                    vm.historyItems.splice(i, 1);
                    //并且把查到的移动的第一个位置
                    vm.historyItems.unshift({
                        stopName: item.stopName,
                        stopId: item.stopId,
                        stopAbbri: item.stopAbbri
                    });
                    break;
                }
            }
            if (!exit) {
                //如果不存在
                vm.historyItems.unshift({
                    stopName: item.stopName,
                    stopId: item.stopId,
                    stopAbbri: item.stopAbbri
                }); //把最新查的插入到最前面
            }
            if (vm.historyItems.length > 5) {
                //如果长度大于5的话，删除5以后的
                vm.historyItems.splice(5, vm.historyItems.length - 5);
            }
            var histroyStops = [];
            for (var i in vm.historyItems) {
                histroyStops.push({
                    stopName: vm.historyItems[i].stopName,
                    stopId: vm.historyItems[i].stopId,
                    stopAbbri: vm.historyItems[i].stopAbbri
                });
            }

            localStorageService.setObject('historyItems', histroyStops);
            console.log(vm.historyItems);
            $state.go('tab.home');
        }

        $scope.$on("$ionicView.beforeEnter", function() {
            vm.searchKey = '';
        });
    })
    //班次查询
    .controller('SchCtrl', function($scope, $rootScope, $state, appService, $ionicLoading) {
        var vm = $scope;
        vm.items = [];
        vm.control = {
            isDisAbleBefor: false,
            isDisAbleAfter: false
        };
        vm.checkbtn = function() {

            var drvdate = $scope.schOption.drvDate; //查询的时间 带时间
            var nowdate = new Date(vm.query.selectedStation.systemDate); //系统时间  不带时间
            // console.log(drvdate, nowdate)
            var predate = nowdate.valueOf()
            predate = predate + vm.query.selectedStation.preSaleDay * 24 * 60 * 60 * 1000
            predate = new Date(predate);
            if (drvdate <= nowdate) {
                $scope.control.isDisAbleBefor = true;
            } else {
                $scope.control.isDisAbleBefor = false;
            }
            if (drvdate >= predate) {
                $scope.control.isDisAbleAfter = true;
            } else {
                $scope.control.isDisAbleAfter = false;
            }
        };

        vm.searchSch = function() {
            $ionicLoading.show();
            var obj = {
                cityId: $scope.schOption.cityId,
                startStation: $scope.schOption.startStation,
                drvDate: $scope.schOption.drvDate,
                stopName: $scope.schOption.stopName
            }

            appService.post('GetSchPlan', obj).then(function(result) {
                vm.items = result.data.result;
            }, function(result) {

            }).finally(function(result) {
                $scope.checkbtn();
                $ionicLoading.hide();
            });

        };
        vm.querySch = function(bol) {
            if (bol) {
                //减一天
                //$scope.query.drvdate.setDate($scope.query.drvdate.getDate() - 1);
                $scope.schOption.drvDate = new Date($scope.schOption.drvDate.setDate($scope.schOption.drvDate.getDate() - 1)); // $scope.schOption.drvDate;
            } else {
                //加一天
                $scope.schOption.drvDate = new Date($scope.schOption.drvDate.setDate($scope.schOption.drvDate.getDate() + 1));
            }
            // vm.searchSch();
        }
        var bol = false;
        vm.$watch('schOption.drvDate', function(newValue, oldValue) {
            //console.log(newValue, oldValue);
            if (newValue != oldValue)
                vm.searchSch();
        }, true);

        vm.$on("$ionicView.afterEnter", function() {
            if ($rootScope.isRefreshSchQuery) {
                //   alert('xxx');
                vm.searchSch();
            } else {
                $rootScope.isRefreshSchQuery = true;
            }
        });


        //购票
        vm.chooseSchBuy = function(item) {
            $rootScope.sch = item;
            $state.go('tab.orderSch');
        };
    })
    //订单预定
    .controller('OrderSchCtrl', function($scope, $rootScope, appService, $state, $ionicLoading) {
        var vm = $scope;
        $rootScope.choosePersonNum = 0;

        vm.init = function() {
            //获取常用旅客
            $ionicLoading.show();
            appService.post('GetPassengers', {
                cityId: vm.sch.cityId,
                carryStationId: vm.sch.carryStaId,
            }).then(function(result) {
                $rootScope.passengers = result.data.result;
            }, function(result) {
                if (result.data && !result.data.unAuthorizedRequest)
                    vm.showError(result.data.error.message);
            }).finally(function(result) {
                $ionicLoading.hide();
            });
        };

        //初始化保险
        vm.initBaoXian = function() {
            appService.post('GetBaoxian', {
                cityId: vm.sch.cityId,
                carryStationId: vm.sch.carryStaId,
            }).then(function(result) {
                $rootScope.baoxians = result.data.result;
                console.log(result.data.result);
                for (var i in result.data.result) {
                    for (var j in result.data.result[i].products) {
                        if (result.data.result[i].products[j].defaultDisplay) {
                            $rootScope.selectedBaoxian = result.data.result[i].products[j];
                            break;
                        }
                    }
                }
            }, function(result) {}).finally(function(result) {});
        };

        //移除乘客
        vm.removePassenger = function(item) {
            item.check = false;
            vm.tongji();
        };
        vm.chooseBaoxian = function() {
            //选择保险
            $state.go('tab.chooseBaoxian');
        }
        vm.orderSch = function() {
            //订单提交
            var data = {
                sch: vm.sch,
                passengers: [], //乘客
                baoXianId: $rootScope.selectedBaoxian.id,
                passenger: null, //取票人
            };
            for (x in vm.passengers.passengers) {
                if (vm.passengers.passengers[x].check) {
                    data.passengers.push(vm.passengers.passengers[x]);
                }
            }
            if (data.passengers.length == 0) {
                //
                vm.showWarn('至少选择一个旅客');
                return;
            }

            $ionicLoading.show();
            appService.post('orderSch', data).then(function(result) {
                //下单成功 跳转到选择支付方式页面
                $state.go('tab.placeOrder')
            }, function(result) {
                vm.showError(result.data.error.message);

            }).finally(function(result) {
                $ionicLoading.hide();
            });
        }

        //工具方法 统计所选旅客 总人数、金额
        vm.tongji = function() {
            $rootScope.choosePersonNum = 0;
            $rootScope.sumPrice = 0;
            if (!vm.passengers) return;
            for (x in vm.passengers.passengers) {
                if (vm.passengers.passengers[x].check) {
                    if ($rootScope.choosePersonNum >= vm.passengers.num) {
                        item.check = false;
                        vm.showWarn('最多只允许选择【' + $rootScope.choosePersonNum + '】个乘客');
                        return;
                    }

                    $rootScope.choosePersonNum++;
                    var faceValue = 0;
                    for (var i in $rootScope.baoxians) {
                        for (var j in $rootScope.baoxians[i].products) {
                            if ($rootScope.baoxians[i].products[j].id == $rootScope.selectedBaoxian.id) {
                                //$rootScope.selectedBaoxian = $rootScope.baoxians[i].products[j].id;
                                faceValue = $rootScope.baoxians[i].products[j].faceValue;
                                break;
                            }
                        }
                    }
                    $rootScope.sumPrice += $scope.sch.fullPrice + faceValue;
                }
            }
        };
        //监控属性变化，执行这个方法
        // vm.$watch('passengers', function() {
        //     vm.tongji();
        // }, true);
        // vm.$watch('selectedBaoxian', function() {
        //     vm.tongji();
        // });

        vm.$on("$ionicView.beforeEnter", function() {
            vm.tongji();
        });

        vm.init();
        vm.initBaoXian();
    })
    //购票须知
    .controller('TicketNoticeCtrl', function($scope, $rootScope, $ionicLoading, appService, $sce) {
        var init = function() {
            $ionicLoading.show();
            appService.post('TicketNotice').then(function(result) {
                $scope.notice = $sce.trustAsHtml(result.data.result);
                console.log($scope.notice);
            }, function(result) {

            }).finally(function(result) {
                $ionicLoading.hide();
            });
        };
        init();
    })
    //选择保险
    .controller('ChooseBaoxianCtrl', function($scope, $rootScope, $ionicHistory, $stateParams, $state, $timeout, $ionicModal) {
        var vm = $scope;
        vm.baoxianChange = function(item) {
            $rootScope.selectedBaoxian = item;
            $ionicHistory.goBack(-1);
        };

        vm.showNotice = function(item) {
            $rootScope.baoXianNotice = item;
            $state.go('tab.baoXianNotice');
        }
    })
    //选择常用旅客
    .controller('ChoosePassengersCtrl', function($scope, $rootScope, $ionicHistory) {
        var vm = $scope;
        vm.itemCheckChange = function(item) {
            $rootScope.choosePersonNum = 0;
            $rootScope.sumPrice = 0;
            for (x in vm.passengers.passengers) {
                if (vm.passengers.passengers[x].check) {
                    if ($rootScope.choosePersonNum >= vm.passengers.num) {
                        item.check = false;
                        vm.showWarn('最多只允许选择【' + $rootScope.choosePersonNum + '】个乘客');
                        return;
                    }

                    $rootScope.choosePersonNum++;
                    var faceValue = 0;
                    var baoXian = vm.passengers.passengers[x].baoXian;
                    if (baoXian) faceValue = baoXian.faceValue;
                    $rootScope.sumPrice += $scope.sch.fullPrice + faceValue;
                }
            }
            //console.log(item);
            //item.check = !item.check;
        };

        vm.submitChoose = function() {
            $ionicHistory.goBack(-1);
        };
    })
    //添加或修改常用旅客
    .controller('CreateOrUpdatePassengerCtrl', function($scope, $rootScope, appService, $ionicLoading, $stateParams, $ionicHistory, $timeout) {
        var vm = $scope;
        vm.passengerId = $stateParams.passengerId;
        vm.title = "添加旅客";
        vm.formModel = {
            id: vm.passengerId
        };
        vm.save = function() {
            $ionicLoading.show();
            var action = "AddPassenger"
            if (vm.passengerId) {
                //修改
                action = "UpdatePassenger"
            }
            appService.post(action, vm.formModel).then(function(result) {
                if (!vm.passengerId) {
                    $rootScope.passengers.passengers.unshift(vm.formModel);
                } else {
                    //旅客信息发生改变。需要发布事件重新获取乘客信息
                    for (x in $rootScope.passengers.passengers) {
                        if ($rootScope.passengers.passengers[x].id == vm.passengerId) {
                            $rootScope.passengers.passengers[x].name = vm.formModel.name;
                            $rootScope.passengers.passengers[x].phone = vm.formModel.phone;
                            $rootScope.passengers.passengers[x].idCard = vm.formModel.idCard;
                            break;
                        }
                    }
                }

                //返回
                $ionicHistory.goBack(-1);
            }, function(result) {
                vm.showError(result.data.error.message);
            }).finally(function(result) {
                $ionicLoading.hide();
            });

        };
        var init = function() {

            if (vm.passengerId) {
                //修改
                vm.title = "修改旅客";
                $ionicLoading.show();
                appService.post('GetPassenger', { id: vm.passengerId }).then(function(result) {
                    vm.formModel = result.data.result;
                }, function(result) {

                }).finally(function(result) {
                    $ionicLoading.hide();
                });
            };
        };

        init();
    })
    //下单
    .controller('PlaceOrderCtrl', function($scope, $http, $rootScope, appService, $state, $ionicLoading, $stateParams, $ionicHistory, $timeout) {
        var vm = $scope;
        if (!vm.sch) {
            $state.go('tab.home');
        };

        vm.data = {};
        vm.init = function() {
            $ionicLoading.show();
            appService.post('LoadAllPaymentMethods').then(function(result) {
                vm.paymentMethods = result.data.result;
            }, function(result) {
                vm.showError(result.data.error.message);
            }).then(function(result) {
                $ionicLoading.hide();
            });
        }
        vm.getPayPictureUrl = function(url) {
            return abp.appPath + 'Common/Images/PayMent/' + url + '.png';
        };
        vm.serverSideChange = function(item) {
            console.log(item, vm.data);
        };
        vm.saveOrder = function() {
            //生成订单
            $ionicLoading.show();
            appService.post('PlaceOrder', {
                payMethodSystemName: vm.data.payMethodSystemName,
                isUserPoint: true,
                orderSourceId: abp.orderSourceId
            }).then(function(result) {
                vm.showSuccess('下单成功', function() {                  
                    if (abp.versionType == 1) {
                        vm.OrderId = result.data.result.orderId
                        $rootScope.gobackActionUse = false;
                        if (vm.data.payMethodSystemName == 'AliPayMethod') {
                            navigator.alipay.payV2({
                                "sign": result.data.result.sign.sign,
                                "info": result.data.result.sign.info
                            }, function(msgCode) {
                                $rootScope.showTimeOut('<h4>【支付宝】支付,等待返回支付结果</h4>', 5, gobilldetail);
                                $rootScope.gobackActionUse = true;
                            }, function(msg) {
                                $rootScope.showTimeOut('<h4>【支付宝】支付,等待返回支付结果</h4>', 5, gobilldetail);
                                $rootScope.gobackActionUse = true;
                            });
                        } else if (vm.data.payMethodSystemName == 'WeiXinMethod') {
                            wxpay.payment(
                                result.data.result.sign,
                                function(msgCode) {
                                    $rootScope.showTimeOut('<h4>【微信】支付,等待返回支付结果</h4>', 5, gobilldetail);
                                    $rootScope.gobackActionUse = true;
                                },
                                function(msg) {
                                    $rootScope.showTimeOut('<h4>【微信】支付,等待返回支付结果</h4>', 5, gobilldetail);
                                    $rootScope.gobackActionUse = true;
                                }
                            );
                        } else {
                            //积分
                            $rootScope.showTimeOut('<h3>积分支付 支付成功</h3>', 5, gobilldetail);
                            $rootScope.gobackActionUse = true;

                        }
                    } else {
                        window.location.href = '/pay';
                    }

                });


            }, function(result) {
                vm.showError(result.data.error.message, function() {
                    if (result.data.error.code == 100) {
                        //到班次查询
                        $state.go('tab.home');

                    } else if (result.data.error.code == 101) {
                        //到订单中心
                        $state.go('tab.myorder')
                    } else {
                        //$state.go('tab.home');
                    }
                });
            }).then(function(result) {
                $ionicLoading.hide();
            });
        };

        var gobilldetail = function() {
            $state.go('tab.myorder', { orderId: vm.OrderId })
        }

        vm.$on("$ionicView.beforeEnter", function() {
            vm.init();
        });

    })
    //登录
    .controller('LoginCtrl', function($scope, $rootScope, appService, $state, $ionicLoading, $stateParams, localStorageService) {
        var vm = $scope;
        var from = $scope.from;
        if ($stateParams.from) {
            from = $stateParams.from;
        }

        $rootScope.closeModal = function(result, backStep) {
            vm.closeModal(result, backStep);
        }

        vm.myLogin = {};
        vm.loginSys = function() {
            $ionicLoading.show();
            appService.login(vm.myLogin).then(function(result) {

                ////存储用户名 以备用户下次登录
                if (abp.versionType == 1) {
                    $rootScope.token = result.data.result;
                    localStorageService.set("token", result.data.result);
                }

                $rootScope.$broadcast('loginOrlogout');
                vm.showSuccess('登录成功', function() {
                    vm.closeModal('', 0);
                    if (from && from != 'register' && from != 'login') {
                        $state.go(from, {}, { reload: true });
                    } else {
                        $state.go('tab.home');
                    }
                });

            }, function(result) {
                if (result.data.error) {
                    if (result.data.error.details) {
                        $scope.showError(result.data.error.details);
                    } else {
                        $scope.showError(result.data.error.message);
                    }
                } else {
                    $scope.showError('手机号或者密码错误');
                }
            }).finally(function(result) {
                $ionicLoading.hide();
            });
        };

        vm.goForgetPW = function() {
            vm.closeModal('', -1);
            $state.go('tab.forgetpw', { from: from })
        }

        vm.close = function() {
            vm.closeModal('', 0);
        }

        vm.regist = function() {
            vm.closeModal('', 0);
            $state.go('tab.regist', { from: from })
        }
    })
    //注册
    .controller('RegistCtrl', function($scope, $ionicPopup, $rootScope, appService, $state, $ionicLoading, $ionicHistory, $stateParams, localStorageService) {
        var vm = $scope;
        $rootScope.isSended = false;
        vm.formData = {
            phone: ''
        };
        var from = $stateParams.from;
        var deviceNo = Math.random() * 10000000000;
        vm.countdown = 60;
        vm.sendSms = function() {
            $scope.data = {
                imageCode: '',
                imageSrc: abp.appPath + 'home/GetCaptcha?deviceNo=' + deviceNo + '&n=' + (new Date()).valueOf()
            };
            $scope.changeImag = function() { //abp.appPath
                $scope.data.imageSrc = abp.appPath + 'home/GetCaptcha?deviceNo=' + deviceNo + '&n=' + (new Date()).valueOf();
            };

            var tmp = '<img ng-src="{{data.imageSrc}}" ng-click="changeImag()" /></img><input type="text" ng-model="data.imageCode"  placeholder="请输入图片验证码">';
            var myPopup = $ionicPopup.show({
                template: tmp,
                title: '输入图片验证码',
                subTitle: '请输入下方图片上的字符点击发送按钮',
                scope: $scope,
                buttons: [
                    { text: '关闭' },
                    {
                        text: '<b>确定</b>',
                        type: 'button-positive',
                        onTap: function(e) {
                            console.log($scope.data);
                            if (!$scope.data.imageCode) {
                                e.preventDefault();
                            } else {
                                return $scope.data;
                            }
                        }
                    },
                ]
            });
            myPopup.then(function(res) {
                if (!res) return;
                appService.post("SendCode", {
                    phone: $scope.formData.phone,
                    deviceNo: deviceNo,
                    captchaCode: res.imageCode
                }).then(function(result) {
                    vm.isSended = true;
                    $scope.countdown = 60;
                    var interval = setInterval(function() {
                        $scope.countdown--;
                        $scope.$digest();
                    }, 1000);

                    var timer = setTimeout(function() {
                        vm.isSended = false;
                        $scope.countdown = 60;
                        $scope.$digest();
                    }, 60000);
                }, function(result) {

                    vm.showError(result.data.error.message, function() {
                        if (result.data.error.code != 100) {
                            vm.sendSms();
                        }
                    });
                    //vm.sendSms();
                }).finally(function() {
                    //$ionicLoading.hide();
                });
            });
        };
        vm.save = function() {
            $ionicLoading.show();
            appService.post('Register', vm.formData).then(function(result) {
                //注册成功
                vm.saveSuccLogin({ UsernameOrEmailAddress: vm.formData.phone, Password: vm.formData.password });
            }, function(result) {
                //   vm.saveSuccLogin(vm.formData);
                //注册失败
                vm.showError(result.data.error.message);
            }).finally(function(result) {
                $ionicLoading.hide();
            });

        };

        vm.saveSuccLogin = function(form) {
            vm.showSuccess('注册成功', function() {
                appService.login(form).then(function(result) {
                    ////存储用户名 以备用户下次登录
                    if (abp.versionType == 1) {
                        $rootScope.token = result.data.result;
                        localStorageService.set("token", result.data.result);
                    }

                    $rootScope.$broadcast('loginOrlogout');
                    vm.closeModal('', 0);
                    if (from && from != 'register' && from != 'login') {
                        $state.go(from, {}, { reload: true });
                    } else {
                        $state.go('tab.home');
                    }

                });
                // $ionicHistory.goBack(-1);
                $rootScope.closeModal('', -1);

                // $ionicHistory.goBack(-1);
            });
        }


    })
    //忘记密码
    .controller('ForgetPwCtrl', function($scope, $ionicPopup, $rootScope, appService, $state, $ionicLoading, $ionicHistory, $stateParams, localStorageService) {
        var vm = $scope;
        $rootScope.isSended = false;
        vm.formData = {
            phone: ''
        };
        var from = $stateParams.from;
        var deviceNo = Math.random() * 10000000000;
        vm.countdown = 60;
        vm.sendSms = function() {
            $scope.data = {
                imageCode: '',
                imageSrc: abp.appPath + 'home/GetCaptcha?deviceNo=' + deviceNo + '&n=' + (new Date()).valueOf()
            };
            $scope.changeImag = function() { //abp.appPath
                $scope.data.imageSrc = abp.appPath + 'home/GetCaptcha?deviceNo=' + deviceNo + '&n=' + (new Date()).valueOf();
            };

            var tmp = '<img ng-src="{{data.imageSrc}}" ng-click="changeImag()" /></img><input type="text" ng-model="data.imageCode"  placeholder="请输入图片验证码">';
            var myPopup = $ionicPopup.show({
                template: tmp,
                title: '输入图片验证码',
                subTitle: '请输入下方图片上的字符点击发送按钮',
                scope: $scope,
                buttons: [
                    { text: '关闭' },
                    {
                        text: '<b>确定</b>',
                        type: 'button-positive',
                        onTap: function(e) {
                            console.log($scope.data);
                            if (!$scope.data.imageCode) {
                                e.preventDefault();
                            } else {
                                return $scope.data;
                            }
                        }
                    },
                ]
            });
            myPopup.then(function(res) {
                if (!res) return;
                appService.post("SendCode", {
                    phone: $scope.formData.phone,
                    deviceNo: deviceNo,
                    captchaCode: res.imageCode
                }).then(function(result) {
                    vm.isSended = true;
                    $scope.countdown = 60;
                    var interval = setInterval(function() {
                        $scope.countdown--;
                        $scope.$digest();
                    }, 1000);

                    var timer = setTimeout(function() {
                        vm.isSended = false;
                        $scope.countdown = 60;
                        $scope.$digest();
                    }, 60000);
                }, function(result) {

                    vm.showError(result.data.error.message, function() {
                        if (result.data.error.code != 100) {
                            vm.sendSms();
                        }
                    });
                    //vm.sendSms();
                }).finally(function() {
                    //$ionicLoading.hide();
                });
            });
        };
        vm.save = function() {
            $ionicLoading.show();
            appService.post('ForgetPassword', vm.formData).then(function(result) {
                //修改密码成功
                vm.showSuccess('修改密码成功', function() {
                    $ionicHistory.goBack(-1);
                });
            }, function(result) {
                //修改密码失败
                vm.showError(result.data.error.message);
            }).finally(function(result) {
                $ionicLoading.hide();
            });
        };

    })
    //公告    
    .controller('NewsCtrl', function($scope, $ionicPopup, $rootScope, appService, $state, $ionicLoading) {

        var vm = $scope;
        vm.items = [];
        vm.requestParams = {
            maxResultCount: 15,
            skipCount: 0
        }
        vm.getAll = function() {
            $ionicLoading.show();
            appService.post('NewsList', vm.requestParams).then(function(result) {
                vm.totalCount = result.data.result.totalCount;
                if (vm.requestParams.skipCount == 0) {
                    vm.items = result.data.result.items;
                } else {
                    vm.items = vm.items.concat(result.data.result.items);
                }
            }, function(result) {
                vm.showError(result.data.error.message);
            }).finally(function(result) {
                if (vm.requestParams.skipCount == 0)
                    $scope.$broadcast('scroll.refreshComplete');
                else
                    $scope.$broadcast("scroll.infiniteScrollComplete");
                $ionicLoading.hide();
            });
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

        vm.newsDetail = function(item) {
            $state.go('tab.newsDetail', { id: item.id })
        }
        vm.$on("$ionicView.afterEnter", function() {
            if (vm.items.length == 0) {
                vm.getAll();
            }
        });
    })
    //公告详情
    .controller('NewsDetailCtrl', function($scope, $ionicPopup, $rootScope, appService, $state, $ionicLoading, $stateParams, $sce) {
        var vm = $scope;
        vm.getAll = function() {
            $ionicLoading.show();
            appService.post('GetNews', { id: $stateParams.id }).then(function(result) {
                vm.item = result.data.result;
                vm.item.content = $sce.trustAsHtml(vm.item.content);
            }, function(result) {
                vm.showError(result.data.error.message);
            }).finally(function(result) {
                $ionicLoading.hide();
            });

        };

        vm.getAll();
    });