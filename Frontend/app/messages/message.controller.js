(function() {
    'use strict';

    angular
        .module('app')
        .controller('MessageController', MessageController);

    MessageController.$inject = ['MessageFactory', '$state', 'localStorageFactory', 'SweetAlert' ];

    /* @ngInject */
    function MessageController(MessageFactory, $state, localStorageFactory, SweetAlert)  {
        var mc = this;
        mc.activate = activate;
        mc.replyMessage = replyMessage;

        mc.receivedConvos = [];
        mc.sentConvos = [];

        activate();

        function activate(){

          var id = localStorageFactory.getKey("storedUserId");

          MessageFactory.getMessages(id).then(
            function(response){
              //console.log(response.data);
            var messages = response.data;
            for(var i = 0; i < messages.length; i++) {
              if(messages[i].receiverId == localStorageFactory.getKey("storedUserId")){
                mc.receivedConvos.push(messages[i]);
                //console.log(mc.receiveMessages);
              } else {
                mc.sentConvos.push(messages[i]);
                //console.log(mc.sentMessages);
              }
            }//close for loop

            },
            function(error){
              console.log(error);
            })
        }

        function replyMessage(m, c){

          var conversation = {
                              'SenderUserId': c.receiverId,
                              'ReceiverUserId': c.senderId
                            };
          MessageFactory.addConversation(conversation).then(
            function(response){
              var conId;
          //with response get conversationId
              if(response.data.conversationId[0] != null){
                 conId = response.data.conversationId[0];
              } else {
                 conId = response.data.conversationId;
              }

              var message = {
                            "ConversationId": conId,
                            "Subject": "Reply: " + m.subject,
                            "Body": mc.body,
                            "DateCreated": new Date()
                          };

              MessageFactory.addMessage(message).then(
                function(response){
                  SweetAlert.swal("Message Sent");
                  $('input').val('');
                },
                function(error){
                  console.log(error);
                  console.log("message error");
                })

            },
            function(error){
              console.log(error);
              console.log("conversation error");
            }
          )








        }//close replyMessage

    }
})();
