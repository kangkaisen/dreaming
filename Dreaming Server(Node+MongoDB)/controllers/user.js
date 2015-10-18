/**
 * Created by 凯森 on 2015/3/3.
 */
var path = require('path');
var User = require('../modles').User;
var wpPush=require('../service/wpPushNotify.js');
var crypto = require('crypto');

//用户注册
exports.signin = function (req, res) {

    var md5 = crypto.createHash('md5'),
        word = md5.update(req.body.password).digest('hex');

    User.getUserByPhone(req.body.phone, function (err, user) {
        if (user) {
            return res.json({code: 1});//用户已经存在
        }

        User.newAndSave(req.body.phone, word, req.body.date,function (err, user) {
            if (err) {
                return res.json({code: 2}); //未知错误
            }


            return res.json({code: 0, user: user});
        });
    });
}

//用户登录
exports.login=function(req,res){

    var md5=crypto.createHash('md5'),
        word=md5.update(req.body.password).digest('hex');
    User.getUserByPhone(req.body.phone,function(err,user){

        if(!user){
            return res.json({code:1});
        }

        if(user.password!==word){
            return res.json({code:2})
        }


        return res.json({code:0,user:user});

    });
}
//用户更新资料
exports.update=function(req,res){
    User.update(req.body.phone,req.body.name,req.body.dream,path.basename(req.files.picture.path),function(err,user){
        if(err){
            return res.sendStatus(404);
        }
        res.json(user);

    });
}
//用户信息
exports.info=function(req,res){

    User.getUserByPhone(req.params.phone, function (err, user) {

        if (user) {
            return res.json(user);
        }
        else{
            return res.sendStatus(404);
        }


    });
}
//更新微软推送地址
exports.updateUrl=function(req,res){
    User.urlUpdate(req.body.phone,req.body.url,function(err,user){

        res.sendStatus(200);
    })
}

