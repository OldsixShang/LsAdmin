angular.module('account.controllers', [])
    //用户中心
    .controller('AccountCtrl', function($scope, $rootScope, appService, $state, $ionicLoading, $ionicHistory, ModalService) {
        var vm = $scope;
        vm.isApp = false;
        if (abp.versionType == 1) {
            vm.isApp = true;
        }
        vm.user = null;
        vm.init = function() {
            $ionicLoading.show();
            appService.post('GetCurrentUserProfileForEdit').then(function(result) {
                vm.user = result.data.result;
            }, function(result) {
                vm.user = null;
            }).finally(function(result) {
                vm.$broadcast('scroll.refreshComplete');
                $ionicLoading.hide();
            });
        };
        vm.logout = function() {
            //注销
            appService.loginOut().finally(function(result) {
                $ionicHistory.clearCache();
                $ionicHistory.clearHistory();
                $rootScope.$broadcast('loginOrlogout');
                $ionicLoading.hide();
            });
        }

        vm.login = function() {
            ModalService.show('AppPhone/templates/login.html', 'LoginCtrl', { 'from': $state.current.name });
        }


        vm.init();
        //$on用于事件
        $rootScope.$on("loginOrlogout", function(event, data) {
            vm.init();
        });
        $scope.$on("$ionicView.afterEnter", function() {
            $ionicHistory.clearHistory();
        });
    })
    .controller('PersonlCtrl', function($scope, $rootScope, appService, $ionicLoading) {
        var vm = $scope;
        vm.user = null;
        vm.init = function() {
            $ionicLoading.show();
            appService.post('GetCurrentUserProfileForEdit').then(function(result) {
                vm.user = result.data.result;
            }, function(result) {
                vm.user = null;
            }).finally(function(result) {
                vm.$broadcast('scroll.refreshComplete');
                $ionicLoading.hide();
            });
        };

        $scope.$on("$ionicView.afterEnter", function(event, toState) {
            vm.init();
        });


    })
    .controller('ModifyPasswordCtrl', function($scope, $rootScope, appService, $ionicLoading, $state) {

        var vm = $scope;
        vm.formData = {};
        vm.save = function() {
            $ionicLoading.show();
            appService.post('ChangePassword', vm.formData).then(function(result) {
                $ionicLoading.hide();
                vm.showSuccess('修改成功', function() {
                    $state.go('tab.account');
                });
            }, function(result) {
                vm.showError(result.data.error.message);
            }).finally(function(result) {
                $ionicLoading.hide();
            });
        };
    })
    .controller('PassengersCtrl', function($scope, $rootScope, appService, $ionicLoading, $state) {
        var vm = $scope;
        vm.items = [];
        vm.init = function() {
            $ionicLoading.show();
            appService.post('GetPassengerList').then(function(result) {
                vm.items = result.data.result;
            }, function(result) {

            }).finally(function(result) {
                $ionicLoading.hide();
            });
        };

        vm.edit = function(item) {
            $rootScope.modefyPassenger = item;
            $state.go('tab.modifyPassenger');
        };
        vm.create = function() {
            $rootScope.modefyPassenger = null;
            $state.go('tab.modifyPassenger');
        };
        vm.delete = function(item) {
            vm.confirm('确定', '你确定要删除此乘客吗?', function() {
                $ionicLoading.show();
                appService.post('DeletePassenger', { id: item.id }).then(function(result) {
                    vm.showSuccess('删除成功', function() {
                        vm.init();
                    });
                }, function(result) {
                    vm.showError('删除成功');
                }).finally(function(result) {
                    $ionicLoading.hide();
                });
            });

        };
        $scope.$on("$ionicView.afterEnter", function() {
            vm.init();
        });

    })
    .controller('ModifyPassengerCtrl', function($scope, appService, $ionicLoading, $ionicHistory) {
        var vm = $scope;
        vm.title = '修改常用乘客';
        vm.isupdate = true;
        if (!vm.modefyPassenger) {
            vm.isupdate = false;
            vm.modefyPassenger = {};
            vm.title = '添加常用乘客';
        }

        vm.item;
        vm.save = function() {
            $ionicLoading.show();
            var action = 'UpdatePassenger';
            if (!vm.isupdate) {
                action = 'AddPassenger';
            }
            appService.post(action, vm.modefyPassenger).then(function(result) {
                vm.showSuccess('保存成功', function() {
                    $ionicHistory.goBack(-1);
                });
            }, function(result) {
                vm.showError(result.data.error.message);
            }).finally(function(result) {
                $ionicLoading.hide();
            });
        };
    })
    .controller('PointCtrl', function($scope, appService, $ionicLoading) {
        var vm = $scope;
        vm.items = [];
        vm.requestParams = {
            maxResultCount: 10,
            skipCount: 0
        }
        vm.getAll = function() {
            appService.post('PointList', vm.requestParams).then(function(result) {
                vm.totalCount = result.data.result.totalCount;
                if (vm.requestParams.skipCount == 0) {
                    vm.items = result.data.result.items;
                } else {
                    vm.items = vm.items.concat(result.data.result.items);
                }
            }, function(result) {
                vm.showError(result.data.error.message);
            }).finally(function(result) {
                $scope.$broadcast('scroll.refreshComplete');
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

        vm.getAll();

    })
    .controller('ReturnRequestCtrl', function($scope, appService, $ionicLoading) {})
    //免责声明
    .controller('MianzeCtrl', function($scope, appService, $ionicLoading, $sce) {
        var init = function() {
            $ionicLoading.show();
            appService.post('GetArticle', { systemName: 'mianze' }).then(function(result) {
                $scope.notice = $sce.trustAsHtml(result.data.result.content);
                console.log($scope.notice);
            }, function(result) {

            }).finally(function(result) {
                $ionicLoading.hide();
            });
        };
        init();
    })
    //关于我们
    .controller('AboutCtrl', function($scope, appService, $ionicLoading, $sce) {
        var init = function() {
            $ionicLoading.show();
            appService.post('GetArticle', { systemName: 'aboutus' }).then(function(result) {
                $scope.notice = $sce.trustAsHtml(result.data.result.content);
                console.log($scope.notice);
            }, function(result) {

            }).finally(function(result) {
                $ionicLoading.hide();
            });
        };
        init();
    })
    //版本信息
    .controller('VesionCtrl', function($scope, $rootScope, appService, $ionicLoading, $sce) {
        $scope.checkUpdate = function() {
            $rootScope.checkUpdate(true);
        }


    })

//投诉建议
.controller('SuggestCtrl', function($scope, appService, $state, $ionicLoading, $ionicHistory) {
        var vm = $scope;

        var vm = $scope;
        vm.items = [];
        vm.requestParams = {
            maxResultCount: 10,
            skipCount: 0
        }
        vm.getAll = function() {
            appService.post('SuggestList', vm.requestParams).then(function(result) {
                vm.totalCount = result.data.result.totalCount;
                if (vm.requestParams.skipCount == 0) {
                    vm.items = result.data.result.items;
                } else {
                    vm.items = vm.items.concat(result.data.result.items);
                }
            }, function(result) {
                vm.showError(result.data.error.message);
            }).finally(function(result) {
                if (vm.requestParams.skipCount == 0) {
                    $scope.$broadcast('scroll.refreshComplete');
                } else {
                    $scope.$broadcast("scroll.infiniteScrollComplete");
                }

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

        vm.create = function() {
            $state.go('tab.suggestadd');
        };
        // 刷新
        vm.$on("$ionicView.afterEnter", function() {
            vm.getAll();
        });


    })
    //投诉建议
    .controller('SuggestaddCtrl', function($scope, $state, appService, $ionicLoading, $ionicHistory) {
        var vm = $scope;

        vm.form = {};
        vm.save = function() {
            $ionicLoading.show();
            appService.post('suggest', vm.form).then(function(result) {
                vm.showSuccess("提交成功，感谢您宝贵的意见。", function() {
                    //  $ionicHistory.goBack(-1);
                    $state.go('tab.suggest');

                });
            }, function(result) {
                vm.showError(result.data.error.message);
            }).finally(function(result) {
                $ionicLoading.hide();
            });
        };


    })