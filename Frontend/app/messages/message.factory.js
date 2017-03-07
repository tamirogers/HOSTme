(function() {
    'use strict';

    angular
        .module('app')
        .factory('MessageFactory', MessageFactory);

    MessageFactory.$inject = ['$http', '$q', 'baseAPI'];

    /* @ngInject */
    function MessageFactory($http, $q, baseAPI) {
        var service = {
            addConversation: addConversation,
            addMessage: addMessage,
            getMessages: getMessages
        };

        return service;

        function addConversation(conversation){
          return $http.post(baseAPI + 'Conversations', conversation);
        }

        function addMessage(message){
          return $http.post(baseAPI + 'Messages', message);
        }

        function getMessages(id){
          return $http.get(baseAPI + 'UserMessages?id=' + id);
        }


    }
})();
