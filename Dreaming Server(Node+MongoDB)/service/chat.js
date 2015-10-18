/**
 * Created by 凯森 on 2015/3/3.
 */
var wpPush=require('./wpPushNotify');
var User = require('../modles').User;
var url=require('url');
var users ={};
module.exports=function(server){

    var io=require('socket.io').listen(server);
    io.sockets.on('connection',function(socket){

        var userId;
        socket.on('login',function(data){
            users[data]= socket;
            userId=data;

        });

        socket.on('chat',function (data) {

            var msgJson=JSON.parse(data);

            if(users[msgJson.toPhone]){
                users[msgJson.toPhone].emit('chat',data);

            }
            else{
                User.getUserByPhone(msgJson.toPhone,function(err,user){
                   var path= (url.parse(user.microsoft)).path;
                        console.log('推送');
                       if(path){
                           wpPush(path,'1|'+data);

                       }
                })
            }

        });

        socket.on('disconnect', function () {
            delete users[userId];
        });
    });
}




