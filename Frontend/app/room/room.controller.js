(function() {
    'use strict';

    angular
        .module('app')
        .controller('RoomController', RoomController);

    RoomController.$inject = ['$state', 'RoomFactory','MessageFactory', '$stateParams', 'filepickerService', 'localStorageFactory', 'SweetAlert'];

    /* @ngInject */
    function RoomController($state, RoomFactory, MessageFactory, $stateParams, filepickerService, localStorageFactory, SweetAlert) {
        var rc = this;
        rc.addRoom = addRoom;
        rc.pickFile = pickFile;
        rc.getRoom = getRoom;
        rc.sendMessage = sendMessage;

        if(localStorageFactory.getKey("roomId")){
          getRoom();
        };

        function addRoom() {

                var room = {
                    'UserId': localStorageFactory.getKey("storedUserId"),
                    'RoomName': rc.rName,
                    'Description': rc.description,
                    'Address': rc.address,
                    'City': rc.city,
                    'State': rc.state,
                    'Zip': rc.zip,
                    'Price': rc.price,
                    'PictureUrl': rc.picUrl,
                    'Private': rc.private,
                    'GuestLimit': rc.guest,
                    'RoomNumber': rc.roomNumber
                }

                RoomFactory.addRoom(room).then(
                    function(response) {
                        SweetAlert.swal("Room Added!", "", "success");
                        $('input').val('');
                        $state.go('profile');
                    },
                    function(error) {
                        console.log(error);
                    })
        } //close addUser

        function pickFile(){
          filepickerService.pick(
            {mimetype: 'image/*',
             containter: 'modal',
             services: ['COMPUTER', 'FACEBOOK']},
             function onSuccess(Blob){
               rc.picUrl = Blob.url;
             }
            )
          }//close pickFile

          function getRoom(){

            var id = localStorageFactory.getKey("roomId");

            RoomFactory.getRoomById(id).then(
              function(response){
                rc.room = response.data;
              },
              function(error){
                console.log(error);
              }
            )
          }

          function sendMessage(id){
            //construct conversation

            var conversation = {
                                'SenderUserId': localStorageFactory.getKey("storedUserId"),
                                'ReceiverUserId': id
                              };
            //first send API call to start conversationId
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
                                'ConversationId': conId,
                                'Subject': rc.subject,
                                'Body': rc.body,
                                'DateCreated': new Date()
                              };

                MessageFactory.addMessage(message).then(
                  function(response){
                    SweetAlert.swal("Message Sent");
                  },
                  function(error){
                    console.log(error);
                    console.log("Error sending message")
                  }
                )
              },
              function(error){
                console.log(error);
                console.log("error sending conversation");
              }
            )

          }//close send message
    }
})();
