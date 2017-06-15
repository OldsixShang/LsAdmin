angular.module('starter.services', [])
    .factory('localStorageService', [function() {
        return {

            set: function(key, value) {
                localStorage[key] = value;
            },

            get: function(key, defaultValue) {
                return localStorage[key] || defaultValue;
            },

            setObject: function(key, value) {
                localStorage[key] = JSON.stringify(value);
            },

            getObject: function(key) {
                var str = localStorage[key] || '{}';
                if (!str) str = '{}';
                return JSON.parse(str);
            }

        }

    }])
    .factory('appService', [
        '$http', '$q', '$timeout', '$rootScope', 'localStorageService',
        function($http, $q, $timeout, $rootScope, localStorageService) {
            return new function() {
                this.post = function(serveName, input, httpParams) {
                    var deferred = $q.defer();
                    $http(angular.extend({
                        url: abp.appPath + 'api/services/app/open/' + serveName,
                        method: 'POST',
                        data: JSON.stringify(input)
                    }, httpParams)).then(function(data) {
                        console.log('succ ..............');
                        deferred.resolve(data);
                    }, function(error) {
                        console.log('error ..............');
                        console.log(JSON.stringify(error));
                        if (!error.data) {
                            error.data = { error: { message: '连接服务器失败！' } };
                        }
                        deferred.reject(error);
                    })
                    return deferred.promise;

                };
                this.login = function(input, httpParams) {
                    if (abp.versionType == 1) {
                        return $http(angular.extend({
                            url: abp.appPath + 'api/Account/Authenticate',
                            method: 'POST',
                            data: input
                        }, httpParams));
                    } else {
                        return $http(angular.extend({
                            url: abp.appPath + 'Account/Login',
                            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                            transformRequest: function(obj) {
                                var str = [];
                                for (var p in obj) {
                                    str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                                }
                                return str.join("&");
                            },
                            method: 'POST',
                            data: input
                        }, httpParams));
                    }
                };

                this.loginOut = function(input, httpParams) {
                    if (abp.versionType == 1) {
                        var deferred = $q.defer();
                        $rootScope.token = null;
                        localStorageService.set('token', null);
                        $timeout(function() {
                            deferred.resolve();
                        }, 1000);
                        return deferred.promise;
                    } else {
                        return $http(angular.extend({
                            url: abp.appPath + 'Account/Logout?isApp=true',
                            method: 'POST',
                            data: input
                        }, httpParams));
                    }


                };
            };
        }
    ])
    .factory('ModalService', ['$ionicModal', '$rootScope', '$q', '$injector', '$controller', '$ionicHistory',
        function($ionicModal, $rootScope, $q, $injector, $controller, $ionicHistory) {
            return {
                show: show
            };

            function show(templateUrl, controller, parameters) {
                var deferred = $q.defer(),
                    ctrlInstance,
                    modalScope = $rootScope.$new(),
                    thisScopeId = modalScope.$id;

                $ionicModal.fromTemplateUrl(templateUrl, {
                    scope: modalScope,
                    animation: 'slide-in-up'
                }).then(function(modal) {
                    modalScope.modal = modal;

                    modalScope.openModal = function() {
                        modalScope.modal = show();
                    };

                    modalScope.closeModal = function(result, backStep) {
                        deferred.resolve(result);
                        if (backStep != 0) {
                            $ionicHistory.goBack(backStep);
                        }
                        modalScope.modal.hide();
                    };


                    modalScope.$on('modal.hidden', function(thisModal) {
                        if (thisModal.currentScope) {
                            var modalScopeId = thisModal.currentScope.$id;
                            if (thisScopeId === modalScopeId) {
                                deferred.resolve(null);
                                _cleanup(thisModal.currentScope);
                            }
                        }
                    });
                    modalScope.from = parameters.from;
                    //Invoke the controller
                    var locals = { '$scope': modalScope, 'parameters': parameters };
                    var ctrlEval = _evalController(controller);
                    ctrlInstance = $controller(controller, locals);
                    if (ctrlEval.isControllerAs) {
                        ctrlInstance.openModal = modalScope.openModal;
                        ctrlInstance.closeModal = modalScope.closeModal;
                        ctrlInstance.from = modalScope.from;
                    }

                    modalScope.modal.show();
                }, function(err) {
                    deferred.reject(err);
                });

                return deferred.promise;
            }

            function _cleanup(scope) {
                scope.$destroy();
                if (scope.modal) {
                    scope.modal.remove();
                }
            }

            function _evalController(ctrlName) {
                var result = {
                    isControllerAs: false,
                    controllerName: '',
                    propName: ''
                };
                var fragments = (ctrlName || '').trim().split(/\s+/);
                result.isControllerAs = fragments.length === 3 && (fragments[1] || '').toLowerCase() === 'as';
                if (result.isControllerAs) {
                    result.controllerName = fragments[0];
                    result.propName = fragments[2];
                } else {
                    result.controllerName = ctrlName;
                }

                return result;
            }
        }
    ])

.factory('$cordovaFileTransfer', ['$q', '$timeout', function($q, $timeout) {
    return {
        download: function(source, filePath, options, trustAllHosts) {
            var q = $q.defer();
            var ft = new FileTransfer();
            var uri = (options && options.encodeURI === false) ? source : encodeURI(source);

            if (options && options.timeout !== undefined && options.timeout !== null) {
                $timeout(function() {
                    ft.abort();
                }, options.timeout);
                options.timeout = null;
            }

            ft.onprogress = function(progress) {
                q.notify(progress);
            };

            q.promise.abort = function() {
                ft.abort();
            };

            ft.download(uri, filePath, q.resolve, q.reject, trustAllHosts, options);
            return q.promise;
        },

        upload: function(server, filePath, options, trustAllHosts) {
            var q = $q.defer();
            var ft = new FileTransfer();
            var uri = (options && options.encodeURI === false) ? server : encodeURI(server);

            if (options && options.timeout !== undefined && options.timeout !== null) {
                $timeout(function() {
                    ft.abort();
                }, options.timeout);
                options.timeout = null;
            }

            ft.onprogress = function(progress) {
                q.notify(progress);
            };

            q.promise.abort = function() {
                ft.abort();
            };

            ft.upload(filePath, uri, q.resolve, q.reject, options, trustAllHosts);
            return q.promise;
        }
    };
}])

.factory('$cordovaFileOpener2', ['$q', function($q) {

    return {
        open: function(file, type) {
            var q = $q.defer();
            cordova.plugins.fileOpener2.open(file, type, {
                error: function(e) {
                    q.reject(e);
                },
                success: function() {
                    q.resolve();
                }
            });
            return q.promise;
        },

        uninstall: function(pack) {
            var q = $q.defer();
            cordova.plugins.fileOpener2.uninstall(pack, {
                error: function(e) {
                    q.reject(e);
                },
                success: function() {
                    q.resolve();
                }
            });
            return q.promise;
        },

        appIsInstalled: function(pack) {
            var q = $q.defer();
            cordova.plugins.fileOpener2.appIsInstalled(pack, {
                success: function(res) {
                    q.resolve(res);
                }
            });
            return q.promise;
        }
    };
}]);