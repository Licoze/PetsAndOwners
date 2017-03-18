(function () {
    'use strict';

    var module = angular.module('PnOApp', ['angularUtils.directives.dirPagination', 'ngRoute']);
    module.config(function ($routeProvider) {
        $routeProvider.when('/Owners',
        {
            templateUrl: '../static/Owners.html',
            controller: 'OwnersController'
        });
        $routeProvider.when('/Owners/:ownerid/Pets',
        {
            templateUrl: '../static/Pets.html',
            controller: 'PetsController'
        });
        $routeProvider.otherwise({ redirectTo: '/Owners' });
    });
    module.config(function (paginationTemplateProvider) {
        paginationTemplateProvider.setPath('../scripts/dirPagination.tpl.html');
    });
})();