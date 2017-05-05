namespace where2eat {

    angular.module('where2eat', ['ui.router', 'ngResource', 'ui.bootstrap']).config((
        $stateProvider: ng.ui.IStateProvider,
        $urlRouterProvider: ng.ui.IUrlRouterProvider,
        $locationProvider: ng.ILocationProvider
    ) => {
        // Define routes
        $stateProvider
            .state('event', {
                url: '/event',
                templateUrl: '/ngApp/views/event.html',
                controller: where2eat.Controllers.EventController,
                controllerAs: 'controller'
            })
            .state('login', {
                url: '/',
                templateUrl: '/ngApp/views/login.html',
                controller: where2eat.Controllers.LoginController,
                controllerAs: 'controller'
            })
            .state('register', {
                url: '/register',
                templateUrl: '/ngApp/views/register.html',
                controller: where2eat.Controllers.RegisterController,
                controllerAs: 'controller'
            })
            .state('externalRegister', {
                url: '/externalRegister',
                templateUrl: '/ngApp/views/externalRegister.html',
                controller: where2eat.Controllers.ExternalRegisterController,
                controllerAs: 'controller'
            }) 
            .state('option', {
                url: '/option',
                templateUrl: '/ngApp/views/option.html',
                controller: where2eat.Controllers.OptionController,
                controllerAs: 'controller'
            })
            .state('decision', {
                url: '/decision',
                templateUrl: '/ngApp/views/decision.html',
                controller: where2eat.Controllers.DecisionController,
                controllerAs: 'controller'
            })
            .state('notFound', {
                url: '/notFound',
                templateUrl: '/ngApp/views/notFound.html'
            });

        // Handle request for non-existent route
        $urlRouterProvider.otherwise('/notFound');

        // Enable HTML5 navigation
        $locationProvider.html5Mode(true);
    });

    
    angular.module('where2eat').factory('authInterceptor', (
        $q: ng.IQService,
        $window: ng.IWindowService,
        $location: ng.ILocationService
    ) =>
        ({
            request: function (config) {
                config.headers = config.headers || {};
                config.headers['X-Requested-With'] = 'XMLHttpRequest';
                return config;
            },
            responseError: function (rejection) {
                if (rejection.status === 401 || rejection.status === 403) {
                    $location.path('/login');
                }
                return $q.reject(rejection);
            }
        })
    );

    angular.module('where2eat').config(function ($httpProvider) {
        $httpProvider.interceptors.push('authInterceptor');
    });

    

}
