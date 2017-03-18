(function () {
    'use strict';

    angular
        .module('PnOApp')
        .controller('PetsController', PetsController);

    PetsController.$inject = ['$location', '$scope', '$http', '$routeParams'];

    function PetsController($location, $scope, $http, $routeParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'PetsController';
        activate();
        
        function activate() {
            //Get table + Set RowsCount;
            $http({ method: 'GET', url: '../api/Owners/' + $routeParams.ownerid + '/Pets' }).then(SuccessTableLoad);
            function SuccessTableLoad(response) {
                $scope.Pets = response.data;
                $scope.RowsCount = response.data.length;
                $scope.$apply();
            }
            
            //Set OwnerName
            $http({ method: 'GET', url: '../api/Owners/' + $routeParams.ownerid }).then(
                function (response) {
                     $scope.OwnerName = response.data.Name+"' Pet";
                     $scope.$apply();
                 });
            //Delete button click
            $scope.DeletePet = function ($index) {
                var pet = $scope.Pets[$index];
                $http({ method: 'DELETE', url: '../api/Owners/' + $routeParams.ownerid + '/Pets/' + pet.PetId }).then(success);

                function success(response) {
                    $scope.Pets.splice($index, 1);
                    $scope.RowsCount--;
                    $scope.$apply();
                }
            }
            //Add button click
            $scope.AddPet = function (name) {
                var _data = { Name: name }
                $http({ method: 'POST', url: '../api/Owners/' + $routeParams.ownerid + '/Pets', data: _data }).then(success);

                function success(response) {
                    $scope.Pets.push(response.data);
                    $scope.RowsCount++;
                    $scope.$apply();
                }
            }
        }
    }
})();
