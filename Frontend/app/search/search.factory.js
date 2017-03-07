(function() {
    'use strict';

    angular
        .module('app')
        .factory('SearchFactory', SearchFactory);

    SearchFactory.$inject = ['$http', '$q', 'baseAPI'];

    /* @ngInject */
    function SearchFactory($http, $q, baseAPI) {
        var service = {
            
        };

        return service;


    }
})();
