(function() {
    'use strict';

    angular
        .module('app')
        .factory('localStorageFactory', localStorageFactory);

    localStorageFactory.$inject = ['localStorageService'];

    /* @ngInject */
    function localStorageFactory(localStorageService) {
        var service = {
            setKey: setKey,
            getKey: getKey,
            logOut: logOut
        };

        return service;

        function setKey(key, value){
          return localStorageService.set(key, value);
        }

        function getKey(key){
          return localStorageService.get(key);
        }

        function logOut(){
          return localStorageService.clearAll();
        }
    }
})();
