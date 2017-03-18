(function () {
    'use strict';

    angular
        .module('PnOApp')
        .controller('OwnersController', OwnersController);

    OwnersController.$inject = ['$location','$scope','$http'];

    function OwnersController($location,$scope,$http) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'OwnersController';

        activate();

        function activate() {
            //Get table + Set RowsCount;
            $http({ method: 'GET', url: '../api/Owners' }).then(SuccessTableLoad);
                function SuccessTableLoad(response) {
                    $scope.Owners = response.data;
                    $scope.RowsCount = response.data.length;
                    $scope.Owners.forEach(function(item,i,arr){                  
                        SetPetCount(item);
                        
                    });
                    
                }
                
                function SetPetCount(item) {
                    $http({ method: 'GET', url: '../api/Owners/' + item.OwnerId + '/Pets' }).then(success);
                    function success(response) {
                        item.PetCount = response.data.length;
                        $scope.$apply();
                    }
                }
            //Delete button click
                $scope.DeleteOwner = function ($index) {
                    var owner=$scope.Owners[$index];
                    $http({ method: 'DELETE', url: '../api/Owners/' + owner.OwnerId }).then(success);

                    function success(response) {
                        $scope.Owners.splice($index, 1);
                        $scope.RowsCount--;
                        $scope.$apply();
                    }
                }
            //Add button click
                $scope.AddOwner = function (name) {
                  var  _data = {Name:name}
                    $http({ method: 'POST', url: '../api/Owners/', data: _data }).then(success);

                    function success(response) {
                        var item = response.data;
                        SetPetCount(item);
                        $scope.Owners.push(item);
                        $scope.RowsCount++;
                        $scope.$apply();
                    }
                }
         }
    }
})();
