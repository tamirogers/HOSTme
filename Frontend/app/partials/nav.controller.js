(function() {
    'use strict';

    angular
        .module('app')
        .controller('NavController', NavController);

    NavController.$inject = ['$scope', '$rootScope', 'localStorageFactory'];

    /* @ngInject */
    function NavController($scope, $rootScope, localStorageFactory) {
        // var vm = this;

        // $scope.isHidden = true;
        $scope.isHidden = localStorageFactory.getKey("navBool");

        $scope.$on('logIn', function() {
          // console.log("logged in");
          // $scope.isHidden = false;
          localStorageFactory.setKey("navBool", false);
          $scope.isHidden = localStorageFactory.getKey("navBool");

        });

        $scope.$on('logOut', function() {
          // console.log("logged out");
          // $scope.isHidden = true;
          localStorageFactory.setKey("navBool", true);
          $scope.isHidden = localStorageFactory.getKey("navBool");
        });

    }
})();
