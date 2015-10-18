///**
// * Created by 凯森 on 2015/3/3.
// */
//var MongoClient = require('mongodb').MongoClient;
//var ObjectID = require('mongodb').ObjectID;
//var User=require('./user.js');
//function Message(msg,type,toId,toImage,toName,toDream,myId,myImage,myName,myDream,time) {
//    this.msg=msg;
//    this.type=type;
//    this.toId = toId;
//    this.toImage = toImage;
//    this.toName=toName;
//    this.toDream=toDream;
//    this.myId = myId;
//    this.myImage = myImage;
//    this.myName=myName;
//    this.myDream=myDream;
//    this.time=time;
//};
//
//module.exports=Message;
//
//
//Message.prototype.save=function(callback) {
//
//
//    var message = {
//
//        toId: this.toId,
//        toImage: this.toImage,
//        toName:this.toName,
//        toDream:this.toDream,
//        myId: this.myId,
//        myImage: this.myImage,
//        myName:this.myName,
//        myDream:this.myDream,
//        msg:this.msg,
//        type:this.type,
//        time:this.time
//    };
//
//    MongoClient.connect("mongodb://kks:112699@localhost:27017/weibo", function (err, db) {
//        if (err) {
//            return callback(err);
//        }
//
//        db.collection('msgs', function (err, collection) {
//            if (err) {
//                db.close();
//                return callback(err);
//            }
//
//            collection.insert(message, {
//                safe: true
//            }, function (err, message) {
//
//                db.close();
//                if (err) {
//                    return callback(err);
//                }
//
//
//            });
//
//        });
//    });
//};
//
//Message.get=function(toId,callback){
//    MongoClient.connect("mongodb://kks:112699@localhost:27017/weibo",function(err, db){
//        if(err){
//            return callback(err);
//        }
//        db.collection('msgs',function(err,collection){
//            if(err){
//                db.close();
//                return callback(err);
//            }
//
//
//            collection.find({toId:toId}).toArray(function(err,docs){
//                db.close();
//                if(err){
//                    return callback(err);
//                }
//                callback(null,docs);
//            });
//        });
//    });
//
//};
//
//Message.remove=function(toId,callback){
//    MongoClient.connect("mongodb://kks:112699@localhost:27017/weibo",function(err, db){
//        if(err){
//            return callback(err);
//        }
//        db.collection('msgs',function(err,collection){
//            if(err){
//                db.close();
//                return callback(err);
//            }
//            collection.remove({
//                toId:toId
//            },{
//                w:1
//            },function(err){
//                if(err){
//                    return callback(err);
//                }
//                callback(null);
//            });
//        });
//    });
//};